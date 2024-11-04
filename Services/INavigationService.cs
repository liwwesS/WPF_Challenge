using WPF_Challenge.Core.ViewModels;

namespace WPF_Challenge.Services;

public interface INavigationService
{
    BaseViewModel CurrentView { get; }
    BaseViewModel AuthView { get; }
    void NavigateTo<TViewModel>() where TViewModel : BaseViewModel;
    void NavigateToAuthView();
}
