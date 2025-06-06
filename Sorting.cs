using System;

namespace Test_Sorting
{
    public static class SortingAlgorithms
    {
        private static void Swap<T>(T[] array, int i, int j)
        {
            T temp = array[i];
            array[i] = array[j];
            array[j] = temp;
        }

        public static void BubbleSort<T>(T[] array) where T : IComparable<T>
        {
            if (array == null || array.Length <= 1) return;
            int n = array.Length;
            bool swapped;
            for (int i = 0; i < n - 1; i++)
            {
                swapped = false;
                for (int j = 0; j < n - 1 - i; j++)
                {
                    if (array[j].CompareTo(array[j + 1]) > 0)
                    {
                        Swap(array, j, j + 1);
                        swapped = true;
                    }
                }
                if (!swapped)
                    break;
            }
        }

        public static void InsertionSort<T>(T[] array) where T : IComparable<T>
        {
            if (array == null || array.Length <= 1) return;
            int n = array.Length;
            for (int i = 1; i < n; i++)
            {
                T key = array[i];
                int j = i - 1;
                while (j >= 0 && array[j].CompareTo(key) > 0)
                {
                    array[j + 1] = array[j];
                    j--;
                }
                array[j + 1] = key;
            }
        }

        public static void SelectionSort<T>(T[] array) where T : IComparable<T>
        {
            if (array == null || array.Length <= 1) return;
            int n = array.Length;
            for (int i = 0; i < n - 1; i++)
            {
                int minIdx = i;
                for (int j = i + 1; j < n; j++)
                {
                    if (array[j].CompareTo(array[minIdx]) < 0)
                    {
                        minIdx = j;
                    }
                }
                Swap(array, i, minIdx);
            }
        }

        // Сортировка Шелла
        public static void ShellSort<T>(T[] array) where T : IComparable<T>
        {
            if (array == null || array.Length <= 1) return;
            int n = array.Length;
            List<int> gaps = new List<int>();
            int h = 1;
            while (h < n / 3)
            {
                gaps.Add(h);
                h = 3 * h + 1;
            }
            gaps.Reverse();
            int[] gapSequence = gaps.ToArray();
            if (gapSequence.Length == 0 && n > 0)
            {
                gapSequence = new int[] { 1 };
            }

            foreach (int gap in gapSequence)
            {
                if (gap <= 0) continue;
                for (int i = gap; i < n; i++)
                {
                    T temp = array[i];
                    int j;
                    for (j = i; j >= gap && array[j - gap].CompareTo(temp) > 0; j -= gap)
                    {
                        array[j] = array[j - gap];
                    }
                    array[j] = temp;
                }
            }
        }

        // Быстрая сортировка 
        private static int Partition<T>(T[] array, int low, int high) where T : IComparable<T>
        {
            T pivot = array[high];
            int i = (low - 1);
            for (int j = low; j < high; j++)
            {
                if (array[j].CompareTo(pivot) <= 0)
                {
                    i++;
                    Swap(array, i, j);
                }
            }
            Swap(array, i + 1, high);
            return i + 1;
        }

        public static void QuickSortRecursive<T>(T[] array) where T : IComparable<T>
        {
            if (array == null || array.Length <= 1) return;
            QuickSortRecursiveInternal(array, 0, array.Length - 1);
        }

        private static void QuickSortRecursiveInternal<T>(T[] array, int low, int high) where T : IComparable<T>
        {
            if (low < high)
            {
                int pi = Partition(array, low, high);
                QuickSortRecursiveInternal(array, low, pi - 1);
                QuickSortRecursiveInternal(array, pi + 1, high);
            }
        }

        public static void QuickSortIterative<T>(T[] array) where T : IComparable<T>
        {
            if (array == null || array.Length <= 1) return;
            Stack<int> stack = new Stack<int>();
            stack.Push(0);
            stack.Push(array.Length - 1);

            while (stack.Count > 0)
            {
                int high = stack.Pop();
                int low = stack.Pop();
                int pi = Partition(array, low, high);

                if (pi - 1 > low)
                {
                    stack.Push(low);
                    stack.Push(pi - 1);
                }
                if (pi + 1 < high)
                {
                    stack.Push(pi + 1);
                    stack.Push(high);
                }
            }
        }

