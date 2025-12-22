<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="Webform7.aspx.cs" Inherits="WebApplication1.Webform7" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title></title>
</head>
<body>
    <form id="form1" runat="server">
        <div>

            <asp:ScriptManager ID="ScriptManager1" runat="server"></asp:ScriptManager>

            School ID
            <asp:TextBox ID="txtSchoolId" ReadOnly="true" runat="server"></asp:TextBox> <br />
            School Name
            <asp:TextBox ID="txtSchoolName" runat="server"></asp:TextBox>
            <asp:RequiredFieldValidator ID="rfvSName" runat="server" ControlToValidate="txtSchoolName" ForeColor="Red" ErrorMessage="Please enter School Name "></asp:RequiredFieldValidator><br />
            <asp:RegularExpressionValidator ID="revName" ValidationExpression="^[A-Za-z ]+$" runat="server" ForeColor="Red" ErrorMessage="Enter only Characters" ControlToValidate="txtSchoolName"></asp:RegularExpressionValidator>
            <br />

            Address 
            <asp:TextBox ID="txtAddess" TextMode="MultiLine"  Width="200px" Height="100px" runat="server"></asp:TextBox>
            <asp:RequiredFieldValidator ID="rfvSAddress" runat="server" ControlToValidate="txtAddess" ErrorMessage="Please Enter School Address "></asp:RequiredFieldValidator><br />

            PhoneNo 
            <asp:TextBox
                ID="txtPhone"
                runat="server"
                MaxLength="20"
                CssClass="form-control"
                placeholder="Enter Mobile Number">

            </asp:TextBox>

           <ajaxToolkit:FilteredTextBoxExtender
    ID="FilteredTextBoxExtenderPhone"
    runat="server"
    TargetControlID="txtPhone"
    FilterType="Custom, Numbers"
    FilterMode="ValidChars"
    ValidChars=" -+" /> 

            <asp:RequiredFieldValidator ID="rfvPhone" runat="server" ForeColor="Red" ControlToValidate="txtPhone" ErrorMessage="Phone Number is required"></asp:RequiredFieldValidator>
     
          <%--  <asp:RegularExpressionValidator
                ID="revPhone"
                runat="server"
                ControlToValidate="txtPhone"
                ErrorMessage="Invalid Mobile Number!"
                ForeColor="Red"
                ValidationExpression="^\\+?[-.()\\s]*[0-9]{3}[-.()\\s]*[0-9]{3}[-.()\\s]*[0-9]{4}[-.()\\s]*$">
            </asp:RegularExpressionValidator>--%>
            <br />
            MobileNo 
            <asp:TextBox
                ID="txtMobile"
                runat="server"
                MaxLength="20"
                CssClass="form-control"
                placeholder="Enter Mobile Number">

            </asp:TextBox>

                <ajaxToolkit:FilteredTextBoxExtender
