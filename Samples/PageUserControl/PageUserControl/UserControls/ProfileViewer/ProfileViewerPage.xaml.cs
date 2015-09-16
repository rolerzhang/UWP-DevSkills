using Windows.UI.Xaml.Controls;

namespace PageUserControl.UserControls
{
    public sealed partial class ProfileViewerPage : Page
    {
        public ProfileViewerPage()
        {
            this.InitializeComponent();
        }

        private void BackButton_Click(object sender, Windows.UI.Xaml.RoutedEventArgs e)
        {
            if (this.Frame.CanGoBack)
            {
                this.Frame.GoBack();
            }
        }
    }
}
