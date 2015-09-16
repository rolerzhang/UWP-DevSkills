using System;
using Windows.UI.Xaml.Data;

namespace PageUserControl.Converter
{
    public class ListPickerItemsPickedEventArgsConverter : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, string language)
        {
            var eventArgs = value as UserControls.ItemsPickedEventArgs;
            if (eventArgs == null)
            {
                return null;
            }
            else
            {
                return (eventArgs.Items == null || eventArgs.Items.Count == 0) ? null : eventArgs.Items[0];
            }
        }

        public object ConvertBack(object value, Type targetType, object parameter, string language)
        {
            throw new NotImplementedException();
        }
    }
}
