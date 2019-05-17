using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Blog.Controllers
{
    public class HomeController : Controller
    {
        public ActionResult Index()           //博客首页
        {
            return View();
        }

        public ActionResult Category()       //博客栏目页面
        {
            return View();
        }

        public ActionResult Links()          //博客友情链接
        {
            return View();
        }

        public ActionResult Readers()        //博客读者墙
        {
            return View();
        }

        public ActionResult Tags()           //博客标签云
        {
            return View();
        }
    }
}