using IMgzavri.Commands.Commands.car;
using IMgzavri.Infrastructure;
using IMgzavri.Infrastructure.Db;
using IMgzavri.Infrastructure.Service;
using IMgzavri.Shared.Contracts;
using IMgzavri.Shared.Domain.Models;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace IMgzavri.Commands.Handlers.Car
{
    public class UpdateCarCommandHandler : CommandHandler<UpdateCarCommand>
    {
        public UpdateCarCommandHandler(IMgzavriDbContext context, IAuthorizedUserService auth, IFileStorageService fileStorage) : base(context, auth, fileStorage)
        {
        }

        public override async Task<Result> HandleAsync(UpdateCarCommand cmd, CancellationToken ct)
        {
            var car = await context.Cars.FirstOrDefaultAsync(x=>x.Id == cmd.CarId);
            if (car == null)
                return Result.Error("");
            car.MarckId = cmd.MarckId;
            car.ModelId = cmd.ModelId;

            context.Update(car);
            await context.SaveChangesAsync();

            return Result.Success();
        }
    }
}
