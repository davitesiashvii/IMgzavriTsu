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
    public class ComplateStatmentCommandHandler : CommandHandler<ComplateStatmentCommand>
    {
        public ComplateStatmentCommandHandler(IMgzavriDbContext context, IAuthorizedUserService auth, IFileStorageService fileStorage) : base(context, auth, fileStorage)
        {
        }

        public async override Task<Result> HandleAsync(ComplateStatmentCommand cmd, CancellationToken ct)
        {
            var statment = await context.Statements.FirstOrDefaultAsync(x => x.Id == cmd.StatmentId);
            if (statment == null)
                throw new Exception("A system error has occurred");
            statment.IsComplited = true;

            context.Statements.Add(statment);
            await context.SaveChangesAsync();

            return Result.Success();
        }
    }
}
