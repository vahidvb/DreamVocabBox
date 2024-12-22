using System.ComponentModel.DataAnnotations;

namespace Entities.Model.Users
{
    public class User : BaseModel<int>
    {

        [StringLength(50)]
        public string UserName { get; set; }
        public DateTime? LastLoginDate { get; set; }
        [StringLength(100)]
        public string? Email { get; set; }
        public Guid SecurityStamp { get; set; }
        [StringLength(500)]
        public string PasswordHash { get; set; }
        public short WrongPassword { get; set; }
        public short WrongOtp { get; set; }
        public User()
        {
            SecurityStamp = Guid.NewGuid();
        }

    }
}
