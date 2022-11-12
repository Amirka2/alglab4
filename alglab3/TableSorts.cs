using System;
using System.IO;
using System.Threading;

namespace alglab3
{
    public class TableSorts
    {
        private string AttributeNames;
        private int keyAttributeIndex;
        public string Path { get; private set; }
        private string pathA = "tables/aFile.txt";
        private string pathB = "tables/bFile.txt";
        private int delay;

        public TableSorts(string path, int keyIndex, int delay) 
        {
            Path = path;
            AttributeNames = GetAttributeNames();
            keyAttributeIndex = keyIndex;
            this.delay = delay;
        }
        public TableSorts(string path, int keyIndex, int delay, string filterAttr, string filterAttrValue)
        {
            Path = path;
            AttributeNames = GetAttributeNames();
            keyAttributeIndex = keyIndex;
            this.delay = delay;
            FilterTable(filterAttr, filterAttrValue);
        }

        public void Sort()
        {
            long linesCount = GetLinesCount(Path);
            int iterations = (int)Math.Log2(linesCount);
            
            for(int i = 0; i < iterations; i++)
            {
                Console.WriteLine("\nновый шаг: \n");
                int elements = (int)Math.Pow(2, i);
                SplitFile(elements);
                CompareAndWrite(elements);
                Console.WriteLine();
                PrintFile(pathA);
                PrintFile(pathB);
                PrintFile(Path);
            }
        }
        private void PrintFile(string path)
        {
            using (StreamReader sr = new StreamReader(path))
            {
                
                var line = sr.ReadLine();
                while(line != null)
                {
                    Console.Write($"{path}: \t");
                    Console.WriteLine(line);
                    line = sr.ReadLine();
                }
                Console.WriteLine();
            }
        }
        private  void FilterTable(string attributeName, string attributeValue)
        {
            using (StreamReader sr = new StreamReader(Path))
            {
                var line = sr.ReadLine();
                AttributeNames = line;
                var attributes = line.Trim().Split(';');
                int index = -1;
                for (int i = 0; i < attributes.Length; i++)
                {
                    if (attributeName == attributes[i])
                        index = i;
                }

                using (StreamWriter sw = new StreamWriter("temp" + Path, false))
                {
                    line = sr.ReadLine();
                    Console.WriteLine("Фильтруем таблицу на подтаблицу");

                    while (line != null)
                    {
                        attributes = line.Trim().Split(';');
                        if (attributes[index] == attributeValue)
                        {
                            Thread.Sleep(delay);
                            sw.WriteLine(line);
                            Console.WriteLine($"{attributes[index]} = {attributeValue}, подходит - записываем в подтаблицу");
                        }
                        line = sr.ReadLine();
                    }
                }
            }
            CopyFileData("temp" + Path);
            
        }
        private  void CopyFileData(string path)
        {
            using (StreamReader sr = new StreamReader(this.Path))
            using (StreamWriter sw = new StreamWriter(path, false))
            {
                var line = sr.ReadLine();
                while (line != null)
                {
                    sw.WriteLine(line);
                    line = sr.ReadLine();
                }
            }
        }

        private  void SplitFile(int elementsToWrite)
        {
            using (StreamReader sr = new StreamReader(Path))
            {
                var line = sr.ReadLine();
                using (StreamWriter swA = new StreamWriter(pathA, false))
                using (StreamWriter swB = new StreamWriter(pathB, false))
                {
                    int count = 0;

                    long linesCount = GetLinesCount(Path);

                    for (int i = 0; i < linesCount / elementsToWrite * 2; i++)
                    {
                        while (count < elementsToWrite)
                        {
                            if (line == null) return;

                            Thread.Sleep(delay);
                            Console.WriteLine($"Записываем строку: {line} в файл A");
                            swA.WriteLine(line);
                            count++;
                            line = sr.ReadLine();
                        }
                        while (count > 0)
                        {
                            if (line == null) return;

                            Thread.Sleep(delay);
                            Console.WriteLine($"Записываем строку: {line} в файл B");
                            swB.WriteLine(line);
                            count--;
                            line = sr.ReadLine();
                        }
                    }
                }
            }
        }

        private void CompareAndWrite(int iterations)
        {
            using (StreamWriter sw = new StreamWriter(Path, false)) 
            using (StreamReader srA = new StreamReader(pathA)) 
            using (StreamReader srB = new StreamReader(pathB))
            {
                var lineA = srA.ReadLine();
                var lineB = srB.ReadLine();

                while (true)
                {
                    for (int i = 0; i < iterations * 2; i++)
                    {
                        if (lineA == null && lineB != null)
                        {
                            while (lineB != null)
                            {
                                Thread.Sleep(delay);
                                Console.WriteLine("Элементы в А закончились, дописываем остатки В");
                                sw.WriteLine(lineB);
                                lineB = srB.ReadLine();
                            }
                            sw.Flush();
                            return;
                        }
                        else if (lineA != null && lineB == null)
                        {
                            while (lineA != null)
                            {
                                Thread.Sleep(delay);
                                Console.WriteLine("Элементы в В закончились, дописываем остатки А");
                                sw.WriteLine(lineA);
                                lineA = srA.ReadLine();
                            }
                            sw.Flush();
                            return;
                        }

                        var attrA = lineA.Trim().Split(';');
                        long x = Int64.Parse(attrA[keyAttributeIndex]);

                        var attrB = lineB.Trim().Split(';');
                        long y = Int64.Parse(attrB[keyAttributeIndex]);

                        Thread.Sleep(delay);
                        Console.Write($"\nСравниваем {x} и {y}. ");
                        if (x < y)
                        {
                            Console.WriteLine($"Записываем {x}\n");
                            sw.WriteLine(lineA);
                            lineA = srA.ReadLine();
                        }
                        else
                        {
                            Console.WriteLine($"Записываем {y}\n");
                            sw.WriteLine(lineB);
                            lineB = srB.ReadLine();
                        }
                    }
                }
            };
        }


