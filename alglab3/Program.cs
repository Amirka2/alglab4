using System;
using System.IO;

namespace alglab3
{
    class Program
    {
        static void Main(string[] args)
        {
            Run2Task();
            StreamWriter sw = new StreamWriter("123.txt", true);
            sw.Write("123");
        }
        static void Run2Task()
        {
            Table table = DataWorker.GetTableFromFile("tables/countries.txt");
            Console.WriteLine("Выберите желаемый метод сортировки (1 - прямое, 2 - естественное, 3 - многопутевое.)");
            //string sortOption = Console.ReadLine();


            Console.WriteLine($"Возможные атрибуты: {table.Header.GetAttributes()}");
            Console.WriteLine("Введите название ключевого атрибута: ");
            //string attributeName = Console.ReadLine().Trim();
            Console.WriteLine("Введите значение ключевого атрибута: ");
            //string attribute = Console.ReadLine().Trim();


            //Table filteredTable = DataWorker.GetFilteredTable(table, attributeName, attribute);
            int[] arr = DataWorker.GetArrayFromTable(table);
            int[] resultArr = { 10000000, 1500000000, 150000000 };
            DataWorker.RewriteTableBySort(table, resultArr, "tables/countriesSorted.txt"); 

        }
    }
}

