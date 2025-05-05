<%@ Page Language="C#" MasterPageFile="~/Main.Master" AutoEventWireup="true" CodeBehind="RoadPermitIssued.aspx.cs"
    Inherits="IMPALWeb.Reports.Ordering.Listing.RoadPermitIssued" %>

<%@ Register Src="~/UserControls/CrystalReport.ascx" TagName="CrystalReport" TagPrefix="UC" %>
<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="ajax" %>
<asp:Content ID="Content1" ContentPlaceHolderID="CPHDetails" runat="server">

    <script language="javascript" type="text/javascript">
        function fnValidate() {
            var txtFromDate = document.getElementById('<%=txtFromDate.ClientID%>');
            var txtToDate = document.getElementById('<%=txtToDate.ClientID%>');
            return ValidateDates(txtFromDate, txtToDate);
        }
    </script>

    <div class="reportFormTitle">
        Road Permit Issued Details</div>
    <div class="reportFilters">
        <table id="reportFiltersTable" class="reportFiltersTable" runat="server">
            <tr>
                <td class="label">
                    <asp:Label ID="lblFromDate" runat="server" Text="From Date" SkinID="LabelNormal"></asp:Label><span
                        class="asterix">*</span>
                </td>
                <td class="inputcontrols">
                    <asp:TextBox ID="txtFromDate" runat="server" TabIndex="1" SkinID="TextBoxCalendarExtenderNormal"></asp:TextBox>
                    <ajax:CalendarExtender ID="calFromDate" Format="dd/MM/yyyy" TargetControlID="txtFromDate"
                        runat="server">
                    </ajax:CalendarExtender>
                </td>
                <td class="label">
                    <asp:Label ID="lblToDate" runat="server" Text="To Date" SkinID="LabelNormal"></asp:Label><span
                        class="asterix">*</span>
                </td>
                <td class="inputcontrols">
                    <asp:TextBox ID="txtToDate" runat="server" TabIndex="2" SkinID="TextBoxCalendarExtenderNormal"></asp:TextBox>
                    <ajax:CalendarExtender ID="calToDate" Format="dd/MM/yyyy" TargetControlID="txtToDate"
                        runat="server">
                    </ajax:CalendarExtender>
                </td>
            </tr>
        </table>
        <div class="reportButtons">
            <asp:Button ID="btnReport" runat="server" Text="Generate Report" TabIndex="3" OnClientClick="javaScript:return fnValidate();"
                SkinID="ButtonViewReport" OnClick="btnReport_Click" />
        </div>
    </div>
    <div class="reportViewerHolder" id="reportViewerHolder" runat="server">
        <UC:CrystalReport ID="crRoadPermitIssued" runat="server" OnUnload="crRoadPermitIssued_Unload" ReportName="RoadPermitIssued" />
    </div>
</asp:Content>
