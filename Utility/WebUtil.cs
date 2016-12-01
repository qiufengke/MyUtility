using System;
using System.IO;
using System.Net;
using System.Security.Cryptography;
using System.Text;
using System.Web;

namespace Utils
{
    /// <summary>
    /// 
    /// </summary>
    public class WebUtil
    {
        /// <summary>
        /// 获取客户的ip地址
        /// </summary>
        /// <returns></returns>
        public static string GetClientIp()
        {
            if (HttpContext.Current == null) return "";

            var context = HttpContext.Current;
            return
                context.Request.ServerVariables["HTTP_VIA"] != null
                    ? context.Request.ServerVariables["HTTP_X_FORWARDED_FOR"].Split(',')[0]
                    : context.Request.ServerVariables["REMOTE_ADDR"];
        }

        /// <summary>
        /// MD5计算
        /// </summary>
        /// <param name="str">需要计算MD5的字符串</param>
        /// <returns>32位的字符串</returns>
        public static string GetMd5(string str)
        {
            var md5 = MD5.Create(); // or var md5 = new MD5CryptoServiceProvider();
            var bytValue = Encoding.UTF8.GetBytes(str);
            var bytHash = md5.ComputeHash(bytValue);
            var sb = new StringBuilder();
            foreach (var b in bytHash)
            {
                sb.Append(b.ToString("X2"));
            }
            return sb.ToString();
        }

        /// <summary>
        /// 发送post请求
        /// </summary>
        /// <param name="url"></param>
        /// <param name="paras"></param>
        /// <param name="encodetype"></param>
        /// <returns></returns>
        private string SendPostRequest(string url, string paras, string encodetype = "utf-8")
        {
            var req = (HttpWebRequest)WebRequest.Create(url);
            req.Method = "POST";
            req.ContentType = "application/x-www-form-urlencoded";

            #region 请求头中传入参数

            //req.Headers.Add("sign", sign);

            #endregion

            var encode = string.IsNullOrEmpty(encodetype)
                ? Encoding.Default
                : Encoding.GetEncoding(encodetype);


            if (!string.IsNullOrEmpty(paras))
            {
                var data = encode.GetBytes(paras);
                req.ContentLength = data.Length;
                using (var reqstream = req.GetRequestStream())
                {
                    reqstream.Write(data, 0, data.Length);
                    reqstream.Close();
                }
            }

            string result;
            using (var response = req.GetResponse())
            {
                var stream = response.GetResponseStream();
                if (stream == null) return "";
                using (var reader = new StreamReader(stream, encode))
                {
                    result = reader.ReadToEnd();
                }
            }

            return result;
        }
    }
}