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
                MainImageLink =  FileStorage.GetImagelinkToMainImageId(car.MainImageId.Value),
                Images =  FileStorage.GetImagelinksToCarId(car.Id)
            };

            var result = new Result();
            result.Response = carVm;
            return result;
        }


        //private async Task<string> GetImagelink(Guid mainImageId)
        //{
        //    FileStoreLinkResult fmRes = null;
        //    try
        //    {
        //        fmRes =  await FileStorage.GetFilePhysicalPath(mainImageId);
        //    }
        //    catch { return null; }

        //    return fmRes.Link;
        //}

        //private async Task<List<string>> GetImagelinks(Guid carId)
        //{
        //    var fmRes = new List<FileStoreLinkResult>();
        //    try
        //    {
        //        fmRes = await FileStorage.GetFilesPhysicalPath(context.CarImages.Where(x => x.CarId == carId).Select(z => z.ImageId).ToList());
        //    }
        //    catch { return null; }

        //    return fmRes.Select(x => x.Link).ToList();
        //}
    }
}
