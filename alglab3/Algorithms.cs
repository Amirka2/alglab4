using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace alglab3
{
    internal class Algorithms
    {
        public static void BubbleSort(List<string> words)
        {
            var watch = Stopwatch.StartNew();
            string temp;
            for (int i = 0; i < words.Count; i++)
            {
                for (int j = i + 1; j < words.Count; j++)
                {
                    if (GetBool(words[i], words[j]))
                    {
                        temp = words[i];
                        words[i] = words[j];
                        words[j] = temp;
                    }
                }
            }
            string[] result = words.ToArray();
            watch.Stop();
            Checker.CountWords(result);
            var elapsedTime = watch.Elapsed;
            Console.WriteLine(elapsedTime * 1000);

        }
        public static void InsertionSort(List<string> words)
        {
            string[] result = new string[words.Count];
            for (int i = 0; i < words.Count; i++)
            {
                int j = i;
                while (j > 0 && GetBool(result[j - 1], words[i]))
                {
                    result[j] = result[j - 1];
                    j--;
                }
                result[j] = words[i];
            }

            
            Checker.CountWords(result);
        }
        public static bool GetBool(string word1, string word2)
        {
            for (int i = 0; i < (word1.Length > word2.Length ? word2.Length : word1.Length); i++)
            {
                char c1 = Char.ToLower(word1[i]);
                char c2 = Char.ToLower(word2[i]);
                if ((int)c1 > (int)c2)
                {
                    return true;
                }
                else if ((int)c1 == (int)c2)
                {
                    continue;
                }
                else
                {
                    return false;
                }
            }
            return false;
        }
    }
}