ID="FilteredTextBoxMobile"
runat="server"
TargetControlID="txtMobile"
FilterType="Custom, Numbers"
FilterMode="ValidChars"
ValidChars=" -+" /> 

            <asp:RequiredFieldValidator ID="rfvMobile" runat="server" ForeColor="Red" ControlToValidate="txtMobile" ErrorMessage="Mobile Number is required"></asp:RequiredFieldValidator>

            <%--<asp:RegularExpressionValidator
                ID="revMobile"
                runat="server"
                ControlToValidate="txtMobile"
                ErrorMessage="Invalid Mobile Number!"
                ForeColor="Red"
                ValidationExpression="^\\+?[-.()\\s]*[0-9]{3}[-.()\\s]*[0-9]{3}[-.()\\s]*[0-9]{4}[-.()\\s]*$">
            </asp:RegularExpressionValidator>--%>
            <br />

            Email
            <asp:TextBox ID="txtEmail" runat="server"></asp:TextBox>
            <br />
            <asp:RequiredFieldValidator ID="rfvEmail" runat="server" ForeColor="Red" ControlToValidate="txtEmail" ErrorMessage="Email is required"></asp:RequiredFieldValidator>
            <br />


            Logo
            <asp:FileUpload ID="txtLogo" runat="server" />
            <asp:Image ID="imgLogoPreview" runat="server" Width="100px" Height="100px" />
            <asp:RequiredFieldValidator ID="rfvLogo" runat="server" ForeColor="Red" ControlToValidate="txtLogo" ErrorMessage="Please select the image"></asp:RequiredFieldValidator>

            <br />
            Web Address 
            <asp:TextBox ID="txtWebAddress" runat="server"></asp:TextBox>
            <asp:RequiredFieldValidator ID="rfvSWebAddress" runat="server" ControlToValidate="txtWebAddress" ForeColor="red" ErrorMessage="please Enter Web Address "></asp:RequiredFieldValidator><br />

            Principal Sign     
            <asp:FileUpload ID="txtPrincipalSign" runat="server" />
            <asp:Image ID="imgPrincipalSignPreview" runat="server" Width="100px" Height="100px" />
            <asp:RequiredFieldValidator ID="rfvsign" runat="server" ForeColor="Red" ControlToValidate="txtPrincipalSign" ErrorMessage="Please select the image"></asp:RequiredFieldValidator>

            <br />

            Bank Name 
            <asp:TextBox ID="txtBankName" runat="server"></asp:TextBox>
            <asp:RequiredFieldValidator ID="rfvBankName" runat="server" ControlToValidate="txtBankName" ForeColor="red" ErrorMessage="please Enter BankName "></asp:RequiredFieldValidator><br />
            Bank AccountNo
            <asp:TextBox ID="txtBankAccountNo" MaxLength="20"  runat="server"></asp:TextBox>
            <ajaxToolkit:FilteredTextBoxExtender
                ID="ftbeAccNo"
                runat="server"
                TargetControlID="txtBankAccountNo"
                FilterType="Numbers" />
            <asp:RequiredFieldValidator ID="rfvBAccNo" runat="server" ControlToValidate="txtBankAccountNo" ForeColor="red" ErrorMessage="please Enter Bank AccountNo "></asp:RequiredFieldValidator><br />
            Branch Name 
            <asp:TextBox ID="txtBranchName" runat="server"></asp:TextBox>
            <asp:RequiredFieldValidator ID="rfvBName" runat="server" ControlToValidate="txtBranchName" ForeColor="red" ErrorMessage="please Enter Branch Name "></asp:RequiredFieldValidator>
            <br />

            Andriod App Link
       <asp:TextBox ID="txtAndriodAppLink" runat="server"></asp:TextBox>
            <asp:RequiredFieldValidator ID="rfvAndriodAppLin" runat="server" ControlToValidate="txtAndriodAppLink" ForeColor="red" ErrorMessage="This Feild is Required"></asp:RequiredFieldValidator>
            <br />
            Pin Code
            <asp:TextBox ID="txtPinCode" runat="server"></asp:TextBox>
            <asp:RequiredFieldValidator ID="rfvPinCode" runat="server" ControlToValidate="txtPinCode" ForeColor="red" ErrorMessage="Please Enter Pin Code "></asp:RequiredFieldValidator>
            <br />

            State:
      <asp:DropDownList ID="ddlState" runat="server"
          CssClass="form-control"
          AutoPostBack="true"
          OnSelectedIndexChanged="ddlState_SelectedIndexChanged">
      </asp:DropDownList>
            <br />
            City:
      <asp:DropDownList ID="ddlCityState" runat="server" CssClass="form-control">
      </asp:DropDownList>




            <asp:RequiredFieldValidator ID="rfvCity" runat="server" ForeColor="Red" ControlToValidate="ddlCityState" ErrorMessage=" Please Select  City"></asp:RequiredFieldValidator>

            <br />
            App Status 
            <asp:DropDownList ID="ddlAppStatus" runat="server" >
                <asp:ListItem Value="Staging">Staging</asp:ListItem>
                <asp:ListItem Value="Production">Production</asp:ListItem>

            </asp:DropDownList>
            <br />
            <br />

            <asp:Button ID="btnsubmit" OnClick="btn_submit" runat="server" Text="Submit" />
            <asp:Label ID="lblMessage" runat="server" Text=""></asp:Label>
        </div>
    </form>
</body>
</html>
