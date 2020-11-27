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
        private int _user_IDX;
        public int User_IDX
        {
            get => _user_IDX;
            set
            {
                _user_IDX = value;
                OnPropertyChanged(nameof(User_IDX));
            }
        }
        private string _user_Name;
        public string User_Name
        {
            get => _user_Name;
            set
            {
                _user_Name = value;
                OnPropertyChanged(nameof(User_Name));
            }
        }
        public int Seat_IDX { get; set; }
        public bool IsCard { get; set; }
        public DateTime OrderedTime { get; set; } = default(DateTime);
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
