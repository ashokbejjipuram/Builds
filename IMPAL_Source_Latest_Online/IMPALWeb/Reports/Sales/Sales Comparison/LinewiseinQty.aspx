<%@ Page Title="" Language="C#" MasterPageFile="~/Main.Master" AutoEventWireup="true"
    CodeBehind="LinewiseinQty.aspx.cs" Inherits="IMPALWeb.Reports.Sales.Sales_Comparison.LinewiseinQty" %>

<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="a" %>
<%@ Register Src="~/UserControls/CrystalReport.ascx" TagName="CrystalReport" TagPrefix="uc1" %>
<asp:Content ID="Content1" ContentPlaceHolderID="CPHDetails" runat="server">

    <script type="text/javascript" src="../../../Javascript/Validation.js"> </script>

    <script type="text/javascript">      function Validatenull() {

          var drp = document.getElementById('<%=ddlYear.ClientID %>');
          var linecode = document.getElementById('<%=ddlLinecode.ClientID %>');
          if (drp.value.trim() == null || drp.value.trim() == "" || drp.value==-1) {
              alert("Current Year Should not be empty");
              return false;

          }
          if (linecode.value.trim() == null || linecode.value.trim() == "" || linecode.value == 0) {
              alert("Line Code Should not be empty");
              return false;

          } 
      }</script>

    <div class="reportFormTitle">
        Line Wise in Quantity
    </div>
    <div class="reportFilters">
        <table id="reportFiltersTable" class="reportFiltersTable" runat="server">
            <tr>
                <td class="label">
                    <asp:Label ID="lblcurrentyear" runat="server" Text="Current Year" SkinID="LabelNormal"></asp:Label>
                    <span class="asterix">*</span>
                </td>
                <td class="inputcontrols">
                    <asp:DropDownList ID="ddlYear" runat="server" TabIndex="1" SkinID="DropDownListNormal">
                    </asp:DropDownList>
                </td>
                <td class="label">
                    <asp:Label ID="lbllinecode" runat="server" Text="Line Code" SkinID="LabelNormal"></asp:Label>
                    <span class="asterix">*</span>
                </td>
                <td class="inputcontrols">
                    <asp:DropDownList ID="ddlLinecode" runat="server" DataSourceID="ODlinecode" TabIndex="2"
                        SkinID="DropDownListNormal" DataTextField="SupplierName" DataValueField="SupplierCode">
                    </asp:DropDownList>
                </td>
            </tr>
        </table>
        <div class="reportButtons">
            <asp:Button ID="btnReport" Text="Generate Report" runat="server" SkinID="ButtonViewReport"
                OnClick="btnReport_Click" TabIndex="2" OnClientClick="javaScript:return Validatenull();" />
        </div>
    </div>
    <div class="reportViewerHolder" id="reportViewerHolder" runat="server">
        <uc1:CrystalReport ID="crlinewise" runat="server" ReportName="linewisesales_compqty" OnUnload="crlinewise_Unload" />
    </div>
    <asp:ObjectDataSource ID="ODlinecode" runat="server" SelectMethod="GetAllSuppliers"
        TypeName="IMPALLibrary.Suppliers"></asp:ObjectDataSource>
</asp:Content>
