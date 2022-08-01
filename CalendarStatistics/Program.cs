using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CalendarStatistics
{
    class Program
    {
        static void Main(string[] args)
        {
            DateTime endDate = new DateTime(2030, 01, 01);
            DateTime today = DateTime.Today;

            // Collect day of week data (big chart)
            // CollectDayOfWeekData(today, endDate);

            // Collect business day data (small chart)
            DateTime[] holidayArray = Holiday.InitializeHolidays(today, endDate);
            CollectDayData(today, endDate, holidayArray);
        }

        private static void CollectDayData(DateTime currentDate, DateTime endDate, DateTime[] holidayArray)
        {
            int[] netBusinessDays = new int[31];
            int nextHolidayIndex = 0;
            DateTime nextHoliday = holidayArray[nextHolidayIndex];
            int currentDay;
            DayOfWeek currentDayOfWeek;
            while (currentDate < endDate)
            {
                currentDay = currentDate.Day;
                currentDayOfWeek = currentDate.DayOfWeek;

                // If it is a non-business day, subtract a day (holiday or weekend)
                if (currentDate.Equals(nextHoliday))
                {
                    // Subtract business day for holiday
                    netBusinessDays[currentDate.Day - 1] -= 1;
                    // Check upcoming holiday (unless we are at the end of the list, then ignore)
                    nextHolidayIndex += 1;
                    try
                    {
                        nextHoliday = holidayArray[nextHolidayIndex];
                    }
                    catch (IndexOutOfRangeException)
                    {
                    }
                }
                else if (currentDayOfWeek.Equals(DayOfWeek.Saturday) || currentDayOfWeek.Equals(DayOfWeek.Sunday))
                {
                    // Subtract business day for weekend
                    netBusinessDays[currentDate.Day - 1] -= 1;
                }
                else
                {
                    // Must be a regular weekday then
                    netBusinessDays[currentDate.Day - 1] += 1;
                }
                currentDate = currentDate.AddDays(1);
            }
            using (System.IO.StreamWriter sw = new System.IO.StreamWriter(@"C:\Temp\DayOutput.csv", true))
            {
                sw.WriteLine("Day of Month, Net Business Days");
                for (int i = 0; i < 31; i++)
                {
                    sw.WriteLine("{0},{1}", i + 1, netBusinessDays[i]);
                }
            }
        }

        static void CollectDayOfWeekData(DateTime currentDate, DateTime endDate)
        {
            int[] mondayArray = new int[31];
            int[] tuesdayArray = new int[31];
            int[] wednesdayArray = new int[31];
            int[] thursdayArray = new int[31];
            int[] fridayArray = new int[31];
            int[] saturdayArray = new int[31];
            int[] sundayArray = new int[31];
            // Collect date data
            while (currentDate < endDate)
            {
                switch (currentDate.DayOfWeek)
                {
                    case DayOfWeek.Monday:
                        mondayArray[currentDate.Day - 1] += 1;
                        break;
                    case DayOfWeek.Tuesday:
                        tuesdayArray[currentDate.Day - 1] += 1;
                        break;
                    case DayOfWeek.Wednesday:
                        wednesdayArray[currentDate.Day - 1] += 1;
                        break;
                    case DayOfWeek.Thursday:
                        thursdayArray[currentDate.Day - 1] += 1;
                        break;
                    case DayOfWeek.Friday:
                        fridayArray[currentDate.Day - 1] += 1;
                        break;
                    case DayOfWeek.Saturday:
                        saturdayArray[currentDate.Day - 1] += 1;
                        break;
                    case DayOfWeek.Sunday:
                        sundayArray[currentDate.Day - 1] += 1;
                        break;
                }
                currentDate = currentDate.AddDays(1);
            }
            // Compare days of week to find max of each day
            string[] resultArray = new string[31];
            int localMax = 0;
            string weekday = "";
            for (int i = 0; i < 31; i++)
            {
                weekday = "monday";
                localMax = mondayArray[i];
                if (tuesdayArray[i] >= localMax)
                {
                    // Handle ties by adding the weekday to the string
                    if (tuesdayArray[i] == localMax)
                    {
                        weekday += ", tuesday";
                    }
                    else
                    {
                        // If bigger, replace it
                        weekday = "tuesday";
                        localMax = tuesdayArray[i];
                    }
                }
                if (wednesdayArray[i] >= localMax)
                {
                    if (wednesdayArray[i] == localMax)
                    {
                        weekday += ", wednesday";
                    }
                    else
                    {
                        weekday = "wednesday";
                        localMax = wednesdayArray[i];
                    }
                }
                if (thursdayArray[i] >= localMax)
                {
                    if (thursdayArray[i] == localMax)
                    {
                        weekday += ", thursday";
                    }
                    else
                    {
                        weekday = "thursday";
                    }
                    localMax = thursdayArray[i];
                }
                if (fridayArray[i] >= localMax)
                {
                    if (fridayArray[i] == localMax)
                    {
                        weekday += ", friday";
                    }
                    else
                    {
                        weekday = "friday";
                        localMax = fridayArray[i];
                    }
                }
                if (saturdayArray[i] >= localMax)
                {
                    if (saturdayArray[i] == localMax)
                    {
                        weekday += ", saturday";
                    }
                    else
                    {
                        weekday = "saturday";
                    }
                    localMax = saturdayArray[i];
                }
                if (sundayArray[i] >= localMax)
                {
                    if (sundayArray[i] == localMax)
                    {
                        weekday += ", sunday";
                    }
                    else
                    {
                        weekday = "sunday";
                    }
                    localMax = sundayArray[i];
                }
                resultArray[i] = weekday;
            }

            // Output results
            using (System.IO.StreamWriter sw = new System.IO.StreamWriter(@"C:\Temp\DayOfWeekOutput.csv", true))
            {
                sw.WriteLine("Day of Month, Monday, Tuesday, Wednesday, Thursday, Friday, Saturday, Sunday");
                for (int i = 0; i < 31; i++)
                {
                    sw.WriteLine("{0}, {1}, {2}, {3}, {4}, {5}, {6}, {7}", i + 1,
                        mondayArray[i], tuesdayArray[i], wednesdayArray[i], thursdayArray[i], fridayArray[i], saturdayArray[i], sundayArray[i]);
                }
            }

            // Print results
            Console.WriteLine("Most common day of week for each day of month");
            for (int i = 0; i < 31; i++)
            {
                Console.WriteLine("{0} : {1}", i + 1, resultArray[i]);
            }
            Console.ReadLine();

        }
    }
}
