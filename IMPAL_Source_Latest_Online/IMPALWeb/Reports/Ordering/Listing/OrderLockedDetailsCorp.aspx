<%@ Page Language="C#" MasterPageFile="~/Main.Master" AutoEventWireup="true" CodeBehind="OrderLockedDetailsCorp.aspx.cs"
    Inherits="IMPALWeb.Reports.Ordering.Listing.OrderLockedDetailsCorp" %>

<%@ Register Src="~/UserControls/CrystalReport.ascx" TagName="CrystalReport" TagPrefix="uc1" %>
<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="ajaxToolkit" %>
<asp:Content ID="CPHDetails" ContentPlaceHolderID="CPHDetails" runat="server">

    <script language="javascript" type="text/javascript">
        function fnValidate() {
            var txtFromDate = document.getElementById('<%=txtFromDate.ClientID%>');
            var txtToDate = document.getElementById('<%=txtToDate.ClientID%>');
            return ValidateDates(txtFromDate, txtToDate);
        }
     
    </script>

    <div class="reportFormTitle">
        Order Locked Details (By Corp)</div>
    <div class="reportFilters">
        <table id="reportFiltersTable" class="reportFiltersTable" runat="server">
            <tr>
                <td class="label">
                    <asp:Label SkinID="LabelNormal" runat="server" ID="lblFromDate" Text="From Date" /><span
                        class="asterix">*</span>
                </td>
                <td class="inputcontrols">
                    <asp:TextBox ID="txtFromDate" runat="server" TabIndex="1" SkinID="TextBoxCalendarExtenderNormal" onblur="return CheckValidDate(this.id, true,'From Date');"></asp:TextBox>
                    <ajaxToolkit:CalendarExtender ID="calFromDate" Format="dd/MM/yyyy" runat="server"
                        TargetControlID="txtFromDate">
                    </ajaxToolkit:CalendarExtender>
                </td>
                <td class="label">
                    <asp:Label ID="lblToDate" runat="server" SkinID="LabelNormal" Text="To Date" /><span
                        class="asterix">*</span>
                </td>
                <td class="inputcontrols">
                    <asp:TextBox ID="txtToDate" runat="server" TabIndex="2" SkinID="TextBoxCalendarExtenderNormal" onblur="return CheckValidDate(this.id, true,'To Date');"></asp:TextBox>
                    <ajaxToolkit:CalendarExtender ID="calToDate" Format="dd/MM/yyyy" runat="server" TargetControlID="txtToDate">
                    </ajaxToolkit:CalendarExtender>
                </td>
            </tr>
        </table>
        <div class="reportButtons">
            <asp:Button ID="btnReport" runat="server" Text="Generate Report" OnClientClick="javaScript:return fnValidate();"
                OnClick="btnReport_Click" TabIndex="3" SkinID="ButtonViewReport" />
        </div>
    </div>
    <div class="reportViewerHolder" id="reportViewerHolder" runat="server">
        <uc1:CrystalReport runat="server" ID="OrderLockCorp" OnUnload="OrderLockCorp_Unload" />
    </div>
</asp:Content>
