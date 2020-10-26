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
        #endregion

        public OrderManager()
        {
            if (CurrentOrder == null)
            {
                CurrentOrder = new OrderModel();
            }
            if(OrderedMenus == null)
            {
                OrderedMenus = new ObservableCollection<MenuModel>();
            }
        }
        #region PropertyChangedEvent
        public event PropertyChangedEventHandler PropertyChanged;
        protected void OnPropertyChanged(string propetryName)
        {
            if(PropertyChanged != null)
            {
                PropertyChanged(this, new PropertyChangedEventArgs(propetryName));
            }
        }
        #endregion
    }
}