<%@ WebHandler Language="C#" Class="Handler" %>

using System;
using System.Web;
using System.Data;

public class Handler : IHttpHandler, System.Web.SessionState.IRequiresSessionState
{    
    public void ProcessRequest (HttpContext context) {
        
        string t = HttpContext.Current.Request.Form["t"].ToString();

        switch (t) { 
            case "1":
                GetProbList(context);
                break;
            case "2":
                GetContent(context);
                break;
            case "3":
                GetProbType(context);
                break;            
        }
    }

    //得到问题列表页面
    public void GetProbList(HttpContext context)
    {
        //获取编程语言
        string land_name = HttpContext.Current.Request.Form["lang_name"].ToString();
        //获取问题类型
        string prob_type = HttpContext.Current.Request.Form["prob_type"].ToString();
        //实例回答列表类
        Answers answer = new Answers();
        answer.Lang_name = land_name;
        answer.Prob_type = prob_type;

        DataTable dt = answer.GetProbList(answer);
        string td_Answers = f.ToJson(dt);
        td_Answers = td_Answers.Replace("\"", "\\\"");
        context.Response.Write("{\"status\":\"" + td_Answers + "\"}");
    }
    
    
    //得到具体内容
    public void GetContent(HttpContext context)
    {
        //获取编程语言
        string land_name = HttpContext.Current.Request.Form["lang_name"].ToString();
        //获取问题类型
        string prob_type = HttpContext.Current.Request.Form["prob_type"].ToString();
        //获得问题标题
        string answer_title = HttpContext.Current.Request.Form["answer_title"].ToString();
        //实例回答列表类
        Answers answer = new Answers();
        answer.Lang_name = land_name;
        answer.Prob_type = prob_type;
        answer.Answer_title = answer_title;

        DataTable dt = answer.GetContent(answer);
        string td_Content = f.ToJson(dt);
        td_Content = td_Content.Replace("\"", "\\\"");
        context.Response.Write("{\"status\":\"" + td_Content + "\"}");
    }
    
    //问题类型
    public void GetProbType(HttpContext context) {
        //获取编程语言
        string land_name = HttpContext.Current.Request.Form["lang_name"].ToString();
        //实例回答列表类
        Answers answer = new Answers();
        answer.Lang_name = land_name;

        DataTable dt = answer.GetProbType(answer);
        string json = f.ToJson(dt);
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
