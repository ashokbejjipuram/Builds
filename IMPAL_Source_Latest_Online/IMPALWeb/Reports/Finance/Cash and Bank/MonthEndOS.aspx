<%@ Page Language="C#" MasterPageFile="~/Main.Master" AutoEventWireup="true" CodeBehind="MonthEndOS.aspx.cs"
    Inherits="IMPALWeb.Reports.Finance.Cash_and_Bank.MonthEndOS" %>

<%@ Register Src="~/UserControls/CrystalReport.ascx" TagName="CrystalReport" TagPrefix="uc1" %>
<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="ajaxToolkit" %>
<asp:Content ID="CPHDetails" ContentPlaceHolderID="CPHDetails" runat="server">

    <script language="javascript" type="text/javascript">
        function fnValidate() {
            var txtFromDate = document.getElementById('<%=txtFromDate.ClientID%>');
            return ValidateOutstandingDate(txtFromDate);
        }
     
    </script>

    <div class="reportFormTitle">
        MonthEnd Outstanding %</div>
    <div class="reportFilters">
        <table id="reportFiltersTable" class="reportFiltersTable" runat="server">
            <tr>
                <td class="label">
                    <asp:Label ID="lblbranchcode" runat="server" SkinID="LabelNormal" Text="Branch Code"></asp:Label><span
                        class="asterix">*</span>
                </td>
                <td class="inputcontrols">
                    <asp:DropDownList SkinID="DropDownListNormal" TabIndex="1" ID="ddlBranchCode" runat="server">
                    </asp:DropDownList>
                </td>
                <td class="label">
                    <asp:Label SkinID="LabelNormal" runat="server" ID="lblDate" Text="Date" /><span class="asterix">*</span>
                </td>
                <td class="inputcontrols">
                    <asp:TextBox ID="txtFromDate" runat="server" TabIndex="2" SkinID="TextBoxCalendarExtenderNormal"></asp:TextBox>
                    <ajaxToolkit:CalendarExtender ID="calDate" Format="dd/MM/yyyy" runat="server" TargetControlID="txtFromDate">
                    </ajaxToolkit:CalendarExtender>
                </td>
                <td class="label">
                    <asp:Label SkinID="LabelNormal" ID="lblReporttype" runat="server" Text="Report Type"></asp:Label>
                </td>
                <td class="inputcontrols">
                    <asp:DropDownList SkinID="DropDownListNormal" TabIndex="3" ID="ddlReportType" runat="server">
                    </asp:DropDownList>
                </td>
            </tr>
        </table>
        <div class="reportButtons">
            <asp:Button ID="btnReport" runat="server" Text="Generate Report" OnClientClick="javaScript:return fnValidate();"
                OnClick="btnReport_Click" TabIndex="4" SkinID="ButtonViewReport" />
        </div>
    </div>
    <div class="reportViewerHolder" id="reportViewerHolder" runat="server">
        <uc1:CrystalReport runat="server" ID="crMonthEnd" OnUnload="crMonthEnd_Unload" ReportName="MonthEndOS" />
    </div>
</asp:Content>
