using System;
using System.Collections.Generic;
using System.Linq;

namespace lab3
{
    public class ReverseComparer : IComparer<string>
    {
        public int Compare(string x, string y)
        {
            // Compare y and x in reverse order.
            return y.CompareTo(x);
        }
    }

    class Program
    {
        static Random random = new Random();
        static int[,] CreateTwoDimensionalArray(int n, int m)
        {
            int[,] array = new int[n, m];
            for (int i = 0; i < n; i++)
            {
                for (int j = 0; j < m; j++)
                {
                    array[i, j] = random.Next(1, 100);
                }
            }
            return array;
        }
        static void SwapLines<T>(ref T[,] array, int firstIndex, int secondIndex)
        {
            for (int i = 0; i < array.GetLength(1); i++)
            {
                T temp = array[firstIndex, i];
                array[firstIndex, i] = array[secondIndex, i];
                array[secondIndex, i] = temp;
            }
        }
        static void ReverseTwoDimensionalArray(int[,] array)
        {
            for (int i = 0; i < array.GetLength(0) / 2; i++)
            {
                SwapLines<int>(ref array, i, array.GetLength(0) - 1 - i);
            }
        }

        static void PrintTwoDimensionalArray(int[,] array)
        {
            for (int i = 0; i < array.GetLength(0); i++)
            {
                for (int j = 0; j < array.GetLength(1); j++)
                {
                    Console.Write($"{array[i, j]} ");
                }
                Console.WriteLine();
            }
        }
        static int[][] CreateArrayOfArrays(int n, int m)
        {
            int[][] array = new int[n][];

            for (int i = 0; i < n; i++)
            {
                array[i] = new int[m];
                for (int j = 0; j < m; j++)
                {
                    array[i][j] = random.Next(1, 100);
                }
            }
            return array;
        }

        static void ReverseArrayOfArrays(int[][] array)
        {
            if (array != null) Array.Reverse(array);
        }

        static void PrintArrayOfArrays(int[][] array)
        {
            if (array != null)
            {
                for (int i = 0; i < array.Length; i++)
                {
                    for (int j = 0; j < array[i].Length; j++)
                    {
                        Console.Write($"{array[i][j]} ");
                    }
                    Console.WriteLine();
                }
            }
        }

        static void Task1()
        {
            int[,] array2Dim = CreateTwoDimensionalArray(5, 3);
            PrintTwoDimensionalArray(array2Dim);
            Console.WriteLine();
            ReverseTwoDimensionalArray(array2Dim);
            PrintTwoDimensionalArray(array2Dim);
            Console.WriteLine();

            int[][] arrayOfArrays = CreateArrayOfArrays(4, 3);
            PrintArrayOfArrays(arrayOfArrays);
            Console.WriteLine();
            ReverseArrayOfArrays(arrayOfArrays);
            PrintArrayOfArrays(arrayOfArrays);
        }

        static (string, string, int, double) CreateTuple(string name, string surname, int age, double salary)
        {
            (string name, string surname, int age, double salary) tuple = (name, surname, age, salary);
            return tuple;
        }

        static void PrintTuple3Ways((string name, string surname, int age, double salary) tuple)
        {
            Console.WriteLine($"1. Name: {tuple.Item1} Surname: {tuple.Item2} Age: {tuple.Item3} Salary: {tuple.Item4}");
            Console.WriteLine($"2. Name: {tuple.name} Surname: {tuple.surname} Age: {tuple.age} Salary: {tuple.salary}");

            var (name, surname, age, salary) = tuple;
            Console.WriteLine($"3. Name: {name} Surname: {surname} Age: {age} Salary: {salary}");
            Console.WriteLine($"4. {tuple}");
        }

        static void Task2()
        {
            PrintTuple3Ways(CreateTuple("Joanna", "Sroka", 22, -42.01));
        }

        static void Task3()
        {
            int @class = 12;
            Console.WriteLine(@class);
        }

        static void Task4()
        {
            string[] source = { "Adam", "Piotr", "Zuzanna" };
            string[] dest = { "Zofia", "Edward", "Dominika", "Andrzej" };
            System.Array.Copy(source, dest, 2);
            System.Array.ForEach(dest, item => Console.Write($"{item} "));

            Console.WriteLine();
            Console.WriteLine(System.Array.Exists(dest, item => item.StartsWith("A")));
            Console.WriteLine(System.Array.Find(dest, item => item.StartsWith("A")));

            string[] matchedItems = System.Array.FindAll(dest, item => item.StartsWith("A"));
            System.Array.ForEach(matchedItems, item => Console.Write($"{item} "));
            Console.WriteLine();

            System.Array.Sort(dest, 0, 3, new ReverseComparer());
            System.Array.ForEach(dest, item => Console.Write($"{item} "));

            Console.WriteLine();
            System.Array.Sort(dest);
            System.Array.ForEach(dest, item => Console.Write($"{item} "));
            Console.WriteLine();
            Console.WriteLine(System.Array.BinarySearch(dest, "Dominika"));
        }

        static void PrintAnonymouType(dynamic anonym)
        {
            Console.WriteLine($"1. Name: {anonym.name} Surname: {anonym.surname} Age: {anonym.age} Salary: {anonym.salary}");
            Console.WriteLine($"2. {anonym}");
        }

        static void Task5()
        {
            var anonym = new { name = "Joanna", surname = "Sroka", age = 22, salary = -42.01 };
            PrintAnonymouType(anonym);
        }

        static void Main(string[] args)
        {
            //Task1();
            //Task2();
            //Task3();
            //Task4();
            //Task5();
        }
    }
}
