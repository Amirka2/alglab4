using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;

namespace alglab3
{
    class Program
    {
        static void Main(string[] args)
        {
            //Run1Task();
            //Run2Task();
            //List<string> str = new List<string>();
            //str.Add("aah");
            //str.Add("abp");
            //str.Add("aaa");


            //Algorithms.BubbleSort(Checker.GetWordsList());
        }
        static void Run1Task()
        {
            try
            {
                Console.Write("Введите числа через пробел: ");
                var input = Console.ReadLine();
                var inputArr = input.Trim().Split(" ");
                int[] arr = new int[inputArr.Length];
                for (int i = 0; i < inputArr.Length; i++)
                {
                    arr[i] = Int32.Parse(inputArr[i]);
                }
                Sorts s = new Sorts(arr);
                Console.WriteLine("Выберите алгоритм сортировки: 1(BubbleSort), 2(QuickSort)");
                var option = Console.ReadLine();
                Console.Write("Введите задержку в милисекундах: ");
                var delay = Int32.Parse(Console.ReadLine());

                switch (option)
                {
                    case "1":
                        s.Bubblesort();
                        Output.Print("bubbleSort.txt", delay);
                        break;
                    case "2":
                        s.Quicksort();
                        Output.Print("quickSort.txt", delay);
                        break;
                    default:
                        break;
                }
            }
            catch (InvalidCastException)
            {
                Console.WriteLine("Неккоректный ввод!");
            }
        }
        static void Run2Task()
        {
            Table table = DataWorker.GetTableFromFile("tables/countries.txt");
            Console.WriteLine("Выберите желаемый метод сортировки (1 - прямое, 2 - естественное, 3 - многопутевое.)");
            string sortOption = Console.ReadLine();


            Console.WriteLine($"Возможные атрибуты: {table.Header.GetAttributes()}");
            Console.WriteLine("Введите название ключевого атрибута: ");
            string attributeName = Console.ReadLine().Trim();
            Console.WriteLine("Введите значение ключевого атрибута: ");
            string attribute = Console.ReadLine().Trim();


            Table filteredTable = DataWorker.GetFilteredTable(table, attributeName, attribute);
            int[] arr = DataWorker.GetArrayFromTable(filteredTable);
            int[] resultArr = new int[arr.Length];

            switch (sortOption)
            {
                case "1":
                    //resultArr = sort1(arr);
                    break;
                case "2":
                    //resultArr = sort2(arr);
                    break;
                case "3":
                    //resultArr = sort3(arr);
                    break;
                default:
                    Console.WriteLine("Введен некорректный способ сортировки");
                    break;
            }

            
            DataWorker.RewriteTableBySort(table, resultArr, "tables/countriesSorted.txt"); 
            
        }
    }
}

