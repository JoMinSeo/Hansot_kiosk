﻿using Kiosk.UIManager;
using System.Windows;
using System.Windows.Controls;

namespace Hansot_kiosk.Control
{
    /// <summary>
    /// PaySelectCtrl.xaml에 대한 상호 작용 논리
    /// </summary>
    public partial class PaySelectCtrl : UserControl
    {
        public PaySelectCtrl()
        {
            InitializeComponent();

            ((MainWindow)System.Windows.Application.Current.MainWindow).DeleGate += init;
        }

        private void init()
        {
            lvOrderdMenus.ItemsSource = App.OrderManager.OrderedMenus;
            this.DataContext = App.OrderManager;
        }

        private void PreviusBtn_Click(object sender, RoutedEventArgs e)
        {
            App.UIStateManager.Pop();
        }

        private void CreditBtn_Click(object sender, RoutedEventArgs e)
        {
            App.OrderManager.CurrentOrder.IsCard = true;
            UserControl uc = App.UIStateManager.Get(UICategory.PAYCREDIT);
            if (uc != null)
                App.UIStateManager.Push(uc);
        }

        private void CashBtn_Click(object sender, RoutedEventArgs e)
        {
            App.OrderManager.CurrentOrder.IsCard = false;
            UserControl uc = App.UIStateManager.Get(UICategory.PAYCASH);
            if (uc != null)
                App.UIStateManager.Push(uc);
        }
    }
}                                            
