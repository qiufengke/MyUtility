using System;
using System.Collections.Generic;
using Newtonsoft.Json;
using Utils;
using Utils.Extensions;

namespace UtilityTest.Test
{
    public class OrmTest : IExecute
    {
        public void Excute()
        {
            var list1 = new List<Student1>
            {
                new Student1 {City = "bj", Sex = "n"},
                new Student1 {City = "bj", Sex = "n"},
                new Student1 {City = "bj", Sex = "n"}
            };
            var list2 = OrmUtil.MapToList<Student1, Student2>(list1);
            var list3 = list1.MapToList<Student1, Student2>();
            var jsonResult = JsonConvert.SerializeObject(list3, Formatting.Indented);

            Console.WriteLine(jsonResult);
        }
    }

    public class Student1
    {
        public int Age = 20;
        public string City;
        public string Sex;
        public int testInt = 500;
        public DateTime Birthday { get; set; }
    }

    public class Student2
    {
        public int Age = 20;
        public string City;
        public string Sex;
        public string testP { get; set; }
        public bool testB { get; set; }
        public int testInt { get; set; }
    }
}