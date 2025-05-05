<%@ Page Title="" Language="C#" MasterPageFile="~/Main.Master" AutoEventWireup="true"
    CodeBehind="SaleValueAging.aspx.cs" Inherits="IMPALWeb.Reports.Sales.Sales_Statement.SaleValueAging" %>
<%@ Register Src="~/UserControls/CrystalReport.ascx" TagName="CrystalReport" TagPrefix="uc1" %>
<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="ajaxToolkit" %>
<asp:Content ID="Content1" ContentPlaceHolderID="CPHDetails" runat="server">

    <script language="javascript" type="text/javascript">
        function Validate(e) {
  
            var txtFromDate = document.getElementById('<%=txtFromDate.ClientID%>');
            var txtToDate = document.getElementById('<%=txtToDate.ClientID%>');
          
            return ValidateDates(txtFromDate, txtToDate);
        }
    </script>

    <div class="reportFormTitle">
        Sales Value-Aging
    </div>
    <div class="reportFilters">
        <table id="reportFiltersTable" class="reportFiltersTable" runat="server">
            <tr>
                <td class="label">
                    <asp:Label ID="lblFromDate" Text="From Date" runat="server" SkinID="LabelNormal"></asp:Label><span
                        class="asterix">*</span>
                </td>
                <td class="inputcontrols">
                    <asp:TextBox ID="txtFromDate" runat="server" TabIndex="1" SkinID="TextBoxCalendarExtenderNormal"></asp:TextBox>
                    <ajaxToolkit:CalendarExtender ID="calFromDate" runat="server" Format="dd/MM/yyyy"
                        TargetControlID="txtFromDate" />
                </td>
                <td class="label">
                    <asp:Label ID="lblToDate" Text="To Date" runat="server" SkinID="LabelNormal"></asp:Label>
                    <span class="asterix">*</span>
                </td>
                <td class="inputcontrols">
                    <asp:TextBox ID="txtToDate" runat="server" TabIndex="2" SkinID="TextBoxCalendarExtenderNormal"></asp:TextBox>
                    <ajaxToolkit:CalendarExtender ID="calToDate" runat="server" Format="dd/MM/yyyy" TargetControlID="txtToDate" />
                </td>
                <td class="label">
                    <asp:Label ID="lblSupplier" Text="Supplier Code" runat="server" SkinID="LabelNormal"></asp:Label>
                </td>
                <td class="inputcontrols">
                    <asp:DropDownList ID="ddlSupplier" runat="server" SkinID="DropDownListNormal" TabIndex="3">
                    </asp:DropDownList>
                </td>
            </tr>
            <tr>
                <td class="label">
                    <asp:Label ID="lblSaleType" Text="Sale Type" runat="server" SkinID="LabelNormal"></asp:Label>
                </td>
                <td class="inputcontrols">
                    <asp:DropDownList ID="ddlSaleType" runat="server" SkinID="DropDownListNormal" TabIndex="4">
                    </asp:DropDownList>
                </td>
                <td class="label">
                    <asp:Label ID="lblReportType" Text="Report Type" runat="server" SkinID="LabelNormal"></asp:Label>
                </td>
                <td class="inputcontrols">
                    <asp:DropDownList ID="ddlReportType" runat="server" SkinID="DropDownListNormal" TabIndex="5">
                    </asp:DropDownList>
                </td>
            </tr>
        </table>
        <div class="reportButtons">
            <asp:Button ID="btnReport" runat="server" Text="Generate Report" TabIndex="6" SkinID="ButtonViewReport"
                OnClick="btnReport_Click" OnClientClick="javaScript:return Validate(this);" />
        </div>
    </div>
    <div class="reportViewerHolder" id="reportViewerHolder" runat="server">
        <uc1:CrystalReport runat="server" ID="crSaleValue" OnUnload="crSaleValue_Unload" />
    </div>
</asp:Content>
