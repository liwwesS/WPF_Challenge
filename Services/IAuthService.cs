using WPF_Challenge.DTOs;
using WPF_Challenge.MVVM.Models;

namespace WPF_Challenge.Services;

public interface IAuthService
{
    bool IsUserAuthenticated { get; set; }
    Task<bool> RegisterAsync(UserDto userDto);
    Task<bool> LoginAsync(string username, string password);
    Task<UserModel> GetCurrentUser();
    Task LogoutAsync();
}
