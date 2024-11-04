using WPF_Challenge.Core;
using WPF_Challenge.Core.ViewModels;
using WPF_Challenge.MVVM.ViewModels;

namespace WPF_Challenge.Services;

public class NavigationService : ObservableObject, INavigationService
{
    private Func<Type, BaseViewModel> _viewModelFactory { get; }

    public NavigationService(Func<Type, BaseViewModel> viewModelFactory)
    {
        _viewModelFactory = viewModelFactory;
    }

    public BaseViewModel CurrentView { get; set; }
    public BaseViewModel AuthView { get; set; }

    public void NavigateTo<TViewModel>() where TViewModel : BaseViewModel
    {
        var viewModel = _viewModelFactory.Invoke(typeof(TViewModel));
        CurrentView = viewModel;
    }

    public void NavigateToAuthView()
    {
        var authViewModel = _viewModelFactory.Invoke(typeof(AuthViewModel));
        AuthView = authViewModel;
    }
}
