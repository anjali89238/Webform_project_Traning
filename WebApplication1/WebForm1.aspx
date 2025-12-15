<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="WebForm1.aspx.cs" Inherits="WebApplication1.WebForm1" %> 
<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="ajaxToolkit" %>
<!DOCTYPE html>
<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
     <title></title>
</head> 

<body>
    <form id="form1" runat="server">
        <div>
            Name
            <asp:TextBox ID="txtName" runat="server"></asp:TextBox>
            <br />
            <asp:RequiredFieldValidator ID="rfvName" runat="server" ForeColor="Red" ControlToValidate="txtName" ErrorMessage="Name is required"></asp:RequiredFieldValidator>

            <br />

            Email
            <asp:TextBox ID="txtEmail" runat="server"></asp:TextBox>
            <br />
            <asp:RequiredFieldValidator ID="rfvEmail" runat="server" ForeColor="Red" ControlToValidate="txtEmail" ErrorMessage="Email is required"></asp:RequiredFieldValidator>
            <asp:RegularExpressionValidator
                ID="revEmail"
                runat="server"
                ControlToValidate="txtEmail"
                ErrorMessage="Invalid Email!"
                ForeColor="Red"
                ValidationExpression="^[^@\s]+@[^@\s]+\.[^@\s]+$">
            </asp:RegularExpressionValidator>
            <br />

            <asp:ScriptManager ID="ScriptManager1" runat="server"></asp:ScriptManager>

            Mobile 
            <asp:TextBox
                ID="txtMobile"
                runat="server"
                MaxLength="10"
                CssClass="form-control"
                placeholder="Enter Mobile Number">
            
            </asp:TextBox>


            <ajaxToolkit:FilteredTextBoxExtender
                ID="ftbeMobile"
                runat="server"
                TargetControlID="txtMobile"
                FilterType="Numbers" />

            <asp:RequiredFieldValidator ID="rfvMobile" runat="server" ForeColor="Red" ControlToValidate="txtMobile" ErrorMessage="Mobile Number is required"></asp:RequiredFieldValidator>

            <asp:RegularExpressionValidator
                ID="revMobile"
                runat="server"
                ControlToValidate="txtMobile"
                ErrorMessage="Invalid Mobile Number!"
                ForeColor="Red"
                ValidationExpression="^[0-9]{10}$">
            </asp:RegularExpressionValidator> 



            <br />

            City:
            <asp:DropDownList ID="ddlCity" runat="server" CssClass="form-control">
            </asp:DropDownList>



            <asp:RequiredFieldValidator ID="rfvCity" runat="server" ForeColor="Red" ControlToValidate="ddlCity" ErrorMessage="City is required"></asp:RequiredFieldValidator>

            <br />
            Gender
            <asp:RadioButtonList ID="txtGender" runat="server">
                <asp:ListItem Value="Male">Male</asp:ListItem>
                <asp:ListItem Value="Female">Female</asp:ListItem>
            </asp:RadioButtonList>
            <br />

            <asp:RequiredFieldValidator
                ID="rfvGender"
                runat="server"
                ControlToValidate="txtGender"
                InitialValue=""
                ErrorMessage="Please select gender"
                ForeColor="Red">
            </asp:RequiredFieldValidator>
            <br />


            Image
            <asp:FileUpload ID="txtimg" runat="server" />
            <br />
            <asp:Image ID="imgPreview" runat="server" Width="100px" Height="100px" />
            <br />
            <asp:RequiredFieldValidator ID="rfvImage" runat="server" ForeColor="Red" ControlToValidate="txtimg" ErrorMessage="please select a Image"></asp:RequiredFieldValidator>
            <br />

            AdhaarNo
            <asp:TextBox ID="txtAdhaar" runat="server"></asp:TextBox>
            <br />
            <ajaxToolkit:FilteredTextBoxExtender
                ID="ttbeAdhaar"
                runat="server"
                TargetControlID="txtAdhaar"
                FilterType="Numbers" />

            <asp:RequiredFieldValidator ID="rfvAdhaar" runat="server" ForeColor="Red" ControlToValidate="txtAdhaar" ErrorMessage="AdhaarNo is required!"></asp:RequiredFieldValidator>
            <br /> 
            <asp:RegularExpressionValidator
                ControlToValidate="txtAdhaar"
                ErrorMessage="Enter 12-digit Adhaar!"
                ForeColor="Red"
                runat="server"
                ValidationExpression="^[0-9]{12}$">
            </asp:RegularExpressionValidator>
            <br />

            <asp:Button ID="Button1" runat="server" OnClick="Button1_Click" Text="Button" />
            <asp:Button ID="btn_UpdateRow" runat="server" OnClick="btnUpdate_Click" Text="Update" />
            <asp:HiddenField ID="hfID" runat="server" />
            <asp:Label ID="lblMessage" runat="server"></asp:Label>


        </div>

        <h1>Registeration Details</h1>
        <asp:GridView ID="GridView1" runat="server" AutoGenerateColumns="false"  OnRowCommand="GridView1_RowCommand">
            <Columns>
                <asp:BoundField DataField="ID" HeaderText="ID" Visible="false" />
                <asp:BoundField DataField="Name" HeaderText="Name" />
                <asp:BoundField DataField="Email" HeaderText="Email" />
                <asp:BoundField DataField="Mobile" HeaderText="Mobile" />
                <asp:BoundField DataField="Gender" HeaderText="Gender" />
                <asp:BoundField DataField="CityName" HeaderText="City" />
                <asp:BoundField DataField="AdhaarNo" HeaderText="AdhaarNo" />
                <asp:TemplateField HeaderText="Image">
                    <ItemTemplate>
                        <asp:Image ID="imgUser" runat="server"
                            ImageUrl='<%# Eval("Image") %>'
                            Width="80" Height="80" />
                    </ItemTemplate>
                </asp:TemplateField>
                <asp:TemplateField HeaderText="Action">
                    <ItemTemplate>
                        <asp:Button ID="btnUpdate" runat="server"    CommandName="getRowforUpdation"  CommandArgument='<%# Eval("ID") %>'   CausesValidation="false"  Text="Update" />
                        <asp:Button ID="btnDelete"  runat="server"  CommandName="deleteRow" CommandArgument='<%# Eval("ID") %>'  CausesValidation="false" Text="Delete" />
                        
                    </ItemTemplate>
                </asp:TemplateField>


            </Columns>

   </asp:GridView>



    </form>



</body>
</html>

