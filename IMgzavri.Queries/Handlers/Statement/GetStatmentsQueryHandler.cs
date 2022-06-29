using IMgzavri.Domain.FileStorage;
using IMgzavri.Infrastructure;
using IMgzavri.Infrastructure.Db;
using IMgzavri.Infrastructure.Service;
using IMgzavri.Queries.Extension;
using IMgzavri.Queries.Queries.Statement;
using IMgzavri.Queries.ViewModels;
using IMgzavri.Queries.ViewModels.Statment;
using IMgzavri.Shared.Contracts;
using IMgzavri.Shared.Domain.Models;
using IMgzavri.Shared.ExternalServices;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace IMgzavri.Queries.Handlers.Statement
{
    public class GetStatmentsQueryHandler : QueryHandler<GetStatmentsQuery>
    {
        public GetStatmentsQueryHandler(IMgzavriDbContext context, IAuthorizedUserService auth, IFileStorageService fileStorage) : base(context, auth, fileStorage)
        {
        }

        public override async Task<Result> HandleAsync(GetStatmentsQuery query, CancellationToken ct)
        {
            var statment = context.Statements.AsQueryable();
            var vm = new List<StatmentVm>() { };
            if(query.SearchStatment != null)
            {
                var user = context.Users.Where(x=>( query.SearchStatment.FirstName != null ? x.FirstName == query.SearchStatment.FirstName : true)
                && (query.SearchStatment.LastName != null ? x.LastName == query.SearchStatment.LastName : true)).ToList();
                if(!user.Any())
                    return Result.Success();
                statment = statment.Where(x => user.Any(z => x.Id == x.CreateUserId));
            }

            if (query.SortStatment != null)
                statment = this.StatmentSort(statment, query.SortStatment);
            else statment.OrderByDescending(x => x.CreatedDate);

            var res = statment.Select(x => new StatmentVm()
            {
                Id = x.Id,
                CarId = x.CarId,
                Description = x.Description,
                CreateDate = x.CreatedDate,
                Seat = x.Seat,
                Price = x.Price,
                RoutFrom = context.Cities.FirstOrDefault(z => z.Id == x.RoutFromId).Name,
                RouteTo = context.Cities.FirstOrDefault(z => z.Id == x.RouteToId).Name,
                DateFrom = x.DateFrom,
                DateTo = x.DateTo,
                IsComplited = x.IsComplited,
                CreateUserId = x.CreateUserId,
                ImageLink = this.GetImagelink(x.CarId)
            }); ;

            var result = new Result();
            
            result.Response = GridDataExtention.GetGridData(statment, query.page, query.offset);

            return result;
        }

        private string GetImagelink(Guid carId)
        {
            FileStoreLinkResult fmRes = null;
            try
            {
                fmRes = FileStorage.GetFilePhysicalPath(context.Cars.FirstOrDefault(x => x.Id == carId).MainImageId.Value);
            }
            catch { return ""; }

            return fmRes.Link;
        }

        private IQueryable<IMgzavri.Domain.Models.Statement> StatmentSort(IQueryable<IMgzavri.Domain.Models.Statement> query, SortStatment sort)
        {
            Expression<Func<IMgzavri.Domain.Models.Statement, object>> orderByExpression;

            if (sort.DateUp)
            {
                orderByExpression = x => x.DateFrom;
                query = query.OrderByDescending(orderByExpression);
            }
            if (sort.DateDown)
            {
                orderByExpression = x => x.DateFrom;
                query = query.OrderBy(orderByExpression);
            }
            if (sort.PriceDown) 
            {
                orderByExpression = x => x.Price;
                query = query.OrderByDescending(orderByExpression);
            }
            if (sort.PriceUp)
            {
                orderByExpression = x => x.Price;
                query = query.OrderBy(orderByExpression);
            }
            if (sort.SeatUp)
            {
                orderByExpression = x => x.Seat;
                query = query.OrderBy(orderByExpression);
            }
            if (sort.SeatDown)
            {
                orderByExpression = x => x.Seat;
                query = query.OrderByDescending(orderByExpression);
            }
            else
            {
                orderByExpression = x => x.CreatedDate;
                query = query.OrderBy(orderByExpression);
            }

            return query;

        }


        private IQueryable<IMgzavri.Domain.Models.Statement> Filter (IQueryable<IMgzavri.Domain.Models.Statement> query, StatmentFilter filter)
        {
            if (filter.PriceFrom != null)
            {
                query = query.Where(x => x.Price >= filter.PriceFrom);
            }
            if (filter.PriceTo != null)
            {
                query = query.Where(x => x.Price <= filter.PriceTo);
            }
            if (filter.Seat != null)
                query = query.Where(x => x.Seat == filter.Seat);
            if (filter.DateFrom != null)
                query = query.Where(x => x.DateFrom == filter.DateFrom);
            if(filter.DateTo != null)
                query = query.Where(x => x.DateTo == filter.DateTo);
            if(filter.RoutFromId != null)
                query = query.Where(x => x.RoutFromId == filter.RoutFromId);
            if(filter.RouteToId != null)
                query = query.Where(x => x.RouteToId == filter.RouteToId);
            return query;
        }
    }
}
