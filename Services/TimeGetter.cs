using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PR6_Lashkov.Services
{
    internal static class TimeGetter
    {
        public static string GetTimeInterval()
        {
            TimeSpan morningStart = new TimeSpan(10, 0, 0); // 10:00
            TimeSpan morningEnd = new TimeSpan(12, 0, 0);   // 12:00
            TimeSpan dayStart = new TimeSpan(12, 1, 0);     // 12:01
            TimeSpan dayEnd = new TimeSpan(17, 0, 0);       // 17:00
            TimeSpan eveningStart = new TimeSpan(17, 1, 0); // 17:01
            TimeSpan eveningEnd = new TimeSpan(19, 0, 0);   // 19:00

            //TimeSpan current = DateTime.Now.TimeOfDay;
            TimeSpan current = new TimeSpan(12, 0, 0);

            if (current >= morningStart && current <= morningEnd)
            {
                return "утро";
            }
            else if (current >= dayStart && current <= dayEnd)
            {
                return "день";
            }
            else if (current >= eveningStart && current <= eveningEnd)
            {
                return "вечер";
            }
            else
            {
                return "ночь";
            }
        }

    }
}
