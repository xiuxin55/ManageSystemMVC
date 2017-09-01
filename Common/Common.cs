using DGBank.Models;
using DGBank.Models.App;
/*
Author:WangXinXin
Create date: 2017/2/8 
Description：通用类
--------------------------------------------------------------------------------------------------------
Versions：
    V1.00 2017/2/8 17:58:10 WangXinXin Add
*/
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Text.RegularExpressions;
using System.Web;
using System.Configuration;
using DGBank.Entities;
using DGBank.BLL;
using System.Net;
using DGBank.Cache;
using CYTX.Utility.Utils;
using System.Web.UI;

namespace WebManager.Comm
{
    public static class Common {
        #region 属性
        /// <summary>
        /// 基金列表临时使用
        /// </summary>
        public static List<FundInfoBase> tmp;

        /// <summary>
        /// 临时存储新闻Id
        /// </summary>
        public static List<MFundNewsInfo> tmpNewsId;

        /// <summary>
        /// 临时存储基金组合-基金产品列表
        /// </summary>
        public static List<FundInfoBase> tmpFundProduct;

        /// <summary>
        /// 初始密码
        /// </summary>
        public static readonly string INITPWD = "123456";

        public static readonly string NEARLYMONTH = "近1个月收益";
        public static readonly string NEARLYTHREEMONTH = "近3个月收益";
        public static readonly string NEARLYSIXMONTH = "近6个月收益";
        public static readonly string NEARLYYEAR = "近1年收益";
        public static readonly string NEARLTHREEYYEAR = "近3年收益";
        public static readonly string NEARLESTABLISH = "成立以来累计收益";

        public static readonly string WEBDISPLAY = "网页端";
        public static readonly string APPDISPLAY = "手机APP";

        public static readonly string NAVURL = "导航地址";
        public static readonly string DETAILIMG = "详情图片";
        public static readonly string FUNDCODE = "基金代码";
        public static readonly string NAVURLNO = "无导航地址";

        public static readonly string WEEK = "七日年化";
        public static readonly string THOUSANDVALUE = "万份收益";
        public static readonly string MONTH = "近1月收益";
        public static readonly string THREEMONTH = "近3月收益";
        public static readonly string SIXMONTH = "近6月收益";
        public static readonly string YEAR = "近1年收益";
        public static readonly string THREEYEAR = "近3年收益";
        public static readonly string CURRENTVALUE = "单位净值";

        public static readonly string WEEKKEY = "Week";
        public static readonly string THOUSANDVALUEKEY = "ThousandValue";
        public static readonly string MONTHKEY = "Month";
        public static readonly string THREEMONTHKEY = "ThreeMonth";
        public static readonly string SIXMONTHKEY = "SixMonth";
        public static readonly string YEARKEY = "OneYear";
        public static readonly string THREEYEARKEY = "ThreeYear";
        public static readonly string CURRENTVALUEKEY = "Current_Value";

        //默认密钥向量
        private static byte[] Keys = { 0x12, 0x34, 0x56, 0x78, 0x90, 0xAB, 0xCD, 0xEF };

        /// <summary>
        /// 加/解密密钥
        /// </summary>
        public static string EncryptDecryptKey = "azjmerbv";

        static TblAdminLogs logItem;//日志实体
        static TblAdminLogsBLL logbll = new TblAdminLogsBLL();//日志Bll

        public static string LoginName { get; set; }//登录名
        public static int UserId { get; set; }//登录名id
        public static List<string> AuthorityPages { get; set; }//当前用户权限页
        #endregion

        #region Method
        /// <summary>
        /// 检查文件类型
        /// </summary>
        /// <param name="fileExtension">扩展名</param>
        /// <returns></returns>
        public static bool checkFileExt(string fileExtension) {
            bool fileOk = false;
            string[] allowExtension = { ".jpg", ".jpeg", ".png" };//限定只能上传jpg和jpeg,png图片

            for (int i = 0; i < allowExtension.Length; i++) {
                if (fileExtension == allowExtension[i]) {
                    fileOk = true;
                    break;
                }
            }

            return fileOk;
        }

        /// <summary>
        /// 字符串做冗错
        /// </summary>
        /// <param name="filePath"></param>
        /// <returns></returns>
        public static string EndWithStr(string filePath) {
            if (string.IsNullOrEmpty(filePath)) return string.Empty;

            if (!filePath.EndsWith("/"))
                return filePath += "/";

            return filePath;
        }

