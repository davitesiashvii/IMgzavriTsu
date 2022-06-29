using IMgzavri.Domain.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;

namespace IMgzavri.Infrastructure
{
    public interface IAuthorizedUserService
    {
        ClaimsPrincipal GetAuthorizedUser();

        bool IsAuthorized();

        Guid GetCurrentUserId();

        string GetCurrentUserEmail();

        GeneratedToken GenerateToken(Users user);

        ClaimsPrincipal GetPrincipalFromToken(string token);
    }
}
