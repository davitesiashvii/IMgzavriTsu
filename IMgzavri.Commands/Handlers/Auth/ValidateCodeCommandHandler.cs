using IMgzavri.Commands.Commands.Auth;
using IMgzavri.Infrastructure;
using IMgzavri.Infrastructure.Db;
using IMgzavri.Infrastructure.Service;
using IMgzavri.Shared.Domain.Models;
using Microsoft.EntityFrameworkCore;

namespace IMgzavri.Commands.Handlers.Auth
{
    public class ValidateCodeCommandHandler : CommandHandler<ValidateCodeCommand>
    {
        public ValidateCodeCommandHandler(IMgzavriDbContext context, IAuthorizedUserService auth, IFileStorageService fileStorage) : base(context, auth, fileStorage)
        {
        }

        public override async Task<Result> HandleAsync(ValidateCodeCommand cmd, CancellationToken ct)
        {
            var user = await context.Users.FirstOrDefaultAsync(x => x.Email == cmd.Email);

            if (user == null)
                return Result.Error("დაფიქსირდა შეცდომა");
            if (user.RendomCode != cmd.Code)
                return Result.Error("კოდი არასწორია");

            return Result.Success();
        }
    }
}
