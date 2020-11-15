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
            get => _currentOrder;
            set
            {
                _currentOrder = value;
                OnPropertyChanged(nameof(CurrentOrder));
            }
        }
        private ObservableCollection<MenuModel> _orderedMenus = new ObservableCollection<MenuModel>();
        public ObservableCollection<MenuModel> OrderedMenus
        {
            get => _orderedMenus;
            set
            {
                _orderedMenus = value;
                OnPropertyChanged(nameof(OrderedMenus));
            }
        }
        private int _totalPrice;
        public int TotalPrice
        {
            get => _totalPrice;
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
        public void init()
        {
            CurrentOrder = new OrderModel();
            OrderedMenus.Clear();

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