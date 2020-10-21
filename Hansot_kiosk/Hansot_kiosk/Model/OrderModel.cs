using System;

namespace Hansot_kiosk.Model
{
    public class OrderModel
    {
        public int IDX { get; set; }
        public int User_IDX { get; set; }
        public int Table { get; set; }
        public bool IsCard { get; set; }
        public DateTime OrderedTime { get; set; }
    }
}
