using Hansot_kiosk.Common;
using Hansot_kiosk.Model;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;

namespace Hansot_kiosk.Manager
{
    public class OrderManager : INotifyPropertyChanged
    {
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

        public OrderManager()
        {
            CurrentOrder = new OrderModel();
            OrderedMenus = new ObservableCollection<MenuModel>();
        }

        public event PropertyChangedEventHandler PropertyChanged;
        protected void OnPropertyChanged(string propetryName)
        {
            if(PropertyChanged != null)
            {
                PropertyChanged(this, new PropertyChangedEventArgs(propetryName));
            }
        }
    }
}