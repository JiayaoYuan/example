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
using System.Data;
using System.Data.SqlClient;

/// <summary>
/// MarriageLogin 的摘要说明
/// </summary>
public class MarriageLogin
{
    DataBase data = new DataBase();
    public MarriageLogin()
    {
        //
        // TODO: 在此处添加构造函数逻辑
        //
    }
    #region 注册交友基本信息－－－实体
    private string userName = "";
    private string passWord = "";
    private string nickName = "";
    private string sex = "";
    private string birthDay = "";
    private string address = "";
    private string stature = "";
    private string avoirdupois = "";
    private string education = "";
    private string earning = "";
    private string nation = "";
    private string bloodType = "";
    private string looks = "";
    private string metier = "";
    private string housing = "";
    private string buyCar = "";
    private string marriage = "";
    private string haveBaby = "";
    private string havingBaby = "";
    private string smoke = "";
    private string drink = "";
    private string useLanguage = "";
    private string sexLike = "";


    /// <summary>
    /// 用户名称
    /// </summary>
    public string UserName
    {
        get { return userName; }
        set { userName = value; }
    }
    /// <summary>
    /// 用户密码
    /// </summary>
    public string PassWord
    {
        get { return passWord; }
        set { passWord = value; }
    }
    /// <summary>
    /// 昵称
    /// </summary>
    public string NickName
    {
        get { return nickName; }
        set { nickName = value; }
    }
    /// <summary>
    /// 性别
    /// </summary>
    public string Sex
    {
        get { return sex; }
        set { sex = value; }
    }
    /// <summary>
    /// 出生日期
    /// </summary>
    public string BirthDay
    {
        get { return birthDay; }
        set { birthDay = value; }
    }
    /// <summary>
    /// 联系地址
    /// </summary>
    public string Address
    {
        get { return address; }
        set { address = value; }
    }
    /// <summary>
    /// 身高
    /// </summary>
    public string Stature
    {
        get { return stature; }
        set { stature = value; }
    }
    /// <summary>
    /// 体重
    /// </summary>
    public string Avoirdupois
    {
        get { return avoirdupois; }
        set { avoirdupois = value; }
    }
    /// <summary>
    /// 学历
    /// </summary>
    public string Education
    {
        get { return education; }
        set { education = value; }
    }
    /// <summary>
    /// 年收入
    /// </summary>
    public string Earning
    {
        get { return earning; }
        set { earning = value; }
    }
    /// <summary>
    /// 民族
    /// </summary>
    public string Nation
    {
        get { return nation; }
        set { nation = value; }
    }
    /// <summary>
    /// 血型
    /// </summary>
    public string BloodType
    {
        get { return bloodType; }
        set { bloodType = value; }
    }
    /// <summary>
    /// 相貌
    /// </summary>
    public string Looks
    {
        get { return looks; }
        set { looks = value; }
    }
    /// <summary>
    /// 职业
    /// </summary>
    public string Metier
    {
        get { return metier; }
        set { metier = value; }
    }
    /// <summary>
    /// 住房
    /// </summary>
    public string Housing
    {
        get { return housing; }
        set { housing = value; }
    }
    /// <summary>
    /// 购车
    /// </summary>
    public string BuyCar
    {
        get { return buyCar; }
        set { buyCar = value; }
    }
    /// <summary>
    /// 婚姻状况
    /// </summary>
    public string Marriage
    {
        get { return marriage; }
        set { marriage = value; }
    }
    /// <summary>
    /// 现在是否有孩子
    /// </summary>
    public string HaveBaby
    {
        get { return haveBaby; }
        set { haveBaby = value; }
    }
    /// <summary>
    /// 未来是否要小孩
    /// </summary>
    public string HavingBaby
    {
        get { return havingBaby; }
        set { havingBaby = value; }
    }
    /// <summary>
    /// 吸烟
    /// </summary>
    public string Smoke
    {
        get { return smoke; }
        set { smoke = value; }
    }
    /// <summary>
    /// 喝酒
    /// </summary>
    public string Drink
    {
        get { return drink; }
        set { drink = value; }
    }
    /// <summary>
    /// 使用语言
    /// </summary>
    public string UseLanguage
    {
        get { return useLanguage; }
        set { useLanguage = value; }
    }
    /// <summary>
    /// 性格爱好
    /// </summary>
    public string SexLike
    {
        get { return sexLike; }
        set { sexLike = value; }
    }
    #endregion

    #region  设置交友条件－－－实体
    private string friendCome = "";
    private int friendAgeStar = 0;
    private int friendAgeEnd = 0;
    private int friendStatureStar = 0;
    private int friendStatureEnd = 0;
    private string friendMarriage = "";

