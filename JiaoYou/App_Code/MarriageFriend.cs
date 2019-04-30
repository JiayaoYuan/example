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
/// MarriageFriend 的摘要说明
/// </summary>
public class MarriageFriend
{
    DataBase data = new DataBase();
    public MarriageFriend()
    {
        //
        // TODO: 在此处添加构造函数逻辑
        //
    }
    #region 好友实体类
    private string userName = "";
    private string friendName = "";

    /// <summary>
    /// 会员名称
    /// </summary>
    public string UserName
    {
        get { return userName; }
        set { userName = value; }
    }
    /// <summary>
    /// 会员朋友
    /// </summary>
    public string FriendName
    {
        get { return friendName; }
        set { friendName = value; }
    }
    #endregion

    /// <summary>
    /// 加为好友
    /// </summary>
    /// <param name="friend"></param>
    /// <returns></returns>
    public int FriendAdd(MarriageFriend friend)
    {
        SqlParameter[] parms ={ 
            data.MakeInParam("@UserName",SqlDbType.VarChar,100,friend.UserName),
            data.MakeInParam("@FriendName",SqlDbType.VarChar,100,friend.FriendName),
        };
        return data.RunProc("Insert into tb_Friend(UserName, FriendName) values (@UserName, @FriendName)", parms);
    }
    /// <summary>
    /// 删除好友
    /// </summary>
    /// <param name="id"></param>
    /// <returns></returns>
    public int FriendDelete(string id)
    {
        return data.RunProc("delete from tb_Friend where id=" + id);
    }
    /// <summary>
    /// 查询该用户是否已经将其加为好友
    /// </summary>
    /// <param name="friend"></param>
    /// <returns></returns>
    public DataSet SelectFriend(MarriageFriend friend)
    {
        SqlParameter[] parms ={ 
            data.MakeInParam("@UserName",SqlDbType.VarChar,100,friend.UserName),
            data.MakeInParam("@FriendName",SqlDbType.VarChar,100,friend.FriendName),
        };
        return data.RunProcReturn("SELECT * FROM tb_Friend WHERE (UserName = @UserName ) AND (FriendName =  @FriendName )", parms, "tb_User");
    }
    /// <summary>
    /// 获取所有好友
    /// </summary>
    /// <param name="userName"></param>
    /// <returns></returns>
    public DataTable GetTableFriend(string userName)
    {
        string sql = "select tb_User.* from tb_Friend,tb_User where tb_Friend.FriendName=tb_User.UserName and tb_Friend.UserName='"+ userName + "'";
        return data.RunProcReturn(sql, "table").Tables[0];
    }
    /// <summary>
    /// 获取好友信息
    /// </summary>
    /// <param name="friend"></param>
    /// <returns></returns>
    public DataSet SelectFriendByUserName(MarriageFriend friend)
    {
        SqlParameter[] parms ={ 
            data.MakeInParam("@UserName",SqlDbType.VarChar,100,friend.UserName),
        };
        return data.RunProcReturn("SELECT * FROM v_Friend WHERE UserName = @UserName", parms, "tb_User");
    }
    /// <summary>
    /// 统计好友个数
    /// </summary>
    /// <param name="friend"></param>
    /// <returns></returns>
    public DataSet SelectFriendSum(MarriageFriend friend)
    {
        SqlParameter[] parms ={ 
            data.MakeInParam("@FriendName",SqlDbType.VarChar,100,friend.FriendName),
        };
        return data.RunProcReturn("SELECT * FROM tb_Friend WHERE FriendName = @FriendName", parms, "tb_User");
    }

}
