using System;
using System.Globalization;
using System.Windows;
using System.Windows.Data;

namespace Cql.Wpf.Validate.Demo.Converters
{
	public class BooleanToVisibilityConverter : IValueConverter
	{
		public object Convert(object value, Type targetType, object invertVisibility, CultureInfo culture)
		{
			if (invertVisibility != null && false.Equals(System.Convert.ToBoolean(invertVisibility)))
			{
				if (value != null && value is bool && !(value as bool?).Value)
				{
					return Visibility.Visible;
				}

				return Visibility.Collapsed;
			}

			if (value != null && value is bool && (value as bool?).Value)
			{
				return Visibility.Visible;
			}

			return Visibility.Collapsed;
		}

		public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
		{
			throw new NotImplementedException();
		}
	}
}
