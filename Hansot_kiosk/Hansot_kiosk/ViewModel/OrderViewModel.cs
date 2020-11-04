﻿using Hansot_kiosk.Common;
using Hansot_kiosk.Model;
using System.Collections.Generic;

namespace Hansot_kiosk.ViewModel
{
    public class OrderViewModel
    {
        public List<MenuModel> lstMenu = new List<MenuModel>();
        //{
        //    new Menu(){ Category = Category.meatmeat, Name = "고기고기", Path = @"/Assets/Menu/meatmeat/고기고기.jpg"},
        //    new Menu(){ Category = Category.meatmeat, Name = "돈까스고기고기", Path = @"/Assets/Menu/meatmeat/돈까스도련님고기고기.jpg"},
        //    new Menu(){ Category = Category.meatmeat, Name = "돈치고기고기", Path = @"/Assets/Menu/meatmeat/돈치 고기고기.jpg"}
        //};

        public OrderViewModel()
        {
            //initMenu();
        }

        public void LoadMenu()
        {
            lstMenu.Add(new MenuModel() { Category = ECategory.MEATMEAT, Name = "고기고기", Path = @"Assets/Menu/meatmeat/고기고기.jpg" });
            lstMenu.Add(new MenuModel() { Category = ECategory.MEATMEAT, Name = "돈까스고기고기", Path = @"/Assets/Menu/meatmeat/돈까스도련님고기고기.jpg" });
            lstMenu.Add(new MenuModel() { Category = ECategory.MEATMEAT, Name = "돈치고기고기", Path = @"Assets/Menu/meatmeat/돈치 고기고기.jpg" });
            //lstMenu = new List<Menu>()
            //    {
            //        new Menu(){ Category = Category.meatmeat, Name = "고기고기", Path = @"/Assets/Menu/meatmeat/고기고기.jpg"},
            //        new Menu(){ Category = Category.meatmeat, Name = "돈까스고기고기", Path = @"/Assets/Menu/meatmeat/돈까스도련님고기고기.jpg"},
            //        new Menu(){ Category = Category.meatmeat, Name = "돈치고기고기", Path = @"/Assets/Menu/meatmeat/돈치 고기고기.jpg"}
            //    };

        }

        //private void initMenu()
        //{
        //    lstMenu = new List<Menu>()
        //    {
        //        new Menu(){ Category = Category.meatmeat, Name = "고기고기", Path = @"/Assets/Menu/meatmeat/고기고기.jpg"},
        //        new Menu(){ Category = Category.meatmeat, Name = "돈까스고기고기", Path = @"/Assets/Menu/meatmeat/돈까스도련님고기고기.jpg"},
        //        new Menu(){ Category = Category.meatmeat, Name = "돈치고기고기", Path = @"/Assets/Menu/meatmeat/돈치 고기고기.jpg"}
        //    };
        //}
    }
}
