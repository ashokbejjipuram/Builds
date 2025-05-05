<%@ Page Language="C#" MasterPageFile="~/Main.Master" AutoEventWireup="true" CodeBehind="LWOrderPlaced.aspx.cs"
    Inherits="IMPALWeb.Reports.Ordering.Listing.LWOrderPlaced" %>

<%@ Register Src="~/UserControls/CrystalReportExport.ascx" TagName="CrystalReport" TagPrefix="UC" %>
<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="ajax" %>
<asp:Content ID="Content1" ContentPlaceHolderID="CPHDetails" runat="server">

    <script language="javascript" type="text/javascript">
        function fnValidate() {
            var txtFromDate = document.getElementById('<%=txtFromDate.ClientID%>');
            var txtToDate = document.getElementById('<%=txtToDate.ClientID%>');
            var ddlFromLine = document.getElementById('<%=ddlFromLine.ClientID %>');
            var ddlToLine = document.getElementById('<%=ddlToLine.ClientID%>');
            if (ddlToLine.value != "0" && ddlFromLine.value == "0") {
                ValidateFromLine(ddlFromLine, ddlToLine);
                return false;
            }
            else
                return ValidateOrderDate(txtFromDate, txtToDate);
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
        Linewise Order Placed
    </div>
    <asp:UpdatePanel ID="upHeader" runat="server" UpdateMode="Conditional">
        <Triggers>
            <asp:PostBackTrigger ControlID="btnReportPDF" />
            <asp:PostBackTrigger ControlID="btnReportExcel" />
            <asp:PostBackTrigger ControlID="btnReportRTF" />
            <asp:PostBackTrigger ControlID="crLinewiseOrederPlaced" />
        </Triggers>
        <ContentTemplate>
            <asp:Panel ID="PanelHeaderDtls" runat="server">
                <div class="reportFilters">
                    <table id="reportFiltersTable" class="reportFiltersTable" runat="server">
                        <tr>
                            <td class="label">
                                <asp:Label ID="lblFromLine" runat="server" Text="From Line" SkinID="LabelNormal"></asp:Label>
                            </td>
                            <td class="inputcontrols">
                                <asp:DropDownList ID="ddlFromLine" runat="server" DataSourceID="ODSuppliers" DataTextField="SupplierName"
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
                                <asp:Label ID="lblToLine" runat="server" Text="To Line" SkinID="LabelNormal"></asp:Label>
                            </td>
                            <td class="inputcontrols">
                                <asp:DropDownList ID="ddlToLine" runat="server" DataSourceID="ODToSuppliers" DataTextField="SupplierName"
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
                                <asp:Label ID="lblReportType" runat="server" Text="Report Type" SkinID="LabelNormal"></asp:Label>
                            </td>
                            <td class="inputcontrols">
                                <asp:DropDownList ID="ddlReportType" runat="server" TabIndex="5" SkinID="DropDownListNormal">
                                </asp:DropDownList>
                            </td>
                        </tr>
                        <tr>
                            <td class="label">
                                <asp:Label ID="lblFromDate" runat="server" Text="From Date" SkinID="LabelNormal"></asp:Label>
                            </td>
                            <td class="inputcontrols">
                                <asp:TextBox ID="txtFromDate" runat="server" TabIndex="3" SkinID="TextBoxCalendarExtenderNormal"></asp:TextBox>
                                <ajax:CalendarExtender ID="calFromDate" Format="dd/MM/yyyy" TargetControlID="txtFromDate"
                                    runat="server">
                                </ajax:CalendarExtender>
                            </td>
                            <td class="label">
                                <asp:Label ID="lblToDate" runat="server" Text="To Date" SkinID="LabelNormal"></asp:Label>
                            </td>
                            <td class="inputcontrols">
                                <asp:TextBox ID="txtToDate" runat="server" TabIndex="4" SkinID="TextBoxCalendarExtenderNormal"></asp:TextBox>
                                <ajax:CalendarExtender ID="calToDate" Format="dd/MM/yyyy" TargetControlID="txtToDate"
                                    runat="server">
                                </ajax:CalendarExtender>
                            </td>
                        </tr>
                    </table>
                </div>
            </asp:Panel>
            <div class="reportButtons">
                <asp:Button ID="btnReport" runat="server" Text="Generate Report" OnClientClick="javaScript:return fnValidate();"
                    TabIndex="6" SkinID="ButtonViewReport" OnClick="btnReport_Click" />
                <asp:Button ID="btnReportPDF" runat="server" Text="PDF Report" TabIndex="4" SkinID="ButtonViewReport"
                    OnClientClick="javaScript:return fnShowHideBtns();" OnClick="btnReportPDF_Click" Style="display: none" />
                <asp:Button ID="btnReportExcel" runat="server" Text="Excel Report" TabIndex="4" SkinID="ButtonViewReport"
                    OnClientClick="javaScript:return fnShowHideBtns();" OnClick="btnReportExcel_Click" Style="display: none" />
                <asp:Button ID="btnReportRTF" runat="server" Text="Word Report" TabIndex="4" SkinID="ButtonViewReport"
                    OnClientClick="javaScript:return fnShowHideBtns();" OnClick="btnReportRTF_Click" Style="display: none" />
                <asp:Button ID="btnBack" SkinID="ButtonNormal" runat="server" Text="Back" OnClick="btnBack_Click" Style="display: none" />
            </div>
            <div class="reportViewerHolder" id="reportViewerHolder" runat="server">
                <UC:CrystalReport ID="crLinewiseOrederPlaced" runat="server" OnUnload="crLinewiseOrederPlaced_Unload" ReportName="LinewiseOrederPlaced" />
            </div>
        </ContentTemplate>
    </asp:UpdatePanel>
</asp:Content>
