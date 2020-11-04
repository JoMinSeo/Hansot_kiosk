using Hansot_kiosk.Model;
using System.Collections.ObjectModel;
using System.ComponentModel;

namespace Hansot_kiosk.Manager
{
    public class OrderManager : INotifyPropertyChanged
    {
        #region Property
        private OrderModel _currentOrder;
        public OrderModel CurrentOrder
        {
            get { return _currentOrder; }
            set
            {
                _currentOrder = value;
                OnPropertyChanged(nameof(CurrentOrder));
            }
        }
        private ObservableCollection<MenuModel> _orderedMenus;
        public ObservableCollection<MenuModel> OrderedMenus
        {
            get { return _orderedMenus; }
            set
            {
                _orderedMenus = value;
                OnPropertyChanged(nameof(OrderedMenus));
            }
        }
        private int _totalPrice;
        public int TotalPrice
        {
            get
            {
                return _totalPrice;
            }
            set
            {
                _totalPrice = value;
                OnPropertyChanged(nameof(TotalPrice));
            }
        }
        #endregion
        public OrderManager()
        {
            init();
        }
        private void init()
        {
            CurrentOrder = new OrderModel();
            OrderedMenus = new ObservableCollection<MenuModel>();

            TotalPrice = 0;

            CurrentOrder.IDX = -1;
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