<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="WebForm10.aspx.cs" Inherits="WebApplication1.WebForm10" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title></title>
</head>
<body>
    <form id="form1" runat="server">
        <div>

            <asp:Label ID="Label4" runat="server" style="font-weight: 700" Text="Registration"></asp:Label>
            <br />
            <br />

        </div>
        <asp:Label ID="Label1" runat="server" Text="Name"></asp:Label>
        <asp:TextBox ID="txtName" runat="server"></asp:TextBox>
        <p>
            <asp:Label ID="Label2" runat="server" Text="Mobile"></asp:Label>
            <asp:TextBox ID="txtMobile" runat="server" MaxLength="10"></asp:TextBox>
        </p>
        <p>
            <asp:Label ID="Label3" runat="server" Text="Email"></asp:Label>
            <asp:TextBox ID="txtEmail" runat="server" style="margin-bottom: 0px"></asp:TextBox>
        </p>
        <p>
            <asp:Button ID="Button1" runat="server" OnClick="Rg_submit" Text="Submit" />
            <asp:Label ID="lblMessage" runat="server" Text=""></asp:Label>
        </p>
    </form>
</body>
</html>
