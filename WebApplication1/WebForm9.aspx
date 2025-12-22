<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="WebForm9.aspx.cs" Inherits="WebApplication1.WebForm9" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title></title>
</head>
<body>
    <form id="form1" runat="server">
        <div>
            <asp:TextBox ID="txtSubject" runat="server"></asp:TextBox><br />
            <%--<asp:DropDownList ID="ddlSession" runat="server"></asp:DropDownList>--%> <br />
            <asp:Button ID="btnSubmit" OnClick="Btn_Submit" runat="server" Text="submit" />
            <br />
            <br />
            <asp:GridView ID="GridView1" DataKeyNames="SessionID" OnRowDataBound="GridView1_RowDataBound"   AutoGenerateColumns="false"  runat="server">

                <Columns>
                    <asp:BoundField  DataField="SessionID" HeaderText="SessionID" Visible="false"/>
                    <asp:BoundField  DataField="SessionName" HeaderText="SessionName" />
<%--                    <asp:BoundField  DataField="Status" HeaderText="Status" />--%>
                <asp:TemplateField HeaderText="Status">
                    <ItemTemplate>
                    <%--<asp:RadioButton ID="rbSelect" runat="server" GroupName="StudentSelection" OnCheckedChanged="chkRow_CheckedChanged" AutoPostBack="True" />--%>
                      <asp:CheckBox ID="cbStatus" AutoPostBack="true"  Checked='<%# Eval("SessionID") != DBNull.Value && Convert.ToInt32(Eval("SessionID")) > 0 %>'  OnCheckedChanged="chkRow_CheckedChanged" runat="server" />

                    </ItemTemplate>
                </asp:TemplateField>
                </Columns>
            </asp:GridView>
        </div>
    </form>
</body>
</html>

