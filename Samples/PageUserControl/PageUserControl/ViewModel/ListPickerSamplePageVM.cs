using System.Collections.ObjectModel;
using RolerFramework;
using RolerFramework.Command;

namespace PageUserControl.ViewModel
{
    public class ListPickerSamplePageVM : BindableBase
    {
        private ObservableCollection<string> _sexes = new ObservableCollection<string>();
        private string _sex;
        private ObservableCollection<string> _images = new ObservableCollection<string>();
        private string _image;

        public ObservableCollection<string> Sexes
        {
            get { return this._sexes; }
            set
            {
                this.SetProperty(ref this._sexes, value);
            }
        }
        public string Sex
        {
            get { return this._sex; }
            set
            {
                this.SetProperty(ref this._sex, value);
            }
        }
        public ObservableCollection<string> Images
        {
            get { return this._images; }
            set
            {
                this.SetProperty(ref this._images, value);
            }
        }
        public string Image
        {
            get { return this._image; }
            set
            {
                this.SetProperty(ref this._image, value);
            }
        }

        public ListPickerSamplePageVM()
        {
            this.SexPickedCommand = new RolerCommand<string>(p => this.SexPickedCommandExecute(p));
            this.ImagePickedCommand = new RolerCommand<string>(p => this.ImagePickedCommandExecute(p));
        }

        #region Commmands

        public RolerCommand<string> SexPickedCommand { get; private set; }
        private void SexPickedCommandExecute(string sex)
        {
            this.Sex = sex;
        }

        public RolerCommand<string> ImagePickedCommand { get; private set; }
        private void ImagePickedCommandExecute(string image)
        {
            this.Image = image;
        }

        #endregion

        #region Methods

        public void LoadData()
        {
            this.Sexes = new ObservableCollection<string> { "男", "女", "第三类" };
            this.Images = new ObservableCollection<string>
            {
                "ms-appx:///Assets/LockScreenLogo.scale-200.png",
                "ms-appx:///Assets/SplashScreen.scale-200.png",
                "ms-appx:///Assets/Square150x150Logo.scale-200.png",
                "ms-appx:///Assets/Square44x44Logo.scale-200.png",
            };
        }

        #endregion
    }
}
