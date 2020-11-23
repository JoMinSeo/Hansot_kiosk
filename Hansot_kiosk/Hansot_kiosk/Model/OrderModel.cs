using System;
using System.ComponentModel;

namespace Hansot_kiosk.Model
{
    public class OrderModel : INotifyPropertyChanged
    {
        public void init()
        {
            TotalPrice = 0;
        }
        private int _idx;
        public int IDX
        {
            get => _idx;
            set
            {
                _idx = value;
                OnPropertyChanged(nameof(IDX));
            }
        }
        public int User_IDX { get; set; }
        public int Seat_IDX { get; set; }
        public bool IsCard { get; set; }
        public DateTime OrderedTime { get; set; }
        private int _totalPrice;
        public int TotalPrice
        {
            get => _totalPrice;
            set
            {
                _totalPrice = value;
                OnPropertyChanged(nameof(TotalPrice));
            }
        }
        #region PropertyChangedEvent
        public event PropertyChangedEventHandler PropertyChanged;
        protected void OnPropertyChanged(string propetryName)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propetryName));
        }
        #endregion

    }
}
