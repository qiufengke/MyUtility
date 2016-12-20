using System;
using System.Diagnostics;
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
            Stopwatch watch = new Stopwatch();
            watch.Start();

            //new EnumTest().Excute();
            //new AlgorithmTest().Excute01();
            //new AlgorithmTest().Excute02();
            //new AlgorithmTest().Excute03();
            //new AlgorithmTest().Excute04();
            //new XmlTest().Excute();
            //new PathTest().Excute();
            //TxtUtil.ReadTxt("");
            new OrmTest().Excute();


            watch.Stop();
            Console.WriteLine($"耗时：{watch.ElapsedMilliseconds}");
            Console.WriteLine("//-----------结束---------");
            Console.Read();
        }


    }
}