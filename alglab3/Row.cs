using System;
namespace alglab3
{
    public class Row
    {
        public string[] attributes { get; private set; }
        public int number { get; private set; }
        public Row(string[] attributes, int number)
        {
            this.attributes = attributes;
            this.number = number;
        }
    }
}

