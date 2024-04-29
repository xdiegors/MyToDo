using System;
using System.Globalization;

namespace MyToDo.Converters;
public class FilterTextConverter : IValueConverter
{
    public object Convert(object value, Type targetType, object
   parameter, CultureInfo culture)
    {
        return (bool)value ? "All" : "Active";
    }
    public object ConvertBack(object value, Type targetType,
   object parameter, CultureInfo culture)
    {
        return null;
    }
}