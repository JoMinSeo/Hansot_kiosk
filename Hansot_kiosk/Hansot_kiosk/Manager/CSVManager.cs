using Hansot_kiosk.Interface;
using Hansot_kiosk.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Hansot_kiosk
{
    public class CSVManager : IInsertManager
    {
        public void InsertOrder(OrderModel orderModel)
        {
            using (System.IO.StreamWriter file = new System.IO.StreamWriter
                (@"csv\Order.csv", true, System.Text.Encoding.GetEncoding("utf-8")))
            {
                file.WriteLine("{0}, {1}, {2}, {3}, {4}, {5}",
                    orderModel.IDX, orderModel.User_IDX, orderModel.Seat_IDX, orderModel.IsCard,
                    orderModel.OrderedTime.ToString("yyyy/MM/dd HH:mm:ss"), orderModel.TotalPrice);
            }
        }

        public void InsertOrderedMenu(MenuModel menuModel, int orderIDX)
        {
            using (System.IO.StreamWriter file = new System.IO.StreamWriter
                (@"csv\OrderedMenu.csv", true, System.Text.Encoding.GetEncoding("utf-8")))
            {
                file.WriteLine("{0}, {1}, {2}",
                    orderIDX, menuModel.IDX, menuModel.Amount);
            }
        }
    }
}
