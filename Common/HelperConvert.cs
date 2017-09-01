using DGBank.BLL;
using DGBank.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace WebManager.Common
{
    public class HelperConvert
    {

        /// <summary>
        /// displayField Convert
        /// </summary>
        /// <param name="displayFiled">displayFiled</param>
        /// <returns></returns>
        public static string DisplayFieldConv(object displayFiled)
        {
            string field = displayFiled == null ? string.Empty : displayFiled.ToString();
            if (field.Length == 0) return "";//"(none)";

            if (field.Equals("Month"))
                return Comm.Common.NEARLYMONTH;
            if (field.Equals("ThreeMonth"))
                return Comm.Common.NEARLYTHREEMONTH;
            if (field.Equals("SixMonth"))
                return Comm.Common.NEARLYSIXMONTH;
            if (field.Equals("OneYear"))
                return Comm.Common.NEARLYYEAR;
            if (field.Equals("ThreeYear"))
                return Comm.Common.NEARLTHREEYYEAR;
            if (field.Equals("Establish"))
                return Comm.Common.NEARLESTABLISH;

            return "";//"(none)";
        }
        /// <summary>
        /// 展示组合类型
        /// </summary>
        /// <param name="displayFiled"></param>
        /// <returns></returns>
        public static string DisplayCombineType(object displayFiled)
        {
            string field = displayFiled == null ? string.Empty : displayFiled.ToString();
            if (field.Length == 0) return "固定公司";//"(none)";

            if (field.Equals("0"))
                return "固定公司";
            if (field.Equals("1"))
                return "自由组合";
            return "固定公司";//"(none)";
        }

        /// <summary>
        /// 得到运行的天数【包括节假日】
        /// </summary>
        /// <param name="createTime"></param>
        /// <returns></returns>
        public static string GetRunDay(object createTime)
        {
            string time = createTime == null ? string.Empty : createTime.ToString();

            if (time.Length == 0) return "";//"(none)";

            return string.Format("{0}天", new TimeSpan(DateTime.Now.Ticks - DateTime.Parse(time).Ticks).Days + 1);//创建基金组合
        }
        /// <summary>
        /// substring str
        /// </summary>
        /// <param name="name"></param>
        /// <param name="length"></param>
        /// <returns></returns>
        public static string GetSubStr(object name, int length)
        {
            return Comm.Common.GetCut(name, length);
        }
        /// <summary>
        /// 根据orgCode得到manageOrg
        /// </summary>
        /// <param name="orgCode"></param>
        /// <returns></returns>
        public static string GetOrgName(object CombineType, object orgCode, int length)
        {
            if (CombineType.ToString() == "0")
            {
                string code = orgCode == null ? string.Empty : orgCode.ToString();
                if (code.Length == 0) return "";// "(none)";
                FundInfoBLL fundBll = new FundInfoBLL();//基金bll
                FundInfo info = fundBll.GetManageOrg(int.Parse(code));
                if (null == info) return "";

                if (string.IsNullOrEmpty(info.ManageOrg)) return "";

                return Comm.Common.GetCut(info.ManageOrg, length);
            }
            else
            {
                return orgCode.ToString();
            }
        }
    }
}