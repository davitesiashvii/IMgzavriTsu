using IMgzavri.Commands.Commands.Statment;
using IMgzavri.Domain.Models;
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
    public class ReservationStatmentCommandHandler : CommandHandler<ReservationStatmentCommand>
    {
        public ReservationStatmentCommandHandler(IMgzavriDbContext context, IAuthorizedUserService auth, IFileStorageService fileStorage) : base(context, auth, fileStorage)
        {
        }

        public async override Task<Result> HandleAsync(ReservationStatmentCommand cmd, CancellationToken ct)
        {
            var currentUserId = Auth.GetCurrentUserId();
            var statment = await context.Statements.FirstOrDefaultAsync(x=>x.Id == cmd.StatmentId);
            if (statment == null)
                throw new Exception("statment not fount");

            var user = context.Users.FirstOrDefault(x => x.Id == currentUserId);

            if (statment.FreeSeat > cmd.SeatCount)
                throw new Exception($"There are no free {cmd.SeatCount} seats");

            statment.FreeSeat = statment.Seat - cmd.SeatCount;

            var client = new Client()
            {
                UserId = user.Id,
                StatmentId = statment.Id,
                ReservedSeat = cmd.SeatCount
            };
            context.Statements.Update(statment);
            context.Clients.Add(client);
            await context.SaveChangesAsync();

            return Result.Success();
        }
    }
}
