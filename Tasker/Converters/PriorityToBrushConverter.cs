using System;
using System.Globalization;
using System.Windows;
using System.Windows.Data;
using System.Windows.Media;
using Tasker.PCL.Enumerations;

namespace Tasker.Converters
{
    public class PriorityToBrushConverter : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            if ((Priority)value == Priority.Normal)
            {
                return (SolidColorBrush)(Application.Current.Resources["TasksLowPriorityBrush"]);
            }
            else
            {
                return (SolidColorBrush)(Application.Current.Resources["TasksHighPriorityBrush"]);
            }
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            throw new NotImplementedException();
        }
    }
}
