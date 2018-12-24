using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Assets.Scripts.SupportClasses
{
    [Serializable]
    public class ProdTimer
    {
        int days;
        int hours;
        int minutes;
        int seconds;

        public ProdTimer(int _days, int _hours, int _minutes, int _seconds)
        {
            days = _days;
            hours = _hours;
            minutes = _minutes;
            seconds = _seconds;
        }

        public int Days { get { return days; } set { days = value; } }
        public int Hours { get { return hours; } set { hours = value; } }
        public int Minutes { get { return minutes; } set { minutes = value; } }
        public int Seconds { get { return seconds; } set { seconds = value; } }

    }
}
