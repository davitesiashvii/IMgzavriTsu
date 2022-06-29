using IMgzavri.Commands.Commands.Auth;
using IMgzavri.Commands.Models.ResultModels;
using IMgzavri.Domain.Models;
using IMgzavri.Infrastructure;
using IMgzavri.Infrastructure.Db;
using IMgzavri.Infrastructure.Service;
using IMgzavri.Shared.Domain.Models;
using Microsoft.EntityFrameworkCore;

namespace IMgzavri.Commands.Handlers.Auth
{
    public class RegisterUserCommandHandler : CommandHandler<RegisterUserCommand>
    {
        public RegisterUserCommandHandler(IMgzavriDbContext context, IAuthorizedUserService auth, IFileStorageService fileStorage) : base(context, auth, fileStorage)
        {
        }

        public override async Task<Result> HandleAsync(RegisterUserCommand cmd, CancellationToken ct)
        {
            var existingUser = await context.Users.FirstOrDefaultAsync(x => x.Email == cmd.Email);

            if (existingUser != null )
                return Result.Error($"იუზერი ამ მეილით {cmd.Email} უკვე დარეგისტრირებულია");

            var user = new Users()
            {
                Id = Guid.NewGuid(),
                Email = cmd.Email,
                FirstName = cmd.FirstName,
                LastName = cmd.LastName,
                Password = cmd.Password,
                IdNumber = cmd.IdNumber,
                MobileNumber = cmd.MobileNumber,
                CreateDate = DateTime.Now,
                VerifyUser = false
            };

            var authResult = Auth.GenerateToken(user);

            authResult.RefreshToken.User = user;
            
            await context.RefreshTokens.AddAsync(authResult.RefreshToken);
            await context.SaveChangesAsync();


            var res = new AuthenticationResult(authResult.Token, authResult.RefreshToken.Token, true);

            var result = new Result();
            result.Response = res;

            return result;
        }
    }
}