    /// <summary>
    /// 朋友地址
    /// </summary>
    public string FriendCome
    {
        get { return friendCome; }
        set { friendCome = value; }
    }
    /// <summary>
    /// 朋友最小年龄
    /// </summary>
    public int FriendAgeStar
    {
        get { return friendAgeStar; }
        set { friendAgeStar = value; }
    }
    /// <summary>
    /// 朋友最大年龄
    /// </summary>
    public int FriendAgeEnd
    {
        get { return friendAgeEnd; }
        set { friendAgeEnd = value; }
    }
    /// <summary>
    /// 朋友最低身高
    /// </summary>
    public int FriendStatureStar
    {
        get { return friendStatureStar; }
        set { friendStatureStar = value; }
    }
    /// <summary>
    /// 朋友最高身高
    /// </summary>
    public int FriendStatureEnd
    {
        get { return friendStatureEnd; }
        set { friendStatureEnd = value; }
    }
    /// <summary>
    /// 朋友婚姻状态
    /// </summary>
    public string FriendMarriage
    {
        get { return friendMarriage; }
        set { friendMarriage = value; }
    }
    #endregion

    #region　　添加会员注册信息
    /// <summary>
    /// 添加会员注册信息（档案）
    /// </summary>
    /// <param name="login"></param>
    /// <returns></returns>
    public int Login(MarriageLogin login)
    {
        SqlParameter[] parms ={
            data.MakeInParam("@UserName",SqlDbType.VarChar,100,login.UserName),
            data.MakeInParam("@PassWord",SqlDbType.VarChar,30,login.PassWord),
            data.MakeInParam("@NickName",SqlDbType.VarChar,30,login.NickName),
            data.MakeInParam("@Sex",SqlDbType.VarChar,10,login.Sex),
            data.MakeInParam("@BirthDay",SqlDbType.VarChar,20,login.BirthDay),
            data.MakeInParam("@Address",SqlDbType.VarChar,150,login.Address),
            data.MakeInParam("@Stature",SqlDbType.VarChar,10,login.Stature),
            data.MakeInParam("@Avoirdupois",SqlDbType.VarChar,10,login.Avoirdupois),
            data.MakeInParam("@Education",SqlDbType.VarChar,10,login.Education),
            data.MakeInParam("@Earning",SqlDbType.VarChar,20,login.Earning),

            data.MakeInParam("@Nation",SqlDbType.VarChar,10,login.Nation),
            data.MakeInParam("@BloodType",SqlDbType.VarChar,6,login.BloodType),
            data.MakeInParam("@Looks",SqlDbType.VarChar,50,login.Looks),
            data.MakeInParam("@Metier",SqlDbType.VarChar,20,login.Metier),
            data.MakeInParam("@Housing",SqlDbType.VarChar,20,login.Housing),
            data.MakeInParam("@BuyCar",SqlDbType.VarChar,20,login.BuyCar),
            data.MakeInParam("@Marriage",SqlDbType.VarChar,20,login.Marriage),
            data.MakeInParam("@HaveBaby",SqlDbType.VarChar,6,login.HaveBaby),
            data.MakeInParam("@HavingBaby",SqlDbType.VarChar,200,login.HavingBaby),
            data.MakeInParam("@Smoke",SqlDbType.VarChar,6,login.Smoke),
            data.MakeInParam("@Drink",SqlDbType.VarChar,6,login.Drink),
            data.MakeInParam("@UseLanguage",SqlDbType.VarChar,50,login.UseLanguage),
            data.MakeInParam("@SexLike",SqlDbType.VarChar,500,login.SexLike),

            data.MakeInParam("@FriendCome",SqlDbType.VarChar,150,login.FriendCome),
            data.MakeInParam("@FriendAgeStar",SqlDbType.Int,4,login.FriendAgeStar),
            data.MakeInParam("@FriendAgeEnd",SqlDbType.Int,4,login.FriendAgeEnd),
            data.MakeInParam("@FriendStatureStar",SqlDbType.Int,4,login.FriendStatureStar),
            data.MakeInParam("@FriendStatureEnd",SqlDbType.Int,4,login.FriendStatureEnd),
            data.MakeInParam("@FriendMarriage",SqlDbType.VarChar,10,login.FriendMarriage),
        };
        int i = data.RunProc("INSERT INTO tb_User (UserName, PassWord,NickName, Sex, BirthDay," +
            " Address, Stature, Avoirdupois, Education, Earning, " +
        "Nation, BloodType, Looks, Metier,Housing ,BuyCar ,Marriage ,HaveBaby ," +
        "HavingBaby ,Smoke ,Drink ,UseLanguage ,SexLike," +
        "FriendCome,FriendAgeStar,FriendAgeEnd,FriendStatureStar,FriendStatureEnd,FriendMarriage" +
        ") VALUES (@UserName, @PassWord, @NickName, @Sex, @BirthDay, @Address, @Stature, @Avoirdupois, @Education, @Earning, " +
        "@Nation, @BloodType, @Looks, @Metier,@Housing ,@BuyCar ,@Marriage ,@HaveBaby ,@HavingBaby ,@Smoke ,@Drink ,@UseLanguage ,@SexLike," +
        "@FriendCome,@FriendAgeStar,@FriendAgeEnd,@FriendStatureStar,@FriendStatureEnd,@FriendMarriage)", parms);
        return i;
    }
    #endregion
    /// <summary>
    /// 修改会员档案信息
    /// </summary>
    /// <param name="login"></param>
    /// <returns></returns>
    public int LoginUpdate(MarriageLogin login)
    {
        SqlParameter[] parms ={
            data.MakeInParam("@UserName",SqlDbType.VarChar,100,login.UserName),
            data.MakeInParam("@PassWord",SqlDbType.VarChar,30,login.PassWord),
            data.MakeInParam("@NickName",SqlDbType.VarChar,30,login.NickName),
            //data.MakeInParam("@Sex",SqlDbType.VarChar,10,login.Sex),
            //data.MakeInParam("@BirthDay",SqlDbType.VarChar,20,login.BirthDay),
            data.MakeInParam("@Address",SqlDbType.VarChar,150,login.Address),
            data.MakeInParam("@Stature",SqlDbType.VarChar,10,login.Stature),
            data.MakeInParam("@Avoirdupois",SqlDbType.VarChar,10,login.Avoirdupois),
            data.MakeInParam("@Education",SqlDbType.VarChar,10,login.Education),
            data.MakeInParam("@Earning",SqlDbType.VarChar,20,login.Earning),

            data.MakeInParam("@Nation",SqlDbType.VarChar,10,login.Nation),
            data.MakeInParam("@BloodType",SqlDbType.VarChar,6,login.BloodType),
            data.MakeInParam("@Looks",SqlDbType.VarChar,50,login.Looks),
            data.MakeInParam("@Metier",SqlDbType.VarChar,20,login.Metier),
            data.MakeInParam("@Housing",SqlDbType.VarChar,20,login.Housing),
            data.MakeInParam("@BuyCar",SqlDbType.VarChar,20,login.BuyCar),
            data.MakeInParam("@Marriage",SqlDbType.VarChar,20,login.Marriage),
            data.MakeInParam("@HaveBaby",SqlDbType.VarChar,6,login.HaveBaby),
            data.MakeInParam("@HavingBaby",SqlDbType.VarChar,200,login.HavingBaby),
            data.MakeInParam("@Smoke",SqlDbType.VarChar,6,login.Smoke),
            data.MakeInParam("@Drink",SqlDbType.VarChar,6,login.Drink),
            data.MakeInParam("@UseLanguage",SqlDbType.VarChar,50,login.UseLanguage),
            data.MakeInParam("@SexLike",SqlDbType.VarChar,500,login.SexLike),

            data.MakeInParam("@FriendCome",SqlDbType.VarChar,150,login.FriendCome),
            data.MakeInParam("@FriendAgeStar",SqlDbType.Int,4,login.FriendAgeStar),
            data.MakeInParam("@FriendAgeEnd",SqlDbType.Int,4,login.FriendAgeEnd),
            data.MakeInParam("@FriendStatureStar",SqlDbType.Int,4,login.FriendStatureStar),
            data.MakeInParam("@FriendStatureEnd",SqlDbType.Int,4,login.FriendStatureEnd),
            data.MakeInParam("@FriendMarriage",SqlDbType.VarChar,10,login.FriendMarriage),
        };
        int i = data.RunProc("Update tb_User set  " + (login.PassWord == "" ? "" : "PassWord=@PassWord,") + "NickName=@NickName, Address=@Address," +
            " Stature=@Stature, Avoirdupois=@Avoirdupois, Education=@Education, Earning=@Earning, " +
        "Nation= @Nation, BloodType= @BloodType, Looks= @Looks, Metier= @Metier,Housing = @Housing ," +
        "BuyCar =@BuyCar ,Marriage =@Marriage ,HaveBaby =@HaveBaby ,HavingBaby =@HavingBaby ," +
        "Smoke =@Smoke ,Drink =@Drink ,UseLanguage = @UseLanguage,SexLike = @SexLike," +
        "FriendCome =@FriendCome ,FriendAgeStar =@FriendAgeStar ,FriendAgeEnd =@FriendAgeEnd ," +
        "FriendStatureStar =@FriendStatureStar ,FriendStatureEnd =@FriendStatureEnd ,FriendMarriage =@FriendMarriage  where UserName=@UserName", parms);
        return i;
    }
    /// <summary>
    /// 删除会员信息
    /// </summary>
    /// <param name="id"></param>
    /// <returns></returns>
    public int LoginDelete(string id)
    {
        //执行sql语句
        return data.RunProc("delete from tb_User where id=" + id);
    }

