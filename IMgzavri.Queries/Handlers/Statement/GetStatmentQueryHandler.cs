using IMgzavri.Domain.FileStorage;
using IMgzavri.Infrastructure;
using IMgzavri.Infrastructure.Db;
using IMgzavri.Infrastructure.Service;
using IMgzavri.Queries.Extension;
using IMgzavri.Queries.Queries.Statement;
using IMgzavri.Queries.ViewModels;
using IMgzavri.Queries.ViewModels.Car;
using IMgzavri.Queries.ViewModels.Statment;
using IMgzavri.Shared.Contracts;
using IMgzavri.Shared.Domain.Models;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace IMgzavri.Queries.Handlers.Statement
{
    public class GetStatmentQueryHandler : QueryHandler<GetStatmentQuery>
    {
        public GetStatmentQueryHandler(IMgzavriDbContext context, IAuthorizedUserService auth, IFileStorageService fileStorage) : base(context, auth, fileStorage)
        {
        }

        public override async Task<Result> HandleAsync(GetStatmentQuery query, CancellationToken ct)
        {
            var userId = Auth.GetCurrentUserId();

            var statment = await context.Statements.FirstOrDefaultAsync(x=>x.CreateUserId == userId && x.Id == query.StatmentId);


            if (statment == null)
                return Result.Error("დაფიქსირდა სისტემური შეცდომა");

            var carVertify = context.Cars.FirstOrDefault(x => x.Id == statment.CarId).IsVertify;

            if(carVertify == null || carVertify == false)
            {
                return Result.Error("Statment car is not vertifed");
            }

            FileStoreLinkResult fmRes = null;
            try
            {
                fmRes =  await FileStorage.GetFilePhysicalPath(context.Cars.FirstOrDefault(x=>x.Id == statment.CarId).MainImageId.Value);
            }
            catch { }

            var str = new StatmentVm()
            {
                Id = statment.Id,
                CarId = statment.CarId,
                Car = this.GetCar(statment.CarId),
                Description = statment.Description,
                CreateDate = statment.CreatedDate,
                MobileNumber = context.Users.FirstOrDefault(x=>x.Id == statment.CreateUserId).MobileNumber,
                Seat = statment.Seat,
                Price = statment.Price,
                RoutFrom = context.Cities.FirstOrDefault(x=>x.Id == statment.RoutFromId).Name,
                RouteTo = context.Cities.FirstOrDefault(x => x.Id == statment.RouteToId).Name,
                DateFrom = statment.DateFrom,
                DateTo = statment.DateTo,
                IsComplited = statment.IsComplited,
                CreateUserId = userId,
                ImageLink = fmRes == null ? null : fmRes.Link,
                freeSeat = statment.FreeSeat.Value,
                isValid = context.Cars.FirstOrDefault(c=>c.Id == statment.CarId).IsVertify
            };
            var result = new Result();
            result.Response = str;
            return result;
        }

        private CarVM GetCar(Guid carId)
        {
            var car = context.Cars.FirstOrDefault(x => x.Id == carId);

            var carVm = new CarVM()
            {
                CarId = car.Id,
                Marck = context.CarMarcks.FirstOrDefault(m => m.Id == car.MarckId).Code,
                Model = context.CarModels.FirstOrDefault(m => m.Id == car.ModelId).Code,
                CreatedDate = car.CreateDate,
                MainImageLink = FileStorage.GetImagelinkToMainImageId(car.MainImageId.Value),
                Images = FileStorage.GetImagelinksToCarId(car.Id),
                IsVertify = car.IsVertify
            };
            return carVm;
        }

        

   
    }
}
