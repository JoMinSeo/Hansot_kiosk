using Hansot_kiosk.Model;
using System;
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
            Init();
        }
        public void Init()
        {
            CurrentOrder = new OrderModel();
            OrderedMenus.Clear();

            TotalPrice = 0;
        }
        public void CompleteOrder()
        {
            this.CurrentOrder.OrderedTime = DateTime.Now;
            // DB로 전송하는 코드

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