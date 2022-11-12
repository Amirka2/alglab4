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
            Run2Task();
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
            Console.WriteLine("Выберите желаемый метод сортировки (1 - прямое, 2 - естественное, 3 - многопутевое.)");
            string sortOption = "1";
            int delay = 10;

            Console.WriteLine($"Возможные атрибуты: {DataWorker.GetAttributesFromFile("tables/countries.txt")} ");
            Console.WriteLine("Введите название ключевого атрибута (он должен быть числовым): ");
            //int keyAttribute = Int32.Parse(Console.ReadLine().Trim());
            Console.WriteLine("Введите название атрибута, по которому отфильтровать таблицу: ");
            //string attributeName = Console.ReadLine().Trim();
            Console.WriteLine("Введите значение фильтрующего атрибута: ");
            //string attributeValue = Console.ReadLine().Trim();

            TableSorts ts = new TableSorts("tables/countries.txt", 4, delay);
            string text = "china;asia;beijin;100;151\nchina;asia;beijin;100;100\nchina;asia;beijin;100;1500\nkorea;asia;Seoul;100;150\n1;asia;Seoul;100;1\n2;asia;Seoul;100;170\n3;asia;Seoul;100;100\n4;asia;Seoul;100;1000";
            using (StreamWriter sw = new StreamWriter("tables/countries.txt", false))
                sw.Write(text);
            

            switch (sortOption)
            {
                case "1":
                    ts.Sort();
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
        }
    }
}

