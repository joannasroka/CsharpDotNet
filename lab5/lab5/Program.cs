using System;
using System.Text;

namespace lab5
{
    class MixedNumber
    {
        private int nominator;
        private int denominator;
        public int TotalPart { get; set; }
        public int Nominator
        {
            private set
            {
                if (value < 0) nominator = -value;
                else nominator = value;

                SimplifyFraction();
            }
            get { return nominator; }
        }
        public int Denominator
        {
            private set
            {
                if (value == 0) throw new ArgumentException("Denominator can't be 0.");
                else if (value < 0) denominator = -value;
                else denominator = value;

                SimplifyFraction();
            }
            get { return denominator; }
        }

        public static int ChangesCounter
        {
            get;
            private set;
        }

        public double Fraction
        {
            get { return TotalPart + (Math.Sign(TotalPart) != 0 ? Math.Sign(TotalPart) : 1) * (double)nominator / denominator; }
        }

        public override string ToString()
        {
            return nominator == 0 ? TotalPart.ToString() :
                TotalPart == 0 ? $"{nominator}/{denominator}" : $"{TotalPart} & {nominator}/{denominator}";
        }
        public MixedNumber(int totalPart, int nominator, int denominator)
        {
            TotalPart = totalPart;
            this.denominator = denominator;
            this.nominator = nominator;

            SimplifyFraction();
        }
        public MixedNumber() : this(0, 0, 1) { }
        public MixedNumber(int totalPart) : this(totalPart, 0, 1) { }
        public MixedNumber(int nominator, int denominator) : this(0, nominator, denominator) { }

        private static int FindGreatestCommonDivisor(int a, int b)
        {
            int temp;

            while (b != 0)
            {
                temp = b;
                b = a % b;
                a = temp;
            }
            return a;
        }

        private static int FindLeastCommonMultiple(int a, int b)
        {
            return (a * b) / FindGreatestCommonDivisor(a, b);
        }
        private void SimplifyFraction()
        {
            bool modified = false;
            if (nominator >= denominator)
            {
                int howManyTotal = nominator / denominator;

                if (TotalPart >= 0) TotalPart += howManyTotal;
                else TotalPart -= howManyTotal;

                nominator -= howManyTotal * denominator;
                modified = true;
            }
            int greatestCommonDivisor = FindGreatestCommonDivisor(nominator, denominator);
            if (greatestCommonDivisor > 1)
            {
                nominator = nominator / greatestCommonDivisor;
                denominator = denominator / greatestCommonDivisor;
                modified = true;
            }
            if (modified) ChangesCounter++;

        }

        public static MixedNumber operator +(MixedNumber a, MixedNumber b)
        {
            MixedNumber result = new MixedNumber();
            result.TotalPart = a.TotalPart + b.TotalPart;
            int aNom = a.Nominator;
            int bNom = b.Nominator;
            int aDenom = a.Denominator;
            int bDenom = b.Denominator;

            int leastCommonMultiple = FindLeastCommonMultiple(a.Denominator, b.Denominator);

            int aSign = a.TotalPart < 0 ? -1 : 1;
            int bSign = b.TotalPart < 0 ? -1 : 1;

            if (a.Nominator == 0)
            {
                result.TotalPart = aSign == 1 ? result.TotalPart - 1 : result.TotalPart + 1;
                aNom = leastCommonMultiple;
                aDenom = leastCommonMultiple;
            }
            if (b.Nominator == 0)
            {
                result.TotalPart = bSign == 1 ? result.TotalPart - 1 : result.TotalPart + 1;
                bNom = leastCommonMultiple;
                bDenom = leastCommonMultiple;
            }

            int aMultiplier = leastCommonMultiple / aDenom;
            int bMultiplier = leastCommonMultiple / bDenom;

            result.Nominator = Math.Abs(aSign * aNom * aMultiplier + bSign * bNom * bMultiplier);
            result.Denominator = leastCommonMultiple;

            result.SimplifyFraction();
            return result;
        }
    }

    static class StringExtension
    {
        public static string ChangeString(this string str)
        {
            StringBuilder result = new StringBuilder("");
            for (int i = 0; i < str.Length; i++)
            {
                if (!Char.IsLetter(str[i])) result.Append('.');
                else
                {
                    if (i % 2 == 0) result.Append(Char.ToUpper(str[i]));
                    else result.Append(Char.ToLower(str[i]));
                }
            }
            return result.ToString();
        }

    }

    class Program
    {
        static void Task1()
        {
            MixedNumber[] mixNumbers =
            {new MixedNumber(),
            new MixedNumber(-3),
            new MixedNumber(2, 6),
                new MixedNumber(-3, 7, 12),
            new MixedNumber(0, 5, 15),
            new MixedNumber(3, 7, 12),
            new MixedNumber(3, 20, 6),
            new MixedNumber(-3, 20, 6)
            };

            foreach (MixedNumber number in mixNumbers)
            {
                Console.WriteLine(number.ToString());
            }
            Console.WriteLine();

            MixedNumber m1 = new MixedNumber(-1, 1, 3);
            MixedNumber m2 = new MixedNumber(1, 2, 3);
            MixedNumber m3 = m1 + m2;
            Console.WriteLine(m3.ToString());

            MixedNumber m4 = new MixedNumber(-3);
            MixedNumber m5 = new MixedNumber(-1, 2, 3);
            MixedNumber m6 = m4 + m5;
            Console.WriteLine(m6.ToString());

            MixedNumber m7 = new MixedNumber(-2, 2, 8);
            double numberDouble = m7.Fraction;
            Console.WriteLine(numberDouble.ToString());

            Console.WriteLine(MixedNumber.ChangesCounter);
        }

        static void Task2()
        {
            string str = "Ala ma 12 kotow!!!";
            Console.WriteLine(str);
            Console.WriteLine(str.ChangeString());
        }
        static void Main(string[] args)
        {
            Task1();
            Task2();
        }
    }
}
