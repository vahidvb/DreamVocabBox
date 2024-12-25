using Entities.Form.Users;
using Entities.Model.Users;

public interface IUserService
{
    Task<string> RegisterAsync(RegisterRequest request);
    Task<string> LoginAsync(LoginRequest request);
    Task<bool> IsUserExist(string userName);
}
