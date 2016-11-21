using System;
using System.Collections;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using Utils;

namespace UtilityTest.Test
{
    public class AlgorithmTest
    {
        /// <summary>
        /// 求节点值在 [min,max] 之间的所有节点
        /// 从小到大顺序输出
        /// </summary>
        public void Excute01()
        {
            //左子树
            var t1 = new BsTree(1, null, null);
            var t4 = new BsTree(4, null, null);
            var t5 = new BsTree(5, t4, null);
            var t3 = new BsTree(3, t1, t5);
            //右子树
            var t11 = new BsTree(11, null, null);
            var t13 = new BsTree(13, t11, null);
            var t9 = new BsTree(9, null, null);
            var t10 = new BsTree(10, t9, t13);

            var t8 = new BsTree(8, t3, t10);
            var count = 0;

            AlgorithmUtil.PrintRange(t8, 2, 9, ref count);
            Console.WriteLine("循环次数：" + count);
        }

        /// <summary>
        /// 算法实现：直接插入排序
        /// </summary>
        public void Excute02()
        {
            int[] a = { 3, 1, 5, 7, 2, 4, 9, 6 };

            AlgorithmUtil.SimpleInsertSort(a);
        }

        #region 求交集的补集元素

        /// <summary>
        ///     求交集的补集元素
        /// </summary>
        public void Excute03()
        {
            var a = new[]
            {
                6, 3, 9, 3, 2, 4, 5, 7, 3, 2, 6, 3, 9, 3, 2, 4, 5, 7, 3, 2, 6, 3, 9, 3, 2, 4, 5, 7, 3, 2, 6, 3, 9, 3, 2,
                4,
                5, 7, 3, 2, 6, 3, 9, 3, 2, 4, 5, 7, 3, 2, 6, 3, 9, 3, 2, 4, 5, 7, 3, 2, 6, 3, 9, 3, 2, 4, 5, 7, 3, 2, 6,
                3, 9, 3, 2, 4, 5, 7, 3, 2, 6, 3, 9, 3, 2, 4, 5, 7, 3, 2, 6, 3, 9, 3, 2, 4, 5, 7, 3, 2, 6, 3, 9, 3, 2, 4,
                5, 7, 3, 2, 6, 3, 9, 3, 2, 4, 5, 7, 3, 2, 61, 31, 91, 3, 26, 47, 58, 79, 3, 2, 4, 5, 5
            };
            int[] b =
            {
                5, 8, 6, 2, 1, 9,
                5, 8, 6, 2, 1, 9,
                5, 8, 6, 2, 1, 9,
                5, 8, 6, 2, 1, 9
            };
            PrintComplementDataByHashTable(a, b);
            PrintComplementDataByEach(a, b);
            PrintComplementDataByStack(a, b);
        }

        /// <summary>
        ///     输出只在一个数组中出现的所有元素，重复元素只输出一次
        ///     方案：1、先排序（直接插入排序）
        ///     2、归并 输出
        /// </summary>
        /// <param name="a"></param>
        /// <param name="b"></param>
        private static void PrintComplementDataByHashTable(int[] a, int[] b)
        {
            var sw = new Stopwatch();
            sw.Start();
            var ht = new Hashtable();
            var ht2 = new Hashtable();
            foreach (var item in a)
            {
                if (!ht.Contains(item))
                    ht.Add(item, 1);
            }
            foreach (var o in b)
            {
                if (!ht2.Contains(o) && !ht.Contains(o))
                {
                    ht2.Add(o, 1);
                }
            }
            foreach (var o in ht.Keys)
            {
                Console.Write(o + ",");
            }
            foreach (var o in ht2.Keys)
            {
                Console.Write(o + ",");
            }
            sw.Stop();
            Console.WriteLine("使用哈希表耗时：" + sw.ElapsedMilliseconds);
        }

        private static void PrintComplementDataByEach(int[] a, int[] b)
        {
            var sw = new Stopwatch();
            sw.Start();
            foreach (var o in a)
            {
                if (!b.Contains(o))
                    Console.Write(o + ",");
            }
            foreach (var o in b)
            {
                if (!a.Contains(o))
                    Console.Write(o + ",");
            }
            sw.Stop();
            Console.WriteLine("使用循环耗时：" + sw.ElapsedMilliseconds);
        }

