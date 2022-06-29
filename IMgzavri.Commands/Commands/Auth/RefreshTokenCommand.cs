using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace IMgzavri.Commands.Commands.Auth
{
     public record RefreshTokenCommand([Required]string Token, [Required]string RefreshToken) : Command;
}
