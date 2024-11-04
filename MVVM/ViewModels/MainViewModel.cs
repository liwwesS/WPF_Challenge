using System.Windows.Input;
using WPF_Challenge.Commands;
using WPF_Challenge.Core;
using WPF_Challenge.Core.ViewModels;
using WPF_Challenge.Services;

namespace WPF_Challenge.MVVM.ViewModels;

public class MainViewModel : BaseViewModel
{
    public INavigationService Navigation { get; set; }
    private readonly MessageService _messageService;

    public ICommand DragMoveCommand { get; }
    public ICommand CloseCommand { get; }
    public ICommand MinimizeCommand { get; }

    public ICommand NavigateToUserViewCommand { get; }
    public ICommand NavigateToDeveloperViewCommand { get; }

    public MainViewModel(INavigationService navigationService, MessageService messageService)
    {
        DragMoveCommand = new DragMoveCommand();
        CloseCommand = new CloseCommand();
        MinimizeCommand = new MinimizeCommand();

        Navigation = navigationService;
        Navigation.NavigateTo<UserViewModel>();
        Navigation.NavigateToAuthView();

        _messageService = messageService;
        _messageService.UserAuthenticated += () => { Navigation.NavigateTo<UserViewModel>(); };
        _messageService.UserLoggedOut += () => { Navigation.NavigateTo<UserViewModel>(); };

        NavigateToUserViewCommand = new RelayCommand(o => { Navigation.NavigateTo<UserViewModel>(); }, o => true);
        NavigateToDeveloperViewCommand = new RelayCommand(o => { Navigation.NavigateTo<DeveloperViewModel>(); }, o => true);
    }
}
