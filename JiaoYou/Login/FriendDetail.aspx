<%@ Page Language="C#" AutoEventWireup="true" CodeFile="FriendDetail.aspx.cs" Inherits="JiaoYou_Login_FriendDetail" %>


<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title>无标题页</title>
    <link href="../css/css.css" rel="stylesheet" type="text/css" />
    <script language="JavaScript" type="text/javascript" src="../js/area_data.js"></script>
    <script src="../js/jquery-1.10.2.min.js"></script>
    <script>
        //获取url参数
        function getQueryString(name) {
            var reg = new RegExp("(^|&)" + name + "=([^&]*)(&|$)", "i");
            var r = window.location.search.substr(1).match(reg);
            if (r != null) return unescape(r[2]); return null;
        }
        function addF() {
            var f = getQueryString('uname');
            var pd = { "t": "10", "txtUserName": f };
            $.ajax({
                type: "post",
                url: "../tools/Handler.ashx",
                data: pd,
                dataType: "json",
                success: function (data) {
                    if (data.status == "-1") {
                        alert('请登录');
                    }
                    else if (data.status == "-2") {
                        alert('已经添加过了');
                    }
                    else {
                        alert('添加好友成功');
                        window.parent.SelectF();
                        //关闭本窗体
                        var close = $('#CloseCover', parent.document);
                        $('#coverDiv', parent.document).remove();
                        $(close).parent().parent().remove();
                    }
                },
                error: function (XMLHttpRequest, textStatus, errorThrown) {
                }
            });
        }
    </script>
</head>
<body>
    <form id="form1" runat="server">
        <div style="width: 740px; margin: auto;">
            <div class="cuhong">基本信息：</div>
            <div style="height: 155px; margin-top: 5px;">
                <div style="float: left; border: 1px solid #000;">
                    <asp:Image ID="imgPhoto" runat="server" Height="150px" Width="120px" />
                </div>
                <div style="float: left; margin-left: 30px;">
                    <div style="border-bottom: 1px solid #808080; height: 28px;">
                        昵 称：<asp:Label ID="lblBase_NickName" runat="server" Text="Label" CssClass="xiaolv"></asp:Label>
                        &nbsp;&nbsp; 性别：<asp:Label ID="lblBase_Sex" runat="server" Text="Label" CssClass="xiaolv"></asp:Label>
                        &nbsp; &nbsp; 出生日期：<asp:Label ID="lblBase_BirthDay" runat="server" Text="Label" CssClass="xiaolv"></asp:Label>
                    </div>
                    <div style="border-bottom: 1px solid #808080; height: 28px;">
                        婚姻状况：<asp:Label ID="lblBase_Marray" runat="server" Text="Label" CssClass="xiaolv"></asp:Label><span class="xiaolv">
                        </span>&nbsp; &nbsp;&nbsp; 身高：<asp:Label ID="lblBase_Hight" runat="server" Text="Label" CssClass="xiaolv"></asp:Label>
                        &nbsp;&nbsp; 体 重：<asp:Label ID="lblBase_Kg" runat="server" Text="Label" CssClass="xiaolv"></asp:Label>
                    </div>
                    <div style="border-bottom: 1px solid #808080; height: 28px;">
                        民 族：<asp:Label ID="lblBase_Mz" runat="server" Text="Label" CssClass="xiaolv"></asp:Label>
                        &nbsp; &nbsp; &nbsp; &nbsp; 学历：<asp:Label ID="lblBase_Study" runat="server" Text="Label" CssClass="xiaolv"></asp:Label>
                        &nbsp;&nbsp; 年 收 入：
                        <asp:Label ID="lblBase_Menoy" runat="server" Text="Label" CssClass="xiaolv"></asp:Label>
                        <span class="xiaolv"></span>
                    </div>
                    <div style="border-bottom: 1px solid #808080; height: 28px;">
                        相 貌：<asp:Label ID="lblBase_Look" runat="server" Text="Label" CssClass="xiaolv"></asp:Label><span class="xiaolv">
                        </span>&nbsp; &nbsp; &nbsp; &nbsp; 职业：<asp:Label ID="lblBase_Job" runat="server" Text="Label" CssClass="xiaolv"></asp:Label>
                        &nbsp;住 房：<asp:Label ID="lblBase_House" runat="server" Text="Label" CssClass="xiaolv"></asp:Label>
                    </div>
                    <div style="border-bottom: 1px solid #808080; height: 28px;">
                        所在地区：<asp:Label ID="lblBase_Area" runat="server" Text="Label" CssClass="xiaolv"></asp:Label><span class="xiaolv"> </span>
                    </div>
                </div>
                <div style="float: left; height: 155px; position: relative; width: 100px;">
                    <input type="button" value="添加好友" style="position: absolute; right: 10px; bottom: 20px; width: 78px;" onclick="addF()" />
                </div>
            </div>
            <div class="cuhong">真情告白：</div>
            <div class="hei" style="vertical-align: top; background-image: url('images/huise.gif'); height: 85px;">
                &nbsp; &nbsp;
                    <asp:Label ID="lblSexLike" runat="server" Text="Label" CssClass="xiaolv"></asp:Label>
            </div>
            <div class="cuhong">择友要求：</div>
            <div class="hei" style="background-image: url('images/huise.gif'); height: 85px;">
                年 龄：<asp:Label ID="lblFAge" runat="server" Text="Label" CssClass="xiaolv"></asp:Label>
                <font style="margin-left: 30px;">身 高：</font><asp:Label ID="lblFHight" runat="server" Text="Label" CssClass="xiaolv"></asp:Label>
                <span class="xiaolv"></span>
                <br />
                所在地区：<asp:Label ID="lblFArea" runat="server" Text="Label" CssClass="xiaolv"></asp:Label>
                <font style="margin-left: 30px;">婚姻状况：</font><asp:Label ID="lblFMarray" runat="server" Text="Label" CssClass="xiaolv"></asp:Label>
                <span class="xiaolv"></span>
            </div>
            <div class="cuhong">其它信息：</div>
            <div class="hei" style="background-image: url('images/huise.gif'); height: 98px;">
                <table style="width: 713px" cellpadding="0" cellspacing="0">
                    <tr>
                        <td style="width: 101px">是否有孩子：</td>
                        <td style="width: 304px">
                            <asp:Label ID="lblHaveChild" runat="server" Text="Label" CssClass="xiaolv"></asp:Label></td>
                        <td style="width: 99px">是否想要孩子：</td>
                        <td>
                            <asp:Label ID="lblHavingChild" runat="server" Text="Label" CssClass="xiaolv"></asp:Label></td>
                    </tr>
                    <tr>
                        <td style="width: 101px">吸烟：</td>
                        <td style="width: 304px">
                            <asp:Label ID="lblSmoke" runat="server" Text="Label" CssClass="xiaolv"></asp:Label></td>
                        <td style="width: 99px">喝酒：</td>
                        <td>
                            <asp:Label ID="lblDrink" runat="server" Text="Label" CssClass="xiaolv"></asp:Label></td>
                    </tr>
                    <tr>
                        <td style="width: 101px">血型：</td>
                        <td style="width: 304px">
                            <asp:Label ID="lblBlood" runat="server" Text="Label" CssClass="xiaolv"></asp:Label></td>
                        <td style="width: 99px">交流语言：</td>
                        <td>
                            <asp:Label ID="lblLanguage" runat="server" Text="Label" CssClass="xiaolv"></asp:Label></td>
                    </tr>
                </table>
            </div>
        </div>
    </form>
</body>
</html>
