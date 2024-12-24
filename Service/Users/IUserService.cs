using Entities.Form.Users;
using Entities.Model.Users;

public interface IUserService
{
    Task<User> RegisterAsync(RegisterRequest request);
    Task<string> LoginAsync(LoginRequest request);
    Task<bool> IsUserExist(string userName);
}
