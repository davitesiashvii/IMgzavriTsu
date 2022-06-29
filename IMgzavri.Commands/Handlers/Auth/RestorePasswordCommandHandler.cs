using IMgzavri.Commands.Commands.Auth;
using IMgzavri.Commands.Models.ResultModels;
using IMgzavri.Infrastructure;
using IMgzavri.Infrastructure.Db;
using IMgzavri.Infrastructure.Service;
using IMgzavri.Shared.Domain.Models;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace IMgzavri.Commands.Handlers.Auth
{
    public class RestorePasswordCommandHandler : CommandHandler<RestorePasswordCommand>
    {
        public RestorePasswordCommandHandler(IMgzavriDbContext context, IAuthorizedUserService auth, IFileStorageService fileStorage) : base(context, auth, fileStorage)
        {
        }

        public override async Task<Result> HandleAsync(RestorePasswordCommand cmd, CancellationToken ct)
        {
            var user = await context.Users.FirstOrDefaultAsync(x=>x.Email == cmd.Email);
            if (user == null)
                return Result.Error("დაფიქსირდა ტექნიკური ხარვეზი");

            user.Password = cmd.Password;
            context.Update(user);

            var authResult = Auth.GenerateToken(user);

            await context.RefreshTokens.AddAsync(authResult.RefreshToken);
            await context.SaveChangesAsync();

            var res = new AuthenticationResult(authResult.Token, authResult.RefreshToken.Token, true);

            var result = new Result();
            result.Response = res;
            return result;
        }
    }
}
