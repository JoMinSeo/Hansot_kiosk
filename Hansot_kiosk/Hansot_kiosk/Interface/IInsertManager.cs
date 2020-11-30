using Hansot_kiosk.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Hansot_kiosk.Interface
{
    interface IInsertManager
    {
        void InsertOrder(OrderModel orderModel);
        void InsertOrderedMenu(MenuModel menuModel, int orderIDX);
    }
}
