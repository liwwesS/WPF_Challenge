using System.Windows;
using System.Windows.Input;
using WPF_Challenge.Core;
using WPF_Challenge.Core.ViewModels;
using WPF_Challenge.MVVM.Views;
using WPF_Challenge.Services;

namespace WPF_Challenge.MVVM.ViewModels;

public class AuthViewModel : BaseViewModel
{
    private readonly IAuthService _authService;
    private readonly MessageService _messageService;

    public string Username { get; set; }
    public string Password { get; set; }
    public bool CanLogin { get; set; }
    public bool IsAuthenticated => _authService.IsUserAuthenticated;

    public ICommand LoginCommand { get; }
    public ICommand LogoutCommand { get; }
    public ICommand OpenRegistrationWindowCommand { get; }

    public AuthViewModel(IAuthService authService, MessageService messageService)
    {
        _authService = authService;
        _messageService = messageService;

        LoginCommand = new RelayCommand(async o => await LoginAsync(), o => CheckLogin());
        LogoutCommand = new RelayCommand(async o => await LogoutAsync());
        OpenRegistrationWindowCommand = new RelayCommand(o => OpenRegistrationWindow());
    }

    private void OpenRegistrationWindow()
    {
        var registrationWindow = new RegistrationView
        {
            DataContext = new RegistrationViewModel(_authService, _messageService)
        };
        registrationWindow.ShowDialog();

        OnPropertyChanged(nameof(IsAuthenticated));
    }

    private async Task LoginAsync()
    {
        var result = await _authService.LoginAsync(Username, Password);

        if (result)
        {
            _messageService.NotifyUserAuthenticated();
            OnPropertyChanged(nameof(IsAuthenticated));
            MessageBox.Show("Вход выполнен успешно!", "Успех", MessageBoxButton.OK, MessageBoxImage.Information);
        }
        else
        {
            MessageBox.Show("Неверное имя пользователя или пароль", "Ошибка", MessageBoxButton.OK, MessageBoxImage.Error);
        }
    }

    private async Task LogoutAsync()
    { 
        await _authService.LogoutAsync();
        _messageService.NotifyUserLoggedOut();
        OnPropertyChanged(nameof(IsAuthenticated));
    }

    private bool CheckLogin()
    {
        return CanLogin = !string.IsNullOrWhiteSpace(Username) &&
                          !string.IsNullOrWhiteSpace(Password);
    }
}
