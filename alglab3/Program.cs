using System;
using System.IO;

namespace alglab3
{
    class Program
    {
        static void Main(string[] args)
        {
            Run2Task();
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

