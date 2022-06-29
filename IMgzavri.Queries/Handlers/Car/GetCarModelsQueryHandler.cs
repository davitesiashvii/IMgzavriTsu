
using IMgzavri.Infrastructure;
using IMgzavri.Infrastructure.Db;
using IMgzavri.Infrastructure.Service;
using IMgzavri.Queries.Queries.Car;
using IMgzavri.Queries.ViewModels.Car;
using IMgzavri.Shared.Domain.Models;

namespace IMgzavri.Queries.Handlers.Car
{
    public class GetCarModelsQueryHandler : QueryHandler<GetCarModelsQuery>
    {
        public GetCarModelsQueryHandler(IMgzavriDbContext context, IAuthorizedUserService auth, IFileStorageService fileStorage) : base(context, auth, fileStorage)
        {
        }

        public async override Task<Result> HandleAsync(GetCarModelsQuery query, CancellationToken ct)
        {
            var carModels = context.CarModels.Where(x=>x.MarckId == query.MarckId);

            if(!carModels.Any())
                return Result.Error("Models not found");

            var res = carModels.Select(x => new CarModelVM
            {
                ModelId = x.Id,
                MarckId = x.MarckId,
                Name = x.Code

            });

            var result = new Result();

            result.Response = res;

            return result;

        }
    }
}
