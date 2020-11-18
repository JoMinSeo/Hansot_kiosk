﻿using System;
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
        private DateTime criteria = default(DateTime);
        private DispatcherTimer timer = new DispatcherTimer();
        public SeatModel(int num)
        {
            this.IDX = num;
            this._isEnableClick = true;
            this.BackGroundColor = new SolidColorBrush(Colors.AliceBlue);

            criteria = App.sQLManager.selectLastOrderDate(num);

            StartTimer();

            if (0 > DateTime.Now.AddMinutes(-1.0).CompareTo(criteria)) //criteria가 1분 이내임
            {
                this._isEnableClick = false;
                BackGroundColor = new SolidColorBrush(Colors.OrangeRed);

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
            int remainingSec = Convert.ToInt32(Math.Truncate((DateTime.Now - criteria).TotalSeconds));

            if (remainingSec < 60)
            {
                RemainingTime = "00 : " + remainingSec;
            }
            else
            {
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
