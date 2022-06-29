using IMgzavri.Domain.FileStorage;
using IMgzavri.Infrastructure;
using IMgzavri.Infrastructure.Db;
using IMgzavri.Infrastructure.Service;
using IMgzavri.Queries.Extension;
using IMgzavri.Queries.Queries.Car;
using IMgzavri.Queries.ViewModels;
using IMgzavri.Queries.ViewModels.Car;
using IMgzavri.Shared.Contracts;
using IMgzavri.Shared.Domain.Models;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace IMgzavri.Queries.Handlers.Car
{
    public class GetCarQueryHandle : QueryHandler<GetCarQuery>
    {
        public GetCarQueryHandle(IMgzavriDbContext context, IAuthorizedUserService auth, IFileStorageService fileStorage) : base(context, auth, fileStorage)
        {
        }

        public override async Task<Result> HandleAsync(GetCarQuery query, CancellationToken ct)
        {
            var car = await context.Cars.FirstOrDefaultAsync(x => x.Id == query.CarId);

            if (car == null)
                return Result.Error("");

            var carVm = new CarVM()
            {
                CarId = car.Id,
                Marck = context.CarMarcks.FirstOrDefault(m => m.Id == car.MarckId).Code,
                Model = context.CarModels.FirstOrDefault(m => m.Id == car.ModelId).Code,
                CreatedDate = car.CreateDate,
                MainImageLink = this.GetImagelink(car.MainImageId.Value),
                Images = this.GetImagelinks(car.UserId)
            };

            var result = new Result();
            result.Response = carVm;
            return result;
        }


        private string GetImagelink(Guid mainImageId)
        {
            FileStoreLinkResult fmRes = null;
            try
            {
                fmRes = FileStorage.GetFilePhysicalPath(mainImageId);
            }
            catch { return null; }

            return fmRes.Link;
        }

        private List<string> GetImagelinks(Guid carId)
        {
            var fmRes = new List<FileStoreLinkResult>();
            try
            {
                fmRes = FileStorage.GetFilesPhysicalPath(context.CarImages.Where(x => x.Id == carId).Select(z => z.ImageId).ToList());
            }
            catch { return null; }

            return fmRes.Select(x => x.Link).ToList();
        }
    }
}
