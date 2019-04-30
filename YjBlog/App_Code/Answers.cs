using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Data;
using System.Data.SqlClient;

/// <summary>
/// Answers 的摘要说明
/// </summary>
public class Answers
{
    DataBase data = new DataBase();
	public Answers()
	{
		//
		// TODO: 在此处添加构造函数逻辑
		//
    }

    #region 回答问题--实体
    //编程语言
    private string lang_name;

    public string Lang_name
    {
        get { return lang_name; }
        set { lang_name = value; }
    }
    //问题类型
    private string prob_type;

    public string Prob_type
    {
        get { return prob_type; }
        set { prob_type = value; }
    }
    //回答问题的标题
    private string answer_title;

    public string Answer_title
    {
        get { return answer_title; }
        set { answer_title = value; }
    }
    #endregion

    #region 回答列表数据
    public DataTable GetProbList(Answers answer) {
        SqlParameter[] parms = {
            data.MakeInParam("@lang_name", SqlDbType.VarChar, 20, answer.Lang_name),
            data.MakeInParam("@prob_type", SqlDbType.NVarChar, 20, answer.Prob_type)
        };
        return data.RunProcReturn("PR_GetAnswer", parms, "td_Answers").Tables[0];
    }
    
    #endregion

    #region 具体内容数据
    public DataTable GetContent(Answers answer) {
        SqlParameter[] parms ={
            data.MakeInParam("@lang_name", SqlDbType.VarChar, 20, answer.Lang_name),
            data.MakeInParam("@prob_type", SqlDbType.NVarChar, 20, answer.Prob_type),
            data.MakeInParam("@answer_title", SqlDbType.NVarChar, 50, answer.Answer_title)
        };
        return data.RunProcReturn("PR_GetContent", parms, "td_Content").Tables[0];
    }
    #endregion

    #region 问题类型
    public DataTable GetProbType(Answers answer)
    {
        SqlParameter[] parms ={
            data.MakeInParam("@lang_name", SqlDbType.VarChar, 20, answer.Lang_name)
        };
        return data.RunProcReturn("select distinct td_Answers.Prob_Type  from td_Answers  where Lang_Name = @lang_name",parms).Tables[0];
    }
    #endregion
}