<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="WebForm11Login.aspx.cs" Inherits="WebApplication1.WebForm11Login" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title></title>
</head>
<body>
    <form id="form1" runat="server">
        <asp:Label runat="server" style="font-weight: 700" Text="Login"></asp:Label>
        <div>
        </div>
        <asp:Label runat="server" Text="User Id"></asp:Label>
        <asp:TextBox ID="txtUserId" runat="server"></asp:TextBox>
        <br />
        <asp:Label ID="Label2" runat="server" Text="Password"></asp:Label>
        <asp:TextBox ID="txtPassword" runat="server"></asp:TextBox>
        <p>
            <asp:Button ID="Button1" runat="server" OnClick="Btn_Login" Text="Login" />
                    <asp:Label runat="server" ID="labelLogin" ></asp:Label>

        </p>
    </form>
</body>
</html>
