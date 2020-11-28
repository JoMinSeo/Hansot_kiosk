using Hansot_kiosk.Common;
using Hansot_kiosk.Model;
using System;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Linq;
using System.Windows;
using System.Windows.Controls;

namespace Hansot_kiosk.Control
{
    /// <summary>
    /// SeatSelectCtrl.xaml에 대한 상호 작용 논리
    /// </summary>
    public partial class SeatSelectCtrl : UserControl, INotifyPropertyChanged
    {
        #region Property
        private ObservableCollection<SeatModel> _seats = new ObservableCollection<SeatModel>();
        public ObservableCollection<SeatModel> Seats
        {
            get => _seats;
            set
            {
                _seats = value;
                OnPropertyChanged(nameof(Seats));
            }
        }
        #endregion
        public SeatSelectCtrl()
        {
            InitializeComponent();
            App.InitDeleGate += init;
        }
        private void init()
        {
            if (Seats.Count > 0)
            {
                Seats.Clear();
            }
            for (int i = 1; i <= 10; i++)
            {
                DateTime criteriaDate = (from orderModel in App.Orders where orderModel.Seat_IDX == i 
                                         select orderModel).Max(orderModel => orderModel.OrderedTime);
                Seats.Add(new SeatModel(i, criteriaDate));
            }
            this.DataContext = this;
            lbSeats.ItemsSource = Seats;

            this.NextBtn.IsEnabled = false;
        }

        private void lbSeats_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if (lbSeats.SelectedItems.Count < 1)
            {
                return;
            }
            SeatModel model = (SeatModel)lbSeats.SelectedItem;
            if (model.IsEnableClick)
            {
                App.OrderManager.CurrentOrder.Seat_IDX = model.IDX;

                this.NextBtn.IsEnabled = true;
            }
        }

        #region UIControl
        private void PreviusBtn_Click(object sender, RoutedEventArgs e)
        {
            App.UIStateManager.Pop();
        }

        private void NextBtn_Click(object sender, RoutedEventArgs e)
        {
            UserControl uc = App.UIStateManager.Get(UICategory.PAYSELECT);
            if (uc != null)
                App.UIStateManager.Push(uc);
        }
        #endregion
        #region PropertyChangedEvent
        public event PropertyChangedEventHandler PropertyChanged;
        protected void OnPropertyChanged(string propetryName)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propetryName));
        }
        #endregion
    }
}
