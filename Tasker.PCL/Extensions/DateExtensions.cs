using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Tasker.PCL.Enumerations;

namespace Tasker.PCL.Extensions
{
    public static class DateExtensions
    {
        public static TimeFrame GetTimeFrame(this DateTime date)
        {
            date = date.TrimTime();

            var today = DateTime.Today;
            var tomorrow = today.AddDays(1);

            if(date.Equals(today))
                return TimeFrame.Today;

            if(date.Equals(tomorrow))
                return TimeFrame.Tomorrow;

            if (date.CompareTo(tomorrow) > 0)
                return TimeFrame.Later;

            return TimeFrame.Past;
        }

        public static string GetTimeFrameString(this DateTime date)
        {
            switch (date.GetTimeFrame())
            {
                case TimeFrame.Unknown:
                    return "unknown";
                    break;
                case TimeFrame.Today:
                    return "today";
                    break;
                case TimeFrame.Tomorrow:
                    return "tomorrow";
                    break;
                case TimeFrame.Later:
                    return "later";
                    break;
                case TimeFrame.Past:
                    return "past";
                    break;
            }
            return "unknown";
        }

        public static long ToUnixTimeMilliseconds(this DateTime date)
        {
            var epoch = new DateTime(1970, 1, 1, 0, 0, 0, DateTimeKind.Local);
            var utcDate = date;
            var milliseconds = utcDate.Subtract(epoch).TotalMilliseconds;
            return Convert.ToInt64(milliseconds);
        }

        public static DateTime FromUnixTimeMilliseconds(long unixTime)
        {
            var epoch = new DateTime(1970, 1, 1, 0, 0, 0, DateTimeKind.Local);
            return epoch.AddMilliseconds(unixTime);
        }

        private static DateTime TrimTime(this DateTime dateTime)
        {
            return new DateTime(dateTime.Year,dateTime.Month,dateTime.Day);
        }
    }
}
