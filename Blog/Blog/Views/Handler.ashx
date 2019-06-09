<%@ WebHandler Language="C#" Class="Handler" %>

using System;
using System.Web;
using System.Data;

public class Handler : IHttpHandler, System.Web.SessionState.IRequiresSessionState
{

    public void ProcessRequest(HttpContext context)
    {
        string t = HttpContext.Current.Request.Form["t"];//干什么用的

        switch (t)
        {
            case "1":
                //登录
                Login(context);
                break;
            case "2":
                //查询自己的密码
                GetPwd(context);
                break;
            case "3":
                //查询基本信息
                GetDesc(context);
                break;
            case "4":
                //获取账号
                GetUserName(context);
                break;
            case "5":
                //获取所有好友
                GetFriend(context);
                break;
            case "6":
                //查询人员
                GetSerach(context);
                break;
            case "7":
                //发送信息
                SendMsg(context);
                break;
            case "8":
                //退出
                Out(context);
                break;
            case "9":
                //是否登录
                CheckLogIn(context);
                break;
            case "10":
                //添加好友
                AddFrend(context);
                break;
        }
    }
    /// <summary>
    /// 添加朋友
    /// </summary>
    /// <param name="context"></param>
    public void AddFrend(HttpContext context)
    {
        //是否登录
        if (HttpContext.Current.Session["UserName"] == null)
        {
            //如果没有登录，返回-1
            context.Response.Write("{\"status\":\"-1\"}");
            //阻止方法向下进行
            return;
        }
        MarriageFriend friend = new MarriageFriend();
        //获取前台传递过来的朋友的用户名
        string txtUserName = HttpContext.Current.Request.Form["txtUserName"];
        //获取当前所登录的用户
        friend.UserName = HttpContext.Current.Session["UserName"].ToString();
        //设置朋友用户名
        friend.FriendName = txtUserName;
        //查询是否已经添加好友了
        if (friend.SelectFriend(friend).Tables[0].Rows.Count > 0)
        {
            //如果已经添加了，返回-2
            context.Response.Write("{\"status\":\"-2\"}");
            return;
        }
        else
        {
            //添加好友
            int i = friend.FriendAdd(friend);
            //返回0
            context.Response.Write("{\"status\":\"0\"}");
        }
    }
    /// <summary>
    /// 是否登录
    /// </summary>
    /// <param name="context"></param>
    public void CheckLogIn(HttpContext context)
    {
        //是否已经登录了
        if (HttpContext.Current.Session["UserName"] == null)
        {
            //如果没有登录
            context.Response.Write("{\"status\":\"-1\"}");
            //则停止向下运行
            return;
        }
        //如果已经登录了，返回0
        context.Response.Write("{\"status\":\"0\"}");
    }
    /// <summary>
    /// 退出
    /// </summary>
    /// <param name="context"></param>
    public void Out(HttpContext context)
    {
        HttpContext.Current.Session["UserName"] = null;
        context.Response.Write("{\"status\":\"0\"}");
    }
    /// <summary>
    /// 登录
    /// </summary>
    /// <param name="context"></param>
    public void Login(HttpContext context)
    {
        //获取用户名
        string txtUserName = HttpContext.Current.Request.Form["txtUserName"];
        //获取密码
        string txtPwd = HttpContext.Current.Request.Form["txtPwd"];
        MarriageLogin login = new MarriageLogin();
        //设置用户名
        login.UserName = txtUserName;
        //设置密码
        login.PassWord = txtPwd;
        //根据用户名密码查询是否存在在数据库中
        DataSet ds = login.Logon(login);
        //如果存在
        if (ds.Tables[0].Rows.Count > 0)
        {
            //设置服务器端的用户标识
            HttpContext.Current.Session["UserName"] = txtUserName;
            //返回0
            context.Response.Write("{\"status\":\"0\"}");
        }
        else
        {
            //返回-1
            context.Response.Write("{\"status\":\"-1\"}");
        }
    }
    //查询自己的密码
    public void GetPwd(HttpContext context)
    {
        //获取前台输入的用户名
        string txtUserName = HttpContext.Current.Request.Form["txtUserName"];
        MarriageLogin login = new MarriageLogin();
        //获取密码
        string pwd = login.GetPwd(txtUserName);
        if (pwd != "")
        {
            //返回密码
            context.Response.Write("{\"status\":\"" + pwd + "\"}");
        }
        else
        {
            //返回-1
            context.Response.Write("{\"status\":\"-1\"}");
        }
    }
    /// <summary>
    /// 查询基础信息
    /// </summary>
    /// <param name="context"></param>
    public void GetDesc(HttpContext context)
    {
        //验证是否登录
        if (HttpContext.Current.Session["UserName"] == null)
        {
            //如果没有登录返回-1
            context.Response.Write("{\"status\":\"-1\"}");
            return;
        }
        MarriageLogin login = new MarriageLogin();
        //获取当前登录的用户名
        string txtUserName = HttpContext.Current.Session["UserName"].ToString();
        //获取用户的所有信息
        DataTable dt = login.GetTableUserDes(txtUserName);
        //格式化当前的信息
        string json = f.ToJson(dt);
        json = json.Replace("\"", "\\\"");
        context.Response.Write("{\"status\":\"" + json + "\"}");
    }
    /// <summary>
    /// 获取账号
    /// </summary>
    /// <param name="context"></param>
    public void GetUserName(HttpContext context)
    {
        //验证是否登录
        if (HttpContext.Current.Session["UserName"] == null)
        {
            //如果没有登录返回-1
            context.Response.Write("{\"status\":\"-1\"}");
            return;
        }
        //返回当前登录的用户名
        context.Response.Write("{\"status\":\"" + HttpContext.Current.Session["UserName"].ToString() + "\"}");
    }
    /// <summary>
    /// 获取所有好友
    /// </summary>
    /// <param name="context"></param>
    public void GetFriend(HttpContext context)
    {
        //验证是否登录
        if (HttpContext.Current.Session["UserName"] == null)
        {
            //如果没有登录返回-1
            context.Response.Write("{\"status\":\"-1\"}");
            return;
        }
        MarriageFriend login = new MarriageFriend();
        //获取此人的所有好友
        DataTable dt = login.GetTableFriend(HttpContext.Current.Session["UserName"].ToString());
        //格式化获取到的信息
        string json = f.ToJson(dt);
        json = json.Replace("\"", "\\\"");
        context.Response.Write("{\"status\":\"" + json + "\"}");
    }
    /// <summary>
    /// 查询人员
    /// </summary>
    /// <param name="context"></param>
    public void GetSerach(HttpContext context)
    {
        MarriageLogin login = new MarriageLogin();
        //获取前台传递过来的性别
        string sex = HttpContext.Current.Request.Form["sex"];
        //获取最小年龄
        int FAgeStar = Convert.ToInt32(HttpContext.Current.Request.Form["FAgeStar"]);
        //获取最大年龄
        int FAgeEnd = Convert.ToInt32(HttpContext.Current.Request.Form["FAgeEnd"]);
        //获取地址
        string Address = HttpContext.Current.Request.Form["Address"] == "0" ? "" : HttpContext.Current.Request.Form["Address"];
        //设置性别
        login.Sex = sex;
        //设置最小年龄
        login.FriendAgeStar = FAgeStar;
        //设置最大年龄
        login.FriendAgeEnd = FAgeEnd;
        //设置地址
        login.Address = Address;
        //根据这些查询人员
        DataTable dt = login.SelcetUserResult(login, false).Tables[0];
        //格式化当前信息
        string json = f.ToJson(dt);
        json = json.Replace("\"", "\\\"");
        context.Response.Write("{\"status\":\"" + json + "\"}");
    }
    /// <summary>
    /// 发送信息
    /// </summary>
    /// <param name="context"></param>
    public void SendMsg(HttpContext context)
    {
        //用户是否登录
        if (HttpContext.Current.Session["UserName"] == null)
        {
            //如果没有登录返回-1
            context.Response.Write("{\"status\":\"-1\"}");
            return;
        }
        //获取前台传递过来的朋友的用户名
        string FriendName = HttpContext.Current.Request.Form["FriendName"];
        //获取前台的信息
        string msgStr = HttpContext.Current.Request.Form["msgStr"];
        MarriageMsg msg = new MarriageMsg();
        //设置发送人
        msg.Sender = HttpContext.Current.Session["UserName"].ToString();
        //设置目标人
        msg.Accepter = FriendName;
        //设置发送信息
        msg.Msg = msgStr;
        //发送信息
        msg.MsgAdd(msg);
        context.Response.Write("{\"status\":\"0\"}");
    }



    public bool IsReusable
    {
        get
        {
            return false;
        }
    }

}