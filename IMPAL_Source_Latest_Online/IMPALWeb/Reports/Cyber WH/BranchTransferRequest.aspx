<%@ Page Language="C#" AutoEventWireup="true" MasterPageFile="~/Main.Master" CodeBehind="BranchTransferRequest.aspx.cs"
    Inherits="IMPALWeb.Reports.Cyber_WH.BranchTransferRequest" %>

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
        Branch Transfer Request</div>
    <div class="reportFilters">
        <table id="reportFiltersTable" class="reportFiltersTable" runat="server">
            <tr>
                <td class="label">
                    <asp:Label ID="lblPONumber" runat="server" Text="PO Number" SkinID="LabelNormal" />
                    <span class="asterix">*</span>
                </td>
                <td class="inputcontrols">
                    <asp:DropDownList ID="ddlPONumber" runat="server" TabIndex="3" SkinID="DropDownListNormal" />
                    <%-- <asp:ObjectDataSource ID="ODPONumber" runat="server" SelectMethod="GetPONumber" TypeName="IMPALLibrary.Masters.PONumber.PONumbers">
                                        </asp:ObjectDataSource>--%>
                </td>
            </tr>
        </table>
        <div class="reportButtons">
            <asp:Button ID="btnReport" runat="server" Text="Generate Report" OnClientClick="javaScript:return Validate();"
                TabIndex="4" SkinID="ButtonViewReport" OnClick="btnReport_Click" />
        </div>
    </div>
    <div class="reportViewerHolder" id="reportViewerHolder" runat="server">
        <UC:CrystalReport ID="crBranchTransferRequest" runat="server" OnUnload="crBranchTransferRequest_Unload" ReportName="Impal-report-branch_transfer_request" />
    </div>
</asp:Content>
