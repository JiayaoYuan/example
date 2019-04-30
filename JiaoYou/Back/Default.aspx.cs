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

public partial class JiaoYou_Back_Default : System.Web.UI.Page
{
    MarriageLogin login = new MarriageLogin();
    protected void Page_Load(object sender, EventArgs e)
    {
        this.BindGridView();
    }
    private void BindGridView()
    {
        GridView1.DataSource = login.SelectUser();
        GridView1.DataKeyNames = new string[] { "id" };
        GridView1.DataBind();
    }
    protected void GridView1_RowDeleting(object sender, GridViewDeleteEventArgs e)
    {
        string id = GridView1.DataKeys[e.RowIndex].Value.ToString();
        login.LoginDelete(id);
        this.BindGridView();
    }
    protected void GridView1_PageIndexChanging(object sender, GridViewPageEventArgs e)
    {
        GridView1.PageIndex = e.NewPageIndex;
        this.BindGridView();
    }
    protected void GridView1_SelectedIndexChanging(object sender, GridViewSelectEventArgs e)
    {
        Session["FriendDetail"] = login.SelectUserName(GridView1.DataKeys[e.NewSelectedIndex].Value.ToString());
        Response.Write("<script>window.open('../Login/FriendDetail.aspx')</script>");
        //ClientScript.RegisterStartupScript(this.GetType(), "1", "<script>window.open('../Login/FriendDetail.aspx?uname=" + Session["FriendDetail"].ToString() + ")</script>");
        Response.Write("<script>history.go(-1)</script>");
    }
    protected void GridView1_RowDataBound(object sender, GridViewRowEventArgs e)
    {
        if (e.Row.RowType == DataControlRowType.DataRow)
        {
            //高亮显示指定行
            e.Row.Attributes.Add("onMouseOver", "Color=this.style.backgroundColor;this.style.backgroundColor='#FFF000'");
            e.Row.Attributes.Add("onMouseOut", "this.style.backgroundColor=Color;");
            //删除指定行数据时，弹出询问对话框
            ((LinkButton)(e.Row.Cells[6].Controls[0])).Attributes.Add("onclick", "return confirm('是否删除当前行数据！')");
        }
    }
}
