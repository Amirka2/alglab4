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
            string sortOption = Console.ReadLine();

            Console.WriteLine($"Возможные атрибуты: {DataWorker.GetAttributesFromFile("tables/header.txt")} ");
            Console.WriteLine("Введите индекс ключевого атрибута (он должен быть числовым): ");
            int keyIndex = Int32.Parse(Console.ReadLine().Trim());
            Console.WriteLine("Введите название атрибута, по которому отфильтровать таблицу: ");
            string attributeName = Console.ReadLine().Trim();
            Console.WriteLine("Введите значение фильтрующего атрибута: ");
            string attributeValue = Console.ReadLine().Trim();
            Console.WriteLine("Введите время задержки вывода (в милисекундах): ");
            int delay = Int32.Parse(Console.ReadLine());

            string text = "Mongolia;asia;Ulaanbaatar;1 564 116;2 736 800\nJapan;asia;Tokyo;377 97;126 435 321\nKazakhstan;asia;Nur-Sultan;2 724 900;16 674 960\nSaudi Arabia;asia;Riyadh;2 218 000;28 686 630\nIndonesia;asia;Jakarta;1 919 440;237 641 330\nIran;asia;Tehran;1 648 000;74 000 000\nPakistan;asia;Islamabad;803 940;190 291 130\nGermany;europe;Berlin;357 168;82 800 000\nFrance;europe;Paris;547 030; 67 348 000\nGreat Britain;europe;London;244 820;66 040 229\nItaly;europe;Rome;301 338;60 589 445\nUkraine;europe;Kiev;603 628;42 418 235\nPoland;europe;Warsaw;312 685;38 422 346\nNetherlands;europe;Amsterdam;41 543;17 271 990\nCzech Republic;europe;Prague;78 866;10 610 947\nArgentina;south america;Buenos Aires;2 766 890;46 226 168\nBolivia;south america;Sucre;1 098 580;12 069 813\nColombia;south america;Bogotá;1 138 910;52 698 313\nPeru;south america;Lima;1 285 220;34 333 082\nChile;south america;Santiago;1 285 220;19 724 059\nVenezuela;south america;Caracas;912 050;27 297 542\nUruguay;south america;Montevideo;176 220;3 504 084\nEcuador;south america;Quito;214 534;18 386 773\nChina;asia;Beijing;9 598 077;1 350 120 000\nIndia;asia;New Delhi;3 287 590;1 224 245 000";
            using (StreamWriter sw = new StreamWriter("tables/countries.txt", false))
                sw.Write(text);
            
            

            switch (sortOption)
            {
                case "1":
                    TableSorts ts = new TableSorts("tables/countries.txt", keyIndex, delay, attributeName, attributeValue);
                    ts.DirectSort();
                    break;
                case "2":
                    NaturalSort ns = new NaturalSort("tables/countries.txt", keyIndex, delay, attributeName, attributeValue);
                    ns.Sort();
                    break;
                case "3":
                    MultipathMergeSort ms = new MultipathMergeSort("tables/countries.txt", keyIndex, delay, attributeName, attributeValue);
                    ms.Sort();
                    break;
                default:
                    Console.WriteLine("Введен некорректный способ сортировки");
                    break;
            }
        }
    }
}

