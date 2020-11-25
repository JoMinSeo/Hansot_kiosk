using System;
using System.Windows;
using System.Windows.Controls;
using AForge.Video.DirectShow;
using Hansot_kiosk.Model;
using Kiosk.UIManager;

namespace Hansot_kiosk.Control
{
    /// <summary>
    /// PayCreditCtrl.xaml에 대한 상호 작용 논리
    /// </summary>
    public partial class PayCreditCtrl : System.Windows.Controls.UserControl
    {
        public PayCreditCtrl()
        {
            InitializeComponent();
            webcam.CameraIndex = 0;

            ((MainWindow)System.Windows.Application.Current.MainWindow).DeleGate += init;
        }

        private void init()
        {
            this.DataContext = App.OrderManager.CurrentOrder;

            tbxQrcode.Text = "";
        }

        FilterInfoCollection filterInfoCollection;

        private void PreviousBtn_Click(object sender, RoutedEventArgs e)
        {
            App.UIStateManager.Pop();
        }

        private void PayCreditCtrl_Loaded(object sender, RoutedEventArgs e)
        {
            filterInfoCollection = new FilterInfoCollection(FilterCategory.VideoInputDevice);
            foreach (FilterInfo filterInfo in filterInfoCollection)
            {
                cboDevice.Items.Add(filterInfo.Name);
            }
            cboDevice.SelectedIndex = 0;
        }

        private void webcam_QrDecoded(object sender, string e)
        {
            tbxQrcode.Text = e;
        }

        private void txtQrcode_TextChanged(object sender, TextChangedEventArgs e)
        {
            string barcode = tbxQrcode.Text;
            UserModel currentUser = App.UserManager.compareName(barcode);

            if (currentUser != null)
            {
                App.OrderManager.CurrentOrder.User_IDX = currentUser.IDX;
                App.OrderManager.CurrentOrder.User_Name = currentUser.Name;

                comfirmBtn.IsEnabled = true;
                return;
            }
        }

        private void comfirmBtn_Click(object sender, RoutedEventArgs e)
        {
            UserControl uc = App.UIStateManager.Get(UICategory.COMPLETE);
            if (uc != null)
                App.UIStateManager.Push(uc);
        }
    }
}
