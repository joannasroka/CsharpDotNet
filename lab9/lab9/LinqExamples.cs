﻿using System;
using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;
using System.Linq;

namespace LinqExamples
{


    public class Department
    {
        public int Id { get; set; }
        public String Name { get; set; }

        public Department(int id, string name)
        {
            this.Id = id;
            this.Name = name;
        }

        public override string ToString()
        {
            return $"{Id,2}), {Name,11}";
        }

    }

    public enum Gender
    {
        Female,
        Male
    }

    public class Student
    {
        public int Id { get; set; }
        public int Index { get; set; }
        public string Name { get; set; }
        public Gender Gender {get;set; }
        public bool Active { get; set; }
        public int  DepartmentId { get; set; }

        public List<string> Topics { get; set; }
        public Student(int id, int index, string name, Gender gender,bool active, 
            int departmentId,List<string> topics)
        {
            this.Id = id;
            this.Index = index;
            this.Name = name;
            this.Gender = gender;
            this.Active = active;
            this.DepartmentId = departmentId;
            this.Topics = topics;
        }

        public override string ToString()
        {
            var result = $"{Id,2}) {Index,5}, {Name,11}, {Gender,6},{(Active ? "active" : "no active"),9},{DepartmentId,2}, topics: ";
            foreach (var str in Topics)
                result += str + ", ";
            return result;
        }
    }

    public static class Generator
    {
        public static int[] GenerateIntsEasy()
        {
            return new int[] { 5, 3, 9, 7, 1, 2, 6, 7, 8 };
        }

        public static int[] GenerateIntsMany()
        {
            var result = new int[10000];
            Random random = new Random();
            for (int i = 0; i < result.Length; i++)
                result[i] = random.Next(1000);
            return result;
        }

        public static List<string> GenerateStringsEasy()
        {
            return new List<string>() {
                "Nowak",
                "Kowalski",
                "Schmidt",
                "Newman",
                "Bandingo",
                "Miniwiliger"
            };
        }
        public static List<Student> GenerateStudentsEasy()
        {
            return new List<Student>() {
            new Student(1,12345,"Nowak", Gender.Female,true,1,
                    new List<string>{"C#","PHP","algorithms"}),
            new Student(2, 13235, "Kowalski", Gender.Male, true,1,
                    new List<string>{"C#","C++","fuzzy logic"}),
            new Student(3, 13444, "Schmidt", Gender.Male, false,2,
                    new List<string>{"Basic","Java"}),
            new Student(4, 14000, "Newman", Gender.Female, false,3,
                    new List<string>{"JavaScript","neural networks"}),
            new Student(5, 14001, "Bandingo", Gender.Male, true,3,
                    new List<string>{"Java","C#"}),
            new Student(6, 14100, "Miniwiliger", Gender.Male, true,2,
                    new List<string>{"algorithms","web programming"}),
            new Student(11,22345,"Nowak", Gender.Female,true,2,
                    new List<string>{"C#","PHP","algorithms"}),
            new Student(12, 23235, "Nowak", Gender.Male, true,1,
                    new List<string>{"C#","C++","fuzzy logic"}),
            new Student(13, 23444, "Schmidt", Gender.Male, false,1,
                    new List<string>{"Basic","Java"}),
            new Student(14, 24000, "Newman", Gender.Female, false,1,
                    new List<string>{"JavaScript","neural networks"}),
            new Student(15, 24001, "Bandingo", Gender.Male, true,3,
                    new List<string>{"Java","C#"}),
            new Student(16, 24100, "Bandingo", Gender.Male, true,2,
                    new List<string>{"algorithms","web programming"}),
            };
        }

        public static List<Department> GenerateDepartmentsEasy()
        {
            return new List<Department>() {
            new Department(1,"Computer Science"),
            new Department(2,"Electronics"),
            new Department(3,"Mathematics"),
            new Department(4,"Mechanics")
            };
        }

    }
    class Program
    {

        public static void ShowAllCollections()
        {
            Generator.GenerateIntsEasy().ToList().ForEach(Console.WriteLine);
            Generator.GenerateDepartmentsEasy().ForEach(Console.WriteLine);
            Generator.GenerateStudentsEasy().ForEach(Console.WriteLine);
        }

