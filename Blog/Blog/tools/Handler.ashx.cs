using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Blog.Models;
using Blog.App_Start;
using System.Data;
using System.Data.Entity;
using System.Data.SqlClient;

namespace Blog.tools
{
    /// <summary>
    /// Handler1 的摘要说明
    /// </summary>
    public class Handler : IHttpHandler, System.Web.SessionState.IRequiresSessionState
    {
        //判断是否用户登录后台
        public static Boolean loginis = false;
        public static object json;
        public static long id;
        private int Count;
        private db_BlogEntities db = new db_BlogEntities();        
        public void ProcessRequest(HttpContext context)
        {
            switch (context.Request.Form["pd"])
            {
                case "1":
                    GetArticle(context);
                    break;
                case "2":
                    InitLogin(context);
                    break;
                case "3":
                    GetArticleCount(context);
                    break;
                case "4":
                    GetCommentCount(context);
                    break;
                case "5":
                    GetLink(context);
                    break;
                case "6":
                    break;
                case "7":
                    Getsortcount(context);
                    break;
                case "8":
                    Getlabels(context);
                    break;
                case "9":
                    GetSortAndLabel(context);
                    break;
                case "10":
                    Getthislabel(context);
                    break;
                case "11":
                    GetPointSortName(context);
                    break;
                case "12":
                    GetArticleContent(context);
                    break;
            }
        }

        private void GetArticle(HttpContext context) //获得文章对象将转化为json数据类型
        {
            tb_articles articles = db.tb_articles.Find(id);
            context.Response.Write(f.StToJSON(articles));
        }

        //初始化登录信息
        private void InitLogin(HttpContext context)
        {
            if (loginis)
            {

                if (id != 0)
                {
                    SqlParameter[] ID = { new SqlParameter("@ID", id) };
                    DataTable dt = DB.SqlQueryForDataTatable(db.Database, "select * from tb_users where user_id = @ID", ID);
                    string json = f.DtToJson(dt);
                    json = json.Replace("\"", "\\\"");
                    context.Response.Write("{\"status\":\"" + json + "\"}");
                }                
            }
            else
            {
                tb_users users = new tb_users();
                users.user_name = "游客";
                users.user_nickname = "游客";
                context.Response.Write(f.StToJSON(users));
            }
        }

        //文章个数
        private void GetArticleCount(HttpContext context)
        {
            Count = db.tb_articles.ToList().Count();
            context.Response.Write(f.FromJSON<object>(Convert.ToString(Count)));
        }

        //评论个数
        private void GetCommentCount(HttpContext context)
        {
            Count = db.tb_comments.ToList().Count();
            context.Response.Write(f.FromJSON<object>(Convert.ToString(Count)));
        }

        //友情链接个数
        private void GetLink(HttpContext context)
        {
            Count = db.tb_friend_links.ToList().Count();
            context.Response.Write(f.FromJSON<object>(Convert.ToString(Count)));
        }

        //指定栏目文章个数
        private void Getsortcount(HttpContext context)
        {
            Count = db.tb_set_article_sort.SqlQuery("select * from tb_set_artitle_sort where sort_id = '" + context.Request.Form["ID"]+"'").Count();
            context.Response.Write(f.FromJSON<object>(Convert.ToString(Count)));
        }

        //关键字的内容
        private void Getlabels(HttpContext context)
        {
            string name = context.Request.Form["Name"];
            tb_labels[] Labels = db.tb_labels.SqlQuery("select * from tb_labels where label_alias = '" + name + "'").ToArray();
            string json = "";
            foreach (var item in Labels)
            {
                json += item.label_name;
                json += ",";
            }
            context.Response.Write(f.StToJSON(json));
        }

