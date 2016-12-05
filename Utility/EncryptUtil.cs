using System;
using System.IO;
using System.Security.Cryptography;
using System.Text;

namespace Utils
{
    /// <summary>
    /// 加解密辅助类
    /// </summary>
    public class EncryptUtil
    {
        #region Des 加解密

        /// <summary>
        /// Des 加密
        /// </summary>
        /// <param name="encryptStr"></param>
        /// <param name="key"></param>
        /// <returns></returns>
        public static string DesEncrypt(string encryptStr, string key)
        {
            if (string.IsNullOrEmpty(encryptStr))
                throw new ArgumentException("the encryptStr is null or empty.");
            if (string.IsNullOrEmpty(key))
                throw new ArgumentException("the key is null or empty.");
            var inputArry = Encoding.Default.GetBytes(encryptStr);
            var byKey = Encoding.ASCII.GetBytes(key);
            var byIv = Encoding.ASCII.GetBytes(key);
            var ms = new MemoryStream();
            using (var cryptoProvider = new DESCryptoServiceProvider())
            {
                using (
                    var cs = new CryptoStream(ms, cryptoProvider.CreateEncryptor(byKey, byIv),
                        CryptoStreamMode.Write))
                {
                    cs.Write(inputArry, 0, inputArry.Length);
                    cs.FlushFinalBlock();
                    cs.Close();
                }
            }

            var str = Convert.ToBase64String(ms.ToArray());
            ms.Close();
            return str;
        }

        /// <summary>
        /// Des 解密
        /// </summary>
        /// <param name="decryptStr"></param>
        /// <param name="key"></param>
        /// <returns></returns>
        public static string DesDecrypt(string decryptStr, string key)
        {
            if (string.IsNullOrEmpty(decryptStr))
                throw new ArgumentException("the decryptStr is null or empty.");

            if (string.IsNullOrEmpty(key))
                throw new ArgumentException("the key is null or empty.");

            var inputArry = Convert.FromBase64String(decryptStr);
            var byKey = Encoding.Default.GetBytes(key);
            var byIv = Encoding.Default.GetBytes(key);
            var ms = new MemoryStream();
            using (var cryptProvider = new DESCryptoServiceProvider())
            {
                using (
                    var cs = new CryptoStream(ms, cryptProvider.CreateDecryptor(byKey, byIv), CryptoStreamMode.Write)
                    )
                {
                    cs.Write(inputArry, 0, inputArry.Length);
                    cs.FlushFinalBlock();
                    cs.Close();
                }
            }

            var str = Encoding.Default.GetString(ms.ToArray());
            ms.Close();
            return str;
        }

        #endregion

        #region 3DES 加解密

        /// <summary>
        /// 3DES 加密
        /// </summary>
        /// <param name="encryStr">加密字符串</param>
        /// <param name="key"></param>
        /// <returns></returns>
        public static string Encrypt3Des(string encryStr, string key)
        {
            if (string.IsNullOrEmpty(encryStr))
                throw new ArgumentException("the encryStr is null or empty.");

            if (string.IsNullOrEmpty(key))
                throw new ArgumentException("the key is null or empty.");

            var inputArry = Encoding.Default.GetBytes(encryStr);
            var hashmd5 = new MD5CryptoServiceProvider();
            var byKey = hashmd5.ComputeHash(Encoding.Default.GetBytes(key));
            var byIv = byKey;
            var ms = new MemoryStream();
            using (var tDescryptProvider = new TripleDESCryptoServiceProvider())
            {
                tDescryptProvider.Mode = CipherMode.ECB;
                using (
                    var cs = new CryptoStream(ms, tDescryptProvider.CreateEncryptor(byKey, byIv),
                        CryptoStreamMode.Write))
                {
                    cs.Write(inputArry, 0, inputArry.Length);
                    cs.FlushFinalBlock();
                    cs.Close();
                }
            }

            var str = Convert.ToBase64String(ms.ToArray());
            ms.Close();
            return str;
        }

        /// <summary>
        /// 3DES 解密
        /// </summary>
        /// <param name="decryStr"></param>
        /// <param name="key"></param>
        /// <returns></returns>
        public static string Decrypt3Des(string decryStr, string key)
        {
            if (string.IsNullOrEmpty(decryStr))
                throw new ArgumentException("the decryStr is null or empty.");

            if (string.IsNullOrEmpty(key))
                throw new ArgumentException("the key is null or empty.");

            var inputArry = Convert.FromBase64String(decryStr);
            var hashmd5 = new MD5CryptoServiceProvider();
            var byKey = hashmd5.ComputeHash(Encoding.Default.GetBytes(key));
            var byIv = byKey;
            var ms = new MemoryStream();
            using (var tDescryptProvider = new TripleDESCryptoServiceProvider())
            {
                tDescryptProvider.Mode = CipherMode.ECB;
                using (
                    var cs = new CryptoStream(ms, tDescryptProvider.CreateDecryptor(byKey, byIv),
                        CryptoStreamMode.Write))
                {
                    cs.Write(inputArry, 0, inputArry.Length);
                    cs.FlushFinalBlock();
                    cs.Close();
                }
            }

            var str = Encoding.Default.GetString(ms.ToArray());
            ms.Close();
            return str;
        }

        #endregion
    }
}