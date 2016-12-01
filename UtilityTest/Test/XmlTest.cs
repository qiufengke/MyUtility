using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Utils;

namespace UtilityTest.Test
{
    public class XmlTest : IExecute
    {
        public void Excute()
        {
            var fileName = "D:\\test.xml";
            var xmlFormat = "/root/red,address,666/lastPushTime,sheng,ce";
            XmlUtil.CreateXml(fileName, xmlFormat);
        }
    }
}
