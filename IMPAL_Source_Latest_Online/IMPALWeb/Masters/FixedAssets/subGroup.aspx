<%@ Page Title="" Language="C#" MasterPageFile="~/Main.Master" AutoEventWireup="true"
    CodeBehind="subGroup.aspx.cs" Inherits="IMPALWeb.Masters.FixedAssets.subGroup" %>

<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="ajaxToolkit" %>
<asp:Content ID="Content1" ContentPlaceHolderID="CPHDetails" runat="server">

    <script type="text/javascript" language="javascript">

        function validateFields(source, arguments) {
            var TxtGLAcctCode = arguments.Value;
            firstchr = TxtGLAcctCode.substring(0, 1, TxtGLAcctCode);
            if (isspecialchar(firstchr)) {
                source.innerHTML = "First character should be Alphabet or Number";
                arguments.IsValid = false;
            }
            else {
                arguments.IsValid = true;
            }

            if (firstchr == " ") {
                source.innerHTML = "First character should not be blank";
                arguments.IsValid = false;
            }

        }

        function enteronlybetween1t0100(sender, args) {
            var maxValue;
            var gridview = document.getElementById("ctl00_CPHDetails_GV_GLMaster");

            var txt = gridview.getElementsByTagName("input");

            for (i = 0; i < txt.length; i++) {
                if (txt[i].id.indexOf("txtFtBookDesc") != -1) {
                    if (txt[i].value > 99) {
                        args.IsValid = false;
                        break;
                    }

                }

                if (txt[i].id.indexOf("txtBookDesc") != -1) {
                    if (txt[i].value > 99) {
                        args.IsValid = false;
                        break;
                    }

                }


                if (txt[i].id.indexOf("txtItDesc") != -1) {
                    if (txt[i].value > 99) {
                        args.IsValid = false;
                        break;
                    }

                }

                if (txt[i].id.indexOf("txtFtItDesc") != -1) {
                    if (txt[i].value > 99) {
                        args.IsValid = false;
                        break;
                    }

                }
            }
        }
    
    
    </script>

    <div id="DivOuter">
        <asp:UpdatePanel ID="UpdatePnlSubGroup" runat="server">
            <ContentTemplate>
                <div class="subFormTitle">
                    Sub Group</div>
                <table class="subFormTable">
                    <tr>
                        <td class="label">
                            <asp:Label ID="lblAssertGroup" runat="server" Text="Group Code" SkinID="LabelNormal"></asp:Label>
                        </td>
                        <td class="inputcontrols">
                            <asp:DropDownList ID="drpAssertGroup" runat="server" AutoPostBack="True" SkinID="DropDownListNormal"
                                OnSelectedIndexChanged="drpAssertGroup_SelectedIndexChanged">
                            </asp:DropDownList>
                            <asp:RequiredFieldValidator ID="RequiredFieldAssertGroup" ValidationGroup="AssertSubGroup"
                                runat="server" ControlToValidate="drpAssertGroup" InitialValue="" SetFocusOnError="true"
                                ErrorMessage="Group is required" SkinID="GridViewLabelError"></asp:RequiredFieldValidator>
                        </td>
                        <td class="label">
                            <asp:Label ID="lblSubGroup" runat="server" Text="Sub Group Code" SkinID="LabelNormal"></asp:Label>
                        </td>
                        <td class="inputcontrols">
                            <asp:DropDownList ID="drpAssertSubGroup" runat="server" SkinID="DropDownListNormal">
                            </asp:DropDownList>
                            <asp:RequiredFieldValidator ID="RequiredFieldAssertSubGroup" ValidationGroup="AssertSubGroup"
                                runat="server" ControlToValidate="drpAssertSubGroup" InitialValue="" SetFocusOnError="true"
                                ErrorMessage="Sub Group is required" SkinID="GridViewLabelError"></asp:RequiredFieldValidator>
                        </td>
                        <td>
                            <asp:ImageButton ID="BtnSearct" ImageUrl="~/Images/ifind.png" ValidationGroup="AssertSubGroup"
                                alt="Query" runat="server" SkinID="ImageButtonSearch" OnClick="BtnSearct_Click" />
                        </td>
                    </tr>
                </table>
                <div class="subFormTitle">
                    Sub Group Details</div>
                <table>
                    <tr>
                        <td>
                            <div class="gridViewScrollFullPage">
                                <asp:GridView ID="GV_GLMaster" runat="server" AllowPaging="true" AutoGenerateColumns="False"
                                    SkinID="GridViewScroll" OnPageIndexChanging="GV_GLMaster_PageIndexChanging" OnRowCancelingEdit="GV_GLMaster_RowCancelingEdit"
                                    OnRowCreated="GV_GLMaster_RowCreated" OnRowEditing="GV_GLMaster_RowEditing" OnRowUpdating="GV_GLMaster_RowUpdating"
                                    OnRowCommand="GV_GLMaster_RowCommand">
                                    <EmptyDataTemplate>
                                        <asp:Label ID="lblEmptySearch" runat="server" SkinID="GridViewLabel">No Results Found</asp:Label>
                                    </EmptyDataTemplate>
                                    <Columns>
                                        <asp:TemplateField HeaderText="Group code" SortExpression="Description">
                                            <EditItemTemplate>
                                                <asp:Label ID="lblEditGroupCode" runat="server" Text='<%# Bind("FA_Group_Code") %>'
                                                    SkinID="GridViewLabel">
                                                </asp:Label>
                                            </EditItemTemplate>
                                            <ItemTemplate>
                                                <asp:Label ID="lblGroupCode" runat="server" Text='<%# Bind("FA_Group_Code") %>' SkinID="GridViewLabel">
                                                </asp:Label>
                                            </ItemTemplate>
                                            <FooterTemplate>
                                                <asp:DropDownList ID="drpFtGroupCode" AutoPostBack="true" runat="server" SkinID="GridViewDropDownListFooter"
                                                    OnSelectedIndexChanged="drpFtGroupCode_SelectedIndexChanged">
                                                </asp:DropDownList>
                                                <asp:RequiredFieldValidator ID="RequiredFieldFtGroupCode" ValidationGroup="InsertSuBGroup"
                                                    runat="server" SkinID="GridViewLabelError" ControlToValidate="drpFtGroupCode"
                                                    InitialValue="" SetFocusOnError="true" ErrorMessage="Group code is required"></asp:RequiredFieldValidator>
                                            </FooterTemplate>
                                        </asp:TemplateField>
                                        <asp:TemplateField HeaderText="code" SortExpression="Description">
                                            <EditItemTemplate>
                                                <asp:Label ID="lblEditcode" runat="server" Text='<%# Bind("FA_Sub_Group_Code") %>'
                                                    SkinID="GridViewLabel">
                                                </asp:Label>
                                            </EditItemTemplate>
                                            <ItemTemplate>
                                                <asp:Label ID="lblcode" runat="server" Text='<%# Bind("FA_Sub_Group_Code") %>' SkinID="GridViewLabel">
                                                </asp:Label>
                                            </ItemTemplate>
                                            <FooterTemplate>
                                                <asp:DropDownList ID="drpFtcode" runat="server" SkinID="GridViewDropDownListFooter">
                                                </asp:DropDownList>
                                                <asp:RequiredFieldValidator ID="RequiredFieldFtcode" ValidationGroup="InsertSuBGroup"
                                                    runat="server" SkinID="GridViewLabelError" ControlToValidate="drpFtcode" InitialValue=""
                                                    SetFocusOnError="true" ErrorMessage="code is required"></asp:RequiredFieldValidator>
                                            </FooterTemplate>
                                        </asp:TemplateField>
                                        <asp:TemplateField HeaderText="Description" SortExpression="Description">
                                            <EditItemTemplate>
                                                <asp:TextBox ID="txtDescription" Text='<%# Bind("FA_Sub_Group_Description") %>' runat="server"
                                                    SkinID="GridViewTextBox">
                                                </asp:TextBox>
                                                <asp:CustomValidator ID="CustValEditDesc" SkinID="GridViewLabelError" runat="server"
                                                    Display="Dynamic" ValidationGroup="InsertSuBGroup" ControlToValidate="txtDescription"
                                                    ClientValidationFunction="validateFields" SetFocusOnError="true" ValidateEmptyText="true"
                                                    ErrorMessage="Please Enter Description"></asp:CustomValidator>
                                                <asp:RequiredFieldValidator ID="RequiredFieldDescription" ValidationGroup="EditSubGroup"
                                                    runat="server" SkinID="GridViewLabelError" ControlToValidate="txtDescription"
                                                    InitialValue="" SetFocusOnError="true" ErrorMessage="code is required"></asp:RequiredFieldValidator>
                                            </EditItemTemplate>
                                            <ItemTemplate>
                                                <asp:Label ID="lblDescription" runat="server" Text='<%# Bind("FA_Sub_Group_Description") %>'
                                                    SkinID="GridViewLabel">
                                                </asp:Label>
                                            </ItemTemplate>
                                            <FooterTemplate>
                                                <asp:TextBox ID="txtFtDescription" runat="server" SkinID="GridViewTextBox">
                                                </asp:TextBox>
                                                <asp:CustomValidator ID="CustValNewDesc" SkinID="GridViewLabelError" runat="server"
                                                    Display="Dynamic" ValidationGroup="InsertSuBGroup" ControlToValidate="txtFtDescription"
                                                    ClientValidationFunction="validateFields" SetFocusOnError="true" ValidateEmptyText="true"
                                                    ErrorMessage="Please Enter Description"></asp:CustomValidator>
                                                <asp:RequiredFieldValidator ID="RequiredFieldFtDescription" ValidationGroup="InsertSuBGroup"
                                                    runat="server" SkinID="GridViewLabelError" ControlToValidate="txtFtDescription"
                                                    InitialValue="" SetFocusOnError="true" ErrorMessage="Description is required"></asp:RequiredFieldValidator>
                                            </FooterTemplate>
                                        </asp:TemplateField>
                                        <asp:TemplateField HeaderText="Book Depreciation %" SortExpression="Description">
                                            <EditItemTemplate>
                                                <asp:TextBox ID="txtBookDesc" onkeypress="enterNumberOnly();" Text='<%# Bind("Depreciation_Percentage") %>'
                                                    runat="server" SkinID="GridViewTextBox">
                                                </asp:TextBox>
                                                <asp:RequiredFieldValidator ID="RequiredFieldBookDesc" ValidationGroup="EditSubGroup"
                                                    runat="server" SkinID="GridViewLabelError" ControlToValidate="txtBookDesc" SetFocusOnError="true"
                                                    ErrorMessage="BookDesc is required"></asp:RequiredFieldValidator>
                                                <asp:CustomValidator ID="CVBookDesc" ErrorMessage="Entered value should be between 0 to 100"
                                                    runat="server" ControlToValidate="txtBookDesc" SetFocusOnError="true" ValidationGroup="EditSubGroup"
                                                    SkinID="GridViewLabelError" EnableClientScript="true" ClientValidationFunction="enteronlybetween1t0100"></asp:CustomValidator>
                                                <asp:RegularExpressionValidator Display="Dynamic" ControlToValidate="txtBookDesc"
                                                    ErrorMessage="Book Depreciation % must be numeric" ID="regMinValEdit" runat="server"
                                                    SetFocusOnError="true" ValidationGroup="EditSubGroup" SkinID="GridViewLabelError"
                                                    ValidationExpression="^[0-9]\d*(\.\d+)?$"></asp:RegularExpressionValidator>
                                            </EditItemTemplate>
                                            <ItemTemplate>
                                                <asp:Label ID="lblBookDesc" runat="server" Text='<%# Bind("Depreciation_Percentage") %>'
                                                    SkinID="GridViewLabel">
                                                </asp:Label>
                                            </ItemTemplate>
                                            <FooterTemplate>
                                                <asp:TextBox ID="txtFtBookDesc" onkeypress="enterNumberOnly();" runat="server" SkinID="GridViewTextBox">
                                                </asp:TextBox>
                                                <asp:RequiredFieldValidator ID="RequiredFieldFtBookDesc" ValidationGroup="InsertSuBGroup"
                                                    runat="server" SkinID="GridViewLabelError" ControlToValidate="txtFtBookDesc"
                                                    InitialValue="" SetFocusOnError="true" ErrorMessage="BookDesc is required"></asp:RequiredFieldValidator>
                                                <asp:CustomValidator ID="CVFtBookDesc" ErrorMessage="Entered value should be between 0 to 100"
                                                    runat="server" ControlToValidate="txtFtBookDesc" SetFocusOnError="true" ValidationGroup="InsertSuBGroup"
                                                    SkinID="GridViewLabelError" EnableClientScript="true" ClientValidationFunction="enteronlybetween1t0100"></asp:CustomValidator>
                                                <asp:RegularExpressionValidator Display="Dynamic" ControlToValidate="txtFtBookDesc"
                                                    ErrorMessage="Book Depreciation % must be numeric" ID="regMinValAdd" runat="server"
                                                    SetFocusOnError="true" ValidationGroup="InsertSuBGroup" SkinID="GridViewLabelError"
                                                    ValidationExpression="^[0-9]\d*(\.\d+)?$"></asp:RegularExpressionValidator>
                                            </FooterTemplate>
                                        </asp:TemplateField>
                                        <asp:TemplateField HeaderText="IT Depreciation %" SortExpression="Description">
                                            <EditItemTemplate>
                                                <asp:TextBox ID="txtItDesc" onkeypress="enterNumberOnly();" runat="server" Text='<%# Bind("IT_Depreciation_Percentage") %>'
                                                    SkinID="GridViewTextBox">
                                                </asp:TextBox>
                                                <asp:RequiredFieldValidator ID="RequiredFieldItDesc" ValidationGroup="EditSubGroup"
                                                    runat="server" SkinID="GridViewLabelError" ControlToValidate="txtItDesc" InitialValue=""
                                                    SetFocusOnError="true" ErrorMessage="IT Depreciation is required"></asp:RequiredFieldValidator>
                                                <asp:CustomValidator ID="CVItDesc" ErrorMessage="Entered value should be between 0 to 100"
                                                    runat="server" ControlToValidate="txtItDesc" SetFocusOnError="true" ValidationGroup="EditSubGroup"
                                                    SkinID="GridViewLabelError" EnableClientScript="true" ClientValidationFunction="enteronlybetween1t0100"></asp:CustomValidator>
                                                <asp:RegularExpressionValidator Display="Dynamic" ControlToValidate="txtItDesc" ErrorMessage="Book Depreciation % must be numeric"
                                                    ID="regITDepEdit" runat="server" SetFocusOnError="true" ValidationGroup="EditSubGroup"
                                                    SkinID="GridViewLabelError" ValidationExpression="^[0-9]\d*(\.\d+)?$"></asp:RegularExpressionValidator>
                                            </EditItemTemplate>
                                            <ItemTemplate>
                                                <asp:Label ID="lblItDesc" runat="server" Text='<%# Bind("IT_Depreciation_Percentage") %>'
                                                    SkinID="GridViewLabel">
                                                </asp:Label>
                                            </ItemTemplate>
                                            <FooterTemplate>
                                                <asp:TextBox ID="txtFtItDesc" runat="server" onkeypress="enterNumberOnly();" SkinID="GridViewTextBox">
                                                </asp:TextBox>
                                                <asp:RequiredFieldValidator ID="RequiredFieldFtItDesc" ValidationGroup="InsertSuBGroup"
                                                    runat="server" SkinID="GridViewLabelError" ControlToValidate="txtFtItDesc" InitialValue=""
                                                    SetFocusOnError="true" ErrorMessage="IT Depreciation is required"></asp:RequiredFieldValidator>
                                                <asp:CustomValidator ID="CVFtItDesc" ErrorMessage="Entered value should be between 0 to 100"
                                                    runat="server" ControlToValidate="txtFtItDesc" SetFocusOnError="true" ValidationGroup="InsertSuBGroup"
                                                    SkinID="GridViewLabelError" EnableClientScript="true" ClientValidationFunction="enteronlybetween1t0100"></asp:CustomValidator>
                                                <asp:RegularExpressionValidator Display="Dynamic" ControlToValidate="txtFtItDesc"
                                                    ErrorMessage="IT Depreciation % must be numeric" ID="regITDepAdd" runat="server"
                                                    SetFocusOnError="true" ValidationGroup="InsertSuBGroup" SkinID="GridViewLabelError"
                                                    ValidationExpression="^[0-9]\d*(\.\d+)?$"></asp:RegularExpressionValidator>
                                            </FooterTemplate>
                                        </asp:TemplateField>
                                        <asp:TemplateField HeaderText="GL main" SortExpression="Description">
                                            <EditItemTemplate>
                                                <asp:DropDownList ID="drpGlMain" AutoPostBack="true" runat="server" SkinID="GridViewDropDownList"
                                                    OnSelectedIndexChanged="drpGlMain_SelectedIndexChanged">
                                                </asp:DropDownList>
                                                <asp:RequiredFieldValidator ID="RequiredFieldGlMain" ValidationGroup="EditSubGroup"
                                                    runat="server" SkinID="GridViewLabelError" ControlToValidate="drpGlMain" InitialValue=""
                                                    SetFocusOnError="true" ErrorMessage="GL main is required"></asp:RequiredFieldValidator>
                                            </EditItemTemplate>
                                            <ItemTemplate>
                                                <asp:Label ID="lblGlMain" runat="server" Text='<%# Bind("GL_Main_Description") %>'
                                                    SkinID="GridViewLabel">
                                                </asp:Label>
                                            </ItemTemplate>
                                            <FooterTemplate>
                                                <asp:DropDownList ID="drpFtGlMain" AutoPostBack="true" runat="server" SkinID="GridViewDropDownListFooter"
                                                    OnSelectedIndexChanged="drpFtGlMain_SelectedIndexChanged">
                                                </asp:DropDownList>
                                                <asp:RequiredFieldValidator ID="RequiredFieldFtGlMain" ValidationGroup="InsertSuBGroup"
                                                    runat="server" SkinID="GridViewLabelError" ControlToValidate="drpFtGlMain" InitialValue=""
                                                    SetFocusOnError="true" ErrorMessage="GL main is required"></asp:RequiredFieldValidator>
                                            </FooterTemplate>
                                        </asp:TemplateField>
                                        <asp:TemplateField HeaderText="GL sub" SortExpression="Description">
                                            <EditItemTemplate>
                                                <asp:DropDownList ID="drpGlSub" AutoPostBack="true" runat="server" SkinID="GridViewDropDownList"
                                                    OnSelectedIndexChanged="drpGlSub_SelectedIndexChanged">
                                                </asp:DropDownList>
                                                <asp:RequiredFieldValidator ID="RequiredFieldGlSub" ValidationGroup="EditSubGroup"
                                                    runat="server" SkinID="GridViewLabelError" ControlToValidate="drpGlSub" InitialValue=""
                                                    SetFocusOnError="true" ErrorMessage="GL sub is required"></asp:RequiredFieldValidator>
                                            </EditItemTemplate>
                                            <ItemTemplate>
                                                <asp:Label ID="lblGlSub" runat="server" Text='<%# Bind("Gl_Sub_Description") %>'
                                                    SkinID="GridViewLabel">
                                                </asp:Label>
                                            </ItemTemplate>
                                            <FooterTemplate>
                                                <asp:DropDownList ID="drpFtGlSub" AutoPostBack="true" runat="server" SkinID="GridViewDropDownListFooter"
                                                    OnSelectedIndexChanged="drpFtGlSub_SelectedIndexChanged">
                                                </asp:DropDownList>
                                                <asp:RequiredFieldValidator ID="RequiredFieldFtGlSub" ValidationGroup="InsertSuBGroup"
                                                    runat="server" SkinID="GridViewLabelError" ControlToValidate="drpFtGlSub" InitialValue=""
                                                    SetFocusOnError="true" ErrorMessage="GL sub is required"></asp:RequiredFieldValidator>
                                            </FooterTemplate>
                                        </asp:TemplateField>
                                        <asp:TemplateField HeaderText="GL account" SortExpression="Description">
                                            <EditItemTemplate>
                                                <asp:DropDownList ID="drpGlAccount" runat="server" SkinID="GridViewDropDownList">
                                                </asp:DropDownList>
                                                <asp:RequiredFieldValidator ID="RequiredFieldGlAccount" ValidationGroup="EditSubGroup"
                                                    runat="server" SkinID="GridViewLabelError" ControlToValidate="drpGlAccount" InitialValue=""
                                                    SetFocusOnError="true" ErrorMessage="GL account is required"></asp:RequiredFieldValidator>
                                            </EditItemTemplate>
                                            <ItemTemplate>
                                                <asp:Label ID="lblGlAccount" runat="server" Text='<%# Bind("Description") %>' SkinID="GridViewLabel">
                                                </asp:Label>
                                            </ItemTemplate>
                                            <FooterTemplate>
                                                <asp:DropDownList ID="drpFtGlAccount" runat="server" SkinID="GridViewDropDownListFooter">
                                                </asp:DropDownList>
                                                <asp:RequiredFieldValidator ID="RequiredFieldFtGlAccount" ValidationGroup="InsertSuBGroup"
                                                    runat="server" SkinID="GridViewLabelError" ControlToValidate="drpFtGlAccount"
                                                    InitialValue="" SetFocusOnError="true" ErrorMessage="GL account is required"></asp:RequiredFieldValidator>
                                            </FooterTemplate>
                                        </asp:TemplateField>
                                        <asp:TemplateField ShowHeader="False">
                                            <ItemTemplate>
                                                <asp:LinkButton ID="btEdit" runat="server" CausesValidation="False" CommandName="Edit"
                                                    SkinID="GridViewLinkButton">
                                                    <asp:Image ID="imgFolder" runat="server" ImageUrl="~/images/iGrid_Edit.png" SkinID="GridViewImageEdit" />
                                                </asp:LinkButton>
                                            </ItemTemplate>
                                            <EditItemTemplate>
                                                <asp:LinkButton ID="btUpdate" runat="server" CausesValidation="True" CommandName="Update"
                                                    ValidationGroup="EditSubGroup" SkinID="GridViewLinkButton">
                                                    <asp:Image ID="imgFolder1" runat="server" ImageUrl="~/images/iGrid_Ok.png" SkinID="GridViewImageEdit" />
                                                </asp:LinkButton>
                                                <asp:LinkButton ID="btCancel" runat="server" CausesValidation="False" CommandName="Cancel"
                                                    SkinID="GridViewLinkButton">
                                                    <asp:Image ID="imgFolder2" runat="server" ImageUrl="~/images/iGrid_Cancel.png" SkinID="GridViewImageEdit" />
                                                </asp:LinkButton>
                                            </EditItemTemplate>
                                            <FooterTemplate>
                                                <asp:Button ID="btAdd" runat="server" Text="Add" CommandName="Insert" ValidationGroup="InsertSuBGroup"
                                                    SkinID="GridViewButton" />
                                            </FooterTemplate>
                                        </asp:TemplateField>
                                    </Columns>
                                </asp:GridView>
                            </div>
                        </td>
                    </tr>
                </table>
                <div class="transactionButtons">
                    <div class="transactionButtonsHolder">
                        <asp:Button SkinID="ButtonViewReport" ID="btnReport" runat="server" Text="Generate Report"
                            OnClick="btnReport_Click" />
                    </div>
                </div>
            </ContentTemplate>
            <Triggers>
                <asp:PostBackTrigger ControlID="btnReport" />
            </Triggers>
        </asp:UpdatePanel>
    </div>

    <script type="text/javascript">
        function pageLoad(sender, args) {
            //gridViewFixedHeader(gridViewID, gridViewWidth, gridViewHeight)
            gridViewFixedHeader('<%=GV_GLMaster.ClientID%>', 1024, 500);
        }
    </script>

</asp:Content>
