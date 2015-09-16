using System.Collections.Generic;
using Windows.UI.Xaml;

namespace PageUserControl.UserControls
{
    internal class ListPickerPageParameter
    {
        public string Title { get; set; }
        public object SelectedItem { get; set; }
        public object ItemsSources { get; set; }
        public DataTemplate ItemTemplate { get; set; }
    }
}
