using Entities.Model.Users;
using Microsoft.AspNetCore.Http;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;

public class JwtMiddleware
{
    private readonly RequestDelegate _next;
    private readonly string _secretKey;
    private readonly string _encryptKey;

    public JwtMiddleware(RequestDelegate next, IConfiguration configuration)
    {
        _next = next;
        _secretKey = configuration["Jwt:SecretKey"];
        _encryptKey = configuration["Jwt:EncryptKey"];
    }

    public async Task Invoke(HttpContext context)
    {
        var token = context.Request.Headers["Authorization"].FirstOrDefault()?.Split(" ").Last();

        if (!string.IsNullOrEmpty(token))
        {
            AttachUserToContext(context, token);
        }

        await _next(context);
    }

    private void AttachUserToContext(HttpContext context, string token)
    {
        try
        {
            var tokenHandler = new JwtSecurityTokenHandler();
            var decryptionKey = Encoding.UTF8.GetBytes(_encryptKey);
            var signingKey = Encoding.UTF8.GetBytes(_secretKey);

            var tokenValidationParameters = new TokenValidationParameters
            {
                ValidateIssuerSigningKey = true,
                IssuerSigningKey = new SymmetricSecurityKey(signingKey),
                TokenDecryptionKey = new SymmetricSecurityKey(decryptionKey),
                ValidateIssuer = false,
                ValidateAudience = false,
                ClockSkew = TimeSpan.Zero
            };

            tokenHandler.ValidateToken(token, tokenValidationParameters, out SecurityToken validatedToken);

            var jwtToken = validatedToken as JwtSecurityToken;
        }
        catch (Exception ex)
        {
        }
    }

}
