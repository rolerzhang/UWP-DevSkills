using RolerFramework;
using RolerFramework.Command;
using Windows.UI.Xaml;

namespace PageUserControl.UserControls
{
    public class ListPickerPageVM : BindableBase
    {
        private const string _DefaultTitle = "请选择一项";

        private object _itemWaitSelected;

        private object _selectedItem;
        private object _itemsSources;
        private DataTemplate _itemTemplate;

        public object SelectedItem
        {
            get { return this._selectedItem; }
            set
            {
                this.SetProperty(ref this._selectedItem, value);
            }
        }
        public object ItemsSources
        {
            get { return this._itemsSources; }
            set
            {
                this.SetProperty(ref this._itemsSources, value);
            }
        }
        public DataTemplate ItemTemplate
        {
            get { return this._itemTemplate; }
            set
            {
                this.SetProperty(ref this._itemTemplate, value);
            }
        }

        public ListPickerPageVM()
        {
            this.PageLoadCommand = new RolerCommand(this.PageLoadCommandExecute);
        }

        #region Commmands

        public RolerCommand PageLoadCommand { get; private set; }
        private void PageLoadCommandExecute()
        {
            this.SelectedItem = this._itemWaitSelected;
        }

        #endregion

        #region Methods

        internal void TryLoadData(ListPickerPageParameter parameter)
        {
            this.FillDataFromParameter(parameter);
        }

        private void FillDataFromParameter(ListPickerPageParameter parameter)
        {
            this.SelectedItem = null;
            if (parameter == null)
            {
                this.ItemsSources = null;
                this.ItemTemplate = null;
            }
            else
            {
                this.ItemsSources = parameter.ItemsSources;
                this.ItemTemplate = parameter.ItemTemplate;
                this._itemWaitSelected = parameter.SelectedItem;
            }
        }

        internal void RefreshNotify()
        {
            this.OnPropertyChanged("SelectedItem");
        }

        #endregion
    }
}
