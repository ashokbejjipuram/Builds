<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="MiniWorkSheet.aspx.cs"
    Inherits="IMPALWeb.Reports.Sales.SalesAnalysis.MiniWorkSheet" MasterPageFile="~/Main.Master" %>

<%@ Register Src="~/UserControls/CrystalReport.ascx" TagName="CrystalReport" TagPrefix="UC" %>
<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="ajaxToolkit" %>
<asp:Content ID="MainContent" ContentPlaceHolderID="CPHDetails" runat="server">

    <script language="javascript" type="text/javascript">
        function Validate() {
            var txtSupplier = document.getElementById('<%=ddlLineCode.ClientID%>');

            if (txtSupplier.value.trim() == "0") {
                alert("Supplier Line should be selected");
                txtSupplier.focus();
                return false;
            }
        }
    </script>

    <div class="reportFormTitle">
        Mini WorkSheet</div>
    <div class="reportFilters">
        <table class="reportFiltersTable" id="reportFiltersTable" runat="server">
            <tr>
                <td class="label">
                    <asp:Label SkinID="LabelNormal" Text="Supplier Line" runat="server"></asp:Label>
                    <span class="asterix">*</span>
                </td>
                <td class="inputcontrols">
                    <asp:DropDownList ID="ddlLineCode" runat="server" TabIndex="3" SkinID="DropDownListNormal"
                        DataSourceID="ODLine" DataTextField="SupplierName" DataValueField="SupplierCode" />
                    <asp:ObjectDataSource ID="ODLine" runat="server" SelectMethod="GetAllSuppliers" TypeName="IMPALLibrary.Suppliers" />
                </td>
            </tr>
        </table>
        <div class="reportButtons">
            <asp:Button ID="btnReport" runat="server" Text="Generate Report" OnClick="btnReport_Click"
                SkinID="ButtonViewReport" OnClientClick="javaScript:return Validate();" TabIndex="6" />
        </div>
    </div>
</asp:Content>
