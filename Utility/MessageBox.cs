/*
Author:WangXinXin
Create date: 2017/2/7 17:45:47
Description：消息提示对话框
--------------------------------------------------------------------------------------------------------
Versions：
    V1.00 2017/2/7 17:45:47 WangXinXin  Add
*/
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Web;
using System.Web.UI;

namespace WebManager.Utility
{
    public class MessageBox
    {
        private MessageBox()
        {
        }

        #region 注册脚本对话框

        /// <summary>
        /// 在页面中，弹出消息提示对话框
        /// </summary>
        /// <param name="page">页面</param>
        /// <param name="message">提示信息</param>
        public static void ShowDialog(System.Web.UI.Page page, string message)
        {
            page.ClientScript.RegisterStartupScript(page.GetType(),
                "message", "<script language='javascript' defer>alert('" + StringDealwith.StringFormat(message) + "');</script>");
        }
        /// <summary>
        /// 在页面中，弹出消息提示对话框  用在updatepanel中，
        /// </summary>
        /// <param name="page">页面</param>
        /// <param name="message">提示信息</param>
        public static void ShowDialogAjax(System.Web.UI.Page page, string message)
        {
            ScriptManager.RegisterStartupScript(page, page.GetType(), "", "alert('" + StringDealwith.StringFormat(message) + "');", true);
        }
        /// <summary>
        ///  在页面中，弹出消息提示对话框  用在updatepanel中，同时可以刷新当前页面或者跳转到其他页面
        /// </summary>
        /// <param name="page">页面</param>
        /// <param name="message">提示信息</param>
        /// <param name="redirect">空(不跳转)； Refresh(刷新)；其他（跳转到传入的页面）</param>
        public static void ShowDialogAjax(System.Web.UI.Page page, string message, string redirect)
        {
            if (redirect == "")
            {
                ScriptManager.RegisterStartupScript(page, page.GetType(), "", "alert('" + StringDealwith.StringFormat(message) + "');", true);
            }
            else if (redirect == "Refresh")
            {
                ScriptManager.RegisterStartupScript(page, page.GetType(), "", "alert('" + StringDealwith.StringFormat(message) + "');self.location='" + System.IO.Path.GetFileName(page.Request.PhysicalPath) + "';", true);
            }
            else
            {
                ScriptManager.RegisterStartupScript(page, page.GetType(), "", "alert('" + StringDealwith.StringFormat(message) + "');self.location='" + redirect + "';", true);
            }
        }
        public static void ShowDialogAjaxForLoginTimeout(System.Web.UI.Page page)
        {
            ScriptManager.RegisterStartupScript(page, page.GetType(), "", "alert('登录超时');", true);
            page.Response.Redirect("~//Account/Login.aspx");//登录超时显示登录界面
        }
        /// <summary>
        /// 单击控件，弹出消息的确认提示框
        /// </summary>
        /// <param name="control">控件</param>
        /// <param name="message">提示消息</param>
        public static void ShowConfirmDialog(System.Web.UI.WebControls.WebControl control, string m)
        {
            MessageCoding mc = new MessageCoding(true);
            //通过代码得到消息内容
            string message = mc[m];
            control.Attributes.Add("onclick", "return confirm('" + message + "');");
        }

