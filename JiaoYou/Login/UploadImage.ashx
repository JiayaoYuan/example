<%@ WebHandler Language="C#" Class="UploadImage" %>

using System;
using System.Web;

public class UploadImage : IHttpHandler, System.Web.SessionState.IRequiresSessionState
{

    public void ProcessRequest(HttpContext context)
    {
        try
        {
            //设置接收类型
            context.Response.ContentType = "text/plain";
            //获取前台返回的文件
            HttpPostedFile file = context.Request.Files[0];
            //如果前台返回文件
            if (file != null)
            {
                //图片保存的文件夹路径
                string path = context.Server.MapPath("~/uploadimages/");
                //每天上传的图片一个文件夹
                string folder = DateTime.Now.ToString("yyyyMMdd");
                //如果文件夹不存在，则创建
                if (!System.IO.Directory.Exists(path + folder))
                {
                    //创建文件和文件夹
                    System.IO.Directory.CreateDirectory(path + folder);
                }
                //上传图片的扩展名
                string type = file.FileName.Substring(file.FileName.LastIndexOf('.'));
                //保存图片的文件名
                string saveName = Guid.NewGuid().ToString() + type;
                //保存图片
                file.SaveAs(path + folder + "/" + saveName);
                //返回名称和路径
                context.Response.Write(folder + "/" + saveName);
                //设置文件名和路径
                string filenames = "uploadimages/" + folder + "/" + saveName;
                //获取用户名
                string userName = HttpContext.Current.Session["UserName"].ToString();
                //修改头像
                string sql = "update tb_User set PhotoPath='" + filenames + "' where UserName='" + userName + "'";
                //插入到数据库中
                DataBase data = new DataBase();
                data.RunProc(sql);
            }
        }
        catch
        { }
    }
    public bool IsReusable
    {
        get
        {
            return false;
        }
    }

}