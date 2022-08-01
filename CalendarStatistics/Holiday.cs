using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CalendarStatistics
{
    class Holiday
    {
        // Holidays are a massive pain but useful to calculate
        public static DateTime[] InitializeHolidays(DateTime startDay, DateTime endDay)
        {
            List<DateTime> tempHolidayList = new List<DateTime>();
            // Figure out all holidays and add an extra year just in case
            for (int i = 0; i < (endDay.Year - startDay.Year + 1); i++)
            {
                tempHolidayList.Add(NewYearsDay(i + startDay.Year));
                tempHolidayList.Add(MLKDay(i + startDay.Year));
                tempHolidayList.Add(PresidentsDay(i + startDay.Year));
                tempHolidayList.Add(MemorialDay(i + startDay.Year));
                tempHolidayList.Add(Juneteenth(i + startDay.Year));
                tempHolidayList.Add(IndependenceDay(i + startDay.Year));
                tempHolidayList.Add(LaborDay(i + startDay.Year));
                tempHolidayList.Add(ColumbusDay(i + startDay.Year));
                tempHolidayList.Add(VeteransDay(i + startDay.Year));
                tempHolidayList.Add(ThanksgivingDay(i + startDay.Year));
                tempHolidayList.Add(ChristmasDay(i + startDay.Year));
            }

            // Clean list for only future events
            List<DateTime> holidayList = new List<DateTime>();
            for(int i = 0; i < tempHolidayList.Count; i++)
            {
                if (tempHolidayList[i] >= startDay)
                {
                    holidayList.Add(tempHolidayList[i]);
                }
            }

            // Return clean list
            return holidayList.ToArray();
        }

        // 25th of December give or take
        private static DateTime ChristmasDay(int year)
        {
            DateTime initDate = new DateTime(year, 12, 25);
            return GetActualHoliday(initDate);
        }

        // 4th Thursday in Novemember
        private static DateTime ThanksgivingDay(int year)
        {
            DateTime initDate = new DateTime(year, 11, 1);
            while (initDate.DayOfWeek != DayOfWeek.Thursday)
            {
                initDate = initDate.AddDays(1);
            }
            return initDate.AddDays(21);
        }

        // November 11th give or take
        private static DateTime VeteransDay(int year)
        {
            DateTime initDate = new DateTime(year, 11, 11);
            return GetActualHoliday(initDate);
        }

        // 2nd monday in october
        private static DateTime ColumbusDay(int year)
        {
            DateTime initDate = new DateTime(year, 10, 1);
            while (initDate.DayOfWeek != DayOfWeek.Monday)
            {
                initDate = initDate.AddDays(1);
            }
            return initDate.AddDays(7);
        }

        // 1st Monday in September
        private static DateTime LaborDay(int year)
        {
            DateTime initDate = new DateTime(year, 9, 1);
            while (initDate.DayOfWeek != DayOfWeek.Monday)
            {
                initDate = initDate.AddDays(1);
            }
            return initDate;
        }

        // July 4th give or take
        private static DateTime IndependenceDay(int year)
        {
            DateTime initDate = new DateTime(year, 7, 4);
            return GetActualHoliday(initDate);
        }

        // June 19th give or take
        private static DateTime Juneteenth(int year)
        {
            DateTime initDate = new DateTime(year, 6, 19);
            return GetActualHoliday(initDate);
        }

        // The last monday of May
        private static DateTime MemorialDay(int year)
        {
            // Easier to start on May 30 and go backwards till you find a Monday
            DateTime initDate = new DateTime(year, 5, 30);
            while(initDate.DayOfWeek != DayOfWeek.Monday)
            {
                initDate = initDate.AddDays(-1);
            }
            return initDate;
        }

        // 3rd Monday of February
        private static DateTime PresidentsDay(int year)
        {
            DateTime initDate = new DateTime(year, 2, 1);
            // Find first Monday of month
            while (initDate.DayOfWeek != DayOfWeek.Monday)
            {
                initDate = initDate.AddDays(1);
            }
            // After we are on a Monday add 14 days to get to the third Monday
            return initDate.AddDays(14);
        }

        // 3rd Monday of January
        private static DateTime MLKDay(int year)
        {
            DateTime initDate = new DateTime(year, 1, 1);
            // Find first Monday of month
            while (initDate.DayOfWeek != DayOfWeek.Monday)
            {
                initDate = initDate.AddDays(1);
            }
            // After we are on a Monday add 14 days to get to the third Monday
            return initDate.AddDays(14);
        }

        // 1st of January give or take rounding
        private static DateTime NewYearsDay(int year)
        {
            DateTime initDate = new DateTime(year, 1, 1);
            return GetActualHoliday(initDate);
        }

        // Federal holiday rounding for holidays that are day-specific rather than using arcane rules
        public static DateTime GetActualHoliday(DateTime day)
        {
            if (day.DayOfWeek == DayOfWeek.Saturday)
            {
                return day.AddDays(-1);
            }
            if (day.DayOfWeek == DayOfWeek.Sunday)
            {
                return day.AddDays(1);
            }
            return day;
        }
    }
}