        /// <summary>
        /// 在页面中，显示消息提示对话框，并进行页面跳转
        /// </summary>
        /// <param name="page">页面</param>
        /// <param name="message">提示信息</param>
        /// <param name="url">目标URL</param>
        public static void ShowDialogAndRedirect(System.Web.UI.Page page, string m, string url)
        {
            MessageCoding mc = new MessageCoding(true);
            //通过代码得到消息内容
            string message = mc[m];

            page.ClientScript.RegisterStartupScript(page.GetType(),
                "message", "<script language='javascript' defer>alert('" + message + "');window.location=\"" + url + "\"</script>");
        }
        /// <summary>
        /// 在页面中，显示消息提示对话框，并进行页面跳转到顶级页面（Target的值为top）
        /// </summary>
        /// <param name="page">页面</param>
        /// <param name="message">提示信息</param>
        /// <param name="url">目标URL</param>
        public static void ShowDialogAndTopRedirect(System.Web.UI.Page page, string m, string url)
        {
            MessageCoding mc = new MessageCoding(true);
            //通过代码得到消息内容
            string message = mc[m];

            StringBuilder sbScriptString = new StringBuilder();
            sbScriptString.Append("<script language='javascript' defer>");
            sbScriptString.AppendFormat("alert('{0}');", message);
            sbScriptString.AppendFormat("top.location.href='{0}'", url);
            sbScriptString.Append("</script>");
            page.ClientScript.RegisterStartupScript(page.GetType(), "message", sbScriptString.ToString());
        }

        #endregion

        #region  ymPrompt对话框

        /// <summary>
        /// 显示消息提示对话框 
        /// </summary>
        /// <param name="page">页面</param>
        /// <param name="title">标题</param>
        /// <param name="message">提示消息</param>		
        public static void YMPromptShowDialog(System.Web.UI.Page page, string t, string m)
        {
            MessageCoding mc = new MessageCoding(true);
            ///通过代码得到消息标题
            string title = mc[t];
            //通过代码得到消息内容
            string message = mc[m];

            page.ClientScript.RegisterStartupScript(page.GetType(),
                "message",
                "<script language='javascript' >ymPrompt.setDefaultCfg({iframe:false}); ymPrompt.alert({title:'" + title + "',message:'" + message + "',height:130,width:220});</script>");
        }

        /// <summary>
        /// 显示消息提示对话框，并设置对话框的高度和宽度
        /// </summary>
        /// <param name="page">页面</param>
        /// <param name="title">标题</param>
        /// <param name="message">提示消息</param>
        /// <param name="width">宽度</param>
        /// <param name="height">高度</param>
        public static void YMPromptShowDialog(System.Web.UI.Page page, string t, string m,
            int width, int height)
        {
            MessageCoding mc = new MessageCoding(true);
            ///通过代码得到消息标题
            string title = mc[t];
            //通过代码得到消息内容
            string message = mc[m];

            page.ClientScript.RegisterStartupScript(page.GetType(),
                "message",
                "<script language='javascript' >ymPrompt.setDefaultCfg({iframe:false}); ymPrompt.alert({title:'" + title
                + "',message:'" + message + "',height:" + height + ",width:" + width + "});</script>");
        }
        /// <summary>
        /// 用于updatepanel里面的弹出提示框
        /// </summary>
        /// <param name="page"></param>
        /// <param name="message"></param>
        public static void YMPromptShowDialog(System.Web.UI.Page page, string message)
        {
            ScriptManager.RegisterStartupScript(page, page.GetType(), "", "ymPrompt.setDefaultCfg({iframe:false}); ymPrompt.alert({title:'提示',message:'" + message + "',height:130,width:200});", true);
        }
        /// <summary>
        /// 显示带有【确定】、【取消】按钮的消息提示对话框
        /// </summary>
        /// <param name="page">页面</param>
        /// <param name="title">标题</param>
        /// <param name="message">提示消息</param>
        public static void YMPromptShowDialogWithOKCancel(System.Web.UI.Page page, string t, string m)
        {
            MessageCoding mc = new MessageCoding(true);
            ///通过代码得到消息标题
            string title = mc[t];
            //通过代码得到消息内容
            string message = mc[m];

            page.ClientScript.RegisterStartupScript(page.GetType(),
                "message",
                "<script language='javascript' >ymPrompt.setDefaultCfg({ iframe:false,autoClose: false }); ymPrompt.alert('"
                + message + "',  220, 130, '" + title + "', function(tp) { if (tp == 'ok') {  parent.history.go(0); } else { parent.history.go(0); } return false;});</script>");
        }

