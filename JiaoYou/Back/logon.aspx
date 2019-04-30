<%@ Page Language="C#" AutoEventWireup="true" CodeFile="logon.aspx.cs" Inherits="JiaoYou_Back_logon" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml" >
<head runat="server">
    <title>交友网管理后台登录</title>
    <style type="text/css">
<!--
body {
	background-image: url(images/bg.gif);
}
-->
</style>
<link href="../css/css.css"rel="stylesheet" type="text/css">
</head>
<body>
    <form id="form1" runat="server">
        <br />
        <br />
        <br />
        <br />
        <br />
        <br />
        <br />
        <br />
        <table id="__01" align="center" border="0" cellpadding="0" cellspacing="0" height="313"
            width="682">
            <tr>
                <td>
                    <img alt="" height="59" src="images/login_01.gif" width="328" /></td>
                <td>
                    <img alt="" height="59" src="images/login_02.gif" width="354" /></td>
            </tr>
            <tr>
                <td rowspan="3">
                    <img alt="" height="254" src="images/login_03.gif" width="328" /></td>
                <td>
                    <img alt="" height="58" src="images/login_04.gif" width="354" /></td>
            </tr>
            <tr>
                <td background="images/login_05.gif" height="119" valign="top" width="354">
                    <table cellpadding="0" cellspacing="0" height="92" width="311">
                        <tr>
                            <td rowspan="3" style="width: 120px">
                                &nbsp;</td>
                            <td align="center" colspan="2" valign="bottom" style="height: 21px">
                                <asp:TextBox ID="txtName" runat="server" CssClass="biaodan"></asp:TextBox></td>
                            <td rowspan="3" width="85">
                                &nbsp;</td>
                        </tr>
                        <tr>
                            <td colspan="2" valign="bottom" style="height: 17px">
                                <asp:TextBox ID="txtPwd" runat="server" CssClass="biaodan"></asp:TextBox></td>
                        </tr>
                        <tr>
                            <td width="48">
                                <asp:Button ID="btnLogin" runat="server" OnClick="btnLogin_Click" Text="提交" /></td>
                            <td width="70">
                                <input name="Submit2" type="reset" value="重置" /></td>
                        </tr>
                    </table>
                </td>
            </tr>
            <tr>
                <td>
                    <img alt="" height="77" src="images/login_06.gif" width="354" /></td>
            </tr>
        </table>
        <div align="center">
            <span class="qianhuang">技术服务热线：0431-888888888传真：0431-888888888 企业邮箱：mingrisoft@mingrisoft.com<br />
                公司地址：吉林省长春市亚泰广场C座 吉ICP备2568945<br />
                Copyright© All Rights Reserved! </span>
            <!-- ImageReady Slices (玫瑰花.psd) -->
            <!-- End ImageReady Slices -->
            <br />
            <br />
        </div>
    </form>
</body>
</html>
