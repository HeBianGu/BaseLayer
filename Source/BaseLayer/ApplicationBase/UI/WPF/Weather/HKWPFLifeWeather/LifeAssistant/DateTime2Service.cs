using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Timers;

namespace LifeCalendar
{
    public sealed class DateTime2Service : INotifyPropertyChanged
    {
        private string _time;

        public string Time
        {
            get { return _time; }
            set
            {
                _time = value;
                OnPropertyChanged("Time");
            }
        }

        private string _date;

        public string Date
        {
            get { return _date; }
            set
            {
                _date = value;
                OnPropertyChanged("Date");
            }
        }

        private string _lunar;

        public string Lunar
        {
            get { return _lunar; }
            set
            {
                _lunar = value;
                OnPropertyChanged("Lunar");
            }
        }
        public event PropertyChangedEventHandler PropertyChanged;

        public DateTime2Service()
        { //2014年4月25日 13:34:34
            Date = DateTime.Now.ToString("yyyy年M月d日 dddd");
            ChineseCalendar chinese = new ChineseCalendar(DateTime.Now);
            Lunar = "【" + chinese.GanZhiYearString.Substring(0, chinese.GanZhiYearString.Length - 1) +" "+ chinese.AnimalString + "年】 " + chinese.ChineseMonthString+chinese.ChineseDayString;
            initialDateTime();
            _tiemr = new Timer(1000);
            _tiemr.Elapsed += _tiemr_Elapsed;
            _tiemr.Start();
        }
        ~DateTime2Service()
        {
            _tiemr.Stop();
            _tiemr.Dispose();
        }
        void _tiemr_Elapsed(object sender, ElapsedEventArgs e)
        {
            initialDateTime();
        }
        void initialDateTime()
        {
            Time = DateTime.Now.ToString("HH:mm");
        }


        private void OnPropertyChanged(string info)
        {
            if (this.PropertyChanged != null)
                PropertyChanged(this, new PropertyChangedEventArgs(info));
        }


        private Timer _tiemr;


    }
}
