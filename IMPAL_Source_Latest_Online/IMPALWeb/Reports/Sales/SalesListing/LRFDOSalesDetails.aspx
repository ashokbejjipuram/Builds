<%@ Page Language="C#" MasterPageFile="~/Main.Master" AutoEventWireup="true" CodeBehind="LRFDOSalesDetails.aspx.cs"
    Inherits="IMPALWeb.Reports.Sales.SalesListing.LRFDOSalesDetails" %>

<%@ Register Src="~/UserControls/CrystalReport.ascx" TagName="CrystalReport" TagPrefix="uc1" %>
<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="ajaxToolkit" %>
<asp:Content ID="Content1" ContentPlaceHolderID="CPHDetails" runat="server">

    <script language="javascript" type="text/javascript">
        function fnValidate() {
            var txtFromDate = document.getElementById('<%=txtFromDate.ClientID%>');
            var txtToDate = document.getElementById('<%=txtToDate.ClientID%>');
            return ValidateDates(txtFromDate, txtToDate);
       }
    </script>

    <div class="reportFormTitle">
        LR / FDO Sales Details</div>
    <div class="reportFilters">
        <table class="reportFiltersTable" id="reportFiltersTable" runat="server">
            <tr>
                <td class="label">
                    <asp:Label ID="lblZone" runat="server" SkinID="LabelNormal" Text="Zone"></asp:Label>
                </td>
                <td class="inputcontrols">
                    <asp:DropDownList ID="ddlZone" SkinID="DropDownListNormal" runat="server" TabIndex="1"
                        OnSelectedIndexChanged="ddlZone_SelectedIndexChanged" AutoPostBack="true">
                    </asp:DropDownList>
                </td>
                <td class="label">
                    <asp:Label ID="lblBranch" SkinID="LabelNormal" runat="server" Text="Branch"></asp:Label>
                </td>
                <td class="inputcontrols">
                    <asp:DropDownList ID="ddlBranch" TabIndex="2" SkinID="DropDownListNormal" runat="server" />
                </td>
            </tr>
            <tr>
                <td class="label">
                    <asp:Label ID="lblfromdate" Text="From Date" SkinID="LabelNormal" runat="server"></asp:Label><span
                        class="asterix">*</span>
                </td>
                <td class="inputcontrols">
                    <asp:TextBox ID="txtFromDate" SkinID="TextBoxCalendarExtenderNormal" TabIndex="3"
                        runat="server"></asp:TextBox>
                    <ajaxToolkit:CalendarExtender ID="calFromdate" Format="dd/MM/yyyy" runat="server"
                        TargetControlID="txtFromDate">
                    </ajaxToolkit:CalendarExtender>
                </td>
                <td class="label">
                    <asp:Label ID="lblToDate" Text="To Date" SkinID="LabelNormal" runat="server"></asp:Label><span
                        class="asterix">*</span>
                </td>
                <td class="inputcontrols">
                    <asp:TextBox ID="txtToDate" SkinID="TextBoxCalendarExtenderNormal" TabIndex="4" runat="server"></asp:TextBox>
                    <ajaxToolkit:CalendarExtender ID="calTodate" Format="dd/MM/yyyy" runat="server" TargetControlID="txtToDate">
                    </ajaxToolkit:CalendarExtender>
                </td>
            </tr>
        </table>
        <div class="reportButtons">
            <asp:Button ID="btnReport" SkinID="ButtonViewReport" runat="server" Text="Generate Report"
                TabIndex="5" OnClick="btnReport_Click" OnClientClick="javaScript:return fnValidate();" />
        </div>
    </div>
    <div class="reportViewerHolder" id="reportViewerHolder" runat="server">
        <uc1:CrystalReport ID="crLRFDOSalesDetails" runat="server" OnUnload="crLRFDOSalesDetails_Unload" />
    </div>
</asp:Content>
