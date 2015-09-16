using System;
using System.Collections.Generic;
using Windows.Foundation;
using Windows.UI.Xaml;

namespace PageUserControl.UserControls
{
    public class ListPicker : PageUserControl<ListPickerPage>
    {
        public ListPicker()
        {
            base.IsChildrenFrameFirst = true;
        }

        public void Show(string title, object itemsSources, DataTemplate itemTemplate, object selectedItem = null)
        {
            var parameter = new ListPickerPageParameter { Title = title, ItemsSources = itemsSources, ItemTemplate = itemTemplate, SelectedItem = selectedItem };
            base.ShowPage(parameter);
        }

        protected override void CommitValue(ListPickerPage page)
        {
            base.CommitValue(page);
            if (page.Value != null)
            {
                this.OnItemsPicked(page.Value);
            }
        }

        public event TypedEventHandler<ListPicker, ItemsPickedEventArgs> ItemsPicked;
        private void OnItemsPicked(IList<object> items)
        {
            var handler = this.ItemsPicked;
            if (handler != null)
            {
                handler(this, new ItemsPickedEventArgs(items));
            }
        }
    }

    public class ItemsPickedEventArgs : EventArgs
    {
        public IList<object> Items { get; private set; }

        internal ItemsPickedEventArgs(IList<object> items)
        {
            this.Items = items;
        }
    }
}
