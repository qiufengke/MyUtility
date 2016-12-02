using System;
using System.IO;

namespace UtilityTest.Test
{
    public class PathTest : IExecute
    {
        public void Excute()
        {
            var str = @".\Config\log4net.config";
            var fileName = Path.GetFullPath(str);

            var str1 = @"..\Config\log4net.config";
            var fileName1 = Path.GetFullPath(str1);

            var str2 = @"Config\log4net.config";
            var fileName2 = Path.GetFullPath(str2);

            var str3 = @"..\..\Config\log4net.config";
            var fileName3 = Path.GetFullPath(str3);

            var str4 = @"\..\..\Config\log4net.config";
            var fileName4 = Path.GetFullPath(str4);

            Console.WriteLine(fileName);
            Console.WriteLine(fileName1);
            Console.WriteLine(fileName2);
            Console.WriteLine(fileName3);
            Console.WriteLine(fileName4);
        }
    }
}