using Hansot_kiosk.Common;
using Hansot_kiosk.Model;
using System.Collections.Generic;
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
        private List<MenuModel> _orderedMenus;
        public List<MenuModel> OrderedMenus
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
            OrderedMenus = new List<MenuModel>();
            //OrderedMenus.Add(new MenuModel()
            //{
            //    Category = Category.MEATMEAT,
            //    Name = "고기고기",
            //    Path = "Assets/Menu/meatmeat/고기고기.jpg",
            //    Page = 1
            //});
            //OrderedMenus.Add(new MenuModel()
            //{
            //    Category = Category.MEATMEAT,
            //    Name = "돈까스고기고기",
            //    Path = @"Assets/Menu/meatmeat/돈까스도련님고기고기.jpg",
            //    Page = 1
            //});
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