using Hansot_kiosk.Common;
using Hansot_kiosk.Model;
using System.Collections.Generic;

namespace Hansot_kiosk.Manager
{
    public class OrderMenuManager
    {
        public List<MenuModel> ListMenu { get; set; } = new List<MenuModel>();
        public OrderMenuManager()
        {
            if (ListMenu == null)
            {
                ListMenu = new List<MenuModel>();
            }
            loadMenu();
        }
        private void loadMenu()
        {
            ListMenu.Add(new MenuModel()
            {
                Category = Category.MEATMEAT,
                Name = "고기고기",
                Path = "Assets/Menu/meatmeat/고기고기.jpg",
                Page = 1
            });
            ListMenu.Add(new MenuModel()
            {
                Category = Category.MEATMEAT,
                Name = "돈까스고기고기",
                Path = @"Assets/Menu/meatmeat/돈까스도련님고기고기.jpg",
                Page = 1
            });
            ListMenu.Add(new MenuModel()
            {
                Category = Category.MEATMEAT,
                Name = "돈치고기고기",
                Path = @"Assets/Menu/meatmeat/돈치 고기고기.jpg",
                Page = 1
            });
            ListMenu.Add(new MenuModel()
            {
                Category = Category.MEATMEAT,
                Name = "새치고기고기",
                Path = @"Assets/Menu/meatmeat/새치 고기고기.jpg",
                Page = 1
            });
            ListMenu.Add(new MenuModel()
            {
                Category = Category.MEATMEAT,
                Name = "생선까스도련님고기고기",
                Path = @"Assets/Menu/meatmeat/생선까스도련님고기고기.jpg",
                Page = 1
            });
            ListMenu.Add(new MenuModel()
            {
                Category = Category.MEATMEAT,
                Name = "탕수육도련님고기고기",
                Path = @"Assets/Menu/meatmeat/탕수육도련님고기고기.jpg",
                Page = 1
            });
            ListMenu.Add(new MenuModel()
            {
                Category = Category.SET,
                Name = "고추장숯불삼겹정식",
                Path = @"Assets/Menu/meatmeat/탕수육도련님고기고기.jpg",
                Page = 1
            });
            ListMenu.Add(new MenuModel()
            {
                Category = Category.SET,
                Name = "국화",
                Path = @"Assets/Menu/meatmeat/탕수육도련님고기고기.jpg",
                Page = 1
            });
            ListMenu.Add(new MenuModel()
            {
                Category = Category.SET,
                Name = "돈까스도련님",
                Path = @"Assets/Menu/meatmeat/탕수육도련님고기고기.jpg",
                Page = 1
            });
        }
    }
}
