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
    public class DeleteStatmentCommandHandler : CommandHandler<DeleteStatmentCommand>
    {
        public DeleteStatmentCommandHandler(IMgzavriDbContext context, IAuthorizedUserService auth, IFileStorageService fileStorage) : base(context, auth, fileStorage)
        {
        }

        public override async Task<Result> HandleAsync(DeleteStatmentCommand cmd, CancellationToken ct)
        {
            var statment = await context.Statements.FirstOrDefaultAsync(x => x.Id == cmd.statmentId);
            //var car = await context.Cars.FirstOrDefaultAsync(x => x.Id == statment.CarId);
            //var carImage = context.CarImages.Where(x => x.CarId == car.Id);
            context.Statements.Remove(statment);
            //context.Cars.Remove(car);
            //if (carImage.Any())
                //context.RemoveRange(carImage);

            await context.SaveChangesAsync();
            return Result.Success();
        }
    }
}
