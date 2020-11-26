using Hansot_kiosk.Common;
using Hansot_kiosk.Model;
using LiveCharts;
using LiveCharts.Wpf;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace Hansot_kiosk.Control
{
    /// <summary>
    /// Interaction logic for AdminCtrl.xaml
    /// </summary>
    public partial class AdminCtrl : UserControl
    {
        public AdminCtrl()
        {
            InitializeComponent();

            lv_Users.ItemsSource = App.Users;
            lv_Menus.ItemsSource = App.Menus;

            this.DataContext = Properties.Settings.Default;
        }

        private void btn_Statistic_Click(object sender, RoutedEventArgs e)
        {
            UserControl uc = App.UIStateManager.Get(UICategory.STATISTIC);
            if (uc != null)
                App.UIStateManager.Push(uc);
        }

        private void cbx_AutoLogin_Checked(object sender, RoutedEventArgs e)
        {
            Properties.Settings.Default.isAutoLogin = true;
            Properties.Settings.Default.Save();
        }

        private void cbx_AutoLogin_Unchecked(object sender, RoutedEventArgs e)
        {
            Properties.Settings.Default.isAutoLogin = false;
            Properties.Settings.Default.Save();
        }

        private void tbx_DiscountedPer_KeyDown(object sender, KeyEventArgs e)
        {
            Debug.WriteLine(e.Key);
            if (!((e.Key.ToString().Length > 1) && (char.IsDigit(e.Key.ToString(), 1)) || Key.Back == e.Key))
            {
                e.Handled = true;
            }
        }

        private void tbx_DiscountedPer_TextChanged(object sender, TextChangedEventArgs e)
        {
            MenuModel senderMenu = (sender as TextBox).DataContext as MenuModel;

            if (senderMenu != null)
            {
                App.SQLManager.UpdateDiscountedPer(senderMenu.IDX, senderMenu.DiscountedPer);
            }
            else
            {
                App.SQLManager.UpdateDiscountedPer(senderMenu.IDX, 0);
            }
        }
    }
}
