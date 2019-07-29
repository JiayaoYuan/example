using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Blog.Models;
using Blog.App_Start;
using System.Data;
using System.Data.Entity;
using System.Data.SqlClient;
using System.Web.SessionState;

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
        public static long loginid;
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
                    SetAddComment(context);
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
                case "13":
                    HotArticle(context);
                    break;
                case "14":
                    RecommendedArticle(context);
                    break;
                case "15":
                    GetLabel(context);
                    break;
                case "16":
                    LabelYun(context); //标签云
                    break;
                case "17":
                    GetSortToArticleCount(context); //指定类型的文章个数
                    break;
                case "18":
                    ArticleOfSortOrLabel(context);  //指定文章标签和分类
                    break;
                case "19":
                    GetVisitCount(context);         //访问量
                    break;
                case "20":                          //公告 
                    GetNotice(context);
                    break;
                case "21":
                    CommendactionToday(context);    //今日推荐
                    break;
                case "22":                          //分页li个数
                    PagingCount(context);
                    break;
                case "23":                          //指定搜索
                    ScreenSearch(context);
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
                if (loginid >= 1)
                {
                    SqlParameter[] ID = { new SqlParameter("@ID", loginid) };
                    DataTable dt = DB.SqlQueryForDataTatable(db.Database, "select * from tb_users where user_id = @ID", ID);
                    string json = f.DtToJson(dt);
                    json = json.Replace("\"", "\\\"");
                    tb_users new_user = db.tb_users.SqlQuery("select * from tb_users where user_id = " + loginid).FirstOrDefault();
                    new_user.user_login_time = DateTime.Now;
                    new_user.user_ip = GetIPAndMac.ipTrue();
                    db.Entry(new_user).State = EntityState.Modified;
                    db.SaveChanges();
                    context.Response.Write("{\"status\":\"" + json + "\"}");
                }
                else{
                    context.Response.Write("{\"status\":\"-1\"}");
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

        //访问量
        private void GetVisitCount(HttpContext context) {
            int visitCount = 0;
            tb_articles[] articles = db.tb_articles.SqlQuery("select * from tb_articles").ToArray();
            foreach (tb_articles item in articles) {
                visitCount += Convert.ToInt32(item.article_views);
            }
            context.Response.Write(f.StToJSON(Convert.ToString(visitCount)));
        }

        //指定栏目文章个数
        private void Getsortcount(HttpContext context)
        {
            tb_sorts sort = db.tb_sorts.SqlQuery("select * from tb_sorts where sort_id = '"+ context.Request.Form["ID"] + "'").FirstOrDefault();
            Count = db.tb_labels.SqlQuery("select * from tb_labels where label_alias = '" + sort.sort_name + "'").Count();
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
            string json = f.OjToJson(sorts);
            context.Response.Write(json);
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
            string json = DB.TableDoubleSingleMark(dt);                      
            context.Response.Write("{\"status\":\"" + json + "\"}");
        }

        //热门文章
        private void HotArticle(HttpContext context)
        {
            SqlParameter[] Index = {};
            DataTable dt = DB.SqlQueryForDataTatable(db.Database, "select top 5 * from tb_articles order by article_views desc", Index); //获得点击数最多前5的文章
            string json = DB.TableDoubleSingleMark(dt);
            context.Response.Write("{\"status\":\"" + json + "\"}");
        }

        //相关推荐
        private void RecommendedArticle(HttpContext context)
        {
            ArticleCount(context, 5);  
        }

        //相关文章标签
        private void GetLabel(HttpContext context)
        {
            tb_set_article_label[] set_label = db.tb_set_article_label.SqlQuery("select * from tb_set_article_label where article_id = " + context.Request.Form["label"]).ToArray();
            tb_labels label = new tb_labels();            
            string[] json = new string[set_label.Length];
            for (int i = 0; i < set_label.Length; i++)
            {
                label = db.tb_labels.Find(Convert.ToInt32(set_label[i].label_id));                
                json[i] = f.OjToJson(label);
                json[i] = json[i].Replace("\"", "\\\"");
                json[i] = json[i].Replace(Convert.ToString('"'), "\\'");
                json[i] = "\\\"json" + (i + 1) + "\\\":\\\"" + json[i] + "\\\"";
            }
            string labelStr = "{" + (string.Join(",", json)) + "}";
            //context.Response.Write("{\"sort_name\":\"" + json1 + "\",\"label_name\":\"" + json2 + "\"}");
            context.Response.Write("{\"label\":\"" + labelStr + "\"}");
        }        

        //添加评论
        private void SetAddComment(HttpContext context) {
            if (context.Request.Form["comment_content"] != "") {
                if (loginis)
                {
                    DB.AddComment(context.Request.Form["user_id"], context.Request.Form["article_id"], context.Request.Form["comment_content"]);
                }
                else {
                    DB.AddComment(null , context.Request.Form["article_id"], context.Request.Form["comment_content"]);
                }                
            }

            if (context.Request.Form["action"] == "NewAdd" && context.Request.Form["comment_content"] != "")
            {
                SqlParameter[] Index = { };
                DataTable dt;
                dt = DB.SqlQueryForDataTatable(db.Database, "select top 1 * from tb_comments a join tb_users b on a.user_id = b.user_id where a.article_id = " + context.Request.Form["article_id"] + "order by a.comment_id desc", Index);
                string json = DB.TableDoubleSingleMark(dt);
                context.Response.Write("{\"status\":\"" + json + "\"}");
            }
            else if (context.Request.Form["action"] == "")
            {
                SqlParameter[] Index = { };
                DataTable dt;
                if (loginis) {
                    dt = DB.SqlQueryForDataTatable(db.Database, "select * from tb_comments a join tb_users b on a.user_id = b.user_id where a.article_id = " + context.Request.Form["article_id"] + "order by a.comment_id desc", Index);
                    string json = DB.TableDoubleSingleMark(dt);
                    context.Response.Write("{\"status\":\"" + json + "\"}");
                }
                else
                {
                    dt = DB.SqlQueryForDataTatable(db.Database, "select * from tb_comments where tb_comments.article_id = " + context.Request.Form["article_id"], Index);
                    string json = DB.TableDoubleSingleMark(dt);
                    string ranstr = "abcdefghijklmnopqrstuvwxyz123456789";
                    json = json.Replace("\\n", "");                    
                    json = json.Replace("root:[{", "root:[{\\\"user_rights\\\":\\\"游客\\\",\\\"user_name\\\":\\\""+ new GetIPAndMac().getRandomStr(ranstr, 9) + "\\\",");
                    context.Response.Write("{\"status\":\"" + json + "\"}");
                }                
            }
            else
            {
                context.Response.Write("{\"status\":\"-1\"}");
            }            
        }

        //标签云和友情链接
        private void LabelYun(HttpContext context) {
            tb_labels[] labels = db.tb_labels.SqlQuery("select * from tb_labels").ToArray();  //全部标签
            tb_friend_links[] friend_links = db.tb_friend_links.SqlQuery("select * from tb_friend_links").ToArray();    //全部友情链接
            int[] set_Article_Labels_Count = new int[labels.Length];
            for (int i = 0; i < labels.Length; i++) {
                tb_set_article_label[] label_count  = db.tb_set_article_label.SqlQuery("select * from tb_set_article_label where label_id = "+labels[i].label_id).ToArray();
                set_Article_Labels_Count[i] = label_count.Length;
            }
            string json1 = DB.ObjectArrayToJson(labels, set_Article_Labels_Count);
            string json2 = f.OjToJson(friend_links);
            json2 = json2.Replace("\"", "\\\"");
            if (json1 == "") {
                json1 = "-1";                
            }
            if (json2 == "")
            {
                json2 = "-1";
            }
            context.Response.Write("{\"status1\":\"" + json1 + "\",\"status2\":\""+ json2 +"\"}");
        }

        //指定类型的文章个数
        private void GetSortToArticleCount(HttpContext context) {
            tb_sorts sort = db.tb_sorts.SqlQuery("select * from tb_sorts where sort_name = '" + context.Request.Form["sort_name"] + "'").FirstOrDefault();
            tb_set_article_sort[] sort_articles = db.tb_set_article_sort.SqlQuery("select * from tb_set_article_sort where sort_id =" + sort.sort_id).ToArray();
            context.Response.Write(f.StToJSON(Convert.ToString(sort_articles.Length)));
        }

        //指定文章标签和分类
        private void ArticleOfSortOrLabel(HttpContext context) {
            string sortname = "", labelname = "";
            tb_set_article_sort article_sort = db.tb_set_article_sort.SqlQuery("select * from tb_set_article_sort where article_id = " + id).FirstOrDefault();
            tb_set_article_label[] article_label = db.tb_set_article_label.SqlQuery("select * from tb_set_article_label where article_id = " + id).ToArray();
            if (article_sort != null && article_label.Length > 0) {
                tb_sorts sort = db.tb_sorts.Find(article_sort.sort_id);
                sortname = sort.sort_name;
                for (int i = 0; i < article_label.Length; i++)
                {
                    tb_labels label = db.tb_labels.Find(article_label[i].label_id);
                    labelname += label.label_name;
                    if (i != article_label.Length - 1)
                    {
                        labelname += ",";
                    }
                }
            }            
            context.Response.Write("{\"sortname\":\"" + sortname + "\",\"labelname\":\"" + labelname + "\"}");
        }

        //公告
        private void GetNotice(HttpContext context) {
            tb_announcement[] announces = db.tb_announcement.SqlQuery("select * from tb_announcement").ToArray();
            if (announces.Length != 0)
            {
                string json = DB.ObjectArrayToJson(announces);
                context.Response.Write("{\"status\":\""+ json +"\"}");
            }
            else
            {
                context.Response.Write("{\"status\":\"-1\"}");
            }
        }

        //今日推荐
        private void CommendactionToday(HttpContext context) {
            ArticleCount(context, 1);            
        }

        //分页li个数
        private void PagingCount(HttpContext context) {
            string pageclass = context.Request.Form["PageClass"];
            switch (pageclass) {
                case "Comments":
                    tb_comments[] comments = db.tb_comments.SqlQuery("select * from tb_comments").ToArray();
                    context.Response.Write("{\"status\":\"" + comments.Length + "\"}");
                    break;
                case "Articles":
                    tb_articles[] articles = db.tb_articles.SqlQuery("select * from tb_articles").ToArray();
                    context.Response.Write("{\"status\":\"" + articles.Length + "\"}");
                    break;
                case "Notices":
                    tb_announcement[] notices = db.tb_announcement.SqlQuery("select * from tb_announcement").ToArray();
                    context.Response.Write("{\"status\":\"" + notices.Length + "\"}");
                    break;
                case "Flinks":
                    tb_friend_links[] flinks = db.tb_friend_links.SqlQuery("select * from tb_friend_links").ToArray();
                    context.Response.Write("{\"status\":\"" + flinks.Length + "\"}");
                    break;
                default:
                    context.Response.Write("{\"status\":\"-1\"}");
                    break;
            }
        }

        //指定搜索
        private void ScreenSearch(HttpContext context) {
            string sn = HttpUtility.UrlDecode(context.Request.Form["SearchName"]).ToString();
            SqlParameter[] Index = {};            
            DataTable dt = DB.SqlQueryForDataTatable(db.Database, "select * from tb_articles where article_title like '%"+ sn +"%'", Index); //获得点击数最多前5的文章
            string json = DB.TableDoubleSingleMark(dt);
            if (json != "{root:[]}")
            {
                context.Response.Write("{\"status\":\"" + json + "\"}");
            }
            else
            {
                context.Response.Write("{\"status\":\"-1\"}");
            }
        }
        private void ArticleCount(HttpContext context, int Count) {
            SqlParameter[] Index = { };
            DataTable dt = DB.SqlQueryForDataTatable(db.Database, "select top "+ Count +" * from tb_articles order by NEWID()", Index); //获得点击数最多前5的文章
            string json = DB.TableDoubleSingleMark(dt);
            if (json != "{root:[]}")
            {
                context.Response.Write("{\"status\":\"" + json + "\"}");
            }
            else
            {
                context.Response.Write("{\"status\":\"-1\"}");
            }
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
        private static db_BlogEntities db = new db_BlogEntities();
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

        //双引号变成单引号
        public static string TableDoubleSingleMark(DataTable dt) {
            for (int i = 0; i < dt.Rows.Count; i++)
            {
                //string str;
                //str = dt.Rows[i][3].ToString();
                dt.Rows[i][3] = dt.Rows[i][3].ToString().Replace("\"", "'");
                dt.Rows[i][3] = dt.Rows[i][3].ToString().Replace("\r\n", "\\r\\n");
                //str = dt.Rows[i][3].ToString();
            }
            string json = f.DtToJson(dt);            
            json = json.Replace("\"", "\\\"");
            return json;
        }

        //添加评论
        public static void AddComment(string user_id, string article_id, string comment_content) {
            tb_comments comments = new tb_comments();
            if (user_id != null) {
                comments.user_id = Convert.ToInt32(user_id);
            }
            else
            {
                comments.user_id = null;
            }
            comments.article_id = Convert.ToInt32(article_id);
            comments.comment_content = comment_content;
            comments.comment_date = DateTime.Now;
            comments.comment_like_count = 0;
            db.tb_comments.Add(comments);
            db.SaveChanges();
        }

        //对象数组转换成json格式
        public static string ObjectArrayToJson(Object[] obj) {
            string[] json = new string[obj.Length];
            for (int i = 0; i < obj.Length; i++)
            {
                json[i] = f.OjToJson(obj[i]);
                json[i] = json[i].Replace("\"", "\\\"");
                json[i] = json[i].Replace(Convert.ToString('"'), "\\'");
                json[i] = "\\\"json" + (i + 1) + "\\\":\\\"" + json[i] + "\\\"";
            }
            string Str = "{" + (string.Join(",", json)) + "}";
            //context.Response.Write("{\"sort_name\":\"" + json1 + "\",\"label_name\":\"" + json2 + "\"}");
            return Str;
        }

        //两个对象数组转换成json格式 --> 后面是文章个数
        public static string ObjectArrayToJson(Object[] obj, int[] Count)
        {
            string[] json = new string[obj.Length];
            for (int i = 0; i < obj.Length; i++)
            {
                json[i] = f.OjToJson(obj[i]);
                json[i] = json[i].Replace("\"", "\\\"");
                json[i] = json[i].Replace(Convert.ToString('"'), "\\'");
                json[i] = json[i].Replace("}", ",\\\\'articleCount\\\\':\\\\'"+ Count[i]+ "\\\\'}");
                json[i] = "\\\"json" + (i + 1) + "\\\":\\\"" + json[i] + "\\\"";
            }
            string Str = "{" + (string.Join(",", json)) + "}";
            //context.Response.Write("{\"sort_name\":\"" + json1 + "\",\"label_name\":\"" + json2 + "\"}");
            return Str;
        }
    }
}