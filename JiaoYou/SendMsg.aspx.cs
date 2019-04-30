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

public partial class JiaoYou_SendMsg : System.Web.UI.Page
{
    MarriageLogin login = new MarriageLogin();
    MarriageMsg msg = new MarriageMsg();
    protected void Page_Load(object sender, EventArgs e)
    {
        //查询用户昵称和照片路径
        DataSet dsUser = login.SelectUser(Session["UserName"].ToString());
        imgUser.ImageUrl = "../" + dsUser.Tables[0].Rows[0]["PhotoPath"].ToString();
        lblUserNick.Text = dsUser.Tables[0].Rows[0]["NickName"].ToString();
        //查询会员昵称和照片路径
        DataSet dsFriend = login.SelectUser(Session["FriendName"].ToString());
        imgFriend.ImageUrl = "../" + dsFriend.Tables[0].Rows[0]["PhotoPath"].ToString();
        lblFriendNick.Text = dsFriend.Tables[0].Rows[0]["NickName"].ToString();
    }
    protected void imgBtnSend_Click(object sender, ImageClickEventArgs e)
    {
        msg.Sender = Session["UserName"].ToString();
        msg.Accepter = Session["FriendName"].ToString();
        msg.Msg = txtMsg.Text;
        int i = msg.MsgAdd(msg);        //将短消息保存到数据库中
        WebMessageBox.Show("信息发送成功！", true);
    }
}