        /// <summary>
        /// 显示带有【确定】按钮的消息提示对话框
        /// </summary>
        /// <param name="page">页面</param>
        /// <param name="title">标题</param>
        /// <param name="message">提示消息</param>
        public static void YMPromptShowDialogWithOK(System.Web.UI.Page page, string t, string m)
        {
            MessageCoding mc = new MessageCoding(true);
            ///通过代码得到消息标题
            string title = mc[t];
            //通过代码得到消息内容
            string message = mc[m];

            page.ClientScript.RegisterStartupScript(page.GetType(),
                "message",
                "<script language='javascript' >ymPrompt.setDefaultCfg({ iframe:false,autoClose: false }); ymPrompt.alert('"
                + message + "',  220, 130, '" + title + "', function(tp) { if (tp == 'ok') {  history.go(-1); } else { history.go(-1);} return false;});</script>");
        }
        public static void YMPromptShowDialogWithEdit(System.Web.UI.Page page, string t, string m)
        {
            MessageCoding mc = new MessageCoding(true);
            ///通过代码得到消息标题
            string title = mc[t];
            //通过代码得到消息内容
            string message = mc[m];

            page.ClientScript.RegisterStartupScript(page.GetType(),
                "message",
                "<script language='javascript' >ymPrompt.setDefaultCfg({ iframe:false,autoClose: false }); ymPrompt.alert('"
                + message + "',  220, 130, '" + title + "', function(tp) { if (tp == 'ok') {  history.go(-2); } else { history.go(-2);} return false;});</script>");
        }

        /// <summary>
        /// 显示只包含提示信息的弹出框,并设置对话框的高度和宽度
        /// </summary>
        /// <param name="page">页面</param>
        /// <param name="title">标题</param>
        /// <param name="content">内容</param>		
        /// <param name="width">宽度</param>
        /// <param name="height">高度</param>
        public static void YMPromptShowContentDialog(System.Web.UI.Page page,
            string t, string m, int width, int height)
        {
            MessageCoding mc = new MessageCoding(true);
            ///通过代码得到消息标题
            string title = mc[t];
            //通过代码得到消息内容
            string Content = mc[m];
            page.ClientScript.RegisterStartupScript(page.GetType(),
                "message",
                "<script language='javascript' >ymPrompt.setDefaultCfg({iframe:false}); ymPrompt.win({message:'"
                + Content + "',width:" + width + ",height:" + height + ",msgCls:'ym-content',title:'" + title + "'});</script>");
        }

        /// <summary>
        /// 弹出一个对话框，并在该对话框中显示指定页面的内容
        /// </summary>
        /// <param name="page">页面</param>
        /// <param name="title">标题</param>
        /// <param name="pageUrl">内容页面的Url</param>
        /// <param name="width">宽度</param>
        /// <param name="height">高度</param> 
        public static void YMPromptShowPageContentDialog(System.Web.UI.Page page,
            string t, string pageUrl, int width, int height)
        {
            MessageCoding mc = new MessageCoding(true);
            ///通过代码得到消息标题
            string title = mc[t];
            page.ClientScript.RegisterStartupScript(page.GetType(),
                "message",
                "<script language='javascript' >ymPrompt.setDefaultCfg({iframe:true}); ymPrompt.win('"
                + pageUrl + "'," + width + "," + height + ",'" + title + "');</script>");
        }


        /// <summary>
        /// 弹出一个对话框，并在该对话框中显示指定页面的内容
        /// </summary>
        /// <param name="page">页面</param>
        /// <param name="title">标题</param>
        /// <param name="pageUrl">内容页面的Url</param>
        /// <param name="width">宽度</param>
        /// <param name="height">高度</param> 
        public static void YMPromptParentShowPageContentDialog(System.Web.UI.Page page,
            string t, string pageUrl, int width, int height)
        {
            MessageCoding mc = new MessageCoding(true);
            ///通过代码得到消息标题
            string title = mc[t];

            page.ClientScript.RegisterStartupScript(page.GetType(),
                "message",
                "<script language='javascript' >ymPrompt.setDefaultCfg({iframe:true}); parent.ymPrompt.win('"
                + pageUrl + "'," + width + "," + height + ",'" + title + "');</script>");
        }


