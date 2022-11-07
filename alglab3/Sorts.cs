using System;
using System.Collections.Generic;
using System.IO;
using System.Text;

namespace alglab3
{
    public class Sorts
    {
        private int[] array { get; set; }
        public List<string> logs;
        public int delay { get; set; }
        private List<string> arrayChanges;

        public Sorts(int[] arr)
        {
            array = arr;
            arrayChanges = new List<string>();
            arrayChanges.Add(GetArray(arr));
        }

        public int[] Bubblesort()
        {
            logs = new List<string>();
            
            int[] temp = new int[array.Length];
            Array.Copy(array, temp, array.Length);

            for (int i = 0; i < temp.Length; i++)
            {
                //logs.Add(i + " - i");
                for (int j = i + 1; j < temp.Length; j++)
                {
                    //logs.Add(j + " - j");
                    logs.Add($"Сравниваем элементы {temp[i]} и {temp[j]}");

                    if (temp[i] > temp[j])
                        Swap(temp, i, j);
                }
            }

            WriteData("bubbleSort.txt");

            return temp;
        }

        public int[] Quicksort()
        {
            logs = new List<string>();

            int[] temp = new int[array.Length];
            Array.Copy(array, temp, array.Length);
            int[] result = Quicksorting(temp, 0, temp.Length - 1);

            WriteData("quickSort.txt");

            return result;
        }

        private int[] Quicksorting(int[] arr, int left, int right)
        {
            if (left < right)
            {
                logs.Add("Левый индекс меньше правого");
                int pivot = Partition(arr, left, right);

                if (pivot > 1)
                {
                    logs.Add($"pivot > 1. Вызываем QuickSort({left}, {pivot - 1})");
                    Quicksorting(arr, left, pivot - 1);
                }
                if (pivot + 1 < right)
                {
                    logs.Add($"pivot + 1 < правого индекса. Вызываем QuickSort({pivot + 1}, {right})");
                    Quicksorting(arr, pivot + 1, right);
                }
            }
            //logs.Add($"правый индекс больше левого. Возвращаем массив {GetArray(arr)}");

            return arr;
        }

        private int Partition(int[] arr, int left, int right)
        { 
            logs.Add("\nВходим в блок partition");
            logs.Add($"pivot = {arr[left]}");
            int pivot = arr[left];
            while (true)
            {
                while (arr[left] < pivot)
                {
                    logs.Add($"{arr[left]} < {pivot}, поэтому смотрим следующий элеменет");
                    left++;
                }

                while (arr[right] > pivot)
                {
                    logs.Add($"{arr[right]} > {pivot}, поэтому смотрим предыдущий элеменет");
                    right--;
                }

                if (left < right)
                {
                    logs.Add("Левый индекс меньше правого");
                    if (arr[left] == arr[right]) return right;

                    Swap(arr, left, right);
                }
                else
                {
                    logs.Add("Правый индекс меньше левого. Возвращаем правый индекс");
                    logs.Add("Выходим из блока partition\n");
                    logs.Add(" ");
                    return right;
                }
            }
        }

        public void Swap(int[] array, int i, int j)
        {
            logs.Add($"Меняем местами элементы {array[i]} и {array[j]} с индексами {i} и {j} : " +
                $"{GetArray(array)}");

            var temp = array[i];
            array[i] = array[j];
            array[j] = temp;

            arrayChanges.Add(GetArray(array));
        }

        private string GetArray(int[] arr)
        {
            var res = new StringBuilder();
            for(int i = 0; i < arr.Length - 1; i++)
            {
                res.Append($"{arr[i]}, ");
            }
            res.Append(arr[arr.Length - 1]);
            return res.ToString();
        }

        private void WriteData(string path)
        {
            File.Delete(path);
            using StreamWriter sw = new StreamWriter(path, true);
            foreach (var line in logs)
            {
                sw.WriteLine(line);
            }
            sw.WriteLine();
            foreach (var line in arrayChanges)
            {
                sw.WriteLine(line);
            }
        }
    }
}

