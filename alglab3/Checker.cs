using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace alglab3
{
    internal class Checker
    {
        public static void CountWords(string[] words)
        {

            IEnumerable<string> enumerable = words as IEnumerable<string>;
            var stringGroups = enumerable.GroupBy(s => s);
            foreach (var stringGroup in stringGroups)
                Console.WriteLine("{0}  {1}", stringGroup.Key, stringGroup.Count());


        }
        public static string Alphabe = "abcdefghijklmnopqrstuvwxyz";
        public static string WorkingFile()
        {

            string text;


            using (StreamReader sr = new StreamReader("text.txt"))
            {
                text = sr.ReadLine();
            }

            return text;
        }

        //получаем сам список слов
        public static List<string> GetWordsList()
        {
            string line = WorkingFile();
            string[] data = line.Split(" ");

            List<string> list = new List<string>();

            foreach (string word in data)
            {
                string result = "";

                for (int i = 0; i < word.Length; i++)
                {
                    for (int j = 0; j < 25; j++)
                    {
                        if (Char.ToLower(word[i]) == Alphabe[j])
                        {
                            result = result + word[i];
                        }
                        continue;

                    }
                }

                list.Add(result);

            }
            return list;

        }
    }
}
