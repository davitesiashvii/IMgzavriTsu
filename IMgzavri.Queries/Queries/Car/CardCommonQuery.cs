using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace IMgzavri.Queries.Queries.Car
{
    public record GetCarMarckQuery(): Query;

    public record GetCarsQuery(): Query;

    public record GetCarQuery(Guid CarId): Query;

    public record GetCarModelsQuery(int MarckId):Query;
}
