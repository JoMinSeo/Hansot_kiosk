using Hansot_kiosk.Common;
using System.ComponentModel;

namespace Hansot_kiosk.Model
{
    public class MenuModel : INotifyPropertyChanged
    {
        private int _idx;
        public int IDX
        {
            get
            {
                return _idx;
            }
            set
            {
                _idx = value;
                OnPropertyChanged(nameof(IDX));
            }
        }
        private string _name;
        public string Name
        {
            get
            {
                return _name;
            }
            set
            {
                _name = value;
                OnPropertyChanged(nameof(Name));
            }
        }
        private int _price;
        public int Price
        {
            get
            {
                return _price;
            }
            set
            {
                _price = value;
                OnPropertyChanged(nameof(Price));
            }
        }
        private string _path;
        public string Path
        {
            get
            {
                return _path;
            }
            set
            {
                _path = value;
                OnPropertyChanged(nameof(Path));
            }
        }
        private ECategory _category;
        public ECategory Category
        {
            get
            {
                return _category;
            }
            set
            {
                _category = value;
                OnPropertyChanged(nameof(Category));
            }
        }
        private int _page;
        public int Page
        {
            get
            {
                return _page;
            }
            set
            {
                _page = value;
                OnPropertyChanged(nameof(Page));
            }
        }
        private int _amount;
        public int Amount
        {
            get
            {
                return _amount;
            }
            set
            {
                _amount = value;
                OnPropertyChanged(nameof(Amount));
            }
        }

        #region PropertyChangedEvent
        public event PropertyChangedEventHandler PropertyChanged;
        protected void OnPropertyChanged(string propetryName)
        {
            if (PropertyChanged != null)
            {
                PropertyChanged(this, new PropertyChangedEventArgs(propetryName));
            }
        }
        #endregion
    }
}
