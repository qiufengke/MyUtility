using System.Xml;
using Utils;

namespace UtilityTest.Test
{
    public class XmlTest : IExecute
    {
        public void Excute()
        {
            var fileNames = new[]
            {
                "D:\\test1.xml",
                "test2.xml",
                "..\\test3.xml"
            };
            var list = new[]
            {
                "/root/red,address,createtime/lastPushTime,city,month/now,bj,now",
                "/trees"
            };
            XmlUtil.CreateXml(fileNames[1], list[0]);
        }
    }
}