﻿using Azure.Core;
using Common;
using Common.Api;
using Common.Extensions;
using Common.Utilities;
using Data;
using Entities.Enum.Friendships;
using Entities.Enum.Users;
using Entities.Form.Users;
using Entities.Model.Users;
using Entities.Response.Friendships;
using Entities.Response.Users;
using Entities.ViewModel.Users;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;
using Service.Users;
using System.Data;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;

namespace Business.Users
{
    public class BUser : BaseBusiness, IUserService
    {
        public BUser(DreamVocabBoxContext db, IConfiguration configuration, IUserRepositoryService userRepositoryService) : base(db, configuration, userRepositoryService)
        {
        }
        public async Task<List<RUserBoxScenario>> GetScenarios() => Enum.GetValues(typeof(UserBoxScenarioEnum))
                                .Cast<UserBoxScenarioEnum>()
                                .Select(scenario => new RUserBoxScenario
                                {
                                    Id = (int)scenario,
                                    Title = scenario.GetTitle(),
                                    Description = scenario.GetDescription()
                                })
                                .ToList();
        public async Task<RUserLogin> UpdateProfileAsync(RUserLogin form)
        {
            form.NickName = form.NickName.Trim();
            form.UserName = form.UserName.Trim().ToLower();
            form.Email = (form.Email ?? "").Trim();

            if (string.IsNullOrEmpty(form.UserName))
                throw new AppException(ApiResultStatusCode.UserNameIsEmpty);

            if (form.UserName.Contains(' '))
                throw new AppException(ApiResultStatusCode.UserNameHasSpace);

            if ((form.Password ?? "").Contains(' '))
                throw new AppException(ApiResultStatusCode.PasswordHasSpace);

            if (form.NickName.IsEmpty())
                throw new AppException(ApiResultStatusCode.NickNameIsEmpty);

            var user = await DataBase.Users.FirstOrDefaultAsync(x => x.Id == form.Id);
            if (user == null)
                throw new AppException(ApiResultStatusCode.UserNotExist);

            var userNameExist = await DataBase.Users.AnyAsync(x => x.Id != form.Id && x.UserName.ToLower() == form.UserName.ToLower());
            if (userNameExist)
                throw new AppException(ApiResultStatusCode.UserNameExist);

            if (form.UserName.StartsWith("guest-") && form.UserName != user.UserName)
                throw new AppException(ApiResultStatusCode.UserNameExist);

            if (user.BoxScenario == form.BoxScenario && user.NickName == form.NickName && user.UserName == form.UserName && user.Avatar == form.Avatar && (user.Email ?? "") == (form.Email ?? "") && (form.Password ?? "").IsEmpty())
                throw new AppException(ApiResultStatusCode.NoChangesFound);


            var emailExist = await DataBase.Users.AnyAsync(x => x.Id != form.Id && !form.Email.IsEmpty() && x.Email != null && x.Email.ToLower() == form.Email);
            if (emailExist)
                throw new AppException(ApiResultStatusCode.EmailExist);


            if (!form.Password.IsEmpty())
                user.PasswordHash = SecurityHelper.HashPassword(form.Password);

            user.Avatar = form.Avatar;
            user.Email = form.Email;
            user.NickName = form.NickName;
            user.UserName = form.UserName;
            user.BoxScenario = form.BoxScenario;

            DataBase.Users.Update(user);
            await DataBase.SaveChangesAsync();

            userRepositoryService.Add(new VMUserMiniInfo() { Id = user.Id, NickName = user.NickName, UserName = user.UserName, SecurityStamp = user.SecurityStamp.ToString(), BoxScenario = user.BoxScenario, Email = user.Email, Avatar = user.Avatar });

            return new RUserLogin()
            {
                Token = GenerateToken(user),
                NickName = user.NickName,
                Avatar = user.Avatar,
                Email = user.Email,
                BoxScenario = user.BoxScenario,
                UserName = user.UserName
            };
        }
        public async Task<RUserLogin> RegisterAsGuestAsync()
        {
            Random random = new Random();
            int randomPassword = random.Next(1000, 9999);

            var user = new User
            {
                UserName = "",
                Email = "",
                NickName = GuestNicknames.GetRandomNickname(),
                PasswordHash = SecurityHelper.HashPassword(randomPassword.ToString())
            };

            await DataBase.Users.AddAsync(user);
            await DataBase.SaveChangesAsync();

            user.UserName = $"guest-{user.Id}";
            DataBase.Users.Update(user);
            await DataBase.SaveChangesAsync();

            return new RUserLogin()
            {
                Token = GenerateToken(user),
                NickName = user.NickName,
                Avatar = user.Avatar,
                Email = user.Email,
                BoxScenario = user.BoxScenario,
                UserName = user.UserName
            };
        }

