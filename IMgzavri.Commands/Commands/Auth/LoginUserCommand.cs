using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace IMgzavri.Commands.Commands.Auth
{
    public record LoginUserCommand(
        string Email,
        string Password
        ) : Command;


}
