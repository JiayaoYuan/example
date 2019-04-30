using System;
using System.Data;
using System.Configuration;
using System.Web;
using System.Web.Security;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Web.UI.WebControls.WebParts;
using System.Web.UI.HtmlControls;

//引入
using System.Data.SqlClient;

/// <summary>
/// MarriageMsg 的摘要说明
/// </summary>
public class MarriageMsg
{
    DataBase data = new DataBase();
    public MarriageMsg()
    {
        //
        // TODO: 在此处添加构造函数逻辑
        //
    }
    #region 发送信息实体类
    private string sender = "";
    private string accepter = "";
    private string msg = "";
    private bool checkRead = false;

    /// <summary>
    /// 发送方
    /// </summary>
    public string Sender
    {
        get { return sender; }
        set { sender = value; }
    }
    /// <summary>
    /// 接收方
    /// </summary>
    public string Accepter
    {
        get { return accepter; }
        set { accepter = value; }
    }
    /// <summary>
    /// 发送信息
    /// </summary>
    public string Msg
    {
        get { return msg; }
        set { msg = value; }
    }
    /// <summary>
    /// 阅读状态
    /// </summary>
    public bool CheckRead
    {
        get { return checkRead; }
        set { checkRead = value; }
    }
    #endregion

    /// <summary>
    /// 添加短消息到数据库
    /// </summary>
    /// <param name="msg"></param>
    /// <returns></returns>
    public int MsgAdd(MarriageMsg msg)
    {
        SqlParameter[] parms ={ 
            data.MakeInParam("@Sender",SqlDbType.VarChar,100,msg.Sender),
            data.MakeInParam("@Accepter",SqlDbType.VarChar,100,msg.Accepter),
            data.MakeInParam("@Msg",SqlDbType.Text,0,msg.Msg),
        };
        return data.RunProc("Insert into tb_Msg(Sender, Accepter,Msg) values (@Sender, @Accepter,@Msg)", parms);
    }

    /// <summary>
    /// 获取接收者短信息
    /// </summary>
    /// <param name="msg"></param>
    /// <returns></returns>
    public DataSet MsgSelectAccepterMsg(MarriageMsg msg)
    {
        SqlParameter[] parms ={ 
            data.MakeInParam("@Accepter",SqlDbType.VarChar,100,msg.Accepter),
        };
        return data.RunProcReturn("SELECT * FROM v_msg where Accepter=@Accepter order by id desc", parms, "v_msg");
    }
    /// <summary>
    /// 获取接收者短信息
    /// </summary>
    /// <param name="msg">信息实例对象</param>
    /// <param name="CheckRead">True已阅读  False未阅读</param>
    /// <returns></returns>
    public DataSet MsgSelectAccepterMsg(MarriageMsg msg, bool CheckRead)
    {
        SqlParameter[] parms ={ 
            data.MakeInParam("@Accepter",SqlDbType.VarChar,100,msg.Accepter),
            data.MakeInParam("@CheckRead",SqlDbType.Bit,1,CheckRead),
        };
        return data.RunProcReturn("SELECT * FROM v_msg where Accepter=@Accepter and CheckRead=@CheckRead  order by id desc", parms, "v_msg");
    }
    /// <summary>
    /// 根据ID查询短信息
    /// </summary>
    /// <param name="ID"></param>
    /// <returns></returns>
    public DataSet MsgSelectID(string ID)
    {
        return data.RunProcReturn("Select * from tb_Msg where id =" + ID, "tb_msgid");
    }
    /// <summary>
    /// 获取删除短信息
    /// </summary>
    /// <param name="id"></param>
    /// <returns></returns>
    public int MsgDelete(string id)
    {
        return data.RunProc("delete from tb_Msg where id=" + id);
    }

    /// <summary>
    /// 修改阅读状态
    /// </summary>
    /// <param name="id"></param>
    /// <returns></returns>
    public int MsgUpdate(string id)
    {
        return data.RunProc("Update tb_Msg set CheckRead=1 where id=" + id);
    }

}
