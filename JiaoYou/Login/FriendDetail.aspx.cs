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

public partial class JiaoYou_Login_FriendDetail : System.Web.UI.Page
{
    MarriageLogin login = new MarriageLogin();
    protected void Page_Load(object sender, EventArgs e)
    {
        if (Session["FriendDetail"] == null)
        {
            Session["FriendDetail"] = Request["uname"];
        }
        else
        {
            if (Session["FriendDetail"].ToString() != Request["uname"].ToString())
            {
                Session["FriendDetail"] = Request["uname"];
            }
        }
        this.Title = Session["FriendDetail"].ToString() + "的详细信息";
        //基本信息
        DataTable dt = login.SelectUser(Session["FriendDetail"].ToString()).Tables[0];
        lblBase_NickName.Text = dt.Rows[0]["NickName"].ToString();
        lblBase_Sex.Text = dt.Rows[0]["Sex"].ToString();
        lblBase_BirthDay.Text = dt.Rows[0]["BirthDay"].ToString();
        lblBase_Marray.Text = dt.Rows[0]["Marriage"].ToString();
        lblBase_Hight.Text = dt.Rows[0]["Stature"].ToString();
        lblBase_Kg.Text = dt.Rows[0]["Avoirdupois"].ToString();
        lblBase_Mz.Text = dt.Rows[0]["Nation"].ToString();
        lblBase_Study.Text = dt.Rows[0]["Education"].ToString();
        lblBase_Menoy.Text = dt.Rows[0]["Earning"].ToString();
        lblBase_Look.Text = dt.Rows[0]["Looks"].ToString();
        lblBase_Job.Text = dt.Rows[0]["Metier"].ToString();
        lblBase_House.Text = dt.Rows[0]["Housing"].ToString();
        lblBase_Area.Text = dt.Rows[0]["Address"].ToString();
        imgPhoto.ImageUrl = "../../" + dt.Rows[0]["PhotoPath"].ToString();
        //真情告白
        lblSexLike.Text = dt.Rows[0]["SexLike"].ToString();
        //择友要求
        lblFAge.Text = dt.Rows[0]["FriendAgeStar"].ToString() + "　至　" + dt.Rows[0]["FriendAgeEnd"].ToString();
        lblFHight.Text = dt.Rows[0]["FriendStatureStar"].ToString() + "　至　" + dt.Rows[0]["FriendStatureEnd"].ToString();
        lblFArea.Text = dt.Rows[0]["FriendCome"].ToString().Replace("0", "");
        lblFMarray.Text = dt.Rows[0]["FriendMarriage"].ToString();

        //其他信息
        lblHaveChild.Text = dt.Rows[0]["HaveBaby"].ToString();
        lblHavingChild.Text = dt.Rows[0]["HavingBaby"].ToString();
        lblSmoke.Text = dt.Rows[0]["Smoke"].ToString();
        lblDrink.Text = dt.Rows[0]["Drink"].ToString();
        lblBlood.Text = dt.Rows[0]["BloodType"].ToString();
        lblLanguage.Text = dt.Rows[0]["UseLanguage"].ToString();
    }
    protected void btnFind_Click(object sender, EventArgs e)
    {
        Session["sex"] = Request.Form["sltSex"].ToString();
        Session["FAgeStar"] = Request.Form["sltFAgeStar"].ToString();
        Session["FAgeEnd"] = Request.Form["sltFAgeEnd"].ToString();
        Session["Address"] = (Request.Form["sltState"].ToString() == "0") ? "" : Request.Form["sltState"].ToString() + ((Request.Form["sltCity"].ToString() == "0") ? "" : Request.Form["sltCity"].ToString());
        // Session["Photo"] = chkPhoto.Checked;
        Response.Redirect("../Result.aspx");
    }
}
