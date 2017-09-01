using CYTX.Utility.Utils;
using DGBank.BLL;
using DGBank.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using WebManager.Models;
using WebManager.Utility;

namespace WebManager.Controllers
{
    public class AccountController : Controller
    {
        //
        // GET: /Account/
        private static readonly TblManagerUserBLL userbll = new TblManagerUserBLL();
        private TblManagerRoleMgrBLL rolebll = new TblManagerRoleMgrBLL();//角色权限bll
        [HttpGet]
        [AllowAnonymous]
        public ActionResult Login()
        {
            return View();
        }
        [AllowAnonymous]
        public ActionResult GetValidateCode()
        {
            ValidateCode vCode = new ValidateCode();
            string code = vCode.CreateValidateCode(4);
            Session["ValidateCode"] = code;
            byte[] bytes = vCode.CreateValidateGraphic(code);
            return File(bytes, @"image/jpeg");
        }
        [HttpPost]
        [AllowAnonymous]
        //[ValidateAntiForgeryToken]
        public ActionResult LoginAction(LoginModel model)
        {
            if (model.VerifyCode != Session["ValidateCode"].ToString())
            {
                ModelState.AddModelError("ValidateCode", "验证码不正确.");
                return Json(new { message = "验证码不正确", messagetype = "error" });
            }
            if (ModelState.IsValid)
            {
                string md5Pswd = CodeEncryption.Md5Encryption(model.UserPwd);
                TblManagerUser users = null;
                int result = userbll.Login(model.UserName, md5Pswd, out users);
                if (result == 0)
                {
                    Session["DGBank_LOGIN_SESSION"] = users.LoginName;
                    int code = int.Parse(users.RoleCode.ToString());
                    Session["DGBank_CurrentRole"] = rolebll.GetInfoByCode(code);
                    return Json(new { message = Url.Action("Index", "Home"), messagetype = "ok" });
                }
                else
                {

                    ModelState.AddModelError("Login", "用户名或密码错误！");
                }
            }

            // 如果我们进行到这一步时某个地方出错，则重新显示表单

            return Json(new { message = "用户名或密码错误", messagetype = "error" } );
        }
    }
}
