<%@ Page Language="C#" MasterPageFile="~/Main.Master" AutoEventWireup="true" CodeBehind="FixedAssetsSchedule.aspx.cs"
    Inherits="IMPALWeb.Reports.Finance.Fixed_Assets.FixedAssetsSchedule" %>

<%@ Register Src="~/UserControls/CrystalReport.ascx" TagName="CrystalReport" TagPrefix="UC" %>
<asp:Content ID="Content1" ContentPlaceHolderID="CPHDetails" runat="server">

    <script language="javascript" type="text/javascript">
        function fnValidate() {
            var ddlAccPeriod = document.getElementById('<%=ddlAccPeriod.ClientID%>');
            return validateacc(ddlAccPeriod);
       }
    </script>

    <div class="reportFormTitle">
        Fixed Assets Schedule</div>
    <div class="reportFilters">
        <table id="reportFiltersTable" class="reportFiltersTable" runat="server">
            <tr>
                <td class="label">
                    <asp:Label SkinID="LabelNormal" ID="lblAccPeriod" runat="server" Text="Accounting Period"></asp:Label><span
                        class="asterix">*</span>
                </td>
                <td class="inputcontrols">
                    <asp:DropDownList SkinID="DropDownListNormal" ID="ddlAccPeriod" runat="server" TabIndex="1">
                    </asp:DropDownList>
                </td>
            </tr>
        </table>
        <div class="reportButtons">
            <asp:Button SkinID="ButtonViewReport" ID="btnReport" runat="server" OnClick="btnReport_Click"
                TabIndex="2" OnClientClick="javaScript:return fnValidate();" Text="Generate Report" />
        </div>
    </div>
    <div class="reportViewerHolder" id="reportViewerHolder" runat="server">
        <UC:CrystalReport runat="server" ID="crFixedAssetsSchedule" OnUnload="crFixedAssetsSchedule_Unload" ReportName="FixedAssetsSchedule" />
    </div>
</asp:Content>
