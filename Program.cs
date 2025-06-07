using System.Diagnostics;
using System.IO;
using System.Text;

namespace Test_Sorting
{
    public class PerformanceTester
    {
        private static List<SortAlgorithm> Algorithms;
        private static readonly int[] ArraySizes = { 50, 5000, 25000, 50000, 250000, 500000 };
        private const int RecursiveMaxSafeSize = 100000;

        public static void Main(string[] args)
        {
            // RunTests();
        }

        private class SortAlgorithm
        {
            public string Name { get; set; } = string.Empty;
            public Action<int[]>? IntSortAction { get; set; }
            public Action<double[]>? DoubleSortAction { get; set; }
            public Action<string[]>? StringSortAction { get; set; }
            public Action<CustomDate[]>? CustomDataSortAction { get; set; }
            public Action<DateTime[]>? DateTimeSortAction { get; set; } 
            public bool IsIntOnly { get; set; } = false;
            public bool IsRecursive { get; set; } = false;
            public bool NeedsAuxiliaryArray { get; set; } = false;
        }

        static PerformanceTester()
        {
            Algorithms = new List<SortAlgorithm>
            {
                new SortAlgorithm { Name = "BubbleSort", IntSortAction = SortingAlgorithms.BubbleSort, DoubleSortAction = SortingAlgorithms.BubbleSort, StringSortAction = SortingAlgorithms.BubbleSort, CustomDataSortAction = SortingAlgorithms.BubbleSort, DateTimeSortAction = SortingAlgorithms.BubbleSort },
                new SortAlgorithm { Name = "InsertionSort", IntSortAction = SortingAlgorithms.InsertionSort, DoubleSortAction = SortingAlgorithms.InsertionSort, StringSortAction = SortingAlgorithms.InsertionSort, CustomDataSortAction = SortingAlgorithms.InsertionSort, DateTimeSortAction = SortingAlgorithms.InsertionSort },
                new SortAlgorithm { Name = "SelectionSort", IntSortAction = SortingAlgorithms.SelectionSort, DoubleSortAction = SortingAlgorithms.SelectionSort, StringSortAction = SortingAlgorithms.SelectionSort, CustomDataSortAction = SortingAlgorithms.SelectionSort, DateTimeSortAction = SortingAlgorithms.SelectionSort },
                new SortAlgorithm { Name = "ShellSort", IntSortAction = SortingAlgorithms.ShellSort, DoubleSortAction = SortingAlgorithms.ShellSort, StringSortAction = SortingAlgorithms.ShellSort, CustomDataSortAction = SortingAlgorithms.ShellSort, DateTimeSortAction = SortingAlgorithms.ShellSort },
                new SortAlgorithm { Name = "QuickSort (Rec)", IsRecursive = true, IntSortAction = SortingAlgorithms.QuickSortRecursive, DoubleSortAction = SortingAlgorithms.QuickSortRecursive, StringSortAction = SortingAlgorithms.QuickSortRecursive, CustomDataSortAction = SortingAlgorithms.QuickSortRecursive, DateTimeSortAction = SortingAlgorithms.QuickSortRecursive },
                new SortAlgorithm { Name = "QuickSort (Iter)", IntSortAction = SortingAlgorithms.QuickSortIterative, DoubleSortAction = SortingAlgorithms.QuickSortIterative, StringSortAction = SortingAlgorithms.QuickSortIterative, CustomDataSortAction = SortingAlgorithms.QuickSortIterative, DateTimeSortAction = SortingAlgorithms.QuickSortIterative },
                new SortAlgorithm { Name = "MergeSort (Rec)", IsRecursive = true, NeedsAuxiliaryArray = true, IntSortAction = SortingAlgorithms.MergeSortRecursive, DoubleSortAction = SortingAlgorithms.MergeSortRecursive, StringSortAction = SortingAlgorithms.MergeSortRecursive, CustomDataSortAction = SortingAlgorithms.MergeSortRecursive, DateTimeSortAction = SortingAlgorithms.MergeSortRecursive },
                new SortAlgorithm { Name = "MergeSort (Iter)", NeedsAuxiliaryArray = true, IntSortAction = SortingAlgorithms.MergeSortIterative, DoubleSortAction = SortingAlgorithms.MergeSortIterative, StringSortAction = SortingAlgorithms.MergeSortIterative, CustomDataSortAction = SortingAlgorithms.MergeSortIterative, DateTimeSortAction = SortingAlgorithms.MergeSortIterative },
                new SortAlgorithm { Name = "HeapSort", IntSortAction = SortingAlgorithms.HeapSort, DoubleSortAction = SortingAlgorithms.HeapSort, StringSortAction = SortingAlgorithms.HeapSort, CustomDataSortAction = SortingAlgorithms.HeapSort, DateTimeSortAction = SortingAlgorithms.HeapSort },
                new SortAlgorithm { Name = "RadixSort", IsIntOnly = true, NeedsAuxiliaryArray = true, IntSortAction = SortingAlgorithms.RadixSort },
                new SortAlgorithm { Name = "BuiltInSort", IntSortAction = SortingAlgorithms.BuiltInSorting, DoubleSortAction = SortingAlgorithms.BuiltInSorting, StringSortAction = SortingAlgorithms.BuiltInSorting, CustomDataSortAction = SortingAlgorithms.BuiltInSorting, DateTimeSortAction = SortingAlgorithms.BuiltInSorting }
            };
        }

