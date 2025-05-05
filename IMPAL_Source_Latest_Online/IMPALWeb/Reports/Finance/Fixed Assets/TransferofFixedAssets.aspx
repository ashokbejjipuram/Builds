<%@ Page Title="" Language="C#" MasterPageFile="~/Main.Master" AutoEventWireup="true"
    CodeBehind="TransferofFixedAssets.aspx.cs" Inherits="IMPALWeb.Reports.Finance.Fixed_Assets.TransferofFixedAssets" %>

<%@ Register Src="~/UserControls/CrystalReport.ascx" TagName="CrystalReport" TagPrefix="uc1" %>
<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="ajaxToolkit" %>
<asp:Content ID="Content1" ContentPlaceHolderID="CPHDetails" runat="server">

    <script type="text/javascript">
        function Validate() {
            var fromdate = document.getElementById('<%=txtFromTransferDate.ClientID %>');
            var todate = document.getElementById('<%=txtToTransferDate.ClientID %>');
            var flag = ValidateDates(fromdate, todate);
            return (flag);
            
        }
    </script>

    <div class="reportFormTitle">
        Transfer of Fixed Assets
    </div>
    <div class="reportFilters">
        <table id="reportFiltersTable" class="reportFiltersTable" runat="server">
            <tr>
                <td class="label">
                    <asp:Label ID="lblFromTransferDate" runat="server" Text="From Transfer Date" SkinID="LabelNormal"></asp:Label>
                    <span class="asterix">*</span>
                </td>
                <td class="inputcontrols">
                    <asp:TextBox ID="txtFromTransferDate" runat="server" SkinID="TextBoxCalendarExtenderNormal"
                        TabIndex="1" />
                    <ajaxToolkit:CalendarExtender ID="calFromTransferDate" runat="server" TargetControlID="txtFromTransferDate"
                        Format="dd/MM/yyyy" />
                </td>
                <td class="label">
                    <asp:Label ID="lblToTransferDate" runat="server" Text="To Transfer Date" SkinID="LabelNormal"></asp:Label>
                    <span class="asterix">*</span>
                </td>
                <td class="inputcontrols">
                    <asp:TextBox ID="txtToTransferDate" runat="server" SkinID="TextBoxCalendarExtenderNormal"
                        TabIndex="2" />
                    <ajaxToolkit:CalendarExtender ID="calToTransferDate" runat="server" TargetControlID="txtToTransferDate"
                        Format="dd/MM/yyyy" />
                </td>
            </tr>
        </table>
        <div class="reportButtons">
            <asp:Button ID="btnReport" Text="Generate Report" runat="server" SkinID="ButtonViewReport"
                TabIndex="3" OnClick="btnReport_Click" OnClientClick="javaScript:return Validate();" />
        </div>
    </div>
    <div class="reportViewerHolder" id="reportViewerHolder" runat="server">
        <uc1:CrystalReport runat="server" ID="crtransferoffixedasset" OnUnload="crtransferoffixedasset_Unload" />
    </div>
</asp:Content>
