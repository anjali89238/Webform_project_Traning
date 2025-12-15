<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="MarksForm.aspx.cs" Inherits="WebApplication1.WebForm2" %>

<%@ Register Assembly="Microsoft.ReportViewer.WebForms" Namespace="Microsoft.Reporting.WebForms" TagPrefix="rsweb" %>
<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="ajaxToolkit" %>
<!DOCTYPE html>
<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title></title>
    <style>
        .modalBackground {
            background-color: black;
            opacity: 0.6;
        }

        .popupPanel {
            background: white;
            width: 200px;
            height: 150px;
            padding: 20px;
            border: 3px solid #444;
            border-radius: 10px;
            text-align: center;
        }

    </style>

</head>
<body>

    <form id="form1" runat="server">

        <asp:ScriptManager ID="ScriptManager1" runat="server"></asp:ScriptManager>

        <asp:Button ID="btnHidden" runat="server" Style="display: none;" />
        
        <asp:Panel ID="pnlPopup" runat="server" CssClass="popupPanel">

            <h2>Welcome!</h2>

            <asp:Button ID="btnClose" runat="server" CausesValidation="false" Text="Close" />

        </asp:Panel>
        <ajaxToolkit:ModalPopupExtender
            ID="mpWelcome"
            runat="server"
            PopupControlID="pnlPopup"
            TargetControlID="btnHidden"
            CancelControlID="btnClose"
            BackgroundCssClass="modalBackground">
        </ajaxToolkit:ModalPopupExtender>

        <div>
            Subject
            <asp:DropDownList ID="ddlSubject" runat="server">
                <asp:ListItem Value="English">English</asp:ListItem>
                <asp:ListItem Value="Maths">Maths</asp:ListItem>
                <asp:ListItem Value="Science">Science</asp:ListItem>
                <asp:ListItem Value="Social Science">Social Science</asp:ListItem>
                <asp:ListItem Value="Hindi">Hindi</asp:ListItem>

            </asp:DropDownList>

            <asp:RequiredFieldValidator ID="rfvSubject" runat="server" ErrorMessage="Select the Subject" ControlToValidate="ddlSubject"></asp:RequiredFieldValidator>
            <br />
            Max Marks
            <asp:TextBox ID="txtMaxMarks" runat="server" OnTextChanged="txtMaxMarks_TextChanged" MaxLength="3"></asp:TextBox>
            <ajaxToolkit:FilteredTextBoxExtender
                ID="ftbeMaxMarks"
                runat="server"
                TargetControlID="txtMaxMarks"
                FilterType="Numbers" />

            <asp:Label ID="lblMaxError" runat="server" ForeColor="Red"></asp:Label>
            <asp:RequiredFieldValidator ID="rfvMaxMarks" runat="server" ErrorMessage="Enter the Maximun Marks" ForeColor="red" ControlToValidate="txtMaxMarks"></asp:RequiredFieldValidator><br />
<%--            <asp:RegularExpressionValidator ID="revMaxMarks" runat="server" ErrorMessage="Enter  Numbers upto 100 only" ControlToValidate="txtMaxMarks" ValidationExpression="^(100|[1-9]?[0-9])$" ForeColor="red"></asp:RegularExpressionValidator>--%>
        </div>


        <%--======================Grid For marks Input====================--%>
        <h1>Marks Table</h1>
        <asp:GridView ID="MarksGridView1" runat="server" AutoGenerateColumns="false" DataKeyNames="ID" Style="margin-right: 250px">

            <Columns>

                <asp:BoundField DataField="Name" HeaderText="Name" />
                <asp:TemplateField HeaderText="Marks">
                    <ItemTemplate>
                        <ajaxToolkit:FilteredTextBoxExtender ID="ftbeMarks" runat="server" TargetControlID="txtMarks" ValidChars=".01234564789" />



                        <asp:TextBox ID="txtMarks" runat="server" CssClass="form-control" AutoPostBack="True"  OnTextChanged="txtMarks_TextChanged"></asp:TextBox>
                        <br />

                        <asp:Label ID="lblError" runat="server"></asp:Label>

                        <br />
                        <asp:RequiredFieldValidator ID="rfvmarks" runat="server" ErrorMessage="Fill the marks" ForeColor="red" ControlToValidate="txtMarks"></asp:RequiredFieldValidator>
                        <br />
