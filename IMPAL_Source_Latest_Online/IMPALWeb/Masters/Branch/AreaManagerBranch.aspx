<%@ Page Title="" Language="C#" MasterPageFile="~/Main.Master" AutoEventWireup="true"
    CodeBehind="AreaManagerBranch.aspx.cs" Inherits="IMPALWeb.AreaManagerBranch" %>

<%--<asp:Content ID="Content2" ContentPlaceHolderID="CPH_Header" runat="server">
</asp:Content>--%>
<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="ajaxToolkit" %>
<asp:Content ID="CPHDetails" ContentPlaceHolderID="CPHDetails" runat="server">

    <script type="text/javascript">
        function ValidateEditFields(lnk) {
            var txtAMCode = null;
            var txtAMName = null;
            var txtBrMgr = null;
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
            txtBrMgr = row.cells[2].children[0];
            ddlOpBranch = row.cells[3].children[0];
            txtStDate = row.cells[4].children[0];
            txtEDate = row.cells[5].children[0];
            //        txtPreviousManager = row.cells[5].children[0];
            //        txtPMStDate = row.cells[6].children[0];
            //        txtPMEDate = row.cells[7].children[0];

            var oStDateVal = txtStDate.value.trim();
            var oToDateVal = txtEDate.value.trim();
            //        var oPrevStDateVal = txtPMStDate.value.trim();
            //        var oPrevToDateVal = txtPMEDate.value.trim();
            var oSysDate = new Date();
            var oFromDate = oStDateVal.split("/");
            var oToDate = oToDateVal.split("/");
            //        var oPrevStDate = oPrevStDateVal.split("/");
            //        var oPrevToDate = oPrevToDateVal.split("/");
            var oFromDateFormatted = oFromDate[1] + "/" + oFromDate[0] + "/" + oFromDate[2];
            var oToDateFormatted = oToDate[1] + "/" + oToDate[0] + "/" + oToDate[2];
            //        var oPrevStDateFormatted = oPrevStDate[1] + "/" + oPrevStDate[0] + "/" + oPrevStDate[2];
            //        var oPrevToDateFormatted = oPrevToDate[1] + "/" + oPrevToDate[0] + "/" + oPrevToDate[2];


            if (validate(txtAMCode.value, "Code ")) {
                txtAMCode.focus();
                return false;
            }
            if (validate(txtAMName.value, " Area Manager ")) {
                txtAMName.focus();
                return false;
            }
            if (validate(txtBrMgr.value, " Branch Manager ")) {
                txtBrMgr.focus();
                return false;
            }

            if (validate(ddlOpBranch.value, " Operating From ")) {
                ddlOpBranch.focus();
                return false;
            }
            if (doValidateDate(txtStDate)) {
                txtStDate.focus();
                return false;
            }
            if (validate(txtStDate.value, "Start Date")) {
                txtStDate.focus();
                return false;
            }
            if (oSysDate < new Date(oFromDateFormatted)) {
                alert("Start Date should not be greater than System Date");
                txtStDate.value = "";
                txtStDate.focus();
                return false;
            }
            if (validate(txtEDate.value, "End Date")) {
                txtEDate.focus();
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
            
        }

    </script>

    <div id="DivOuter">
        <div class="subFormTitle">
            Area Manager & Branch
        </div>
        <asp:UpdatePanel ID="UpdatePanel1" runat="server">
            <ContentTemplate>
                <table>
                    <tr>
                        <td>
                            <asp:GridView ID="GV_AMB" runat="server" AutoGenerateColumns="False" DataSourceID="ObjectDataAMB"
                                AllowPaging="True" HorizontalAlign="Left" BackColor="White" BorderStyle="None"
                                BorderWidth="1px" CellPadding="3" CaptionAlign="Left" OnDataBinding="GV_AMB_DataBinding"
                                ShowFooter="True" OnSelectedIndexChanged="GV_AMB_SelectedIndexChanged" OnRowCommand="GV_AMB_RowCommand"
                                OnRowUpdating="GV_AMB_RowUpdating" SkinID="GridView">
                                <Columns>
                                    <asp:TemplateField HeaderText="Code">
                                        <ItemTemplate>
                                            <asp:Label ID="lblAMBCode" runat="server" Text='<%# Bind("AreaManagerCode") %>'></asp:Label>
                                        </ItemTemplate>
                                        <EditItemTemplate>
                                            <asp:TextBox ID="txtAMBCode" runat="server" Text='<%# Bind("AreaManagerCode") %>'
                                                Wrap="False" Width="50px"></asp:TextBox>
                                        </EditItemTemplate>
                                        <FooterTemplate>
                                            <asp:TextBox ID="txtNewAMBCode" runat="server" Wrap="False" Width="80px"></asp:TextBox>
                                            
                                            <asp:RequiredFieldValidator ID="rvNewAMCode" runat="server" ControlToValidate="txtNewAMBCode"
                                                ErrorMessage="Please enter a valid Code" ValidationGroup="AMBAddGroup">
                                            </asp:RequiredFieldValidator>
                                        </FooterTemplate>
                                        <FooterStyle Wrap="False" />
                                    </asp:TemplateField>
                                    <asp:TemplateField HeaderText="Area Manager">
                                        <ItemTemplate>
                                            <asp:Label ID="txtAMBName" runat="server" Text='<%# Bind("AreaManagerName") %>'></asp:Label>
                                        </ItemTemplate>
                                        <EditItemTemplate>
                                            <asp:TextBox ID="txtAMBName" runat="server" Text='<%# Bind("AreaManagerName") %>'
                                                Wrap="False" Width="50px"></asp:TextBox>
                                                
                                        <asp:RequiredFieldValidator ID="rvNewAMName" runat="server" ControlToValidate="txtAMBName"
                                                ErrorMessage="Please enter a valid Name" ValidationGroup="AMBEditGroup">
                                            </asp:RequiredFieldValidator>                
                                        </EditItemTemplate>
                                        <FooterTemplate>
                                            <asp:DropDownList ID="ddlAMBName" runat="server" DataSourceID="ODSAMB" DataTextField="AreaManagerName"
                                                DataValueField="AreaManagerCode" Height="19px" Width="199px">
                                            </asp:DropDownList>
                                            <asp:RequiredFieldValidator ID="rvNewAMName" runat="server" ControlToValidate="ddlAMBName"
                                                ErrorMessage="Please enter a valid Name" ValidationGroup="AMBAddGroup">
                                            </asp:RequiredFieldValidator>
                                        </FooterTemplate>
                                        <FooterStyle Wrap="False" />
                                    </asp:TemplateField>
                                    <asp:TemplateField HeaderText="Branch Manager">
                                        <ItemTemplate>
                                            <asp:Label ID="txtBMName" runat="server" Text='<%# Bind("BranchManager") %>'></asp:Label>
                                        </ItemTemplate>
                                        <EditItemTemplate>
                                            <asp:TextBox ID="txtBMName" runat="server" Text='<%# Bind("BranchManager") %>' Wrap="False"
                                                Width="100px"></asp:TextBox>
                                            <br />
                                            <asp:RequiredFieldValidator ID="rvBMName" runat="server" ControlToValidate="txtBMName"
                                                ErrorMessage="Please enter a valid Name" ValidationGroup="AMBEditGroup">
                                            </asp:RequiredFieldValidator>
                                        </EditItemTemplate>
                                        <FooterTemplate>
                                            <asp:TextBox ID="txtNewBMName" runat="server" Wrap="False"></asp:TextBox>
                                            <br />
                                            <asp:RequiredFieldValidator ID="rvNewBMName" runat="server" ControlToValidate="txtNewBMName"
                                                ErrorMessage="Please enter a valid Name" ValidationGroup="AMBAddGroup">
                                            </asp:RequiredFieldValidator>
                                        </FooterTemplate>
                                        <FooterStyle Wrap="False" />
                                    </asp:TemplateField>
                                    <asp:TemplateField HeaderText="Operating From " SortExpression="BranchName">
                                        <EditItemTemplate>
                                            <asp:DropDownList ID="ddlOpBranch" runat="server" 
                                                DataSourceID="ODSOPBranch" DataTextField="BranchName" DataValueField="BranchCode" Width="185px">
                                            </asp:DropDownList>
                                            
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
                                            <asp:TextBox ID="txtAMBStDate" runat="server" Text='<%# Bind("StartDate") %>' Wrap="False"
                                                Width="100px"></asp:TextBox>
                                        </EditItemTemplate>
                                        <FooterTemplate>
                                            <asp:TextBox ID="txtAMBStDate" runat="server" SkinID="GridViewTextBox"></asp:TextBox>
                                            <%--<asp:ImageButton ID="imgBtnChequeDate" runat="server" alt="Calendar" ImageUrl="~/Images/Calendar.png"
                                                SkinID="ImageButtonCalendar" />--%>
                                            <ajaxToolkit:CalendarExtender ID="calExttxtAMBStDate" runat="server" EnableViewState="true"
                                                Format="dd/MM/yyyy" TargetControlID="txtAMBStDate">
                                            </ajaxToolkit:CalendarExtender>
                                            <%--<asp:TextBox ID="txtAMBStDate" runat="server" Wrap="False" Width="112px"></asp:TextBox>--%>
                                        </FooterTemplate>
                                        <FooterStyle Wrap="False" />
                                    </asp:TemplateField>
                                    <asp:TemplateField HeaderText="End Date">
                                        <ItemTemplate>
                                            <asp:Label ID="lblEDate" runat="server" Text='<%# Bind("EndDate") %>'></asp:Label>
                                        </ItemTemplate>
                                        <EditItemTemplate>
                                            <asp:TextBox ID="txtAMBEDate" runat="server" Text='<%# Bind("EndDate") %>' Wrap="False"
                                                Width="100px"></asp:TextBox>
                                        </EditItemTemplate>
                                        <FooterTemplate>
                                            <asp:TextBox ID="txtAMBEDate" runat="server" SkinID="GridViewTextBox"></asp:TextBox>
                                            <ajaxToolkit:CalendarExtender ID="calExttxtAMBEDate" runat="server" EnableViewState="true"
                                                Format="dd/MM/yyyy" TargetControlID="txtAMBEDate">
                                            </ajaxToolkit:CalendarExtender>
                                            <%--<asp:TextBox ID="txtAMBEDate" runat="server" Wrap="False" Width="105px"></asp:TextBox>--%>
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
                                            <asp:LinkButton ID="btUpdate" runat="server" CausesValidation="True" CommandName="Update"
                                                OnClientClick="return ValidateEditFields(this)" ValidationGroup="AMBEditGroup">
                                                <asp:Image ID="imgFolder1" runat="server" ImageUrl="~/images/iGrid_Ok.png" />
                                            </asp:LinkButton>
                                            <asp:LinkButton ID="btCancel" runat="server" CausesValidation="False" CommandName="Cancel">
                                                <asp:Image ID="imgFolder2" runat="server" ImageUrl="~/images/iGrid_Cancel.png" />
                                            </asp:LinkButton>
                                        </EditItemTemplate>
                                        <FooterTemplate>
                                            <asp:Button ID="btAdd" runat="server" Text="Add" CommandName="Insert" ValidationGroup="AMBAddGroup"
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
                <asp:ObjectDataSource ID="ObjectDataAMB" runat="server" SelectMethod="GetAllAreaManagerBranches"
                    TypeName="IMPALLibrary.AreaManagerBranches" OnInserting="ODSAMB_Inserting" InsertMethod="AddNewAreaManagerBranches"
                    OnUpdating="ODSAMB_Updating" UpdateMethod="UpdateAreaManagerBranches">
                    <UpdateParameters>
                        <asp:Parameter Name="AreaManagerCode" Type="String" />
                        <asp:Parameter Name="AreaManagerName" Type="String" />
                        <asp:Parameter Name="BranchManager" Type="String" />
                        <asp:Parameter Name="OperatingBranch" Type="String" />
                        <asp:Parameter Name="StartDate" Type="String" />
                        <asp:Parameter Name="EndDate" Type="String" />
                    </UpdateParameters>
                    <InsertParameters>
                        <asp:Parameter Name="AreaManagerCode" Type="String" />
                        <asp:Parameter Name="AreaManagerName" Type="String" />
                        <asp:Parameter Name="BranchManager" Type="String" />
                        <asp:Parameter Name="OperatingBranch" Type="String" />
                        <asp:Parameter Name="StartDate" Type="String" />
                        <asp:Parameter Name="EndDate" Type="String" />
                    </InsertParameters>
                </asp:ObjectDataSource>
                <asp:ObjectDataSource ID="ODSAMBBranch" runat="server" SelectMethod="GetAllBranch"
                    TypeName="IMPALLibrary.Branches"></asp:ObjectDataSource>
                <asp:ObjectDataSource ID="ODSAMB" runat="server" SelectMethod="GetAllAreaManagers"
                    TypeName="IMPALLibrary.AreaManagers"></asp:ObjectDataSource>
                <%--<asp:ObjectDataSource ID="ODSOPBranch" runat="server" SelectMethod="GetAllBranch"
                    TypeName="IMPALLibrary.Branches"></asp:ObjectDataSource>--%>
                <asp:ObjectDataSource ID="ODSOPBranch" runat="server" SelectMethod="GetAllBranches_Area"
                    TypeName="IMPALLibrary.Branches"></asp:ObjectDataSource>
            </ContentTemplate>
            <Triggers>
                <asp:PostBackTrigger ControlID="btnReport" />
            </Triggers>
        </asp:UpdatePanel>
    </div>
</asp:Content>
