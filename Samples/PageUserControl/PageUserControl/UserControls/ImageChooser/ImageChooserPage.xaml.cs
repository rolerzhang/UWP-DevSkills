using Windows.UI.Xaml.Controls;
using Windows.UI.Xaml.Navigation;

namespace PageUserControl.UserControls
{
    public sealed partial class ImageChooserPage : Page
    {
        internal string Value { get; private set; }

        public ImageChooserPage()
        {
            this.InitializeComponent();
            this.NavigationCacheMode = NavigationCacheMode.Required;

            this.gridView.ItemsSource = new string[]
            {
                "ms-appx:///Assets/LockScreenLogo.scale-200.png",
                "ms-appx:///Assets/SplashScreen.scale-200.png",
                "ms-appx:///Assets/Square150x150Logo.scale-200.png",
                "ms-appx:///Assets/Square44x44Logo.scale-200.png",
            };
        }

        protected override void OnNavigatedFrom(NavigationEventArgs e)
        {
            base.OnNavigatedFrom(e);
            if (e.NavigationMode == NavigationMode.Back)
            {
                this.NavigationCacheMode = NavigationCacheMode.Disabled;
            }
        }

        private void gridView_ItemClick(object sender, ItemClickEventArgs e)
        {
            var image = e.ClickedItem as string;
            if (image != null)
            {
                this.Value = image;
                if (this.Frame.CanGoBack)
                {
                    this.Frame.GoBack();
                }
            }
        }
    }
}