        private static void PrintComplementDataByStack(int[] a, int[] b)
        {
            var sw = new Stopwatch();
            sw.Start();
            var st = new Stack();
            var st2 = new Stack();
            foreach (var o in a)
            {
                if (!st.Contains(o))
                    st.Push(o);
            }
            foreach (var o in b)
            {
                if (!st.Contains(o) && !st2.Contains(o))
                    st2.Push(o);
            }
            foreach (var o in st)
            {
                Console.Write(o + ",");
            }
            foreach (var o in st2)
            {
                Console.Write(o + ",");
            }
            sw.Stop();
            Console.WriteLine("使用栈耗时：" + sw.ElapsedMilliseconds);
        }

        #endregion

        #region 功能说明：在文章中随机生成5个内链(不连续)

        /// <summary>
        /// 在文章中随机生成5个内链(不连续)
        /// </summary>
        public void Excute04()
        {
            var url = Directory.GetCurrentDirectory() + "\\test.txt";
            var content = File.ReadAllText(url, Encoding.Default);
            var keyWordsList = new Dictionary<string, string>
            {
                {"牵引", "http://baike.baidu.com/subview/91398/16967547.htm"},
                {"压迫", "http://baike.baidu.com/subview/91398/16967547.htm"},
                {"到头部重力", "http://baike.baidu.com/subview/91398/16967547.htm"},
                {"力度", "http://baike.baidu.com/subview/51980/5109730.htm"},
                {"解除", "http://baike.baidu.com/subview/91398/16967547.htm"},
                {"不可逆", "http://baike.baidu.com/subview/91398/16967547.htm"}
            };
            Replace(content, keyWordsList);
        }

        private static void Replace(string content, Dictionary<string, string> dic)
        {
            var innerLinkCount = 0; //已添加内链个数，最多不超过5个
            var keyWordsList = dic;

            var origialContent = Regex.Replace(content, @"<a[^>]+>(.*?)</a>", "$1"); //去掉文本链接
            CreateNew(origialContent, "test2");
            content = origialContent;

            foreach (var o in keyWordsList)
            {
                var aLink = string.Format("<a href='{0}'>{1}</a>", o.Value, o.Key);
                var reg = new Regex(o.Key);
                var mc = reg.Matches(content);
                var count = mc.Count;

                switch (count)
                {
                    case 0:
                        continue;
                    case 1:
                        CreateLinksInBaike(mc[0], aLink, ref content, ref innerLinkCount);
                        break;
                    default:
                        var index = (int)Math.Ceiling(count * 1.0 / 2);
                        var result = CreateLinksInBaike(mc[index], aLink, ref content, ref innerLinkCount);
                        if (!result)
                        {
                            for (var i = 0; i < mc.Count; i++)
                            {
                                if (i == index) continue;
                                result = CreateLinksInBaike(mc[i], aLink, ref content, ref innerLinkCount);
                                if (result) break;
                            }
                        }
                        break;
                }

                if (innerLinkCount == 5) break;
            }

            CreateNew(content);
        }

        private static bool CreateLinksInBaike(Match m, string link, ref string content, ref int count)
        {
            var startIndex = m.Index <= 1 ? 0 : m.Index - 1;
            var length =
                startIndex + m.Length + 1 < content.Length - 1
                    ? m.Length + 1 + m.Index - startIndex
                    : content.Length - startIndex;
            var cutContent = content.Substring(startIndex, length);

            if ((!cutContent.Contains(">")) && (!cutContent.Contains("<")))
            {
                content = content.Remove(m.Index, m.Length);
                content = content.Insert(m.Index, link);
                count++;
                return true;
            }

            return false;
        }

        private static void CreateNew(string content, string name = "test0")
        {
            var url = Directory.GetCurrentDirectory() + "\\" + name + ".html";
            using (var fs = new FileStream(url, FileMode.Create))
            {
                using (var sw = new StreamWriter(fs, Encoding.Default))
                {
                    sw.Write(content);
                }
            }

            url = Directory.GetCurrentDirectory() + "\\" + name + ".txt";
            using (var fs = new FileStream(url, FileMode.Create))
            {
                using (var sw = new StreamWriter(fs, Encoding.Default))
                {
                    sw.Write(content);
                }
            }

            Console.WriteLine("成功");
        }

        #endregion
    }
}