        public async Task<RUserLogin> RegisterAsync(RegisterRequest request)
        {
            request.NickName = request.NickName.Trim();
            request.UserName = request.UserName.Trim().ToLower();
            request.Email = request.Email.Trim();

            if (request.UserName.StartsWith("guest-") || await IsUserExist(request.UserName))
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

            await DataBase.Users.AddAsync(user);
            await DataBase.SaveChangesAsync();

            return new RUserLogin()
            {
                Token = GenerateToken(user),
                NickName = user.NickName,
                Avatar = user.Avatar,
                Email = user.Email,
                BoxScenario = user.BoxScenario,
                UserName = user.UserName
            };
        }

        public async Task<RUserLogin> LoginAsync(LoginRequest request)
        {
            var user = await DataBase.Users.FirstOrDefaultAsync(u => u.UserName == request.UserName);

            if (user == null)
                throw new AppException(ApiResultStatusCode.UserNotExist);

            if (user.PasswordHash != SecurityHelper.HashPassword(request.Password))
                throw new AppException(ApiResultStatusCode.WrongPassword);

            user.LastLoginDate = DateTime.Now;
            await DataBase.SaveChangesAsync();
            return new RUserLogin()
            {
                Token = GenerateToken(user),
                NickName = user.NickName,
                Avatar = user.Avatar,
                Email = user.Email,
                BoxScenario = user.BoxScenario,
                UserName = user.UserName
            };
        }

        public async Task<bool> IsUserExist(string userName) => await DataBase.Users.AnyAsync(u => u.UserName.ToLower() == userName.ToLower());

