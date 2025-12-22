<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="WebForm8.aspx.cs" Inherits="WebApplication1.WebForm8" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title></title>
</head>
<body>
    <form id="form1" runat="server">
        <div>

            <asp:ScriptManager runat="server" ID="ScriptManager1"></asp:ScriptManager>
          

            Title: 
            <asp:TextBox ID="txtTitle" runat="server"></asp:TextBox>
            <asp:RequiredFieldValidator ID="rfvTitle" runat="server" ControlToValidate="txtTitle" ErrorMessage="Please Enter Title"></asp:RequiredFieldValidator><br />

            Discription:
  <asp:TextBox ID="txtDiscription" TextMode="MultiLine" Width="200px" Height="100px" runat="server"></asp:TextBox>
            <asp:RequiredFieldValidator ID="rfvSDiscription" runat="server" ControlToValidate="txtDiscription" ErrorMessage="Please Enter Discription"></asp:RequiredFieldValidator><br />

            Date: 
            <asp:TextBox ID="txtDate" runat="server"></asp:TextBox>

            <ajaxToolkit:CalendarExtender ID="CalendarExtender1"  Format="dd MMM yyyy" TargetControlID="txtDate"  Enabled="true" runat="server" /><br />

           Select Class: <asp:CheckBoxList ID="cblClass" runat="server"></asp:CheckBoxList>
             <asp:HiddenField ID="hfCircularID" runat="server" />
            <asp:Button ID="Button1" runat="server" OnClick="Button1_Click" Text="Button" />
            <br />


            <asp:GridView ID="GridView1" AutoGenerateColumns="false"   OnRowCommand="TestGridView_RowCommand" runat="server">
                <Columns>
                    <asp:BoundField  DataField="ID" HeaderText="ID" Visible="false"/>

                    <asp:BoundField  DataField="Title" HeaderText="Title"/>
                    <asp:BoundField  DataField="Discription" HeaderText="Discription"/>
                    <asp:BoundField  DataField="ClassID" HeaderText="Class"/>
                    <asp:BoundField  DataField="Date"   DataFormatString="{0:dd MMM yyyy}" HeaderText="Date"/>
                      <asp:TemplateField HeaderText="Action">
                    <ItemTemplate>
                        <asp:Button ID="btnUpdate" runat="server" Text="Edit" CommandName="btn_update" CausesValidation="false"  CommandArgument='<%# Eval("ID") %>' />
                    </ItemTemplate>

                </asp:TemplateField>
                    </Columns>
            </asp:GridView>
        </div>
    </form>
</body>
</html>