        public static void MethodWhereSimple()
        {
            var resInt = Generator.GenerateIntsEasy().Where(x => x % 2 == 0);
            resInt.ToList().ForEach(Console.WriteLine);
            var resStr = Generator.GenerateStringsEasy().Where(s => s.Length>6);
            resStr.ToList().ForEach(Console.WriteLine);
            var resStud = Generator.GenerateStudentsEasy().Where(s => s.Active && s.DepartmentId==1);
            resStud.ToList().ForEach(Console.WriteLine);
        }
        public static void ClauseWhereSimple()
        {
            var resInt = from v in Generator.GenerateIntsEasy()
                         where v % 2 == 0
                         select v;
            resInt.ToList().ForEach(Console.WriteLine);
            var resStr = from s in Generator.GenerateStringsEasy()
                         where s.Length > 6
                         select s;
            resStr.ToList().ForEach(Console.WriteLine);
            var resStud = from s in Generator.GenerateStudentsEasy()
                          where s.Active && s.DepartmentId == 1
                          select s;
            resStud.ToList().ForEach(Console.WriteLine);
        }

        public static void SimpleAggregiates()
        {
            var ints = Generator.GenerateIntsEasy();
            var resMin = ints.Where(x => x % 2 == 0).Min();
            Console.WriteLine(resMin);
            var resMax = ints.Where(x => x % 2 == 0).Max();
            Console.WriteLine(resMin);
            var strs = Generator.GenerateStringsEasy();
            var resStrMin1 = strs.Min(); //dictionaryOrder
            Console.WriteLine(resStrMin1);
            var resStrMin2 = strs.Min(s=>s.Length); // minimum length
            Console.WriteLine(resStrMin2);
        }

        public static void ComplexAggregiates()
        {
            var strs = Generator.GenerateStringsEasy();
            //var strs = new string[] { };
            //var strs = new string[] { "one" };
            //var resStr = strs.Aggregate((a, b) => a + "," + b);
            //var resStr = strs.Aggregate("", (a, b) => a + "," + b);
            //var resStr = strs.Aggregate("",(a, b) => a + "," + b, res=>res.Length);
            var resStr = strs.Aggregate((0,""),
                (tuple, b) => (tuple.Item1 + 1,tuple.Item2 + b), res=>((double)res.Item2.Length)/res.Item1);
            //var avrLen= strs.Average(s => s.Length);
            //Console.WriteLine(avrLen);
            Console.WriteLine(resStr);
        }

        public static void WhereWithPos()
        {
            var resStr = Generator.GenerateStringsEasy()
                .Where((s,pos) => pos % 2==0);
            resStr.ToList().ForEach(Console.WriteLine);
            var resStud = Generator.GenerateStudentsEasy()
                .Where((s, pos) => s.Active && pos % 3 == 0 );
            resStud.ToList().ForEach(Console.WriteLine);
            Console.WriteLine("--------");
            var resStud2 = Generator.GenerateStudentsEasy()
                .Where(s=>s.Active)               
                .Where((s, pos) => pos % 3 == 0);
            resStud2.ToList().ForEach(Console.WriteLine);
        }

        public static void TestSelect()
        {
            var resStud = Generator.GenerateStudentsEasy()
                .Where(s => s.Index < 20000)
                .Select(s => new { Header=s.Id + ") "+s.Index, s.Name });
            foreach(var x in resStud)
            {
                Console.WriteLine($" {x.Header} =====> {x.Name}");
            }
            Console.WriteLine("-------------");
            var resStud2 = from s in Generator.GenerateStudentsEasy()
                           where s.Index < 20000
                           select new { Header = s.Id + ") " + s.Index, s.Name };
            foreach (var x in resStud2)
            {
                Console.WriteLine($" {x.Header} =====> {x.Name}");
            }
            Console.WriteLine("-------------");
            var resStud3 = from s in Generator.GenerateStudentsEasy()
                           where s.Index < 20000
                           select (Header : s.Id + ") " + s.Index, s.Name);
            foreach (var x in resStud3)
            {
                Console.WriteLine($" {x.Header} =====> {x.Name}");
            }
        }

