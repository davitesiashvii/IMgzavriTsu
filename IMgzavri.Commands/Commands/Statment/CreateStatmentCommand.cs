using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace IMgzavri.Commands.Commands.Statment
{
    public record CreateStatmentCommand(
        Guid CarId,
        string? Description,
        int Seat,
        Double Price,
        int RoutFromId,
        int RouteToId,
        DateTime DateFrom,
        DateTime DateTo
        ): Command;
    
    
}
