using System;
using System.Collections.Generic;

namespace alglab3
{
    public class Table
    {
        public Row Header { get; private set; }
        private List<Row> Rows = new List<Row>();

        public int RowCount { get; private set; }

        public Table()
        {
        }

        public Table(string[] attributesNames)
        {
            Header = new Row(attributesNames, 0);
        }
        public void AddRow(Row row)
        {
            Rows.Add(row);
            RowCount++;
        }
        public void AddRow(string[] attributes, int number)
        {
            Row row = new Row(attributes, number);
            AddRow(row);
        }
    }
}