        /// <summary>
        /// 检查文件大小 
        /// </summary>
        /// <param name="contentLength">文件大小</param>
        /// <returns></returns>
        public static bool checkfileSize(int contentLength) {
            if (contentLength > 2048000)
                return false;
            return true;
        }

        static string s_strUploadPath;
        /// <summary>
        /// 获取配置的上传文件路径
        /// </summary>
        /// <param name="Server"></param>
        /// <returns></returns>
        public static string GetUploadPath(HttpServerUtility Server = null) {
            if (s_strUploadPath == null) {
                if (Server == null) Server = HttpContext.Current.Server;
                string path = ConfigurationManager.AppSettings["fileUpLoadPath"];
                if (string.IsNullOrWhiteSpace(path))
                    s_strUploadPath = string.Empty;
                else if (path.StartsWith("~/"))
                    s_strUploadPath = Server.MapPath(path);
                else if (path.StartsWith("/"))
                    s_strUploadPath = Path.Combine(Server.MapPath("~/"), path.Replace('/', '\\'));
                else
                    s_strUploadPath = path;
                if (s_strUploadPath.Length > 0 && s_strUploadPath.Last() != '\\')
                    s_strUploadPath = s_strUploadPath + "\\";
            }
            return s_strUploadPath;
        }
        /// <summary>
        /// 根据length截取数据
        /// </summary>
        /// <param name="str"></param>
        /// <param name="length"></param>
        /// <returns></returns>
        public static string GetCut(object str, int length) {
            string propStr = str == null ? string.Empty : str.ToString();
            if (propStr.Length == 0) return ""; //"(none)";

            if (propStr.Length > length)
                return propStr.Substring(0, length) + "...";

            return propStr;
        }


        /// <summary>
        /// 检测目录是否存在
        /// </summary>
        /// <param name="ServerIP">IP</param>
        /// <param name="pFtpUserID">账号</param>
        /// <param name="pFtpPW">密码</param>
        /// <param name="FileSource"></param>
        /// <returns>true->目录存在、创建指定目录成功，false->创建目录失败</returns>
        public static bool checkDirectory(string ServerIP, string pFtpUserID, string pFtpPW, string FileSource) {
            //检测目录是否存在
            Uri uri = new Uri("ftp://" + ServerIP + "/" + FileSource + "/");
            if (DirectoryIsExist(uri, pFtpUserID, pFtpPW))
                return true;

            //创建目录
            uri = new Uri("ftp://" + ServerIP + "/" + FileSource);
            if (CreateDirectory(uri, pFtpUserID, pFtpPW))
                return true;
            else { return false; }
        }

        //解密密码
        public static string DGFtpPassword {
            get {
                return CodeEncryption.RijndaelDecryption(ConfigurationManager.AppSettings["dgftppwd"].ToString());
            }
        }


        /// <summary>
        /// ftp创建目录(创建文件夹)
        /// </summary>
        /// <param name="IP">IP服务地址</param>
        /// <param name="UserName">登陆账号</param>
        /// <param name="UserPass">密码</param>
        /// <returns>true->成功 ,false->失败</returns>
        public static bool CreateDirectory(Uri IP, string UserName, string UserPass) {
            try {
                FtpWebRequest FTP = (FtpWebRequest)FtpWebRequest.Create(IP);
                FTP.Credentials = new NetworkCredential(UserName, UserPass);
                FTP.Proxy = null;
                FTP.KeepAlive = false;
                FTP.Method = WebRequestMethods.Ftp.MakeDirectory;
                FTP.UseBinary = true;
                FtpWebResponse response = FTP.GetResponse() as FtpWebResponse;
                response.Close();
                return true;
            }
            catch {
                return false;
            }
        }

        /// <summary>
        /// 检测目录是否存在
        /// </summary>
        /// <param name="pFtpServerIP">ftpurl</param>
        /// <param name="pFtpUserID">账号</param>
        /// <param name="pFtpPW">密码</param>
        /// <returns>false不存在，true存在</returns>
        public static bool DirectoryIsExist(Uri pFtpServerIP, string pFtpUserID, string pFtpPW) {
            string[] value = GetFileList(pFtpServerIP, pFtpUserID, pFtpPW);
            return null == value ? false : true;
        }

