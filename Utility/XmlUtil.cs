using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Xml;

namespace Utils
{
    /// <summary>
    /// XML读写辅助类
    /// </summary>
    public class XmlUtil
    {
        /// <summary>
        /// 生成XML
        /// </summary>
        /// <param name="fileName">文件名(包含路径)</param>
        /// <param name="fileFormat">eg : /root/red,address,createtime/lastPushTime,city,month/now,bj,now</param>
        public static void CreateXml(string fileName, string fileFormat)
        {
            if (string.IsNullOrEmpty(fileName))
                throw new ArgumentException("没有设置XML的生成路径，eg: C:\\test.xml.");

            if (string.IsNullOrEmpty(fileFormat))
                throw new ArgumentException("没有设置XML的生成格式，eg: /root/date/tuesday.");

            if (!File.Exists(fileName))
                File.Create(fileName).Close(); // 生成文件后关闭流，防止后面使用文件时报程序占用的异常

            var nodeList = fileFormat.Split(new[] { '/' }, StringSplitOptions.RemoveEmptyEntries);
            if (nodeList.Any())
            {
                Generate(fileName, nodeList);
            }
            else
            {
                throw new ArgumentException("XML的生成格式设置错误，eg: /root/date/tuesday.");
            }
        }

        private static void Log(string content = "")
        {
            var errorFileUrl = $"XMLError\\error_{DateTime.Now.ToString("yyyyMMdd")}.log";
            var errorMsg = DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss") + ": " + content;
            TxtUtil.WriteTxt(errorMsg, errorFileUrl);
        }

        /// <summary>
        /// 开始生成XML
        /// </summary>
        /// <param name="fileName"></param>
        /// <param name="nodeList"></param>
        private static void Generate(string fileName, string[] nodeList)
        {
            List<string> xleNames;
            if (nodeList[0].Split(new[] { ',' }, StringSplitOptions.RemoveEmptyEntries).Length > 1)
            {
                xleNames = nodeList.ToList();
                // 添加根节点
                xleNames.Insert(0, "root");
            }
            else
            {
                xleNames = nodeList.ToList();
            }

            var doc = new XmlDocument();
            var dec = doc.CreateXmlDeclaration("1.0", "UTF-8", null);
            doc.AppendChild(dec);

            List<XmlElement> tmpNode = null;
            var count = xleNames.Count;

            for (var i = count - 1; i >= 0; i--)
            {
                tmpNode = LinkedNode(i, tmpNode, doc, xleNames);
            }

            if (tmpNode != null && tmpNode.Any())
            {
                doc.AppendChild(tmpNode[0]);
            }

            doc.Save(fileName);
        }

        /// <summary>
        /// 将XML节点组装到根节点上返回
        /// </summary>
        /// <param name="i"></param>
        /// <param name="childList"></param>
        /// <param name="doc">必须是同一个doc</param>
        /// <param name="nodeList"></param>
        /// <returns></returns>
        private static List<XmlElement> LinkedNode(int i, List<XmlElement> childList, XmlDocument doc,
            List<string> nodeList)
        {
            var list = new List<XmlElement>();
            var xleList = nodeList[i].Split(',');
            var count = xleList.Count();
            var childCount = childList?.Count() ?? 0;

            // 处理XML的最内层节点
            if (i > 0 && i == nodeList.Count - 1)
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
            // 处理XML的最外层节点(根节点)
            // 前面坐了处理，此处count一定等于1
            if (i == 0 && count == 1)
            {
                var xle = doc.CreateElement(xleList[0]);
                if (childList != null)
                {
                    foreach (var o in childList)
                    {
                        if (o != null) xle.AppendChild(o);
                    }
                }
                list.Add(xle);
                return list;
            }
            // 处理XML的中间层节点
            if (count > 0)
            {
                for (var j = 0; j < count; j++)
                {
                    // 如果为空字符串，则不创建节点
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

            return list;
        }
    }
}