<%@ Page Title="" Language="C#" MasterPageFile="~/Main.Master" AutoEventWireup="true"
    CodeBehind="StockMonthlyStatement.aspx.cs" Inherits="IMPALWeb.Reports.Finance.Account_Receivable.StockMonthlyStatement" %>

<%@ Register Src="~/UserControls/CrystalReport.ascx" TagName="CrystalReport" TagPrefix="uc1" %>
<asp:Content ID="Content1" ContentPlaceHolderID="CPHDetails" runat="server">

    <script type="text/javascript">
    function validateMonthYear() {
        var Monthyear = document.getElementById('<%=ddlMonthYear.ClientID%>');
          var Branch = document.getElementById('<%=ddlBranch.ClientID%>');
          if (Monthyear.value == null || Monthyear.value == "" || Monthyear.value == "0") {
              alert("Month Year should not be null");
              return false;
          }
          else if (Branch.value == null || Branch.value == "" || Branch.value == "0") {
              alert("Branch should not be null");
              return false;
          }
    }

    </script>

    <div class="reportFormTitle reportFormTitleExtender350">
        Stock/Debtors Monthly Statement
    </div>
    <div class="reportFilters">
        <table id="reportFiltersTable" class="reportFiltersTable" runat="server">
            <tr>
                <td class="label">
                    <asp:Label ID="lblMonthYear" runat="server" SkinID="LabelNormal" Text="Month Year"></asp:Label>
                    <span class="asterix">*</span>
                </td>
                <td class="inputcontrols">
                    <asp:DropDownList ID="ddlMonthYear" runat="server" SkinID="DropDownListNormal" TabIndex="1">
                    </asp:DropDownList>
                </td>
                <td class="label">
                    <asp:Label ID="lblBranch" runat="server" SkinID="LabelNormal" Text="Branch"></asp:Label>
                    <span class="asterix">*</span>
                </td>
                <td class="inputcontrols">
                    <asp:DropDownList ID="ddlBranch" runat="server" SkinID="DropDownListNormal" TabIndex="2">
                    </asp:DropDownList>
                </td>
            </tr>
        </table>
        <div class="reportButtons">
            <asp:Button ID="btnReport" runat="server" Text="Generate Report" SkinID="ButtonViewReport"
                TabIndex="3" OnClick="btnReport_Click" OnClientClick="javaScript:return validateMonthYear();" />
        </div>
    </div>
    <div class="reportViewerHolder" id="reportViewerHolder" runat="server">
        <uc1:CrystalReport ID="crMonthlyStatement" runat="server" OnUnload="crMonthlyStatement_Unload" />
    </div>
</asp:Content>
