using System;
using UtilityTest.Test;

namespace UtilityTest
{
    internal class Program
    {
        private static void Main(string[] args)
        {
            Console.WriteLine("//-----------开始---------");
            //new EnumTest().Excute();
            //new AlgorithmTest().Excute01();
            //new AlgorithmTest().Excute02();
            //new AlgorithmTest().Excute03();
            //new AlgorithmTest().Excute04();
            new XmlTest().Excute();
            Console.WriteLine("//-----------结束---------");
            Console.Read();
        }
    }
}