        public static void TestSelectMany()
        {
            var resStud = Generator.GenerateStudentsEasy()
                .Where(s => s.Index < 20000)
                .SelectMany(s => s.Topics);
            resStud.ToList().ForEach(x => Console.Write(x + ";"));
            Console.WriteLine();
            Console.WriteLine(resStud.Count());
            var resChars = resStud
                .SelectMany(s => s);
            resChars.ToList().ForEach(x => Console.Write(x + ";"));
            Console.WriteLine();
            Console.WriteLine(resChars.Count());
        }

        public static void TestSelectManyQuery()
        {
            var resStud = from s in Generator.GenerateStudentsEasy()
                          where s.Index < 20000
                          from topic in s.Topics
                          select topic;
            resStud.ToList().ForEach(x => Console.Write(x + ";"));
            Console.WriteLine();
            Console.WriteLine(resStud.Count());
            var resChars = from s in resStud
                           from c in s
                           select c;
            resChars.ToList().ForEach(x => Console.Write(x + ";"));
            Console.WriteLine();
            Console.WriteLine(resChars.Count());
        }

        public static void TestSelectManyWith2Lambdas()
        {
            var resStud = Generator.GenerateStudentsEasy()
                .Where(s => s.Index < 20000 && s.Name.Length <= 6)
                .SelectMany(s => s.Topics,(stud,topic)=> new { stud.Name, topic });
            resStud.ToList().ForEach(Console.WriteLine);
            Console.WriteLine("----------------");
            var resStud2 = from s in Generator.GenerateStudentsEasy()
                          where s.Index < 20000 && s.Name.Length <= 6
                          from topic in s.Topics
                          select new { s.Name, topic };
            resStud2.ToList().ForEach(Console.WriteLine);
        }
        public static void TestOrderBy()
        {

            var resStud = Generator.GenerateStudentsEasy()
                .OrderBy(s => s.Name)
                .ThenByDescending(s => s.Index);
            resStud.ToList().ForEach(Console.WriteLine);
            Console.WriteLine("----------------");
            var resStud2 = from s in Generator.GenerateStudentsEasy()
                           orderby s.Name, s.Index descending
                           select s;
            resStud2.ToList().ForEach(Console.WriteLine);
        }

        class MyComparer : IComparer<string>
        {
            int IComparer<string>.Compare(string x, string y)
            {
                return x.Length-y.Length;
            }
        }
        public static void TestOrderByWithComparer()
        {
            var resStud = Generator.GenerateStudentsEasy()
                .OrderBy(s => s.Name, new MyComparer());
            resStud.ToList().ForEach(Console.WriteLine);
            //no version for Query expression
        }

        public static void TestTakeAndSkip()
        {
            var resStud = Generator.GenerateStudentsEasy()
               .Skip(4).Take(5);
            resStud.ToList().ForEach(Console.WriteLine);
            //no version for Query expression
        }

        public static void TestTakeWhileAndSkipWhile()
        {
            Generator.GenerateStudentsEasy().ToList().ForEach(Console.WriteLine);
            Console.WriteLine("------------");
            var resStud = Generator.GenerateStudentsEasy()
               .SkipWhile(s=>s.Active)
               .SkipWhile(s =>!s.Active)
               .TakeWhile(s=>s.Active);
            resStud.ToList().ForEach(Console.WriteLine);
            //no version for Query expression
        }

        public static void TestLazyExecution()
        {
            var studs = Generator.GenerateStudentsEasy();
            var resStud = from s in studs
                          where s.Index < 20000 && s.Name.Length <= 6
                          select s;

            studs.Add(new Student(30, 15000, "Wuc", Gender.Male, true, 1,
                    new List<string> { "C#", "Java", "algorithms" }));

            resStud.ToList().ForEach(Console.WriteLine);
            
            Console.WriteLine("---------------");
            var resStud2 = (from s in studs
                          where s.Index < 20000 && s.Name.Length <= 6
                          select s).Count();

            studs.Add(new Student(31, 15001, "Wow", Gender.Female, true, 1,
                    new List<string> { "C#" }));

            Console.WriteLine(resStud2);
        }

