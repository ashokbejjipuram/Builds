<%@ Page Language="C#" MasterPageFile="~/Main.Master" AutoEventWireup="true" CodeBehind="CancelledOrderDetails.aspx.cs"
    Inherits="IMPALWeb.Reports.Ordering.Listing.CancelledOrderDetails" %>

<%@ Register Src="~/UserControls/CrystalReport.ascx" TagName="CrystalReport" TagPrefix="UC" %>
<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="ajax" %>
<asp:Content ID="Content1" ContentPlaceHolderID="CPHDetails" runat="server">

    <script language="javascript" type="text/javascript">
        function fnValidate() {
            var txtFromDate = document.getElementById('<%=txtFromDate.ClientID%>');
            var txtToDate = document.getElementById('<%=txtToDate.ClientID%>');
            var ddlFromLine = document.getElementById('<%=ddlFromLineCd.ClientID %>');
            var ddlToLine = document.getElementById('<%=ddlToLineCd.ClientID%>');

            if (ddlToLine.value != "0" && ddlFromLine.value == "0") {
                ValidateFromLine(ddlFromLine, ddlToLine);
                return false;
            }
            else
                return ValidateOrderDate(txtFromDate, txtToDate);
        }
    </script>

    <div class="reportFormTitle ">
        Cancelled Order Details</div>
    <div class="reportFilters">
        <table id="reportFiltersTable" class="reportFiltersTable" runat="server">
            <tr>
                <td class="label">
                    <asp:Label ID="lblFromLineCd" runat="server" Text="From Line Code" SkinID="LabelNormal"></asp:Label>
                </td>
                <td class="inputcontrols">
                    <asp:DropDownList ID="ddlFromLineCd" runat="server" DataSourceID="ODSuppliers" DataTextField="SupplierName"
                        DataValueField="SupplierCode" TabIndex="1" SkinID="DropDownListNormal">
                    </asp:DropDownList>
                    <asp:ObjectDataSource ID="ODSuppliers" runat="server" SelectMethod="Getlinewiseorder"
                        TypeName="IMPALLibrary.Suppliers">
                        <SelectParameters>
                            <asp:SessionParameter Name="strBranchCode" SessionField="BranchCode" />
                        </SelectParameters>
                    </asp:ObjectDataSource>
                </td>
                <td class="label">
                    <asp:Label ID="lblToLineCd" runat="server" Text="To Line Code" SkinID="LabelNormal"></asp:Label>
                </td>
                <td class="inputcontrols">
                    <asp:DropDownList ID="ddlToLineCd" runat="server" DataSourceID="ODToSuppliers" DataTextField="SupplierName"
                        DataValueField="SupplierCode" TabIndex="2" SkinID="DropDownListNormal">
                    </asp:DropDownList>
                    <asp:ObjectDataSource ID="ODToSuppliers" runat="server" SelectMethod="Getlinewiseorder"
                        TypeName="IMPALLibrary.Suppliers">
                        <SelectParameters>
                            <asp:SessionParameter Name="strBranchCode" SessionField="BranchCode" />
                        </SelectParameters>
                    </asp:ObjectDataSource>
                </td>
                <td class="label">
                    <asp:Label ID="lblFromDate" runat="server" Text="From Date" SkinID="LabelNormal"></asp:Label><span
                        class="asterix">*</span>
                </td>
                <td class="inputcontrols">
                    <asp:TextBox ID="txtFromDate" runat="server" TabIndex="3" SkinID="TextBoxCalendarExtenderNormal"></asp:TextBox>
                    <ajax:CalendarExtender ID="calFromDate" Format="dd/MM/yyyy" TargetControlID="txtFromDate"
                        runat="server">
                    </ajax:CalendarExtender>
                </td>
            </tr>
            <tr>
                <td class="label">
                    <asp:Label ID="lblToDate" runat="server" Text="To Date" SkinID="LabelNormal"></asp:Label><span
                        class="asterix">*</span>
                </td>
                <td class="inputcontrols">
                    <asp:TextBox ID="txtToDate" runat="server" TabIndex="4" SkinID="TextBoxCalendarExtenderNormal"></asp:TextBox>
                    <ajax:CalendarExtender ID="calToDate" Format="dd/MM/yyyy" TargetControlID="txtToDate"
                        runat="server">
                    </ajax:CalendarExtender>
                </td>
                <td class="label">
                    <asp:Label ID="lblReportType" runat="server" Text="Report Type" SkinID="LabelNormal"></asp:Label>
                </td>
                <td class="inputcontrols">
                    <asp:DropDownList ID="ddlReportType" runat="server" TabIndex="5" SkinID="DropDownListNormal">
                    </asp:DropDownList>
                </td>
            </tr>
        </table>
        <div class="reportButtons">
            <asp:Button ID="btnReport" runat="server" Text="Generate Report" OnClientClick="javaScript:return fnValidate();"
                OnClick="btnReport_Click" TabIndex="6" SkinID="ButtonViewReport" />
        </div>
    </div>
    <div class="reportViewerHolder" id="reportViewerHolder" runat="server">
        <UC:CrystalReport ID="crCancelledOrderDetails_HO" runat="server" OnUnload="crCancelledOrderDetails_HO_Unload" ReportName="CancelledOrderDetails_HO" />
    </div>
</asp:Content>
