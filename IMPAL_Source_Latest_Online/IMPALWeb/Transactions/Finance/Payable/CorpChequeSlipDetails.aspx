<%@ Page Language="C#" MasterPageFile="~/Main.Master" AutoEventWireup="true" CodeBehind="CorpChequeSlipDetails.aspx.cs"
    Inherits="IMPALWeb.Transactions.Finance.Payable.CorpChequeSlipDetails" %>

<%@ Register Src="~/UserControls/CrystalReport.ascx" TagName="CrystalReport" TagPrefix="UC" %>
<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="ajaxToolkit" %>
<asp:Content ID="Content1" ContentPlaceHolderID="CPHDetails" runat="server">

    <script language="javascript" type="text/javascript">
        var CtrlIdPrefix = "ctl00_CPHDetails_";
        
        function fnValidate() {

            var trdisplay = document.getElementById(CtrlIdPrefix + "ChequeWise");
            if (trdisplay.style.display == "none") {
                var txtFromDate = document.getElementById('<%=txtFromDate.ClientID%>');
                var txtToDate = document.getElementById('<%=txtToDate.ClientID%>');
                return ValidateDates(txtFromDate, txtToDate);
            }
            else {
                var FromChequeNo = document.getElementById(CtrlIdPrefix + "txtChequeFrom");
                if (FromChequeNo.value == "") {
                    alert("From Cheque Number should not be null");
                    FromChequeNo.focus();
                    return false;
                }

                var ToChequeNo = document.getElementById(CtrlIdPrefix + "txtChequeTo");
                if (ToChequeNo.value == "") {
                    alert("To Cheque Number should not be null");
                    FromChequeNo.focus();
                    return false;
                }
            }
            return false;
        }
    </script>

    <div class="subFormTitle subFormTitleExtender350">
        Cheque Slip Details (Corporate)
    </div>
    <div class="reportFilters">
        <table id="reportFiltersTable" class="reportFiltersTable" runat="server">
            <tr>
                <td class="label">
                    <asp:Label ID="lblBankName" runat="server" Text="Bank Name" SkinID="LabelNormal"></asp:Label>
                    <span class="asterix">*</span>
                </td>
                <td class="inputcontrols">
                    <asp:DropDownList ID="ddlBankName" runat="server" AutoPostBack="True" SkinID="DropDownListNormal"
                        OnSelectedIndexChanged="ddlBankName_SelectedIndexChanged">
                    </asp:DropDownList>
                </td>
                <td class="label">
                    <asp:Label ID="lblReportType" Text="Branch" SkinID="LabelNormal" runat="server"></asp:Label><span
                        class="asterix">*</span>
                </td>
                <td class="inputcontrols">
                    <asp:DropDownList ID="ddlReportType" runat="server" SkinID="DropDownListNormal" AutoPostBack="true"
                        OnSelectedIndexChanged="ddlReportType_SelectedIndexChanged" Enabled="false">
                    </asp:DropDownList>
                </td>
            </tr>
        </table>
        <div id="DateWise" runat="server">
            <table class="reportFiltersTable">
                <tr>
                    <td class="label">
                        <asp:Label ID="lblFromDate" runat="server" Text="From Date" SkinID="LabelNormal"></asp:Label>
                        <span class="asterix">*</span>
                    </td>
                    <td class="inputcontrols">
                        <asp:TextBox ID="txtFromDate" SkinID="TextBoxCalendarExtenderNormal" TabIndex="1"
                            runat="server" onblur="return checkDateForValidDate(this.id);"></asp:TextBox>
                        <ajaxToolkit:CalendarExtender ID="calFromdate" Format="dd/MM/yyyy" runat="server"
                            TargetControlID="txtFromDate">
                        </ajaxToolkit:CalendarExtender>
                    </td>
                    <td class="label">
                        <asp:Label ID="lblToDate" runat="server" Text="To Date" SkinID="LabelNormal"></asp:Label>
                        <span class="asterix">*</span>
                    </td>
                    <td class="inputcontrols">
                        <asp:TextBox ID="txtToDate" SkinID="TextBoxCalendarExtenderNormal" TabIndex="2" runat="server"
                            onblur="return checkDateToDate(this.id);"></asp:TextBox>
                        <ajaxToolkit:CalendarExtender ID="calTodate" Format="dd/MM/yyyy" runat="server" TargetControlID="txtToDate">
                        </ajaxToolkit:CalendarExtender>
                    </td>
                </tr>
            </table>
        </div>
        <div id="ChequeWise" runat="server">
            <table class="reportFiltersTable">
                <tr>
                    <td class="label">
                        <asp:Label ID="lblChequeFrom" runat="server" Text="Cheque# From" SkinID="LabelNormal"></asp:Label>
                        <span class="asterix">*</span>
                    </td>
                    <td class="inputcontrols">
                        <asp:TextBox ID="txtChequeFrom" SkinID="TextBoxNormal" runat="server"></asp:TextBox>
                    </td>
                    <td class="label">
                        <asp:Label ID="lblChequeTo" runat="server" Text="To" SkinID="LabelNormal"></asp:Label>
                        <span class="asterix">*</span>
                    </td>
                    <td class="inputcontrols">
                        <asp:TextBox ID="txtChequeTo" SkinID="TextBoxNormal" runat="server"></asp:TextBox>
                    </td>
                </tr>
            </table>
        </div>
        <div class="reportButtons">
            <asp:Button SkinID="ButtonNormal" ID="btnreport" runat="server" Text="Report" TabIndex="3"
                OnClientClick="javaScript:return fnValidate();" OnClick="btnreport_Click" />
            <asp:Button ID="btnReset" runat="server" CausesValidation="false" SkinID="ButtonNormal"
                Text="Reset" OnClick="btnReset_Click" />
        </div>
    </div>
    <div class="reportViewerHolder" id="reportViewerHolder" runat="server">
        <UC:CrystalReport ID="crCorpChequeSlipDetails" runat="server" />
    </div>
</asp:Content>
