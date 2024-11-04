using System.ComponentModel;
using System.Windows;

namespace WPF_Challenge.Helpers;

public static class ViewModelLocator
{
    public static IServiceProvider ServiceProvider { get; set; }

    public static bool GetAutoWireViewModel(DependencyObject obj)
    {
        return (bool)obj.GetValue(AutoWireViewModelProperty);
    }

    public static void SetAutoWireViewModel(DependencyObject obj, bool value)
    {
        obj.SetValue(AutoWireViewModelProperty, value);
    }

    public static readonly DependencyProperty AutoWireViewModelProperty =
        DependencyProperty.RegisterAttached("AutoWireViewModel",
            typeof(bool), typeof(ViewModelLocator),
            new PropertyMetadata(false, OnAutoWireViewModelChanged)
        );

    private static void OnAutoWireViewModelChanged(DependencyObject d, DependencyPropertyChangedEventArgs e)
    {
        if (DesignerProperties.GetIsInDesignMode(d) || ServiceProvider == null)
        {
            return;
        }

        var viewType = d.GetType();
        var viewTypeName = viewType.FullName;

        var viewModelTypeName = viewTypeName.Replace(".Views.", ".ViewModels.") + "Model";

        var viewModelType = Type.GetType(viewModelTypeName);
        if (viewModelType == null)
        {
            throw new InvalidOperationException($"Не удалось найти ViewModel для {viewTypeName}");
        }

        var viewModel = ServiceProvider.GetService(viewModelType) ?? Activator.CreateInstance(viewModelType);

        ((FrameworkElement)d).DataContext = viewModel;
    }
}