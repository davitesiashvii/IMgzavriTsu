using IMgzavri.Api.Models;
using IMgzavri.Domain.Models;
using IMgzavri.Infrastructure;
using IMgzavri.Shared.Contracts;
using IMgzavri.Shared.Domain.Models;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Security.Cryptography;
using System.Text;

namespace IMgzavri.Api.Services
{
    public class AuthorizedUserService : IAuthorizedUserService
    {
        private readonly IHttpContextAccessor _contextAccessor;
        private readonly JwtSettings _jwtSettings;
        private readonly TokenValidationParameters _tokenValidationParameters;

        public AuthorizedUserService(IHttpContextAccessor contextAccessor, JwtSettings jwtSettings, TokenValidationParameters tokenValidationParameters)
        {
            _contextAccessor = contextAccessor;
            _jwtSettings = jwtSettings;
            _tokenValidationParameters = tokenValidationParameters;
        }

        public ClaimsPrincipal GetAuthorizedUser() => _contextAccessor.HttpContext.User;

        public Guid GetCurrentUserId() =>
            Guid.Parse(_contextAccessor
                .HttpContext
                .User
                .Claims
                .First(x => x.Type == "id").Value);

        public string GetCurrentUserEmail() =>
            _contextAccessor
                .HttpContext
                .User
                .Claims
                .First(x => x.Type == ClaimTypes.Email).Value;

        public GeneratedToken GenerateToken(Users user)
        {
            var tokenHandler = new JwtSecurityTokenHandler();

            var key = Encoding.ASCII.GetBytes(_jwtSettings.Secret);

            var tokenDescriptor = new SecurityTokenDescriptor
            {
                Subject = new ClaimsIdentity(new Claim[]
                {
                    new Claim("id", user.Id.ToString()),
                    new Claim(ClaimTypes.Email, user.Email),
                    new Claim("mobileNumber",user.MobileNumber),
                    new Claim("idNumber",user.IdNumber)
                }),
                Expires = DateTime.UtcNow.AddDays(7),
                SigningCredentials = new SigningCredentials(new SymmetricSecurityKey(key), SecurityAlgorithms.HmacSha256Signature)
            };


            var token = tokenHandler.CreateToken(tokenDescriptor);

            var refreshToken = new RefreshToken
            {
                Id = Guid.NewGuid(),
                JwtId = token.Id,
                UserId = user.Id,
                CreateDate = DateTime.UtcNow,
                ExpiryDate = DateTime.UtcNow.AddMonths(6),
                Token = this.GenerateRefreshToken()
            };

            return new GeneratedToken()
            {
                Token = tokenHandler.WriteToken(token),
                RefreshToken = refreshToken
            };
        }

        public bool IsAuthorized()
        {
            var authorizedUser = GetAuthorizedUser();

            return authorizedUser != null && authorizedUser.Claims.Count() != 0;
        }

        public ClaimsPrincipal GetPrincipalFromToken(string token)
        {
            var tokenHandler = new JwtSecurityTokenHandler();

            try
            {
                var tokenValidationParameters = _tokenValidationParameters.Clone();
                tokenValidationParameters.ValidateLifetime = false;
                var principal = tokenHandler.ValidateToken(token, tokenValidationParameters, out var validatedToken);
                if (!IsJwtWithValidSecurityAlgorithm(validatedToken))
                {
                    return null;
                }

                return principal;
            }
            catch
            {
                return null;
            }
        }

        private bool IsJwtWithValidSecurityAlgorithm(SecurityToken validatedToken)
        {
            return (validatedToken is JwtSecurityToken jwtSecurityToken) &&
                   jwtSecurityToken.Header.Alg.Equals(SecurityAlgorithms.HmacSha256,
                       StringComparison.InvariantCultureIgnoreCase);
        }

        private string GenerateRefreshToken()
        {
            var randomNumber = new byte[64];
            using var rng = RandomNumberGenerator.Create();
            rng.GetBytes(randomNumber);
            return Convert.ToBase64String(randomNumber);
        }

      

    }
}
