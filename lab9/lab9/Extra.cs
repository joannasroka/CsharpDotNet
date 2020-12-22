using System;
using System.Collections.Generic;
using System.Text;
using LinqExamples;
using lab6;
using lab7;

namespace lab9
{
    public class Topic
    {
        public int Id { get; set; }
        public string Name { get; set; }


        public Topic(int id, string name)
        {
            Id = id;
            Name = name;
        }
    }


    public class StudentWithTopic
    {
        public int Id { get; set; }
        public int Index { get; set; }
        public string Name { get; set; }
        public Gender Gender { get; set; }
        public bool Active { get; set; }
        public int DepartmentId { get; set; }

        public List<int> Topics { get; set; }
        public StudentWithTopic(int id, int index, string name, Gender gender, bool active,
            int departmentId, List<int> topics)
        {
            this.Id = id;
            this.Index = index;
            this.Name = name;
            this.Gender = gender;
            this.Active = active;
            this.DepartmentId = departmentId;
            this.Topics = topics;
        }

        public StudentWithTopic()
        { }

        public override string ToString()
        {
            var result = $"{Id,2}) {Index,5}, {Name,11}, {Gender,6},{(Active ? "active" : "no active"),9},{DepartmentId,2}, topics: ";
            foreach (var str in Topics)
                result += str + ", ";
            return result;
        }
    }

    static class Extra
    {
        public static int TotalCapacity(Vehicle[] array)
        {
            int totalCapacity = 0;

            foreach (Vehicle vehicle in array)
            {
                if (vehicle is TruckCar) totalCapacity += (vehicle as TruckCar).LoadCapacity;
            }
            return totalCapacity;
        }

    }
}
