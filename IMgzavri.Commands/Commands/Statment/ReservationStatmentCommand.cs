using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace IMgzavri.Commands.Commands.Statment
{
    public record ReservationStatmentCommand(Guid StatmentId,int SeatCount) : Command;
}
