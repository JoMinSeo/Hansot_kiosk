using System.Collections.Generic;
namespace Hansot_kiosk.Model
{
    public class TcpModel
    {
        public int MSGType = 0;
        public string Id = "2219";
        public string Content = string.Empty;
        public string ShopName = "한솥 구지점";
        public bool Group = false;
        public List<OrderInfo> Menus = new List<OrderInfo>();
        public int OrderNumber = 0;
    }

    public class OrderInfo {
        public string Name = string.Empty;
        public int Count = 0;
        public int Price = 0;

        public OrderInfo(string name, int count, int price)
        {
            Name = name;
            Count = count;
            Price = price;
        }
    }


}
