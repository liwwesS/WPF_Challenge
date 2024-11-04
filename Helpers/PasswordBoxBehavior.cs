using System.Windows.Controls;
using System.Windows;

namespace WPF_Challenge.Helpers;

public static class PasswordBoxBehavior
{
    public static readonly DependencyProperty PasswordProperty =
        DependencyProperty.RegisterAttached(
            "Password",
            typeof(string),
            typeof(PasswordBoxBehavior),
            new PropertyMetadata(string.Empty, OnPasswordChanged));

    public static string GetPassword(DependencyObject obj)
    {
        return (string)obj.GetValue(PasswordProperty);
    }

    public static void SetPassword(DependencyObject obj, string value)
    {
        obj.SetValue(PasswordProperty, value);
    }

    private static void OnPasswordChanged(DependencyObject d, DependencyPropertyChangedEventArgs e)
    {
        if (d is not PasswordBox passwordBox) return;

        passwordBox.PasswordChanged -= PasswordBox_PasswordChanged;
        passwordBox.PasswordChanged += PasswordBox_PasswordChanged;
        if (!string.IsNullOrEmpty(passwordBox.Password))
            SetPassword(d, passwordBox.Password);
    }

    private static void PasswordBox_PasswordChanged(object sender, RoutedEventArgs e)
    {
        if (sender is PasswordBox passwordBox)
        {
            SetPassword(passwordBox, passwordBox.Password);
        }
    }
}