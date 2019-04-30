<%@ Page Language="C#" AutoEventWireup="true" CodeFile="FriendMsg.aspx.cs" Inherits="JiaoYou_Login_FriendMsg" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title>会员信息</title>
    <link href="../css/css.css" rel="stylesheet" type="text/css" />

</head>
<body>
    <form id="form1" runat="server">
        <div>
            <table style="width: 795px; height: 338px" cellpadding="0" cellspacing="0" align="center" bgcolor="#ffffff">
                <tr>
                    <td align="center" bgcolor="#ffccff" class="cuhong" colspan="3" style="height: 34px; color:#EF93BF">会员短消息</td>
                </tr>
                <tr>
                    <td colspan="3" style="height: 30px">
                        <%--单选按钮--%>
                        <asp:RadioButton ID="rdoBtnAll" runat="server" AutoPostBack="True" Checked="True"
                            GroupName="check" OnCheckedChanged="rdoBtnAll_CheckedChanged" Text="全部信息" />&nbsp;&nbsp;&nbsp;
                        <asp:RadioButton ID="rdoBtnTrue" runat="server" AutoPostBack="True" GroupName="check"
                            OnCheckedChanged="rdoBtnTrue_CheckedChanged" Text="已阅读信息" />
                        <asp:RadioButton ID="rdoBtnFalse" runat="server" AutoPostBack="True" GroupName="check"
                            OnCheckedChanged="rdoBtnFalse_CheckedChanged" Text="未阅读信息" /></td>
                </tr>
                <tr>
                    <td colspan="3" style="height: 247px;" valign="top">
                        <%--显示所有信息的列表和翻页控件--%>
                        <asp:GridView ID="GridView1" runat="server" AllowPaging="True" AutoGenerateColumns="False"
                            BackColor="White" BorderColor="#CC9966" BorderStyle="None" BorderWidth="1px"
                            CellPadding="4" OnPageIndexChanging="GridView1_PageIndexChanging" OnRowDataBound="GridView1_RowDataBound"
                            OnRowDeleting="GridView1_RowDeleting" OnRowEditing="GridView1_RowEditing" OnSelectedIndexChanging="GridView1_SelectedIndexChanging"
                            PageSize="15" Width="783px">
                            <PagerSettings FirstPageText="第一页" LastPageText="末一页" Mode="NextPreviousFirstLast"
                                NextPageText="下一页" PreviousPageText="上一页" />
                            <FooterStyle BackColor="#FFFFCC" ForeColor="#330099" />
                            <Columns>
                                <asp:BoundField DataField="MsgDate" DataFormatString="{0:yyyy-MM-dd}" HeaderText="时间" HtmlEncode="False" />
                                <asp:BoundField DataField="NickName" HeaderText="对方昵称" />
                                <asp:BoundField DataField="Sender" HeaderText="对方邮件" />
                                <asp:BoundField DataField="Msg" HeaderText="短信息" />
                                <asp:CommandField SelectText="查看消息" ShowSelectButton="True" />
                                <asp:CommandField EditText="回复信息" ShowEditButton="True" />
                                <asp:CommandField ShowDeleteButton="True" />
                            </Columns>
                            <RowStyle BackColor="White" ForeColor="#330099" />
                            <SelectedRowStyle BackColor="#FFCC66" Font-Bold="True" ForeColor="#663399" />
                            <PagerStyle BackColor="#FFFFCC" ForeColor="#330099" HorizontalAlign="Center" />
                            <HeaderStyle BackColor="#939393" Font-Bold="True" ForeColor="#FFFFCC" />
                        </asp:GridView>
                    </td>
                </tr>
                <tr>
                    <td colspan="3" style="vertical-align: top"></td>
                </tr>
            </table>

        </div>
    </form>
</body>
</html>
