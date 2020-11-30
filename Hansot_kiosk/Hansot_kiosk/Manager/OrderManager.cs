using Hansot_kiosk.Model;
using System;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Linq;

namespace Hansot_kiosk.Manager
{
    public class OrderManager : INotifyPropertyChanged
    {
        #region Property
        private OrderModel _currentOrder = new OrderModel();
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
        #endregion
        public OrderManager()
        {
            App.InitDeleGate += init;
        }
        private void init()
        {
            CurrentOrder.init();
            OrderedMenus.Clear();

            CurrentOrder.IDX = (from orderModel in App.Orders select orderModel).Max(orderModel => orderModel.IDX) + 1;
        }
        public void CompleteOrder()
        {
            this.CurrentOrder.OrderedTime = DateTime.Now;
            App.SQLManager.InsertOrder(this.CurrentOrder);
            App.CSVManager.InsertOrder(this.CurrentOrder);

            TcpModel tcpModel = new TcpModel();
            tcpModel.MSGType = 2;
            tcpModel.OrderNumber = this.CurrentOrder.IDX;

            foreach (MenuModel menu in OrderedMenus)
            {
                App.SQLManager.InsertOrderedMenu(menu, this.CurrentOrder.IDX);
                App.CSVManager.InsertOrderedMenu(menu, this.CurrentOrder.IDX);
                tcpModel.Menus.Add(new OrderInfo(menu.Name,menu.Amount, menu.Price));
            }

            if (App.isLogined)
            {
                App.TCPManager.PostMessage(tcpModel);
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