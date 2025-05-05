<%@ Page Title="" Language="C#" MasterPageFile="~/Main.Master" AutoEventWireup="true"
    CodeBehind="TargetVsSales.aspx.cs" Inherits="IMPALWeb.Reports.Sales.Sales_Comparison.TargetVsSales" %>

<%@ Register Src="~/UserControls/CrystalReport.ascx" TagName="CrystalReport" TagPrefix="uc1" %>
<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="ajaxToolkit" %>
<asp:Content ID="Content1" ContentPlaceHolderID="CPHDetails" runat="server">

    <script type="text/javascript">
        function validateDate() {
            var txtDate = document.getElementById('<%=txtDate.ClientID%>');
            if (txtDate.value == null || txtDate.value == "") {
                alert("Date should not be null");
                txtDate.focus();
                return false;
            }
            else if (txtDate.value.trim() != null && fnIsDate(txtDate.value) == false) {
                txtDate.value = "";
                txtDate.focus();
                return false;
            }
        }
    </script>

    <div class="reportFormTitle">
        Target Vs Sales
    </div>
    <div class="reportFilters">
        <table id="reportFiltersTable" class="reportFiltersTable" runat="server">
            <tr>
                <td class="label">
                    <asp:Label ID="lblDate" runat="server" Text="Date" SkinID="LabelNormal"></asp:Label><span
                        id="astDate" class="asterix" runat="server" visible="false">*</span>
                </td>
                <td class="inputcontrols">
                    <asp:TextBox ID="txtDate" runat="server" SkinID="TextBoxCalendarExtenderNormal" TabIndex="4"></asp:TextBox>
                    <ajaxToolkit:CalendarExtender ID="calDate" Format="dd/MM/yyyy" TargetControlID="txtDate"
                        runat="server">
                    </ajaxToolkit:CalendarExtender>
                </td>
                <td class="label">
                    <asp:Label ID="lblReportType" runat="server" Text="Report Type" SkinID="LabelNormal"></asp:Label>
                </td>
                <td class="inputcontrols">
                    <asp:DropDownList ID="ddlReportType" runat="server" TabIndex="6" SkinID="DropDownListNormal">
                    </asp:DropDownList>
                </td>
            </tr>
        </table>
        <div class="reportButtons">
            <asp:Button ID="btnReport" Text="Generate Report" runat="server" SkinID="ButtonViewReport"
                OnClick="btnReport_Click" TabIndex="2" OnClientClick="javaScript:return validateDate();" />
        </div>
    </div>
    <div class="reportViewerHolder" id="reportViewerHolder" runat="server">
        <uc1:CrystalReport runat="server" ID="crtarget" OnUnload="crtarget_Unload" />
    </div>
</asp:Content>
