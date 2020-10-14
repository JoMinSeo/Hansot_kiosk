using Hansot_kiosk.Common;
using Hansot_kiosk.Model;
using System.Collections.Generic;

namespace Hansot_kiosk.Manager
{
    public class OrderManager
    {
        public List<Menu> lstMenu { get; set; }
        public OrderManager()
        {
            if (lstMenu == null)
            {
                lstMenu = new List<Menu>();
            }
            loadMenu();
        }
        private void loadMenu()
        {
            lstMenu.Add(new Menu()
            {
                Category = Category.meatmeat,
                Name = "고기고기",
                Path = @"Assets/Menu/meatmeat/고기고기.jpg"
            });
            lstMenu.Add(new Menu()
            {
                Category = Category.meatmeat,
                Name = "돈까스고기고기",
                Path = @"Assets/Menu/meatmeat/돈까스도련님고기고기.jpg"
            });
            lstMenu.Add(new Menu()
            {
                Category = Category.meatmeat,
                Name = "돈치고기고기",
                Path = @"Assets/Menu/meatmeat/돈치 고기고기.jpg"
            });
            lstMenu.Add(new Menu()
            {
                Category = Category.meatmeat,
                Name = "새치고기고기",
                Path = @"Assets/Menu/meatmeat/새치 고기고기.jpg"
            });
            lstMenu.Add(new Menu()
            {
                Category = Category.meatmeat,
                Name = "생선까스도련님고기고기",
                Path = @"Assets/Menu/meatmeat/생선까스도련님고기고기.jpg"
            });
            lstMenu.Add(new Menu()
            {
                Category = Category.meatmeat,
                Name = "탕수육도련님고기고기",
                Path = @"Assets/Menu/meatmeat/탕수육도련님고기고기.jpg"
            });
        }
    }
}