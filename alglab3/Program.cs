using System;

namespace alglab3
{
    class Program
    {
        static void Main(string[] args)
        {
            int[] arr = new int[] { 1, 2, 6, 3, 0, 30, -1 };

            Sorts s = new Sorts(arr);

            s.Quicksort();
            s.Bubblesort();
            Output.Print("quickSort.txt", 1000);
            Output.Print("bubbleSort.txt", 1000);

        }
    }
}

