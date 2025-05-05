<%@ Page Title="" Language="C#" MasterPageFile="~/Main.Master" AutoEventWireup="true"
    CodeBehind="FDNregister.aspx.cs" Inherits="IMPALWeb.Reports.Finance.Account_Receivable.FDNregister" %>

<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="ajaxToolkit" %>
<%@ Register Src="~/UserControls/CrystalReport.ascx" TagName="CrystalReport" TagPrefix="uc1" %>
<asp:Content ID="Content1" ContentPlaceHolderID="CPHDetails" runat="server">

    <script language="javascript" type="text/javascript">
        function Validate(e) {

            var txtFromDate = document.getElementById('<%=txtFromDate.ClientID%>');
            var txtToDate = document.getElementById('<%=txtToDate.ClientID%>');
            return ValidateDates(txtFromDate, txtToDate);
        }
    </script>

    <div class="reportFormTitle">
        FDN Register
    </div>
    <div class="reportFilters">
        <table id="reportFiltersTable" class="reportFiltersTable" runat="server">
            <tr>
                <td class="label">
                    <asp:Label ID="lblFromDate" runat="server" SkinID="LabelNormal" Text="From date"></asp:Label>
                    <span class="asterix">*</span>
                </td>
                <td class="inputcontrols">
                    <asp:TextBox ID="txtFromDate" runat="server" SkinID="TextBoxCalendarExtenderNormal"
                        TabIndex="1"></asp:TextBox>
                    <ajaxToolkit:CalendarExtender ID="calFromdate" runat="server" Format="dd/MM/yyyy"
                        TargetControlID="txtFromDate">
                    </ajaxToolkit:CalendarExtender>
                </td>
                <td class="label">
                    <asp:Label ID="lblTodate" runat="server" SkinID="LabelNormal" Text="To date"></asp:Label>
                    <span class="asterix">*</span>
                </td>
                <td class="inputcontrols">
                    <asp:TextBox ID="txtToDate" runat="server" SkinID="TextBoxCalendarExtenderNormal"
                        TabIndex="2"></asp:TextBox>
                    <ajaxToolkit:CalendarExtender ID="calToDate" runat="server" Format="dd/MM/yyyy" TargetControlID="txtToDate">
                    </ajaxToolkit:CalendarExtender>
                </td>
            </tr>
        </table>
        <div class="reportButtons">
            <asp:Button ID="btnReport" runat="server" Text="Generate Report" SkinID="ButtonViewReport"
                OnClientClick="javaScript:return Validate(this);" OnClick="btnReport_Click" TabIndex="3" />
        </div>
    </div>
    <div class="reportViewerHolder" id="reportViewerHolder" runat="server">
        <uc1:CrystalReport ID="crFDNregister" runat="server" OnUnload="crFDNregister_Unload" />
    </div>
</asp:Content>
