using System.Globalization;
using System.Windows.Data;

namespace WPF_Challenge.Helpers;

public class BoolToOpacityConverter : IValueConverter
{
    public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
    {
        if (value is bool canRegister)
        {
            return canRegister ? 1.0 : 0.5;
        }
        return 0.5;
    }

    public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
    {
        throw new NotImplementedException();
    }
}