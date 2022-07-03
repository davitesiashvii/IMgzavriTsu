using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace IMgzavri.Commands.Commands.Profile
{
    public record CreateRateCommand(Guid UserId, int Rate) : Command;

}
