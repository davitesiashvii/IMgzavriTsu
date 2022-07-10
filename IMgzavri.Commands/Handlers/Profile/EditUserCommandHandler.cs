using IMgzavri.Commands.Commands.Profile;
using IMgzavri.Commands.Models.ResponceModels;
using IMgzavri.Domain.FileStorage;
using IMgzavri.Domain.Models;
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
            var curenentUserId = Auth.GetCurrentUserId();
            var user = await context.Users.FirstOrDefaultAsync(x=>x.Id == curenentUserId);

            if (user == null)
                return Result.Error("ოპერაციის შესრტულების დროს მოხდა შეცდომა");

            if(!cmd.IdNumberImages.Any() || !cmd.IdNumberImages.Any())
            {
                return Result.Error("ოპერაციის შესრტულების დროს მოხდა შეცდომა");
            }
            
            FileSavingResult res = null;         

            try
            {
                var fileSavingModel = new FileSavingModel(cmd.Photo.Name, cmd.Photo.Extension, cmd.Photo.ContentType, cmd.Photo.Size, Convert.FromBase64String(cmd.Photo.File), curenentUserId, curenentUserId);

                res = await FileStorage.UploadFile(fileSavingModel);
            }
            catch { }

            var profileLicenseImages = new List<ProfileImages>();
            var profileIdNumberImages = new List<ProfileImages>();
            if (cmd.DrivingLicenseImages.Any() && cmd.IdNumberImages.Any())
            {
                var res2 = new List<FileSavingResult>();
                res2 = await this.CreateImages(cmd.DrivingLicenseImages, user.Id);
                profileLicenseImages = res2.Select(x => new ProfileImages()
                {
                    Id = Guid.NewGuid(),
                    ImageId = x.FileId,
                    Type = 1,
                    UserId = user.Id,
                }).ToList();

                res2 = await this.CreateImages(cmd.IdNumberImages, user.Id);
                profileIdNumberImages = res2.Select(x => new ProfileImages()
                {
                    Id = Guid.NewGuid(),
                    ImageId = x.FileId,
                    Type = 2,
                    UserId = user.Id,
                }).ToList();
            }
            var profileImages = new List<ProfileImages>();

            profileImages.AddRange(profileLicenseImages);
            profileImages.AddRange(profileIdNumberImages);

            context.ProfileImages.AddRange(profileImages);

            user.IdNumber = cmd.IdNumber;
            user.NumberLicense = cmd.NumberLicense;
            user.VerifyUser = true;
            user.PhotoId = res == null ? null : res.FileId;

            context.Users.Update(user);
            context.SaveChanges();
            return Result.Success();
        }

        private async Task<List<FileSavingResult>> CreateImages(List<SaveFileModel> file, Guid userId)
        {
            var Id = Guid.NewGuid();
            var filesModel = new List<FileSavingModel>() { };
            var res2 = new List<FileSavingResult>();
            try
            {
                file.ForEach(x =>
                {
                    filesModel.Add(new FileSavingModel(x.Name, x.Extension, x.ContentType, x.Size, Convert.FromBase64String(x.File), userId, Id));
                });
                
                res2 = await FileStorage.UploadFiles(filesModel);
            }
            catch { }
            return res2;
        }
    }
}
