using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Test_Sorting
{
    public class CustomDate : IComparable<CustomDate>
    {
        public int Year;
        public int Month;
        public int Day;

        public CustomDate(int year, int month, int day)
        {
            Year = year;
            Month = month;
            Day = day;
        }

        public int CompareTo(CustomDate other)
        {
            int yearComparison = Year.CompareTo(other.Year);

            if (yearComparison != 0) return yearComparison;

            int monthComparison = Month.CompareTo(other.Month);
            if (monthComparison != 0) return monthComparison;

            return Day.CompareTo(other.Day);
        }

        public override string ToString()
        {
            return $"{Day:D2}.{Month:D2}.{Year}";
        }
        
    }
}
