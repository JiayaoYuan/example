﻿using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Data.Entity.Infrastructure;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using Blog.Models;
using Blog.App_Start;
using Blog.tools;

namespace Blog.Controllers
{
    public class ArticlesController : Controller
    {
        private db_BlogEntities db = new db_BlogEntities();

        // GET: Articles        
        public ActionResult Article(int page)
        {
            tb_articles[] articles;
            if (page != 0)
            {
                articles = db.tb_articles.SqlQuery("select top 15 * from tb_articles where article_id not in (select top " + ((page - 1) * 15) + " article_id from tb_articles order by article_id desc) order by article_id desc").ToArray();
                HttpContext.Session["page"] = page;
                return View(articles);
            }
            articles = db.tb_articles.SqlQuery("select top 15 * from tb_articles where article_id = 0").ToArray();
            HttpContext.Session["page"] = page;
            return View(articles);
        }

        // GET: Articles/Details/5
        public ActionResult Details(long? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            tb_articles tb_articles = db.tb_articles.Find(id);
            if (tb_articles == null)
            {
                return HttpNotFound();
            }
            return View(tb_articles);
        }

        // GET: Articles/Create
        public ActionResult Add()
        {
            return View();
        }

        // POST: Articles/Create
        // 为了防止“过多发布”攻击，请启用要绑定到的特定属性，有关 
        // 详细信息，请参阅 https://go.microsoft.com/fwlink/?LinkId=317598。
        [HttpPost]
        [ValidateAntiForgeryToken]
        [ValidateInput(false)]
        public ActionResult Add([Bind(Include = "article_id,user_id,article_title,article_content,article_description,article_label_img,article_views,article_comment_count,article_date,article_like_count")] tb_articles tb_articles)
        {
            if (ModelState.IsValid)
            {
                tb_articles.user_id = 1;
                tb_articles.article_title = HttpContext.Request.Form["title"];
                tb_articles.article_content = HttpContext.Request.Form["content"];
                tb_articles.article_description = HttpContext.Request.Form["describe"];         //文章描述
                tb_articles.article_label_img = HttpContext.Request.Form["titlepic"];   //文章标签图片地址
                tb_articles.article_views = 0;          //流量量
                tb_articles.article_comment_count = 0;  //评论总数
                tb_articles.article_date = Convert.ToDateTime(DateTime.Now.ToString());
                tb_articles.article_like_count = 0;     //点赞数
                //文章设置关键字
                if (HttpContext.Request.Form["keywords"] != "")
                {
                    tb_articles.article_keyword = HttpContext.Request.Form["keywords"];
                }
                db.tb_articles.Add(tb_articles);
                db.SaveChanges();

                tb_articles now_article = db.tb_articles.SqlQuery("select * from tb_articles order by article_id desc").FirstOrDefault();

                //文章设置分类
                tb_set_article_sort article_sort = new tb_set_article_sort();
                article_sort.article_id = now_article.article_id;
                article_sort.sort_id = Convert.ToInt32(HttpContext.Request.Form["category"]);
                db.tb_set_article_sort.Add(article_sort);
                db.SaveChanges();

                //文章设置标签表                
                string[] label;
                if (HttpContext.Request.Form["tags"] != "")/* || HttpContext.Request.Form["keywords"] != ""*/
                {
                    label = HttpContext.Request.Form["tags"].Split(',');
                    List<string> strList = new List<string>(label);
                    for (int i = 0; i < label.Length; i++) {
                        if (label[i] == "") {
                            strList.RemoveAt(i);
                        }
                    }
                    foreach (string item in strList)
                    {
                        tb_labels labels = db.tb_labels.SqlQuery("select * from tb_labels where label_name = '" + item + "'").FirstOrDefault();
                        if (labels != null)
                        {
                            tb_set_article_label article_label = new tb_set_article_label();
                            article_label.article_id = now_article.article_id;  //给定文章id
                            article_label.label_id = labels.label_id;  //给定标签id
                            db.tb_set_article_label.Add(article_label);
                            db.SaveChanges();
                        }
                    }
                }
                return Redirect("/YjLihouT/Articles/Article?page=1");
            }

            return View(tb_articles);
        }

