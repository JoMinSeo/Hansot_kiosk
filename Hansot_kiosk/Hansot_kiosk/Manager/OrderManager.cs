using Hansot_kiosk.Model;
using System.Collections.Generic;

namespace Hansot_kiosk.Manager
{
    public class OrderManager
    {
        public OrderModel currentOrder { get; set; }
        public List<MenuModel> orderedMenus { get; set; }

        public OrderManager()
        {
            currentOrder = new OrderModel();
            orderedMenus = new List<MenuModel>();
        }
    }
}