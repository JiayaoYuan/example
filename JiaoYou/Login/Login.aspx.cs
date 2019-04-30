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

public partial class JiaoYou_Login_Login : System.Web.UI.Page
{
    MarriageLogin login = new MarriageLogin();
    protected void Page_Load(object sender, EventArgs e)
    {

    }
    //注册按钮事件
    protected void btnLogin_Click(object sender, EventArgs e)
    {
        /************注册交友信息************/
        //获取前台页面的txtUserName控件的用户名
        login.UserName = txtUserName.Text;
        //验证前台输入的用户名是否重复
        if (login.SelectUser(login.UserName).Tables[0].Rows.Count > 0)
            //如果重复，弹出提示
            WebMessageBox.Show("该用户ＩＤ存在，请重新输入！");
        //获取txtPwd控件返回的密码
        login.PassWord = txtPwd.Text;
        //获取txtNickName控件返回的昵称
        login.NickName = txtNickName.Text;
        //获取页面提交过来的性别
        login.Sex = Request.Form["sltSex"].ToString();
        //验证页面有没有提交过来性别
        if (login.Sex == "0")
            //如果没有提交过来性别则弹出提示
            WebMessageBox.Show("请选择您的性别！");
        //获取txtBirth控件返回的生日
        login.BirthDay = txtBirth.Text;
        //获取前台提交过来的地址
        login.Address = Request.Form["sltState"].ToString() + Request.Form["sltCity"].ToString();
        //如果没有获取到前台提交的地址
        if (Request.Form["sltState"].ToString() == "0" || Request.Form["sltCity"].ToString() == "0")
            //如果没有填写地址弹出提示
            WebMessageBox.Show("请选择您的所在省和地区！");
        //获取前台提交过来的身高
        login.Stature = Request.Form["sltStature"].ToString();
        //验证有没有传递过来身高
        if (login.Stature == "0")
            //如果没有填写身高弹出提示
            WebMessageBox.Show("请选择您的身高！");
        //获取前台提交过来的体重
        login.Avoirdupois = Request.Form["sltAvoirdupois"].ToString();
        //验证有没有
        if (login.Avoirdupois == "0")
            //如果没有弹出提示
            WebMessageBox.Show("请选择您的体重！");
        //获取前台提交过来的学历
        login.Education = Request.Form["sltEducation"].ToString();
        //验证有没有学历
        if (login.Education == "0")
            //如果没有弹出提示
            WebMessageBox.Show("请选择您的最高学历！");
        //获取前台提交过来的年收入
        login.Earning = Request.Form["sltEarning"].ToString();
        //如果没有年收入
        if (login.Earning == "0")
            //弹出提示
            WebMessageBox.Show("请选择您的年收入！");
        //获取前台传递过来的民族
        login.Nation = Request.Form["sltNation"].ToString();
        //如果没有传递填写
        if (login.Nation == "0")
            //弹出提示
            WebMessageBox.Show("请选择您所属民族！");
        //获取前台传递过来的血型
        login.BloodType = Request.Form["sltBloodType"].ToString();
        //验证有没有填写血型
        if (login.BloodType == "0")
            //如果没有填写血型弹出提示
            WebMessageBox.Show("请选择您的血型！");
        //获取前台传递古来的相貌
        login.Looks = Request.Form["sltLooks"].ToString();
        //如果没有填写相貌
        if (login.Looks == "0")
            //弹出提示
            WebMessageBox.Show("请选择您的相貌！");
        //根据txtMetier获取职业
        login.Metier = txtMetier.Text;
        //获取前台传递过来的住房情况
        login.Housing = Request.Form["sltHousing"].ToString();
        //如果住房情况没有填写
        if (login.Housing == "0")
            //弹出提示
            WebMessageBox.Show("请选择住房情况！");
        //获取前台传递过来的购车情况
        login.BuyCar = Request.Form["sltBuyCar"].ToString();
        //如果没有填写
        if (login.BuyCar == "0")
            //如果没有弹出提示
            WebMessageBox.Show("请选择您的购车情况！");
        //获取前台传递过来的购车情况
        login.Marriage = Request.Form["sltMarriage"].ToString();
        //如果没有填写
        if (login.Marriage == "0")
            //如果没有弹出提示
            WebMessageBox.Show("请选择您的婚姻状况！");
        //获取前台传递过来的小孩
        login.HaveBaby = Request.Form["sltHaveBaby"].ToString();
        //如果没有填写
        if (login.HaveBaby == "0")
            //如果没有弹出提示
            WebMessageBox.Show("请选择您现在时候有小孩！");
        login.HavingBaby = Request.Form["sltHavingBaby"].ToString();
        if (login.HavingBaby == "0")
            //如果没有弹出提示
            WebMessageBox.Show("请选择您将来是否要小孩！");
        //获取前台传递过来的是否抽烟
        login.Smoke = Request.Form["sltSmoke"].ToString();
        //如果没有填写
        if (login.Smoke == "0")
            //如果没有弹出提示
            WebMessageBox.Show("请选择您是否吸烟！");
        //获取前台传递过来是否喝酒
        login.Drink = Request.Form["sltDrink"].ToString();
        //如果没有填写
        if (login.Drink == "0")
            //如果没有弹出提示
            WebMessageBox.Show("请选择您是否喝酒！");
        //设置交流语言
        login.UseLanguage = txtLanage.Text;
        //设置性格与爱好
        login.SexLike = txtSexLike.Text;
        /***设置速配条件***/
        //获取对方的地址
        login.FriendCome = Request.Form["sltFState"].ToString() + Request.Form["sltFCity"].ToString();
        //获取对方的最小年龄
        login.FriendAgeStar = Convert.ToInt32(Request.Form["sltFAgeStar"]);
        //获取对方的最大年龄
        login.FriendAgeEnd = Convert.ToInt32(Request.Form["sltFAgeEnd"]);
        //获取对方的最小身高
        login.FriendStatureStar = Convert.ToInt32(Request.Form["sltFStatureStar"]);
        //获取对方的最大身高
        login.FriendStatureEnd = Convert.ToInt32(Request.Form["sltFStatureEnd"].ToString());
        //获取对方的婚姻状态
        login.FriendMarriage = Request.Form["sltFMarriage"].ToString();
        //添加注册信息
        int i = login.Login(login);
        //设置服务器端客户端标识
        Session["UserName"] = login.UserName;
        //跳转页面
        Response.Redirect("index.html");
    }
    protected void btnCheck_Click(object sender, EventArgs e)
    {
        login.UserName = txtUserName.Text;
        if (login.SelectUser(login.UserName).Tables[0].Rows.Count > 0)
            WebMessageBox.Show("该用户ＩＤ存在，请重新输入！");
    }
}
