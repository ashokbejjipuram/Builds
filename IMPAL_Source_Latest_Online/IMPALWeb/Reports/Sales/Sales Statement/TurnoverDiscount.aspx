<%@ Page Language="C#" AutoEventWireup="true" MasterPageFile="~/Main.Master" CodeBehind="TurnoverDiscount.aspx.cs"
    Inherits="IMPALWeb.Reports.Sales.SalesStatement.TurnoverDiscount" %>

<%@ Register Src="~/UserControls/CrystalReportExport.ascx" TagName="CrystalReport" TagPrefix="uc" %>
<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="ajaxToolkit" %>
<asp:Content ID="Content" ContentPlaceHolderID="CPHDetails" runat="server">

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

    <div class="reportFormTitle reportFormTitleExtender250">
        Turnover Discount - PartNo wise
    </div>
    <asp:UpdatePanel ID="upHeader" runat="server" UpdateMode="Conditional">
        <Triggers>
            <asp:PostBackTrigger ControlID="btnReportPDF" />
            <asp:PostBackTrigger ControlID="btnReportExcel" />
            <asp:PostBackTrigger ControlID="btnReportRTF" />
            <asp:PostBackTrigger ControlID="crTurnoverDiscount" />
        </Triggers>
        <ContentTemplate>
            <asp:Panel ID="PanelHeaderDtls" runat="server">
                <div class="reportFilters">
                    <table id="reportFiltersTable" class="reportFiltersTable" runat="server">
                        <tr>
                            <td class="label">
                                <asp:Label ID="lblCustomer" runat="server" Text="Customer" SkinID="LabelNormal"></asp:Label>
                            </td>
                            <td class="inputcontrols">
                                <asp:DropDownList ID="ddlCustomer" runat="server" DataTextField="CustomerName" DataValueField="CustomerCode"
                                    SkinID="DropDownListNormal" TabIndex="1">
                                </asp:DropDownList>
                            </td>
                            <td class="label">
                                <asp:Label ID="lblLineCode" runat="server" Text="Line Code" SkinID="LabelNormal"></asp:Label>
                            </td>
                            <td class="inputcontrols">
                                <asp:DropDownList ID="ddlLineCode" runat="server" DataSourceID="ODSuppliers" DataTextField="SupplierName"
                                    DataValueField="SupplierCode" SkinID="DropDownListNormal" TabIndex="2" AutoPostBack="true"
                                    OnSelectedIndexChanged="ddlLineCode_IndexChanged">
                                </asp:DropDownList>
                                <asp:ObjectDataSource ID="ODSuppliers" runat="server" SelectMethod="GetAllSuppliers"
                                    TypeName="IMPALLibrary.Suppliers"></asp:ObjectDataSource>
                            </td>
                            <td class="label">
                                <asp:Label ID="lblItemCode" runat="server" Text="Item code" SkinID="LabelNormal"></asp:Label>
                            </td>
                            <td class="inputcontrols">
                                <asp:DropDownList ID="ddlItemCodes" runat="server" SkinID="DropDownListNormal" DropDownStyle="DropDownList"
                                    TabIndex="3">
                                </asp:DropDownList>
                            </td>
                        </tr>
                        <tr>
                            <td class="label">
                                <asp:Label ID="lblFromDate" Text="From Date" runat="server" SkinID="LabelNormal"></asp:Label>
                                <span class="asterix">*</span>
                            </td>
                            <td class="inputcontrols">
                                <asp:TextBox ID="txtFromDate" runat="server" SkinID="TextBoxCalendarExtenderNormal"
                                    TabIndex="7"></asp:TextBox>
                                <ajaxToolkit:CalendarExtender ID="calFromdate" Format="dd/MM/yyyy" runat="server"
                                    TargetControlID="txtFromDate">
                                </ajaxToolkit:CalendarExtender>
                                <asp:HiddenField ID="hidFromDate" runat="server" />
                            </td>
                            <td class="label">
                                <asp:Label ID="lblToDate" Text="To Date" runat="server" SkinID="LabelNormal"></asp:Label>
                                <span class="asterix">*</span>
                            </td>
                            <td class="inputcontrols">
                                <asp:TextBox ID="txtToDate" runat="server" SkinID="TextBoxCalendarExtenderNormal"
                                    TabIndex="8"></asp:TextBox>
                                <ajaxToolkit:CalendarExtender ID="calTodate" Format="dd/MM/yyyy" runat="server" TargetControlID="txtToDate">
                                </ajaxToolkit:CalendarExtender>
                                <asp:HiddenField ID="hidToDate" runat="server" />
                            </td>
                            <td class="inputcontrols" rowspan="2">
                                <asp:Button runat="server" Text="Next" ID="btnNext" SkinID="ButtonNormal" OnClick="btnNext_Click"
                                    TabIndex="4" /><br />
                                <br />
                                <asp:Button runat="server" ID="btnRemove" SkinID="ButtonNormal" Text="Remove" OnClick="btnRemove_Click"
                                    TabIndex="5" />
                            </td>
                            <td class="inputcontrols" rowspan="2">
                                <asp:ListBox ID="lstItem" SelectionMode="Multiple" SkinID="ListBoxNormal" runat="server"
                                    TabIndex="6"></asp:ListBox>
                            </td>
                        </tr>
                        <tr>
                            <td class="label">
                                <asp:Label ID="lblReporttype" runat="server" Text="Report Type" SkinID="LabelNormal"></asp:Label>
                            </td>
                            <td class="inputcontrols">
                                <asp:DropDownList ID="ddlReportType" runat="server" SkinID="DropDownListNormal" TabIndex="9"
                                    DataTextField="DisplayText" DataValueField="DisplayValue">
                                </asp:DropDownList>
                            </td>
                        </tr>
                    </table>
                </div>
            </asp:Panel>
            <div class="reportButtons">
                <asp:Button ID="btnReport" runat="server" OnClick="btnReport_Click" OnClientClick="javaScript:return Validate();"
                    Text="Generate Report" SkinID="ButtonViewReport" TabIndex="10" />
                <asp:Button ID="btnReportPDF" runat="server" Text="PDF Report" TabIndex="4" SkinID="ButtonViewReport"
                    OnClientClick="javaScript:return fnShowHideBtns();" OnClick="btnReportPDF_Click" Style="display: none" />
                <asp:Button ID="btnReportExcel" runat="server" Text="Excel Report" TabIndex="4" SkinID="ButtonViewReport"
                    OnClientClick="javaScript:return fnShowHideBtns();" OnClick="btnReportExcel_Click" Style="display: none" />
                <asp:Button ID="btnReportRTF" runat="server" Text="Word Report" TabIndex="4" SkinID="ButtonViewReport"
                    OnClientClick="javaScript:return fnShowHideBtns();" OnClick="btnReportRTF_Click" Style="display: none" />
                <asp:Button ID="btnBack" SkinID="ButtonNormal" runat="server" Text="Back" OnClick="btnBack_Click" Style="display: none" />
            </div>
            <div class="reportViewerHolder" id="reportViewerHolder" runat="server">
                <uc:CrystalReport ID="crTurnoverDiscount" runat="server" OnUnload="crTurnoverDiscount_Unload" />
            </div>
        </ContentTemplate>
    </asp:UpdatePanel>
</asp:Content>
