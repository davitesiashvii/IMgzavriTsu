﻿using IMgzavri.Domain.FileStorage;
using IMgzavri.Infrastructure;
using IMgzavri.Infrastructure.Db;
using IMgzavri.Infrastructure.Service;
using IMgzavri.Queries.Queries.Statement;
using IMgzavri.Queries.ViewModels.Statment;
using IMgzavri.Shared.Domain.Models;
using IMgzavri.Shared.ExternalServices;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace IMgzavri.Queries.Handlers.Statement
{
    public class GetUserStatmentsQueryHandler : QueryHandler<GetUserStatmentsQuery>
    {
        public GetUserStatmentsQueryHandler(IMgzavriDbContext context, IAuthorizedUserService auth, IFileStorageService fileStorage) : base(context, auth, fileStorage)
        {
        }

        public async override Task<Result> HandleAsync(GetUserStatmentsQuery query, CancellationToken ct)
        {
            var userId = Auth.GetCurrentUserId();

            var statment = context.Statements.Where(x => x.CreateUserId == userId && x.DateFrom > DateTime.Now).OrderByDescending(x=>x.CreatedDate);

            if (!statment.Any())
                return Result.Error("დაფიქსირდა სისტემური შეცდომა");

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
                ImageLink = this.GetImagelink(x.CarId),
                freeSeat = x.FreeSeat.Value,
            }).ToList();

            var result = new Result();

            result.Response = res;

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
    }
}
