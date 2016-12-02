using System;
using System.IO;
using System.Text;

namespace Utils
{
    /// <summary>
    /// 
    /// </summary>
    public class TxtUtil
    {
        /// <summary>
        /// 以追加方式写入到TXT
        /// </summary>
        /// <param name="content"></param>
        /// <param name="path"></param>
        /// <param name="encodType"></param>
        public static void WriteTxt(string content, string path, string encodType = "UTF-8")
        {
            var fileName = Path.GetFullPath(path);                       // 物理路径
            var dir = fileName.Substring(0, fileName.LastIndexOf('\\')); // 目标文件夹
            if (!File.Exists(path))
            {
                Directory.CreateDirectory(dir);
                File.Create(fileName).Close();
            }
            var encod = string.IsNullOrEmpty(encodType)
                                   ? Encoding.UTF8
                                   : Encoding.GetEncoding(encodType);
            using (StreamWriter sw = new StreamWriter(path, true, encod))
            {
                sw.WriteLine(content);
            }
        }

        /// <summary>
        /// 读取TXT
        /// </summary>
        /// <param name="path"></param>
        /// <param name="encodType"></param>
        /// <returns></returns>
        public static string ReadTxt(string path, string encodType = "UTF-8")
        {
            var encod = string.IsNullOrEmpty(encodType)
                    ? Encoding.UTF8
                    : Encoding.GetEncoding(encodType);
            using (StreamReader sr = new StreamReader(path, encod))
            {
                return sr.ReadToEnd();
            }
        }
    }
}