using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

/// <summary>
/// Content 的摘要说明
/// </summary>
public class Content
{
	public Content()
	{
		//
		// TODO: 在此处添加构造函数逻辑
		//
    }

    #region 具体内容--实体

    private string title;

    public string Title
    {
        get { return title; }
        set { title = value; }
    }
    private string article1;

    public string Article1
    {
        get { return article1; }
        set { article1 = value; }
    }
    private string article2;

    public string Article2
    {
        get { return article2; }
        set { article2 = value; }
    }
    private string article3;

    public string Article3
    {
        get { return article3; }
        set { article3 = value; }
    }
    private string photopath1;

    public string Photopath1
    {
        get { return photopath1; }
        set { photopath1 = value; }
    }
    private string photopath2;

    public string Photopath2
    {
        get { return photopath2; }
        set { photopath2 = value; }
    }
    private string photopath3;

    public string Photopath3
    {
        get { return photopath3; }
        set { photopath3 = value; }
    }
    private DateTime addtime;

    public DateTime Addtime
    {
        get { return addtime; }
        set { addtime = value; }
    }

    #endregion
}