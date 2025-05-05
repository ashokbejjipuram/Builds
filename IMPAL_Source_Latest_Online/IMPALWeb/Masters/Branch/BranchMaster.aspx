<%@ Page Language="C#" MasterPageFile="~/Main.Master" AutoEventWireup="true" CodeBehind="BranchMaster.aspx.cs"
    Inherits="IMPALWeb.Masters.Branch.BranchMaster" %>

<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="ajaxToolkit" %>
<asp:Content ID="CPHDetails" ContentPlaceHolderID="CPHDetails" runat="server">

    <script type="text/javascript" language="javascript">
        function validateFields(source, arguments) {
            //             debugger;
            var firstchr = (arguments.Value).substring(0, 1, (arguments.Value));
            if (isspecialchar(firstchr)) {
                arguments.IsValid = false;
                return;
            }
            else {
                arguments.IsValid = true;
                return;
            }
        }
        function isspecialchar(c) {
            myArray = ['!', '#', '$', '%', '^', '&', '*', '(', ')', '-', '+', '=', '_', '`', '~', ']', '[', '|', '@', '/', '"', ':', ';', '{', '}', ',', "'", '.', '?', '\\'];

            for (var j = 0; j < myArray.length; j++) {
                if (c == myArray[j]) {
                    return true;
                }

            }
            return false;
        }

        function valBranchCode(source, arguments) {
//            debugger;
            var len = (arguments.Value).length;
            if (len <= 2) {
                arguments.IsValid = false;
                return;
            }
            else {
                arguments.IsValid = true;
                return;
            }
        }
    </script>

    <asp:UpdatePanel ID="UpdatePanel1" runat="server">
        <ContentTemplate>
            <div id="DivOuter" runat="server">
                <asp:FormView ID="BranchFormView" runat="server" OnDataBound="BranchFormView_DataBound">
                    <HeaderStyle ForeColor="white" BackColor="Blue" />
                    <ItemTemplate>
                        <div>
                            <div class="subFormTitle subFormTitleExtender350">
                                BRANCH</div>
                            <table class="subFormTable">
                                <tr>
                                    <td class="labelColSpan2">
                                        <asp:Label ID="lblCode" runat="server" SkinID="LabelNormal" Text="Code"></asp:Label>
                                        <span class="asterix">*</span>
                                    </td>
                                    <td class="inputcontrolsColSpan2">
                                        <asp:DropDownList ID="ddlBranch" AutoPostBack="true" runat="server" SkinID="DropDownListNormalBig"
                                            OnSelectedIndexChanged="ddlBranch_SelectedIndexChanged">
                                        </asp:DropDownList>
                                    </td>
                                    <td class="labelColSpan2">
                                        <asp:Label ID="lblName" runat="server" SkinID="LabelNormal" Text="Name"></asp:Label>
                                        <span class="asterix">*</span>
                                    </td>
                                    <td class="inputcontrolsColSpan2">
                                        <asp:TextBox ID="txtName" runat="server" ReadOnly="true" Text='<%# Bind("Branch_Name") %>'
                                            SkinID="TextBoxDisabledBig"></asp:TextBox>
                                    </td>
                                </tr>
                            </table>
                            <div class="subFormTitle subFormTitleExtender350">
                                COMMUNICATION DETAIL</div>
                            <table class="subFormTable">
                                <tr>
                                    <td class="labelColSpan2">
                                        <asp:Label ID="lblAddress" runat="server" SkinID="LabelNormal" Text="Address"></asp:Label>
                                        <span class="asterix">*</span>
                                    </td>
                                    <td class="inputcontrolsColSpan2">
                                        <asp:TextBox ID="txtAddress" runat="server" ReadOnly="true" TextMode="MultiLine"
                                            Text='<%# Bind("Address") %>' SkinID="TextBoxMultilineDisable"></asp:TextBox>
                                    </td>
                                    <td class="labelColSpan2">
                                        <asp:Label ID="lblState" runat="server" SkinID="LabelNormal" Text="State"></asp:Label>
                                        <span class="asterix">*</span>
                                    </td>
                                    <td class="inputcontrolsColSpan2">
                                        <asp:DropDownList ID="ddlState" runat="server" Enabled="false" SkinID="DropDownListDisabledBig">
                                        </asp:DropDownList>
                                    </td>
                                </tr>
                                <tr>
                                    <td class="labelColSpan2">
                                        <asp:Label ID="lblPhone" runat="server" SkinID="LabelNormal" Text="Phone"></asp:Label>
                                    </td>
                                    <td class="inputcontrolsColSpan2">
                                        <asp:TextBox ID="txtPhone" runat="server" ReadOnly="true" Text='<%# Bind("Phone") %>'
                                            SkinID="TextBoxDisabledBig"></asp:TextBox>
                                    </td>
                                    <td class="labelColSpan2">
                                        <asp:Label ID="lblFax" runat="server" SkinID="LabelNormal" Text="Fax"></asp:Label>
                                    </td>
                                    <td class="inputcontrolsColSpan2">
                                        <asp:TextBox ID="txtFax" runat="server" ReadOnly="true" Text='<%# Bind("Fax") %>'
                                            SkinID="TextBoxDisabledBig"></asp:TextBox>
                                    </td>
                                </tr>
                                <tr>
                                    <td class="labelColSpan2">
                                        <asp:Label ID="lblTelex" runat="server" SkinID="LabelNormal" Text="Telex"></asp:Label>
                                    </td>
                                    <td class="inputcontrolsColSpan2">
                                        <asp:TextBox ID="txtTelex" runat="server" ReadOnly="true" Text='<%# Bind("Telex") %>'
                                            SkinID="TextBoxDisabledBig"></asp:TextBox>
                                    </td>
                                    <td class="labelColSpan2">
                                        <asp:Label ID="lblEmailid" runat="server" SkinID="LabelNormal" Text="Email id"></asp:Label>
                                    </td>
                                    <td class="inputcontrolsColSpan2">
                                        <asp:TextBox ID="txtEmailid" runat="server" ReadOnly="true" Text='<%# Bind("Email") %>'
                                            SkinID="TextBoxDisabledBig"></asp:TextBox>
                                    </td>
                                </tr>
                                <tr>
                                    <td class="labelColSpan2">
                                        <asp:Label ID="lblClassification" runat="server" SkinID="LabelNormal" Text="Classification"></asp:Label>
                                    </td>
                                    <td class="inputcontrolsColSpan2">
                                        <asp:TextBox ID="txtClassification" runat="server" ReadOnly="true" Text='<%# Bind("Classification_Code") %>'
                                            SkinID="TextBoxDisabledBig"></asp:TextBox>
                                    </td>
                                    <td class="labelColSpan2">
                                        <asp:Label ID="lblReportingState" runat="server" SkinID="LabelNormal" Text="Reporting State"></asp:Label>
                                    </td>
                                    <td class="inputcontrolsColSpan2">
                                        <asp:DropDownList ID="ddlRptingState" runat="server" Enabled="false" SkinID="DropDownListDisabledBig">
                                        </asp:DropDownList>
                                    </td>
                                </tr>
                            </table>
                            <div class="subFormTitle subFormTitleExtender350">
                                BRANCH INFORMATION</div>
                            <table class="subFormTable">
                                <tr>
                                    <td class="labelColSpan2">
                                        <asp:Label ID="lblBranchManager" runat="server" SkinID="LabelNormal" Text="Branch Manager"></asp:Label>
                                        <span class="asterix">*</span>
                                    </td>
                                    <td class="inputcontrolsColSpan2">
                                        <asp:TextBox ID="txtBranchManager" runat="server" ReadOnly="true" Text='<%# Bind("Branch_Manager") %>'
                                            SkinID="TextBoxDisabledBig"></asp:TextBox>
                                    </td>
                                    <td class="labelColSpan2">
                                        <asp:Label ID="lblAccountsManager_Officer" runat="server" SkinID="LabelNormal" Text="Accounts Manager/Officer"></asp:Label>
                                    </td>
                                    <td class="inputcontrolsColSpan2">
                                        <asp:TextBox ID="txtlblAccountsManager_Officer" runat="server" ReadOnly="true" Text='<%# Bind("Branch_Accountant") %>'
                                            SkinID="TextBoxDisabledBig"></asp:TextBox>
                                    </td>
                                </tr>
                                <tr>
                                    <td class="labelColSpan2">
                                        <asp:Label ID="lblEDPin_charge" runat="server" SkinID="LabelNormal" Text="EDP in-charge"></asp:Label>
                                        <span class="asterix">*</span>
                                    </td>
                                    <td class="inputcontrolsColSpan2">
                                        <asp:TextBox ID="txtEDPin_charge" runat="server" ReadOnly="true" Text='<%# Bind("EDP_In_charge") %>'
                                            SkinID="TextBoxDisabledBig"></asp:TextBox>
                                    </td>
                                    <td class="labelColSpan2">
                                        <asp:Label ID="lblOpeningDate" runat="server" SkinID="LabelNormal" Text="Opening Date"></asp:Label>
                                    </td>
                                    <td class="inputcontrolsColSpan2">
                                        <asp:TextBox ID="txtOpeningDate" runat="server" ReadOnly="true" Text='<%# Bind("Opening_Date") %>'
                                            SkinID="TextBoxDisabledBig"></asp:TextBox>
                                    </td>
                                </tr>
                                <tr>
                                    <td class="labelColSpan2">
                                        <asp:Label ID="lblArea_sq_feet" runat="server" SkinID="LabelNormal" Text="Area (sq.feet)"></asp:Label>
                                        <span class="asterix">*</span>
                                    </td>
                                    <td class="inputcontrolsColSpan2">
                                        <asp:TextBox ID="txtArea_sq_feet" runat="server" ReadOnly="true" Text='<%# Bind("Area_in_Square_Feet") %>'
                                            SkinID="TextBoxDisabledBig"></asp:TextBox>
                                    </td>
                                    <td class="labelColSpan2">
                                        <asp:Label ID="lblMonthlyRent" runat="server" SkinID="LabelNormal" Text="Monthly Rent"></asp:Label>
                                    </td>
                                    <td class="inputcontrolsColSpan2">
                                        <asp:TextBox ID="txtMonthlyRent" runat="server" ReadOnly="true" Text='<%# Bind("Monthly_Rent") %>'
                                            SkinID="TextBoxDisabledBig"></asp:TextBox>
                                    </td>
                                </tr>
                                <tr>
                                    <td class="labelColSpan2">
                                        <asp:Label ID="lblRentalAdvance" runat="server" SkinID="LabelNormal" Text="Rental Advance"></asp:Label>
                                    </td>
                                    <td class="inputcontrolsColSpan2">
                                        <asp:TextBox ID="txtRentalAdvance" runat="server" ReadOnly="true" Text='<%# Bind("Rental_Advance")%>'
                                        SkinID="TextBoxDisabledBig"></asp:TextBox>
                                    </td>
                                    <td class="labelColSpan2">
                                        <asp:Label ID="lblContractStartDate" runat="server" SkinID="LabelNormal" Text="Contract Start Date"></asp:Label>
                                    </td>
                                    <td class="inputcontrolsColSpan2">
                                        <asp:TextBox ID="txtContractStartDate" runat="server" ReadOnly="true" Text='<%# Bind("Rental_Contract_Start_Date") %>'
                                             SkinID="TextBoxDisabledBig"></asp:TextBox>
                                        
                                    </td>
                                </tr>
                                <tr>
                                    <td class="labelColSpan2">
                                        <asp:Label ID="lblContractCompletionDate" runat="server" SkinID="LabelNormal" Text="Contract Completion Date"></asp:Label>
                                    </td>
                                    <td class="inputcontrolsColSpan2">
                                        <asp:TextBox ID="txtContractCompletionDate" runat="server" ReadOnly="true" Text='<%# Bind("Rental_Contract_End_Date") %>'
                                            SkinID="TextBoxDisabledBig"></asp:TextBox>
                                    </td>
                                    <td class="labelColSpan2">
                                        <asp:Label ID="lblNoticePeriod" runat="server" SkinID="LabelNormal" Text="Notice Period"></asp:Label>
                                    </td>
                                    <td class="inputcontrolsColSpan2">
                                        <asp:TextBox ID="txtNoticePeriod" runat="server" ReadOnly="true" Text='<%# Bind("Termination_Notice_Period") %>'
                                            SkinID="TextBoxDisabledBig"></asp:TextBox>
                                    </td>
                                </tr>
                                <tr>
                                    <td class="labelColSpan2">
                                        <asp:Label ID="lblDestination" runat="server" SkinID="LabelNormal" Text="Destination"></asp:Label>
                                    </td>
                                    <td class="inputcontrolsColSpan2">
                                        <asp:TextBox ID="txtDestination" runat="server" ReadOnly="true" Text='<%# Bind("Branch_Destination") %>'
                                            SkinID="TextBoxDisabledBig"></asp:TextBox>
                                    </td>
                                    <td class="labelColSpan2">
                                        <asp:Label ID="lblPreferredCarrier" runat="server" SkinID="LabelNormal" Text="Preferred Carrier"></asp:Label>
                                    </td>
                                    <td class="inputcontrolsColSpan2">
                                        <asp:TextBox ID="txtPreferredCarrier" runat="server" ReadOnly="true" Text='<%# Bind("Branch_Carrier") %>'
                                            SkinID="TextBoxDisabledBig"></asp:TextBox>
                                    </td>
                                </tr>
                                <tr>
                                    <td class="labelColSpan2">
                                        <asp:Label ID="lblRoadPermitApplicable" runat="server" SkinID="LabelNormal" Text="Road Permit Applicable"></asp:Label>
                                    </td>
                                    <td class="inputcontrolsColSpan2">
                                        <asp:CheckBox ID="chkRoadPermitApplicable" Enabled="false" runat="server" Checked='<%# Bind("Road_Permit_Indicator") %>' />
                                    </td>
                                    <td class="labelColSpan2">
                                        <asp:Label ID="lblStatus" runat="server" SkinID="LabelNormal" Text="Status"></asp:Label>
                                    </td>
                                    <td class="inputcontrolsColSpan2">
                                        <asp:TextBox ID="txtStatus" runat="server" ReadOnly="true" Text='<%# Bind("Status") %>'
                                            SkinID="TextBoxDisabledBig"></asp:TextBox>
                                    </td>
                                </tr>
                                <tr>
                                    <td class="labelColSpan2">
                                        <asp:Label ID="lblCloseDate" runat="server" SkinID="LabelNormal" Text="Close Date"></asp:Label>
                                    </td>
                                    <td class="inputcontrolsColSpan2">
                                        <asp:TextBox ID="txtCloseDate" runat="server" ReadOnly="true" Text='<%# Bind("End_Date") %>'
                                            SkinID="TextBoxDisabledBig"></asp:TextBox>
                                    </td>
                                    <td class="labelColSpan2">
                                        <asp:Label ID="lblOSLocking" runat="server" SkinID="LabelNormal" Text="OS Locking"></asp:Label>
                                    </td>
                                    <%--<td class="inputcontrolsColSpan2">
                                        <asp:TextBox ID="txtOSLocking" runat="server" ReadOnly="true" Text='<%# Bind("OS_Lock") %>' SkinID="TextBoxDisabledBig"></asp:TextBox>
                                    </td>--%>
                                    <td class="inputcontrols">
                                        <asp:DropDownList ID="ddlOSLocking" runat="server" SkinID="DropDownListNormalBig"
                                            Enabled="false">
                                        </asp:DropDownList>
                                    </td>
                                </tr>
                                <tr>
                                    <td class="labelColSpan2">
                                        <asp:Label ID="lblO_SCalculationDays" runat="server" SkinID="LabelNormal" Text="O/S Calculation Days"></asp:Label>
                                    </td>
                                    <td class="inputcontrolsColSpan2">
                                        <asp:TextBox ID="txtO_SCalculationDays" runat="server" ReadOnly="true" Text='<%# Bind("OS_Cancellation_days") %>'
                                            SkinID="TextBoxDisabledBig"></asp:TextBox>
                                    </td>
                                    <td class="labelColSpan2">
                                    </td>
                                    <td class="inputcontrolsColSpan2">
                                    </td>
                                </tr>
                            </table>
                            <div class="subFormTitle subFormTitleExtender350">
                                MINIMUM PARAMETER FOR STOCK TRANSFER
                            </div>
                            <table class="subFormTable">
                                <tr>
                                    <td class="labelColSpan2">
                                        <asp:Label ID="lblStockAging" runat="server" SkinID="LabelNormal" Text="Stock Aging"></asp:Label>
                                        <span class="asterix">*</span>
                                    </td>
                                    <td class="inputcontrolsColSpan2">
                                        <asp:TextBox ID="txtStockAging" runat="server" ReadOnly="true" Text='<%# Bind("Min_Stock_Age_for_STDN") %>'
                                            SkinID="TextBoxDisabledBig"></asp:TextBox>
                                    </td>
                                    <td class="labelColSpan2">
                                        <asp:Label ID="lblStockValue" runat="server" SkinID="LabelNormal" Text="Stock Value"></asp:Label>
                                    </td>
                                    <td class="inputcontrolsColSpan2">
                                        <asp:TextBox ID="txtStockValue" runat="server" ReadOnly="true" Text='<%# Bind("Min_Stock_Value_for_STDN") %>'
                                            SkinID="TextBoxDisabledBig" contentEditable="false"></asp:TextBox>
                                    </td>
                                </tr>
                            </table>
                            <div class="subFormTitle subFormTitleExtender350">
                                SALES TAX</div>
                            <table class="subFormTable">
                                <tr>
                                    <td class="labelColSpan2">
                                        <asp:Label ID="lblLocalST_RCNo" runat="server" SkinID="LabelNormal" Text="Local ST RC no"></asp:Label>
                                        <span class="asterix">*</span>
                                    </td>
                                    <td class="inputcontrolsColSpan2">
                                        <asp:TextBox ID="txtLocalST_RCNo" runat="server" ReadOnly="true" Text='<%# Bind("Local_sales_tax_number") %>'
                                            SkinID="TextBoxDisabledBig"></asp:TextBox>
                                    </td>
                                    <td class="labelColSpan2">
                                        <asp:Label ID="lblCentralSalesTaxNumber" runat="server" SkinID="LabelNormal" Text="Central Sales Tax Number"></asp:Label>
                                    </td>
                                    <td class="inputcontrolsColSpan2">
                                        <asp:TextBox ID="txtCentralSalesTaxNumber" runat="server" ReadOnly="true" Text='<%# Bind("Central_sales_tax_number") %>'
                                            SkinID="TextBoxDisabledBig" contentEditable="false"></asp:TextBox>
                                    </td>
                                </tr>
                                <tr>
                                    <td class="labelColSpan2">
                                        <asp:Label ID="lblCESSNumber" runat="server" SkinID="LabelNormal" Text="CESS Number"></asp:Label>
                                    </td>
                                    <td class="inputcontrolsColSpan2">
                                        <asp:TextBox ID="txtCESSNumber" runat="server" ReadOnly="true" Text='<%# Bind("CESS_number") %>'
                                            SkinID="TextBoxDisabledBig"></asp:TextBox>
                                    </td>
                                    <td class="labelColSpan2">
                                        <asp:Label ID="lblTINNumber" runat="server" SkinID="LabelNormal" Text="TIN Number"></asp:Label>
                                    </td>
                                    <td class="inputcontrolsColSpan2">
                                        <asp:TextBox ID="txtTINNumber" runat="server" ReadOnly="true" Text='<%# Bind("tin") %>'
                                            SkinID="TextBoxDisabledBig" contentEditable="false"></asp:TextBox>
                                    </td>
                                </tr>
                            </table>
                        </div>
                    </ItemTemplate>
                    <InsertItemTemplate>
                        <div>
                            <div class="subFormTitle subFormTitleExtender350">
                                BRANCH</div>
                            <table class="subFormTable">
                                <tr>
                                    <td class="labelColSpan2">
                                        <asp:Label ID="lblCode" runat="server" SkinID="LabelNormal" Text="Code"></asp:Label>
                                        <span class="asterix">*</span>
                                    </td>
                                    <td class="inputcontrolsColSpan2">
                                        <asp:TextBox ID="txtBranchCode" runat="server" TabIndex="0" SkinID="TextBoxNormalBig"
                                            MaxLength="3"></asp:TextBox>
                                        <asp:ImageButton ID="ImgButtonQuery" ImageUrl="~/Images/ifind.png" alt="Query" runat="server"
                                            SkinID="ImageButtonSearch" OnClick="ImgButtonQuery_Click" />
                                    </td>
                                    <td class="labelColSpan2">
                                        <asp:Label ID="lblName" runat="server" SkinID="LabelNormal" Text="Name"></asp:Label>
                                    </td>
                                    <td class="inputcontrolsColSpan2">
                                        <asp:TextBox ID="txtBranchName" runat="server" TabIndex="1" SkinID="TextBoxNormalBig"
                                            MaxLength="40"></asp:TextBox>
                                    </td>
                                </tr>
                            </table>
                            <div class="subFormTitle subFormTitleExtender350">
                                COMMUNICATION DETAIL</div>
                            <table class="subFormTable">
                                <tr>
                                    <td class="labelColSpan2">
                                        <asp:Label ID="lblAddress" runat="server" SkinID="LabelNormal" Text="Address"></asp:Label>
                                        <span class="asterix">*</span>
                                    </td>
                                    <td class="inputcontrolsColSpan2">
                                        <asp:TextBox ID="txtAddress" runat="server" TabIndex="2" TextMode="MultiLine" SkinID="TextBoxMultilineDisable"
                                            MaxLength="200"></asp:TextBox>
                                    </td>
                                    <td class="labelColSpan2">
                                        <asp:Label ID="lblState" runat="server" SkinID="LabelNormal" Text="State"></asp:Label>
                                    </td>
                                    <td class="inputcontrolsColSpan2">
                                        <asp:DropDownList ID="ddlState" TabIndex="3" runat="server" SkinID="DropDownListNormalBig">
                                        </asp:DropDownList>
                                    </td>
                                </tr>
                                <tr>
                                    <td class="labelColSpan2">
                                        <asp:Label ID="lblPhone" runat="server" SkinID="LabelNormal" Text="Phone"></asp:Label>
                                    </td>
                                    <td class="inputcontrolsColSpan2">
                                        <asp:TextBox ID="txtPhone" runat="server" TabIndex="4" SkinID="TextBoxNormalBig"
                                            MaxLength="40"></asp:TextBox>
                                    </td>
                                    <td class="labelColSpan2">
                                        <asp:Label ID="lblFax" runat="server" SkinID="LabelNormal" Text="Fax"></asp:Label>
                                    </td>
                                    <td class="inputcontrolsColSpan2">
                                        <asp:TextBox ID="txtFax" runat="server" TabIndex="5" SkinID="TextBoxNormalBig" MaxLength="40"></asp:TextBox>
                                    </td>
                                </tr>
                                <tr>
                                    <td class="labelColSpan2">
                                        <asp:Label ID="lblTelex" runat="server" SkinID="LabelNormal" Text="Telex"></asp:Label>
                                    </td>
                                    <td class="inputcontrolsColSpan2">
                                        <asp:TextBox ID="txtTelex" runat="server" TabIndex="6" SkinID="TextBoxNormalBig"
                                            MaxLength="40"></asp:TextBox>
                                    </td>
                                    <td class="labelColSpan2">
                                        <asp:Label ID="lblEmailid" runat="server" SkinID="LabelNormal" Text="Email id"></asp:Label>
                                    </td>
                                    <td class="inputcontrolsColSpan2">
                                        <asp:TextBox ID="txtEmailid" runat="server" TabIndex="7" SkinID="TextBoxNormalBig"
                                            MaxLength="60"></asp:TextBox>
                                    </td>
                                </tr>
                                <tr>
                                    <td class="labelColSpan2">
                                        <asp:Label ID="lblClassification" runat="server" SkinID="LabelNormal" Text="Classification"></asp:Label>
                                        <span class="asterix">*</span>
                                    </td>
                                    <td class="inputcontrolsColSpan2">
                                        <asp:DropDownList ID="ddlClassification" TabIndex="8" runat="server" SkinID="DropDownListNormalBig">
                                        </asp:DropDownList>
                                    </td>
                                    <td class="labelColSpan2">
                                        <asp:Label ID="lblReportingState" runat="server" SkinID="LabelNormal" Text="Reporting State"></asp:Label>
                                        <span class="asterix">*</span>
                                    </td>
                                    <td class="inputcontrolsColSpan2">
                                        <asp:DropDownList ID="ddlReportingSate" TabIndex="9" runat="server" SkinID="DropDownListNormalBig">
                                        </asp:DropDownList>
                                    </td>
                                </tr>
                            </table>
                            <div class="subFormTitle subFormTitleExtender350">
                                BRANCH INFORMATION</div>
                            <table class="subFormTable">
                                <tr>
                                    <td class="labelColSpan2">
                                        <asp:Label ID="lblBranchManager" runat="server" SkinID="LabelNormal" Text="Branch Manager"></asp:Label>
                                    </td>
                                    <td class="inputcontrolsColSpan2">
                                        <asp:TextBox ID="txtBranchManager" runat="server" TabIndex="10" SkinID="TextBoxNormalBig"
                                            MaxLength="40"></asp:TextBox>
                                    </td>
                                    <td class="labelColSpan2">
                                        <asp:Label ID="lblAccountsManager_Officer" runat="server" SkinID="LabelNormal" Text="Accounts Manager/Officer"></asp:Label>
                                    </td>
                                    <td class="inputcontrolsColSpan2">
                                        <asp:TextBox ID="txtlblAccountsManager_Officer" TabIndex="11" runat="server" SkinID="TextBoxNormalBig"
                                            MaxLength="40"></asp:TextBox>
                                    </td>
                                </tr>
                                <tr>
                                    <td class="labelColSpan2">
                                        <asp:Label ID="lblEDPin_charge" runat="server" SkinID="LabelNormal" Text="EDP in-charge"></asp:Label>
                                    </td>
                                    <td class="inputcontrolsColSpan2">
                                        <asp:TextBox ID="txtEDPin_charge" runat="server" TabIndex="12" SkinID="TextBoxNormalBig"
                                            MaxLength="40"></asp:TextBox>
                                    </td>
                                    <td class="labelColSpan2">
                                        <asp:Label ID="lblOpeningDate" runat="server" SkinID="LabelNormal" Text="Opening Date"></asp:Label>
                                        <span class="asterix">*</span>
                                    </td>
                                    <td class="inputcontrolsColSpan2">
                                        <asp:TextBox ID="txtOpeningDate" runat="server" TabIndex="13" onmousedown="TriggerCalender('BranchFormView_ImgOpeningDate');"
                                            contentEditable="false" SkinID="TextBoxNormalBig"></asp:TextBox>
                                        <asp:ImageButton ID="ImgOpeningDate" runat="server" alt="Calendar" Height="18" ImageUrl="~/Images/Calendar.png"
                                            TabIndex="10" Width="18" />
                                        <ajaxToolkit:CalendarExtender ID="OpeningDateExtender" runat="server" EnableViewState="true"
                                            Format="dd/MM/yyyy" PopupButtonID="ImgOpeningDate" PopupPosition="BottomLeft"
                                            TargetControlID="txtOpeningDate">
                                        </ajaxToolkit:CalendarExtender>
                                    </td>
                                </tr>
                                <tr>
                                    <td class="labelColSpan2">
                                        <asp:Label ID="lblArea_sq_feet" runat="server" SkinID="LabelNormal" Text="Area (sq.feet)"></asp:Label>
                                    </td>
                                    <td class="inputcontrolsColSpan2">
                                        <asp:TextBox ID="txtArea_sq_feet" runat="server" TabIndex="14" SkinID="TextBoxNormalBig"
                                            MaxLength="9"></asp:TextBox>
                                    </td>
                                    <td class="labelColSpan2">
                                        <asp:Label ID="lblMonthlyRent" runat="server" SkinID="LabelNormal" Text="Monthly Rent"></asp:Label>
                                    </td>
                                    <td class="inputcontrolsColSpan2">
                                        <asp:TextBox ID="txtMonthlyRent" runat="server" TabIndex="15" SkinID="TextBoxNormalBig"
                                            MaxLength="8"></asp:TextBox>
                                    </td>
                                </tr>
                                <tr>
                                    <td class="labelColSpan2">
                                        <asp:Label ID="lblRentalAdvance" runat="server" SkinID="LabelNormal" Text="Rental Advance"></asp:Label>
                                    </td>
                                    <td class="inputcontrolsColSpan2">
                                        <asp:TextBox ID="txtRentalAdvance" runat="server" TabIndex="16" SkinID="TextBoxNormalBig"
                                            MaxLength="8"></asp:TextBox>
                                    </td>
                                    <td class="labelColSpan2">
                                        <asp:Label ID="lblContractStartDate" runat="server" SkinID="LabelNormal" Text="Contract Start Date"></asp:Label>
                                    </td>
                                    <td class="inputcontrolsColSpan2">
                                        <asp:TextBox ID="txtContractStartDate" runat="server" onmousedown="TriggerCalender('BranchFormView_ImgContractStartDate');"
                                            contentEditable="false" SkinID="TextBoxNormalBig"></asp:TextBox>
                                        <asp:ImageButton ID="ImgContractStartDate" runat="server" TabIndex="17" alt="Calendar"
                                            Height="18" ImageUrl="~/Images/Calendar.png" Width="18" />
                                        <ajaxToolkit:CalendarExtender ID="ContractStartDateExtender" runat="server" EnableViewState="true"
                                            Format="dd/MM/yyyy" PopupButtonID="ImgContractStartDate" PopupPosition="BottomLeft"
                                            TargetControlID="txtContractStartDate">
                                        </ajaxToolkit:CalendarExtender>
                                    </td>
                                </tr>
                                <tr>
                                    <td class="labelColSpan2">
                                        <asp:Label ID="lblContractCompletionDate" runat="server" SkinID="LabelNormal" Text="Contract Completion Date"></asp:Label>
                                    </td>
                                    <td class="inputcontrolsColSpan2">
                                        <asp:TextBox ID="txtContractCompletionDate" runat="server" onmousedown="TriggerCalender('BranchFormView_ImgContractCompletionDate');"
                                            contentEditable="false" SkinID="TextBoxNormalBig"></asp:TextBox>
                                        <asp:ImageButton ID="ImgContractCompletionDate" TabIndex="18" runat="server" alt="Calendar"
                                            Height="18" ImageUrl="~/Images/Calendar.png" Width="18" />
                                        <ajaxToolkit:CalendarExtender ID="ContractCompletionDateExtender1" runat="server"
                                            EnableViewState="true" Format="dd/MM/yyyy" PopupButtonID="ImgContractCompletionDate"
                                            PopupPosition="BottomLeft" TargetControlID="txtContractCompletionDate">
                                        </ajaxToolkit:CalendarExtender>
                                    </td>
                                    <td class="labelColSpan2">
                                        <asp:Label ID="lblNoticePeriod" runat="server" SkinID="LabelNormal" Text="Notice Period"></asp:Label>
                                    </td>
                                    <td class="inputcontrolsColSpan2">
                                        <asp:TextBox ID="txtNoticePeriod" runat="server" TabIndex="19" SkinID="TextBoxNormalBig"
                                            MaxLength="2"></asp:TextBox>
                                    </td>
                                </tr>
                                <tr>
                                    <td class="labelColSpan2">
                                        <asp:Label ID="lblDestination" runat="server" SkinID="LabelNormal" Text="Destination"></asp:Label>
                                    </td>
                                    <td class="inputcontrolsColSpan2">
                                        <asp:TextBox ID="txtDestination" runat="server" TabIndex="20" SkinID="TextBoxNormalBig"
                                            MaxLength="40"></asp:TextBox>
                                    </td>
                                    <td class="labelColSpan2">
                                        <asp:Label ID="lblPreferredCarrier" runat="server" SkinID="LabelNormal" Text="Preferred Carrier"></asp:Label>
                                    </td>
                                    <td class="inputcontrolsColSpan2">
                                        <asp:TextBox ID="txtPreferredCarrier" runat="server" TabIndex="21" SkinID="TextBoxNormalBig"
                                            MaxLength="40"></asp:TextBox>
                                    </td>
                                </tr>
                                <tr>
                                    <td class="labelColSpan2">
                                        <asp:Label ID="lblRoadPermitApplicable" runat="server" SkinID="LabelNormal" Text="Road Permit Applicable"></asp:Label>
                                    </td>
                                    <td class="inputcontrolsColSpan2">
                                        <asp:CheckBox ID="chkRoadPermitApplicable" TabIndex="22" runat="server" />
                                    </td>
                                    <td class="labelColSpan2">
                                        <asp:Label ID="lblStatus" runat="server" SkinID="LabelNormal" Text="Status"></asp:Label>
                                        <span class="asterix">*
                                    </td>
                                    <td class="inputcontrolsColSpan2">
                                        <asp:DropDownList ID="ddlStatus" runat="server" TabIndex="23" SkinID="DropDownListNormalBig">
                                            <asp:ListItem Value="0" Text="Active"></asp:ListItem>
                                            <asp:ListItem Value="1" Text="InActive"></asp:ListItem>
                                        </asp:DropDownList>
                                    </td>
                                </tr>
                                <tr>
                                    <td class="labelColSpan2">
                                        <asp:Label ID="lblCloseDate" runat="server" SkinID="LabelNormal" Text="Close Date"></asp:Label>
                                    </td>
                                    <td class="inputcontrolsColSpan2">
                                        <asp:TextBox ID="txtCloseDate" runat="server" onmousedown="TriggerCalender('BranchFormView_ImgCloseDate');"
                                            contentEditable="false" SkinID="TextBoxNormalBig"></asp:TextBox>
                                        <asp:ImageButton ID="ImgCloseDate" runat="server" TabIndex="24" alt="Calendar" Height="18"
                                            ImageUrl="~/Images/Calendar.png" Width="18" />
                                        <ajaxToolkit:CalendarExtender ID="CalCloseDate" runat="server" EnableViewState="true"
                                            Format="dd/MM/yyyy" PopupButtonID="ImgCloseDate" PopupPosition="BottomLeft" TargetControlID="txtCloseDate">
                                        </ajaxToolkit:CalendarExtender>
                                    </td>
                                    <td class="labelColSpan2">
                                        <asp:Label ID="lblOSLocking" runat="server" SkinID="LabelNormal" Text="OS Locking"></asp:Label>
                                    </td>
                                    <%--<td class="inputcontrolsColSpan2">
                                        <asp:TextBox ID="txtOSLocking" runat="server" TabIndex="25"  
                                            SkinID="TextBoxNormalBig" MaxLength="1"></asp:TextBox>
                                    </td>--%>
                                    <td class="inputcontrols">
                                        <asp:DropDownList ID="ddlOSLocking" runat="server" SkinID="DropDownListNormalBig"
                                            TabIndex="25">
                                        </asp:DropDownList>
                                    </td>
                                </tr>
                                <tr>
                                    <td class="labelColSpan2">
                                        <asp:Label ID="lblO_SCalculationDays" runat="server" SkinID="LabelNormal" Text="O/S Calculation Days"></asp:Label>
                                    </td>
                                    <td class="inputcontrolsColSpan2">
                                        <asp:TextBox ID="txtO_SCalculationDays" TabIndex="26" runat="server" SkinID="TextBoxNormalBig"
                                            MaxLength="2"></asp:TextBox>
                                    </td>
                                    <td class="labelColSpan2">
                                    </td>
                                    <td class="inputcontrolsColSpan2">
                                    </td>
                                </tr>
                            </table>
                            <div class="subFormTitle subFormTitleExtender350">
                                MINIMUM PARAMETER FOR STOCK TRANSFER
                            </div>
                            <table class="subFormTable">
                                <tr>
                                    <td class="labelColSpan2">
                                        <asp:Label ID="lblStockAging" runat="server" SkinID="LabelNormal" Text="Stock Aging"></asp:Label>
                                        <span class="asterix">*</span>
                                    </td>
                                    </td>
                                    <td class="inputcontrolsColSpan2">
                                        <asp:TextBox ID="txtStockAging" runat="server" TabIndex="27" SkinID="TextBoxNormalBig"
                                            MaxLength="3"></asp:TextBox>
                                    </td>
                                    <td class="labelColSpan2">
                                        <asp:Label ID="lblStockValue" runat="server" SkinID="LabelNormal" Text="Stock Value"></asp:Label>
                                        <span class="asterix">*
                                    </td>
                                    <td class="inputcontrolsColSpan2">
                                        <asp:TextBox ID="txtStockValue" runat="server" TabIndex="28" SkinID="TextBoxNormalBig"
                                            MaxLength="8"></asp:TextBox>
                                    </td>
                                </tr>
                            </table>
                            <div class="subFormTitle subFormTitleExtender350">
                                SALES TAX
                            </div>
                            <table class="subFormTable">
                                <tr>
                                    <td class="labelColSpan2">
                                        <asp:Label ID="lblLocalST_RCNo" runat="server" SkinID="LabelNormal" Text="Local ST RC no"></asp:Label>
                                        <span class="asterix">*</span>
                                    </td>
                                    </td>
                                    <td class="inputcontrolsColSpan2">
                                        <asp:TextBox ID="txtLocalST_RCNo" runat="server" TabIndex="29" SkinID="TextBoxNormalBig"
                                            MaxLength="20"></asp:TextBox>
                                    </td>
                                    <td class="labelColSpan2">
                                        <asp:Label ID="lblCentralSalesTaxNumber" runat="server" SkinID="LabelNormal" Text="Central Sales Tax Number"></asp:Label>
                                    </td>
                                    <td class="inputcontrolsColSpan2">
                                        <asp:TextBox ID="txtCentralSalesTaxNumber" TabIndex="30" runat="server" SkinID="TextBoxNormalBig"
                                            MaxLength="12"></asp:TextBox>
                                    </td>
                                </tr>
                                <tr>
                                    <td class="labelColSpan2">
                                        <asp:Label ID="lblCESSNumber" runat="server" SkinID="LabelNormal" Text="CESS Number"></asp:Label>
                                    </td>
                                    <td class="inputcontrolsColSpan2">
                                        <asp:TextBox ID="txtCESSNumber" runat="server" TabIndex="31" SkinID="TextBoxNormalBig"
                                            MaxLength="20"></asp:TextBox>
                                    </td>
                                    <td class="labelColSpan2">
                                        <asp:Label ID="lblTINNumber" runat="server" SkinID="LabelNormal" Text="TIN Number"></asp:Label>
                                    </td>
                                    <td class="inputcontrolsColSpan2">
                                        <asp:TextBox ID="txtTINNumber" runat="server" TabIndex="32" SkinID="TextBoxNormalBig"
                                            MaxLength="12"></asp:TextBox>
                                    </td>
                                </tr>
                            </table>
                            <asp:RequiredFieldValidator ID="ReqtxtBranch" ValidationGroup="btnUpdate" ControlToValidate="txtBranchCode"
                                Display="None" SetFocusOnError="true" runat="server" ErrorMessage="Please Select Branch."></asp:RequiredFieldValidator>
                            <ajaxToolkit:ValidatorCalloutExtender ID="ValidatorCalloutExtender1" TargetControlID="ReqtxtBranch"
                                PopupPosition="Right" runat="server">
                            </ajaxToolkit:ValidatorCalloutExtender>
                            <asp:CustomValidator ID="ValtxtBranchCode" runat="server" Display="None" ValidationGroup="btnUpdate"
                                ControlToValidate="txtBranchCode" ClientValidationFunction="validateFields"
                                ErrorMessage="First character should be alphabet or number"></asp:CustomValidator>
                            <ajaxToolkit:ValidatorCalloutExtender ID="ValidatorCalloutExtender32" TargetControlID="ValtxtBranchCode"
                                PopupPosition="Left" runat="server">
                            </ajaxToolkit:ValidatorCalloutExtender>
                            <asp:CustomValidator ID="CusttxtBranchCode" runat="server" Display="None" ValidationGroup="btnUpdate"
                                ControlToValidate="txtBranchCode" ClientValidationFunction="valBranchCode"
                                ErrorMessage="Code should be 3 digit"></asp:CustomValidator>
                            <ajaxToolkit:ValidatorCalloutExtender ID="ValidatorCalloutExtender33" TargetControlID="CusttxtBranchCode"
                                PopupPosition="Left" runat="server">
                            </ajaxToolkit:ValidatorCalloutExtender>
                            <asp:RequiredFieldValidator ID="ReqBranchName" SetFocusOnError="true" ValidationGroup="btnUpdate"
                                ControlToValidate="txtBranchName" runat="server" ErrorMessage="Branch Name is required."
                                Display="None"></asp:RequiredFieldValidator>
                            <ajaxToolkit:ValidatorCalloutExtender ID="ValidatorCalloutExtender2" TargetControlID="ReqBranchName"
                                PopupPosition="Left" runat="server">
                            </ajaxToolkit:ValidatorCalloutExtender>
                            <asp:CustomValidator ID="ValBranchName" runat="server" Display="None" ValidationGroup="btnUpdate"
                                ControlToValidate="txtBranchName" ClientValidationFunction="validateFields"
                                ErrorMessage="First character should be alphabet or number"></asp:CustomValidator>
                            <ajaxToolkit:ValidatorCalloutExtender ID="ValidatorCalloutExtender18" TargetControlID="ValBranchName"
                                PopupPosition="Left" runat="server">
                            </ajaxToolkit:ValidatorCalloutExtender>
                            <asp:RequiredFieldValidator ID="ReqAddress" SetFocusOnError="true" ValidationGroup="btnUpdate"
                                ControlToValidate="txtAddress" runat="server" ErrorMessage="Address is required."
                                Display="None"></asp:RequiredFieldValidator>
                            <ajaxToolkit:ValidatorCalloutExtender ID="ValidatorCalloutExtender3" TargetControlID="ReqAddress"
                                PopupPosition="Right" runat="server">
                            </ajaxToolkit:ValidatorCalloutExtender>
                            <asp:CustomValidator ID="ValAddress" runat="server" Display="None" ValidationGroup="btnUpdate"
                                ControlToValidate="txtAddress" ClientValidationFunction="validateFields"
                                ErrorMessage="First character should be alphabet or number"></asp:CustomValidator>
                            <ajaxToolkit:ValidatorCalloutExtender ID="ValidatorCalloutExtender19" TargetControlID="ValAddress"
                                PopupPosition="Left" runat="server">
                            </ajaxToolkit:ValidatorCalloutExtender>
                            <asp:CustomValidator ID="ValFax" runat="server" Display="None" ValidationGroup="btnUpdate"
                                ControlToValidate="txtFax" ClientValidationFunction="validateFields"
                                ErrorMessage="First character should be alphabet or number"></asp:CustomValidator>
                            <ajaxToolkit:ValidatorCalloutExtender ID="ValidatorCalloutExtender22" TargetControlID="ValFax"
                                PopupPosition="Left" runat="server">
                            </ajaxToolkit:ValidatorCalloutExtender>
                            <asp:CustomValidator ID="ValTelex" runat="server" Display="None" ValidationGroup="btnUpdate"
                                ControlToValidate="txtTelex" ClientValidationFunction="validateFields"
                                ErrorMessage="First character should be alphabet or number"></asp:CustomValidator>
                            <ajaxToolkit:ValidatorCalloutExtender ID="ValidatorCalloutExtender23" TargetControlID="ValTelex"
                                PopupPosition="Left" runat="server">
                            </ajaxToolkit:ValidatorCalloutExtender>
                            <asp:CustomValidator ID="ValEmailID" runat="server" Display="None" ValidationGroup="btnUpdate"
                                ControlToValidate="txtEmailid" ClientValidationFunction="validateFields"
                                ErrorMessage="First character should be alphabet"></asp:CustomValidator>
                            <ajaxToolkit:ValidatorCalloutExtender ID="ValidatorCalloutExtender28" TargetControlID="ValEmailID"
                                PopupPosition="Left" runat="server">
                            </ajaxToolkit:ValidatorCalloutExtender>
                            <asp:RegularExpressionValidator ID="regValidator" runat="server" Display="None" ErrorMessage="Email is not vaild" ControlToValidate="txtEmailid" 
                                ValidationExpression="^(([a-zA-Z0-9_\-\.]+)@([a-zA-Z0-9_\-\.]+)\.([a-zA-Z]{2,5}){1,25})+([;.](([a-zA-Z0-9_\-\.]+)@([a-zA-Z0-9_\-\.]+)\.([a-zA-Z]{2,5}){1,25})+)*$" 
                                ValidationGroup="btnUpdate"></asp:RegularExpressionValidator>
                                <ajaxToolkit:ValidatorCalloutExtender ID="ValidatorCalloutExtender29" TargetControlID="regValidator"
                                PopupPosition="Left" runat="server">
                            </ajaxToolkit:ValidatorCalloutExtender>
                            <asp:RequiredFieldValidator ID="ReqClassification" ValidationGroup="btnUpdate" ControlToValidate="ddlClassification"
                                Display="None" InitialValue="0" runat="server" ErrorMessage="Please Select Classification."></asp:RequiredFieldValidator>
                            <ajaxToolkit:ValidatorCalloutExtender ID="ValidatorCalloutExtender4" TargetControlID="ReqClassification"
                                PopupPosition="Right" runat="server">
                            </ajaxToolkit:ValidatorCalloutExtender>
                            <asp:RequiredFieldValidator ID="ReqReportingState" ValidationGroup="btnUpdate" ControlToValidate="ddlReportingSate"
                                Display="None" InitialValue="0" runat="server" ErrorMessage="Please Select Reporting State."></asp:RequiredFieldValidator>
                            <ajaxToolkit:ValidatorCalloutExtender ID="ValidatorCalloutExtender5" TargetControlID="ReqReportingState"
                                PopupPosition="Left" runat="server">
                            </ajaxToolkit:ValidatorCalloutExtender>
                            <asp:RequiredFieldValidator ID="ReqddlState" ValidationGroup="btnUpdate" ControlToValidate="ddlState"
                                Display="None" InitialValue="0" runat="server" ErrorMessage="Please Select State."></asp:RequiredFieldValidator>
                            <ajaxToolkit:ValidatorCalloutExtender ID="ValidatorCalloutExtender6" TargetControlID="ReqddlState"
                                PopupPosition="Left" runat="server">
                            </ajaxToolkit:ValidatorCalloutExtender>
                            <%--<asp:RegularExpressionValidator ID="ValPhone" runat="server" Display="None" 
                                ErrorMessage="Phone number must be numbers only" ControlToValidate="txtPhone" 
                                ValidationExpression="^\d+$" ValidationGroup="btnUpdate"></asp:RegularExpressionValidator>
                            <ajaxToolkit:ValidatorCalloutExtender ID="ValidatorCalloutExtender31" TargetControlID="ValPhone"
                                PopupPosition="Left" runat="server">
                            </ajaxToolkit:ValidatorCalloutExtender>--%>
                            <asp:CustomValidator ID="ValBranchManager" runat="server" Display="None" ValidationGroup="btnUpdate"
                                ControlToValidate="txtBranchManager" ClientValidationFunction="validateFields"
                                ErrorMessage="First character should be alphabet or number"></asp:CustomValidator>
                            <ajaxToolkit:ValidatorCalloutExtender ID="ValidatorCalloutExtender24" TargetControlID="ValBranchManager"
                                PopupPosition="Left" runat="server">
                            </ajaxToolkit:ValidatorCalloutExtender>
                            <asp:CustomValidator ID="ValAccountManager" runat="server" Display="None" ValidationGroup="btnUpdate"
                                ControlToValidate="txtlblAccountsManager_Officer" ClientValidationFunction="validateFields"
                                ErrorMessage="First character should be alphabet or number"></asp:CustomValidator>
                            <ajaxToolkit:ValidatorCalloutExtender ID="ValidatorCalloutExtender25" TargetControlID="ValAccountManager"
                                PopupPosition="Left" runat="server">
                            </ajaxToolkit:ValidatorCalloutExtender>
                            <asp:CustomValidator ID="ValED" runat="server" Display="None" ValidationGroup="btnUpdate"
                                ControlToValidate="txtEDPin_charge" ClientValidationFunction="validateFields"
                                ErrorMessage="First character should be alphabet or number"></asp:CustomValidator>
                            <ajaxToolkit:ValidatorCalloutExtender ID="ValidatorCalloutExtender26" TargetControlID="ValED"
                                PopupPosition="Left" runat="server">
                            </ajaxToolkit:ValidatorCalloutExtender>
                            <asp:RequiredFieldValidator ID="ReqOpeningDate" SetFocusOnError="true" ValidationGroup="btnUpdate"
                                ControlToValidate="txtOpeningDate" runat="server" ErrorMessage="Opening Date is required."
                                Display="None"></asp:RequiredFieldValidator>
                            <ajaxToolkit:ValidatorCalloutExtender ID="ValidatorCalloutExtender7" TargetControlID="ReqOpeningDate"
                                PopupPosition="Left" runat="server">
                            </ajaxToolkit:ValidatorCalloutExtender>
                            <asp:CustomValidator ID="ValDestination" runat="server" Display="None" ValidationGroup="btnUpdate"
                                ControlToValidate="txtDestination" ClientValidationFunction="validateFields"
                                ErrorMessage="First character should be alphabet or number"></asp:CustomValidator>
                            <ajaxToolkit:ValidatorCalloutExtender ID="ValidatorCalloutExtender27" TargetControlID="ValDestination"
                                PopupPosition="Left" runat="server">
                            </ajaxToolkit:ValidatorCalloutExtender>
                            <asp:CustomValidator ID="ValPreferred" runat="server" Display="None" ValidationGroup="btnUpdate"
                                ControlToValidate="txtPreferredCarrier" ClientValidationFunction="validateFields"
                                ErrorMessage="First character should be alphabet or number"></asp:CustomValidator>
                            <ajaxToolkit:ValidatorCalloutExtender ID="ValidatorCalloutExtender30" TargetControlID="ValPreferred"
                                PopupPosition="Left" runat="server">
                            </ajaxToolkit:ValidatorCalloutExtender>
                            <asp:RequiredFieldValidator ID="ReqStatusValidator1" ValidationGroup="btnUpdate"
                                ControlToValidate="ddlStatus" Display="None" runat="server" ErrorMessage="Please Select Status."></asp:RequiredFieldValidator>
                            <ajaxToolkit:ValidatorCalloutExtender ID="ValidatorCalloutExtender8" TargetControlID="ReqStatusValidator1"
                                PopupPosition="Left" runat="server">
                            </ajaxToolkit:ValidatorCalloutExtender>
                            <asp:RequiredFieldValidator ID="ReqStockAging" ValidationGroup="btnUpdate" ControlToValidate="txtStockAging"
                                Display="None" runat="server" ErrorMessage="Please Enter Stock Aging."></asp:RequiredFieldValidator>
                            <ajaxToolkit:ValidatorCalloutExtender ID="ValidatorCalloutExtender9" TargetControlID="ReqStockAging"
                                PopupPosition="Right" runat="server">
                            </ajaxToolkit:ValidatorCalloutExtender>
                            <asp:RequiredFieldValidator ID="ReqStockvalue" ValidationGroup="btnUpdate" ControlToValidate="txtStockValue"
                                Display="None" runat="server" ErrorMessage="Please Enter Stock Value."></asp:RequiredFieldValidator>
                            <ajaxToolkit:ValidatorCalloutExtender ID="ValidatorCalloutExtender10" TargetControlID="ReqStockvalue"
                                PopupPosition="Left" runat="server">
                            </ajaxToolkit:ValidatorCalloutExtender>
                            <asp:RegularExpressionValidator ID="ValStockValue" runat="server" Display="None" 
                                ErrorMessage="Stock value should be numbers only" ControlToValidate="txtStockValue" 
                                ValidationExpression="^\d+$" ValidationGroup="btnUpdate"></asp:RegularExpressionValidator>
                            <ajaxToolkit:ValidatorCalloutExtender ID="ValidatorCalloutExtender21" TargetControlID="ValStockValue"
                                PopupPosition="Left" runat="server">
                            </ajaxToolkit:ValidatorCalloutExtender>
                            <!---
                           Custom validation
                           -->
                            <asp:CompareValidator ID="CmpAreaSqFeetvalidator" runat="server" SetFocusOnError="true"
                                ControlToValidate="txtArea_sq_feet" Operator="DataTypeCheck" ValidationGroup="btnUpdate"
                                Type="Double" Display="None" ErrorMessage="Area Sq feet allows only numeric digits" />
                            <ajaxToolkit:ValidatorCalloutExtender ID="ValidatorCalloutExtender11" TargetControlID="CmpAreaSqFeetvalidator"
                                PopupPosition="Right" runat="server">
                            </ajaxToolkit:ValidatorCalloutExtender>
                            <asp:CompareValidator ID="ComMonthlyRent" runat="server" SetFocusOnError="true" ControlToValidate="txtMonthlyRent"
                                Operator="DataTypeCheck" ValidationGroup="btnUpdate" Type="Double" Display="None"
                                ErrorMessage="Monthly Rent allows only numeric digits" />
                            <ajaxToolkit:ValidatorCalloutExtender ID="ValidatorCalloutExtender12" TargetControlID="ComMonthlyRent"
                                PopupPosition="Left" runat="server">
                            </ajaxToolkit:ValidatorCalloutExtender>
                            <asp:CompareValidator ID="ComRentalAdvance" runat="server" SetFocusOnError="true"
                                ControlToValidate="txtRentalAdvance" Operator="DataTypeCheck" ValidationGroup="btnUpdate"
                                Type="Double" Display="None" ErrorMessage="Rental Advance allows only numeric digits" />
                            <ajaxToolkit:ValidatorCalloutExtender ID="ValidatorCalloutExtender13" TargetControlID="ComRentalAdvance"
                                PopupPosition="Right" runat="server">
                            </ajaxToolkit:ValidatorCalloutExtender>
                            <asp:CompareValidator ID="ComNoticePeriod" runat="server" SetFocusOnError="true"
                                ControlToValidate="txtNoticePeriod" Operator="DataTypeCheck" ValidationGroup="btnUpdate"
                                Type="Integer" Display="None" ErrorMessage="Notice Period allows only numeric digits" />
                            <ajaxToolkit:ValidatorCalloutExtender ID="ValidatorCalloutExtender14" TargetControlID="ComNoticePeriod"
                                PopupPosition="Left" runat="server">
                            </ajaxToolkit:ValidatorCalloutExtender>
                            <asp:CompareValidator ID="ComStockAging" runat="server" SetFocusOnError="true" ControlToValidate="txtStockAging"
                                Operator="DataTypeCheck" ValidationGroup="btnUpdate" Type="Integer" Display="None"
                                ErrorMessage="Stock Aging allows only Intger digits" />
                            <ajaxToolkit:ValidatorCalloutExtender ID="ValidatorCalloutExtender15" TargetControlID="ComStockAging"
                                PopupPosition="Right" runat="server">
                            </ajaxToolkit:ValidatorCalloutExtender>
                            <asp:CompareValidator ID="ComStockValue" runat="server" SetFocusOnError="true" ControlToValidate="txtStockValue"
                                Operator="DataTypeCheck" ValidationGroup="btnUpdate" Type="Double" Display="None"
                                ErrorMessage="Stock value allows only double digits" />
                            <ajaxToolkit:ValidatorCalloutExtender ID="ValidatorCalloutExtender16" TargetControlID="ComStockValue"
                                PopupPosition="Left" runat="server">
                            </ajaxToolkit:ValidatorCalloutExtender>
                            <asp:CompareValidator ID="ComO_SCalculationDays" runat="server" SetFocusOnError="true"
                                ControlToValidate="txtO_SCalculationDays" Operator="DataTypeCheck" ValidationGroup="btnUpdate"
                                Type="Integer" Display="None" ErrorMessage="OS Calculation Days allows only Intger digits" />
                            <ajaxToolkit:ValidatorCalloutExtender ID="ValidatorCalloutExtender17" TargetControlID="ComO_SCalculationDays"
                                PopupPosition="Right" runat="server">
                            </ajaxToolkit:ValidatorCalloutExtender>
                        </div>
                    </InsertItemTemplate>
                    <EditItemTemplate>
                        <div>
                            <div class="subFormTitle subFormTitleExtender350">
                                BRANCH</div>
                            <table class="subFormTable">
                                <tr>
                                    <td class="labelColSpan2">
                                        <asp:Label ID="lblCode" runat="server" SkinID="LabelNormal" Text="Code"></asp:Label>
                                        <span class="asterix">*</span>
                                    </td>
                                    <td class="inputcontrolsColSpan2">
                                        <asp:DropDownList ID="ddlBranch" AutoPostBack="true" TabIndex="0" runat="server"
                                            SkinID="DropDownListNormalBig" OnSelectedIndexChanged="ddlBranch_SelectedIndexChanged">
                                        </asp:DropDownList>
                                    </td>
                                    <td class="labelColSpan2">
                                        <asp:Label ID="lblName" runat="server" SkinID="LabelNormal" Text="Name"></asp:Label>
                                    </td>
                                    <td class="inputcontrolsColSpan2">
                                        <asp:TextBox ID="txtBranchName" Text='<%# Bind("Branch_Name") %>' runat="server"
                                            TabIndex="1" SkinID="TextBoxNormalBig" MaxLength="40"></asp:TextBox>
                                    </td>
                                </tr>
                            </table>
                            <div class="subFormTitle subFormTitleExtender350">
                                COMMUNICATION DETAIL</div>
                            <table class="subFormTable">
                                <tr>
                                    <td class="labelColSpan2">
                                        <asp:Label ID="lblAddress" runat="server" SkinID="LabelNormal" Text="Address"></asp:Label>
                                        <span class="asterix">*</span>
                                    </td>
                                    <td class="inputcontrolsColSpan2">
                                        <asp:TextBox ID="txtAddress" runat="server" Text='<%# Bind("Address") %>' TabIndex="2"
                                            TextMode="MultiLine" SkinID="TextBoxMultilineDisable" MaxLength="200"></asp:TextBox>
                                    </td>
                                    <td class="labelColSpan2">
                                        <asp:Label ID="lblState" runat="server" SkinID="LabelNormal" Text="State"></asp:Label>
                                    </td>
                                    <td class="inputcontrolsColSpan2">
                                        <asp:DropDownList ID="ddlState" TabIndex="3" runat="server" SkinID="DropDownListNormalBig">
                                        </asp:DropDownList>
                                    </td>
                                </tr>
                                <tr>
                                    <td class="labelColSpan2">
                                        <asp:Label ID="lblPhone" runat="server" SkinID="LabelNormal" Text="Phone"></asp:Label>
                                    </td>
                                    <td class="inputcontrolsColSpan2">
                                        <asp:TextBox ID="txtPhone" runat="server" Text='<%# Bind("Phone") %>' TabIndex="4"
                                            SkinID="TextBoxNormalBig" MaxLength="40"></asp:TextBox>
                                    </td>
                                    <td class="labelColSpan2">
                                        <asp:Label ID="lblFax" runat="server" SkinID="LabelNormal" Text="Fax"></asp:Label>
                                    </td>
                                    <td class="inputcontrolsColSpan2">
                                        <asp:TextBox ID="txtFax" runat="server" Text='<%# Bind("Fax") %>' TabIndex="5" SkinID="TextBoxNormalBig"
                                            MaxLength="40"></asp:TextBox>
                                    </td>
                                </tr>
                                <tr>
                                    <td class="labelColSpan2">
                                        <asp:Label ID="lblTelex" runat="server" SkinID="LabelNormal" Text="Telex"></asp:Label>
                                    </td>
                                    <td class="inputcontrolsColSpan2">
                                        <asp:TextBox ID="txtTelex" runat="server" Text='<%# Bind("Telex") %>' TabIndex="6"
                                            SkinID="TextBoxNormalBig" MaxLength="40"></asp:TextBox>
                                    </td>
                                    <td class="labelColSpan2">
                                        <asp:Label ID="lblEmailid" runat="server" SkinID="LabelNormal" Text="Email id"></asp:Label>
                                    </td>
                                    <td class="inputcontrolsColSpan2">
                                        <asp:TextBox ID="txtEmailid" runat="server" Text='<%# Bind("Email") %>' TabIndex="7"
                                            SkinID="TextBoxNormalBig" MaxLength="60"></asp:TextBox>
                                    </td>
                                </tr>
                                <tr>
                                    <td class="labelColSpan2">
                                        <asp:Label ID="lblClassification" runat="server" SkinID="LabelNormal" Text="Classification"></asp:Label>
                                        <span class="asterix">*</span>
                                    </td>
                                    <td class="inputcontrolsColSpan2">
                                        <asp:DropDownList ID="ddlClassification" TabIndex="8" runat="server" SkinID="DropDownListNormalBig">
                                        </asp:DropDownList>
                                    </td>
                                    <td class="labelColSpan2">
                                        <asp:Label ID="lblReportingState" runat="server" SkinID="LabelNormal" Text="Reporting State"></asp:Label>
                                        <span class="asterix">*</span>
                                    </td>
                                    <td class="inputcontrolsColSpan2">
                                        <asp:DropDownList ID="ddlReportingSate" TabIndex="9" runat="server" SkinID="DropDownListNormalBig">
                                        </asp:DropDownList>
                                    </td>
                                </tr>
                            </table>
                            <div class="subFormTitle subFormTitleExtender350">
                                BRANCH INFORMATION</div>
                            <table class="subFormTable">
                                <tr>
                                    <td class="labelColSpan2">
                                        <asp:Label ID="lblBranchManager" runat="server" SkinID="LabelNormal" Text="Branch Manager"></asp:Label>
                                    </td>
                                    <td class="inputcontrolsColSpan2">
                                        <asp:TextBox ID="txtBranchManager" Text='<%# Bind("Branch_Manager") %>' runat="server"
                                            TabIndex="10" SkinID="TextBoxNormalBig" MaxLength="40"></asp:TextBox>
                                    </td>
                                    <td class="labelColSpan2">
                                        <asp:Label ID="lblAccountsManager_Officer" runat="server" SkinID="LabelNormal" Text="Accounts Manager/Officer"></asp:Label>
                                    </td>
                                    <td class="inputcontrolsColSpan2">
                                        <asp:TextBox ID="txtlblAccountsManager_Officer" Text='<%# Bind("Branch_Accountant") %>'
                                            TabIndex="11" runat="server" SkinID="TextBoxNormalBig" MaxLength="40"></asp:TextBox>
                                    </td>
                                </tr>
                                <tr>
                                    <td class="labelColSpan2">
                                        <asp:Label ID="lblEDPin_charge" runat="server" SkinID="LabelNormal" Text="EDP in-charge"></asp:Label>
                                    </td>
                                    <td class="inputcontrolsColSpan2">
                                        <asp:TextBox ID="txtEDPin_charge" Text='<%# Bind("EDP_In_charge") %>' runat="server"
                                            TabIndex="12" SkinID="TextBoxNormalBig" MaxLength="40"></asp:TextBox>
                                    </td>
                                    <td class="labelColSpan2">
                                        <asp:Label ID="lblOpeningDate" runat="server" SkinID="LabelNormal" Text="Opening Date"></asp:Label>
                                        <span class="asterix">*</span>
                                    </td>
                                    <td class="inputcontrolsColSpan2">
                                        <asp:TextBox ID="txtOpeningDate" runat="server" onmousedown="TriggerCalender('BranchFormView_ImgOpeningDate');"
                                            Text='<%# Bind("Opening_Date") %>' TabIndex="13" contentEditable="false" SkinID="TextBoxNormalBig"></asp:TextBox>
                                        <asp:ImageButton ID="ImgOpeningDate" runat="server" alt="Calendar" Height="18" ImageUrl="~/Images/Calendar.png"
                                            TabIndex="10" Width="18" />
                                        <ajaxToolkit:CalendarExtender ID="OpeningDateExtender" runat="server" EnableViewState="true"
                                            Format="dd/MM/yyyy" PopupButtonID="ImgOpeningDate" PopupPosition="BottomLeft"
                                            TargetControlID="txtOpeningDate">
                                        </ajaxToolkit:CalendarExtender>
                                    </td>
                                </tr>
                                <tr>
                                    <td class="labelColSpan2">
                                        <asp:Label ID="lblArea_sq_feet" runat="server" SkinID="LabelNormal" Text="Area (sq.feet)"></asp:Label>
                                    </td>
                                    <td class="inputcontrolsColSpan2">
                                        <asp:TextBox ID="txtArea_sq_feet" Text='<%# Bind("Area_in_Square_Feet") %>' runat="server"
                                            TabIndex="14" SkinID="TextBoxNormalBig" MaxLength="9"></asp:TextBox>
                                    </td>
                                    <td class="labelColSpan2">
                                        <asp:Label ID="lblMonthlyRent" runat="server" SkinID="LabelNormal" Text="Monthly Rent"></asp:Label>
                                    </td>
                                    <td class="inputcontrolsColSpan2">
                                        <asp:TextBox ID="txtMonthlyRent" Text='<%# Bind("Monthly_Rent") %>' runat="server"
                                            TabIndex="15" SkinID="TextBoxNormalBig" MaxLength="8"></asp:TextBox>
                                    </td>
                                </tr>
                                <tr>
                                    <td class="labelColSpan2">
                                        <asp:Label ID="lblRentalAdvance" runat="server" SkinID="LabelNormal" Text="Rental Advance"></asp:Label>
                                    </td>
                                    <td class="inputcontrolsColSpan2">
                                        <asp:TextBox ID="txtRentalAdvance" Text='<%# Bind("Rental_Advance") %>' runat="server"
                                            TabIndex="16" SkinID="TextBoxNormalBig" MaxLength="8"></asp:TextBox>
                                    </td>
                                    <td class="labelColSpan2">
                                        <asp:Label ID="lblContractStartDate" runat="server" SkinID="LabelNormal" Text="Contract Start Date"></asp:Label>
                                    </td>
                                    <td class="inputcontrolsColSpan2">
                                        <asp:TextBox ID="txtContractStartDate" onmousedown="TriggerCalender('BranchFormView_ImgContractStartDate');"
                                            runat="server" Text='<%# Bind("Rental_Contract_Start_Date") %>' contentEditable="false"
                                            SkinID="TextBoxNormalBig"></asp:TextBox>
                                        <asp:ImageButton ID="ImgContractStartDate" runat="server" TabIndex="17" alt="Calendar"
                                            Height="18" ImageUrl="~/Images/Calendar.png" Width="18" />
                                        <ajaxToolkit:CalendarExtender ID="ContractStartDateExtender" runat="server" EnableViewState="true"
                                            Format="dd/MM/yyyy" PopupButtonID="ImgContractStartDate" PopupPosition="BottomLeft"
                                            TargetControlID="txtContractStartDate">
                                        </ajaxToolkit:CalendarExtender>
                                    </td>
                                </tr>
                                <tr>
                                    <td class="labelColSpan2">
                                        <asp:Label ID="lblContractCompletionDate" runat="server" SkinID="LabelNormal" Text="Contract Completion Date"></asp:Label>
                                    </td>
                                    <td class="inputcontrolsColSpan2">
                                        <asp:TextBox ID="txtContractCompletionDate" onmousedown="TriggerCalender('BranchFormView_ImgContractCompletionDate');"
                                            runat="server" Text='<%# Bind("Rental_Contract_End_Date") %>' contentEditable="false"
                                            SkinID="TextBoxNormalBig"></asp:TextBox>
                                        <asp:ImageButton ID="ImgContractCompletionDate" TabIndex="18" runat="server" alt="Calendar"
                                            Height="18" ImageUrl="~/Images/Calendar.png" Width="18" />
                                        <ajaxToolkit:CalendarExtender ID="ContractCompletionDateExtender1" runat="server"
                                            EnableViewState="true" Format="dd/MM/yyyy" PopupButtonID="ImgContractCompletionDate"
                                            PopupPosition="BottomLeft" TargetControlID="txtContractCompletionDate">
                                        </ajaxToolkit:CalendarExtender>
                                    </td>
                                    <td class="labelColSpan2">
                                        <asp:Label ID="lblNoticePeriod" runat="server" SkinID="LabelNormal" Text="Notice Period"></asp:Label>
                                    </td>
                                    <td class="inputcontrolsColSpan2">
                                        <asp:TextBox ID="txtNoticePeriod" Text='<%# Bind("Termination_Notice_Period") %>'
                                            runat="server" TabIndex="19" SkinID="TextBoxNormalBig" MaxLength="2"></asp:TextBox>
                                    </td>
                                </tr>
                                <tr>
                                    <td class="labelColSpan2">
                                        <asp:Label ID="lblDestination" runat="server" SkinID="LabelNormal" Text="Destination"></asp:Label>
                                    </td>
                                    <td class="inputcontrolsColSpan2">
                                        <asp:TextBox ID="txtDestination" Text='<%# Bind("Branch_Destination") %>' runat="server"
                                            TabIndex="20" SkinID="TextBoxNormalBig" MaxLength="40"></asp:TextBox>
                                    </td>
                                    <td class="labelColSpan2">
                                        <asp:Label ID="lblPreferredCarrier" runat="server" SkinID="LabelNormal" Text="Preferred Carrier"></asp:Label>
                                    </td>
                                    <td class="inputcontrolsColSpan2">
                                        <asp:TextBox ID="txtPreferredCarrier" Text='<%# Bind("Branch_Carrier") %>' runat="server"
                                            TabIndex="21" SkinID="TextBoxNormalBig" MaxLength="40"></asp:TextBox>
                                    </td>
                                </tr>
                                <tr>
                                    <td class="labelColSpan2">
                                        <asp:Label ID="lblRoadPermitApplicable" runat="server" SkinID="LabelNormal" Text="Road Permit Applicable"></asp:Label>
                                    </td>
                                    <td class="inputcontrolsColSpan2">
                                        <asp:CheckBox ID="chkRoadPermitApplicable" Checked='<%# Bind("Road_Permit_Indicator") %>'
                                            TabIndex="22" runat="server" />
                                    </td>
                                    <td class="labelColSpan2">
                                        <asp:Label ID="lblStatus" runat="server" SkinID="LabelNormal" Text="Status"></asp:Label>
                                        <span class="asterix">*
                                    </td>
                                    <td class="inputcontrolsColSpan2">
                                        <asp:DropDownList ID="ddlStatus" runat="server" TabIndex="23" SkinID="DropDownListNormalBig">
                                            <asp:ListItem Value="0" Text="Active"></asp:ListItem>
                                            <asp:ListItem Value="1" Text="InActive"></asp:ListItem>
                                        </asp:DropDownList>
                                    </td>
                                </tr>
                                <tr>
                                    <td class="labelColSpan2">
                                        <asp:Label ID="lblCloseDate" runat="server" SkinID="LabelNormal" Text="Close Date"></asp:Label>
                                    </td>
                                    <td class="inputcontrolsColSpan2">
                                        <asp:TextBox ID="txtCloseDate" runat="server" onmousedown="TriggerCalender('BranchFormView_ImgCloseDate');"
                                            Text='<%# Bind("End_Date") %>' contentEditable="false" SkinID="TextBoxNormalBig"></asp:TextBox>
                                        <asp:ImageButton ID="ImgCloseDate" runat="server" TabIndex="24" alt="Calendar" Height="18"
                                            ImageUrl="~/Images/Calendar.png" Width="18" />
                                        <ajaxToolkit:CalendarExtender ID="CalCloseDate" runat="server" EnableViewState="true"
                                            Format="dd/MM/yyyy" PopupButtonID="ImgCloseDate" PopupPosition="BottomLeft" TargetControlID="txtCloseDate">
                                        </ajaxToolkit:CalendarExtender>
                                    </td>
                                    <td class="labelColSpan2">
                                        <asp:Label ID="lblOSLocking" runat="server" SkinID="LabelNormal" Text="OS Locking"></asp:Label>
                                    </td>
                                    <%--<td class="inputcontrolsColSpan2">
                                        <asp:TextBox ID="txtOSLocking" Text='<%# Bind("OS_Lock") %>'  runat="server" TabIndex="25"  
                                            SkinID="TextBoxNormalBig" MaxLength="1"></asp:TextBox>
                                    </td>--%>
                                    <td class="inputcontrols">
                                        <asp:DropDownList ID="ddlOSLocking" runat="server" SkinID="DropDownListNormalBig" TabIndex="25">
                                        </asp:DropDownList>
                                    </td>
                                </tr>
                                <tr>
                                    <td class="labelColSpan2">
                                        <asp:Label ID="lblO_SCalculationDays" runat="server" SkinID="LabelNormal" Text="O/S Calculation Days"></asp:Label>
                                    </td>
                                    <td class="inputcontrolsColSpan2">
                                        <asp:TextBox ID="txtO_SCalculationDays" Text='<%# Bind("OS_Cancellation_days") %>'
                                            TabIndex="26" runat="server" SkinID="TextBoxNormalBig" MaxLength="2"></asp:TextBox>
                                    </td>
                                    <td class="labelColSpan2">
                                    </td>
                                    <td class="inputcontrolsColSpan2">
                                    </td>
                                </tr>
                            </table>
                            <div class="subFormTitle subFormTitleExtender350">
                                MINIMUM PARAMETER FOR STOCK TRANSFER
                            </div>
                            <table class="subFormTable">
                                <tr>
                                    <td class="labelColSpan2">
                                        <asp:Label ID="lblStockAging" runat="server" SkinID="LabelNormal" Text="Stock Aging"></asp:Label>
                                        <span class="asterix">*</span>
                                    </td>
                                    </td>
                                    <td class="inputcontrolsColSpan2">
                                        <asp:TextBox ID="txtStockAging" Text='<%# Bind("Min_Stock_Age_for_STDN") %>' runat="server"
                                            TabIndex="27" SkinID="TextBoxNormalBig" MaxLength="3"></asp:TextBox>
                                    </td>
                                    <td class="labelColSpan2">
                                        <asp:Label ID="lblStockValue" runat="server" SkinID="LabelNormal" Text="Stock Value"></asp:Label>
                                        <span class="asterix">*
                                    </td>
                                    <td class="inputcontrolsColSpan2">
                                        <asp:TextBox ID="txtStockValue" runat="server" Text='<%# Bind("Min_Stock_Value_for_STDN") %>'
                                            TabIndex="28" SkinID="TextBoxNormalBig" MaxLength="8"></asp:TextBox>
                                    </td>
                                </tr>
                            </table>
                            <div class="subFormTitle subFormTitleExtender350">
                                SALES TAX
                            </div>
                            <table class="subFormTable">
                                <tr>
                                    <td class="labelColSpan2">
                                        <asp:Label ID="lblLocalST_RCNo" runat="server" SkinID="LabelNormal" Text="Local ST RC no"></asp:Label>
                                        <span class="asterix">*</span>
                                    </td>
                                    </td>
                                    <td class="inputcontrolsColSpan2">
                                        <asp:TextBox ID="txtLocalST_RCNo" runat="server" TabIndex="29" Text='<%# Bind("Local_sales_tax_number") %>'
                                            SkinID="TextBoxNormalBig" MaxLength="20"></asp:TextBox>
                                    </td>
                                    <td class="labelColSpan2">
                                        <asp:Label ID="lblCentralSalesTaxNumber" runat="server" SkinID="LabelNormal" Text="Central Sales Tax Number"></asp:Label>
                                    </td>
                                    <td class="inputcontrolsColSpan2">
                                        <asp:TextBox ID="txtCentralSalesTaxNumber" TabIndex="30" runat="server" Text='<%# Bind("Central_sales_tax_number") %>'
                                            SkinID="TextBoxNormalBig" MaxLength="20"></asp:TextBox>
                                    </td>
                                </tr>
                                <tr>
                                    <td class="labelColSpan2">
                                        <asp:Label ID="lblCESSNumber" runat="server" SkinID="LabelNormal" Text="CESS Number"></asp:Label>
                                    </td>
                                    <td class="inputcontrolsColSpan2">
                                        <asp:TextBox ID="txtCESSNumber" runat="server" Text='<%# Bind("CESS_number") %>'
                                            TabIndex="31" SkinID="TextBoxNormalBig" MaxLength="20"></asp:TextBox>
                                    </td>
                                    <td class="labelColSpan2">
                                        <asp:Label ID="lblTINNumber" runat="server" SkinID="LabelNormal" Text="TIN Number"></asp:Label>
                                    </td>
                                    <td class="inputcontrolsColSpan2">
                                        <asp:TextBox ID="txtTINNumber" runat="server" Text='<%# Bind("tin") %>' TabIndex="32"
                                            SkinID="TextBoxNormalBig" MaxLength="12"></asp:TextBox>
                                    </td>
                                </tr>
                            </table>
                            <asp:RequiredFieldValidator ID="ReqtxtBranch" ValidationGroup="btnUpdate" ControlToValidate="ddlBranch"
                                Display="None" InitialValue="0" runat="server" ErrorMessage="Please Select Branch."></asp:RequiredFieldValidator>
                            <ajaxToolkit:ValidatorCalloutExtender ID="ValidatorCalloutExtender1" TargetControlID="ReqtxtBranch"
                                PopupPosition="Right" runat="server">
                            </ajaxToolkit:ValidatorCalloutExtender>
                            <asp:RequiredFieldValidator ID="ReqBranchName" SetFocusOnError="true" ValidationGroup="btnUpdate"
                                ControlToValidate="txtBranchName" runat="server" ErrorMessage="Branch Name is required."
                                Display="None"></asp:RequiredFieldValidator>
                            <ajaxToolkit:ValidatorCalloutExtender ID="ValidatorCalloutExtender2" TargetControlID="ReqBranchName"
                                PopupPosition="Left" runat="server">
                            </ajaxToolkit:ValidatorCalloutExtender>
                            <asp:CustomValidator ID="ValBranchName" runat="server" Display="None" ValidationGroup="btnUpdate"
                                SetFocusOnError="true" ControlToValidate="txtBranchName" ClientValidationFunction="validateFields"
                                ErrorMessage="First character should be alphabet or number"></asp:CustomValidator>
                            <ajaxToolkit:ValidatorCalloutExtender ID="ValidatorCalloutExtender18" TargetControlID="ValBranchName"
                                PopupPosition="Left" runat="server">
                            </ajaxToolkit:ValidatorCalloutExtender>
                            <asp:RequiredFieldValidator ID="ReqAddress" SetFocusOnError="true" ValidationGroup="btnUpdate"
                                ControlToValidate="txtAddress" runat="server" ErrorMessage="Address is required."
                                Display="None"></asp:RequiredFieldValidator>
                            <ajaxToolkit:ValidatorCalloutExtender ID="ValidatorCalloutExtender3" TargetControlID="ReqAddress"
                                PopupPosition="Right" runat="server">
                            </ajaxToolkit:ValidatorCalloutExtender>
                            <asp:CustomValidator ID="ValAddress" runat="server" Display="None" ValidationGroup="btnUpdate"
                                SetFocusOnError="true" ControlToValidate="txtAddress" ClientValidationFunction="validateFields"
                                ErrorMessage="First character should be alphabet or number"></asp:CustomValidator>
                            <ajaxToolkit:ValidatorCalloutExtender ID="ValidatorCalloutExtender20" TargetControlID="ValAddress"
                                PopupPosition="Left" runat="server">
                            </ajaxToolkit:ValidatorCalloutExtender>
                            <asp:CustomValidator ID="ValFax" runat="server" Display="None" ValidationGroup="btnUpdate"
                                ControlToValidate="txtFax" ClientValidationFunction="validateFields"
                                ErrorMessage="First character should be alphabet or number"></asp:CustomValidator>
                            <ajaxToolkit:ValidatorCalloutExtender ID="ValidatorCalloutExtender22" TargetControlID="ValFax"
                                PopupPosition="Left" runat="server">
                            </ajaxToolkit:ValidatorCalloutExtender>
                            <asp:CustomValidator ID="ValTelex" runat="server" Display="None" ValidationGroup="btnUpdate"
                                ControlToValidate="txtTelex" ClientValidationFunction="validateFields"
                                ErrorMessage="First character should be alphabet or number"></asp:CustomValidator>
                            <ajaxToolkit:ValidatorCalloutExtender ID="ValidatorCalloutExtender23" TargetControlID="ValTelex"
                                PopupPosition="Left" runat="server">
                            </ajaxToolkit:ValidatorCalloutExtender>
                            <asp:CustomValidator ID="ValEmailID" runat="server" Display="None" ValidationGroup="btnUpdate"
                                ControlToValidate="txtEmailid" ClientValidationFunction="validateFields"
                                ErrorMessage="First character should be alphabet"></asp:CustomValidator>
                            <ajaxToolkit:ValidatorCalloutExtender ID="ValidatorCalloutExtender28" TargetControlID="ValEmailID"
                                PopupPosition="Left" runat="server">
                            </ajaxToolkit:ValidatorCalloutExtender>
                            <asp:RegularExpressionValidator ID="regValidator" runat="server" Display="None" ErrorMessage="Email is not vaild" ControlToValidate="txtEmailid" 
                                ValidationExpression="^(([a-zA-Z0-9_\-\.]+)@([a-zA-Z0-9_\-\.]+)\.([a-zA-Z]{2,5}){1,25})+([;.](([a-zA-Z0-9_\-\.]+)@([a-zA-Z0-9_\-\.]+)\.([a-zA-Z]{2,5}){1,25})+)*$" 
                                ValidationGroup="btnUpdate"></asp:RegularExpressionValidator>
                                    <ajaxToolkit:ValidatorCalloutExtender ID="ValidatorCalloutExtender29" TargetControlID="regValidator"
                                PopupPosition="Left" runat="server">
                            </ajaxToolkit:ValidatorCalloutExtender>
                            <asp:RequiredFieldValidator ID="ReqClassification" ValidationGroup="btnUpdate" ControlToValidate="ddlClassification"
                                Display="None" InitialValue="0" runat="server" ErrorMessage="Please Select Classification."></asp:RequiredFieldValidator>
                            <ajaxToolkit:ValidatorCalloutExtender ID="ValidatorCalloutExtender4" TargetControlID="ReqClassification"
                                PopupPosition="Right" runat="server">
                            </ajaxToolkit:ValidatorCalloutExtender>
                            <asp:RequiredFieldValidator ID="ReqReportingState" ValidationGroup="btnUpdate" ControlToValidate="ddlReportingSate"
                                Display="None" InitialValue="0" runat="server" ErrorMessage="Please Select Reporting State."></asp:RequiredFieldValidator>
                            <ajaxToolkit:ValidatorCalloutExtender ID="ValidatorCalloutExtender5" TargetControlID="ReqReportingState"
                                PopupPosition="Left" runat="server">
                            </ajaxToolkit:ValidatorCalloutExtender>
                            <asp:RequiredFieldValidator ID="ReqddlState" ValidationGroup="btnUpdate" ControlToValidate="ddlState"
                                Display="None" InitialValue="0" runat="server" ErrorMessage="Please Select State."></asp:RequiredFieldValidator>
                            <ajaxToolkit:ValidatorCalloutExtender ID="ValidatorCalloutExtender6" TargetControlID="ReqddlState"
                                PopupPosition="Left" runat="server">
                            </ajaxToolkit:ValidatorCalloutExtender>
                            <asp:RegularExpressionValidator ID="ValPhone" runat="server" Display="None" 
                                ErrorMessage="Phone number must be numbers only" ControlToValidate="txtPhone" 
                                ValidationExpression="^\d+$" ValidationGroup="btnUpdate"></asp:RegularExpressionValidator>
                            <ajaxToolkit:ValidatorCalloutExtender ID="ValidatorCalloutExtender31" TargetControlID="ValPhone"
                                PopupPosition="Left" runat="server">
                            </ajaxToolkit:ValidatorCalloutExtender>
                            <asp:CustomValidator ID="ValBranchManager" runat="server" Display="None" ValidationGroup="btnUpdate"
                                ControlToValidate="txtBranchManager" ClientValidationFunction="validateFields"
                                ErrorMessage="First character should be alphabet or number"></asp:CustomValidator>
                            <ajaxToolkit:ValidatorCalloutExtender ID="ValidatorCalloutExtender24" TargetControlID="ValBranchManager"
                                PopupPosition="Left" runat="server">
                            </ajaxToolkit:ValidatorCalloutExtender>
                            <asp:CustomValidator ID="ValAccountManager" runat="server" Display="None" ValidationGroup="btnUpdate"
                                ControlToValidate="txtlblAccountsManager_Officer" ClientValidationFunction="validateFields"
                                ErrorMessage="First character should be alphabet or number"></asp:CustomValidator>
                            <ajaxToolkit:ValidatorCalloutExtender ID="ValidatorCalloutExtender25" TargetControlID="ValAccountManager"
                                PopupPosition="Left" runat="server">
                            </ajaxToolkit:ValidatorCalloutExtender>
                            <asp:CustomValidator ID="ValED" runat="server" Display="None" ValidationGroup="btnUpdate"
                                ControlToValidate="txtEDPin_charge" ClientValidationFunction="validateFields"
                                ErrorMessage="First character should be alphabet or number"></asp:CustomValidator>
                            <ajaxToolkit:ValidatorCalloutExtender ID="ValidatorCalloutExtender26" TargetControlID="ValED"
                                PopupPosition="Left" runat="server">
                            </ajaxToolkit:ValidatorCalloutExtender>
                            <asp:RequiredFieldValidator ID="ReqOpeningDate" SetFocusOnError="true" ValidationGroup="btnUpdate"
                                ControlToValidate="txtOpeningDate" runat="server" ErrorMessage="Opening Date is required."
                                Display="None"></asp:RequiredFieldValidator>
                            <ajaxToolkit:ValidatorCalloutExtender ID="ValidatorCalloutExtender7" TargetControlID="ReqOpeningDate"
                                PopupPosition="Left" runat="server">
                            </ajaxToolkit:ValidatorCalloutExtender>
                            <asp:CustomValidator ID="ValDestination" runat="server" Display="None" ValidationGroup="btnUpdate"
                                ControlToValidate="txtDestination" ClientValidationFunction="validateFields"
                                ErrorMessage="First character should be alphabet or number"></asp:CustomValidator>
                            <ajaxToolkit:ValidatorCalloutExtender ID="ValidatorCalloutExtender27" TargetControlID="ValDestination"
                                PopupPosition="Left" runat="server">
                            </ajaxToolkit:ValidatorCalloutExtender>
                            <asp:CustomValidator ID="ValPreferred" runat="server" Display="None" ValidationGroup="btnUpdate"
                                ControlToValidate="txtPreferredCarrier" ClientValidationFunction="validateFields"
                                ErrorMessage="First character should be alphabet or number"></asp:CustomValidator>
                            <ajaxToolkit:ValidatorCalloutExtender ID="ValidatorCalloutExtender30" TargetControlID="ValPreferred"
                                PopupPosition="Left" runat="server">
                            </ajaxToolkit:ValidatorCalloutExtender>
                            <asp:RequiredFieldValidator ID="ReqStatusValidator1" ValidationGroup="btnUpdate"
                                ControlToValidate="ddlStatus" Display="None" runat="server" ErrorMessage="Please Select Status."></asp:RequiredFieldValidator>
                            <ajaxToolkit:ValidatorCalloutExtender ID="ValidatorCalloutExtender8" TargetControlID="ReqStatusValidator1"
                                PopupPosition="Left" runat="server">
                            </ajaxToolkit:ValidatorCalloutExtender>
                            <asp:RequiredFieldValidator ID="ReqStockAging" ValidationGroup="btnUpdate" ControlToValidate="txtStockAging"
                                Display="None" runat="server" ErrorMessage="Please Enter Stock Aging."></asp:RequiredFieldValidator>
                            <ajaxToolkit:ValidatorCalloutExtender ID="ValidatorCalloutExtender9" TargetControlID="ReqStockAging"
                                PopupPosition="Right" runat="server">
                            </ajaxToolkit:ValidatorCalloutExtender>
                            <asp:RequiredFieldValidator ID="ReqStockvalue" ValidationGroup="btnUpdate" ControlToValidate="txtStockValue"
                                Display="None" runat="server" ErrorMessage="Please Enter Stock Value."></asp:RequiredFieldValidator>
                            <ajaxToolkit:ValidatorCalloutExtender ID="ValidatorCalloutExtender10" TargetControlID="ReqStockvalue"
                                PopupPosition="Left" runat="server">
                            </ajaxToolkit:ValidatorCalloutExtender>
                            <asp:RegularExpressionValidator ID="ValStockValue" runat="server" Display="None" 
                                ErrorMessage="Stock value should be numbers only" ControlToValidate="txtStockValue" 
                                ValidationExpression="^\d+$" ValidationGroup="btnUpdate"></asp:RegularExpressionValidator>
                            <ajaxToolkit:ValidatorCalloutExtender ID="ValidatorCalloutExtender21" TargetControlID="ValStockValue"
                                PopupPosition="Left" runat="server">
                            </ajaxToolkit:ValidatorCalloutExtender>
                            <!---
                           Custom validation
                           -->
                            <asp:CompareValidator ID="CmpAreaSqFeetvalidator" runat="server" SetFocusOnError="true"
                                ControlToValidate="txtArea_sq_feet" Operator="DataTypeCheck" ValidationGroup="btnUpdate"
                                Type="Double" Display="None" ErrorMessage="Area Sq feet allows only numeric digits" />
                            <ajaxToolkit:ValidatorCalloutExtender ID="ValidatorCalloutExtender11" TargetControlID="CmpAreaSqFeetvalidator"
                                PopupPosition="Right" runat="server">
                            </ajaxToolkit:ValidatorCalloutExtender>
                            <asp:CompareValidator ID="ComMonthlyRent" runat="server" SetFocusOnError="true" ControlToValidate="txtMonthlyRent"
                                Operator="DataTypeCheck" ValidationGroup="btnUpdate" Type="Double" Display="None"
                                ErrorMessage="Monthly Rent allows only numeric digits" />
                            <ajaxToolkit:ValidatorCalloutExtender ID="ValidatorCalloutExtender12" TargetControlID="ComMonthlyRent"
                                PopupPosition="Left" runat="server">
                            </ajaxToolkit:ValidatorCalloutExtender>
                            <asp:CompareValidator ID="ComRentalAdvance" runat="server" SetFocusOnError="true"
                                ControlToValidate="txtRentalAdvance" Operator="DataTypeCheck" ValidationGroup="btnUpdate"
                                Type="Double" Display="None" ErrorMessage="Rental Advance allows only numeric digits" />
                            <ajaxToolkit:ValidatorCalloutExtender ID="ValidatorCalloutExtender13" TargetControlID="ComRentalAdvance"
                                PopupPosition="Right" runat="server">
                            </ajaxToolkit:ValidatorCalloutExtender>
                            <asp:CompareValidator ID="ComNoticePeriod" runat="server" SetFocusOnError="true"
                                ControlToValidate="txtNoticePeriod" Operator="DataTypeCheck" ValidationGroup="btnUpdate"
                                Type="Integer" Display="None" ErrorMessage="Notice Period allows only numeric digits" />
                            <ajaxToolkit:ValidatorCalloutExtender ID="ValidatorCalloutExtender14" TargetControlID="ComNoticePeriod"
                                PopupPosition="Left" runat="server">
                            </ajaxToolkit:ValidatorCalloutExtender>
                            <asp:CompareValidator ID="ComStockAging" runat="server" SetFocusOnError="true" ControlToValidate="txtStockAging"
                                Operator="DataTypeCheck" ValidationGroup="btnUpdate" Type="Integer" Display="None"
                                ErrorMessage="Stock Aging allows only Intger digits" />
                            <ajaxToolkit:ValidatorCalloutExtender ID="ValidatorCalloutExtender15" TargetControlID="ComStockAging"
                                PopupPosition="Right" runat="server">
                            </ajaxToolkit:ValidatorCalloutExtender>
                            <asp:CompareValidator ID="ComStockValue" runat="server" SetFocusOnError="true" ControlToValidate="txtStockValue"
                                Operator="DataTypeCheck" ValidationGroup="btnUpdate" Type="Double" Display="None"
                                ErrorMessage="Stock value allows only double digits" />
                            <ajaxToolkit:ValidatorCalloutExtender ID="ValidatorCalloutExtender16" TargetControlID="ComStockValue"
                                PopupPosition="Left" runat="server">
                            </ajaxToolkit:ValidatorCalloutExtender>
                            <asp:CompareValidator ID="ComO_SCalculationDays" runat="server" SetFocusOnError="true"
                                ControlToValidate="txtO_SCalculationDays" Operator="DataTypeCheck" ValidationGroup="btnUpdate"
                                Type="Integer" Display="None" ErrorMessage="OS Calculation Days allows only Intger digits" />
                            <ajaxToolkit:ValidatorCalloutExtender ID="ValidatorCalloutExtender17" TargetControlID="ComO_SCalculationDays"
                                PopupPosition="Right" runat="server">
                            </ajaxToolkit:ValidatorCalloutExtender>
                        </div>
                    </EditItemTemplate>
                </asp:FormView>
                <table class="subFormTable">
                    <tr>
                        <td class="labelColSpan2" align="center" colspan="4">
                            <asp:Label ID="lblNoRecord" SkinID="Error" ForeColor="Red" runat="server"></asp:Label>
                        </td>
                    </tr>
                    <tr>
                        <td class="labelColSpan2">
                        </td>
                        <td class="inputcontrolsColSpan2">
                        </td>
                        <td class="labelColSpan2">
                        </td>
                        <td class="inputcontrolsColSpan2">
                        </td>
                    </tr>
                </table>
            </div>
            <div class="transactionButtons">
                <div class="transactionButtonsHolder">
                    <asp:Button ID="BtnSubmit" runat="server" Text="Submit" SkinID="ButtonNormalBig"
                        ValidationGroup="btnUpdate" CausesValidation="true" OnClick="BtnSubmit_Click" />
                    <asp:Button ID="BtnUpdate" runat="server" Text="Update" SkinID="ButtonNormalBig"
                        ValidationGroup="btnUpdate" CausesValidation="true" OnClick="BtnUpdate_Click" />
                    <asp:Button ID="btnReset" runat="server" Text="Reset" SkinID="ButtonNormalBig" CausesValidation="false"
                        OnClick="btnReset_Click" />
                    <asp:Button ID="btnReport" runat="server" Text="Generate Report" SkinID="ButtonNormalBig"
                        OnClick="btnReport_Click" />
                    <asp:HiddenField ID="hdnBrnCurrentBranchCode" runat="server" />
                    <asp:HiddenField ID="hdnBrnCurrentStateCode" runat="server" />
                    <asp:HiddenField ID="hdnBrnCurrentReportingStateCode" runat="server" />
                    <asp:HiddenField ID="hdnBrnCurrentClassification" runat="server" />
                </div>
            </div>
        </ContentTemplate>
        <Triggers>
            <asp:PostBackTrigger ControlID="btnReport" />
        </Triggers>
    </asp:UpdatePanel>
</asp:Content>
