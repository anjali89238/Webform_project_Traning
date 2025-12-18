<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="WebForm5.aspx.cs" Inherits="WebApplication1.WebForm5" %>
<!DOCTYPE html>
<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title>

    </title>
</head>
<body>
    <form id="form1" runat="server">
        <div>
            Subject
            <asp:DropDownList ID="ddlSubject" runat="server">
     <asp:ListItem Value="English">English</asp:ListItem>
     <asp:ListItem Value="Maths">Maths</asp:ListItem>
     <asp:ListItem Value="Science">Science</asp:ListItem>
     <asp:ListItem Value="Social Science">Social Science</asp:ListItem>
     <asp:ListItem Value="Hindi">Hindi</asp:ListItem>

 </asp:DropDownList>
            <asp:Button ID="btnShow"  runat="server"  Text="show"  OnClick="ShowMarksBySubject" />
            <br />
            <br />

            <asp:GridView ID="GridView1"  runat="server" AutoGenerateColumns="false">
                <Columns>
                    
                    <asp:BoundField  DataField="Name" HeaderText="Name" />
                    <asp:BoundField  DataField="Marks" HeaderText="Marks" />
                    <asp:BoundField  DataField="MaxMarks" HeaderText="MaxMarks" />
                    <asp:BoundField  DataField="Subject" HeaderText="Subject" />
                    <asp:BoundField  DataField="Percentage" HeaderText="Percentage" />

                </Columns>

            </asp:GridView>
           Average Percentage :<asp:Label ID="txtAvgPrcnt" runat="server" Text=""></asp:Label>

        </div>
    </form>
</body>
</html>
