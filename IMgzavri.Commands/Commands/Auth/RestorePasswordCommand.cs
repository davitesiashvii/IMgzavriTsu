using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace IMgzavri.Commands.Commands.Auth
{
    public record VertifyEmailAndSendValidateCodeCommand(string Email) : Command;

    public record ValidateCodeCommand(string Email, int Code) : Command;

    public record RestorePasswordCommand(string Email, string Password) : Command;
}
