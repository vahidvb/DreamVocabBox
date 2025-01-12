using Entities.Enum.Users;
using System.ComponentModel.DataAnnotations;

namespace Entities.Model.Users
{
    public class User : BaseModel<int>
    {

        [StringLength(50)]
        public string UserName { get; set; }
        public string NickName { get; set; }
        public DateTime? LastLoginDate { get; set; }
        [StringLength(100)]
        public string? Email { get; set; }
        public Guid SecurityStamp { get; set; }
        [StringLength(500)]
        public string PasswordHash { get; set; }
        public string Role { get; set; }
        public int Avatar { get; set; }
        public UserBoxScenarioEnum BoxScenario { get; set; }
        public User()
        {
            SecurityStamp = Guid.NewGuid();
            Role = "User";
            Avatar = 1;
            BoxScenario = UserBoxScenarioEnum.DailyBox;
        }

    }
}
