using PageUserControl.UserControls;
using Windows.UI.Popups;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;
using Windows.UI.Xaml.Navigation;

namespace PageUserControl.Pages
{
    public sealed partial class MainPage : Page
    {
        public MainPage()
        {
            this.InitializeComponent();
            this.NavigationCacheMode = NavigationCacheMode.Required;

            this.childrenFrame.Navigate(typeof(HomePage));
        }

        protected override void OnNavigatedFrom(NavigationEventArgs e)
        {
            base.OnNavigatedFrom(e);
            if (e.NavigationMode == NavigationMode.Back)
            {
                this.NavigationCacheMode = NavigationCacheMode.Disabled;
            }
        }

        #region Events

        private void PaneButton_Click(object sender, RoutedEventArgs e)
        {
            this.splitView.IsPaneOpen = !this.splitView.IsPaneOpen;
        }

        private void ProfileViewerButton_Click(object sender, RoutedEventArgs e)
        {
            this.splitView.IsPaneOpen = false;

            var profileViewer = new ProfileViewer();
            profileViewer.Show();
        }

        private void ImageChooserButton_Click(object sender, RoutedEventArgs e)
        {
            this.splitView.IsPaneOpen = false;

            var imageChooser = new ImageChooser();
            imageChooser.Completed += ImageChooser_Completed;
            imageChooser.Show();
        }

        private void ListPickerButton_Click(object sender, RoutedEventArgs e)
        {
            this.splitView.IsPaneOpen = false;
            this.childrenFrame.Navigate(typeof(ListPickerSamplePage));
        }

        private void ImageChooser_Completed(object sender, ChooseImageCompletedEventArgs e)
        {
            ((ImageChooser)sender).Completed -= ImageChooser_Completed;

            var messageDialog = new MessageDialog(e.Image);
            messageDialog.ShowAsync();
        }


        #endregion
    }
}
