using System;
using System.Collections.Generic;
using System.Linq;
using System.Data;
using System.Data.Entity;
using System.Data.Entity.Infrastructure;
using System.Net;
using System.Web;
using System.Web.Mvc;
using Blog.Models;
using Blog.tools;
using Blog.App_Start;

namespace Blog.Controllers
{
    public class HomeController : Controller
    {
        db_BlogEntities db = new db_BlogEntities();
        public ActionResult Index(int page)           //博客首页
        {
            tb_articles[] articles;
            if (page > 0)
            {
                articles = db.tb_articles.SqlQuery("select top 1 * from tb_articles where article_id not in (select top " + (page - 1) + " article_id from tb_articles order by article_id) order by article_id").ToArray();
            }
            else {
                articles = db.tb_articles.SqlQuery("select top 0 * from tb_articles where article_id not in (select top 0 article_id from tb_articles order by article_id) order by article_id").ToArray();
            }
            HttpContext.Session["page"] = page; 
            return View(articles);
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

        public ActionResult article(int id)       //文章中转站
        {
            string filename = Convert.ToString(id + 1000);
            CommanFile comfile = new CommanFile();
            Handler.id = id;
            if (comfile.CopyFile(filename))
            {
                return RedirectToAction("../../articles/" + filename + ".html");
            }
            return RedirectToAction("/Home/Index");
        }

    }
}