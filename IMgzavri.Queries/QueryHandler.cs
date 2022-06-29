
using IMgzavri.Infrastructure;
using IMgzavri.Infrastructure.Db;
using IMgzavri.Infrastructure.Service;
using IMgzavri.Shared.Contracts;
using IMgzavri.Shared.Domain.Models;
using SimpleSoft.Mediator;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace IMgzavri.Queries
{
    public abstract class QueryHandler<TQuery> : IQueryHandler<TQuery, Result> where TQuery : Query
    {
        protected IMgzavriDbContext context;
        //protected ClientServiceProvider ClientServiceProvider;
        protected IAuthorizedUserService Auth;

        protected IFileStorageService FileStorage { get; }

        protected QueryHandler(IMgzavriDbContext context, IAuthorizedUserService auth, IFileStorageService fileStorage)
        {
            this.context = context;
            //ClientServiceProvider = clientServiceProvider;
            Auth = auth;
            FileStorage = fileStorage;
        }

        public abstract Task<Result> HandleAsync(TQuery query, CancellationToken ct);
    }
}
