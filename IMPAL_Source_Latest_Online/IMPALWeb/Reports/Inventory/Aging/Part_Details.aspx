<%@ Page Title="" Language="C#" MasterPageFile="~/Main.Master" AutoEventWireup="true"
    CodeBehind="Part_Details.aspx.cs" Inherits="IMPALWeb.Reports.Inventory.Aging.Part_Details" %>

<%@ Register Src="~/UserControls/CrystalReport.ascx" TagName="CrystalReport" TagPrefix="uc1" %>
<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="a" %>
<asp:Content ID="Content1" ContentPlaceHolderID="CPHDetails" runat="server">

    <script type="text/javascript">        
        function Validatenull() {

            var drp = document.getElementById('<%=ddsuppliercode.ClientID %>');
            if (drp.value.trim() == "0" || drp.value.trim() == "" || drp.value.trim() == null) {
                alert("Selected Supplier Code Should not be Empty");
                return false;
            }
        }
         function Validatedate() {
            var drp = document.getElementById('<%=ddaging.ClientID %>');
            var txtFromDate = document.getElementById('<%=txtFromDate.ClientID %>');
            var txttodate = document.getElementById('<%=txtToDate.ClientID %>');
            if (drp.value.trim() == "6") {
                return ValidateDates(txtFromDate, txttodate);
            }
        }
    </script>

    <div class="reportFormTitle">
        Part Details
    </div>
    <div class="reportFilters">
        <table id="reportFiltersTable" class="reportFiltersTable" runat="server">
            <tr>
                <td>
                    <table id="reportFiltersTable" class="reportFiltersTable">
                        <tr>
                            <td class="label">
                                <asp:Label ID="lblaging" Text="Aging" runat="server" SkinID="LabelNormal"></asp:Label>
                                <span class="asterix">*</span>
                            </td>
                            <td class="inputcontrols">
                                <asp:DropDownList runat="server" ID="ddaging" AutoPostBack="True" TabIndex="1" SkinID="DropDownListNormal"
                                    OnSelectedIndexChanged="ddaging_SelectedIndexChanged">
                                </asp:DropDownList>
                            </td>
                            <td class="label">
                                <asp:Label ID="lblsuppliercode" Text="Supplier Code" SkinID="LabelNormal" runat="server"></asp:Label>
                            </td>
                            <td class="inputcontrols">
                                <asp:DropDownList ID="ddsuppliercode" runat="server" DataSourceID="ODsuppliers" DataTextField="SupplierName"
                                    SkinID="DropDownListNormal" TabIndex="2" DataValueField="SupplierCode" AutoPostBack="True"
                                    OnSelectedIndexChanged="ddsuppliercode_SelectedIndexChanged">
                                </asp:DropDownList>
                                <asp:ObjectDataSource ID="ODsuppliers" runat="server" SelectMethod="GetAllSuppliers"
                                    TypeName="IMPALLibrary.Suppliers"></asp:ObjectDataSource>
                            </td>
                            <td class="label">
                                <asp:Label ID="lblReportType" Text="Report Type" SkinID="LabelNormal" runat="server"></asp:Label>
                            </td>
                            <td class="inputcontrols">
                                <asp:DropDownList ID="ddlreporttype" runat="server" SkinID="DropDownListNormal" TabIndex="3"
                                    OnSelectedIndexChanged="ddlreporttype_SelectedIndexChanged">
                                </asp:DropDownList>
                            </td>
                        </tr>
                        <tr>
                            <div class="reportFilters" id="divDate" runat="server">
                                <td class="label">
                                    <asp:Label ID="lblFromDate" SkinID="LabelNormal" Text="From Date" runat="server"></asp:Label>
                                    <span class="asterix">*</span>
                                </td>
                                <td class="inputcontrols">
                                    <asp:TextBox ID="txtFromDate" TabIndex="4" SkinID="TextBoxCalendarExtenderNormal"
                                        runat="server"></asp:TextBox>
                                    <asp:HiddenField ID="hidFromDate" runat="server" />
                                    <a:CalendarExtender ID="calfromdate" runat="server" Format="dd/MM/yyyy" TargetControlID="txtFromDate">
                                    </a:CalendarExtender>
                                </td>
                                <td class="label">
                                    <asp:Label ID="lblToDate" Text="To Date" runat="server" SkinID="LabelNormal"></asp:Label>
                                    <span class="asterix">*</span>
                                </td>
                                <td class="inputcontrols">
                                    <asp:TextBox ID="txtToDate" SkinID="TextBoxCalendarExtenderNormal" runat="server"
                                        TabIndex="5" />
                                    <asp:HiddenField ID="hidToDate" runat="server" />
                                    <a:CalendarExtender ID="caltodate" runat="server" Format="dd/MM/yyyy" TargetControlID="txtToDate">
                                    </a:CalendarExtender>
                                </td>
                            </div>
                        </tr>
                        <tr>
                            <td colspan="4">
                                &nbsp;
                            </td>
                        </tr>
                        <tr>
                            <td>
                                &nbsp;
                            </td>
                            <td id="div1" runat="server" colspan="2">
                                <asp:Button ID="btnnextitem" runat="server" OnClick="btnnextitem_Click" SkinID="ButtonNormal"
                                    TabIndex="6" Text="Next Item" OnClientClick="javaScript:return Validatenull();" />
                                <asp:Button ID="btnremove" runat="server" OnClick="btnremove_Click" SkinID="ButtonNormal"
                                    TabIndex="7" Text="Remove" />
                            </td>
                        </tr>
                        <tr>
                            <div class="reportFilters" id="divList" runat="server">
                                <td>
                                    &nbsp;
                                </td>
                                <td class="inputcontrols" colspan="2">
                                    <asp:ListBox ID="lstsuppliers" runat="server" SelectionMode="Multiple" SkinID="ListBoxNormal"
                                        TabIndex="8"></asp:ListBox>
                                </td>
                            </div>
                        </tr>
                        <tr>
                            <td colspan="4">
                                &nbsp;
                            </td>
                        </tr>
                    </table>
                </td>
            </tr>
        </table>
        <div class="reportButtons">
            <asp:Button ID="btnReport" runat="server" OnClick="btnReport_Click" OnClientClick="javaScript:return Validatedate();"
                SkinID="ButtonViewReport" TabIndex="9" Text="Generate Report" />
        </div>
    </div>
    <div class="reportViewerHolder" id="reportViewerHolder" runat="server">
        <uc1:CrystalReport ID="crpartdetails" runat="server" OnUnload="crpartdetails_Unload" />
    </div>
</asp:Content>
