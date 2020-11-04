using System;

namespace lab4
{
    class Program
    {
        static (int firstNumber, int secondNumber) GetFromConsoleXY(string firstComment, string secondComment)
        {
            Console.WriteLine(firstComment);
            int firstNumber = Convert.ToInt32(Console.ReadLine());
            Console.WriteLine(secondComment);
            int secondNumber = Convert.ToInt32(Console.ReadLine());

            return (firstNumber, secondNumber);
        }

        static void GetFromConsoleXY(string firstComment, string secondComment, out int firstNumber, out int secondNumber)
        {
            Console.WriteLine(firstComment);
            firstNumber = Convert.ToInt32(Console.ReadLine());
            Console.WriteLine(secondComment);
            secondNumber = Convert.ToInt32(Console.ReadLine());
        }

        static void Task1()
        {
#if DEBUG
            Console.WriteLine("Creating tuple in Task 1");
#endif
            var tuple = GetFromConsoleXY("Joanna", "Sroka");
#if DEBUG
            Console.WriteLine("Return tuple: ");
#endif
            Console.WriteLine($"{tuple.firstNumber}, {tuple.secondNumber}");

            int firstNumber;
            int secondNumber;
            GetFromConsoleXY("Joanna", "Sroka", out firstNumber, out secondNumber);
#if DEBUG
            Console.WriteLine("Void method with 2 parameters: ");
#endif
            Console.WriteLine($"{firstNumber}, {secondNumber}");
        }

        static void DrawSthSeveralTimes(char sign, int number)
        {
            for (int i = 0; i < number; i++)
            {
                Console.Write(sign);
            }
        }

        static void DrawCard(string firstLine, string secondLine = "Nowak", char sign = '*', int frameWidth = 3, int cardWidth = 25)
        {
#if DEBUG
            Console.WriteLine("Drawing card in task 2");
#endif

            if (cardWidth < (2 * frameWidth + firstLine.Length) || cardWidth < (2 * frameWidth + secondLine.Length))
            {
#if DEBUG
                Console.WriteLine("Too small width of a card.");
#endif
                return;
            }
            for (int i = 0; i < frameWidth; i++)
            {
                DrawSthSeveralTimes(sign, cardWidth);
                Console.WriteLine();
            }
            int space = cardWidth - 2 * frameWidth - firstLine.Length;
            int leftSpace = space / 2;
            int rightSpace = space - leftSpace;

            DrawSthSeveralTimes(sign, frameWidth);
            DrawSthSeveralTimes(' ', leftSpace);
            Console.Write(firstLine);
            DrawSthSeveralTimes(' ', rightSpace);
            DrawSthSeveralTimes(sign, frameWidth);
            Console.WriteLine();

            space = cardWidth - 2 * frameWidth - secondLine.Length;
            leftSpace = space / 2;
            rightSpace = space - leftSpace;

            DrawSthSeveralTimes(sign, frameWidth);
            DrawSthSeveralTimes(' ', leftSpace);
            Console.Write(secondLine);
            DrawSthSeveralTimes(' ', rightSpace);
            DrawSthSeveralTimes(sign, frameWidth);
            Console.WriteLine();

            for (int i = 0; i < frameWidth; i++)
            {
                DrawSthSeveralTimes(sign, cardWidth);
                Console.WriteLine();
            }
        }

        static void Task2()
        {
            //DrawCard("Ryszard", "Rys", 'X', 2, 20);
            //DrawCard("Ryszard");
            //DrawCard("Ryszard", "Rys", 'X', 2);

            //DrawCard("Ryszard", frameWidth: 2, secondLine: "Rys");
            //DrawCard("Ryszard", "Rys", cardWidth: 20, sign: 'X');
            //DrawCard(cardWidth: 20, secondLine: "Rys", frameWidth: 2, firstLine: "Ryszard", sign: 'X');
            DrawCard(sign: 'o', firstLine: "Ryszard");
            DrawCard(secondLine: "Rys", sign: 'v', firstLine: "Ryszard", cardWidth: 2);
        }

        static (int, int, int, int) CountMyTypes(params object[] tab)
        {
#if DEBUG
            Console.WriteLine("Counting types in Task 3");
#endif
            int evenIntegers = 0;
            int positiveRealNumbers = 0;
            int atLeast5Characters = 0;
            int others = 0;

            foreach (var value in tab)
            {
                switch (value)
                {
                    case int evenInt when evenInt % 2 == 0:
                    case byte evenByte when evenByte % 2 == 0:
                    case long evenLong when evenLong % 2 == 0:
                    case short evenShort when evenShort % 2 == 0:
                        // sbyte, ushort, uint, ulong
#if DEBUG
                        Console.WriteLine("It's even integer.");
#endif
                        evenIntegers++;
                        break;
                    case double posReal when posReal > 0:
                    case float posFloat when posFloat > 0:
                    case decimal posDecimal when posDecimal > 0:
#if DEBUG
                        Console.WriteLine("It's positive real.");
#endif
                        positiveRealNumbers++;
                        break;
                    case string text when text.Length >= 5:
#if DEBUG
                        Console.WriteLine("It's text with at least 5 characters.");
#endif
                        atLeast5Characters++;
                        break;
                    default:
#if DEBUG
                        Console.WriteLine("It's other type.");
#endif
                        others++;
                        break;
                }
            }
            return (evenIntegers, positiveRealNumbers, atLeast5Characters, others);
        }

        static void Task3()
        {
            long number = 24;
            (int, int, int, int) result = CountMyTypes("Dluzszy napis", -13.24, "Kot", 13.24, number, "Ala", -120);

            Console.WriteLine($"Even integers = {result.Item1}, positive real = {result.Item2}," +
                $" at least 5 char = {result.Item3}, others = {result.Item4}");
        }

        static void Main(string[] args)
        {
            Task1();
            //Task2();
            //Task3();
        }
    }
}
