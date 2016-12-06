using System;
using System.IO;
using UtilityTest.Test;
using Utils;

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
            //new PathTest().Excute();
            //TxtUtil.ReadTxt("");
            Console.WriteLine("//-----------结束---------");
            Console.Read();
        }


    }
}