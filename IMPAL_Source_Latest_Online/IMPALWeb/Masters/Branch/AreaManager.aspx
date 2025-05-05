<%@ Page Title="" Language="C#" MasterPageFile="~/Main.Master" AutoEventWireup="true"
    CodeBehind="AreaManager.aspx.cs" Inherits="IMPALWeb.AreaManager" %>

<%--<asp:Content ID="Content2" ContentPlaceHolderID="CPH_Header" runat="server">
</asp:Content>--%>
<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="ajaxToolkit" %>
<asp:Content ID="CPHDetails" ContentPlaceHolderID="CPHDetails" runat="server">

    <script type="text/javascript">


        //This function is to validate Fields when it is in edit mode
        function ValidateEditFields(lnk) {
            var txtAMCode = null;
            var txtAMName = null;
            var ddlOpBranch = null;
            var txtPreviousManager = null;
            var txtStDate = null;
            var txtEDate = null;
            var txtPMStDate = null;
            var txtPMEDate = null;
            var row = lnk.parentNode.parentNode;
            var rowIndex = row.rowIndex - 1;

            txtAMCode = row.cells[0].children[0];
            txtAMName = row.cells[1].children[0];
            ddlOpBranch = row.cells[2].children[0];
            txtStDate = row.cells[3].children[0];
            txtEDate = row.cells[4].children[0];
            txtPreviousManager = row.cells[5].children[0];
            txtPMStDate = row.cells[6].children[0];
            txtPMEDate = row.cells[7].children[0];

            var oStDateVal = txtStDate.value.trim();
            var oToDateVal = txtEDate.value.trim();
            var oPrevStDateVal = txtPMStDate.value.trim();
            var oPrevToDateVal = txtPMEDate.value.trim();
            var oSysDate = new Date();
            var oFromDate = oStDateVal.split("/");
            var oToDate = oToDateVal.split("/");
            var oPrevStDate = oPrevStDateVal.split("/");
            var oPrevToDate = oPrevToDateVal.split("/");
            var oFromDateFormatted = oFromDate[1] + "/" + oFromDate[0] + "/" + oFromDate[2];
            var oToDateFormatted = oToDate[1] + "/" + oToDate[0] + "/" + oToDate[2];
            var oPrevStDateFormatted = oPrevStDate[1] + "/" + oPrevStDate[0] + "/" + oPrevStDate[2];
            var oPrevToDateFormatted = oPrevToDate[1] + "/" + oPrevToDate[0] + "/" + oPrevToDate[2];


            if (validatespl(txtAMCode.value, "Code")) {
                txtAMCode.focus();
                return false;
            }
            if (validatespl(txtAMName.value, "Name")) {
                txtAMName.focus();
                return false;
            }
            if (validatespl(ddlOpBranch.value, "Operating From")) {
                ddlOpBranch.focus();
                return false;
            }
            if (doValidateDate(txtStDate)) {
                txtStDate.focus();
                return false;
            }
            if (oSysDate < new Date(oFromDateFormatted)) {
                alert("Start Date should not be greater than System Date");
                txtStDate.value = "";
                txtStDate.focus();
                return false;
            }

            if (doValidateDate(txtEDate)) {
                txtEDate.focus();
                return false;
            }
            else if (oSysDate < new Date(oToDateFormatted)) {
                alert("End Date should not be greater than System Date");
                txtEDate.value = "";
                txtEDate.focus();
                return false;
            }
            else if (new Date(oFromDateFormatted) > new Date(oToDateFormatted)) {
                alert("End Date should be greater than Start Date");
                txtEDate.value = "";
                txtEDate.focus();
                return false;
            }
            if (validatespl(txtPreviousManager.value, "Name")) {
                txtPreviousManager.focus();
                return false;
            }
            if (doValidateDate(txtPMStDate)) {
                txtPMStDate.focus();
                return false;
            }
            if (oSysDate < new Date(oPrevStDateFormatted)) {
                alert("Previous Manager Start Date should not be greater than System Date");
                txtPMStDate.value = "";
                txtPMStDate.focus();
                return false;
            }
            if (doValidateDate(txtPMEDate)) {
                txtPMEDate.focus();
                return false;
            }
            else if (oSysDate < new Date(oPrevToDateFormatted)) {
                alert("Previous Manager End Date should not be greater than System Date");
                txtPMEDate.value = "";
                txtPMEDate.focus();
                return false;
            }
            else if (new Date(oPrevStDateFormatted) > new Date(oPrevToDateFormatted)) {
                alert("Previous Manager End Date should be greater than Previous Manager Start Date");
                txtPMEDate.value = "";
                txtPMEDate.focus();
                return false;
            }
            else if (new Date(oPrevToDateFormatted) > new Date(oFromDateFormatted)) {
                alert("Previous Manager End Date is greater than Start Date");
                txtStDate.value = "";
                txtStDate.focus();
                return false;
            }

        }

    </script>

    <div id="DivOuter">
        <div class="subFormTitle">
            Area Manager
        </div>
        <asp:UpdatePanel ID="UpdatePanel1" runat="server">
            <ContentTemplate>
                <table>
                    <tr>
                        <td>
                            <asp:GridView ID="GV_AM" runat="server" AutoGenerateColumns="False" DataSourceID="ObjectDataAM"
                                AllowPaging="True" HorizontalAlign="Left" BackColor="White" BorderStyle="None"
                                BorderWidth="1px" CellPadding="3" CaptionAlign="Left" OnDataBinding="GV_AM_DataBinding"
                                ShowFooter="True" OnSelectedIndexChanged="GV_AM_SelectedIndexChanged" OnRowCommand="GV_AM_RowCommand"
                                OnRowUpdating="GV_AM_RowUpdating" SkinID="GridView" OnRowCreated="GV_AM_RowCreated">
                                <Columns>
                                    <asp:TemplateField HeaderText="Code">
                                        <ItemTemplate>
                                            <asp:Label ID="lblAMCode" runat="server" Text='<%# Bind("AreaManagerCode") %>'></asp:Label>
                                        </ItemTemplate>
                                        <EditItemTemplate>
                                            <asp:TextBox ID="txtAMCode" runat="server" Text='<%# Bind("AreaManagerCode") %>'
                                                Wrap="False" Enabled="false" Width="87px"></asp:TextBox>
                                            <br />
                                            <asp:RequiredFieldValidator ID="rvAMCode" runat="server" ControlToValidate="txtAMCode"
                                                ErrorMessage="Please enter a valid Code" ValidationGroup="AMEditGroup">
                                            </asp:RequiredFieldValidator>
                                        </EditItemTemplate>
                                        <FooterTemplate>
                                            <asp:TextBox ID="txtNewAMCode" runat="server" Wrap="False" Width="79px" MaxLength="6"></asp:TextBox>
                                            <br />
                                            <asp:RequiredFieldValidator ID="rvNewAMCode" runat="server" ControlToValidate="txtNewAMCode"
                                                ErrorMessage="Please enter a valid Code" ValidationGroup="AMAddGroup">
                                            </asp:RequiredFieldValidator>
                                        </FooterTemplate>
                                        <FooterStyle Wrap="False" />
                                    </asp:TemplateField>
                                    <asp:TemplateField HeaderText="Name">
                                        <ItemTemplate>
                                            <asp:Label ID="txtAMName" runat="server" Text='<%# Bind("AreaManagerName") %>'></asp:Label>
                                        </ItemTemplate>
                                        <EditItemTemplate>
                                            <asp:TextBox ID="txtAMName" runat="server" Text='<%# Bind("AreaManagerName") %>'
                                                Wrap="False" Width="100px"></asp:TextBox>
                                            <br />
                                            <asp:RequiredFieldValidator ID="rvAMName" runat="server" ControlToValidate="txtAMName"
                                                ErrorMessage="Please enter a valid Name" ValidationGroup="AMEditGroup">
                                            </asp:RequiredFieldValidator>
                                        </EditItemTemplate>
                                        <FooterTemplate>
                                            <asp:TextBox ID="txtNewAMName" runat="server" Wrap="False"></asp:TextBox>
                                            <br />
                                            <asp:RequiredFieldValidator ID="rvNewAMName" runat="server" ControlToValidate="txtNewAMName"
                                                ErrorMessage="Please enter a valid Name" ValidationGroup="AMAddGroup">
                                            </asp:RequiredFieldValidator>
                                        </FooterTemplate>
                                        <FooterStyle Wrap="False" />
                                    </asp:TemplateField>
                                    <asp:TemplateField HeaderText="Operating From " SortExpression="BranchName">
                                        <EditItemTemplate>
                                            <asp:DropDownList ID="ddlOpBranch" runat="server" DataSourceID="ODSOPBranch" DataTextField="BranchName"
                                                DataValueField="BranchCode" Width="185px">
                                            </asp:DropDownList>
                                            <br />
                                            <asp:RequiredFieldValidator ID="rvNewddlOpBranch" runat="server" ControlToValidate="ddlOpBranch"
                                                ErrorMessage="Please enter a valid Branch" InitialValue="0" ValidationGroup="AMEditGroup">
                                            </asp:RequiredFieldValidator>
                                        </EditItemTemplate>
                                        <FooterTemplate>
                                            <asp:DropDownList ID="ddlOpBranch" runat="server" DataSourceID="ODSOPBranch" DataTextField="BranchName"
                                                DataValueField="BranchCode" Width="187px">
                                            </asp:DropDownList>
                                        </FooterTemplate>
                                        <ItemTemplate>
                                            <asp:Label ID="lblOpBranch" runat="server" Text='<%# Bind("OperatingBranch") %>'>
                                            </asp:Label>
                                        </ItemTemplate>
                                    </asp:TemplateField>
                                    <asp:TemplateField HeaderText="Start Date">
                                        <ItemTemplate>
                                            <asp:Label ID="lblStDate" runat="server" Text='<%# Bind("StartDate") %>'></asp:Label>
                                        </ItemTemplate>
                                        <EditItemTemplate>
                                            <asp:TextBox ID="txtStDate" SkinID="TextBoxCalendarExtenderNormal" runat="server"
                                                Text='<%# Bind("StartDate") %>' Wrap="False" Width="100px"></asp:TextBox>
                                            <ajaxToolkit:CalendarExtender ID="calExttxtStDate" runat="server" EnableViewState="true"
                                                Format="dd/MM/yyyy" TargetControlID="txtStDate">
                                            </ajaxToolkit:CalendarExtender>
                                            
                                        </EditItemTemplate>
                                        <FooterTemplate>
                                            <asp:TextBox ID="txtNewStDate" SkinID="TextBoxCalendarExtenderNormal" runat="server"
                                                Wrap="False"></asp:TextBox>
                                            <ajaxToolkit:CalendarExtender ID="calExttxtNewStDate" runat="server" EnableViewState="true"
                                                Format="dd/MM/yyyy" TargetControlID="txtNewStDate">
                                            </ajaxToolkit:CalendarExtender>
                                        </FooterTemplate>
                                        <FooterStyle Wrap="False" />
                                    </asp:TemplateField>
                                    <asp:TemplateField HeaderText="End Date">
                                        <ItemTemplate>
                                            <asp:Label ID="lblEDate" runat="server" Text='<%# Bind("EndDate") %>'></asp:Label>
                                        </ItemTemplate>
                                        <EditItemTemplate>
                                            <asp:TextBox ID="txtEDate" SkinID="TextBoxCalendarExtenderNormal" runat="server"
                                                Text='<%# Bind("EndDate") %>' Wrap="False" Width="100px"></asp:TextBox>
                                            <ajaxToolkit:CalendarExtender ID="calExttxtEDate" runat="server" EnableViewState="true"
                                                Format="dd/MM/yyyy" TargetControlID="txtEDate">
                                            </ajaxToolkit:CalendarExtender>
                                        </EditItemTemplate>
                                        <FooterTemplate>
                                            <asp:TextBox ID="txtNewEDate" SkinID="TextBoxCalendarExtenderNormal" runat="server"
                                                Wrap="False" Width="125px"></asp:TextBox>
                                            <ajaxToolkit:CalendarExtender ID="calExttxtNewEDate" runat="server" EnableViewState="true"
                                                Format="dd/MM/yyyy" TargetControlID="txtNewEDate">
                                            </ajaxToolkit:CalendarExtender>
                                        </FooterTemplate>
                                        <FooterStyle Wrap="False" />
                                    </asp:TemplateField>
                                    <asp:TemplateField HeaderText="Previous Manager">
                                        <ItemTemplate>
                                            <asp:Label ID="lblPreviousManager" runat="server" Text='<%# Bind("PreviousManager") %>'></asp:Label>
                                        </ItemTemplate>
                                        <EditItemTemplate>
                                            <asp:TextBox ID="txtPreviousManager" runat="server" Text='<%# Bind("PreviousManager") %>'
                                                Wrap="False" Width="100px"></asp:TextBox>
                                        </EditItemTemplate>
                                        <FooterTemplate>
                                            <asp:TextBox ID="txtNewPreviousManager" runat="server" Wrap="False"></asp:TextBox>
                                        </FooterTemplate>
                                        <FooterStyle Wrap="False" />
                                    </asp:TemplateField>
                                    <asp:TemplateField HeaderText="Start Date ">
                                        <ItemTemplate>
                                            <asp:Label ID="lblPMStDate" runat="server" Text='<%# Bind("PreviousManagerStartDate") %>'></asp:Label>
                                        </ItemTemplate>
                                        <EditItemTemplate>
                                            <asp:TextBox ID="txtPMStDate" SkinID="TextBoxCalendarExtenderNormal" runat="server"
                                                Text='<%# Bind("PreviousManagerStartDate") %>' Wrap="False" Width="100px"></asp:TextBox>
                                            <ajaxToolkit:CalendarExtender ID="calExttxtPMStDate" runat="server" EnableViewState="true"
                                                Format="dd/MM/yyyy" TargetControlID="txtPMStDate">
                                            </ajaxToolkit:CalendarExtender>
                                        </EditItemTemplate>
                                        <FooterTemplate>
                                            <asp:TextBox ID="txtNewPMStDate" SkinID="TextBoxCalendarExtenderNormal" runat="server"
                                                Wrap="False" Width="112px"></asp:TextBox>
                                            <ajaxToolkit:CalendarExtender ID="calExttxtNewPMStDate" runat="server" EnableViewState="true"
                                                Format="dd/MM/yyyy" TargetControlID="txtNewPMStDate">
                                            </ajaxToolkit:CalendarExtender>
                                        </FooterTemplate>
                                        <FooterStyle Wrap="False" />
                                    </asp:TemplateField>
                                    <asp:TemplateField HeaderText="End Date ">
                                        <ItemTemplate>
                                            <asp:Label ID="lblPMEDate" runat="server" Text='<%# Bind("PreviousManagerEndDate") %>'></asp:Label>
                                        </ItemTemplate>
                                        <EditItemTemplate>
                                            <asp:TextBox ID="txtPMEDate" SkinID="TextBoxCalendarExtenderNormal" runat="server"
                                                Text='<%# Bind("PreviousManagerEndDate") %>' Wrap="False" Width="100px"></asp:TextBox>
                                            <ajaxToolkit:CalendarExtender ID="calExttxtPMEDate" runat="server" EnableViewState="true"
                                                Format="dd/MM/yyyy" TargetControlID="txtPMEDate">
                                            </ajaxToolkit:CalendarExtender>
                                        </EditItemTemplate>
                                        <FooterTemplate>
                                            <asp:TextBox ID="txtNewPMEDate" SkinID="TextBoxCalendarExtenderNormal" runat="server"></asp:TextBox>
                                            <ajaxToolkit:CalendarExtender ID="calExttxtNewPMEDate" runat="server" EnableViewState="true"
                                                Format="dd/MM/yyyy" TargetControlID="txtNewPMEDate">
                                            </ajaxToolkit:CalendarExtender>
                                        </FooterTemplate>
                                        <FooterStyle Wrap="False" />
                                    </asp:TemplateField>
                                    <asp:TemplateField ShowHeader="False">
                                        <ItemTemplate>
                                            <asp:LinkButton ID="btEdit" runat="server" CausesValidation="False" CommandName="Edit">
                                                <asp:Image ID="imgFolder" runat="server" ImageUrl="~/images/iGrid_Edit.png" />
                                            </asp:LinkButton>
                                        </ItemTemplate>
                                        <EditItemTemplate>
                                            <asp:LinkButton ID="btUpdate" runat="server" OnClientClick="return ValidateEditFields(this)"
                                                CausesValidation="True" CommandName="Update" ValidationGroup="AMEditGroup">
                                                <asp:Image ID="imgFolder1" runat="server" ImageUrl="~/images/iGrid_Ok.png" />
                                            </asp:LinkButton>
                                            <asp:LinkButton ID="btCancel" runat="server" CausesValidation="False" CommandName="Cancel">
                                                <asp:Image ID="imgFolder2" runat="server" ImageUrl="~/images/iGrid_Cancel.png" />
                                            </asp:LinkButton>
                                        </EditItemTemplate>
                                        <FooterTemplate>
                                            <asp:Button ID="btAdd" runat="server" Text="Add" CommandName="Insert" ValidationGroup="AMAddGroup"
                                                OnClientClick="return ValidateEditFields(this)" />
                                        </FooterTemplate>
                                    </asp:TemplateField>
                                </Columns>
                                <PagerStyle BackColor="#E7E7FF" ForeColor="#4A3C8C" HorizontalAlign="Right" />
                            </asp:GridView>
                        </td>
                    </tr>
                </table>
                <div class="transactionButtons">
                    <div class="transactionButtonsHolder">
                        <asp:Button SkinID="ButtonViewReport" ID="btnReport" runat="server" Text="Generate Report"
                            OnClick="btnReport_Click" />
                    </div>
                </div>
                <asp:ObjectDataSource ID="ObjectDataAM" runat="server" SelectMethod="GetAllAreaManagers"
                    TypeName="IMPALLibrary.AreaManagers" OnInserting="ODSAM_Inserting" InsertMethod="AddNewAreaManagers"
                    OnUpdating="ODSAM_Updating" UpdateMethod="UpdateAreaManagers">
                    <InsertParameters>
                        <asp:Parameter Name="AreaManagerCode" Type="String" />
                        <asp:Parameter Name="AreaManagerName" Type="String" />
                        <asp:Parameter Name="OperatingBranch" Type="String" />
                        <asp:Parameter Name="StartDate" Type="String" />
                        <asp:Parameter Name="EndDate" Type="String" />
                        <asp:Parameter Name="PreviousManager" Type="String" />
                        <asp:Parameter Name="PreviousManagerStartDate" Type="String" />
                        <asp:Parameter Name="PreviousManagerEndDate" Type="String" />
                    </InsertParameters>
                </asp:ObjectDataSource>
                <%--<asp:ObjectDataSource ID="ODSOPBranch" runat="server" SelectMethod="GetAllBranch"
                    TypeName="IMPALLibrary.Branches"></asp:ObjectDataSource>--%>
                <asp:ObjectDataSource ID="ODSOPBranch" runat="server" SelectMethod="GetAllBranches"
                    TypeName="IMPALLibrary.Branches"></asp:ObjectDataSource>
            </ContentTemplate>
            <Triggers>
                <asp:PostBackTrigger ControlID="btnReport" />
            </Triggers>
        </asp:UpdatePanel>
    </div>
</asp:Content>
