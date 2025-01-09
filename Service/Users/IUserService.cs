using Entities.Form.Users;
using Entities.Model.Users;
using Entities.Response.Users;

public interface IUserService
{
    Task<RUserLogin> UpdateProfileAsync(RUserLogin form);
    Task<RUserLogin> RegisterAsGuestAsync();
    Task<RUserLogin> RegisterAsync(RegisterRequest request);
    Task<RUserLogin> LoginAsync(LoginRequest request);
    Task<bool> IsUserExist(string userName);
}
