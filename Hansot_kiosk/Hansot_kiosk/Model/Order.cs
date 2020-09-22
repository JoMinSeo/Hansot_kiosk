using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Hansot_kiosk.Model
{
    public class Order
    {
        public int IDX { get; set; }
        public int User_IDX { get; set; }
        public int Table { get; set; }
        public bool IsCard { get; set; }
        public DateTime OrderedTime { get; set; }
    }
}
