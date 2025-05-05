<%@ Page Language="C#" MasterPageFile="~/Main.Master" AutoEventWireup="true" CodeBehind="CoveringLetterForCA.aspx.cs"
    Inherits="IMPALWeb.Reports.Insurance.CoveringLetterForCA" %>

<%@ Register Src="~/UserControls/CrystalReport.ascx" TagName="CrystalReport" TagPrefix="UC" %>
<asp:Content ID="CPHDetails" ContentPlaceHolderID="CPHDetails" runat="server">

    <script language="javascript" type="text/javascript">
        function fnValidate() {
            var ddlClaimBillNumber = document.getElementById('<%=ddlClaimBillNumber.ClientID%>');
            return ValidateClaimNumb(ddlClaimBillNumber);
        }
    </script>

    <div class="reportFormTitle">
        Covering Letter for CA</div>
    <div class="reportFilters">
        <table id="reportFiltersTable" class="reportFiltersTable" runat="server">
            <tr>
                <td class="label">
                    <asp:Label SkinID="LabelNormal" ID="lblClaimbill" runat="server" Text="Claim Bill No."></asp:Label><span
                        class="asterix">*</span>
                </td>
                <td class="inputcontrols">
                    <asp:DropDownList SkinID="DropDownListNormal" ID="ddlClaimBillNumber" runat="server"
                        TabIndex="1">
                    </asp:DropDownList>
                </td>
            </tr>
        </table>
        <div class="reportButtons">
            <asp:Button SkinID="ButtonViewReport" ID="btnReport" runat="server" Text="Generate Report"
                OnClientClick="javaScript:return fnValidate();" OnClick="btnReport_Click" />
        </div>
    </div>
    <div class="reportViewerHolder" id="reportViewerHolder" runat="server">
        <UC:CrystalReport runat="server" ID="crCoverLetterForCA" OnUnload="crCoverLetterForCA_Unload" ReportName="CoverLetterForCA" />
    </div>
</asp:Content>
