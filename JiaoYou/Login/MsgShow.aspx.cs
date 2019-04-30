using System;
using System.Data;
using System.Configuration;
using System.Collections;
using System.Web;
using System.Web.Security;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Web.UI.WebControls.WebParts;
using System.Web.UI.HtmlControls;

public partial class JiaoYou_Login_MsgShow : System.Web.UI.Page
{
    MarriageMsg msg = new MarriageMsg();
    MarriageLogin login = new MarriageLogin();
    static string FriendName = "";
    protected void Page_Load(object sender, EventArgs e)
    {
        DataSet dsMsg = msg.MsgSelectID(Session["MsgID"].ToString());//根据消息ID查询数据
        txtMsg.Text = dsMsg.Tables[0].Rows[0]["msg"].ToString();//显示消息内容
        string UserName = dsMsg.Tables[0].Rows[0][2].ToString();//显示用户名
        FriendName = dsMsg.Tables[0].Rows[0][1].ToString();//显示好友姓名

        DataSet dsUser = login.SelectUser(UserName);
        imgUser.ImageUrl = "../" + "../" + dsUser.Tables[0].Rows[0]["PhotoPath"].ToString();
        lblUserNick.Text = dsUser.Tables[0].Rows[0]["NickName"].ToString();

        DataSet dsFriend = login.SelectUser(FriendName);
        imgFriend.ImageUrl = "../"+ "../" + dsFriend.Tables[0].Rows[0]["PhotoPath"].ToString();
        lblFriendNick.Text = dsFriend.Tables[0].Rows[0]["NickName"].ToString();
    }
    protected void imgBtnSend_Click(object sender, ImageClickEventArgs e)
    {
        Session["FriendName"] = FriendName;
        Response.Redirect("../SendMsg.aspx");
    }
}
