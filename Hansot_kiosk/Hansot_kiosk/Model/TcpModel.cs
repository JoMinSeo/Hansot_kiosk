using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Hansot_kiosk.Model
{
    public class TcpModel
    {
        public int MSGType = 0;
        public string Id = "2219";
        public string Content = string.Empty;
        public string ShopName = string.Empty;
        public List<OrderInfo> orderinfo = new List<OrderInfo>();
        public string OrderNumber = string.Empty;
    }

    public class OrderInfo {
        string Name = string.Empty;
        int Count = 0;
        int Price = 0;
    }
}
