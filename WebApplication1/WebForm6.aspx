<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="WebForm6.aspx.cs" Inherits="WebApplication1.WebForm6" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title></title>
</head>
<body>

    <form id="form1" runat="server">
        <div>
            <asp:GridView ID="GridView1"   AutoGenerateColumns="false" DataKeyNames="LectureID"  OnRowDataBound="GridView1_RowDataBound" runat="server">
                <Columns>
                    <asp:BoundField  DataField="LectureID"   HeaderText="LID"/>
                        
                  <asp:TemplateField HeaderText="Faculty">
                    <ItemTemplate>
                      
                        <asp:DropDownList ID="ddlFaculty"  runat="server"></asp:DropDownList>
                        <br />

                      
                    </ItemTemplate>
                </asp:TemplateField>
                  <asp:TemplateField HeaderText="Subject">
                    <ItemTemplate>
                      
                        <asp:DropDownList ID="ddlSubject" runat="server"></asp:DropDownList>
                        <br />

              
                    </ItemTemplate>
                </asp:TemplateField>

                </Columns>
            </asp:GridView>
            <asp:Button ID="btnSubmit" runat="server"  OnClick="submitData" Text="Submit" />
               <asp:HiddenField ID="hfID" runat="server" />
            <br />
            <br />
            <br />
          <%--  <asp:GridView ID="GridView2" runat="server" AutoGenerateColumns="false" >

                <Columns>
                    <asp:BoundField  DataField="LectureID" HeaderText="LID" />
                    <asp:BoundField  DataField="FacultyName" HeaderText="Faculty" />
                    <asp:BoundField  DataField="Subject" HeaderText="Subject" />
                     <asp:TemplateField HeaderText="Action">
     <ItemTemplate>
         <asp:Button ID="btnUpdate" runat="server" Text="Edit" CommandName="btn_update" CausesValidation="false"  CommandArgument='<%# Eval("LectureID") %>' />
     </ItemTemplate>

 </asp:TemplateField>

                </Columns>

            </asp:GridView>--%>


        </div>
    </form>

</body>
</html>