        public static void RunTests()
        {
            Console.WriteLine("Начало тестирования производительности алгоритмов сортировки...");

            foreach (int size in ArraySizes)
            {
                Console.WriteLine($"\nТестирование для размера массива: {size}");
                string fileName = $"SortResults_Size_{size}.txt";
                using (StreamWriter writer = new StreamWriter(fileName, false, Encoding.UTF8))
                {
                    writer.WriteLine($"Результаты тестирования сортировок для размера массива: {size}");
                    writer.WriteLine("----------------------------------------------------------");

                    writer.Write("Тип данных\\Алгоритм\t");
                    foreach (var algo in Algorithms)
                    {
                        writer.Write($"{algo.Name}\t");
                    }
                    writer.WriteLine();

                    TestAndWriteResults(size, "int", writer,
                        (s) => GenerateArray.GenerateIntArray(s),
                        (s) => GenerateArray.GenerateSortedIntArray(s),
                        (s) => GenerateArray.GenerateReverseSortedIntArray(s),
                        (s) => GenerateArray.GenerateArrayWithManyDuplicates(s, 10),
                        (s) => GenerateArray.GeneratePartiallyOrderedIntArray(s, (int)(s * 0.01))
                    );

                    TestAndWriteResults(size, "double", writer,
                        (s) => GenerateArray.GenerateDoubleArray(s)
                    );

                    TestAndWriteResults(size, "string", writer,
                        (s) => GenerateArray.GenerateStringArray(s)
                    );

                    TestAndWriteResults(size, "CustomData", writer,
                        (s) => GenerateArray.GenerateCustomDateArray(s)
                    );

                    TestAndWriteResults(size, "DateTime", writer,
                        (s) => GenerateArray.GenerateDateTimeArray(s)
                    );

                }
                Console.WriteLine($"Результаты для размера {size} записаны в файл: {fileName}");
            }

            Console.WriteLine("\nТестирование завершено.");
        }

