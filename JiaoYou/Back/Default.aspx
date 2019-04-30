<%@ Page Language="C#" AutoEventWireup="true" CodeFile="Default.aspx.cs" Inherits="JiaoYou_Back_Default" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title>交友网后台管理中心</title>
    <link href="../css/css.css" rel="stylesheet" type="text/css" />
</head>
<body>
    <form id="form1" runat="server">
        <table id="__01" width="1003" height="621" border="0" cellpadding="0" cellspacing="0">
            <tr>
                <td colspan="2" rowspan="2">
                    <img src="images/ack_Default_01.gif" width="273" height="101" alt=""></td>
                <td rowspan="2">
                    <img src="images/ack_Default_02.gif" width="439" height="101" alt=""></td>
                <td>
                    <img src="images/ack_Default_03.gif" width="181" height="74" alt=""></td>
                <td rowspan="4">
                    <img src="images/ack_Default_04.gif" width="110" height="620" alt=""></td>
            </tr>
            <tr>
                <td>
                    <img src="images/ack_Default_05.gif" alt="" width="181" height="27" border="0" usemap="#Map"></td>
            </tr>
            <tr>
                <td rowspan="2">
                    <img src="images/ack_Default_06.gif" width="32" height="519" alt=""></td>
                <td height="445" colspan="3" valign="top" background="images/ack_Default_07.gif">
                    <table width="861" height="426" cellpadding="0" cellspacing="0">
                        <tr>
                            <td height="23" colspan="3">
                                &nbsp;</td>
                        </tr>
                        <tr>
                            <td width="20">
                                &nbsp;</td>
                            <td width="754" valign="top" class="huise">
                                <asp:GridView ID="GridView1" runat="server" AllowPaging="True" AutoGenerateColumns="False"
                                    BackColor="White" BorderColor="#CC9966" BorderStyle="None" BorderWidth="1px"
                                    CellPadding="4" OnPageIndexChanging="GridView1_PageIndexChanging" OnRowDeleting="GridView1_RowDeleting"
                                    OnSelectedIndexChanging="GridView1_SelectedIndexChanging" Width="745px" OnRowDataBound="GridView1_RowDataBound">
                                    <PagerSettings FirstPageText="第一页" LastPageText="末一页" Mode="NextPreviousFirstLast"
                                        NextPageText="下一页" PreviousPageText="上一页" />
                                    <FooterStyle BackColor="#FFFFCC" ForeColor="#330099" />
                                    <Columns>
                                        <asp:BoundField DataField="UserName" HeaderText="电子邮件" />
                                        <asp:BoundField DataField="NickName" HeaderText="昵称" />
                                        <asp:BoundField DataField="Sex" HeaderText="性别" />
                                        <asp:BoundField DataField="Birthday" HeaderText="出生日期" />
                                        <asp:BoundField DataField="Address" HeaderText="地址" />
                                        <asp:CommandField SelectText="详细档案" ShowSelectButton="True" />
                                        <asp:CommandField DeleteText="删除信息" ShowDeleteButton="True" />
                                    </Columns>
                                    <RowStyle BackColor="White" ForeColor="#330099" />
                                    <SelectedRowStyle BackColor="#FFCC66" Font-Bold="True" ForeColor="#663399" />
                                    <PagerStyle BackColor="#FFFFCC" ForeColor="#330099" HorizontalAlign="Center" />
                                    <HeaderStyle BackColor="#990000" Font-Bold="True" ForeColor="#FFFFCC" />
                                </asp:GridView>
                            </td>
                            <td width="85">
                                &nbsp;</td>
                        </tr>
                    </table>
                </td>
            </tr>
            <tr>
                <td colspan="3">
                    <img src="images/ack_Default_08.gif" width="861" height="74" alt=""></td>
            </tr>
            <tr>
                <td>
                    <img src="images/分隔符.gif" width="32" height="1" alt=""></td>
                <td>
                    <img src="images/分隔符.gif" width="241" height="1" alt=""></td>
                <td>
                    <img src="images/分隔符.gif" width="439" height="1" alt=""></td>
                <td>
                    <img src="images/分隔符.gif" width="181" height="1" alt=""></td>
                <td>
                    <img src="images/分隔符.gif" width="110" height="1" alt=""></td>
            </tr>
        </table>
            <map name="Map">
        <area shape="rect" coords="14,7,71,26" href="../login.html">
        <area shape="rect" coords="92,6,146,24" href="../Login/index.html">
    </map>
        
    </form>

</body>
</html>