        // Сортировка слиянием
        private static void Merge<T>(T[] array, int left, int middle, int right, T[] tempArray) where T : IComparable<T>
        {
            Array.Copy(array, left, tempArray, left, right - left + 1);
            int i = left;
            int j = middle + 1;
            int k = left;

            while (i <= middle && j <= right)
            {
                if (tempArray[i].CompareTo(tempArray[j]) <= 0)
                {
                    array[k] = tempArray[i];
                    i++;
                }
                else
                {
                    array[k] = tempArray[j];
                    j++;
                }
                k++;
            }
            while (i <= middle)
            {
                array[k] = tempArray[i];
                i++;
                k++;
            }
        }

        public static void MergeSortRecursive<T>(T[] array) where T : IComparable<T>
        {
            if (array == null || array.Length <= 1) return;
            T[] tempArray = new T[array.Length];
            MergeSortRecursiveInternal(array, 0, array.Length - 1, tempArray);
        }

        private static void MergeSortRecursiveInternal<T>(T[] array, int left, int right, T[] tempArray) where T : IComparable<T>
        {
            if (left < right)
            {
                int middle = left + (right - left) / 2;
                MergeSortRecursiveInternal(array, left, middle, tempArray);
                MergeSortRecursiveInternal(array, middle + 1, right, tempArray);
                Merge(array, left, middle, right, tempArray);
            }
        }

        public static void MergeSortIterative<T>(T[] array) where T : IComparable<T>
        {
            if (array == null || array.Length <= 1) return;
            int n = array.Length;
            T[] tempArray = new T[n];

            for (int size = 1; size < n; size *= 2)
            {
                for (int leftStart = 0; leftStart < n - 1; leftStart += 2 * size)
                {
                    int mid = Math.Min(leftStart + size - 1, n - 1);
                    int rightEnd = Math.Min(leftStart + 2 * size - 1, n - 1);
                    if (mid < rightEnd)
                    {
                        Merge(array, leftStart, mid, rightEnd, tempArray);
                    }
                }
            }
        }

        // Сортировка кучей
        public static void HeapSort<T>(T[] array) where T : IComparable<T>
        {
            if (array == null || array.Length <= 1) return;
            int n = array.Length;

            for (int i = n / 2 - 1; i >= 0; i--)
            {
                Heapify(array, n, i);
            }

            for (int i = n - 1; i > 0; i--)
            {
                Swap(array, 0, i);
                Heapify(array, i, 0);
            }
        }

        private static void Heapify<T>(T[] array, int n, int i) where T : IComparable<T>
        {
            int largest = i;
            while (true)
            {
                int left = 2 * i + 1;
                int right = 2 * i + 2;

                if (left < n && array[left].CompareTo(array[largest]) > 0)
                {
                    largest = left;
                }

                if (right < n && array[right].CompareTo(array[largest]) > 0)
                {
                    largest = right;
                }

                if (largest != i)
                {
                    Swap(array, i, largest);
                    i = largest;
                }
                else
                {
                    break;
                }
            }
        }

        // Поразрядная сортировка
        public static void RadixSort(int[] array)
        {
            if (array == null || array.Length <= 1) return;

            int minVal = array.Min();
            int offset = 0;

            if (minVal < 0)
            {
                offset = -minVal; 
                for (int i = 0; i < array.Length; i++)
                {
                    array[i] += offset;
                }
            }

            int maxVal = array.Max();

            for (int exp = 1; maxVal / exp > 0; exp *= 10)
            {
                CountSort(array, exp);
            }

            if (offset > 0)
            {
                for (int i = 0; i < array.Length; i++)
                {
                    array[i] -= offset;
                }
            }
        }

        private static void CountSort(int[] array, int exp)
        {
            int n = array.Length;
            int[] output = new int[n];
            int[] count = new int[10];

            for (int i = 0; i < 10; i++)
                count[i] = 0;

            for (int i = 0; i < n; i++)
                count[(array[i] / exp) % 10]++;

            for (int i = 1; i < 10; i++)
                count[i] += count[i - 1];

            for (int i = n - 1; i >= 0; i--)
            {
                output[count[(array[i] / exp) % 10] - 1] = array[i];
                count[(array[i] / exp) % 10]--;
            }

            for (int i = 0; i < n; i++)
                array[i] = output[i];
        }

        // Встроенная сортировка 

        public static void BuiltInSorting<T>(T[] array) where T : IComparable<T>
        {
            if (array == null) return;
            Array.Sort(array);
        }

        public static bool IsSorted<T>(T[] array) where T : IComparable<T>
        {
            if (array == null || array.Length <= 1) return true;
            for (int i = 0; i < array.Length - 1; i++)
            {
                if (array[i].CompareTo(array[i + 1]) > 0)
                {
                    return false;
                }
            }
            return true;
        }

    }
}