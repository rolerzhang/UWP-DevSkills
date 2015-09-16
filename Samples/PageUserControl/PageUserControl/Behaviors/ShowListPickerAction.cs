using System;
using System.Windows.Input;
using Microsoft.Xaml.Interactivity;
using PageUserControl.UserControls;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Data;

namespace PageUserControl.Behaviors
{
    public sealed class ShowListPickerAction : DependencyObject, IAction
    {
        #region Properties

        #region Title

        public string Title
        {
            get { return (string)GetValue(TitleProperty); }
            set { SetValue(TitleProperty, value); }
        }

        // Using a DependencyProperty as the backing store for Title.  This enables animation, styling, binding, etc...
        public static readonly DependencyProperty TitleProperty =
            DependencyProperty.Register("Title", typeof(string), typeof(ShowListPickerAction), new PropertyMetadata(DependencyProperty.UnsetValue));

        #endregion

        #region SelectedItem

        public object SelectedItem
        {
            get { return (object)GetValue(SelectedItemProperty); }
            set { SetValue(SelectedItemProperty, value); }
        }

        // Using a DependencyProperty as the backing store for SelectedItem.  This enables animation, styling, binding, etc...
        public static readonly DependencyProperty SelectedItemProperty =
            DependencyProperty.Register("SelectedItem", typeof(object), typeof(ShowListPickerAction), new PropertyMetadata(DependencyProperty.UnsetValue));

        #endregion

        #region ItemsSources

        public object ItemsSource
        {
            get { return (object)GetValue(ItemsSourceProperty); }
            set { SetValue(ItemsSourceProperty, value); }
        }

        // Using a DependencyProperty as the backing store for ItemsSource.  This enables animation, styling, binding, etc...
        public static readonly DependencyProperty ItemsSourceProperty =
            DependencyProperty.Register("ItemsSource", typeof(object), typeof(ShowListPickerAction), new PropertyMetadata(DependencyProperty.UnsetValue));

        #endregion

        #region ItemTemplate

        public DataTemplate ItemTemplate
        {
            get { return (DataTemplate)GetValue(ItemTemplateProperty); }
            set { SetValue(ItemTemplateProperty, value); }
        }

        // Using a DependencyProperty as the backing store for ItemTemplate.  This enables animation, styling, binding, etc...
        public static readonly DependencyProperty ItemTemplateProperty =
            DependencyProperty.Register("ItemTemplate", typeof(DataTemplate), typeof(ShowListPickerAction), new PropertyMetadata(DependencyProperty.UnsetValue));

        #endregion

        #region ItemsPickedCommand

        public ICommand ItemsPickedCommand
        {
            get { return (ICommand)GetValue(ItemsPickedCommandProperty); }
            set { SetValue(ItemsPickedCommandProperty, value); }
        }

        // Using a DependencyProperty as the backing store for ItemsPickedCommand.  This enables animation, styling, binding, etc...
        public static readonly DependencyProperty ItemsPickedCommandProperty =
            DependencyProperty.Register("ItemsPickedCommand", typeof(ICommand), typeof(ShowListPickerAction), new PropertyMetadata(DependencyProperty.UnsetValue));

        #endregion

        #region ItemsPickedInputConverter

        public IValueConverter ItemsPickedInputConverter
        {
            get { return (IValueConverter)GetValue(ItemsPickedInputConverterProperty); }
            set { SetValue(ItemsPickedInputConverterProperty, value); }
        }

        // Using a DependencyProperty as the backing store for ItemsPickedInputConverter.  This enables animation, styling, binding, etc...
        public static readonly DependencyProperty ItemsPickedInputConverterProperty =
            DependencyProperty.Register("ItemsPickedInputConverter", typeof(IValueConverter), typeof(ShowListPickerAction), new PropertyMetadata(DependencyProperty.UnsetValue));

        #endregion

        #endregion

        public object Execute(object sender, object parameter)
        {
            var listPicker = new ListPicker();
            listPicker.ItemsPicked += ListPicker_ItemsPicked;
            listPicker.Show(this.Title, this.ItemsSource, this.ItemTemplate, this.SelectedItem);
            return null;
        }

        private async void ListPicker_ItemsPicked(ListPicker sender, ItemsPickedEventArgs args)
        {
            sender.ItemsPicked -= ListPicker_ItemsPicked;
            if (this.ItemsPickedCommand != null && args.Items != null && args.Items.Count > 0)
            {
                object parameter = this.ItemsPickedInputConverter != null ?
                    this.ItemsPickedInputConverter.Convert(args, typeof(object), null, null) :
                    args;
                await this.Dispatcher.RunAsync(Windows.UI.Core.CoreDispatcherPriority.Normal, () =>
                {
                    if (this.ItemsPickedCommand.CanExecute(parameter))
                    {
                        this.ItemsPickedCommand.Execute(parameter);
                    }
                });
            }
            this.OnItemsPicked(args);
        }

        public event EventHandler<ItemsPickedEventArgs> ItemsPicked;
        private void OnItemsPicked(ItemsPickedEventArgs args)
        {
            var handler = this.ItemsPicked;
            if (handler != null)
            {
                handler(this, args);
            }
        }
    }
}
