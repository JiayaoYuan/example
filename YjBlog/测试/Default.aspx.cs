using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;
using System.Data.SqlClient;

public partial class 测试_Default : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {
        DataBase db = new DataBase();
        db.Open();
        if (db.con.State == ConnectionState.Open)
        {
            label1.Text = "连接成功";
        }
        else {
            label1.Text = "连接失败";
        }
    }
}