        /// <summary>
        /// 关闭当前弹出窗体
        /// </summary>
        public static void YMPromptCloseDialog(System.Web.UI.Page page)
        {
            page.ClientScript.RegisterStartupScript(page.GetType(),
                "message", "<script language='javascript' >ymPrompt.close();</script>");
        }

        /// <summary>
        /// 提示弹出框，点确定后关闭，并刷新父窗体
        /// </summary>
        /// <param name="page">页面</param>
        /// <param name="title">标题</param>
        /// <param name="message">内容</param>
        public static void YMPromptDialogAndFreshParentPage(System.Web.UI.Page page, string t, string m)
        {
            MessageCoding mc = new MessageCoding(true);
            ///通过代码得到消息标题
            string title = mc[t];
            //通过代码得到消息内容
            string message = mc[m];
            page.ClientScript.RegisterStartupScript(page.GetType(),
                "message", "<script language='javascript' >ymPrompt.setDefaultCfg({ autoClose: false ,iframe:false}); ymPrompt.alert('"
                + message + "',  220, 130, '" + title + "', function(tp) { try{ parent.ymPrompt.close(); parent.window.location.href = parent.window.location.href;}catch(err){ ymPrompt.close(); window.location.href = window.location.href;} return false;});</script>");
        }

        /// <summary>
        /// 提示弹出框，点确定后关闭，并刷新父窗体，同时设置对话框的大小
        /// </summary>
        /// <param name="page">页面</param>
        /// <param name="title">提示标题</param>
        /// <param name="message">提示内容</param>
        /// <param name="width">宽度</param>
        /// <param name="height">高度</param>
        public static void YMPromptDialogAndFreshParentPage(System.Web.UI.Page page,
            string t, string m, int width, int height)
        {
            MessageCoding mc = new MessageCoding(true);
            ///通过代码得到消息标题
            string title = mc[t];
            //通过代码得到消息内容
            string message = mc[m];
            page.ClientScript.RegisterStartupScript(page.GetType(),
                "message", "<script language='javascript' >ymPrompt.setDefaultCfg({ autoClose: false ,iframe:false}); ymPrompt.alert('"
                + message + "', " + width + ", " + height + ", '" + title + "', function(tp) { parent.ymPrompt.close(); parent.window.location.href = parent.window.location.href; return false;});</script>");
        }

        /// <summary>
        /// 定义提示消息类型
        /// </summary>
        public class MessageCoding
        {
            public static Hashtable hashtable = new Hashtable();
            public MessageCoding(bool title)
            {
                ///数据库类型的错误
                hashtable["1001"] = "请输入关键字！";
                hashtable["1002"] = "没有查到数据！"; ;
                hashtable["1003"] = "时间格式错误";
                hashtable["1004"] = "出现错误！";
                hashtable["1005"] = "股票代码不在此市场！";
                hashtable["1006"] = "请选择要删除的行！";
                hashtable["1007"] = "删除成功！";
                hashtable["1008"] = "删除失败！";
                hashtable["1009"] = "操作成功！";
                hashtable["1010"] = "操作失败！";
                hashtable["1011"] = "用户不存在！";
                hashtable["1012"] = "参数错误！";
                hashtable["1013"] = "有相同记录！";
                hashtable["1014"] = "请填写完整！";
                hashtable["1015"] = "密码错误！";
                ///弹出框的Title内容
                if (title)
                {
                    hashtable["2001"] = "错误消息";
                }
            }
            public string this[string messCode]
            {
                get
                {   ///判断errorCode是否存在，如果不存在，则返回空字符串
                    if (hashtable.ContainsKey(messCode.ToLower().ToString()) == false) return string.Empty;
                    return hashtable[messCode].ToString();
                }
            }
        }

        #endregion
    }
}