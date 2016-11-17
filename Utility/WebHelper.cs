using System.Security.Cryptography;
using System.Text;
using System.Web;

namespace Utility
{
    public class WebHelper
    {
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
    }
}