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
    public class GetCarsQueryHandler : QueryHandler<GetCarsQuery>
    {
        public GetCarsQueryHandler(IMgzavriDbContext context, IAuthorizedUserService auth, IFileStorageService fileStorage) : base(context, auth, fileStorage)
        {
        }

        public override async Task<Result> HandleAsync(GetCarsQuery query, CancellationToken ct)
        {
            var curentUserId = Auth.GetCurrentUserId();
            
            var cars = await context.Cars.Where(x=>x.UserId == curentUserId).ToListAsync();
            var resultCars = new List<CarVM>() { };
            var result = new Result();
            if (!cars.Any())
            {
                result.Response = resultCars;
                return result;
            }

            resultCars.AddRange(cars.Select(x => new CarVM
            {
                CarId = x.Id,
                CreatedDate = x.CreateDate,
                Marck = context.CarMarcks.FirstOrDefault(m => m.Id == x.MarckId).Code,
                Model = context.CarModels.FirstOrDefault(m => m.Id == x.ModelId).Code,
                MainImageLink = this.GetImagelink(x.MainImageId.Value),
                Images = this.GetImagelinks(x.Id)
            }));
            result.Response = resultCars;
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
                fmRes = FileStorage.GetFilesPhysicalPath(context.CarImages.Where(x => x.Id == carId).Select(z=>z.ImageId).ToList());
            }
            catch { return null; }

            return fmRes.Select(x=>x.Link).ToList();
        }




    }
}
