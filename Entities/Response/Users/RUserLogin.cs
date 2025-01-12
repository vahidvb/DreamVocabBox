using Entities.Enum.Users;

namespace Entities.Response.Users
{
    public class RUserLogin
    {
        public int Id { get; set; }
        public string? Token { get; set; }
        public string UserName { get; set; }
        public string? Password { get; set; }
        public string NickName { get; set; }
        public UserBoxScenarioEnum BoxScenario { get; set; }

        public string? Email { get; set; }
        public int Avatar { get; set; } = 1;
    }
}
