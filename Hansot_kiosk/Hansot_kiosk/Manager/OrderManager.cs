using Hansot_kiosk.Common;
using Hansot_kiosk.Model;
using System.Collections.Generic;

namespace Hansot_kiosk.Manager
{
    public class OrderManager
    {
        public List<Model.Menu> SelectedMenus { get; set; } = new List<Menu>();
    }
}