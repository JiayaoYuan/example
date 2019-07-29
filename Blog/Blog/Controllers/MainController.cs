using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Data;
using System.Data.Entity;
using Blog.Models;
using System.Net;
using Blog.App_Start;
using Blog.tools;

namespace Blog.Controllers
{
    public class MainController : Controller
    {
        private db_BlogEntities db = new db_BlogEntities();
        // GET: YjLihouT
        public ActionResult Index(string n ,tb_users user)
        {
            tb_users new_user;
            tb_users n_user;
            if (n != null) {
                n_user = db.tb_users.SqlQuery("select * from tb_users where user_name = '" + n + "'").FirstOrDefault();
                Handler.loginid = n_user.user_id;
                if (n_user != null) {
                    return View(n_user);
                }
            }
            if (user.user_name != null)
            {                
                new_user = db.tb_users.SqlQuery("select * from tb_users where user_name = '" + user.user_name + "'").FirstOrDefault();
                new_user.user_login_count++;
                db.Entry(new_user).State = EntityState.Modified;
                db.SaveChanges();
                new_user.user_ip = GetIPAndMac.ipTrue();
                return View(new_user);               
            }
            n_user = db.tb_users.SqlQuery("select * from tb_users where user_name = '游客'").FirstOrDefault();
            return View(n_user);
        }

        public ActionResult Comment()
        {
            return View();
        }

        public ActionResult Flink(int page)
        {
            tb_friend_links[] flinks;
            if (page != 0)
            {
                flinks = db.tb_friend_links.SqlQuery("select top 15 * from tb_friend_links where friend_link_id not in (select top " + ((page - 1) * 15) + " friend_link_id from tb_friend_links order by friend_link_id desc) order by friend_link_id desc").ToArray();
                for (int i = 0; i < flinks.Length; i++)
                {
                    //flinks[i].friend_links = flinks[i].friend_links.Replace("<a href ='", "");
                    flinks[i].friend_links = flinks[i].friend_links.Remove(flinks[i].friend_links.IndexOf("/'"));
                    flinks[i].friend_links = flinks[i].friend_links.Remove(0, flinks[i].friend_links.IndexOf("'") + 1);
                }
                HttpContext.Session["page"] = page;
                return View(flinks);
            }
            flinks = db.tb_friend_links.SqlQuery("select top 15 * from tb_friend_links where friend_link_id = 0").ToArray();
            HttpContext.Session["page"] = page;
            return View(flinks);
        }

        [HttpPost]
        public ActionResult Flink([Bind(Include = "friend_link_id, friend_links, friend_link_name, friend_link_description, friend_link_logo")] tb_friend_links tb_friend_links)
        {
            return View();
        }

        public ActionResult AddFlink()
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult AddFlink([Bind(Include = "friend_link_id, friend_links, friend_link_name, friend_link_description, friend_link_logo")] tb_friend_links tb_friend_links)
        {
            string target = "";
            string rel = "";
            string url = "";
            switch (HttpContext.Request.Form["target"]) {
                case "0":
                    target = "_blank";
                    break;
                case "1":
                    target = "_self";
                    break;
                case "2":
                    target = "_top";
                    break;
            }
            switch (HttpContext.Request.Form["rel"])
            {
                case "0":
                    rel = "nofollow";
                    break;
                case "1":
                    rel = "none";
                    break;
            }
            url = "<a href = '" + HttpContext.Request.Form["url"] + "' target = '" + target + "' ref = '" + rel + "'><img src = '" + HttpContext.Request.Form["imgurl"] + "' alt='' />" + HttpContext.Request.Form["name"] + "</a>";
            tb_friend_links friend_links = new tb_friend_links();
            friend_links.friend_link_name = HttpContext.Request.Form["name"];
            friend_links.friend_links = url;
            friend_links.friend_link_logo = HttpContext.Request.Form["imgurl"];
            friend_links.friend_link_description = HttpContext.Request.Form["describe"];
            db.tb_friend_links.Add(friend_links);
            db.SaveChanges();
            return RedirectToAction("Flink");
        }

        public ActionResult DeleteFlink(long? id) {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            tb_friend_links friend_links = db.tb_friend_links.Find(id);
            if (friend_links == null)
            {
                return HttpNotFound();
            }
            db.tb_friend_links.Remove(friend_links);
            db.SaveChanges();
            return RedirectToAction("Flink");
        }

        [HttpPost]
        public ActionResult DeleteFlinkAll()
        {
            tb_friend_links[] flinks = db.tb_friend_links.SqlQuery("select * from tb_friend_links").ToArray();
            foreach (tb_friend_links item in flinks)
            {
                string checkbox = HttpContext.Request.Form["checkbox&" + item.friend_link_id];
                if (checkbox != null)
                {
                    db.tb_friend_links.Remove(item);
                    db.SaveChanges();
                }
            }
            return RedirectToAction("Flink");
        }

        public ActionResult Notice(int page) {
            tb_announcement[] announces;
            if (page != 0)
            {
                announces = db.tb_announcement.SqlQuery("select top 15 * from tb_announcement where announce_id not in (select top " + ((page - 1) * 15) + " announce_id from tb_announcement order by announce_id desc) order by announce_id desc").ToArray();
                HttpContext.Session["page"] = page;
                return View(announces);
            }
            announces = db.tb_announcement.SqlQuery("select top 15 * from tb_announcement where announce_id = 0").ToArray();
            HttpContext.Session["page"] = page;
            return View(announces);
        }

        public ActionResult DeleteNotice(long? id) {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            tb_announcement announce = db.tb_announcement.Find(id);
            if (announce == null)
            {
                return HttpNotFound();
            }
            db.tb_announcement.Remove(announce);
            db.SaveChanges();
            return RedirectToAction("Notice");
        }

        [HttpPost]
        public ActionResult DeleteNoticeAll() {            
            tb_announcement[] announce = db.tb_announcement.SqlQuery("select * from tb_announcement").ToArray();
            foreach(tb_announcement item in announce) {
                string checkbox = HttpContext.Request.Form["checkbox&"+item.announce_id];
                if (checkbox != null) {
                    db.tb_announcement.Remove(item);
                    db.SaveChanges();
                }
            }
            return RedirectToAction("Notice");
        }

        public ActionResult AddNotice() {
            return View();
        }

        [HttpPost]
        [ValidateInput(false)]
        [ValidateAntiForgeryToken]
        public ActionResult AddNotice([Bind(Include = "announce_id, announce_title, announce_content, announce_publish_time")] tb_announcement tb_announcement) {
            tb_announcement.announce_title = HttpContext.Request.Form["title"];
            tb_announcement.announce_content = HttpContext.Request.Form["content"];
            tb_announcement.announce_publish_time = DateTime.Now.GetDateTimeFormats('M')[0].ToString();
            db.tb_announcement.Add(tb_announcement);
            db.SaveChanges();
            return RedirectToAction("Notice");
        }
    }
}