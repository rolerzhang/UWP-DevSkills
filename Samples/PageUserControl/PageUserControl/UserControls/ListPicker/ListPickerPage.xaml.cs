using System.Collections.Generic;
using Windows.UI.Xaml.Controls;
using Windows.UI.Xaml.Navigation;

namespace PageUserControl.UserControls
{
    public sealed partial class ListPickerPage : Page
    {
        private ListPickerPageVM _viewModel;

        internal IList<object> Value { get; private set; }

        public ListPickerPage()
        {
            this.InitializeComponent();
            this.DataContext = new ListPickerPageVM();

            NavigationCacheMode = NavigationCacheMode.Required;
            this.Loaded += ListPickerPage_Loaded;
        }

        private void ListPickerPage_Loaded(object sender, Windows.UI.Xaml.RoutedEventArgs e)
        {
            this.Loaded -= ListPickerPage_Loaded;

            if (this._viewModel != null)
            {
                this._viewModel.PageLoadCommand.Execute(null);
            }
        }

        protected override void OnNavigatedTo(NavigationEventArgs e)
        {
            if (this._viewModel == null)
            {
                this._viewModel = this.DataContext as ListPickerPageVM;
            }
            var parameter = e.Parameter as ListPickerPageParameter;
            this._viewModel.TryLoadData(parameter);
        }

        protected override void OnNavigatedFrom(NavigationEventArgs e)
        {
            base.OnNavigatedFrom(e);
            if (e.NavigationMode == NavigationMode.Back)
            {
                this.NavigationCacheMode = NavigationCacheMode.Disabled;
            }
        }

        private void ListView_ItemClick(object sender, ItemClickEventArgs e)
        {
            if (e.ClickedItem != null)
            {
                this.Value = new List<object> { e.ClickedItem };
                if (this.Frame.CanGoBack)
                {
                    this.Frame.GoBack();
                }
            }
        }
    }
}
