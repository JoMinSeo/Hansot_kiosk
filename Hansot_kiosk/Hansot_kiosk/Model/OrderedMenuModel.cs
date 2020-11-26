using Hansot_kiosk.Common;

namespace Hansot_kiosk.Model
{
    public class OrderedMenuModel
    {
        public int IDX { get; set; }
        public int OrderIdx { get; set; }
        public int MenuIDX { get; set; }
        public ECategory Category { get; set; }
        public int Price { get; set; }
        public int Amount { get; set; }
    }
}
