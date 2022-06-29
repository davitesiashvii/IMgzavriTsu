using IMgzavri.Commands.Commands.Profile;
using IMgzavri.Domain.FileStorage;
using IMgzavri.Infrastructure;
using IMgzavri.Infrastructure.Db;
using IMgzavri.Infrastructure.Service;
using IMgzavri.Shared.Contracts;
using IMgzavri.Shared.Domain.Models;
using Microsoft.EntityFrameworkCore;

namespace IMgzavri.Commands.Handlers.Profile
{
    public class EditUserCommandHandler : CommandHandler<EditUserCommand>
    {
        public EditUserCommandHandler(IMgzavriDbContext context, IAuthorizedUserService auth, IFileStorageService fileStorage) : base(context, auth, fileStorage)
        {
        }

        public override async Task<Result> HandleAsync(EditUserCommand cmd, CancellationToken ct)
        {
            var user = await context.Users.FirstOrDefaultAsync(x=>x.Id == cmd.userId);

            if (user == null)
                return Result.Error("ოპერაციის შესრტულების დროს მოხდა შეცდომა");

            FileSavingResult res = null;         

            try
            {
                var fileSavingModel = new FileSavingModel(cmd.Photo.Name, cmd.Photo.Extension, cmd.Photo.ContentType, cmd.Photo.Size, Convert.FromBase64String(cmd.Photo.File), cmd.userId, cmd.userId);

                res = FileStorage.UploadFile(fileSavingModel);
            }
            catch { }

            user.IdNumber = cmd.IdNumber;
            user.NumberLicense = cmd.NumberLicense;
            user.VerifyUser = true;
            user.PhotoId = res == null ? null : res.FileId;

            context.Users.Update(user);
            return Result.Success();
        }
    }
}
