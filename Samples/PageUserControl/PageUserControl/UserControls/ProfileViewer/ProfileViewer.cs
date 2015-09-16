namespace PageUserControl.UserControls
{
    public class ProfileViewer : PageUserControl<ProfileViewerPage>
    {
        public void Show()
        {
            base.ShowPage();
        }

        protected override void CommitValue(ProfileViewerPage page)
        {
            //若无返回的业务需要，则留空此方法。
        }
    }
}
