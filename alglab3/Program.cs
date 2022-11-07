using System;

namespace alglab3
{
    class Program
    {
        static void Main(string[] args)
        {
            Table table = DataWorker.GetTableFromFile("tables/countries.txt");
            Table filteredTable = DataWorker.GetFilteredTable(table, "asia");
        }
    }
}

