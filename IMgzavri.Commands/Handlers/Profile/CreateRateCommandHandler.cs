using IMgzavri.Commands.Commands.Profile;
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

namespace IMgzavri.Commands.Handlers.Profile
{
    public class CreateRateCommandHandler : CommandHandler<CreateRateCommand>
    {
        public CreateRateCommandHandler(IMgzavriDbContext context, IAuthorizedUserService auth, IFileStorageService fileStorage) : base(context, auth, fileStorage)
        {
        }

        public async override Task<Result> HandleAsync(CreateRateCommand cmd, CancellationToken ct)
        {
            var curentUserId = Auth.GetCurrentUserId();

            var creatorUser = await context.Users.FirstOrDefaultAsync(x=>x.Id == cmd.UserId);
            
            var rateCount = creatorUser.RateCount == null ? 0 : creatorUser.RateCount;
            var rate = creatorUser.Rate == null ? 0 : creatorUser.Rate;
            creatorUser.Rate = (creatorUser.Rate + rate) / (rateCount + 1);

            creatorUser.RateCount = rateCount + 1;

            context.Users.Update(creatorUser);

            await context.SaveChangesAsync();

            return Result.Success();
        }
    }
}
