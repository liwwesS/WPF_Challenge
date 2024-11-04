using System.Net.Http;
using Microsoft.Extensions.DependencyInjection;
using System.Windows;
using WPF_Challenge.Core.ViewModels;
using WPF_Challenge.MVVM.ViewModels;
using WPF_Challenge.MVVM.Views;
using WPF_Challenge.Services;
using WPF_Challenge.Helpers;

namespace WPF_Challenge;

public partial class App : Application
{
    private readonly IServiceProvider _serviceProvider;

    public App()
    {
        IServiceCollection services = new ServiceCollection();

        services.AddSingleton<MainView>(provider => new MainView
        {
            DataContext = provider.GetRequiredService<MainViewModel>()
        });

        services.AddSingleton<RegistrationView>(provider => new RegistrationView
        {
            DataContext = provider.GetRequiredService<RegistrationViewModel>()
        });

        services.AddSingleton<MessageService>();
        services.AddSingleton<MainViewModel>();
        services.AddTransient<UserViewModel>();
        services.AddSingleton<DeveloperViewModel>();
        services.AddSingleton<AuthViewModel>();
        services.AddSingleton<RegistrationViewModel>();

        services.AddScoped<HttpClient>();
        services.AddSingleton<IAuthService, AuthService>();
        services.AddSingleton<INavigationService, NavigationService>();
        services.AddSingleton<Func<Type, BaseViewModel>>(serviceProvider => viewModelType => (BaseViewModel)serviceProvider.GetRequiredService(viewModelType));

        ViewModelLocator.ServiceProvider = _serviceProvider;
        _serviceProvider = services.BuildServiceProvider();
    }

    protected override void OnStartup(StartupEventArgs e)
    {
        MainWindow = _serviceProvider.GetRequiredService<MainView>();
        MainWindow.Show();

        base.OnStartup(e);
    }
}
