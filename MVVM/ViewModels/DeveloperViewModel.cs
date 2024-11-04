using System.Diagnostics;
using System.Windows.Input;
using WPF_Challenge.Core;
using WPF_Challenge.Core.ViewModels;

namespace WPF_Challenge.MVVM.ViewModels;

public class DeveloperViewModel : BaseViewModel
{
    public ICommand OpenTelegramCommand { get; }
    public ICommand OpenLinkedInCommand { get; }

    public DeveloperViewModel()
    {
        OpenTelegramCommand = new RelayCommand(o => OpenLink("https://t.me/liwwesss"));
        OpenLinkedInCommand = new RelayCommand(o => OpenLink("https://www.linkedin.com/in/liwwess/"));
    }

    private void OpenLink(string url)
    {
        try
        {
            Process.Start(new ProcessStartInfo
            {
                FileName = url,
                UseShellExecute = true
            });
        }
        catch (Exception ex)
        {
            Console.WriteLine($"Не удалось открыть ссылку: {ex.Message}");
        }
    }
}