        //private  void CompareAandBAndCreateResult(string pathA, string pathB, string resultPath, int keyAttrIndex)
        //{
        //    using (StreamReader srA = new StreamReader(pathA))
        //    using (StreamReader srB = new StreamReader(pathB))
        //    {
        //        var lineA = srA.ReadLine();
        //        var lineB = srB.ReadLine();

        //        using (StreamWriter sw = new StreamWriter(resultPath, false))
        //        {
        //            while (lineA != null || lineB != null)
        //            {
        //                if (lineA == null)
        //                {
        //                    sw.WriteLine(lineB);
        //                    return;
        //                }
        //                else
        //                {
        //                    sw.WriteLine(lineA);
        //                    return;
        //                }

        //                var a = GetKeyAttribute(lineA, keyAttrIndex);
        //                var b = GetKeyAttribute(lineB, keyAttrIndex);
        //                if (a > b)
        //                    sw.WriteLine(lineB);
        //                else
        //                    sw.WriteLine(lineB);

        //                lineA = srA.ReadLine();
        //                lineB = srB.ReadLine();
        //            }
        //        }
        //    }
        //}

        //private  void Compare2Step(string pathA, string pathB, string resultPath, int keyAttrIndex)
        //{
        //    using (StreamReader srA = new StreamReader(pathA))
        //    using (StreamReader srB = new StreamReader(pathB))
        //    {
        //        var lineA = srA.ReadLine();
        //        var lineB = srB.ReadLine();

        //        using (StreamWriter sw = new StreamWriter(resultPath, false))
        //        {
        //            while (lineA != null || lineB != null)
        //            {
                        
        //                var a = GetKeyAttribute(lineA, keyAttrIndex);
        //                var b = GetKeyAttribute(lineB, keyAttrIndex);
        //                if (a < b)
        //                {
        //                    WriteAndChangeLineA();
        //                    if (a < b)
        //                    {
        //                        WriteAndChangeLineA();
        //                        Compare2Elements(srA, srB, sw, keyAttrIndex);
        //                    }
        //                    else
        //                    {
        //                        WriteAndChangeLineB();
        //                        if (a < b)
        //                        {
        //                            WriteAndChangeLineA();
        //                            Compare2Elements(srA, srB, sw, keyAttrIndex);
        //                        }
        //                    }
        //                }
        //                else
        //                {
        //                    WriteAndChangeLineB();
        //                    if (a < b)
        //                    {
        //                        WriteAndChangeLineA();
        //                        Compare2Elements(srA, srB, sw, keyAttrIndex);
        //                    }
        //                    else
        //                    {
        //                        WriteAndChangeLineB();
        //                        if (a < b)
        //                        {
        //                            WriteAndChangeLineA();
        //                            Compare2Elements(srA, srB, sw, keyAttrIndex);
        //                        }
        //                    }
        //                }

        //                lineA = srA.ReadLine();
        //                lineB = srB.ReadLine();

        //                void WriteAndChangeLineA()
        //                {
        //                    sw.WriteLine(lineA);
        //                    lineA = srA.ReadLine();
        //                    if (lineA == null) return;
        //                    a = GetKeyAttribute(lineA, keyAttrIndex);
        //                }
        //                void WriteAndChangeLineB()
        //                {
        //                    sw.WriteLine(lineB);
        //                    lineB = srB.ReadLine();
        //                    if (lineB == null) return;
        //                    b = GetKeyAttribute(lineB, keyAttrIndex);
        //                }
        //            }
                    
        //        }
        //    }
        //}
        //private  void Compare2Elements(StreamReader srA, StreamReader srB, StreamWriter sw, int attributeIndex)
        //{
        //    var lineA = srA.ToString();
        //    var lineB = srB.ToString();
        //    var a = GetKeyAttribute(lineA, attributeIndex);
        //    var b = GetKeyAttribute(lineB, attributeIndex);

        //    if (a < b)
        //    {
        //        sw.WriteLine(lineA);
        //        sw.WriteLine(lineB);
        //    }
        //    else
        //    {
        //        sw.WriteLine(lineB);
        //        sw.WriteLine(lineA);
        //    }
        //}

        private string GetAttributeNames()
        {
            using (StreamReader sr = new StreamReader(Path))
            {
                return sr.ReadLine();
            }
        }
        private  long GetKeyAttribute(string line, int attributeIndex)
        {
            var attributes = line.Trim().Split(';');
            try
            {
                long res;
                res = Int64.Parse(attributes[attributeIndex]);
                return res;
            } catch (InvalidCastException)
            {
                Console.WriteLine("не получилось привести ключевой атрибут из строки таблицы к числу");
            } 

            return -1L;
        }
        private long GetLinesCount(string path)
        {
            var linesCount = 1;
            int nextLine = '\n';
            using (var streamReader = new StreamReader(
                new BufferedStream(
                    File.OpenRead(path), 10 * 1024 * 1024))) // буфер в 10 мегабайт
            {
                while (!streamReader.EndOfStream)
                {
                    if (streamReader.Read() == nextLine) linesCount++;
                }
            }

            return linesCount;
        }
    }
}