        //GET:Articles/UpdateArticle/5
        public ActionResult UpdateArticle(long? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            tb_articles tb_articles = db.tb_articles.Find(id);
            if (tb_articles == null)
            {
                return HttpNotFound();
            }
            Handler.id = Convert.ToInt32(id);
            return View(tb_articles);
        }

        // POST: Articles/UpdateArticle/5
        // 为了防止“过多发布”攻击，请启用要绑定到的特定属性，有关 
        // 详细信息，请参阅 https://go.microsoft.com/fwlink/?LinkId=317598。
        [HttpPost]
        [ValidateAntiForgeryToken]
        [ValidateInput(false)]
        public ActionResult UpdateArticle([Bind(Include = "article_id,user_id,article_title,article_content,article_description,article_label_img,article_views,article_comment_count,article_date,article_like_count")] tb_articles tb_articles,long? id)
        {
            if (ModelState.IsValid)
            {
                //修改需要对主键赋值，注意：这里需要对所有字段赋值，没有赋值的字段会用NULL更新到数据库
                tb_articles = db.tb_articles.SqlQuery("select * from tb_articles where article_id = '" + id + "'").FirstOrDefault();
                tb_articles.article_id = Convert.ToInt32(id);
                tb_articles.article_title = HttpContext.Request.Form["title"];
                tb_articles.article_content = HttpContext.Request.Form["content"];
                tb_articles.article_date = Convert.ToDateTime(DateTime.Now.ToString());
                tb_articles.article_description = HttpContext.Request.Form["describe"];         //文章描述
                tb_articles.article_label_img = HttpContext.Request.Form["titlepic"];   //文章标签图片地址
                tb_articles.article_keyword = HttpContext.Request.Form["keywords"];     //文章关键字

                //将实体附加到对象管理器中
                db.tb_articles.Attach(tb_articles);
                //获取到user的状态实体，可以修改其状态
                var setEntry = ((IObjectContextAdapter)db).ObjectContext.ObjectStateManager.GetObjectStateEntry(tb_articles);                
                setEntry.SetModifiedProperty("article_title");
                setEntry.SetModifiedProperty("article_content");
                setEntry.SetModifiedProperty("article_date");
                if (tb_articles.article_label_img != "") {
                    setEntry.SetModifiedProperty("article_label_img");
                }
                if (tb_articles.article_description != "") {
                    setEntry.SetModifiedProperty("article_description");
                }
                if (tb_articles.article_keyword != "") {
                    setEntry.SetModifiedProperty("article_keyword");
                }
                //文章设置标签表          
                string[] labels;
                tb_set_article_label[] article_labels = db.tb_set_article_label.SqlQuery("select * from tb_set_article_label where article_id =" + tb_articles.article_id).ToArray();
                if (HttpContext.Request.Form["tags"] != "")/* || HttpContext.Request.Form["keywords"] != ""*/
                {
                    labels = HttpContext.Request.Form["tags"].Split(',');
                    List<string> strList = new List<string>(labels);
                    for (int i = 0; i < labels.Length; i++)
                    {
                        if (labels[i] == "")
                        {
                            strList.RemoveAt(i);
                        }
                    }
                    if (article_labels.Length == strList.Count()) {
                        foreach (string item in strList)
                        {
                            tb_labels label = db.tb_labels.SqlQuery("select * from tb_labels where label_name = '" + item + "'").FirstOrDefault();
                            if (label != null) {
                                ArticleLabel(tb_articles, label, item);
                                db.SaveChanges();
                            }                            
                        }
                    }
                    if (article_labels.Length > strList.Count())
                    {                        
                        for (int i = 0; i < article_labels.Length; i++)
                        {                            
                            tb_labels label = db.tb_labels.Find(article_labels[i].label_id);                            
                            for (int j = 0; j < strList.Count(); j++)
                            {
                                if (label.label_name != strList[j])
                                {
                                    db.tb_set_article_label.Remove(article_labels[i]);
                                    db.SaveChanges();
                                }
                            }
                        }
                        //for (int i = 0; i < labels.Length; i++)
                        //{
                        //    tb_labels label = db.tb_labels.SqlQuery("select * from tb_labels where label_name = '" + labels[i] + "'").FirstOrDefault();
                        //    ArticleLabel(tb_articles, label, labels[i]);
                        //    db.SaveChanges();
                        //}
                    }
                    if (article_labels.Length < strList.Count())
                    {                        
                        for(int i = article_labels.Length; i < strList.Count(); i++)
                        {
                            for (int j = 0; j < strList.Count(); j++)
                            {
                                for (int k = 0; k < article_labels.Length; k++)
                                {
                                    tb_labels label2 = db.tb_labels.SqlQuery("select * from tb_labels where label_id = '" + article_labels[k].label_id + "'").FirstOrDefault();
                                    if (label2.label_name != strList[j])
                                    {
                                        tb_labels label = db.tb_labels.SqlQuery("select * from tb_labels where label_name = '" + strList[j] + "'").FirstOrDefault();
                                        if (label != null) {
                                            tb_set_article_label article_Label = new tb_set_article_label();
                                            article_Label.article_id = tb_articles.article_id;
                                            article_Label.label_id = label.label_id;
                                            db.tb_set_article_label.Add(article_Label);
                                            db.SaveChanges();
                                        }
                                    }
                                }
                            };                            
                        }                        
                    }
                };

                return Redirect("/YjLihouT/Articles/Article?page=1");
            }
            return View(tb_articles);
        }

