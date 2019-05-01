using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Data;
using System.Data.SqlClient;

/// <summary>
/// AnswerType 的摘要说明
/// </summary>
public class AnswerType
{
    DataBase data = new DataBase();
	public AnswerType()
	{
		//
		// TODO: 在此处添加构造函数逻辑
		//
    }

    #region 回答问题类型--实体
    private string lang_name;

    public string Lang_name
    {
        get { return lang_name; }
        set { lang_name = value; }
    }
    private string prob_type;

    public string Prob_type
    {
        get { return prob_type; }
        set { prob_type = value; }
    }
    #endregion

    #region 问题类型
    public DataTable GetProbType(AnswerType answertype)
    {
        SqlParameter[] parms ={
            data.MakeInParam("@lang_name", SqlDbType.VarChar, 20, answertype.Lang_name)
        };
        return data.RunProcReturn("select distinct td_AnswerType.Prob_Type  from td_AnswerType  where Lang_Name = @lang_name", parms).Tables[0];
    }
    #endregion
}