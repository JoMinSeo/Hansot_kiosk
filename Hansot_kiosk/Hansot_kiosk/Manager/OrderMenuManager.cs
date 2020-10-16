using Hansot_kiosk.Common;
using Hansot_kiosk.Model;
using System.Collections.Generic;

namespace Hansot_kiosk.Manager
{
    public class OrderMenuManager
    {
        public List<Menu> ListMenu { get; set; } = new List<Menu>();
        public OrderMenuManager()
        {
            if (ListMenu == null)
            {
                ListMenu = new List<Menu>();
            }
            loadMenu();
        }
        private void loadMenu()
        {
            ListMenu.Add(new Menu()
            {
                Category = Category.meatmeat,
                Name = "고기고기",
                Path = @"/Assets/Menu/meatmeat/고기고기.jpg",
                Page = 1
            });
            ListMenu.Add(new Menu()
            {
                Category = Category.meatmeat,
                Name = "돈까스고기고기",
                Path = @"/Assets/Menu/meatmeat/돈까스도련님고기고기.jpg",
                Page = 1
            });
            ListMenu.Add(new Menu()
            {
                Category = Category.meatmeat,
                Name = "돈치고기고기",
                Path = @"/Assets/Menu/meatmeat/돈치 고기고기.jpg",
                Page = 1
            });
            ListMenu.Add(new Menu()
            {
                Category = Category.meatmeat,
                Name = "새치고기고기",
                Path = @"/Assets/Menu/meatmeat/새치 고기고기.jpg",
                Page = 1
            });
            ListMenu.Add(new Menu()
            {
                Category = Category.meatmeat,
                Name = "생선까스도련님고기고기",
                Path = @"/Assets/Menu/meatmeat/생선까스도련님고기고기.jpg",
                Page = 1
            });
            ListMenu.Add(new Menu()
            {
                Category = Category.meatmeat,
                Name = "탕수육도련님고기고기",
                Path = @"/Assets/Menu/meatmeat/탕수육도련님고기고기.jpg",
                Page = 1
            });
        }
    }
}
