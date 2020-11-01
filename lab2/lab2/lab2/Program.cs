using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Numerics;

namespace lab2
{
    class Program
    {
        static void QuadraticEquation(double a, double b, double c)
        {
            if (a != 0)
            {
                double delta = b * b - 4 * a * c;
                if (delta > 0)
                {
                    double sqrtDelta = Math.Sqrt(delta);
                    double x1 = (-b - sqrtDelta) / (2 * a);
                    double x2 = (-b + sqrtDelta) / (2 * a);
                    Console.WriteLine($"Rozwiazaniami rownania sa: x1 = {x1:G5}  oraz x2 = {x2:G5}");
                }
                else if (delta == 0)
                {
                    double x = (-1) * b / (2 * a);
                    Console.WriteLine("Rozwiazanie rownania to: x = {0:G5}", x);
                }
                else Console.WriteLine("Brak rozwiazan w zbiorze liczb rzeczywistych.");
            }
            else
            {
                if (b != 0)
                {
                    double x = (-c) / b;
                    Console.WriteLine("Rozwiazanie rownania to: x = {0:G5}", x);
                }
                else if (c == 0)
                {
                    Console.WriteLine("Rozwiazanie to zbior liczb rzeczywistych. ");
                }
                else Console.WriteLine("Brak rozwiazan.");
            }
        }
        static void Task1()
        {
            Console.WriteLine("Rozwiazanie rownania kwadratowego.");

            Console.Write("Podaj a: ");
            double a = Convert.ToDouble(Console.ReadLine());

            Console.Write("Podaj b: ");
            double b = Convert.ToDouble(Console.ReadLine());

            Console.Write("Podaj c: ");
            double c = Convert.ToDouble(Console.ReadLine());

            QuadraticEquation(a, b, c);
        }
        static void BinaryAndHexRepresentation(int a, int b)
        {
            Console.WriteLine($"A = {a}, binarna reprezentacja to: {Convert.ToString(a, 2)} \n" +
                $"B = {b}, binarna reprezentacja to: {Convert.ToString(b, 2)} \n" +
                "Heksadecymalna reprezentacja A i B: \n" +
                $"A = {a:X} Negacja A = {~a:X} \n" +
                $"B = {b:X} Negacja B = {~b:X} \n" +
                $"Koniunkcja A i B = {b & a:X} \n" +
                $"Alternatywa A lub B = {a | b:X}");
        }
        static void Task2()
        {
            Console.WriteLine("Binarna i heksadecymalna reprezentacja liczb.");
            Console.Write("Podaj A: ");
            int a = Convert.ToInt32(Console.ReadLine());

            Console.Write("Podaj B: ");
            int b = Convert.ToInt32(Console.ReadLine());

            BinaryAndHexRepresentation(a, b);
        }

        static int firstNumber = 0;
        static int largest = 0;
        static int secondLargest = 0;
        static int sameNumbersCounter = 0;
        static void CompareNumbers(int currentNumber, int counter)
        {
            if (counter == 0)
            {
                firstNumber = currentNumber;
                sameNumbersCounter++;
                largest = firstNumber;
                secondLargest = firstNumber;
            }
            else
            {
                if (currentNumber == firstNumber) sameNumbersCounter++;
                if (currentNumber > largest)
                {
                    secondLargest = largest;
                    largest = currentNumber;
                }
                else
                {
                    if (currentNumber > secondLargest && currentNumber != largest || largest == secondLargest) secondLargest = currentNumber;
                }
            }
        }
        static void FindSecondLargest(int n)
        {
            int counter = 0;
            string numbers;
            int startIndex;

            if (n <= 1)
            {
                Console.WriteLine("Brak rozwiazania.");
                return;
            }

            while (counter != n)
            {
                numbers = Console.ReadLine();
                startIndex = 0;

                for (int i = 0; i < numbers.Length; i++)
                {
                    if (numbers[i] == '\t' || numbers[i] == ' ' || numbers[i] == '\n')
                    {
                        CompareNumbers(Convert.ToInt32(numbers.Substring(startIndex, i - startIndex)), counter);

                        startIndex = i + 1;
                        counter++;
                    }
                }
                string lastNumber = numbers.Substring(startIndex);
                if (lastNumber != "")
                {
                    CompareNumbers(Convert.ToInt32(lastNumber), counter);
                    counter++;
                }
            }
            if (sameNumbersCounter == n) Console.WriteLine("Brak rozwiazania.");
            else Console.WriteLine($"Druga najwieksza liczba to: {secondLargest}");
        }
        static void Task3()
        {
            Console.Write("Znajdowanie drugiej najwiekszej liczby. ");
            Console.Write("Podaj liczbe liczb n: ");
            int n = Convert.ToInt32(Console.ReadLine());
            FindSecondLargest(n);
        }
        static void Main(string[] args)
        {
            //Task1();
            //Task2();
            //Task3();
        }
    }
}
