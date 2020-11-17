using Hansot_kiosk.Model;
using Kiosk.UIManager;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Windows;
using System.Windows.Controls;

namespace Hansot_kiosk.Control
{
    /// <summary>
    /// SeatSelectCtrl.xaml에 대한 상호 작용 논리
    /// </summary>
    public partial class SeatSelectCtrl : UserControl, INotifyPropertyChanged
    {
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
        public SeatSelectCtrl()
        {
            InitializeComponent();
            init();
        }
        private void init()
        {
            for (int i = 1; i <= 10; i++)
            {
                Seats.Add(new SeatModel(i));
            }
            this.DataContext = this;
            lbSeats.ItemsSource = Seats;
        }

        private void lbSeats_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if (lbSeats.SelectedItems.Count < 1)
            {
                return;
            }
            SeatModel model = (SeatModel)lbSeats.SelectedItem;
            App.orderManager.CurrentOrder.Seat = model.IDX;
            App.uIStateManager.Push(App.uIStateManager.Get(UICategory.PAYSELECT));
        }

        #region UIControl
        private void PreviusBtn_Click(object sender, RoutedEventArgs e)
        {
            App.uIStateManager.Pop();
        }

        private void NextBtn_Click(object sender, RoutedEventArgs e)
        {
            UserControl uc = App.uIStateManager.Get(UICategory.PAYSELECT);
            if (uc != null)
                App.uIStateManager.Push(uc);
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
