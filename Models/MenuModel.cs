using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace WebManager.Models
{
    public class MenuModel
    {
        public int Key { get; set; }
        public string Name { get; set; }
        public string ActionName { get; set; }
        public string ControllerName { get; set; }
        public string ImageUrl { get; set; }
        public List<MenuModel> NextNode { get; set; }
    }
}