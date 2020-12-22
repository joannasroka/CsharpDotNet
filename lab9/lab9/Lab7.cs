using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text.RegularExpressions;

namespace lab7
{
    class Program
    {
        static void OpenFileWithoutStreamReader(string fileName)
        {
            var reader = new StreamReader(fileName);
            CountWords(reader);

        }
        static void OpenFileWithStreamReader(StreamReader streamReader)
        {
            CountWords(streamReader);
        }

        static void CountWords(StreamReader reader)
        {
            Regex delimiterChars = new Regex(@"[^a-zA-Z]");
            string[] words = delimiterChars.Split(reader.ReadToEnd());

            Dictionary<string, int> dictionary = new Dictionary<string, int>();

            for (int i = 0; i < words.Length; i++)
            {
                if (words[i].Length > 0)
                {
                    words[i] = words[i].ToLower();

                    if (dictionary.ContainsKey(words[i]))
                    {
                        dictionary[words[i]] += 1;
                    }
                    else
                    {
                        dictionary.Add(words[i], 1);
                    }
                }
            }

            List<KeyValuePair<string, int>> dicAfterSort = dictionary.ToList();
            dicAfterSort.Sort((pair1, pair2) => pair2.Value.CompareTo(pair1.Value));
            int howManyShow = 10;

            foreach (KeyValuePair<string, int> elem in dicAfterSort)
            {
                if (howManyShow > 0)
                {
                    Console.WriteLine($"Word = {elem.Key}, Counter = {elem.Value}");
                    howManyShow--;
                }

            }

        }

        static void Task1()
        {
            Console.WriteLine("Podaj nazwe pliku: ");
            string fileName = Console.ReadLine();
            OpenFileWithoutStreamReader(fileName);
        }

        static void Task2()
        {
            Console.WriteLine("Podaj nazwe pliku: ");
            string fileName = Console.ReadLine();

            try
            {
                OpenFileWithoutStreamReader(fileName);
            }

            catch (Exception exc) when (exc is FileNotFoundException || exc is DirectoryNotFoundException)
            {
                Console.WriteLine("Nie mozna znalezc pliku o tej nazwie.");
            }

            catch (IOException exc)
            {
                Console.WriteLine("Nieprawidlowa skladnia nazwy pliku.");
            }
            catch (Exception exc) when (exc is ArgumentException || exc is ArgumentNullException)
            {
                Console.WriteLine("Nazwa pliku nie moze byc pusta.");
            }
            catch (UnauthorizedAccessException exc)
            {
                Console.WriteLine("Odmowa dostepu do pliku.");
            }

        }

        static void Task3()
        {
            Console.WriteLine("Podaj nazwe pliku: ");
            string fileName = Console.ReadLine();
            if (fileName == "" || fileName == null)
            {
                Console.WriteLine("Nazwa pliku nie moze byc pusta.");
            }
            else if (!File.Exists(fileName))
            {
                Console.WriteLine("Nie mozna znalezc pliku o tej nazwie.");
            }
            else
            {
                try
                {
                    using (StreamReader reader = new StreamReader(fileName))
                    {
                        OpenFileWithStreamReader(reader);
                    }
                }
                catch (IOException exc)
                {
                    Console.WriteLine("Nieprawidlowa skladnia nazwy pliku.");
                }

            }

        }

        static void MainLab7(string[] args)
        {
            //Task1();
            //Task2();
            //Task3();
        }
    }
}
