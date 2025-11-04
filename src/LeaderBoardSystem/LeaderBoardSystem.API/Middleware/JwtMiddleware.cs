using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;

namespace LeaderboardSystem.API.Middleware
{
    public class JwtMiddleware(RequestDelegate next, IConfiguration configuration)
    {
        private readonly RequestDelegate _next = next;
        private readonly IConfiguration _configuration = configuration;

        public async Task Invoke(HttpContext context)
        {
            var token = context.Request.Headers.Authorization.FirstOrDefault()?.Split(" ").Last();

            if (token != null)
                AttachUserToContext(context, token);

            await _next(context);
        }

        private void AttachUserToContext(HttpContext context, string token)
        {
            try
            {
                var jwtSettings = _configuration.GetSection("JwtSettings");
                var secret = jwtSettings["Secret"];

                if (string.IsNullOrEmpty(secret))
                    return;

                var tokenHandler = new JwtSecurityTokenHandler();
                var key = Encoding.UTF8.GetBytes(secret);

                tokenHandler.ValidateToken(token, new TokenValidationParameters
                {
                    ValidateIssuerSigningKey = true,
                    IssuerSigningKey = new SymmetricSecurityKey(key),
                    ValidateIssuer = true,
                    ValidIssuer = jwtSettings["Issuer"],
                    ValidateAudience = true,
                    ValidAudience = jwtSettings["Audience"],
                    ValidateLifetime = true,
                    ClockSkew = TimeSpan.Zero
                }, out SecurityToken validatedToken);

                var jwtToken = (JwtSecurityToken)validatedToken;
                var userId = jwtToken.Claims.First(x => x.Type == JwtRegisteredClaimNames.Sub).Value;
                var username = jwtToken.Claims.First(x => x.Type == JwtRegisteredClaimNames.UniqueName).Value;
                var role = jwtToken.Claims.First(x => x.Type == ClaimTypes.Role).Value;

                var claims = new List<Claim>
            {
                new(ClaimTypes.NameIdentifier, userId),
                new(ClaimTypes.Name, username),
                new(ClaimTypes.Role, role)
            };

                var identity = new ClaimsIdentity(claims, "jwt");
                context.User = new ClaimsPrincipal(identity);
            }
            catch
            {
                // Token validation failed - do nothing
            }
        }
    }
}
