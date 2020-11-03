using Hansot_kiosk.Common;
using Hansot_kiosk.Model;
using System.Collections.Generic;
using System.Collections.ObjectModel;

namespace Hansot_kiosk.Manager
{
    public class MenuManager
    {
        public ObservableCollection<MenuModel> ListMenu { get; set; } = new ObservableCollection<MenuModel>();
        public MenuManager()
        {
            if (ListMenu == null)
            {
                ListMenu = new ObservableCollection<MenuModel>();
            }
            loadMenu();
        }
        private void loadMenu()
        {
            ListMenu.Add(new MenuModel()
            {
                Category = Category.MEATMEAT,
                Name = "고기고기",
                Path = @"https://www.hsd.co.kr/images/93ceb60dc7c04f2aa9dd4f044d8ad1ea20190628094358.jpg",
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
                Path = @"Assets/Menu/meatmeat/donchiMeatmeat.jpg",
                Page = 1
            });
            ListMenu.Add(new MenuModel()
            {
                Category = Category.MEATMEAT,
                Name = "새치고기고기",
                Path = @"Assets/Menu/meatmeat/SechiMeatmeat.jpg",
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
