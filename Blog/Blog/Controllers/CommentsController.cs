using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using Blog.Models;

namespace Blog.Controllers
{
    public class CommentsController : Controller
    {
        private db_BlogEntities db = new db_BlogEntities();

        // GET: Comments
        public ActionResult Index()
        {
            var tb_comments = db.tb_comments.Include(t => t.tb_users);
            return View(tb_comments.ToList());
        }

        // GET: Comments/Details/5
        public ActionResult Details(long? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            tb_comments tb_comments = db.tb_comments.Find(id);
            if (tb_comments == null)
            {
                return HttpNotFound();
            }
            return View(tb_comments);
        }

        // GET: Comments/Create
        public ActionResult Create()
        {
            ViewBag.user_id = new SelectList(db.tb_users, "user_id", "user_ip");
            return View();
        }

        // POST: Comments/Create
        // 为了防止“过多发布”攻击，请启用要绑定到的特定属性，有关 
        // 详细信息，请参阅 https://go.microsoft.com/fwlink/?LinkId=317598。
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "comment_id,user_id,article_id,comment_like_count,comment_date,comment_content,parent_comment_id")] tb_comments tb_comments)
        {
            if (ModelState.IsValid)
            {
                db.tb_comments.Add(tb_comments);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            ViewBag.user_id = new SelectList(db.tb_users, "user_id", "user_ip", tb_comments.user_id);
            return View(tb_comments);
        }

        // GET: Comments/Edit/5
        public ActionResult Edit(long? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            tb_comments tb_comments = db.tb_comments.Find(id);
            if (tb_comments == null)
            {
                return HttpNotFound();
            }
            ViewBag.user_id = new SelectList(db.tb_users, "user_id", "user_ip", tb_comments.user_id);
            return View(tb_comments);
        }

        // POST: Comments/Edit/5
        // 为了防止“过多发布”攻击，请启用要绑定到的特定属性，有关 
        // 详细信息，请参阅 https://go.microsoft.com/fwlink/?LinkId=317598。
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "comment_id,user_id,article_id,comment_like_count,comment_date,comment_content,parent_comment_id")] tb_comments tb_comments)
        {
            if (ModelState.IsValid)
            {
                db.Entry(tb_comments).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.user_id = new SelectList(db.tb_users, "user_id", "user_ip", tb_comments.user_id);
            return View(tb_comments);
        }

        // GET: Comments/Delete/5
        public ActionResult Delete(long? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            tb_comments tb_comments = db.tb_comments.Find(id);
            if (tb_comments == null)
            {
                return HttpNotFound();
            }
            return View(tb_comments);
        }

        // POST: Comments/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(long id)
        {
            tb_comments tb_comments = db.tb_comments.Find(id);
            db.tb_comments.Remove(tb_comments);
            db.SaveChanges();
            return RedirectToAction("Index");
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }
    }
}
