<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="WebForm4.aspx.cs" Inherits="WebApplication1.WebForm4" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title></title>
</head>
<body>
    <form id="form1" runat="server">
        <div>
            <asp:ScriptManager ID="ScriptManager1" runat="server" />
            <asp:UpdatePanel ID="UpdatePanel1" runat="server">
                <ContentTemplate>

                    Enter Role
                    <asp:TextBox ID="txtRole" runat="server"  ></asp:TextBox> 
                    <asp:Button ID="btnAdd"   OnClick="btnAddRole" runat="server" Text="Add Role" CausesValidation="false" /><br />
                    Roles
            <asp:DropDownList ID="ddlRoles" runat="server" OnSelectedIndexChanged="ddlItems_SelectedIndexChanged" CssClass="form-control" AutoPostBack="True"></asp:DropDownList>
                    <br />
                    <asp:RequiredFieldValidator ID="rfvRole" runat="server" ForeColor="Red" ControlToValidate="ddlRoles" ErrorMessage="Please select the role"></asp:RequiredFieldValidator>

                    <asp:CheckBoxList ID="cblPageMaster" runat="server"></asp:CheckBoxList>
                </ContentTemplate>
                 <Triggers>
    <asp:AsyncPostBackTrigger ControlID="ddlRoles" EventName="SelectedIndexChanged" />
</Triggers>

            </asp:UpdatePanel>
                
<%--            <asp:Button ID="submit" runat="server" Text="Submit" OnClick="btn_Submit" />--%>
            <asp:Label ID="lblSubmit" runat="server"></asp:Label>

            <asp:Button ID="update" runat="server" Text="Submit" OnClick="btn_Update" />
            <asp:HiddenField ID="hfID" runat="server" />
                    

<br />
        </div>
        <br />
        <br />
        Data Table
        <asp:GridView ID="TestGridView" AutoGenerateColumns="false" runat="server" OnRowCommand="TestGridView_RowCommand">
            <Columns>
                <asp:BoundField DataField="RoleID" HeaderText="ID" Visible="false" />
                <asp:BoundField DataField="RoleName" HeaderText="Role" />

                <asp:BoundField DataField="PageName" HeaderText="PageMaster" />
                <asp:TemplateField HeaderText="Action">
                    <ItemTemplate>
                        <asp:Button ID="btnUpdate" runat="server" Text="Edit" CommandName="btn_update" CausesValidation="false"  CommandArgument='<%# Eval("RoleID") %>' />
                        <asp:Button ID="btnDelete" runat="server" Text="Delete" CommandName="btn_delete" CausesValidation="false"  CommandArgument='<%# Eval("RoleID") %>' />
                    </ItemTemplate>

                </asp:TemplateField>
            </Columns>

        </asp:GridView>
    </form>
</body>
</html>
