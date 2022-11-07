using System;
using System.IO;

namespace alglab3
{
    public static class DataWorker
    {
        public static Table GetTableFromFile(string path)
        {
            Table result = new Table();
            try
            {
                StreamReader sr = new StreamReader(path);
                string line = sr.ReadLine();
                string[] attributesNames = line.Split(';');

                Table table = new Table(attributesNames);
                line = sr.ReadLine();

                while(line != null)
                {
                    int number = -1;
                    string[] attributes = GetAttributesFromString(line, out number);

                    table.AddRow(attributes, number);

                    line = sr.ReadLine();
                }
                result = table;
            }
            catch(IOException)
            {
                Console.WriteLine("Table is empty!");
            }

            return result;
        }

        private static string[] GetAttributesFromString(string input, out int number)
        {
            string[] attributes = input.Split(';');
            string[] res = new string[attributes.Length - 1];
            number = -1;

            try
            {
                number = Int32.Parse(attributes[attributes.Length - 1]);
            }
            catch (InvalidCastException)
            {
                Console.WriteLine("Числовой аттрибут не является числом");
            }

            for (int i = 0; i < res.Length; i++)
            {
                res[i] = attributes[i];
            }

            return res;
        }

        public static Table GetFilteredTable(Table table, string attribute)
        {
            Table newTable = new Table(table.Header.attributes);
            foreach(var row in table.Rows)
            {
                foreach(var attr in row.attributes)
                {
                    if (attr == attribute)
                        newTable.AddRow(row);
                }
            }

            return newTable;
        }
    }
}

