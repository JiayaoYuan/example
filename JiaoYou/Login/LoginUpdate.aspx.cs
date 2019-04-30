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

public partial class JiaoYou_Login_LoginUpdate : System.Web.UI.Page
{
    MarriageLogin login = new MarriageLogin();
    protected void Page_Load(object sender, EventArgs e)
    {
        DataTable dt = login.SelectUser(Session["UserName"].ToString()).Tables[0];
        txtUserName.Text = dt.Rows[0]["UserName"].ToString();
        lblPwd.Text = dt.Rows[0]["PassWord"].ToString();
        sltSex.Value = dt.Rows[0]["Sex"].ToString();
        txtNickName.Text = dt.Rows[0]["NickName"].ToString();
        txtBirth.Text = dt.Rows[0]["BirthDay"].ToString();
        string address = dt.Rows[0]["Address"].ToString();
        //将省市分开
        int position = address.LastIndexOf("省");
        if (position == -1)
        {
            position = address.LastIndexOf("区");
        }
        sltState.Value = address.Substring(0, position + 1);
        sltCity.Items.Add(address.Substring(position + 1, address.Length - position - 1));
        sltCity.Value = address.Substring(position + 1, address.Length - position - 1);
        sltStature.Value = dt.Rows[0]["Stature"].ToString();
        sltAvoirdupois.Value = dt.Rows[0]["Avoirdupois"].ToString();
        sltEducation.Value = dt.Rows[0]["Education"].ToString();
        sltEarning.Value = dt.Rows[0]["Earning"].ToString();
        sltNation.Value = dt.Rows[0]["Nation"].ToString();
        sltBloodType.Value = dt.Rows[0]["BloodType"].ToString();
        sltLooks.Value = dt.Rows[0]["Looks"].ToString();
        txtMetier.Text = dt.Rows[0]["Metier"].ToString();
        sltHousing.Value = dt.Rows[0]["Housing"].ToString();
        sltBuyCar.Value = dt.Rows[0]["BuyCar"].ToString();
        sltMarriage.Value = dt.Rows[0]["Marriage"].ToString();
        sltHaveBaby.Value = dt.Rows[0]["HaveBaby"].ToString();
        sltHavingBaby.Value = dt.Rows[0]["HavingBaby"].ToString();
        sltSmoke.Value = dt.Rows[0]["Smoke"].ToString();
        sltDrink.Value = dt.Rows[0]["Drink"].ToString();
        txtLanage.Text = dt.Rows[0]["UseLanguage"].ToString();
        txtSexLike.Text = dt.Rows[0]["SexLike"].ToString();
        //********修改速配条件***********//
        string Faddress = dt.Rows[0]["FriendCome"].ToString();
        if (Faddress != "00") //主要用于设置地区不限的用户。
        {
            //将省市分开
            int Fposition = Faddress.LastIndexOf("省");
            if (Fposition == -1)
            {
                Fposition = Faddress.LastIndexOf("区");
            }
            sltFState.Value = Faddress.Substring(0, Fposition + 1);
            sltFCity.Items.Add(Faddress.Substring(Fposition + 1, Faddress.Length - Fposition - 1));
            sltFCity.Value = Faddress.Substring(Fposition + 1, Faddress.Length - Fposition - 1);
        }
        sltFAgeStar.Value = dt.Rows[0]["FriendAgeStar"].ToString();
        sltFAgeEnd.Value = dt.Rows[0]["FriendAgeEnd"].ToString();
        sltFStatureStar.Value = dt.Rows[0]["FriendStatureStar"].ToString();
        sltFStatureEnd.Value = dt.Rows[0]["FriendStatureEnd"].ToString();
        sltFMarriage.Value = dt.Rows[0]["FriendMarriage"].ToString();

    }
    protected void btnUpdate_Click(object sender, EventArgs e)
    {
        /************修改交友信息************/
        login.UserName = txtUserName.Text;
        login.PassWord = txtPwd.Text;
        login.NickName = txtNickName.Text;
        login.Address = Request.Form["sltState"].ToString() + Request.Form["sltCity"].ToString();
        if (Request.Form["sltState"].ToString() == "0" || Request.Form["sltCity"].ToString() == "0")
            WebMessageBox.Show("请选择您的所在省和地区！");
        login.Stature = Request.Form["sltStature"].ToString();
        if (login.Stature == "0")
            WebMessageBox.Show("请选择您的身高！");
        login.Avoirdupois = Request.Form["sltAvoirdupois"].ToString();
        if (login.Avoirdupois == "0")
            WebMessageBox.Show("请选择您的体重！");
        login.Education = Request.Form["sltEducation"].ToString();
        if (login.Education == "0")
            WebMessageBox.Show("请选择您的最高学历！");
        login.Earning = Request.Form["sltEarning"].ToString();
        if (login.Earning == "0")
            WebMessageBox.Show("请选择您的年收入！");
        login.Nation = Request.Form["sltNation"].ToString();
        if (login.Nation == "0")
            WebMessageBox.Show("请选择您所属民族！");
        login.BloodType = Request.Form["sltBloodType"].ToString();
        if (login.BloodType == "0")
            WebMessageBox.Show("请选择您的血型！");
        login.Looks = Request.Form["sltLooks"].ToString();
        if (login.Looks == "0")
            WebMessageBox.Show("请选择您的相貌！");
        login.Metier = txtMetier.Text;

        login.Housing = Request.Form["sltHousing"].ToString();
        if (login.Housing == "0")
            WebMessageBox.Show("请选择住房情况！");
        login.BuyCar = Request.Form["sltBuyCar"].ToString();
        if (login.BuyCar == "0")
            WebMessageBox.Show("请选择您的购车情况！");
        login.Marriage = Request.Form["sltMarriage"].ToString();
        if (login.Marriage == "0")
            WebMessageBox.Show("请选择您的婚姻状况！");
        login.HaveBaby = Request.Form["sltHaveBaby"].ToString();
        if (login.HaveBaby == "0")
            WebMessageBox.Show("请选择您现在时候有小孩！");
        login.HavingBaby = Request.Form["sltHavingBaby"].ToString();
        if (login.HavingBaby == "0")
            WebMessageBox.Show("请选择您将来是否要小孩！");
        login.Smoke = Request.Form["sltSmoke"].ToString();
        if (login.Smoke == "0")
            WebMessageBox.Show("请选择您是否吸烟！");
        login.Drink = Request.Form["sltDrink"].ToString();
        if (login.Drink == "0")
            WebMessageBox.Show("请选择您是否喝酒！");
        login.UseLanguage = txtLanage.Text;
        login.SexLike = txtSexLike.Text;
        /***设置速配条件***/
        login.FriendCome = Request.Form["sltFState"].ToString() + Request.Form["sltFCity"].ToString();
        login.FriendAgeStar = Convert.ToInt32(Request.Form["sltFAgeStar"]);
        login.FriendAgeEnd = Convert.ToInt32(Request.Form["sltFAgeEnd"]);
        login.FriendStatureStar = Convert.ToInt32(Request.Form["sltFStatureStar"]);
        login.FriendStatureEnd = Convert.ToInt32(Request.Form["sltFStatureEnd"].ToString());
        login.FriendMarriage = Request.Form["sltFMarriage"].ToString();
        int i = login.LoginUpdate(login);
        WebMessageBox.Show("会员档案信息修改成功！");
    }
}
