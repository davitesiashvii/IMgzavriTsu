using IMgzavri.Api.Extensions;
using IMgzavri.Commands;
using IMgzavri.Infrastructure.Db;
using IMgzavri.Infrastructure.Service;
using IMgzavri.Queries;
using IMgzavri.Shared.Contracts;
using IMgzavri.Shared.Domain.Models;
using IMgzavri.Shared.ExternalServices;
using Microsoft.AspNetCore.Mvc.Infrastructure;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection.Extensions;
using Microsoft.Extensions.FileProviders;
using Microsoft.OpenApi.Models;
using System.Reflection;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

//InitializeDatabase(builder.Services);

builder.Services.Configure<IRecommendSettings>(builder.Configuration);
var config = builder.Configuration.Get<IRecommendSettings>();

builder.Services.AddControllers();
builder.Services.AddEndpointsApiExplorer();
//builder.Services.AddSwaggerGen();

builder.Services.AddRazorPages().AddRazorRuntimeCompilation();

builder.Services.AddJwtAndExternalAuthentication(builder.Configuration);
builder.Services.AddAuthentication();
builder.Services.AddAuthorization();

builder.Services.AddCors();
builder.Services.AddMvc();

builder.Services.AddMediator(o =>
{
    o.AddHandlersFromAssemblyOf<Command>();
    o.AddHandlersFromAssemblyOf<Query>();
});


//builder.Services.AddSwaggerGen(x =>
//{
//    x.SwaggerDoc("v1", new OpenApiInfo { Title = "Tweetbook API", Version = "v1" });

//    //x.ExampleFilters();

//    x.AddSecurityDefinition("Bearer", new OpenApiSecurityScheme
//    {
//        Description = "JWT Authorization header using the bearer scheme",
//        Name = "Authorization",
//        In = ParameterLocation.Header,
//        Type = SecuritySchemeType.ApiKey
//    });
//    x.AddSecurityRequirement(new OpenApiSecurityRequirement
//                {
//                    {new OpenApiSecurityScheme{Reference = new OpenApiReference
//                    {
//                        Id = "Bearer",
//                        Type = ReferenceType.SecurityScheme
//                    }}, new List<string>()}
//                });

    
//});


//builder.Services.AddClientForFileStorage(config.GlobalSettings.FileStorageClientName, config.GlobalSettings.FileStorageUrl);

builder.Services.AddDbContext<IMgzavriDbContext>(options => options.UseSqlServer(builder.Configuration.GetConnectionString("IMgzavriDbContext")));
builder.Services.Configure<MailSettings>(builder.Configuration.GetSection("MailSettings"));

builder.Services.TryAddSingleton<IHttpContextAccessor, HttpContextAccessor>();
builder.Services.TryAddSingleton<IActionContextAccessor, ActionContextAccessor>();

builder.Services.AddSingleton<IMailService, MailService>();

builder.Services.AddScoped<IFileStorageService, FileStorageService>();

builder.Services.AddScoped<IFileProcessor, FileProcessor>();

//InitializeDatabase(builder.Services);



var app = builder.Build();

// Configure the HTTP request pipeline.
//if (app.Environment.IsDevelopment())
//{
//    app.UseSwagger();
//    app.UseSwaggerUI(c =>
//    {
//        c.SwaggerEndpoint("/swagger/v1/swagger.json", "dotnetClaimAuthorization V1");
//    });
//}
app.UseDeveloperExceptionPage();

//app.UseCors(c => c.AllowAnyOrigin()
//               .WithOrigins(builder.Configuration.GetSection("GlobalSettings")["Origin"].Split(";"))
//               .WithMethods("GET", "POST", "PUT", "DELETE")
//               .AllowCredentials()
//               .AllowAnyHeader());

app.UseAuthentication();

var config1 = builder.Configuration.Get<IRecommendFileStorageSettings>();
app.UseStaticFiles();
app.UseFileServer(new FileServerOptions
{
    FileProvider = new
        PhysicalFileProvider(Path.Combine(config1.GlobalSettings.FileSystemBasePath, config1.GlobalSettings.MainFolderName)),
    RequestPath = new PathString(config1.GlobalSettings.FileServerRequestPath)
});

//app.UseHsts();
app.UseRouting();

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();


//void InitializeDatabase(IServiceCollection services)
//{
//    var sp = services.BuildServiceProvider();
//    var scopeFactory = sp.GetRequiredService<IServiceScopeFactory>();
//    using var scope = scopeFactory.CreateScope();
//    using var context = sp.GetService(typeof(IMgzavriDbContext)) as IMgzavriDbContext;
//    context.Database.Migrate();
//}


