<%@ Page Language="C#" AutoEventWireup="true" CodeFile="SendMsg.aspx.cs" Inherits="JiaoYou_SendMsg" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml" >
<head runat="server">
    <title>发送短消息</title>
    <link href="css/css.css"rel="stylesheet" type="text/css" />
</head>
<body>
    <form id="form1" runat="server">
        <table align="center" cellpadding="0" cellspacing="0" height="290" width="607">
            <tr>
                <td background="images/duihua.JPG" height="399" valign="top" width="607">
                    <table cellpadding="0" cellspacing="0" height="333" width="607">
                        <tr>
                            <td rowspan="2" width="10">
                                &nbsp;</td>
                            <td height="28" width="30">
                                &nbsp;</td>
                            <td colspan="2" height="28">
                                &nbsp;</td>
                        </tr>
                        <tr>
                            <td height="304" valign="top">
                                <asp:TextBox ID="txtMsg" runat="server" Height="295px" TextMode="MultiLine" Width="460px"></asp:TextBox>
                    <asp:RequiredFieldValidator ID="RequiredFieldValidator1" runat="server" ControlToValidate="txtMsg"
                        ErrorMessage="** 不允许发送空信息" ForeColor="White"></asp:RequiredFieldValidator></td>
                            <td valign="middle" width="120">
                                <table align="center" cellpadding="0" cellspacing="0" height="120" width="106">
                                    <tr>
                                        <td>
                                            <table align="center" bgcolor="#000000" border="1" bordercolor="#ffffff" cellpadding="1"
                                                cellspacing="1" height="123" width="100">
                                                <tr>
                                                    <td align="center" bgcolor="#ffffff" height="119" valign="middle">
                                            <asp:Image ID="imgFriend" runat="server" Height="115px" Width="93px" /></td>
                                                </tr>
                                            </table>
                                        </td>
                                    </tr>
                                    <tr>
                                        <td align="center" class="hei">
                                            昵称：<asp:Label ID="lblFriendNick" runat="server" Text="Label" CssClass="hei"></asp:Label></td>
                                    </tr>
                                </table>
                                <table align="center" cellpadding="0" cellspacing="0" height="120" width="106">
                                    <tr>
                                        <td>
                                            <table align="center" bgcolor="#000000" border="1" bordercolor="#ffffff" cellpadding="1"
                                                cellspacing="1" height="123" width="100">
                                                <tr>
                                                    <td align="center" bgcolor="#ffffff" height="119" valign="middle">
                                            <asp:Image ID="imgUser" runat="server" Height="115px" Width="93px" /></td>
                                                </tr>
                                            </table>
                                        </td>
                                    </tr>
                                    <tr>
                                        <td align="center" class="hei">
                                            昵称：<asp:Label ID="lblUserNick" runat="server" Text="Label" CssClass="hei"></asp:Label></td>
                                    </tr>
                                </table>
                            </td>
                            <td width="7">
                                &nbsp;</td>
                        </tr>
                    </table>
                    <table height="62" width="601">
                        <tr>
                            <td>
                                &nbsp; 
                    <asp:ImageButton ID="imgBtnSend" runat="server" AlternateText="发送信息" OnClick="imgBtnSend_Click" ImageUrl="images/fasongxinxi.GIF" /><a href="#"></a></td>
                        </tr>
                    </table>
                </td>
            </tr>
        </table>
    </form>
</body>
</html>
