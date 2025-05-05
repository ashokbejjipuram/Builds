<%@ Page Language="C#" AutoEventWireup="true" MasterPageFile="~/Main.Master" CodeBehind="MissedOrder.aspx.cs"
    Inherits="IMPALWeb.Reports.Cyber_WH.MissedOrder" %>
    
<%@ Register Src="~/UserControls/CrystalReport.ascx" TagName="CrystalReport" TagPrefix="UC" %>
<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="ajax" %>
<asp:Content ID="MainContent" ContentPlaceHolderID="CPHDetails" runat="server">
    <script language="javascript" type="text/javascript">
        function Validate() {
            var ddlPONumber = document.getElementById('<%=ddlPONumber.ClientID%>');
            return ValidatePONumber(ddlPONumber);
           
        }
          
    </script>

    <div class="reportFormTitle">
        Missed Order</div>
    <div class="reportFilters">
        <table id="reportFiltersTable" class="reportFiltersTable" runat="server">
            <tr>
                <td class="label">
                    <asp:Label ID="lblPONumber" runat="server" Text="PO Number" SkinID="LabelNormal" />
                    <span class="asterix">*</span>
                </td>
                <td class="inputcontrols">
                    <asp:DropDownList ID="ddlPONumber" runat="server" TabIndex="1" SkinID="DropDownListNormal" />
                </td>
            </tr>
        </table>
        <div class="reportButtons">
            <asp:Button ID="btnReport" runat="server" Text="Generate Report" OnClientClick="javaScript:return Validate();"
                TabIndex="2" SkinID="ButtonViewReport" OnClick="btnReport_Click" />
        </div>
    </div>
    <div class="reportViewerHolder" id="reportViewerHolder" runat="server">
        <UC:CrystalReport ID="crMissedOrder" runat="server" OnUnload="crMissedOrder_Unload" ReportName="Impal-report-branch_transfer_missed_stmt_new" />
    </div>
</asp:Content>
