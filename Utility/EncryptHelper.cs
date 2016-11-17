using System;
using System.IO;
using System.Security.Cryptography;
using System.Text;

namespace Utility
{
    public class EncryptHelper
    {
        #region Des 加解密

        public static string DesEncrypt(string encryptStr, string key)
        {
            try
            {
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
            catch (Exception)
            {
                return encryptStr;
            }
        }

        public static string DesDecrypt(string decryptStr, string key)
        {
            try
            {
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
            catch (Exception)
            {
                return decryptStr;
            }
        }

        #endregion

        #region 3DES 加解密

        public static string Encrypt3Des(string encryStr, string key)
        {
            try
            {
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
            catch (Exception)
            {
                return encryStr;
            }
        }

        public static string Decrypt3Des(string decryStr, string key)
        {
            try
            {
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
            catch (Exception)
            {
                return decryStr;
            }
        }

        #endregion
    }
}