        public static void TestToDictionaryAndToLookup()
        {
            var resStud = Generator.GenerateStudentsEasy().ToDictionary(s=>s.Index, s=>s.Name);
            resStud.ToList().ForEach(s => Console.WriteLine(s.Key+"-->"+s.Value));

            Console.WriteLine("---------------");
            var resStud2 = Generator.GenerateStudentsEasy().ToLookup(s => s.Name);
            foreach(var dept in resStud2)
            {
                Console.WriteLine(dept.Key);
                resStud2[dept.Key].ToList().ForEach(s => Console.WriteLine("  " + s));
            }
        }

        public static void TestGroupBy()
        {
            var resStud = from s in Generator.GenerateStudentsEasy()
                          group s by s.DepartmentId;

            foreach (var dept in resStud)
            {
                Console.WriteLine(dept.Key);
                dept.ToList().ForEach(s => Console.WriteLine("  " + s));
            }
        }


        public static void TestGroupByComplex()
        {
            var resStud = from s in Generator.GenerateStudentsEasy()
                          group s by new { s.Active, s.Gender } into sGroup
                          orderby sGroup.Key.Active,sGroup.Key.Gender
                          select new
                          {
                              Active = sGroup.Key.Active,
                              sGroup.Key.Gender, // simpler
                              Students = sGroup.OrderBy(s=>s.Name)
                          };

            foreach (var group in resStud)
            {
                Console.WriteLine((group.Active ? "active" : "no active") + "      "+group.Gender);
                group.Students.ToList().ForEach(s => Console.WriteLine("  " + s));
            }
        }

        public static void TestGroupJoin()
        {
            var resStud = Generator.GenerateDepartmentsEasy()
                .GroupJoin(Generator.GenerateStudentsEasy(),
                        dept => dept.Id,
                        stud => stud.DepartmentId,
                        (department, students) => new
                        {
                            Department = department,
                            Students = students
                        }
                        );

            foreach (var group in resStud)
            {
                Console.WriteLine(group.Department.Name);
                group.Students.ToList().ForEach(s => Console.WriteLine("  " + s));
            }
            Console.WriteLine("------------");
            var resStud2 = from d in Generator.GenerateDepartmentsEasy()
                           join s in Generator.GenerateStudentsEasy()
                           on d.Id equals s.DepartmentId into dGroup
                           select new
                           {
                               Department = d,
                               Students = dGroup
                           };
                        

            foreach (var group in resStud2)
            {
                Console.WriteLine(group.Department.Name);
                group.Students.ToList().ForEach(s => Console.WriteLine("  " + s));
            }


        }
        public static void TestJoinSpecial()
        {
            var resStud = Generator.GenerateDepartmentsEasy()
                        .Join(Generator.GenerateStudentsEasy(),
                        dept => dept.Id,
                        stud => stud.DepartmentId,
                        (department, student) => new
                        {
                            DepartmentName = department.Name,
                            StudentName = student.Name
                        }
                        );

            foreach (var elem in resStud)
            {
                Console.WriteLine($"{elem.DepartmentName} -> {elem.StudentName}");
            }
            //Console.WriteLine("------------");
            //var resStud2 = from d in Generator.GenerateDepartmentsEasy()
            //               join s in Generator.GenerateStudentsEasy()
            //               on d.Id equals s.DepartmentId into dGroup
            //               select new
            //               {
            //                   Department = d,
            //                   Students = dGroup
            //               };


            //foreach (var group in resStud2)
            //{
            //    Console.WriteLine(group.Department.Name);
            //    group.Students.ToList().ForEach(s => Console.WriteLine("  " + s));
            //}


        }


        public static void TestJoin()
        {
            var studs = Generator.GenerateStudentsEasy();
            // there are no 6 department
            studs.Add(new Student(30, 15000, "Wuc", Gender.Male, true, 6,
                            new List<string> { "C#", "Java", "algorithms" }));
            var resStud = studs
                         .Join(Generator.GenerateDepartmentsEasy(),
                        stud => stud.DepartmentId,
                        dept => dept.Id,
                        (student,department) => new
                        {
                            DepartmentName = department.Name,
                            StudentName = student.Name
                        }
                        );

            foreach (var elem in resStud)
            {
                Console.WriteLine($"{elem.DepartmentName} -> {elem.StudentName}");
            }
            Console.WriteLine("------------");
            var resStud2 = from s in Generator.GenerateStudentsEasy()
                           join d in Generator.GenerateDepartmentsEasy()
                           on s.DepartmentId equals d.Id
                           select new
                           {
                               DepartmentName = d.Name,
                               StudentName = s.Name
                           };
            foreach (var elem in resStud2)
            {
                Console.WriteLine($"{elem.DepartmentName} -> {elem.StudentName}");
            }
        }

