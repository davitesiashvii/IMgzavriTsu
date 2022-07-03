using IMgzavri.Commands.Commands.Statment;
using IMgzavri.Infrastructure;
using IMgzavri.Infrastructure.Db;
using IMgzavri.Infrastructure.Service;
using IMgzavri.Shared.Domain.Models;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace IMgzavri.Commands.Handlers.Statment
{
    public class ExcecuteStatmentCommandHandler : CommandHandler<ExcecuteStatmentCommand>
    {
        public ExcecuteStatmentCommandHandler(IMgzavriDbContext context, IAuthorizedUserService auth, IFileStorageService fileStorage) : base(context, auth, fileStorage)
        {
        }

        public override async Task<Result> HandleAsync(ExcecuteStatmentCommand cmd, CancellationToken ct)
        {
            var curentUserId = Auth.GetCurrentUserId();

            var statment = await context.Statements.FirstOrDefaultAsync(x => x.Id == cmd.StatmentId);

            if (statment == null || statment.CreateUserId != curentUserId)
                throw new Exception("A system error has occurred");
            statment.IsComplited = false;

            context.Statements.Add(statment);
            await context.SaveChangesAsync();

            return Result.Success();

        }
    }
}
