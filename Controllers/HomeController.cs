using DGBank.BLL;
using DGBank.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Security;
using WebManager.Filters;
using WebManager.Models;
using WebManager.Utility;

namespace WebManager.Controllers
{
    [PermissionRequired]
    public class HomeController : Controller
    {
        //
        // GET: /Home/

        public ActionResult Index()
        {
            return View();
        }
      
        private TblManagerRoleMgrBLL rolebll = new TblManagerRoleMgrBLL();//角色权限bll
        [HttpPost]
        public ActionResult PartialLeft()
        {
            //权限控制
            try
            {
                if (null == Session["DGBank_CurrentRole"]) return PartialView(new List<MenuModel>());
                TblManagerRoleMgr roleEnity = (TblManagerRoleMgr)Session["DGBank_CurrentRole"];
                if (null == roleEnity) return PartialView(new List<MenuModel>());
                var pageconstant = PageConstant.ManagerAuthority;

                string[] authority = roleEnity.Authority.Split(',');
                var AuthorityMenus = pageconstant.Menus.ToList();
                foreach (var key in AuthorityMenus)
                {
                    var item = key.NextNode.Where(e => authority.Contains(e.Key.ToString()));
                    key.NextNode = item != null ? item.ToList() : null;
                }
                var result = AuthorityMenus.Where(e => e.NextNode != null && e.NextNode.Any());
                PageConstant.ManagerAuthority.OwnerMenus = result ?? new List<MenuModel>();
                return PartialView(PageConstant.ManagerAuthority.OwnerMenus);
            }
            catch (Exception ex)
            {
                
                throw ex;
            }
            
        }
        public ActionResult Welcome()
        {
            return PartialView();
        }
        [HttpPost]
        public ActionResult Exit()
        {
            Session.RemoveAll();
            FormsAuthentication.SignOut();
            Session.Abandon();
            return Content("<script>top.location.href='" + Url.Action("Index", "Home") + "';</script>");
        }




    }
}
