using System;
using System.Diagnostics;

namespace BigONotation
{
    class Program
    {
        static void Main()
        {
            const int size = 100000;
            var rnd = new Random();
            var original = new int[size];
            for (int i = 0; i < size; i++)
                original[i] = rnd.Next();

            var dataBubble = (int[])original.Clone();
            var dataQuick = (int[])original.Clone();
            var dataMerge = (int[])original.Clone();

            var sw = Stopwatch.StartNew();
            BubbleSort(dataBubble);
            sw.Stop();
            Console.WriteLine($"BubbleSort: {sw.ElapsedMilliseconds} ms");

            sw.Restart();
            QuickSort(dataQuick, 0, dataQuick.Length - 1);
            sw.Stop();
            Console.WriteLine($"QuickSort: {sw.ElapsedMilliseconds} ms");

            sw.Restart();
            MergeSort(dataMerge, 0, dataMerge.Length - 1);
            sw.Stop();
            Console.WriteLine($"MergeSort: {sw.ElapsedMilliseconds} ms");
        }

        static void BubbleSort(int[] arr)
        {
            int n = arr.Length;
            for (int i = 0; i < n - 1; i++)
            {
                for (int j = 0; j < n - 1 - i; j++)
                {
                    if (arr[j] > arr[j + 1])
                    {
                        var temp = arr[j];
                        arr[j] = arr[j + 1];
                        arr[j + 1] = temp;
                    }
                }
            }
        }

        static void QuickSort(int[] arr, int left, int right)
        {
            if (left >= right) return;
            int pivot = arr[(left + right) / 2];
            int index = Partition(arr, left, right, pivot);
            QuickSort(arr, left, index - 1);
            QuickSort(arr, index, right);
        }

        static int Partition(int[] arr, int left, int right, int pivot)
        {
            while (left <= right)
            {
                while (arr[left] < pivot) left++;
                while (arr[right] > pivot) right--;
                if (left <= right)
                {
                    var temp = arr[left];
                    arr[left] = arr[right];
                    arr[right] = temp;
                    left++;
                    right--;
                }
            }
            return left;
        }

        static void MergeSort(int[] arr, int left, int right)
        {
            if (left >= right) return;
            int mid = left + (right - left) / 2;
            MergeSort(arr, left, mid);
            MergeSort(arr, mid + 1, right);
            Merge(arr, left, mid, right);
        }

        static void Merge(int[] arr, int left, int mid, int right)
        {
            int n1 = mid - left + 1;
            int n2 = right - mid;
            var leftArr = new int[n1];
            var rightArr = new int[n2];

            Array.Copy(arr, left, leftArr, 0, n1);
            Array.Copy(arr, mid + 1, rightArr, 0, n2);

            int i = 0, j = 0, k = left;
            while (i < n1 && j < n2)
            {
                if (leftArr[i] <= rightArr[j])
                {
                    arr[k++] = leftArr[i++];
                }
                else
                {
                    arr[k++] = rightArr[j++];
                }
            }
            while (i < n1)
                arr[k++] = leftArr[i++];
            while (j < n2)
                arr[k++] = rightArr[j++];
        }
    }
}
