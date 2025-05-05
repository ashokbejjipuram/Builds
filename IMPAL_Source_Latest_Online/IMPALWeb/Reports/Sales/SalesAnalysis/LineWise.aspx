<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="LineWise.aspx.cs" Inherits="IMPALWeb.Reports.Sales.SalesAnalysis.LineWise"
    MasterPageFile="~/Main.Master" %>

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

    <div class="reportFormTitle">
        Line wise with Previous Year
    </div>
    <asp:UpdatePanel ID="upHeader" runat="server" UpdateMode="Conditional">
        <Triggers>
            <asp:PostBackTrigger ControlID="btnReportPDF" />
            <asp:PostBackTrigger ControlID="btnReportExcel" />
            <asp:PostBackTrigger ControlID="btnReportRTF" />
            <asp:PostBackTrigger ControlID="crLineWise" />
        </Triggers>
        <ContentTemplate>
            <asp:Panel ID="PanelHeaderDtls" runat="server">
                <div class="reportFilters">
                    <table class="reportFiltersTable" id="reportFiltersTable" runat="server">
                        <tr>
                            <td class="label">
                                <asp:Label SkinID="LabelNormal" runat="server" Text="From Date"></asp:Label>
                                <span class="asterix">*</span>
                            </td>
                            <td class="inputcontrols">
                                <asp:TextBox ID="txtFromDate" runat="server" TabIndex="1" SkinID="TextBoxCalendarExtenderNormal" />
                                <asp:HiddenField ID="hidFromDate" runat="server" />
                                <%--Stores value in MM/dd/yyyy format--%>
                                <ajaxToolkit:CalendarExtender ID="calFromDate" runat="server" TargetControlID="txtFromDate"
                                    Format="dd/MM/yyyy" />
                            </td>
                            <td class="label">
                                <asp:Label SkinID="LabelNormal" Text="To Date" runat="server"></asp:Label>
                                <span class="asterix">*</span>
                            </td>
                            <td class="inputcontrols">
                                <asp:TextBox ID="txtToDate" runat="server" TabIndex="2" SkinID="TextBoxCalendarExtenderNormal" />
                                <asp:HiddenField ID="hidToDate" runat="server" />
                                <%--Stores value in MM/dd/yyyy format--%>
                                <ajaxToolkit:CalendarExtender ID="calToDate" runat="server" TargetControlID="txtToDate"
                                    Format="dd/MM/yyyy" />
                            </td>
                            <td class="label">
                                <asp:Label SkinID="LabelNormal" Text="Line Code" runat="server"></asp:Label>
                            </td>
                            <td class="inputcontrols">
                                <%-- Dropdown populated from Suppliers.cs--%>
                                <asp:DropDownList ID="ddlLineCode" runat="server" TabIndex="3" SkinID="DropDownListNormal"
                                    DataSourceID="ODLine" DataTextField="SupplierName" DataValueField="SupplierCode" />
                                <asp:ObjectDataSource ID="ODLine" runat="server" SelectMethod="GetAllSuppliers" TypeName="IMPALLibrary.Suppliers" />
                            </td>
                        </tr>
                        <tr>
                            <td class="label">
                                <asp:Label SkinID="LabelNormal" runat="server" Text="Town Code"></asp:Label>
                            </td>
                            <td class="inputcontrols">
                                <asp:DropDownList ID="ddlTownCode" runat="server" TabIndex="4" AutoPostBack="true"
                                    OnSelectedIndexChanged="ddlTownCode_IndexChanged" DataTextField="TownName" DataValueField="Towncode"
                                    SkinID="DropDownListNormal" />
                            </td>
                            <td class="label">
                                <asp:Label ID="lblReportType" SkinID="LabelNormal" runat="server" Text="Report Type" />
                            </td>
                            <td class="inputcontrols">
                                <asp:DropDownList ID="ddlReportType" runat="server" TabIndex="5" DataTextField="DisplayText"
                                    DataValueField="DisplayValue" SkinID="DropDownListNormal" />
                            </td>
                        </tr>
                    </table>
                </div>
            </asp:Panel>
            <div class="reportButtons">
                <asp:Button ID="btnReport" runat="server" Text="Generate Report" OnClick="btnReport_Click"
                    SkinID="ButtonViewReport" OnClientClick="javaScript:return Validate();" TabIndex="6" />
                <asp:Button ID="btnReportPDF" runat="server" Text="PDF Report" TabIndex="4" SkinID="ButtonViewReport"
                    OnClientClick="javaScript:return fnShowHideBtns();" OnClick="btnReportPDF_Click" Style="display: none" />
                <asp:Button ID="btnReportExcel" runat="server" Text="Excel Report" TabIndex="4" SkinID="ButtonViewReport"
                    OnClientClick="javaScript:return fnShowHideBtns();" OnClick="btnReportExcel_Click" Style="display: none" />
                <asp:Button ID="btnReportRTF" runat="server" Text="Word Report" TabIndex="4" SkinID="ButtonViewReport"
                    OnClientClick="javaScript:return fnShowHideBtns();" OnClick="btnReportRTF_Click" Style="display: none" />
                <asp:Button ID="btnBack" SkinID="ButtonNormal" runat="server" Text="Back" OnClick="btnBack_Click" Style="display: none" />
            </div>
            <div class="reportViewerHolder" id="reportViewerHolder" runat="server">
                <UC:CrystalReport ID="crLineWise" runat="server" OnUnload="crLineWise_Unload" />
            </div>
        </ContentTemplate>
    </asp:UpdatePanel>
</asp:Content>
