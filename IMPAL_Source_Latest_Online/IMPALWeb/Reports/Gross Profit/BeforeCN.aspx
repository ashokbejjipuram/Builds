<%@ Page Language="C#" MasterPageFile="~/Main.Master" AutoEventWireup="true" CodeBehind="BeforeCN.aspx.cs"
    Inherits="IMPALWeb.Reports.Gross_Profit.BeforeCN" %>

<%@ Register Src="~/UserControls/CrystalReport.ascx" TagName="CrystalReport" TagPrefix="UC" %>
<asp:Content ID="Content1" ContentPlaceHolderID="CPHDetails" runat="server">

    <script language="javascript" type="text/javascript">
    function fnValidate() {
        var ddlMonYr = document.getElementById('<%=ddlMonYr.ClientID%>');
        if (ddlMonYr.value == null || ddlMonYr.value == "") {
            alert("Month Year should not be null !");
            ddlMonYr.focus();
            return false;
        }
        var ddlBSZ = document.getElementById('<%=ddlBSZ.ClientID %>');
        if (ddlBSZ.value == null || ddlBSZ.value == "") {
            alert("Branch/State/Zone should not be null !");
            ddlBSZ.focus();
            return false;
        }
    }
    </script>

    <div class="reportFormTitle">
        Before CN</div>
    <div class="reportFilters">
        <table id="reportFiltersTable" class="reportFiltersTable" runat="server">
            <tr>
                <td class="label">
                    <asp:Label ID="lblMonYr" runat="server" Text="Month Year" SkinID="LabelNormal"></asp:Label><span
                        class="asterix">*</span>
                </td>
                <td class="inputcontrols">
                    <asp:DropDownList ID="ddlMonYr" runat="server" SkinID="DropDownListNormal" TabIndex="1">
                    </asp:DropDownList>
                </td>
                <td class="label">
                    <asp:Label ID="lblBSZ" runat="server" Text="Branch/State/Zone" SkinID="LabelNormal"></asp:Label><span
                        class="asterix">*</span>
                </td>
                <td class="inputcontrols">
                    <asp:DropDownList ID="ddlBSZ" runat="server" SkinID="DropDownListNormal" TabIndex="2">
                    </asp:DropDownList>
                </td>
            </tr>
        </table>
        <div class="reportButtons">
            <asp:Button ID="btnReport" runat="server" Text="Generate Report" TabIndex="3" SkinID="ButtonViewReport"
                OnClientClick="javaScript:return fnValidate();" OnClick="btnReport_Click" />
        </div>
    </div>
    <div class="reportViewerHolder" id="reportViewerHolder" runat="server">
        <UC:CrystalReport ID="crBeforeCn_Branch" runat="server" OnUnload="crBeforeCn_Branch_Unload" ReportName="BeforeCn_Branch" />
    </div>
</asp:Content>
