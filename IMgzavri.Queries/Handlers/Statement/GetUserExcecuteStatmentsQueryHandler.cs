using IMgzavri.Domain.FileStorage;
using IMgzavri.Infrastructure;
using IMgzavri.Infrastructure.Db;
using IMgzavri.Infrastructure.Service;
using IMgzavri.Queries.Queries.Statement;
using IMgzavri.Queries.ViewModels.Car;
using IMgzavri.Queries.ViewModels.Statment;
using IMgzavri.Shared.Domain.Models;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace IMgzavri.Queries.Handlers.Statement
{
    public class GetUserExcecuteStatmentsQueryHandler : QueryHandler<GetUserExcecuteStatmentsQuery>
    {
        public GetUserExcecuteStatmentsQueryHandler(IMgzavriDbContext context, IAuthorizedUserService auth, IFileStorageService fileStorage) : base(context, auth, fileStorage)
        {
        }

        public async override Task<Result> HandleAsync(GetUserExcecuteStatmentsQuery query, CancellationToken ct)
        {
            var userId = Auth.GetCurrentUserId();

            var client = context.Clients.Where(x => x.UserId == userId);

            var res = new List<StatmentVm>() { };

            foreach(var item in client)
            {

                var statment = await context.Statements.FirstOrDefaultAsync(x => x.Id == item.StatmentId && x.DateTo > DateTime.Now);

                if (statment == null)
                    continue;

                FileStoreLinkResult fmRes = null;
                try
                {
                    fmRes = await FileStorage.GetFilePhysicalPath(context.Cars.FirstOrDefault(x => x.Id == statment.CarId).MainImageId.Value);
                }
                catch{ }

                var str = new StatmentVm()
                {
                    Id = statment.Id,
                    CarId = statment.CarId,
                    Description = statment.Description,
                    MobileNumber = context.Users.FirstOrDefault(x => x.Id == statment.CreateUserId).MobileNumber,
                    CreateDate = statment.CreatedDate,
                    Seat = statment.Seat,
                    Price = statment.Price,
                    RoutFrom = context.Cities.FirstOrDefault(x => x.Id == statment.RoutFromId).Name,
                    RouteTo = context.Cities.FirstOrDefault(x => x.Id == statment.RouteToId).Name,
                    DateFrom = statment.DateFrom,
                    DateTo = statment.DateTo,
                    IsComplited = statment.IsComplited,
                    CreateUserId = userId,
                    ImageLink = fmRes == null ? null : fmRes.Link,
                    freeSeat = statment.FreeSeat.Value,
                    isValid = context.Cars.FirstOrDefault(c => c.Id == statment.CarId).IsVertify
                };
                res.Add(str);
            }

            var result = new Result();

            result.Response = res;

            return result;
        }
    }

    
}
