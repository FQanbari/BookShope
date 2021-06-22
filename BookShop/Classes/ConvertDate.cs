using MD.PersianDateTime;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BookShop.Classes
{
    public class ConvertDate
    {
        public DateTime ShamsiToMiladi(string date)
        {
            PersianDateTime persianDateTime = PersianDateTime.Parse(date);
            return persianDateTime.ToDateTime();
        }

        public string MiladiToShamsi(DateTime date)
        {
            PersianDateTime persianDateTime = new PersianDateTime(date);
            return persianDateTime.ToString("yyyy/MM/dd");
        }

        public string MiladiToShamsi(DateTime date,string format)
        {
            PersianDateTime persianDateTime = new PersianDateTime(date);
            return persianDateTime.ToString(format);
        }
    }
}
