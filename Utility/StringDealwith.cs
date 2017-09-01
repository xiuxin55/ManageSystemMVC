/*
Author:WangXinXin
Create date: 2017/2/7 17:48:35
Description：消息提示对话框字符串处理类
--------------------------------------------------------------------------------------------------------
Versions：
    V1.00 2017/2/7 17:48:35 WangXinXin Add
*/
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Web;
using System.Web.Security;

namespace WebManager.Utility
{
    /// <summary>
    /// 字符串处理
    /// </summary>
    public class StringDealwith
    {
        #region StringDealwith 
        public StringDealwith()
        {

        } 
        #endregion

        #region 字符串处理

        /// <summary>
        /// 根据时间创建字符串
        /// </summary>
        /// <returns></returns>
        public static string CreateDateTimeString()
        {
            DateTime now = DateTime.Now;
            string str = now.Year.ToString()
                + now.Month.ToString()
                + now.Day.ToString()
                + now.Hour.ToString()
                + now.Minute.ToString()
                + now.Second.ToString()
                + now.Millisecond.ToString();
            return (str);
        }
        /// <summary>
        /// 获取时间格式
        /// </summary>
        /// <returns></returns>
        public static string CreateDateTime()
        {
            return System.DateTime.Now.ToString("yyyyMMddHHmmss");
        }
        /// <summary>
        /// 格式化显示字符串的长度，如果超过指定的长度，则显示...
        /// </summary>
        /// <param name="str"></param>
        /// <param name="length"></param>
        /// <returns></returns>
        public static string FormatStringLength(string str, int length)
        {
            if (string.IsNullOrEmpty(str)) return string.Empty;

            ///如果包含中文字符，中文字符的长度加倍
            if (Encoding.UTF8.GetByteCount(str) > str.Length)
            {   ///调整为中文的长度，等于英文的一半
                length = length / 2;
            }
            if (str.Length > length)
            {
                return str.Substring(0, length) + "...";
            }
            return str;
        }
        /// <summary>
        /// MD5字符串加密
        /// </summary>
        /// <param name="key">要加密的字符串</param>
        /// <returns>加密完的字符串</returns>
        public static string String2MD5(string key)
        {
            return FormsAuthentication.HashPasswordForStoringInConfigFile(key, "md5").Substring(8, 16).ToLower();
        }

        /// <summary>
        /// 获取数字、字母的随机字符串
        /// </summary>
        /// <returns></returns>
        public static string getRandonStr()
        {
            string strBase = "0123456789qwertyuiopasdfghjklzxcvbnm";
            char[] charArray = strBase.ToCharArray();
            string strRet = "";
            for (int i = 0; i < 5; i++)
            {
                Random rd = new Random();
                int tempID = rd.Next(0, 35);
                strRet += charArray[tempID].ToString();
            }
            return strRet;
        }

        public static string StringFormat(string mess)
        {
            // 去掉特殊字符
            string resultStr = mess.Replace("'", "’").Replace("\"", "“").Replace(",", "，");
            //去掉换行
            resultStr = resultStr.Replace((char)13, (char)0).Replace((char)10, (char)0);
            return resultStr;
        }
        #endregion
    }
}