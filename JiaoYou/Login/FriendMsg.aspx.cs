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


public partial class JiaoYou_Login_FriendMsg : System.Web.UI.Page
{
    MarriageMsg msg = new MarriageMsg();
    //3种类别：全部显示(-1代表全部显示)，显示未阅读信息（0），显示已阅读信息(1)
    static int CheckType = -1;

    protected void Page_Load(object sender, EventArgs e)
    {
        if (Session["UserName"] == null)
            WebMessageBox.Show("由于您长时间对本站没有任何操作，系统自动退出。请您重新登录！", "..//Default.aspx");
        if (!IsPostBack)
            this.BindGridView();
    }
    private void BindGridView()
    {
        msg.Accepter = Session["UserName"].ToString();  //获取用户名
        switch (CheckType)                              //根据类型值来显示不同的短消息
        {
            case -1:
                GridView1.DataSource = msg.MsgSelectAccepterMsg(msg);
                GridView1.DataKeyNames = new string[] { "id" };
                GridView1.DataBind();
                break;
            case 0:
                GridView1.DataSource = msg.MsgSelectAccepterMsg(msg, false);
                GridView1.DataKeyNames = new string[] { "id" };
                GridView1.DataBind();
                break;
            case 1:
                GridView1.DataSource = msg.MsgSelectAccepterMsg(msg, true);
                GridView1.DataKeyNames = new string[] { "id" };
                GridView1.DataBind();
                break;
        }
    }
    //删除消息
    protected void GridView1_RowDeleting(object sender, GridViewDeleteEventArgs e)
    {
        string id = GridView1.DataKeys[e.RowIndex].Value.ToString();   //获取主贱ID值
        int d = msg.MsgDelete(id);                                     //删除指定的短消息
        this.BindGridView();                                           //调用自定义方法进行数据绑定
    }
    //查看消息
    protected void GridView1_SelectedIndexChanging(object sender, GridViewSelectEventArgs e)
    {
        string id = GridView1.DataKeys[e.NewSelectedIndex].Value.ToString();   //获取主贱ID值
        int u = msg.MsgUpdate(id);  //修改阅读状态
        Session["MsgID"] = id;
        Response.Write("<script>window.open('MsgShow.aspx','','height=600, width=800, top=200, left=200')</script>");
        Response.Write("<script>history.go(-1)</script>");
    }
    //回复消息
    protected void GridView1_RowEditing(object sender, GridViewEditEventArgs e)
    {
        int u = msg.MsgUpdate(GridView1.DataKeys[e.NewEditIndex].Value.ToString());  //修改阅读状态
        Session["FriendName"] = GridView1.Rows[e.NewEditIndex].Cells[2].Text;
        Response.Write("<script>window.open('../SendMsg.aspx','','height=600, width=800, top=200, left=200')</script>");
        Response.Write("<script>history.go(-1)</script>");
    }

    protected void GridView1_RowDataBound(object sender, GridViewRowEventArgs e)
    {
        if (e.Row.RowType == DataControlRowType.DataRow)
        {
            //高亮显示指定行
            e.Row.Attributes.Add("onMouseOver", "Color=this.style.backgroundColor;this.style.backgroundColor='#f2f2f2'");
            e.Row.Attributes.Add("onMouseOut", "this.style.backgroundColor=Color;");
            //多余字　使用...显示
            e.Row.Cells[3].Text = StringFormat.Out(e.Row.Cells[3].Text, 25);
            //删除指定行数据时，弹出询问对话框
            ((LinkButton)(e.Row.Cells[6].Controls[0])).Attributes.Add("onclick", "return confirm('是否删除当前行数据！')");
        }
    }
    protected void GridView1_PageIndexChanging(object sender, GridViewPageEventArgs e)
    {
        GridView1.PageIndex = e.NewPageIndex;   //进行分页
        this.BindGridView();
    }
    protected void rdoBtnAll_CheckedChanged(object sender, EventArgs e)
    {
        CheckType = -1;  //显示所有短消息
        this.BindGridView();
    }
    protected void rdoBtnTrue_CheckedChanged(object sender, EventArgs e)
    {
        CheckType = 1;　　//显示已阅读短消息
        this.BindGridView();
    }
    protected void rdoBtnFalse_CheckedChanged(object sender, EventArgs e)
    {
        CheckType = 0;    //显示未阅读短消息
        this.BindGridView();
    }
}
