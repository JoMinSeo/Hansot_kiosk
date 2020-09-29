﻿using System;
using System.Collections.Generic;
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
    /// Interaction logic for OrderCtrl.xaml
    /// </summary>
    public partial class OrderCtrl : UserControl
    {
        public OrderCtrl()
        {
            InitializeComponent();

            CategoryBtnAdd("버튼 1");
            CategoryBtnAdd("버튼 2");
        }
        private void CategoryBtnAdd(string content)
        {
            this.CategoryBtnStpnl.Children.Add(new Button { Content = content, FontSize = 70, Margin = new Thickness(10) { Left = 5, Right = 5 } }) ;
        }
    }
}
