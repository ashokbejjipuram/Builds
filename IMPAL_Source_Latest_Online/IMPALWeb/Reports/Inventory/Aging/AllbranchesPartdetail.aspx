<%@ Page Title="" Language="C#" MasterPageFile="~/Main.Master" AutoEventWireup="true"
    CodeBehind="AllbranchesPartdetail.aspx.cs" Inherits="IMPALWeb.Reports.Inventory.Aging.AllbranchesPartdetail" %>

<%@ Register Src="~/UserControls/CrystalReport.ascx" TagName="CrystalReport" TagPrefix="uc1" %>
<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="ajaxToolkit" %>
<asp:Content ID="Content1" ContentPlaceHolderID="CPHDetails" runat="server">

    <script type="text/javascript">        function Validatedate() {

            var drp = document.getElementById('<%=ddlAging.ClientID %>');
            var txtFromDate = document.getElementById('<%=txtFromDate.ClientID %>');
            var txttodate = document.getElementById('<%=txtToDate.ClientID %>');
            if (drp.value.trim() == "5") {
                return ValidateDates(txtFromDate, txttodate);
            }
        }
    </script>

    <div class="reportFormTitle ">
        All Branches Part Details
    </div>
    <table id="reportFiltersTable" class="reportFiltersTable" runat="server">
        <tr>
            <td>
                <div class="reportFilters">
                    <table id="SubTable" class="reportFiltersTable">
                        <tr>
                            <td class="label">
                                <asp:Label ID="lblsuppliercode" runat="server" Text="Supplier Code" SkinID="LabelNormal"></asp:Label>
                            </td>
                            <td class="inputcontrols">
                                <asp:DropDownList ID="ddlSupplierCode" runat="server" DataSourceID="ODsuppliercode"
                                    SkinID="DropDownListNormal" DataTextField="SupplierName" DataValueField="SupplierCode"
                                    TabIndex="1">
                                </asp:DropDownList>
                            </td>
                            <td class="label">
                                <asp:Label ID="lblaging" runat="server" Text="Aging" SkinID="LabelNormal"></asp:Label>
                                <span class="asterix">*</span>
                            </td>
                            <td class="inputcontrols">
                                <asp:DropDownList ID="ddlAging" AutoPostBack="True" runat="server" SkinID="DropDownListNormal"
                                    TabIndex="2">
                                </asp:DropDownList>
                            </td>
                            <td class="label">
                                <asp:Label ID="lblrpttype" runat="server" Text="Report Type" SkinID="LabelNormal"></asp:Label>
                            </td>
                            <td class="inputcontrols">
                                <asp:DropDownList ID="ddlReportType" AutoPostBack="True" runat="server" SkinID="DropDownListNormal"
                                    TabIndex="3">
                                </asp:DropDownList>
                            </td>
                        </tr>
                        <tr>
                            <div class="reportFilters" id="divdate" runat="server">
                                <td class="label">
                                    <asp:Label ID="lblFromDate" runat="server" Text="From Date" SkinID="LabelNormal"></asp:Label>
                                    <span class="asterix">*</span>
                                </td>
                                <td class="inputcontrols">
                                    <asp:TextBox ID="txtFromDate" runat="server" TabIndex="4" SkinID="TextBoxCalendarExtenderNormal"></asp:TextBox>
                                    <asp:HiddenField ID="hidFromDate" runat="server" />
                                    <ajaxToolkit:CalendarExtender ID="calfromdate" runat="server" Format="dd/MM/yyyy"
                                        TargetControlID="txtFromDate" />
                                </td>
                                <td class="label">
                                    <asp:Label ID="lblToDate" runat="server" Text="To Date" SkinID="LabelNormal"></asp:Label>
                                    <span class="asterix">*</span>
                                </td>
                                <td class="inputcontrols">
                                    <asp:TextBox ID="txtToDate" runat="server" TabIndex="5" SkinID="TextBoxCalendarExtenderNormal"
                                        OnTextChanged="txtToDate_TextChanged"></asp:TextBox>
                                    <asp:HiddenField ID="hidToDate" runat="server" />
                                    <ajaxToolkit:CalendarExtender ID="caltodate" runat="server" Format="dd/MM/yyyy" TargetControlID="txtToDate" />
                                </td>
                            </div>
                        </tr>
                    </table>
                </div>
            </td>
        </tr>
    </table>
    <div class="reportButtons">
        <asp:Button ID="btnReport" Text="Generate Report" runat="server" SkinID="ButtonViewReport"
            OnClientClick="javaScript:return Validatedate();" TabIndex="6" OnClick="btnReport_Click" />
    </div>
    <div class="reportViewerHolder" id="reportViewerHolder" runat="server">
        <uc1:CrystalReport runat="server" ID="crallbranch" OnUnload="crallbranch_Unload" />
    </div>
    <asp:ObjectDataSource ID="ODsuppliercode" runat="server" SelectMethod="GetAllSuppliers"
        TypeName="IMPALLibrary.Suppliers"></asp:ObjectDataSource>
</asp:Content>
