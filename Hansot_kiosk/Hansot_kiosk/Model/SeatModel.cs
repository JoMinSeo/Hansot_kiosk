using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Hansot_kiosk.Model
{
    public class SeatModel : INotifyPropertyChanged
    {
        private int _idx;
        public int IDX
        {
            get => _idx;
            set
            {
                _idx = value;
                OnPropertyChanged(nameof(_idx));
            }
        }
        private string _remainingTime;
        public string RemainingTime
        {
            get => _remainingTime;
            set
            {
                _remainingTime = value;
                OnPropertyChanged(nameof(RemainingTime));
            }
        }
        private bool _isEnable;
        public bool IsEnable
        {
            get => _isEnable;
            set
            {
                _isEnable = value;
                OnPropertyChanged(nameof(IsEnable));
            }
        }
        private DateTime criteria = default(DateTime);
        public SeatModel(int num)
        {
            this.IDX = num;
            this.IsEnable = true;

            criteria = App.sQLManager.selectLastOrderDate(num);

            if (0 > DateTime.Now.AddMinutes(-1.0).CompareTo(criteria)) //criteria가 1분 이내임
            {
                this.IsEnable = false;
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
