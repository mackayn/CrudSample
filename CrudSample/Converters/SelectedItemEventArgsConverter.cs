using System;
using System.Globalization;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace CrudSample.Converters
{
	public class SelectedItemEventArgsConverter : IValueConverter, IMarkupExtension
	{
		public object Convert (object value, Type targetType, object parameter, CultureInfo culture)
		{
		    var eventArgs = value as ItemTappedEventArgs;
		    return eventArgs?.Item;
		}

	    public object ConvertBack (object value, Type targetType, object parameter, CultureInfo culture)
		{
			throw new NotImplementedException ();
		}

	    public object ProvideValue(IServiceProvider serviceProvider)
	    {
	        return this;
	    }
    }
}