        //改变文章标签
        public void ArticleLabel(tb_articles articles, tb_labels label, string item) {             
            if (label != null)
            {
                tb_set_article_label article_label = db.tb_set_article_label.SqlQuery("select * from tb_set_article_label where label_id =" + label.label_id).FirstOrDefault(); //指定文章标签id                            
                article_label.label_id = label.label_id;  //给定标签id
                article_label.article_id = articles.article_id; //给文章id
                db.tb_set_article_label.Attach(article_label);
                db.Entry(article_label).State = EntityState.Modified;
                db.SaveChanges();
            }
            else
            {
                tb_labels add_label = new tb_labels();
                add_label.label_name = item;
                tb_sorts sorts = db.tb_sorts.SqlQuery("select * from tb_sorts where sort_id = " + Convert.ToInt32(HttpContext.Request.Form["category"])).FirstOrDefault();
                add_label.label_alias = sorts.sort_name;
                add_label.label_description = "";
                db.tb_labels.Add(add_label);
                db.SaveChanges();
            }
        }

        // GET: Articles/Delete/5
        public ActionResult Delete(long? id)
        {
            tb_articles tb_articles = db.tb_articles.Find(id);
            tb_set_article_sort set_sort = db.tb_set_article_sort.SqlQuery("select * from tb_set_article_sort where article_id =" + id).FirstOrDefault();
            db.tb_set_article_sort.Remove(set_sort);
            tb_set_article_label[] set_label = db.tb_set_article_label.SqlQuery("select * from tb_set_article_label where article_id =" + id).ToArray();
            foreach (var item in set_label) {
                db.tb_set_article_label.Remove(item);
            }
            db.tb_articles.Remove(tb_articles);
            db.SaveChanges();

            CommanFile actionFile = new CommanFile();
            actionFile.DeleteFile(Convert.ToString(id + 1000));
            return Redirect("/YjLihouT/Articles/Article?page=1");
        }

        [HttpPost]
        public ActionResult DeleteAll()
        {
            tb_articles[] articles = db.tb_articles.SqlQuery("select * from tb_articles").ToArray();
            foreach (tb_articles item in articles)
            {
                string checkbox = HttpContext.Request.Form["checkbox&" + item.article_id];
                if (checkbox != null)
                {
                    tb_set_article_sort set_sort = db.tb_set_article_sort.SqlQuery("select * from tb_set_article_sort where article_id =" + item.article_id).FirstOrDefault();
                    db.tb_set_article_sort.Remove(set_sort);
                    tb_set_article_label[] set_label = db.tb_set_article_label.SqlQuery("select * from tb_set_article_label where article_id =" + item.article_id).ToArray();
                    foreach (var item2 in set_label)
                    {
                        db.tb_set_article_label.Remove(item2);
                    }
                    db.tb_articles.Remove(item);
                    db.SaveChanges();

                    CommanFile actionFile = new CommanFile();
                    actionFile.DeleteFile(Convert.ToString(item.article_id + 1000));                    
                }
            }
            return Redirect("/YjLihouT/Articles/Article?page=1");
        }

        // POST: Articles/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(long id)
        {
            tb_articles tb_articles = db.tb_articles.Find(id);
            db.tb_articles.Remove(tb_articles);
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