        /// <summary>
        /// GetFileList
        /// </summary>
        /// <param name="pFtpServerIP"></param>
        /// <param name="pFtpUserID"></param>
        /// <param name="pFtpPW"></param>
        /// <returns></returns>
        public static string[] GetFileList(Uri pFtpServerIP, string pFtpUserID, string pFtpPW) {
            StringBuilder result = new StringBuilder();
            try {
                FtpWebRequest reqFTP = (FtpWebRequest)FtpWebRequest.Create(pFtpServerIP);
                reqFTP.UseBinary = true;
                reqFTP.Credentials = new NetworkCredential(pFtpUserID, pFtpPW);
                reqFTP.Method = WebRequestMethods.Ftp.ListDirectoryDetails;

                WebResponse response = reqFTP.GetResponse();
                StreamReader reader = new StreamReader(response.GetResponseStream());
                string line = reader.ReadLine();
                while (line != null) {
                    result.Append(line);
                    result.Append("\n");
                    line = reader.ReadLine();
                }
                reader.Close();
                response.Close();
                return result.ToString().Split('\n');
            }
            catch {
                return null;
            }
        }

        /// <summary>
        /// FileExist
        /// </summary>
        /// <param name="ip">ip</param>
        /// <param name="ftpuser">账号</param>
        /// <param name="ftppwd">密码</param>
        /// <param name="fileSource">file source</param>
        /// <returns>true->存在，false->不存在</returns>
        public static bool FileExist(string ip, string ftpuser, string ftppwd, string fileSource) {
            Uri uri = new Uri("ftp://" + ip + "/" + fileSource);
            var request = (FtpWebRequest)WebRequest.Create(uri);
            request.Credentials = new NetworkCredential(ftpuser, ftppwd);
            request.Method = WebRequestMethods.Ftp.GetFileSize;

            try {
                FtpWebResponse response = (FtpWebResponse)request.GetResponse();
            }
            catch (WebException ex) {
                FtpWebResponse response = (FtpWebResponse)ex.Response;
                if (response.StatusCode ==
                    FtpStatusCode.ActionNotTakenFileUnavailable)
                    return false;
            }

            return true;
        }

        /// <summary>
        /// 删除Ftp文件
        /// </summary>
        /// <param name="ftpfilePath">FTP服务器文件路径 如：ftp://114.113.151.71:10090/fish.jpg</param>
        /// <param name="ftpuser">账号</param>
        /// <param name="ftppwd">密码</param>
        public static bool DeleteFileName(string ftpfilePath, string ftpuser, string ftppwd) {
            try {
                FtpWebRequest FTP = (FtpWebRequest)FtpWebRequest.Create(ftpfilePath);
                FTP.Credentials = new NetworkCredential(ftpuser, ftppwd);
                FTP.Proxy = null;
                FTP.KeepAlive = false;
                FTP.Method = WebRequestMethods.Ftp.DeleteFile;
                FtpWebResponse response = (FtpWebResponse)FTP.GetResponse();
                response.Close();
                return true;
            }
            catch (Exception ex) {
                return false;
            }
        }

        /// <summary>
        /// 正整数正则
        /// </summary>
        /// <param name="pageNum">参数</param>
        /// <returns>true->正整数 false->非</returns>
        public static bool RegIsNum(string pageNum) {
            if (string.IsNullOrEmpty(pageNum.Trim())) return false;

            string pattern = @"^[0-9]*[1-9][0-9]*$";
            return Regex.IsMatch(pageNum.Trim(), pattern);
        }

        /// <summary>
        /// ImageUrl转换
        /// </summary>
        /// <param name="imgUrl">path</param>
        /// <returns></returns>
        public static string ReplaceImgUrl(string imgUrl) {
            if (string.IsNullOrEmpty(imgUrl)) return string.Empty;
            //只留下最后的文件名，如： /filename
            imgUrl = imgUrl.Replace('\\', '/');
            int iPos = imgUrl.LastIndexOf('/');

            if (iPos >= 0)
                imgUrl = imgUrl.Remove(0, iPos);
            else
                imgUrl = "/" + imgUrl;
            return imgUrl;
        }

        /// <summary>
        /// 记录操作日志
        /// </summary>
        /// <param name="pageUrl">page url</param>
        /// <param name="logcontent">logcontent</param>
        public static void SaveLog(string pageUrl, string logcontent) {
            logItem = new TblAdminLogs();
            logItem.LogTime = DateTime.Now;
            logItem.PageUrl = pageUrl;

            logItem.LogContent = logcontent;
            logItem.CreateUserId = UserId;
            logItem.CreatUserName = LoginName;
            logbll.SaveOrUpdate(logItem);
        }


