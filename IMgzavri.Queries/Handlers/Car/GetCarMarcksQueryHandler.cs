
using IMgzavri.Infrastructure;
using IMgzavri.Infrastructure.Db;
using IMgzavri.Infrastructure.Service;
using IMgzavri.Queries.Queries.Car;
using IMgzavri.Queries.ViewModels.Car;
using IMgzavri.Shared.Contracts;
using IMgzavri.Shared.Domain.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace IMgzavri.Queries.Handlers.Car
{
    public class GetCarMarcksQueryHandler : QueryHandler<GetCarMarckQuery>
    {
        public GetCarMarcksQueryHandler(IMgzavriDbContext context, IAuthorizedUserService auth, IFileStorageService fileStorage) : base(context, auth, fileStorage)
        {
        }

        public override async Task<Result> HandleAsync(GetCarMarckQuery query, CancellationToken ct)
        {
            var carMarcks = context.CarMarcks.AsQueryable();

            if(!carMarcks.Any())
                return Result.Error("dfd");


            var res = carMarcks.Select(x => new CarMarckVm
            {
                MarckId = x.Id,
                Name = x.Code,
            });

            var result = new Result();

            result.Response = res;

            return result;

        }
    }
}
