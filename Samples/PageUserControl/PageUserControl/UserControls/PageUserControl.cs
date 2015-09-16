using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;
using Windows.UI.Xaml.Navigation;

namespace PageUserControl.UserControls
{
    public abstract class PageUserControl<TPage>
        where TPage : Page
    {
        private const string _FrameNameInFramePage = "childrenFrame";

        private Frame _frame;
        private object _frameContentWhenOpened;
        private TPage _page;

        /// <summary>
        /// 获取是否优先呈现在ChildrenFrame中。
        /// </summary>
        public bool IsChildrenFrameFirst { get; protected set; }

        #region Methods

        protected void ShowPage()
        {
            this.OpenPickerPage();
        }

        protected void ShowPage(object parameter)
        {
            this.OpenPickerPage(parameter);
        }

        //若需向调用者返回某值，则需要实现此方法。
        protected virtual void CommitValue(TPage page)
        {
        }

        private void OpenPickerPage(object parameter = null)
        {
            if (null == _frame)
            {
                _frame = Window.Current.Content as Frame;
                if (null != _frame)
                {
                    //这里是约定MainPage页中childrenFrame是子Frame。
                    //此方法并非绝对，仍有很多灵活的方法可以扩展，比如附加属性来指定谁是ChildrenFrame。
                    if (this.IsChildrenFrameFirst && this._frame.CurrentSourcePageType.Equals(typeof(Pages.MainPage)))
                    {
                        var framePage = (Pages.MainPage)_frame.Content;
                        var frameInFramePage = framePage.FindName(_FrameNameInFramePage) as Frame;
                        if (frameInFramePage != null)
                        {
                            this._frame = frameInFramePage;
                        }
                    }

                    _frameContentWhenOpened = _frame.Content;

                    _frame.Navigated += OnFrameNavigated;
                    _frame.NavigationStopped += OnFrameNavigationStopped;
                    _frame.NavigationFailed += OnFrameNavigationFailed;

                    if (parameter == null)
                    {
                        _frame.Navigate(typeof(TPage));
                    }
                    else
                    {
                        _frame.Navigate(typeof(TPage), parameter);
                    }
                }
            }
        }

        private void ClosePickerPage()
        {
            // 注销事件
            if (null != _frame)
            {
                _frame.Navigated -= OnFrameNavigated;
                _frame.NavigationStopped -= OnFrameNavigationStopped;
                _frame.NavigationFailed -= OnFrameNavigationFailed;

                _frame = null;
                _frameContentWhenOpened = null;
            }

            //若缓存页面有值，则尝试做提交处理。
            if (null != this._page)
            {
                this.CommitValue(this._page);
                this._page = null;
            }
        }

        #endregion

        #region Events

        private void OnFrameNavigated(object sender, NavigationEventArgs e)
        {
            //若是Back则做关闭处理，若是Forward则把新页缓存。
            if (e.Content == _frameContentWhenOpened)
            {
                ClosePickerPage();
            }
            else if (null == this._page)
            {
                var page = e.Content as TPage;

                if (page != null)
                {
                    this._page = page;
                }
            }
        }

        private void OnFrameNavigationFailed(object sender, NavigationFailedEventArgs e)
        {
            ClosePickerPage();
        }

        private void OnFrameNavigationStopped(object sender, NavigationEventArgs e)
        {
            ClosePickerPage();
        }

        #endregion
    }
}
