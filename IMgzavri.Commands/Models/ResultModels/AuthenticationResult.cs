using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace IMgzavri.Commands.Models.ResultModels
{
    public record AuthenticationResult(string Token, string RefreshToken, bool Success) { }


}
