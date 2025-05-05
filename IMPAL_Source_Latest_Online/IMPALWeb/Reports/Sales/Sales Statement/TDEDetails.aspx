<%@ Page Language="C#" AutoEventWireup="true" MasterPageFile="~/Main.Master" CodeBehind="TDEDetails.aspx.cs"
    Inherits="IMPALWeb.Reports.Sales.SalesStatement.TDEDetails" %>

<%@ Register Src="~/UserControls/CrystalReportExport.ascx" TagName="CrystalReport" TagPrefix="UC" %>
<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="ajaxToolkit" %>
<asp:Content ID="MainContent" ContentPlaceHolderID="CPHDetails" runat="server">

    <script language="javascript" type="text/javascript">
        function Validate() {
            var txtFromDate = document.getElementById('<%=txtFromDate.ClientID%>');
            var txtToDate = document.getElementById('<%=txtToDate.ClientID%>');
            var hidFromDate = document.getElementById('<%=hidFromDate.ClientID%>');
            var hidToDate = document.getElementById('<%=hidToDate.ClientID%>');
            return ValidateDate(txtFromDate, txtToDate, hidFromDate, hidToDate);
        }

        function fnShowHideBtns() {
            document.getElementById('<%=btnReportPDF.ClientID%>').style.display = "none";
            document.getElementById('<%=btnReportExcel.ClientID%>').style.display = "none";
            document.getElementById('<%=btnReportRTF.ClientID%>').style.display = "none";
            document.getElementById('<%=btnBack.ClientID%>').style.display = "inline";

            window.document.forms[0].target = '_blank';
        }
    </script>

    <div class="reportFormTitle reportFormTitleExtender350">
        Turnover Discount Eligibility Details
    </div>
    <asp:UpdatePanel ID="upHeader" runat="server" UpdateMode="Conditional">
        <Triggers>
            <asp:PostBackTrigger ControlID="btnReportPDF" />
            <asp:PostBackTrigger ControlID="btnReportExcel" />
            <asp:PostBackTrigger ControlID="btnReportRTF" />
            <asp:PostBackTrigger ControlID="crSalesTDEDetails" />
        </Triggers>
        <ContentTemplate>
            <asp:Panel ID="PanelHeaderDtls" runat="server">
                <div class="reportFilters">
                    <table id="reportFiltersTable" class="reportFiltersTable" runat="server">
                        <tr>
                            <td class="label">
                                <asp:Label ID="lblLineCode" runat="server" Text="Line Code" SkinID="LabelNormal">
                                </asp:Label>
                            </td>
                            <td class="inputcontrols">
                                <asp:DropDownList ID="ddlSupplier" runat="server" DataSourceID="ODSuppliers" DataTextField="SupplierName"
                                    DataValueField="SupplierCode" SkinID="DropDownListNormal">
                                </asp:DropDownList>
                                <asp:ObjectDataSource ID="ODSuppliers" runat="server" SelectMethod="GetAllSuppliers"
                                    TypeName="IMPALLibrary.Suppliers"></asp:ObjectDataSource>
                            </td>
                            <td class="label">
                                <asp:Label ID="lblCustomer" runat="server" Text="Customer" SkinID="LabelNormal" />
                            </td>
                            <td class="inputcontrols">
                                <asp:DropDownList ID="ddlCustomer" runat="server" SkinID="DropDownListNormal">
                                </asp:DropDownList>
                            </td>
                        </tr>
                        <tr>
                            <td class="label">
                                <asp:Label ID="lblFromDate" Text="From Date" runat="server" SkinID="LabelNormal">
                                </asp:Label><span class="asterix">*</span>
                            </td>
                            <td class="inputcontrols">
                                <asp:TextBox ID="txtFromDate" runat="server" SkinID="TextBoxCalendarExtenderNormal"></asp:TextBox>
                                <ajaxToolkit:CalendarExtender ID="calFromdate" Format="dd/MM/yyyy" runat="server"
                                    TargetControlID="txtFromDate">
                                </ajaxToolkit:CalendarExtender>
                                <asp:HiddenField ID="hidFromDate" runat="server" />
                                <%--Stores value in MM/dd/yyyy format--%>
                            </td>
                            <td class="label">
                                <asp:Label ID="lblToDate" Text="To Date" runat="server" SkinID="LabelNormal">
                                </asp:Label><span class="asterix">*</span>
                            </td>
                            <td class="inputcontrols">
                                <asp:TextBox ID="txtToDate" runat="server" SkinID="TextBoxCalendarExtenderNormal"></asp:TextBox>
                                <ajaxToolkit:CalendarExtender ID="calTodate" Format="dd/MM/yyyy" runat="server" TargetControlID="txtToDate">
                                </ajaxToolkit:CalendarExtender>
                                <asp:HiddenField ID="hidToDate" runat="server" />
                                <%--Stores value in MM/dd/yyyy format--%>
                            </td>
                        </tr>
                    </table>
                </div>
            </asp:Panel>
            <div class="reportButtons">
                <asp:Button ID="btnReport" runat="server" Text="Generate Report" OnClick="btnReport_Click"
                    OnClientClick="javaScript:return Validate();" SkinID="ButtonViewReport" />
                <asp:Button ID="btnReportPDF" runat="server" Text="PDF Report" TabIndex="4" SkinID="ButtonViewReport"
                    OnClientClick="javaScript:return fnShowHideBtns();" OnClick="btnReportPDF_Click" Style="display: none" />
                <asp:Button ID="btnReportExcel" runat="server" Text="Excel Report" TabIndex="4" SkinID="ButtonViewReport"
                    OnClientClick="javaScript:return fnShowHideBtns();" OnClick="btnReportExcel_Click" Style="display: none" />
                <asp:Button ID="btnReportRTF" runat="server" Text="Word Report" TabIndex="4" SkinID="ButtonViewReport"
                    OnClientClick="javaScript:return fnShowHideBtns();" OnClick="btnReportRTF_Click" Style="display: none" />
                <asp:Button ID="btnBack" SkinID="ButtonNormal" runat="server" Text="Back" OnClick="btnBack_Click" Style="display: none" />
            </div>
            <div class="reportViewerHolder" id="reportViewerHolder" runat="server">
                <UC:CrystalReport ID="crSalesTDEDetails" runat="server" OnUnload="crSalesTDEDetails_Unload" ReportName="TurnOverDiscount" />
            </div>
        </ContentTemplate>
    </asp:UpdatePanel>
</asp:Content>
