using System;
using System.ComponentModel;
using System.Windows.Media;
using System.Windows.Threading;

namespace Hansot_kiosk.Model
{
    public class SeatModel : INotifyPropertyChanged
    {
        #region Property
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
        private bool _isEnableClick;
        public bool IsEnableClick
        {
            get => _isEnableClick;
            set
            {
                _isEnableClick = value;

                if (value)
                {
                    this.BackGroundColor = new SolidColorBrush(Colors.AliceBlue);
                }
                else if (!value)
                {
                    this.BackGroundColor = new SolidColorBrush(Colors.OrangeRed);
                }

                OnPropertyChanged(nameof(_isEnableClick));
            }
        }
        private Brush _backGroundColor;
        public Brush BackGroundColor
        {
            get => _backGroundColor;
            set
            {
                _backGroundColor = value;
                OnPropertyChanged(nameof(BackGroundColor));
            }
        }
        #endregion
        private DateTime criteriaDate = default(DateTime);
        public string SCriteria
        {
            get => criteriaDate.ToString("최근 주문 : MM월 dd일 tt hh:mm");
        }
        private DispatcherTimer timer = new DispatcherTimer();
        public SeatModel(int num, DateTime criteriaDateTime)
        {
            this.IDX = num;
            this.IsEnableClick = true;

            criteriaDate = criteriaDateTime;

            if (0 > DateTime.Now.AddMinutes(-1.0).CompareTo(criteriaDate)) //criteria가 1분 이내임
            {
                this.IsEnableClick = false;

                StartTimer();
            }
        }
        #region DispatcherTimer
        private void StartTimer()
        {
            timer.Interval = TimeSpan.FromSeconds(1);
            timer.Tick += new EventHandler(Timer_Tick);
            timer.Start();
        }

        private void Timer_Tick(object sender, EventArgs e)
        {
            int remainingSec = Convert.ToInt32(Math.Truncate((DateTime.Now - criteriaDate).TotalSeconds));

            if (remainingSec < 60)
            {
                remainingSec = 60 - remainingSec;
                if(remainingSec < 10)
                {
                    RemainingTime = "00 : 0" + remainingSec;
                }
                else
                {
                    RemainingTime = "00 : " + remainingSec;
                }
            }
            else
            {
                this.IsEnableClick = true;
                this.RemainingTime = "";
                timer.Tick -= new EventHandler(Timer_Tick);
            }
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
