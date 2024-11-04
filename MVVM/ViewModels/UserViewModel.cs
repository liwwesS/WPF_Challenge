using WPF_Challenge.Core.ViewModels;
using WPF_Challenge.Services;

namespace WPF_Challenge.MVVM.ViewModels;

public class UserViewModel : BaseViewModel
{
    private readonly IAuthService _authService;

    public long Id { get; set; }
    public string FirstName { get; set; }
    public string LastName { get; set; }
    public string Email { get; set; }
    public string Phone { get; set; }

    public bool IsAuthenticated => _authService.IsUserAuthenticated;
    
    public UserViewModel(IAuthService authService)
    {
        _authService = authService;
        _ = LoadUserDataAsync();
    }

    private async Task LoadUserDataAsync()
    {
        if (!IsAuthenticated) return;

        var user = await _authService.GetCurrentUser();

        if (user != null)
        {
            Id = user.Id;
            FirstName = user.FirstName;
            LastName = user.LastName;   
            Email = user.Email;
            Phone = user.Phone;
        }
    }
}
