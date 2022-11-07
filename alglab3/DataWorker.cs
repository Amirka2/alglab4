using System;
using System.IO;
using System.Text;

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

        public static Table GetFilteredTable(Table table, string attributeName, string attribute)
        {
            Table newTable = new Table(table.Header.attributes);
            int attrIndex = -1;
            for(int i = 0; i < newTable.Header.attributes.Length; i++)          //находим индекс нужного атрибута
            {
                if (attributeName == newTable.Header.attributes[i])
                    attrIndex = i;
            }

            foreach(var row in table.Rows)
            {
                if (row.attributes[attrIndex] == attribute)
                    newTable.AddRow(row);
            }

            return newTable;
        }

        public static int[] GetArrayFromTable(Table table)
        {
            int[] array = new int[table.Count];

            for(int i = 0; i < array.Length; i++)
            {
                array[i] = table.Rows[i].number;
            }

            return array;
        }

        public static void RewriteTableBySort(Table table, int[] sortedArray, string path)
        {
            Table newTable = new Table(table.Header.attributes);

            for(int i = 0; i < sortedArray.Length; i++)
            {
                for(int j = 0; j < table.Rows.Count; j++)
                {
                    if (sortedArray[i] == table.Rows[j].number)
                        newTable.AddRow(table.Rows[j]);
                }
            }

            WriteTableData(newTable, path);
        }

        static void WriteTableData(Table table, string path)
        {
            StringBuilder sb = new StringBuilder();
            for(int j = 0; j < table.Rows.Count; j++)
            {
                for (int i = 0; i < table.Rows[0].attributes.Length; i++)
                {
                    sb.Append(table.Rows[j].attributes[i] + ";");
                    Console.Write(table.Rows[j].attributes[i] + ";");
                }
                sb.Append(table.Rows[j].number);
                Console.WriteLine(table.Rows[j].number);
            }
            StreamWriter sw = new StreamWriter(path, true);
            sb.ToString();
            sw.Write(sb.ToString());
        }
    }
}

