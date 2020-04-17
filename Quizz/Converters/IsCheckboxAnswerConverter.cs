using System;
using System.Globalization;
using Xamarin.Forms;

namespace Quizz.Converters
{
    internal class IsCheckboxAnswerConverter : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            var type = (int)value;
            if (type == 2 || type == 1)
                return true;
            return false;
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            throw new NotImplementedException();
        }
    }
}