        //栏目，标签值
        private void GetSortAndLabel(HttpContext context)
        {
            tb_set_article_sort set_sort = db.tb_set_article_sort.SqlQuery("select * from tb_set_article_sort where article_id =" + context.Request.Form["sort"]).FirstOrDefault();
            tb_sorts sort = db.tb_sorts.Find(set_sort.sort_id);
            tb_set_article_label[] set_label = db.tb_set_article_label.SqlQuery("select * from tb_set_article_label where article_id = " + context.Request.Form["label"]).ToArray();            
            tb_labels label = new tb_labels();
            string[] json = new string[set_label.Length];
            for (int i = 0; i< set_label.Length; i++)
            {
                label = db.tb_labels.Find(Convert.ToInt32(set_label[i].label_id));
                json[i] = f.OjToJson(label);
                json[i] = json[i].Replace("\"", "\\\"");
                json[i] = json[i].Replace(Convert.ToString('"'), "\\'");
                json[i] = "\\\"json" + (i + 1) + "\\\":\\\"" + json[i] + "\\\"";
            }
            string labelStr = "{" + (string.Join(",", json)) + "}";
            //string json1 = f.StToJSON(sort.sort_name);
            //string json2 = f.StToJSON(label.label_name);
            string sortStr = f.OjToJson(sort);            
            sortStr = sortStr.Replace("\"", "\\\"");
            //context.Response.Write("{\"sort_name\":\"" + json1 + "\",\"label_name\":\"" + json2 + "\"}");
            context.Response.Write("{\"sort\":\"" + sortStr + "\",\"label\":\"" + labelStr + "\"}");
        }

        //这是栏目
        private void Getthislabel(HttpContext context)
        {
            tb_sorts[] sorts = db.tb_sorts.SqlQuery("select * from tb_sorts").ToArray();
            context.Response.Write(f.OjToJson(sorts));
        }

        //点击标题返回关键字
        private void GetPointSortName(HttpContext context)
        {
            tb_labels[] labels = db.tb_labels.SqlQuery("select * from tb_labels where label_alias = '" + context.Request.Form["sort_name"] + "'").ToArray();
            string json = "";
            for (int i = 0; i < labels.Length; i++) {
                json += labels[i].label_name;
                if(i != labels.Length - 1)
                {
                    json += ",";
                }
            }
            context.Response.Write(f.StToJSON(json));
        }

        //返回文章内容
        private void GetArticleContent(HttpContext context)
        {
            /*tb_articles articles = db.tb_articles.Find(id); *///获得内容
            SqlParameter[] ID = { new SqlParameter("@ID", id) };
            DataTable dt = DB.SqlQueryForDataTatable(db.Database, "select * from tb_articles where article_id = @ID", ID);
            for (int i = 0; i < dt.Rows.Count; i++) {
                string str;
                str = dt.Rows[i][3].ToString();
                dt.Rows[i][3] = dt.Rows[i][3].ToString().Replace("\"", "'");
                dt.Rows[i][3] = dt.Rows[i][3].ToString().Replace("\r\n", "\\r\\n");
                str = dt.Rows[i][3].ToString();
            }
            string json = f.DtToJson(dt);
            json = json.Replace("\\\"", "\"");
            json = json.Replace("\"", "\\\"");
            context.Response.Write("{\"status\":\"" + json + "\"}");
        }
        public bool IsReusable
        {
            get
            {
                return false;
            }
        }        
    }

    //自定义sql语句返回datatable表类
    public static class DB
    {
        /// <summary>
        /// EF SQL 语句返回 dataTable
        /// </summary>
        /// <param name="db"></param>
        /// <param name="sql"></param>
        /// <param name="parameters"></param>
        /// <returns></returns>
        public static DataTable SqlQueryForDataTatable(this Database db, string sql, SqlParameter[] parameters)
        {
            SqlConnection conn = new System.Data.SqlClient.SqlConnection();
            conn.ConnectionString = db.Connection.ConnectionString;
            if (conn.State != ConnectionState.Open)
            {
                conn.Open();
            }
            SqlCommand cmd = new SqlCommand();
            cmd.Connection = conn;
            cmd.CommandText = sql;
            if (parameters.Length > 0)
            {
                foreach (var item in parameters)
                {
                    cmd.Parameters.Add(item);
                }
            }
            SqlDataAdapter adapter = new SqlDataAdapter(cmd);
            DataTable table = new DataTable();
            adapter.Fill(table);
            return table;
        }
    }
}