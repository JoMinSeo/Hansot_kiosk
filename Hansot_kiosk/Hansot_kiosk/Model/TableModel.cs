using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Hansot_kiosk.Model
{
    public class TableModel : INotifyPropertyChanged
    {
        private string _name;
        public string Name
        {
            get => _name;
            set
            {
                _name = value;
                OnPropertyChanged(nameof(Name));
            }
        }
        private string _remainingSec;
        public string RemainingSec
        {
            get => _remainingSec;
            set
            {
                _remainingSec = value;
                OnPropertyChanged(nameof(RemainingSec));
            }
        }
        private string _remainingMin;
        public string RemainingMin
        {
            get => _remainingMin;
            set
            {
                _remainingMin = value;
                OnPropertyChanged(nameof(RemainingMin));
            }
        }
        public TableModel(int num)
        {
            this.Name = num + "번 테이블";
        }

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
    }
}
