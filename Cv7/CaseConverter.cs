using System.Globalization;
using System.Windows.Data;

namespace Cv7;

public class CaseConverter : IValueConverter
{
    public object? Convert(object? value, Type targetType, object? parameter, CultureInfo culture)
    {
        return (value as string)?.ToUpper();
    }

    public object? ConvertBack(object? value, Type targetType, object? parameter, CultureInfo culture)
    {
        return (value as string)?.ToLower();
    }
}