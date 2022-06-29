using IMgzavri.Commands.Commands.Auth;
using IMgzavri.Commands.Models.ResultModels;
using IMgzavri.Infrastructure;
using IMgzavri.Infrastructure.Db;
using IMgzavri.Infrastructure.Service;
using IMgzavri.Shared.Contracts;
using IMgzavri.Shared.Domain.Models;
using Microsoft.EntityFrameworkCore;

namespace IMgzavri.Commands.Handlers.Auth
{
    public class RefreshTokenCommandHandler : CommandHandler<RefreshTokenCommand>
    {
        public RefreshTokenCommandHandler(IMgzavriDbContext context, IAuthorizedUserService auth, IFileStorageService fileStorage) : base(context, auth, fileStorage)
        {
        }

        public override async Task<Result> HandleAsync(RefreshTokenCommand cmd, CancellationToken ct)
        {
            if (cmd.Token == null || cmd.RefreshToken == null)
                return Result.Error("Invalid Token");

            var refreshToken = await context.RefreshTokens.FirstOrDefaultAsync(x=>x.Token == cmd.RefreshToken);

            if(refreshToken == null || refreshToken.Token != cmd.RefreshToken || refreshToken.ExpiryDate <= DateTime.Now)
                return Result.Error("Invalid Token");

            var validatedToken = Auth.GetPrincipalFromToken(cmd.Token);

            if (validatedToken == null)
                return  Result.Error("Invalid Token");

            var user = await context.Users.FirstOrDefaultAsync(x => x.Id == refreshToken.UserId);

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
