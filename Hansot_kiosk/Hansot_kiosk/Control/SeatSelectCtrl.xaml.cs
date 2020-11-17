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
        private ObservableCollection<TableModel> _tables = new ObservableCollection<TableModel>();
        public ObservableCollection<TableModel> Tables
        {
            get => _tables;
            set
            {
                _tables = value;
                OnPropertyChanged(nameof(Tables));
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
                TableModel temp = new TableModel(i);
                Tables.Add(temp);
            }
            this.DataContext = this;
            lbTables.ItemsSource = Tables;
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
            if (PropertyChanged != null)
            {
                PropertyChanged(this, new PropertyChangedEventArgs(propetryName));
            }
        }
        #endregion

        private void lbTables_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {

        }
    }
}
