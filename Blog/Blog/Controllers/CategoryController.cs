using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Data.Entity.Infrastructure;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using Blog.Models;

namespace Blog.Controllers
{
    public class CategoryController : Controller
    {
        private db_BlogEntities db = new db_BlogEntities();
        // GET: Category
        public ActionResult Index()
        {
            return View(db.tb_sorts.ToList());
        }

        // POST: Category/Create
        // 为了防止“过多发布”攻击，请启用要绑定到的特定属性，有关 
        // 详细信息，请参阅 https://go.microsoft.com/fwlink/?LinkId=317598。
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Index([Bind(Include = "sort_id,sort_name,sort_alias,sort_description,parent_sort_id")] tb_sorts tb_sorts)
        {
            if (ModelState.IsValid)
            {
                tb_sorts.sort_name = HttpContext.Request.Form["name"];
                tb_sorts.sort_alias = HttpContext.Request.Form["alias"];
                tb_sorts.parent_sort_id = Convert.ToInt32(HttpContext.Request.Form["fid"]);
                tb_sorts.sort_description = HttpContext.Request.Form["describe"];
                db.tb_sorts.Add(tb_sorts);

                tb_labels Labels = new tb_labels();
                string[] label_name = HttpContext.Request.Form["keywords"].Split(',');
                foreach(string item in label_name)
                {
                    tb_labels new_labels = db.tb_labels.SqlQuery("select * from tb_labels where label_name = '" + item + "'").FirstOrDefault();
                    if (new_labels == null)
                    {
                        Labels.label_name = item;
                        Labels.label_alias = tb_sorts.sort_name;
                        Labels.label_description = "";
                        db.tb_labels.Add(Labels);
                    }
                }                                

                db.SaveChanges();
                return View(db.tb_sorts.ToList());
            }

            return View();
        }

        // GET: Category/Details/5
        public ActionResult Details(long? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            tb_sorts tb_sorts = db.tb_sorts.Find(id);
            if (tb_sorts == null)
            {
                return HttpNotFound();
            }
            return View(tb_sorts);
        }

        // GET: Category/Create
        public ActionResult Create()
        {
            return View();
        }

        // GET: Category/Edit/5
        public ActionResult Edit(long? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            tb_sorts tb_sorts = db.tb_sorts.Find(id);
            if (tb_sorts == null)
            {
                return HttpNotFound();
            }
            return View(tb_sorts);
        }

        // POST: Category/Edit/5
        // 为了防止“过多发布”攻击，请启用要绑定到的特定属性，有关 
        // 详细信息，请参阅 https://go.microsoft.com/fwlink/?LinkId=317598。
        [HttpPost]
        [ValidateAntiForgeryToken]
        [ValidateInput(false)]
        public ActionResult Edit([Bind(Include = "sort_id,sort_name,sort_alias,sort_description,parent_sort_id")] tb_sorts tb_sorts,long? id)
        {
            if (ModelState.IsValid)
            {
                //修改需要对主键赋值，注意：这里需要对所有字段赋值，没有赋值的字段会用NULL更新到数据库
                tb_sorts = db.tb_sorts.SqlQuery("select * from tb_sorts where sort_id = '" + id + "'").FirstOrDefault();
                tb_sorts.sort_id = Convert.ToInt32(id);
                tb_sorts.sort_name = HttpContext.Request.Form["name"];
                tb_sorts.sort_alias = HttpContext.Request.Form["alias"];
                tb_sorts.sort_description = HttpContext.Request.Form["describe"];
                tb_sorts.parent_sort_id = Convert.ToInt32(HttpContext.Request.Form["fid"]);
                
                //将实体附加到对象管理器中
                db.tb_sorts.Attach(tb_sorts);
                //获取到user的状态实体，可以修改其状态
                var setEntry = ((IObjectContextAdapter)db).ObjectContext.ObjectStateManager.GetObjectStateEntry(tb_sorts);
                setEntry.SetModifiedProperty("sort_name");
                setEntry.SetModifiedProperty("sort_alias");
                if (tb_sorts.sort_description != null)
                {
                    setEntry.SetModifiedProperty("sort_description");
                }
                if (tb_sorts.parent_sort_id != 0)
                {
                    setEntry.SetModifiedProperty("parent_sort_id");
                }
                tb_labels[] labels = db.tb_labels.SqlQuery("select * from tb_labels where label_alias = '" + tb_sorts.sort_name + "'").ToArray();
                if(labels != null)
                {
                    string[] str = HttpContext.Request.Form["keywords"].Split(',');                    
                    for (int i = 0; i < labels.Length; i++)
                    {                        
                        labels[i].label_name = str[i];
                        //将实体附加到对象管理器中
                        db.tb_labels.Attach(labels[i]);
                        // 把当前实体的状态改为Modified
                        db.Entry(labels[i]).State = EntityState.Modified;                        
                    }
                }
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(tb_sorts);
        }

        // GET: Category/Delete/5
        public ActionResult Delete(long? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            tb_sorts tb_sorts = db.tb_sorts.Find(id);
            if (tb_sorts == null)
            {
                return HttpNotFound();
            }
            return View(tb_sorts);
        }

        // POST: Category/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(long id)
        {
            tb_sorts tb_sorts = db.tb_sorts.Find(id);
            db.tb_sorts.Remove(tb_sorts);
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
