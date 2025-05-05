<%@ Page Language="C#" AutoEventWireup="true" MasterPageFile="~/Main.Master" CodeBehind="LineWiseSales_Target_Town.aspx.cs"
    Inherits="IMPALWeb.Reports.Sales.SalesListing.LineWiseSales_Target_Town" %>

<%@ Register Src="~/UserControls/CrystalReportExport.ascx" TagName="CrystalReport" TagPrefix="uc1" %>
<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="ajaxToolkit" %>
<asp:Content ID="Content1" ContentPlaceHolderID="CPHDetails" runat="server">

    <script language="javascript" type="text/javascript">
        function fnValidate() {
            var txtDate = document.getElementById('<%=txtDate.ClientID%>');

            var oDateVal = txtDate.value.trim();
            var oSysDate = new Date();
            var oDate = oDateVal.split("/");
            var oDateFormatted = oDate[1] + "/" + oDate[0] + "/" + oDate[2];

            if (oDateVal == null || oDateVal == "") {
                alert("Date should not be null!");
                txtDate.focus();
                return false;
            }
            else if (oDateVal != null && fnIsDate(txtDate.value) == false) {
                txtDate.value = "";
                txtDate.focus();
                return false;

            }
            else if (oSysDate < new Date(oDateFormatted)) {
                alert("Date should not be greater than System Date");
                txtDate.value = "";
                txtDate.focus();
                return false;
            }
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
        Linewise Sales Town (Target)
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
                                <asp:Label SkinID="LabelNormal" ID="lbldate" Text="Date" runat="server"></asp:Label><span
                                    class="asterix">*</span>
                            </td>
                            <td class="inputcontrols">
                                <asp:TextBox SkinID="TextBoxCalendarExtenderNormal" TabIndex="1" ID="txtDate"
                                    runat="server"></asp:TextBox>
                                <ajaxToolkit:CalendarExtender ID="caldate" Format="dd/MM/yyyy" runat="server"
                                    TargetControlID="txtDate">
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
                            <td class="label">
                                <asp:Label SkinID="LabelNormal" runat="server" Text="Town"></asp:Label>
                            </td>
                            <td class="inputcontrols">
                                <asp:DropDownList ID="ddlTownCode" runat="server" TabIndex="3" SkinID="DropDownListNormal"
                                    DataTextField="TownName" DataValueField="Towncode" OnSelectedIndexChanged="ddlTownCode_SelectedIndexChanged" />
                            </td>
                        </tr>
                    </table>
                </div>
                <input id="hdnTownCodes" type="hidden" runat="server" />
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
