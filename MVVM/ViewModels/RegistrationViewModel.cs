using System.Windows;
using System.Windows.Input;
using WPF_Challenge.Commands;
using WPF_Challenge.Core;
using WPF_Challenge.Core.ViewModels;
using WPF_Challenge.DTOs;
using WPF_Challenge.MVVM.Views;
using WPF_Challenge.Services;

namespace WPF_Challenge.MVVM.ViewModels;

public class RegistrationViewModel : BaseViewModel
{
    private readonly IAuthService _authService;
    private readonly MessageService _messageService;

    public string Username { get; set; }
    public string Password { get; set; }
    public string ConfirmPassword { get; set; }
    public string FirstName { get; set; }
    public string LastName { get; set; }
    public string Email { get; set; }
    public string Phone { get; set; }

    public bool CanRegister { get; set; }

    public ICommand DragMoveCommand { get; }
    public ICommand CloseCommand { get; }
    public ICommand MinimizeCommand { get; }

    public ICommand RegisterCommand { get; }

    public RegistrationViewModel(IAuthService authService, MessageService messageService)
    {
        _authService = authService;
        _messageService = messageService;

        DragMoveCommand = new DragMoveCommand();
        CloseCommand = new CloseCommand();
        MinimizeCommand = new MinimizeCommand();

        RegisterCommand = new RelayCommand(async o => await RegisterAsync(), o => CheckRegister());
    }

    private async Task RegisterAsync()
    {
        if (Password != ConfirmPassword)
        {
            MessageBox.Show("Пароли не совпадают", "Ошибка", MessageBoxButton.OK, MessageBoxImage.Error);
            return;
        }

        var result = await _authService.RegisterAsync(new UserDto
        {
            Username = Username,
            Password = Password,
            FirstName = FirstName,
            LastName = LastName,
            Email = Email,
            Phone = Phone
        });

        if (result)
        {
            MessageBox.Show("Регистрация успешна!", "Успех", MessageBoxButton.OK, MessageBoxImage.Information);
            Application.Current.Windows.OfType<RegistrationView>().FirstOrDefault()?.Close();

            await _authService.LoginAsync(Username, Password);
            _messageService.NotifyUserAuthenticated();
        }
        else
        {
            MessageBox.Show("Ошибка регистрации", "Ошибка", MessageBoxButton.OK, MessageBoxImage.Error);
        }
    }

    private bool CheckRegister()
    {
        return CanRegister = !string.IsNullOrWhiteSpace(Username) &&
               !string.IsNullOrWhiteSpace(Password) &&
               !string.IsNullOrWhiteSpace(ConfirmPassword) &&
               !string.IsNullOrWhiteSpace(FirstName) &&
               !string.IsNullOrWhiteSpace(LastName) &&
               !string.IsNullOrWhiteSpace(Phone);
    }
}
