using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Xml;

namespace Utils
{
    /// <summary>
    /// 
    /// </summary>
    public class XmlUtil
    {
        /// <summary>
        /// 生成XML
        /// </summary>
        /// <param name="fileName">文件名(包含路径)</param>
        /// <param name="fileFormat">eg : /root/red,address,/lastPushTime,,666</param>
        public static void CreateXml(string fileName, string fileFormat)
        {
            var doc = new XmlDocument();
            var fileUrl = "";
            try
            {
                fileUrl = fileName;
                if (!File.Exists(fileUrl)) File.Create(fileUrl).Close();
            }
            catch
            {
                //ignore
            }

            var nodeList = fileFormat.Split(new[] { '/' }, StringSplitOptions.RemoveEmptyEntries);
            List<string> xleNames;
            if (nodeList[0].Split(new[] { '/' }, StringSplitOptions.RemoveEmptyEntries).Length > 1)
            {
                xleNames = nodeList.ToList();
                xleNames.Insert(0, "root");
            }
            else
            {
                xleNames = nodeList.ToList();
            }
            var dec = doc.CreateXmlDeclaration("1.0", "UTF-8", null);
            doc.AppendChild(dec);

            List<XmlElement> tmpNode = null;
            var count = xleNames.Count;
            for (var i = count - 1; i >= 0; i--)
            {
                tmpNode = LinkedNode(i, tmpNode, doc, xleNames);
            }
            if (tmpNode != null)
            {
                foreach (var o in tmpNode)
                {
                    doc.AppendChild(o);
                }
            }

            doc.Save(fileUrl);
        }

        private static List<XmlElement> LinkedNode(int i, List<XmlElement> childList, XmlDocument doc,
            List<string> nodeList)
        {
            var list = new List<XmlElement>();
            var xleList = nodeList[i].Split(',');
            var count = xleList.Count();
            var childCount = childList == null ? 0 : childList.Count();

            if (i == nodeList.Count - 1)
            {
                foreach (var item in xleList)
                {
                    if (string.IsNullOrEmpty(item))
                    {
                        list.Add(null);
                    }
                    else
                    {
                        var xle = doc.CreateElement(item);
                        list.Add(xle);
                    }
                }
                return list;
            }
            if (i == 0)
            {
                if (count == 1)
                {
                    var xle = doc.CreateElement(xleList[0]);
                    if (childList != null)
                    {
                        foreach (var o in childList)
                        {
                            if (o != null)
                                xle.AppendChild(o);
                        }
                        list.Add(xle);
                        return list;
                    }
                }
            }
            else
            {
                if (count > 0)
                {
                    for (var j = 0; j < count; j++)
                    {
                        if (string.IsNullOrEmpty(xleList[j]))
                            list.Add(null);
                        else
                        {
                            var xle = doc.CreateElement(xleList[j]);
                            if (childList != null && j < childCount && childList[j] != null)
                                xle.AppendChild(childList[j]);
                            list.Add(xle);
                        }
                    }
                }
            }

            return list;
        }
    }
}