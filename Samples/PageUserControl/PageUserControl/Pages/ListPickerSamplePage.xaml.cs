using PageUserControl.ViewModel;
using Windows.UI.Xaml.Controls;
using Windows.UI.Xaml.Navigation;

namespace PageUserControl.Pages
{
    public sealed partial class ListPickerSamplePage : Page
    {
        private ListPickerSamplePageVM _viewModel;

        public ListPickerSamplePage()
        {
            this.InitializeComponent();
            this.NavigationCacheMode = NavigationCacheMode.Required;
        }

        protected override void OnNavigatedTo(NavigationEventArgs e)
        {
            base.OnNavigatedTo(e);
            if (this._viewModel == null)
            {
                this._viewModel = this.DataContext as ListPickerSamplePageVM;
            }
            this._viewModel.LoadData();
        }

        protected override void OnNavigatedFrom(NavigationEventArgs e)
        {
            base.OnNavigatedFrom(e);
            if (e.NavigationMode == NavigationMode.Back)
            {
                this.NavigationCacheMode = NavigationCacheMode.Disabled;
            }
        }
    }
}
