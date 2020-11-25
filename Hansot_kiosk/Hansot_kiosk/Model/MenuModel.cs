using Hansot_kiosk.Common;
using System.ComponentModel;

namespace Hansot_kiosk.Model
{
    public class MenuModel : INotifyPropertyChanged
    {
        private int _idx;
        public int IDX
        {
            get => _idx;
            set
            {
                _idx = value;
                OnPropertyChanged(nameof(IDX));
            }
        }
        private string _name;
        public string Name
        {
            get => _name;
            set
            {
                _name = value;
                OnPropertyChanged(nameof(Name));
            }
        }
        private int _price;
        public int Price
        {
            get => _price;
            set
            {
                _price = value;
                DiscountedPrice = value;
                OnPropertyChanged(nameof(Price));
            }
        }
        private string _path;
        public string Path
        {
            get => _path;
            set
            {
                _path = value;
                OnPropertyChanged(nameof(Path));
            }
        }
        private ECategory _category;
        public ECategory Category
        {
            get => _category;
            set
            {
                _category = value;
                OnPropertyChanged(nameof(Category));
            }
        }
        private int _amount;
        public int Amount
        {
            get => _amount;
            set
            {
                _amount = value;
                OnPropertyChanged(nameof(Amount));
            }
        }
        private bool _isDiscounted;
        public bool IsDiscounted
        {
            get => _isDiscounted;
            set
            {
                _isDiscounted = value;
                OnPropertyChanged(nameof(IsDiscounted));
            }
        }
        private int _discountedPer;
        public int DiscountedPer
        {
            get => _discountedPer;
            set
            {
                _discountedPer = value;
                if (DiscountedPer > 0)
                {
                    IsDiscounted = true;
                    DiscountedPrice = ((100 - DiscountedPer) * Price)/100;
                }
                OnPropertyChanged(nameof(DiscountedPer));
            }
        }
        private int _discountedPrice;
        public int DiscountedPrice
        {
            get => _discountedPrice;
            set
            {
                _discountedPrice = value;
                OnPropertyChanged(nameof(DiscountedPrice));
            }
        }
        #region PropertyChangedEvent
        public event PropertyChangedEventHandler PropertyChanged;
        protected void OnPropertyChanged(string propetryName)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propetryName));
        }
        #endregion
    }
}
