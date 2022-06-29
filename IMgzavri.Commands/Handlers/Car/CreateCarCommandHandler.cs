using IMgzavri.Commands.Commands.car;
using IMgzavri.Domain.FileStorage;
using IMgzavri.Domain.Models;
using IMgzavri.Infrastructure;
using IMgzavri.Infrastructure.Db;
using IMgzavri.Infrastructure.Service;
using IMgzavri.Shared.Domain.Models;


namespace IMgzavri.Commands.Handlers.Car
{
    public class CreateCarCommandHandler : CommandHandler<CreateCarCommand>
    {
        public CreateCarCommandHandler(IMgzavriDbContext context, IAuthorizedUserService auth, IFileStorageService fileStorage) : base(context, auth, fileStorage)
        {
        }

        public override async Task<Result> HandleAsync(CreateCarCommand cmd, CancellationToken ct)
        {
            var userId = Auth.GetCurrentUserId();

            FileSavingResult res = null;
            var Id = Guid.NewGuid();
            try
            {
                var fileSavingModel = new FileSavingModel(cmd.MainImage.Name, cmd.MainImage.Extension, cmd.MainImage.ContentType, cmd.MainImage.Size, Convert.FromBase64String(cmd.MainImage.File), userId, Id);

                res = FileStorage.UploadFile(fileSavingModel);
            }
            catch { }

            var carImages = new List<CarImage>();
            if (cmd.Images.Any())
            {
                var filesModel = new List<FileSavingModel>() { };
                var res2 = new List<FileSavingResult>();
                try
                {
                    cmd.Images.ForEach(x =>
                    {
                        filesModel.Add(new FileSavingModel(x.Name, x.Extension, x.ContentType, x.Size, Convert.FromBase64String(x.File), userId, Id));
                    });
                    res2 = FileStorage.UploadFiles(filesModel);
                }
                catch { }
                carImages = res2.Select(x => new CarImage()
                {
                    Id = Guid.NewGuid(),
                    ImageId = x.FileId,
                }).ToList();
            }

            var car = new IMgzavri.Domain.Models.Car()
            {
                Id = Guid.NewGuid(),
                UserId = userId,
                CreateDate = DateTime.Now,
                MarckId = cmd.MarckId,
                ModelId = cmd.ModelId,
                MainImageId = res.FileId,
                CarImages = carImages
            };
            await context.Cars.AddAsync(car);
            await context.SaveChangesAsync();

            return Result.Success();
        }
    }
}