<%--                        <asp:RegularExpressionValidator ID="revmarks" runat="server" ErrorMessage="Enter  Numbers upto 3 digits" ControlToValidate="txtMarks" ValidationExpression="^(100|[1-9]?[0-9])$" ForeColor="red"></asp:RegularExpressionValidator>--%>
                    </ItemTemplate>
                </asp:TemplateField>

            </Columns>
        </asp:GridView>

        <asp:Button ID="btnSubmit" runat="server" Text="Submit" OnClick="btnSubmit_Click" />


        <%--============================NESTED GRID===============================--%>
        <h1>Nested Grid</h1>
        <asp:GridView ID="gvParent" runat="server" AutoGenerateColumns="false" OnRowDataBound="gvParent_RowDataBound" DataKeyNames="ID" Style="margin-right: 250px">

            <Columns>

                <asp:BoundField DataField="Name" HeaderText="Name" />
                <asp:BoundField DataField="Email" HeaderText="Email" />
                <asp:BoundField DataField="Mobile" HeaderText="Mobile" />
                <asp:BoundField DataField="Gender" HeaderText="Gender" />
                <asp:BoundField DataField="CityName" HeaderText="City" />
                <asp:BoundField DataField="AdhaarNo" HeaderText="AdhaarNo" />
                <asp:TemplateField HeaderText="Marks">
                    <ItemTemplate>
                        <asp:GridView ID="gvChild" runat="server" AutoGenerateColumns="False"  OnRowCommand="deleteChildGridRow">

                            <Columns>
                                <asp:BoundField DataField="MID" HeaderText="MID" Visible="false" />

                                <asp:BoundField DataField="Subject" HeaderText="Subject" />
                                <asp:BoundField DataField="Marks" HeaderText="Marks" />


                             <%--   <asp:TemplateField HeaderText="Action">
                                    <ItemTemplate>
                                        <asp:Button ID="btnDelete" runat="server" CommandName="deleteRow" CommandArgument='<%# Eval("MID") %>' CausesValidation="false" Text="Delete" />


                                        <asp:HiddenField ID="hfDeleteMID" runat="server" />
                                           <asp:Button ID="btnHidden2" runat="server" Style="display: none" />

                                        <asp:Panel ID="delpnlPopup" runat="server" CssClass="popupPanel">

                                            <h4>Do You Want To Delete Row?</h4>
                                            <asp:Button ID="btnYes" runat="server" CommandName="deleteRow" CommandArgument='<%# Eval("MID") %>' CausesValidation="false" Text="Yes" OnClick="btnYes_Click" />

                                            <asp:Button ID="btnNo" runat="server" CausesValidation="false" Text="No" />

                                        </asp:Panel>

                                        <ajaxToolkit:ModalPopupExtender
                                            ID="mpDeletePopup"
                                            runat="server"
                                            PopupControlID="delpnlPopup"
                                            TargetControlID="btnHidden2"
                                            CancelControlID="btnNo"
                                            BackgroundCssClass="modalBackground">
                                        </ajaxToolkit:ModalPopupExtender>



                                    </ItemTemplate>
                                </asp:TemplateField>--%>




                                <asp:TemplateField HeaderText="Action">
    <ItemTemplate>

        <!-- BUTTON TO OPEN POPUP -->
        <asp:Button ID="btnDelete" runat="server"
            Text="Delete"
            CommandName="deleteRow"
            CommandArgument='<%# Eval("MID") %>'
            CausesValidation="false" />

        <!-- HIDDEN TRIGGER -->
        <asp:Button ID="btnHidden2" runat="server" Style="display:none" />

        <!-- POPUP PANEL -->
        <asp:Panel ID="delpnlPopup" runat="server" CssClass="popupPanel">
            <h4>Do You Want To Delete Row?</h4>

            <!-- CONFIRM DELETE -->
            <asp:Button ID="btnYes" runat="server"
                Text="Yes"
                CommandName="confirmDelete"
                CommandArgument='<%# Eval("MID") %>'
                CausesValidation="false" />

            <asp:Button ID="btnNo" runat="server" Text="No" CausesValidation="false" />
        </asp:Panel>

        <!-- MODAL POPUP EXTENDER -->
        <ajaxToolkit:ModalPopupExtender
            ID="mpDeletePopup"
            runat="server"
            PopupControlID="delpnlPopup"
            TargetControlID="btnHidden2"
            CancelControlID="btnNo"
            BackgroundCssClass="modalBackground">
        </ajaxToolkit:ModalPopupExtender>

    </ItemTemplate>
</asp:TemplateField>


                            </Columns>

                        </asp:GridView>
                    </ItemTemplate>
                </asp:TemplateField>



            </Columns>
        </asp:GridView>


        <h1>Report Marks Table</h1>
        <asp:GridView ID="ReportGridView1" runat="server" AutoGenerateColumns="false" Style="margin-right: 250px">

            <Columns>

                <asp:BoundField DataField="Name" HeaderText="Name" />
                <asp:BoundField DataField="Subject" HeaderText="Subject" />
                <asp:BoundField DataField="Marks" HeaderText="Marks" />
                <asp:BoundField DataField="Percentage" HeaderText="Percentage" />
                <asp:BoundField DataField="RankNo" HeaderText="Rank" />

            </Columns>
        </asp:GridView>

         <rsweb:ReportViewer ID="ReportViewer1"
                        runat="server"
                        Width="100%"
                        Height="100%"
                        ProcessingMode="Local">
        </rsweb:ReportViewer>
         

    </form>
 
</body>
</html>
