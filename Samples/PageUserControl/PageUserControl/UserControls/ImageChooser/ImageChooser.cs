using System;

namespace PageUserControl.UserControls
{
    public class ImageChooser : PageUserControl<ImageChooserPage>
    {
        public ImageChooser()
        {
            //优先在ChildrenFrame呈现。
            base.IsChildrenFrameFirst = true;
        }

        public void Show()
        {
            base.ShowPage();
        }

        protected override void CommitValue(ImageChooserPage page)
        {
            base.CommitValue(page);

            //若标识结果的页面属性值有效，则通过事件抛给调用者。
            if (!string.IsNullOrWhiteSpace(page.Value))
            {
                this.OnCompleted(page.Value);
            }
        }

        public event EventHandler<ChooseImageCompletedEventArgs> Completed;
        private void OnCompleted(string image)
        {
            var handler = this.Completed;
            if (handler != null)
            {
                handler(this, new ChooseImageCompletedEventArgs(image));
            }
        }
    }

    public class ChooseImageCompletedEventArgs : EventArgs
    {
        public string Image { get; private set; }

        internal ChooseImageCompletedEventArgs(string image)
        {
            this.Image = image;
        }
    }
}
