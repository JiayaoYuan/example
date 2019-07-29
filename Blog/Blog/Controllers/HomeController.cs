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
        public ActionResult Index(int page, string s)           //博客首页
        {
            tb_articles[] articles;
            if (s != "" && s != null) {
                if (page == 1)
                {
                    articles = db.tb_articles.SqlQuery("select * from tb_articles where article_keyword like '%" + s + "%'").ToArray();
                }
                else {
                    articles = db.tb_articles.SqlQuery("select * from tb_articles where article_id = 0").ToArray();
                }
                HttpContext.Session["SearchName"] = s;
                HttpContext.Session["homepage"] = page;
                return View(articles);
            }
            articles = db.tb_articles.SqlQuery("select top 4 * from tb_articles where article_id not in (select top " + (page - 1) * 4 + " article_id from tb_articles order by article_id desc) order by article_id desc").ToArray();
            tb_articles[] articleCount = db.tb_articles.SqlQuery("select * from tb_articles").ToArray();
            for (int i = 0; i < articleCount.Length; i++)
            {
                string filename = Convert.ToString(articleCount[i].article_id + 1000);
                CommanFile comfile = new CommanFile();
                Handler.id = articleCount[i].article_id;
                comfile.CopyFile(filename);
            }
            s = "";
            HttpContext.Session["SearchName"] = s;
            HttpContext.Session["homepage"] = page;
            return View(articles);
        }

        public ActionResult SortCategory(int page, int sort_id)       //博客栏目页面
        {            
            tb_set_article_sort[] tb_Sets = db.tb_set_article_sort.SqlQuery("select * from tb_set_article_sort where sort_id = " + sort_id).ToArray();
            tb_articles[] articles = new tb_articles[4];
            if (tb_Sets.Length > 0)
            {
                for (int i = (page - 1); i < page; i++)
                {
                    int Out = 0;
                    for (int j = (i * 4); j < tb_Sets.Length; j++) {                        
                        articles[Out] = db.tb_articles.SqlQuery("select * from tb_articles where article_id = " + tb_Sets[j].article_id).FirstOrDefault();
                        Out++;
                        if (Out == 4) {
                            break;
                        }
                    }                    
                }
            }
            HttpContext.Session["sortpage"] = page;
            HttpContext.Session["sort_id"] = sort_id;
            return View(articles);
        }

        public ActionResult LabelCategory(int page, int label_id)       //博客栏目页面
        {
            tb_set_article_label[] tb_Sets = db.tb_set_article_label.SqlQuery("select * from tb_set_article_label where label_id = " + label_id).ToArray();
            tb_articles[] articles = new tb_articles[4];
            if (tb_Sets.Length > 0)
            {
                for (int i = (page - 1); i < page; i++)
                {
                    int Out = 0;
                    for (int j = (i * 4); j < tb_Sets.Length; j++)
                    {
                        articles[Out] = db.tb_articles.SqlQuery("select * from tb_articles where article_id = " + tb_Sets[j].article_id).FirstOrDefault();
                        Out++;
                        if (Out == 4)
                        {
                            break;
                        }
                    }
                }
            }
            HttpContext.Session["labelpage"] = page;
            HttpContext.Session["label_id"] = label_id;
            return View(articles);
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
            tb_articles articles = db.tb_articles.Find(id);
            articles.article_views++;
            db.tb_articles.Attach(articles);
            db.Entry(articles).State = EntityState.Modified;
            db.SaveChanges();

            string filename = Convert.ToString(id + 1000);
            CommanFile comfile = new CommanFile();
            Handler.id = id;
            if (comfile.CopyFileBool(filename))
            {
                return RedirectToAction("../../articles/" + filename + ".html");
            }
            return RedirectToAction("/Home/Index");
        }

    }
}