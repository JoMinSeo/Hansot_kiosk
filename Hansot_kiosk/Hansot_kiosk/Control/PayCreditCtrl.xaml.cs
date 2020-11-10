using System;
using System.Collections.Generic;
using System.Drawing;
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
using AForge.Video;
using AForge.Video.DirectShow;
using System.Windows.Threading;
using ZXing;
using System.IO;
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