        public static void CartesianProduct()
        {
            var resCart = from num in Generator.GenerateIntsEasy()
                          where num % 2 == 0
                          from d in Generator.GenerateStringsEasy()
                          where d.Length < 7
                          select new
                          {
                              Number = num,
                              Word=d
                           };
            foreach (var elem in resCart)
            {
                Console.WriteLine($"{elem.Number} -> {elem.Word}");
            }
            Console.WriteLine("--------");
            var resCart2 = Generator.GenerateIntsEasy()
                           .Where(num => num % 2 == 0)
                           .SelectMany(
                                s=>Generator.GenerateStringsEasy().Where(s=>s.Length < 7),
                                (n,s)=>new {
                                   Number = n,
                                   Word = s
                                });
            foreach (var elem in resCart2)
            {
//                Console.WriteLine($"{elem.Number} -> {elem.Word}");
                Console.WriteLine($"{elem}");
            }
        }

        class Comp : IEqualityComparer<Student>
        {
            public bool Equals(Student x, Student y)
            {
                return x.Id == x.Id;
            }

            public int GetHashCode(Student obj)
            {
                return obj.Id.GetHashCode();
            }
        }
        public static void TestDistinc()
        {
            var set1 = Generator.GenerateStudentsEasy()
                       .Where(s => s.Id >= 0 && s.Id <=2)
                       .ToList();
            set1.Add(new Student(1, 12345, "Nowak", Gender.Female, true, 1,
                    new List<string> { "C#", "PHP", "algorithms" })); // copy od first student

            set1.Distinct().ToList().ForEach(Console.WriteLine);
            Console.WriteLine("----------------");
            set1.Distinct(new Comp()).ToList().ForEach(Console.WriteLine);
        }

        public static void TestUnion()
        {
            var set1 = Generator.GenerateStudentsEasy()
                       .Where(s => s.Id >= 1 && s.Id <= 4);
            var set2 = Generator.GenerateStudentsEasy()
                       .Where(s => s.Id >= 3 && s.Id <= 6);
            set1.Union(set2).ToList().ForEach(Console.WriteLine);
            Console.WriteLine("----------------");
            set1.Union(set2, new Comp()).ToList().ForEach(Console.WriteLine);
        }

        public static void TestUnionAnnonymous()
        {
            var set1 = Generator.GenerateStudentsEasy()
                       .Where(s => s.Id >= 1 && s.Id <= 4)
                       .Select( s=>new
                       {
                           s.Id, s.Index, s.Name
                       }
                       );
            var set2 = Generator.GenerateStudentsEasy()
                       .Where(s => s.Id >= 3 && s.Id <= 6)
                       .Select(s => new
                        {
                            s.Id, s.Index, s.Name
                        }
                       );
            set1.Union(set2).ToList().ForEach(Console.WriteLine);
        }


        static void MainLinq()
        {
            //ShowAllCollections();
            //Console.WriteLine("--------------");
            //ClauseWhereSimple();
            //SimpleAggregiates();
            //ComplexAggregiates();
            //WhereWithPos();
            //TestSelect();
            //TestSelectMany();
            //TestSelectManyQuery();
            //TestSelectManyWith2Lambdas();
            //TestOrderBy();
            //TestOrderByWithComparer();
            //TestTakeAndSkip();
            //TestTakeWhileAndSkipWhile();
            //TestLazyExecution();
            //TestToDictionaryAndToLookup();
            //TestGroupBy();
            //TestGroupByComplex();
            //TestGroupJoin(); 
            //TestJoinSpecial(); 
            //TestJoin();
            //CartesianProduct();
            //TestDistinc();
            //TestUnion();
            TestUnionAnnonymous();
        }
    }
}
