using Hansot_kiosk.Common;

namespace Hansot_kiosk.Model
{
    public class MenuModel
    {
        public int IDX { get; set; }
        public string Name { get; set; }
        public int Price { get; set; }
        public string Path { get; set; }
        public Category Category { get; set; }
        public int Page { get; set; }
        public int Amount { get; set; }
    }
}
