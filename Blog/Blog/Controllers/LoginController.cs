using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Data;
using System.Data.Entity;
using System.Net;
using Blog.Models;
using Blog.tools;

namespace Blog.Controllers
{
    public class LoginController : Controller
    {
        private db_BlogEntities db = new db_BlogEntities();

        // GET: Login
        public ActionResult Index()
        {
            Handler.loginis = false;               //用户登录重新证明
            tb_users tb_Users = new tb_users();
            tb_Users.user_name = "请输入用户名";
            tb_Users.user_password = "请输入密码";
            return View(tb_Users);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Index([Bind(Include = "user_id,user_ip, user_name, user_password, user_email, user_profile_photo, user_level, user_rights, user_registration_time, user_birthday, user_age, user_telephone_number, user_nickname")] tb_users tb_Users)
        {
            if (ModelState.IsValid)
            {
                tb_Users.user_name = HttpContext.Request.Form["username"];
                tb_Users.user_password = HttpContext.Request.Form["userpwd"];
                tb_users new_user = db.tb_users.SqlQuery("select * from tb_users where user_name = '" + tb_Users.user_name + "'").FirstOrDefault();
                if(new_user != null)
                {
                    if (tb_Users.user_password == new_user.user_password)
                    {
                        if (new_user.user_rights == "管理员")
                        {
                            Handler.loginis = true;               //证明用户登录过后台
                            Handler.id = new_user.user_id;
                            return RedirectToAction("../Main/Index");
                        }
                        Response.Write("<script>您不是管理员不能登录后台！</script>");
                    }
                    tb_Users.user_password = "输入密码错误";
                }
                else
                {
                    tb_Users.user_name = "没有这个用户";
                    tb_Users.user_password = "";
                }
            }            
            return View(tb_Users);
        }
    }
}