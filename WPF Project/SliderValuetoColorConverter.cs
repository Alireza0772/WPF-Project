using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Media;

namespace WPFProject
{
	[ValueConversion(typeof(double),typeof(Brush))]
	class SliderValuetoColorConverter : IValueConverter
	{
		public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
		{
			double sliderPosition = (double)value;
			byte redValue = (byte)((100 - sliderPosition) / 100 * 255);
			byte greenValue = (byte)(sliderPosition / 100 * 255);
			return new SolidColorBrush(Color.FromRgb(redValue, greenValue, 0));
		}
		public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture) => throw new NotImplementedException();
	}
}
