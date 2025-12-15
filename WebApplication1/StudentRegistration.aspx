<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="StudentRegistration.aspx.cs" Inherits="WebApplication1.StudentRegistration" %>

<%@ Register Assembly="Microsoft.ReportViewer.WebForms" Namespace="Microsoft.Reporting.WebForms" TagPrefix="rsweb" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title></title>
</head>
<body>
    <form id="form1" runat="server">
        <div>
            <asp:ScriptManager ID="ScriptManager1" runat="server"></asp:ScriptManager>

            Form No.
      <asp:TextBox ID="txtFormNo" runat="server"></asp:TextBox>
            <br />
            <asp:RequiredFieldValidator ID="rfvFormNo" runat="server" ForeColor="Red" ControlToValidate="txtFormNo" ErrorMessage="Form No. is required"></asp:RequiredFieldValidator>

            <br />


            Class:
    <asp:DropDownList ID="ddlClass" runat="server" CssClass="form-control">
    </asp:DropDownList>



            <asp:RequiredFieldValidator ID="rfvCityState" runat="server" ForeColor="Red" ControlToValidate="ddlClass" ErrorMessage="Class is required"></asp:RequiredFieldValidator>

            <br />
            Session:
            <asp:DropDownList ID="ddlSession" runat="server" CssClass="form-control">
            </asp:DropDownList>



            <asp:RequiredFieldValidator ID="rfvSession" runat="server" ForeColor="Red" ControlToValidate="ddlSession" ErrorMessage="Session is required"></asp:RequiredFieldValidator>

            <br />


            Name:
 <asp:TextBox ID="txtName" runat="server"></asp:TextBox>
            <br />
            <asp:RequiredFieldValidator ID="rfvName" runat="server" ForeColor="Red" ControlToValidate="txtName" ErrorMessage="Name is required"></asp:RequiredFieldValidator>
          <br />   <asp:RegularExpressionValidator ID="revName"  ValidationExpression="^[A-Za-z ]+$" runat="server" ForeColor="Red"  ErrorMessage="Enter only Characters" ControlToValidate="txtName"></asp:RegularExpressionValidator>
            <br />

            DOB: 
            <asp:TextBox ID="txtDOB" ReadOnly="true" runat="server"></asp:TextBox>
            <asp:RequiredFieldValidator ID="rfvDOB" runat="server" ForeColor="Red" ControlToValidate="txtDOB" ErrorMessage="DOB is required"></asp:RequiredFieldValidator>
            <ajaxToolkit:CalendarExtender ID="calDOB" runat="server" TargetControlID="txtDOB" />
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

            Blood Group:
    <asp:DropDownList ID="ddlBG" runat="server" CssClass="form-control">
    </asp:DropDownList>



            <asp:RequiredFieldValidator ID="rfvBG" runat="server" ForeColor="Red" ControlToValidate="ddlBG" ErrorMessage="Blood Group is required"></asp:RequiredFieldValidator>

            <br />

            Religion:
            <asp:DropDownList ID="ddlReligion" runat="server" CssClass="form-control">
            </asp:DropDownList>



            <asp:RequiredFieldValidator ID="rfvReligion" runat="server" ForeColor="Red" ControlToValidate="ddlReligion" ErrorMessage=" Please Select  Religion"></asp:RequiredFieldValidator>

            <br />

            Category:
            <asp:DropDownList ID="ddlCategory" runat="server" CssClass="form-control">
            </asp:DropDownList>



            <asp:RequiredFieldValidator ID="rfvCategory" runat="server" ForeColor="Red" ControlToValidate="ddlCategory" ErrorMessage=" Please Select  Category"></asp:RequiredFieldValidator>

            <br />
            State:
            <asp:DropDownList ID="ddlState" runat="server"
                CssClass="form-control"
                AutoPostBack="true"
                OnSelectedIndexChanged="ddlState_SelectedIndexChanged">
            </asp:DropDownList>

            City:
            <asp:DropDownList ID="ddlCityState" runat="server" CssClass="form-control">
            </asp:DropDownList>




            <asp:RequiredFieldValidator ID="rfvCity" runat="server" ForeColor="Red" ControlToValidate="ddlCityState" ErrorMessage=" Please Select  City"></asp:RequiredFieldValidator>

            <br />

            PIN Code:
            <asp:TextBox ID="txtPin" runat="server"></asp:TextBox>
            <br />
            <asp:RequiredFieldValidator ID="rfvPin" runat="server" ForeColor="Red" ControlToValidate="txtPin" ErrorMessage="Pin Code is required"></asp:RequiredFieldValidator>

            <br />

            Address:
            <asp:TextBox ID="txtAddress" runat="server"></asp:TextBox>
            <br />
            <asp:RequiredFieldValidator ID="rfvAddress" runat="server" ForeColor="Red" ControlToValidate="txtAddress" ErrorMessage="Address is required"></asp:RequiredFieldValidator>

            <br />

            Adhaar No:
            <asp:TextBox ID="txtAdhaar" MaxLength="12" runat="server"></asp:TextBox>
            <br />
            <asp:RequiredFieldValidator ID="rfvAdhaar" runat="server" ForeColor="Red"  ControlToValidate="txtAdhaar" ErrorMessage="Adhaar No. is required"></asp:RequiredFieldValidator>
          <br />
            <ajaxToolkit:FilteredTextBoxExtender ID="fteAdhaar" FilterType="Numbers"  TargetControlID="txtAdhaar"   runat="server" />
            <br />

            Father Name:
            <asp:TextBox ID="txtFatherName" runat="server"></asp:TextBox>
            <br />
                        <asp:RegularExpressionValidator ID="revFather"  ValidationExpression="^[A-Za-z ]+$" runat="server" ForeColor="Red"  ErrorMessage="Enter only Characters" ControlToValidate="txtFatherName"></asp:RegularExpressionValidator>
            <br />
            <asp:RequiredFieldValidator ID="rfvFatherName" runat="server" ForeColor="Red" ControlToValidate="txtFatherName" ErrorMessage="Father Name is required"></asp:RequiredFieldValidator>

            <br />

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

            Email
 <asp:TextBox ID="txtEmail" runat="server"></asp:TextBox>
            <br />
            <asp:RequiredFieldValidator ID="rfvEmail" runat="server" ForeColor="Red" ControlToValidate="txtEmail" ErrorMessage="Email is required"></asp:RequiredFieldValidator>
            <br />

            Mother Name:
            <asp:TextBox ID="txtMotherName" runat="server"></asp:TextBox>
            <br />
            <asp:RequiredFieldValidator ID="rfvMotherName" runat="server" ForeColor="Red" ControlToValidate="txtMotherName" ErrorMessage="Mother Name is required"></asp:RequiredFieldValidator>
              <br />
              <asp:RegularExpressionValidator ID="revMother"  ValidationExpression="^[A-Za-z ]+$" runat="server" ForeColor="Red"  ErrorMessage="Enter only Characters" ControlToValidate="txtMotherName"></asp:RegularExpressionValidator>
    
            <br />

            <asp:FileUpload ID="txtImg" runat="server" />
            <asp:Image ID="imgPreview" runat="server" Width="100px" Height="100px" />
            <asp:RequiredFieldValidator ID="rfvImg" runat="server" ForeColor="Red" ControlToValidate="txtImg" ErrorMessage="Please select the image"></asp:RequiredFieldValidator>

            <br />
            <asp:FileUpload ID="txtFatherImg" runat="server" />
            <asp:Image ID="FatherImgPreview" runat="server" Width="100px" Height="100px"/>
            <asp:RequiredFieldValidator ID="rfvFatherImg" runat="server" ForeColor="Red" ControlToValidate="txtFatherImg" ErrorMessage="Please select the image"></asp:RequiredFieldValidator>

            <br />
            <asp:FileUpload ID="txtMotherImg" runat="server" />
            <asp:Image ID="MotherImgPreview" runat="server" Width="100px" Height="100px" />
            <asp:RequiredFieldValidator ID="rfvMotherImg" runat="server" ForeColor="Red" ControlToValidate="txtMotherImg" ErrorMessage="Please select the image"></asp:RequiredFieldValidator>

            <br />

            Subject
   <asp:DropDownList ID="ddlSubject" runat="server">
       <asp:ListItem Value="English">English</asp:ListItem>
       <asp:ListItem Value="Maths">Maths</asp:ListItem>
       <asp:ListItem Value="Science">Science</asp:ListItem>
       <asp:ListItem Value="Social Science">Social Science</asp:ListItem>
       <asp:ListItem Value="Hindi">Hindi</asp:ListItem>

   </asp:DropDownList>
            <br />

            <asp:Button ID="btnSumit" runat="server" OnClick="btn_submitData" Text="Button" />

        </div>
         <rsweb:ReportViewer ID="ReportViewer1" Width="100%" Height="800px" runat="server" ProcessingMode="Local"></rsweb:ReportViewer>
         <rsweb:ReportViewer ID="ReportViewer2" Width="100%" Height="800px" runat="server" ProcessingMode="Local" onSubreportProcessing="BindStudentSubreport">


         </rsweb:ReportViewer>

    </form>
   
</body>
</html>
