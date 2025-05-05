<%@ Page Language="C#" AutoEventWireup="true" MasterPageFile="~/Main.Master" CodeBehind="OtherPayment.aspx.cs"
    Inherits="IMPALWeb.Transactions.Finance.Payable.OtherPayment" EnableEventValidation="false" %>

<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="ajaxToolkit" %>
<%@ Register Src="~/UserControls/ChartAccount.ascx" TagName="ChartAccount" TagPrefix="uc1" %>
<asp:Content ID="Content3" ContentPlaceHolderID="CPHDetails" runat="server">

    <script language="javascript" type="text/javascript">
        var CtrlIdPrefix = "ctl00_CPHDetails_";

        function checkDateToDate(id) {

            var idDate = document.getElementById(id).value;

            if (idDate != '') {
                var status = fnIsDate(idDate);

                if (!status) {
                    document.getElementById(id).value = "";
                    document.getElementById(id).focus();
                }
                else {


                    var idFromDate = document.getElementById("ctl00_CPHDetails_txtChequeSlipDate").value

                    var toDate = new Date();
                    toDate = convertDate(idDate);

                    var frmDate = new Date();
                    frmDate = convertDate(idFromDate);

                    var date = new Date();
                    date.setHours(0);
                    date.setMinutes(0);
                    date.setSeconds(0);
                    date.setMilliseconds(0);


                    if (toDate < frmDate) {
                        document.getElementById(id).value = "";
                        alert("Cheque Date should not be less than Cheque Slip Date");
                    }

                    if (toDate > date) {
                        document.getElementById(id).value = "";
                        alert("Cheque Date should not be greater then System Date");
                    }
                }
            }
        }

        function ValidationSubmit() {

            var branch = document.getElementById(CtrlIdPrefix + "ddlBranch");
            if (branch.value == "" || branch.value == "0") {
                alert("Branch should not be null");                
                return false;
            }

            var remarks = document.getElementById(CtrlIdPrefix + "txtRemarks");
            if (remarks.value == "") {
                alert("Remarks should not be null");
                remarks.focus();
                return false;
            }

            var chartofaccount = document.getElementById(CtrlIdPrefix + "txtChartofAccount");
            if (chartofaccount.value == "") {
                alert("Chart of account should not be null");
                return false;
            }

            var supplier = document.getElementById(CtrlIdPrefix + "ddlSupplier");
            if (supplier.value == "" || supplier.value == "0") {
                alert("Supplier should not be null");
                supplier.focus();
                return false;
            }

            var horefnumber = document.getElementById(CtrlIdPrefix + "ddlHOREFNumber");
            if (horefnumber.value == "" || horefnumber.value == "0") {
                alert("Ho Ref No should not be null");
                horefnumber.focus();
                return false;
            }

            var chequenumber = document.getElementById(CtrlIdPrefix + "txtChequeNumber");
            if (chequenumber.value == "") {
                alert("Cheque Number should not be null");
                chequenumber.focus();
                return false;
            }

            var chequedate = document.getElementById(CtrlIdPrefix + "txtChequeDate");
            if (chequedate.value == "") {
                alert("Cheque Date should not be null");
                chequedate.focus();
                return false;
            }
        }
    </script>

    <asp:UpdatePanel ID="upHeader" runat="server" UpdateMode="Conditional">
        <ContentTemplate>
            <div id="DivTop" runat="server">
                <div>
                    <div class="reportFormTitle  reportFormTitleExtender250">
                        CHEQUE SLIP [ OTHER PAYMENTS ]</div>
                    <table class="subFormTable">
                        <tr>
                            <td colspan="6" class="label">
                                <asp:Label ID="lblHeaderMessage" Text="" runat="server" SkinID="Error"></asp:Label>
                            </td>
                        </tr>
                        <tr>
                            <td class="label">
                                <asp:Label ID="lblChequeSlipNumber" runat="server" Text="Cheque Slip Number" SkinID="LabelNormal"></asp:Label>
                            </td>
                            <td class="inputcontrols">
                                <asp:TextBox ID="txtChequeSlipNumber" runat="server" Enabled="false" SkinID="TextBoxDisabled"></asp:TextBox>
                                <asp:DropDownList ID="ddlChequeSlipNumber" runat="server" AutoPostBack="True" SkinID="DropDownListNormal"
                                    OnSelectedIndexChanged="ddlChequeSlipNumber_SelectedIndexChanged">
                                    <asp:ListItem Selected="True" Text="-- Select --" Value="0"></asp:ListItem>
                                </asp:DropDownList>
                                <asp:ImageButton ID="imgEditToggle" ImageUrl="~/images/ifind.png" SkinID="ImageButtonSearch"
                                    runat="server" OnClick="imgEditToggle_Click" />
                            </td>
                            <td class="label">
                                <asp:Label ID="lblChequeSlipDate" runat="server" Text="Cheque Slip Date" SkinID="LabelNormal"></asp:Label>
                            </td>
                            <td class="inputcontrols">
                                <asp:TextBox ID="txtChequeSlipDate" runat="server" SkinID="TextBoxDisabled" Enabled="true"></asp:TextBox>
                                <ajaxToolkit:CalendarExtender ID="calChequeSlipDate" runat="server" EnableViewState="true"
                                    Format="dd/MM/yyyy" TargetControlID="txtChequeSlipDate">
                                </ajaxToolkit:CalendarExtender>
                            </td>
                            <td class="label">
                                <asp:Label ID="Label10" runat="server" Text="Branch" SkinID="LabelNormal"></asp:Label>
                                <span class="asterix">*</span>
                            </td>
                            <td class="inputcontrols">
                                <asp:DropDownList ID="ddlBranch" runat="server" DataSourceID="ODS_AllBranch" DataTextField="BranchName"
                                    DataValueField="BranchCode" SkinID="DropDownListNormal" Enabled="false">
                                </asp:DropDownList>
                            </td>
                        </tr>
                        <tr>
                            <td class="label">
                                <asp:Label ID="Label1" runat="server" Text="Chart of Account" SkinID="LabelNormal"></asp:Label>
                            </td>
                            <td class="inputcontrols">
                                <asp:TextBox ID="txtChartofAccount" runat="server" SkinID="TextBoxNormal" Enabled="false"></asp:TextBox>
                                <uc1:ChartAccount runat="server" DefaultBranch="true" ID="BankAccNo" Filter="009,036,047,049" OnSearchImageClicked="ucChartAccount_SearchImageClicked" />
                            </td>
                             <td class="label">
                                <asp:Label ID="lblSupplier" runat="server" Text="Supplier" SkinID="LabelNormal"></asp:Label>
                                
                            </td>
                            <td class="inputcontrols">
                                <asp:DropDownList ID="ddlSupplier" runat="server" DataSourceID="ODS_Suppliers" DataTextField="SupplierName"
                                    AutoPostBack="True" DataValueField="SupplierCode" SkinID="DropDownListNormal"
                                    OnSelectedIndexChanged="ddlSupplier_SelectedIndexChanged">
                                </asp:DropDownList>
                            </td>
                            <td class="label">
                                <asp:Label ID="Label2" runat="server" Text="Ho REF. Number" SkinID="LabelNormal"></asp:Label>
                            </td>
                            <td class="inputcontrols">
                                <asp:DropDownList ID="ddlHOREFNumber" runat="server" AutoPostBack="True" SkinID="DropDownListNormal"                                    
                                    OnSelectedIndexChanged="ddlHOREFNumber_SelectedIndexChanged">
                                </asp:DropDownList>
                                <asp:TextBox ID="txtHOREFNumber" runat="server" SkinID="TextBoxNormal"></asp:TextBox>
                            </td>
                           
                        </tr>
                        <tr>
                            <td class="label">
                                <asp:Label ID="Label3" runat="server" Text="Amount" SkinID="LabelNormal"></asp:Label>
                            </td>
                            <td class="inputcontrols">
                                <asp:TextBox ID="txtAmount" runat="server" SkinID="TextBoxDisabled" Enabled="false"></asp:TextBox>
                            </td>
                            <td class="label">
                                <asp:Label ID="lblRemarks" runat="server" Text="Remarks" SkinID="LabelNormal"></asp:Label>
                            </td>
                            <td class="inputcontrols">
                                <asp:TextBox ID="txtRemarks" runat="server" SkinID="TextBoxNormal"></asp:TextBox>
                            </td>
                        </tr>
                    </table>
                    <div class="subFormTitle">
                        CHEQUE DETAILS</div>
                    <table class="subFormTable">
                        <tr>
                            <td class="label">
                                <asp:Label ID="Label4" runat="server" Text="Cheque Number" SkinID="LabelNormal"></asp:Label>
                            </td>
                            <td class="inputcontrols">
                                <asp:TextBox ID="txtChequeNumber" runat="server" SkinID="TextBoxNormal" MaxLength="6"></asp:TextBox>
                            </td>
                            <td class="label">
                                <asp:Label ID="Label5" runat="server" Text="Cheque Date" SkinID="LabelNormal"></asp:Label>
                            </td>
                            <td class="inputcontrols">
                                <asp:TextBox ID="txtChequeDate" runat="server" SkinID="TextBoxNormal" onblur="return checkDateToDate(this.id);"></asp:TextBox>
                                 <ajaxToolkit:CalendarExtender ID="calchequedate" Format="dd/MM/yyyy" runat="server" TargetControlID="txtChequeDate">
                                </ajaxToolkit:CalendarExtender>
                            </td>
                            <td class="label">
                                <asp:Label ID="Label6" runat="server" Text="Bank" SkinID="LabelNormal"></asp:Label>
                            </td>
                            <td class="inputcontrols">
                                <asp:TextBox ID="txtChequeBank" runat="server" SkinID="TextBoxDisabled" Enabled="false"></asp:TextBox>
                            </td>
                        </tr>
                        <tr>
                            <td class="label">
                                <asp:Label ID="Label7" runat="server" Text="Branch" SkinID="LabelNormal"></asp:Label>
                            </td>
                            <td class="inputcontrols">
                                <asp:TextBox ID="txtChequeBranch" runat="server" SkinID="TextBoxDisabled" Enabled="false"></asp:TextBox>
                            </td>
                        </tr>
                    </table>
                </div>
            </div>
        </ContentTemplate>
        <Triggers>
            <asp:AsyncPostBackTrigger ControlID="BtnSubmit" EventName="Click" />
        </Triggers>
    </asp:UpdatePanel>
    <div id="divItemDetails" runat="server">
        <asp:UpdatePanel ID="UpdPanelGrid" runat="server" UpdateMode="Conditional">
            <ContentTemplate>
                <div id="idTrans" runat="server" class="subFormTitle">
                    ITEM DETAILS</div>
                <div id="idGrid" runat="server" align="center">
                    <asp:GridView ID="grvItemDetails" runat="server" AutoGenerateColumns="False" AllowPaging="false"
                        SkinID="GridViewTransaction">
                        <Columns>
                            <asp:TemplateField HeaderText="Branch">
                                <ItemTemplate>
                                    <asp:TextBox ID="txtBranch" runat="server" SkinID="GridViewTextBox" Text='<%# Bind("Branch") %>'
                                        Enabled="false"> </asp:TextBox>
                                </ItemTemplate>
                            </asp:TemplateField>
                            <asp:TemplateField HeaderText="Amount">
                                <ItemTemplate>
                                    <asp:TextBox ID="txtAmount" runat="server" SkinID="GridViewTextBox" Text='<%# Bind("Amount") %>'></asp:TextBox>
                                </ItemTemplate>
                            </asp:TemplateField>
                        </Columns>
                    </asp:GridView>
                </div>
                <br />
                <div class="transactionButtons">
                    <div class="transactionButtonsHolder">
                        <asp:Button ID="BtnSubmit" runat="server" ValidationGroup="BtnSubmit" SkinID="ButtonNormal"
                            CausesValidation="true" Text="Submit" OnClick="BtnSubmit_Click" />
                        <asp:Button ID="btnReset" ValidationGroup="BtnSubmit" runat="server" CausesValidation="false"
                            SkinID="ButtonNormal" Text="Reset" OnClick="btnReset_Click" />
                    </div>
                </div>
            </ContentTemplate>
            <Triggers>
                <asp:AsyncPostBackTrigger ControlID="ddlChequeSlipNumber" EventName="SelectedIndexChanged" />
            </Triggers>
        </asp:UpdatePanel>
    </div>
    <asp:ObjectDataSource ID="ODS_Suppliers" runat="server" SelectMethod="GetAllSuppliers"
        TypeName="IMPALLibrary.Suppliers" DataObjectTypeName="IMPALLibrary.Suppliers">
    </asp:ObjectDataSource>
    <asp:ObjectDataSource ID="ODS_AllBranch" runat="server" SelectMethod="GetAllBranchesCRP"
        TypeName="IMPALLibrary.Branches" DataObjectTypeName="IMPALLibrary.Branches">
    </asp:ObjectDataSource>
</asp:Content>
