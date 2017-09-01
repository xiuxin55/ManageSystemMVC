using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Caching;
using System.Security.Cryptography;
using System.Text;
using System.Web;
using System.Web.Security;

namespace WebManager.Comm
{
    public class EncryptHelper
    {
        /// <summary>
        /// 产生一组RSA公钥、私钥
        /// </summary>
        /// <returns></returns>
        public static Dictionary<string, string> CreateRsaKeyPair()
        {
            var keyPair = new Dictionary<string, string>();
            CspParameters RSAParams = new CspParameters();
            RSAParams.Flags = CspProviderFlags.UseMachineKeyStore;
            System.Security.Cryptography.RSACryptoServiceProvider rsaProvider = new RSACryptoServiceProvider(1024, RSAParams);
            RSAParameters parameter = rsaProvider.ExportParameters(true);
            keyPair.Add("PUBLIC", BytesToHexString(parameter.Exponent) + "," + BytesToHexString(parameter.Modulus));
            keyPair.Add("PRIVATE", rsaProvider.ToXmlString(true));
            return keyPair;
        }

        /// <summary>
        /// RSA解密字符串
        /// </summary>
        /// <param name="encryptData">密文</param>
        /// <param name="privateKey">私钥</param>
        /// <returns>明文</returns>
        public static string DecryptRSA(string encryptData, string privateKey)
        {
            string decryptData = "";
            try
            {
                CspParameters RSAParams = new CspParameters();
                RSAParams.Flags = CspProviderFlags.UseMachineKeyStore;
                System.Security.Cryptography.RSACryptoServiceProvider provider = new RSACryptoServiceProvider(1024, RSAParams);
                provider.FromXmlString(privateKey);
                byte[] result = provider.Decrypt(HexStringToBytes(encryptData), false);
                ASCIIEncoding enc = new ASCIIEncoding();
                decryptData = enc.GetString(result);
            }
            catch (Exception e)
            {
                throw new Exception("RSA解密出错!", e);
            }
            return decryptData;

        }

        private static string BytesToHexString(byte[] input)
        {
            StringBuilder hexString = new StringBuilder(64);

            for (int i = 0; i < input.Length; i++)
            {
                hexString.Append(String.Format("{0:X2}", input[i]));
            }
            return hexString.ToString();
        }

        public static byte[] HexStringToBytes(string hex)
        {
            if (hex.Length == 0)
            {
                return new byte[] { 0 };
            }
            if (hex.Length % 2 == 1)
            {
                hex = "0" + hex;
            }
            byte[] result = new byte[hex.Length / 2];
            for (int i = 0; i < hex.Length / 2; i++)
            {
                result[i] = byte.Parse(hex.Substring(2 * i, 2), System.Globalization.NumberStyles.AllowHexSpecifier);
            }
            return result;
        }

        private static ObjectCache Cache
        {
            get { return MemoryCache.Default; }
        }
        /// <summary>
        /// 获取缓存
        /// </summary>
        /// <param name="key"></param>
        /// <returns></returns>
        public static object CacheGet(string key)
        {
            return Cache[key];
        }
        /// <summary>
        /// 设置缓存
        /// </summary>
        /// <param name="key"></param>
        /// <param name="data"></param>
        /// <param name="cacheTime"></param>
        public static void CacheSet(string key, object data, int cacheTime)
        {
            CacheItemPolicy policy = new CacheItemPolicy();
            policy.AbsoluteExpiration = DateTime.Now + TimeSpan.FromMinutes(cacheTime);
            Cache.Add(new CacheItem(key, data), policy);
        }
        /// <summary>
        /// 判断缓存是否存在
        /// </summary>
        /// <param name="key"></param>
        /// <returns></returns>
        public static bool IsSet(string key)
        {
            return (Cache[key] != null);
        }
        /// <summary>
        /// 缓存失效
        /// </summary>
        /// <param name="key"></param>
        public static void CacheRemove(string key)
        {
            Cache.Remove(key);
        }
        /// <summary>
        /// 对字符串进行加密（不可逆）
        /// </summary>
        /// <param name="Password">要加密的字符串</param>
        /// <param name="Format">加密方式,0 is SHA1,1 is MD5</param>
        /// <returns></returns>
        public static string NoneEncrypt(string Password, int Format)
        {
            string strResult = "";
            switch (Format)
            {
                case 0:
                    strResult = FormsAuthentication.HashPasswordForStoringInConfigFile(Password, "SHA1");
                    break;
                case 1:
                    strResult = FormsAuthentication.HashPasswordForStoringInConfigFile(Password, "MD5");
                    break;
                default:
                    strResult = Password;
                    break;
            }
            return strResult;
        }
    }
}