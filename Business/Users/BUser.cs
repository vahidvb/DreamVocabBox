using Common;
using Common.Api;
using Common.Utilities;
using Data;
using Entities.Form.Users;
using Entities.Model.Users;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;
using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;

namespace Business.Users
{
    public class BUser : IUserService
    {
        private readonly DreamVocabBoxContext db;
        private readonly IConfiguration configuration;
        public BUser(DreamVocabBoxContext db, IConfiguration configuration)
        {
            this.db = db;
            this.configuration = configuration;
        }
        public async Task<string> RegisterAsync(RegisterRequest request)
        {
            request.NickName = request.NickName.Trim();
            request.UserName = request.UserName.Trim();
            request.Email = request.Email.Trim();

            if (await IsUserExist(request.UserName))
                throw new AppException(ApiResultStatusCode.UserNameExist);

            if (string.IsNullOrEmpty(request.UserName))
                throw new AppException(ApiResultStatusCode.UserNameIsEmpty);

            if (request.UserName.Contains(' '))
                throw new AppException(ApiResultStatusCode.UserNameHasSpace);

            if (request.Password.Contains(' '))
                throw new AppException(ApiResultStatusCode.PasswordHasSpace);

            if (string.IsNullOrEmpty(request.NickName))
                throw new AppException(ApiResultStatusCode.NickNameIsEmpty);

            if (string.IsNullOrEmpty(request.Password))
                throw new AppException(ApiResultStatusCode.PasswordIsEmpty);

            var user = new User
            {
                UserName = request.UserName,
                Email = request.Email,
                NickName = request.NickName,
                PasswordHash = SecurityHelper.HashPassword(request.Password)
            };

            await db.Users.AddAsync(user);
            await db.SaveChangesAsync();

            return GenerateToken(user);
        }

        public async Task<string> LoginAsync(LoginRequest request)
        {
            var user = await db.Users.FirstOrDefaultAsync(u => u.UserName == request.UserName);

            if (user == null)
                throw new AppException(ApiResultStatusCode.UserNotExist);

            if (!(request.Password == user.PasswordHash))
                throw new AppException(ApiResultStatusCode.WrongPassword);

            user.LastLoginDate = DateTime.Now;
            await db.SaveChangesAsync();
            return GenerateToken(user);
        }

        public async Task<bool> IsUserExist(string userName) => await db.Users.AnyAsync(u => u.UserName == userName);

        private string GenerateToken(User user)
        {
            var key = Encoding.UTF8.GetBytes(configuration["Jwt:SecretKey"]);
            var SigningCredentials = new SigningCredentials(new SymmetricSecurityKey(key), SecurityAlgorithms.HmacSha256Signature);

            var encryptionKey = Encoding.UTF8.GetBytes(configuration["Jwt:EncryptKey"]);
            var encryptingCridentials = new EncryptingCredentials(new SymmetricSecurityKey(encryptionKey), SecurityAlgorithms.Aes128KW, SecurityAlgorithms.Aes128CbcHmacSha256);


            var issuedAt = DateTime.Now.AddMinutes(Convert.ToInt32(configuration["Jwt:IssuedAt"]));
            var notBefore = DateTime.Now.AddMinutes(Convert.ToInt32(configuration["Jwt:NotBeforeMinute"]));
            var expires = DateTime.Now.AddMinutes(Convert.ToInt32(configuration["Jwt:ExpiresMinute"]));

            var descriptor = new SecurityTokenDescriptor
            {
                Issuer = configuration["Jwt:Issuer"],
                Audience = configuration["Jwt:Audience"],
                IssuedAt = issuedAt,
                NotBefore = notBefore,
                Expires = expires,
                SigningCredentials = SigningCredentials,
                EncryptingCredentials = encryptingCridentials,
                Subject = new ClaimsIdentity(new[]
                {
                    new Claim("SecurityStamp", user.SecurityStamp.ToString()),
                    new Claim("UserId", user.Id.ToString()),
                    new Claim("NickName",user.NickName),
                    new Claim("UserName",user.UserName),
                    new Claim("Role",user.Role),
                    new Claim(ClaimTypes.NameIdentifier, user.Id.ToString()),
                    new Claim(ClaimTypes.Name,user.GetType().Name),
                    new Claim(ClaimTypes.Role, user.Role),
                }),
            };

            var tokenHandler = new JwtSecurityTokenHandler();
            var securityToken = tokenHandler.CreateToken(descriptor);
            var jwt = tokenHandler.WriteToken(securityToken);

            return jwt;
        }

    }
}
