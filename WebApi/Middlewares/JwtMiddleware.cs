using Common.Extensions;
using Microsoft.IdentityModel.Tokens;
using Service.Users;
using System.IdentityModel.Tokens.Jwt;
using System.Text;

public class JwtMiddleware
{
    private readonly RequestDelegate _next;
    private readonly string _secretKey;
    private readonly string _encryptKey;
    private readonly IServiceScopeFactory _scopeFactory;

    public JwtMiddleware(RequestDelegate next, IConfiguration configuration, IServiceScopeFactory scopeFactory)
    {
        _next = next;
        _secretKey = configuration["Jwt:SecretKey"];
        _encryptKey = configuration["Jwt:EncryptKey"];
        _scopeFactory = scopeFactory;
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
            using (var scope = _scopeFactory.CreateScope())
            {
                var userRepositoryService = scope.ServiceProvider.GetRequiredService<IUserRepositoryService>();

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

                var userId = jwtToken.Claims.FirstOrDefault(c => c.Type == "UserId")?.Value.ToInt() ?? 0;

                var securityStampFromToken = jwtToken.Claims.FirstOrDefault(c => c.Type == "SecurityStamp")?.Value;

                if (userId == 0 || securityStampFromToken == null)
                    throw new UnauthorizedAccessException();

                var cachedUser = userRepositoryService.Get(userId);
                if (cachedUser != null)
                {
                    if (cachedUser.SecurityStamp != securityStampFromToken)
                        throw new UnauthorizedAccessException();
                }
                else
                {
                    var user = userRepositoryService.Add(userId);
                    if (user == null || user.SecurityStamp != securityStampFromToken)
                        throw new UnauthorizedAccessException();
                }
            }
        }
        catch (Exception)
        {
            throw new UnauthorizedAccessException();
        }
    }
}
