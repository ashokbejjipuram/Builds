<%@ Page Language="C#" AutoEventWireup="true" MasterPageFile="~/Main.Master" CodeBehind="LineWiseSales_Target.aspx.cs"
    Inherits="IMPALWeb.Reports.Sales.SalesListing.LineWiseSales_Target" %>

<%@ Register Src="~/UserControls/CrystalReportExport.ascx" TagName="CrystalReport" TagPrefix="uc1" %>
<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="ajaxToolkit" %>
<asp:Content ID="Content1" ContentPlaceHolderID="CPHDetails" runat="server">

    <script language="javascript" type="text/javascript">
        function fnValidate() {
            var txtFromDate = document.getElementById('<%=txtFromDate.ClientID%>');
            var txtToDate = document.getElementById('<%=txtToDate.ClientID%>');
            return ValidateDates(txtFromDate, txtToDate);
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
        Linewise Sales(Target)
    </div>
    <asp:UpdatePanel ID="upHeader" runat="server" UpdateMode="Conditional">
        <Triggers>
            <asp:PostBackTrigger ControlID="btnReportPDF" />
            <asp:PostBackTrigger ControlID="btnReportExcel" />
            <asp:PostBackTrigger ControlID="btnReportRTF" />
            <asp:PostBackTrigger ControlID="crLineWiseSalesTarget" />
        </Triggers>
        <ContentTemplate>
            <asp:Panel ID="PanelHeaderDtls" runat="server">
                <div class="reportFilters">
                    <table class="reportFiltersTable" id="reportFiltersTable" runat="server">
                        <tr>
                            <td class="label">
                                <asp:Label SkinID="LabelNormal" ID="lblfromdate" Text="From Date" runat="server"></asp:Label><span
                                    class="asterix">*</span>
                            </td>
                            <td class="inputcontrols">
                                <asp:TextBox SkinID="TextBoxCalendarExtenderNormal" TabIndex="1" ID="txtFromDate"
                                    runat="server"></asp:TextBox>
                                <ajaxToolkit:CalendarExtender ID="calFromdate" Format="dd/MM/yyyy" runat="server"
                                    TargetControlID="txtFromDate">
                                </ajaxToolkit:CalendarExtender>
                            </td>
                            <td class="label">
                                <asp:Label SkinID="LabelNormal" ID="lblToDate" Text="To Date" runat="server"></asp:Label><span
                                    class="asterix">*</span>
                            </td>
                            <td class="inputcontrols">
                                <asp:TextBox SkinID="TextBoxCalendarExtenderNormal" TabIndex="2" ID="txtToDate" runat="server"></asp:TextBox>
                                <ajaxToolkit:CalendarExtender ID="calTodate" Format="dd/MM/yyyy" runat="server" TargetControlID="txtToDate">
                                </ajaxToolkit:CalendarExtender>
                            </td>
                            <td class="label">
                                <asp:Label SkinID="LabelNormal" ID="lblLinecode" runat="server" Text="Line Code"></asp:Label>
                            </td>
                            <td class="inputcontrols">
                                <asp:DropDownList SkinID="DropDownListNormal" ID="ddlLinecode" TabIndex="3" runat="server"
                                    DataSourceID="SalesWiseSuppliers" DataTextField="SupplierName" DataValueField="SupplierCode">
                                </asp:DropDownList>
                                <asp:ObjectDataSource ID="SalesWiseSuppliers" runat="server" SelectMethod="GetSalesBasedSuppliers"
                                    TypeName="IMPALLibrary.Suppliers">
                                    <SelectParameters>
                                        <asp:SessionParameter Name="strBranchCode" SessionField="BranchCode" />
                                    </SelectParameters>
                                </asp:ObjectDataSource>
                            </td>
                        </tr>
                    </table>
                </div>
            </asp:Panel>
            <div class="reportButtons">
                <asp:Button SkinID="ButtonViewReport" ID="btnReport" runat="server" Text="Generate Report"
                    TabIndex="4" OnClick="btnReport_Click" OnClientClick="javaScript:return fnValidate();" />
                <asp:Button ID="btnReportPDF" runat="server" Text="PDF Report" TabIndex="4" SkinID="ButtonViewReport"
                    OnClientClick="javaScript:return fnShowHideBtns();" OnClick="btnReportPDF_Click" Style="display: none" />
                <asp:Button ID="btnReportExcel" runat="server" Text="Excel Report" TabIndex="4" SkinID="ButtonViewReport"
                    OnClientClick="javaScript:return fnShowHideBtns();" OnClick="btnReportExcel_Click" Style="display: none" />
                <asp:Button ID="btnReportRTF" runat="server" Text="Word Report" TabIndex="4" SkinID="ButtonViewReport"
                    OnClientClick="javaScript:return fnShowHideBtns();" OnClick="btnReportRTF_Click" Style="display: none" />
                <asp:Button ID="btnBack" SkinID="ButtonNormal" runat="server" Text="Back" OnClick="btnBack_Click" Style="display: none" />
            </div>
            <div class="reportViewerHolder" id="reportViewerHolder" runat="server">
                <uc1:CrystalReport ID="crLineWiseSalesTarget" runat="server" OnUnload="crLineWiseSalesTarget_Unload" />
                </td>
            </div>
        </ContentTemplate>
    </asp:UpdatePanel>
</asp:Content>