        private string GenerateToken(User user)
        {
            var key = Encoding.UTF8.GetBytes(Configuration["Jwt:SecretKey"]);
            var SigningCredentials = new SigningCredentials(new SymmetricSecurityKey(key), SecurityAlgorithms.HmacSha256Signature);

            var encryptionKey = Encoding.UTF8.GetBytes(Configuration["Jwt:EncryptKey"]);
            var encryptingCridentials = new EncryptingCredentials(new SymmetricSecurityKey(encryptionKey), SecurityAlgorithms.Aes128KW, SecurityAlgorithms.Aes128CbcHmacSha256);


            var issuedAt = DateTime.Now.AddMinutes(Convert.ToInt32(Configuration["Jwt:IssuedAt"]));
            var notBefore = DateTime.Now.AddMinutes(Convert.ToInt32(Configuration["Jwt:NotBeforeMinute"]));
            var expires = DateTime.Now.AddMinutes(Convert.ToInt32(Configuration["Jwt:ExpiresMinute"]));

            var descriptor = new SecurityTokenDescriptor
            {
                Issuer = Configuration["Jwt:Issuer"],
                Audience = Configuration["Jwt:Audience"],
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
        public async Task<List<RFriendship>> SearchUsers(string SearchText, int UserId)
        {
            var users = await DataBase.Users
                .Where(u => (u.NickName.ToLower().StartsWith(SearchText) || u.UserName.ToLower().StartsWith(SearchText))
                            && u.Id != UserId)
                .ToListAsync();

            var sentFriendships = await DataBase.Friendships
                .Where(f => f.SenderUserId == UserId)
                .ToListAsync();

            var receivedFriendships = await DataBase.Friendships
                .Where(f => f.ReceiverUserId == UserId)
                .ToListAsync();

            var result = users
                .Where(u => !sentFriendships.Any(f => f.ReceiverUserId == u.Id && f.Status == FriendshipStatusEnum.Blocked) &&
                            !receivedFriendships.Any(f => f.SenderUserId == u.Id && f.Status == FriendshipStatusEnum.Blocked))
                .Select(u =>
                {
                    var sentRequest = sentFriendships.LastOrDefault(f => f.ReceiverUserId == u.Id
                        && f.Status != FriendshipStatusEnum.Canceled
                        && f.Status != FriendshipStatusEnum.Deleted
                        && f.Status != FriendshipStatusEnum.Rejected);

                    var receivedRequest = receivedFriendships.LastOrDefault(f => f.SenderUserId == u.Id
                        && f.Status != FriendshipStatusEnum.Canceled
                        && f.Status != FriendshipStatusEnum.Deleted
                        && f.Status != FriendshipStatusEnum.Rejected);

                    return new RFriendship
                    {
                        NickName = u.NickName,
                        UserName = u.UserName,
                        Avatar = u.Avatar,
                        UserId = u.Id,
                        FriendshipStatus = sentRequest != null ? sentRequest.Status : (receivedRequest != null ? receivedRequest.Status : FriendshipStatusEnum.Nothing),
                        IsSentByUser = sentRequest != null
                    };
                })
                .OrderBy(x => x.UserId)
                .Take(20)
                .ToList();

            return result;
        }


        public async Task<RUserProfileStatics> GetUserProfileStatics(int UserId)
        {
            var user = await DataBase.Users.FirstOrDefaultAsync(u => u.Id == UserId);
            if (user == null)
                throw new AppException(ApiResultStatusCode.UserNotExist);

            var vocabularies = await DataBase.Vocabularies
                .Where(v => v.UserId == UserId).ToListAsync();

            var checkedVocabularies = await DataBase.VocabularyChecks
                .Where(vc => vc.UserId == UserId)
                .ToListAsync();

            List<int> inBoxVocabularyCount = new List<int>();

            for (int i = 1; i <= 7; i++)
                inBoxVocabularyCount.Add(vocabularies.Count(x => x.BoxNumber == i));

            int totalDays = (DateTime.Now - user.RegisterDate).Days;
            totalDays = totalDays == 0 ? 1 : totalDays;
            string lastCheck = checkedVocabularies.LastOrDefault()?.RegisterDate.ToHumanReadableTime("yMwdhm") ?? "Not checked yet";
            if (lastCheck == "")
                lastCheck = "Just";
            string lastAdd = vocabularies.LastOrDefault()?.RegisterDate.ToHumanReadableTime("yMwdhm") ?? "Not added yet";
            if (lastAdd == "")
                lastAdd = "Just";
            return new RUserProfileStatics
            {
                UserId = user.Id,
                UserName = user.UserName,
                NickName = user.NickName,
                Avatar = user.Avatar,
                TotalVocabularyCount = vocabularies.Count,
                RegisterAge = user.RegisterDate.ToHumanReadableTime("yMwd"),
                LastCheck = lastCheck,
                LastAddVocabulary = lastAdd,
                InBoxVocabularyCount = inBoxVocabularyCount,
                TotalCheck = checkedVocabularies.Count,
                AverageDailyAdd = vocabularies.Count / totalDays,
                AverageDailyCheck = checkedVocabularies.Count / totalDays,
                AverageDailyLearnedCheck = checkedVocabularies.Count(vc => vc.Learned) / totalDays,
                AverageDailyNotLearnedCheck = checkedVocabularies.Count(vc => !vc.Learned) / totalDays,
            };
        }

        public async Task<RUserProfile> GetProfile(int UserId)
        {
            var user = userRepositoryService.Get(UserId);
            var res = user.MapTo<RUserProfile>();
            var friendshipPending = await DataBase.Friendships.AsNoTracking().CountAsync(f => f.ReceiverUserId == UserId && f.Status == FriendshipStatusEnum.Pending);
            res.FriendshipPending = friendshipPending;
            var messagesUnread = await DataBase.Messages.AsNoTracking().CountAsync(f => f.ReceiverUserId == UserId && f.ReadAt == null);
            res.MessagesUnread = messagesUnread;
            var allVocabularyCount = await DataBase.Vocabularies.AsNoTracking().CountAsync(f => f.UserId == UserId);
            res.AllVocabularyCount = allVocabularyCount;
            res.Scenarios = await GetScenarios();
            return res;
        }
        public async Task<RUserPublicInfo> GetUserPublic(int UserId)
        {
            var user = userRepositoryService.Get(UserId);
            if (user != null)
                return new RUserPublicInfo()
                {
                    Avatar = user.Avatar,
                    NickName = user.NickName,
                    UserId = UserId,
                    UserName = user.UserName,
                };
            else
            {
                var dbUser = await DataBase.Users.FirstOrDefaultAsync(x => x.Id == UserId);
                if (dbUser != null)
                    return new RUserPublicInfo()
                    {
                        Avatar = dbUser.Avatar,
                        NickName = dbUser.NickName,
                        UserId = UserId,
                        UserName = dbUser.UserName,
                    };
            }
            throw new AppException(ApiResultStatusCode.NotFound);
        }
    }
}