        /// <summary> 
        /// 计算余页 
        /// </summary> 
        /// <param name="pageSize">显示的数据条数</param>
        /// <param name="recordsCount">信息总条数</param>
        /// <returns></returns> 
        public static int OverPage(int recordsCount, int pageSize) {
            int pages = 0;
            if (recordsCount % pageSize != 0)
                pages = 1;
            else
                pages = 0;
            return pages;
        }

        /// <summary> 
        /// 计算余页，防止SQL语句执行时溢出查询范围 
        /// </summary> 
        /// <param name="pageSize">显示的数据条数</param>
        /// <param name="recordsCount">信息总条数</param>
        /// <returns></returns> 
        public static int ModPage(int recordsCount, int pageSize) {
            int pages = 0;
            if (recordsCount % pageSize == 0 && recordsCount != 0)
                pages = 1;
            else
                pages = 0;
            return pages;
        }

        /// <summary>
        /// 验证email
        /// </summary>
        /// <param name="str_Email"></param>
        /// <returns></returns>
        public static bool IsEmail(string str_Email) {
            Regex r = new Regex("^\\s*([A-Za-z0-9_-]+(\\.\\w+)*@(\\w+\\.)+\\w{2,5})\\s*$");

            if (r.IsMatch(str_Email))
                return true;
            return false;
        }

        /// <summary>
        /// 数字
        /// </summary>
        /// <param name="str_number"></param>
        /// <returns></returns>
        public static bool IsNumber(string str_number) {
            return System.Text.RegularExpressions.Regex.IsMatch(str_number, @"^[0-9]*$");
        }

        /// <summary>
        /// 验证电话号码
        /// </summary>
        /// <param name="?"></param>
        /// <returns></returns>
        public static bool IsTelephone(string str_telephone) {
            return System.Text.RegularExpressions.Regex.IsMatch(str_telephone, @"^(\d{3,4}-)?\d{6,8}$");
        }

        /// <summary>
        /// 验证手机号码
        /// </summary>
        /// <param name="str_handset"></param>
        /// <returns></returns>
        public static bool IsHandset(string str_handset) {
            return System.Text.RegularExpressions.Regex.IsMatch(str_handset, @"^[1]+[3,5,8,7]+\d{9}");
        }
        /// <summary>
        /// 验证Url
        /// </summary>
        /// <param name="str_url"></param>
        /// <returns></returns>
        public static bool IsUrl(string str_url)
        {
            bool IsExist = false;
            try
            {
                HttpWebRequest request = (HttpWebRequest)WebRequest.Create(new Uri(str_url));
                ServicePointManager.Expect100Continue = false;
                ((HttpWebResponse)request.GetResponse()).Close();
                IsExist = true;
            }
            catch (Exception exception)
            {
                IsExist = false;
            }

            return IsExist;

        }
        /// <summary>
        /// 过滤危险脚本
        /// </summary>
        /// <param name="content"></param>
        /// <returns></returns>
        public static bool IsContent(string content)
        {
            //@"<script[\s\s]+</script *>"
            if (System.Text.RegularExpressions.Regex.IsMatch(content, @"/<script.*?>.*?<\/script>/ig", RegexOptions.IgnoreCase))
            {
                return false;
            }

            if (System.Text.RegularExpressions.Regex.IsMatch(content, @" (href|src).*=.*script:", RegexOptions.IgnoreCase))
            {
                return false;
            }
            if (System.Text.RegularExpressions.Regex.IsMatch(content, @"(?i)(?<=<[^>]*?\s+?)on[a-z]+?=(['""]?).*?\1(?=\s+?|/)", RegexOptions.IgnoreCase))
            {
                return false;
            }
            //if (System.Text.RegularExpressions.Regex.IsMatch(content, @"<iframe[\s\s]+</iframe *>", RegexOptions.IgnoreCase))
            //{
            //    return false;
            //}
            //if (System.Text.RegularExpressions.Regex.IsMatch(content, @"<frameset[\s\s]+</frameset *>", RegexOptions.IgnoreCase))
            //{
            //    return false;
            //}
            return true;
        }
        #endregion

    }
}