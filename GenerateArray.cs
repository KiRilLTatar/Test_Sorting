using System;
using System.Linq;

namespace Test_Sorting
{
    public static class GenerateArray
    {
        private static readonly Random Rnd = new Random();

        public static int[] GenerateIntArray(int size)
        {
            int[] array = new int[size];
            for (int i = 0; i < size; i++)
            {
                array[i] = Rnd.Next(-size, size);
            }
            return array;
        }

        public static double[] GenerateDoubleArray(int size)
        {
            double[] array = new double[size];
            for (int i = 0; i < size; i++)
            {
                array[i] = Rnd.NextDouble() * size * 2 - size; 
            }
            return array;
        }

        public static string[] GenerateStringArray(int size)
        {
            string[] array = new string[size];
            for (int i = 0; i < size; i++)
            {
                array[i] = GenerateRandomString(Rnd.Next(5, 15));
            }
            return array;
        }

        private static string GenerateRandomString(int length)
        {
            const string chars = "ABCDEFGHIJKLMNOPQRSTUVWXYZabcdefghijklmnopqrstuvwxyz0123456789";
            return new string(Enumerable.Repeat(chars, length)
              .Select(s => s[Rnd.Next(s.Length)]).ToArray());
        }

        public static CustomDate[] GenerateCustomDateArray(int size)
        {
            CustomDate[] array = new CustomDate[size];
            DateTime startDate = new DateTime(1900, 1, 1);
            int totalDays = (new DateTime(2100, 12, 31) - startDate).Days; 

            for (int i = 0; i < size; i++)
            {
                DateTime randomDateTime = startDate.AddDays(Rnd.Next(totalDays));
                array[i] = new CustomDate(randomDateTime.Year, randomDateTime.Month, randomDateTime.Day);
            }
            return array;
        }

        public static int[] GenerateSortedIntArray(int size)
        {
            int[] array = new int[size];
            for (int i = 0; i < size; i++)
            {
                array[i] = i - size / 2; 
            }
            return array;
        }

        public static int[] GenerateReverseSortedIntArray(int size)
        {
            int[] array = new int[size];
            for (int i = 0; i < size; i++)
            {
                array[i] = size - 1 - i - size / 2;
            }
            return array;
        }

        public static int[] GenerateArrayWithManyDuplicates(int size, int distinctValues)
        {
            int[] array = new int[size];
            for (int i = 0; i < size; i++)
            {
                array[i] = Rnd.Next(0, distinctValues) - distinctValues / 2; 
            }
            return array;
        }

        public static int[] GeneratePartiallyOrderedIntArray(int size, int swaps)
        {
            int[] array = GenerateSortedIntArray(size); 
            for (int i = 0; i < swaps; i++)
            {
                int idx1 = Rnd.Next(0, size);
                int idx2 = Rnd.Next(0, size);
                int temp = array[idx1];
                array[idx1] = array[idx2];
                array[idx2] = temp;
            }
            return array;
        }

        public static DateTime[] GenerateDateTimeArray(int size)
        {
            DateTime[] array = new DateTime[size];
            DateTime start = new DateTime(1900, 1, 1);
            int rangeInDays = (new DateTime(2100, 1, 1) - start).Days;

            for (int i = 0; i < size; i++)
            {
                array[i] = start.AddDays(Rnd.Next(rangeInDays))
                               .AddHours(Rnd.Next(24))
                               .AddMinutes(Rnd.Next(60))
                               .AddSeconds(Rnd.Next(60))
                               .AddMilliseconds(Rnd.Next(1000));
            }
            return array;
        }
    }
}