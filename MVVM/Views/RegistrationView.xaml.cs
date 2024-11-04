using System.Windows;

namespace WPF_Challenge.MVVM.Views;

public partial class RegistrationView : Window
{
    public RegistrationView()
    {
        InitializeComponent();
    }

    private void CloseClick(object sender, RoutedEventArgs e)
    {
        Close();
    }
}