        private static void TestAndWriteResults<T>(int size, string dataType, StreamWriter writer,
                                                    params Func<int, T[]>[] arrayGenerators)
                                                    where T : IComparable<T>
        {
            string[] intArrayTypeDescriptions = {
                "Случайные",
                "Отсортированные",
                "Обратно отсортированные",
                "С дубликатами",
                "Частично упорядоченные"
            };

            string otherDataTypeDescription = "Случайные";


            for (int i = 0; i < arrayGenerators.Length; i++)
            {
                Func<int, T[]> generator = arrayGenerators[i];
                string description;

                if (dataType == "int")
                {
                    description = dataType + " (" + intArrayTypeDescriptions[i] + ")";
                }
                else
                {
                    description = dataType + " (" + otherDataTypeDescription + ")";
                    if (i > 0) break; 
                }

                writer.Write($"{description}\t");

                foreach (var algo in Algorithms)
                {
                    string result = "N/A";

                    if (algo.IsIntOnly && dataType != "int")
                    {
                        result = "N/A";
                    }
                    else if (algo.IsRecursive && size > RecursiveMaxSafeSize)
                    {
                        result = "INF (SOF)";
                    }
                    else
                    {
                        try
                        {
                            double time = -1.0;

                            if (dataType == "int" && algo.IntSortAction != null)
                            {
                                int[] intArray = generator(size) as int[];
                                if (intArray != null)
                                    time = MeasureSortTime<int>(algo.IntSortAction, intArray);
                            }
                            else if (dataType == "double" && algo.DoubleSortAction != null)
                            {
                                double[] doubleArray = generator(size) as double[];
                                if (doubleArray != null)
                                    time = MeasureSortTime<double>(algo.DoubleSortAction, doubleArray);
                            }
                            else if (dataType == "string" && algo.StringSortAction != null)
                            {
                                string[] stringArray = generator(size) as string[];
                                if (stringArray != null)
                                    time = MeasureSortTime<string>(algo.StringSortAction, stringArray);
                            }
                            else if (dataType == "CustomData" && algo.CustomDataSortAction != null)
                            {
                                CustomDate[] customDataArray = generator(size) as CustomDate[];
                                if (customDataArray != null)
                                    time = MeasureSortTime<CustomDate>(algo.CustomDataSortAction, customDataArray);
                            }
                            else if (dataType == "DateTime" && algo.DateTimeSortAction != null)
                            {
                                DateTime[] dateTimeArray = generator(size) as DateTime[];
                                if (dateTimeArray != null)
                                    time = MeasureSortTime<DateTime>(algo.DateTimeSortAction, dateTimeArray);
                            }


                            result = time >= 0 ? time.ToString("F4") : (time == -2.0 ? "INF (SOF)" : "ERROR");
                        }
                        catch (Exception ex)
                        {
                            if (ex is StackOverflowException)
                            {
                                Console.WriteLine($"\nStackOverflow для {algo.Name} ({dataType}) при размере {size}");
                                result = "INF (SOF)";
                            }
                            else
                            {
                                Console.WriteLine($"\nПри выполнении {algo.Name} для {description}: {ex.Message}");
                                result = "ERROR";
                            }
                        }
                    }
                    writer.Write($"{result}\t");
                }
                writer.WriteLine();
            }
        }

        private static double MeasureSortTime<T>(Action<T[]> sortAlgorithm, T[] arrayToTest) where T : IComparable<T>
        {
            T[] copyArray = new T[arrayToTest.Length];
            Array.Copy(arrayToTest, copyArray, arrayToTest.Length);

            Stopwatch stopwatch = new Stopwatch();
            double elapsedMilliseconds = -1.0;

            if (sortAlgorithm.Method.Name.Contains("Recursive") && copyArray.Length <= RecursiveMaxSafeSize)
            {
                Thread sortThread = new Thread(() =>
                {
                    try
                    {
                        stopwatch.Start();
                        sortAlgorithm(copyArray);
                        stopwatch.Stop();
                        elapsedMilliseconds = stopwatch.Elapsed.TotalMilliseconds;
                    }
                    catch (StackOverflowException)
                    {
                        Console.WriteLine($"\nStackOverflow для {typeof(T).Name} в {sortAlgorithm.Method.Name} при размере {copyArray.Length}. Возвращаем INF.");
                        elapsedMilliseconds = -2.0;
                    }
                    catch (Exception ex)
                    {
                        Console.WriteLine($"\nОшибка сортировки {sortAlgorithm.Method.Name} для {typeof(T).Name}: {ex.Message}");
                        elapsedMilliseconds = -3.0;
                    }
                }, 32 * 1024 * 1024);

                sortThread.Start();
                sortThread.Join();
            }
            else
            {
                try
                {
                    stopwatch.Start();
                    sortAlgorithm(copyArray);
                    stopwatch.Stop();
                    elapsedMilliseconds = stopwatch.Elapsed.TotalMilliseconds;
                }
                catch (StackOverflowException)
                {
                    Console.WriteLine($"\nStackOverflow для {typeof(T).Name} в {sortAlgorithm.Method.Name} при размере {copyArray.Length}. Возвращаем INF.");
                    elapsedMilliseconds = -2.0;
                }
                catch (Exception ex)
                {
                    Console.WriteLine($"\nОшибка сортировки {sortAlgorithm.Method.Name} для {typeof(T).Name}: {ex.Message}");
                    elapsedMilliseconds = -3.0;
                }
            }

            if (elapsedMilliseconds >= 0 && !SortingAlgorithms.IsSorted(copyArray))
            {
                Console.WriteLine($"\nАлгоритм {sortAlgorithm.Method.Name} не отсортировал массив корректно для размера {copyArray.Length}");
                return -4.0;
            }

            return elapsedMilliseconds;
        }

    }
}