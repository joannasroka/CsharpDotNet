using System;
using System.Linq;
using System.Collections.Generic;
using LinqExamples;
using lab6;
using System.Reflection;
using System.IO;
using System.Text.RegularExpressions;


namespace lab9
{
    class Program
    {
        const int NUMBER_OF_MOST_COMMON_WORDS = 10;
        static readonly List<Student> students = Generator.GenerateStudentsEasy();

        static void CountWords(StreamReader reader)
        {
            string text = reader.ReadToEnd().ToLower();
            var matchingChars = new Regex("[a-z]+");

            List<(string, int)> mostCommon = matchingChars.Matches(text)
                .GroupBy(match => match.Value)
                .Select(group => (group.Key, group.Count()))
                .OrderByDescending(group => group.Item2)
                .Take(NUMBER_OF_MOST_COMMON_WORDS).ToList();

            for (int i = 0; i < mostCommon.Count; i++)
            {
                var (word, count) = mostCommon[i];
                Console.WriteLine($"Word = {word} Counter = {count}");
            }

        }

        static void OpenFileWithStreamReader(StreamReader streamReader)
        {
            CountWords(streamReader);
        }
        static void OpenFile()
        {
            string fileName = "test1.txt";
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

        static void Task1()
        {
            OpenFile();
        }

        static void GroupByNameId(int n)
        {

            if (n >= 0)
            {
                var resStud = students
                     .OrderBy(s => s.Name)
                     .ThenBy(s => s.Index)
                     .Select((s, index) => new { Student = s, Index = index })
                     .GroupBy(item => item.Index / n)
                     .Select(group => group.Select(item => item.Student))
                     .ToList();

                for (int i = 0; i < resStud.Count; i++)
                {
                    Console.WriteLine($"Group number: {i}");
                    foreach (var stud in resStud[i])
                    {
                        Console.WriteLine($"{stud}");
                    }
                }
            }
        }

        static void Task2()
        {
            GroupByNameId(5);
        }

        static void SortTopicsByFrequency()
        {
            var resStud = students
               .SelectMany(s => s.Topics)
               .GroupBy(topic => topic)
               .Select(group => new { Topic = group.Key, Count = group.Count() })
               .OrderByDescending(group => group.Count)
               .ToList();

            resStud.ForEach(Console.WriteLine);

        }

        static void SortTopicsByFrequencyGender()
        {
            var resStud = students
               .SelectMany(s => s.Topics, (stud, topic) => new { Gender = stud.Gender, Topic = topic })
               .GroupBy(info => new { info.Gender, info.Topic })
               .Select(group => new { Group = group.Key, Count = group.Count() })
               .OrderBy(group => group.Group.Gender)
               .ThenByDescending(group => group.Count)
               .ToList();

            resStud.ForEach(Console.WriteLine);
        }

        public static void Task3()
        {
            Console.WriteLine("Tematy posortowane wg czestosci.");
            SortTopicsByFrequency();

            Console.WriteLine("Tematy posortowane wg plci i czestosci.");
            SortTopicsByFrequencyGender();
        }

        public static List<Topic> GenerateTopics()
        {
            return new List<Topic>() {
            new Topic(1,"C#"),
            new Topic(2,"PHP"),
            new Topic(3,"algorithms"),
            new Topic(4,"C++"),
            new Topic(5,"fuzzy logic"),
            new Topic(6,"Basic"),
            new Topic(7,"Java"),
            new Topic(8,"web programming"),
            new Topic(9,"JavaScript"),
            new Topic(10,"neural networks")
            };
        }

        public static List<Topic> GenerateTopicsQuery()
        {
            var topics = students
               .SelectMany(student => student.Topics).Distinct()
               .OrderBy(topic => topic)
               .Select((topic, index) => new Topic(index + 1, topic))
               .ToList();

            return topics;
        }


        static void StudentsToStudentsWithTopic()
        {
            List<Topic> top = GenerateTopicsQuery();

            var resStud = students
                .Select(stud => new StudentWithTopic()
                {
                    Id = stud.Id,
                    Index = stud.Index,
                    Name = stud.Name,
                    Gender = stud.Gender,
                    Active = stud.Active,
                    DepartmentId = stud.DepartmentId,
                    Topics = stud.Topics
                    .SelectMany(t => top.Where(i => i.Name.Equals(t))
                    .Select(i => i.Id))
                    .ToList()
                });

            resStud.ToList().ForEach(Console.WriteLine);

        }

        static void StudentsToStudentsWithTopicAllInOne()
        {
            var resStud = students
                .Select(stud => new StudentWithTopic()
                {
                    Id = stud.Id,
                    Index = stud.Index,
                    Name = stud.Name,
                    Gender = stud.Gender,
                    Active = stud.Active,
                    DepartmentId = stud.DepartmentId,
                    Topics = stud.Topics
                    .SelectMany(t => (students
                        .SelectMany(student => student.Topics).Distinct()
                        .OrderBy(topic => topic)
                        .Select((topic, index) => new Topic(index + 1, topic))
                        .ToList())
                        .Where(i => i.Name.Equals(t))
                    .Select(i => i.Id))
                    .ToList()
                });

            resStud.ToList().ForEach(Console.WriteLine);

        }


        static void Task4()
        {
            StudentsToStudentsWithTopic();

            Console.WriteLine("Generacja listy tematow i listy nowego typu studentow w jednym zapytaniu: ");
            StudentsToStudentsWithTopicAllInOne();
        }

        static void Task5()
        {
            Vehicle[] array = {
                new Car(15300.0, 100, "Ford", "Fusion"),
                new Bicycle(100.0, 30, 15, false),
                new PassengerCar(3123.30, 150, "Fiat", "126p", "Red", 1995),
                new TruckCar(102000.0, 90, "Daf", "14X", 300, 6),
                new TruckCar(20000.0, 87, "Tir", "Extra", 200, 4)};

            int result = (int)typeof(Extra).GetMethod("TotalCapacity").Invoke(null, new object[] { array });
            Console.WriteLine($"Result = {result}");

        }

        static void Task5Reflection()
        {
            Type type = typeof(TruckCar);

            ConstructorInfo truck = type.GetConstructor(new[]
            { typeof(double),
            typeof(int),
            typeof(string),
            typeof(string),
            typeof(int),
            typeof(int)
            });
            TruckCar truck1 = (TruckCar)truck.Invoke(new object[] { 102000.0, 90, "Daf", "14X", 300, 6 });
            TruckCar truck2 = (TruckCar)truck.Invoke(new object[] { 20000.0, 87, "Tir", "Extra", 200, 4 });

            Vehicle[] array = { truck1, truck2 };

            int result = (int)typeof(Extra).GetMethod("TotalCapacity").Invoke(null, new object[] { array });
            Console.WriteLine($"Result = {result}");
        }

        static void Main(string[] args)
        {
            Task1();
            Task2();
            Task3();
            Task4();
            Task5();
            Task5Reflection();

        }
    }

}
