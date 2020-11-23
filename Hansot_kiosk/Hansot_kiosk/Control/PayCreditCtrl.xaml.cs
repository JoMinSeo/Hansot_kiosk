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
            this.DataContext = App.orderManager.CurrentOrder;
        }

        FilterInfoCollection filterInfoCollection;

        private void PreviousBtn_Click(object sender, RoutedEventArgs e)
        {
            App.uIStateManager.Pop();
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
            txtQrcode.Text = e;
        }

        private void txtQrcode_TextChanged(object sender, TextChangedEventArgs e)
        {
            String barcode = txtQrcode.Text;
            UserModel currentUser = App.userManager.compareName(barcode);

            if (currentUser != null)
            {
                App.orderManager.CurrentOrder.User_IDX = currentUser.IDX;
                comfirmBtn.IsEnabled = true;
                return;
            }
        }

        private void comfirmBtn_Click(object sender, RoutedEventArgs e)
        {
            UserControl uc = App.uIStateManager.Get(UICategory.COMPLETE);
            if (uc != null)
                App.uIStateManager.Push(uc);
        }
    }
}
