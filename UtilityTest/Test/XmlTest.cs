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
                "test.xml",
                "..\\test3.xml"
            };
            var list = new[]
            {
                "/root/red,address[id=3;ip='127.0.0.1'],createtime/lastPushTime,city,month/now,bj,now",
                "/trees",
                "/root/red,address[ip=127.0.0.1;id=6],createtime"
            };
            XmlUtil.CreateXml(fileNames[1], list[0]);
        }
    }
}