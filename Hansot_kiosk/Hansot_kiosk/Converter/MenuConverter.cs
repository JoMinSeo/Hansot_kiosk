using Hansot_kiosk.Model;
using System;
using System.Globalization;
using System.Windows.Data;

namespace Hansot_kiosk.Converter
{
    public class MenuConverter : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            throw new NotImplementedException();
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            throw new NotImplementedException();
        }

        public OrderedMenuModel MenuToOrderedMenu(MenuModel menu)
        {
            OrderedMenuModel orderedMenu = new OrderedMenuModel();

            orderedMenu.MenuIDX = menu.IDX;
            orderedMenu.MenuName = menu.Name;
            orderedMenu.Category = menu.Category;

            return orderedMenu;
        }
    }
}