    #region　　会员登录
    /// <summary>
    /// 验证会员登录名称和密码
    /// </summary>
    /// <param name="login"></param>
    /// <returns></returns>
    public DataSet Logon(MarriageLogin login)
    {
        SqlParameter[] parms ={
            data.MakeInParam("@UserName",SqlDbType.VarChar,100,login.UserName),
            data.MakeInParam("@PassWord",SqlDbType.VarChar,30,login.PassWord),
        };
        return data.RunProcReturn("Select * from tb_User where UserName=@UserName and PassWord=@PassWord", parms, "tb_User");
    }

    #endregion

    /// <summary>
    /// 查询所有的会员信息
    /// </summary>
    /// <returns></returns>
    public DataSet SelectUser()
    {
        return data.RunProcReturn("select * from tb_User", "tb_User");
    }
    /// <summary>
    /// 查询自己的基础信息
    /// </summary>
    /// <param name="userName"></param>
    /// <returns></returns>
    public DataTable GetTableUserDes(string userName)
    {
        return data.RunProcReturn("select * from tb_User where UserName='" + userName + "'", "tb_User").Tables[0];
    }
    /// <summary>
    /// 按指定条件查询会员信息
    /// </summary>
    /// <param name="login"></param>
    /// <returns></returns>
    public DataSet SelcetUserResult(MarriageLogin login, bool checkPhoto)
    {
        //设置sql语句需要的参数
        SqlParameter[] parms ={
            data.MakeInParam("@Sex",SqlDbType.VarChar,4,login.Sex),
            data.MakeInParam("@FriendAgeStar",SqlDbType.Int,4,login.FriendAgeStar),
            data.MakeInParam("@FriendAgeEnd",SqlDbType.Int,4,login.FriendAgeEnd),
            data.MakeInParam("@Address",SqlDbType.VarChar,100,"%"+login.Address+"%"),
        };
        if (checkPhoto)  //显示查询结果有相片交友信息
            //根据参数执行sql语句
            return data.RunProcReturn("select * from tb_User where Sex=@Sex and DATEDIFF(year, Birthday, getdate()) >= @FriendAgeStar and DATEDIFF(year, Birthday, getdate()) <= @FriendAgeEnd and Address like @Address and photoPath <> '~/JiaoYou/Photo/nophoto.gif'", parms, "tb_User");
        else
            return data.RunProcReturn("select * from tb_User where Sex=@Sex and DATEDIFF(year, Birthday, getdate()) >= @FriendAgeStar and DATEDIFF(year, Birthday, getdate()) <= @FriendAgeEnd and Address like @Address", parms, "tb_User");
    }
    /// <summary>
    /// 根据会员Email获取会员的ＩＤ
    /// </summary>
    /// <param name="user"></param>
    /// <returns></returns>
    public string SelectUserID(string user)
    {
        SqlParameter[] parms ={
            data.MakeInParam("@UserName",SqlDbType.VarChar,30,user),
        };
        return data.RunProcReturn("Select * from tb_User where UserName =@UserName ", parms, "tb_User").Tables[0].Rows[0][0].ToString();
    }
    /// <summary>
    /// 根据ＩＤ获取会员Ｅmail
    /// </summary>
    /// <param name="id"></param>
    /// <returns></returns>
    public string SelectUserName(string id)
    {
        return data.RunProcReturn("Select * from tb_User where id =" + id, "tb_User").Tables[0].Rows[0][1].ToString();
    }

    /// <summary>
    /// 获取指定会员的档案信息
    /// </summary>
    /// <param name="user"></param>
    /// <returns></returns>
    public DataSet SelectUser(string user)
    {
        SqlParameter[] parms ={
            data.MakeInParam("@UserName",SqlDbType.VarChar,30,user),
        };
        return data.RunProcReturn("Select * from tb_User where UserName =@UserName ", parms, "tb_User");
    }
    /// <summary>
    /// 上传图片
    /// </summary>
    /// <param name="id"></param>
    /// <param name="PhotoPath"></param>
    /// <returns></returns>
    public int UpdateUserPhoto(string id, string PhotoPath)
    {
        return data.RunProc("update tb_User set PhotoPath='" + PhotoPath + "'where id=" + id);
    }
    /// <summary>
    /// 查询密码
    /// </summary>
    /// <param name="userName"></param>
    /// <returns></returns>
    public string GetPwd(string userName)
    {
        return data.RunProcReturn("Select PassWord from tb_User where UserName ='" + userName + "'", "tb_User").Tables[0].Rows[0][0].ToString();
    }
}
