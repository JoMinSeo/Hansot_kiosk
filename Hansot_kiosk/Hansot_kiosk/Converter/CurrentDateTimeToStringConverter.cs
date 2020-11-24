using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Data;

namespace Hansot_kiosk.Converter
{
    class CurrentDateTimeToStringConverter : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            DateTime flag = (value is DateTime) ? (DateTime)value : default(DateTime);
            string sFlag = String.Format("{0:yyyy년 MM월 dd일 tt hh시 mm분 ss초}", flag);
            return sFlag;
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            throw new NotImplementedException();
        }
    }
}
