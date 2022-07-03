using IMgzavri.Infrastructure;
using IMgzavri.Infrastructure.Db;
using IMgzavri.Infrastructure.Service;
using IMgzavri.Queries.Queries.Profile;
using IMgzavri.Shared.Domain.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace IMgzavri.Queries.Handlers.Profile
{
    public class GetRateQueryHandler : QueryHandler<GetRateQuery>
    {
        public GetRateQueryHandler(IMgzavriDbContext context, IAuthorizedUserService auth, IFileStorageService fileStorage) : base(context, auth, fileStorage)
        {
        }

        public async override Task<Result> HandleAsync(GetRateQuery query, CancellationToken ct)
        {
            string rate = "0";
            if(query.UserId != "")
            {
                var userId = Guid.Parse(query.UserId);
                var userRate = context.Users.FirstOrDefault(x=>x.Id == userId).Rate;
                rate = String.Format("{0:0.0}", userRate);
            }

            if(query.StatmentId != "")
            {
                var statmentId = Guid.Parse(query.StatmentId);
                var userId = context.Statements.FirstOrDefault(x=>x.Id == statmentId).CreateUserId;
                var userRate = context.Users.FirstOrDefault(x=>x.Id == userId).Rate;
                rate = String.Format("{0:0.0}", userRate);
            }

            var result = new Result();
            result.Response = rate;
            return result;
        }
    }
}
