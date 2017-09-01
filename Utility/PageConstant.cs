using CYTX.Utility;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using WebManager.Models;

namespace WebManager.Utility
{
    public class PageConstant
    {
        /// <summary>
        /// 管理网站权限管理字典
        /// </summary>
        public static readonly EManagerAuthority ManagerAuthority = new EManagerAuthority();

        /// <summary>
        /// 管理网站权限字典
        /// </summary>
        public class EManagerAuthority
        {

            public List<MenuModel> Menus = new  List<MenuModel>();
            public EManagerAuthority()
            {
                var fundProjects = new MenuModel() { Name = "基金项目后台管理", ImageUrl = "001.png" };
                fundProjects.NextNode = new List<MenuModel>();
                fundProjects.NextNode.Add(new MenuModel() { Key=1, Name = "广告设置", ActionName = "ADSettings", ControllerName = "ADSettings" });
                fundProjects.NextNode.Add(new MenuModel() { Key = 2, Name = "基金推荐设置", ActionName = "RecommandList", ControllerName = "FundRecommandSetting" });
                fundProjects.NextNode.Add(new MenuModel() { Key = 3, Name = "基金组合设置", ActionName = "FundCombinesList", ControllerName = "FundCombines" });
                fundProjects.NextNode.Add(new MenuModel() { Key = 4, Name = "定投宝典设置", ActionName = "CastSurelyBibleList", ControllerName = "CastSurelyBible" });

                var log = new MenuModel() { Name = "日志管理", ImageUrl = "002.png" };
                log.NextNode = new List<MenuModel>();
                log.NextNode.Add(new MenuModel() { Key = 5, Name = "日志列表", ActionName = "LogList", ControllerName = "LogManage" });

                var manage = new MenuModel() { Name = "管理用户", ImageUrl = "005.png" };
                manage.NextNode = new List<MenuModel>();
                manage.NextNode.Add(new MenuModel() { Key = 6, Name = "用户管理", ActionName = "UserList", ControllerName = "Manager" });
                manage.NextNode.Add(new MenuModel() { Key = 7, Name = "权限管理", ActionName = "RoleMgrList", ControllerName = "Manager" });
                Menus.Add(fundProjects);
                Menus.Add(log);
                Menus.Add(manage);
            }
        /// <summary>
        /// 当前登录用户拥有的权限
        /// </summary>
            public IEnumerable<MenuModel> OwnerMenus;
        }

    }
}