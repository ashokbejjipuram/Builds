<%@ Page Title="" Language="C#" MasterPageFile="~/Main.Master" AutoEventWireup="true"
    CodeBehind="StockList.aspx.cs" Inherits="IMPALWeb.Reports.Ordering.Stock.StockList" %>

<%@ Register Src="~/UserControls/CrystalReportExport.ascx" TagName="CrystalReport" TagPrefix="uc1" %>
<asp:Content ID="Content1" ContentPlaceHolderID="CPHDetails" runat="server">
    <script language="javascript" type="text/javascript">
        function fnShowHideBtns() {
            document.getElementById('<%=btnReportPDF.ClientID%>').style.display = "none";
            document.getElementById('<%=btnReportExcel.ClientID%>').style.display = "none";
            document.getElementById('<%=btnReportRTF.ClientID%>').style.display = "none";
            document.getElementById('<%=btnBack.ClientID%>').style.display = "inline";

            window.document.forms[0].target = '_blank';
        }
    </script>
    <div class="reportFormTitle">
        Stock List
    </div>
    <asp:UpdatePanel ID="upHeader" runat="server" UpdateMode="Conditional">
        <Triggers>
            <asp:PostBackTrigger ControlID="btnReportPDF" />
            <asp:PostBackTrigger ControlID="btnReportExcel" />
            <asp:PostBackTrigger ControlID="btnReportRTF" />
            <asp:PostBackTrigger ControlID="crstocklist" />
        </Triggers>
        <ContentTemplate>
            <asp:Panel ID="PanelHeaderDtls" runat="server">
                <div class="reportFilters">
                    <table id="reportFiltersTable" class="reportFiltersTable" runat="server">
                        <tr>
                            <td class="label">
                                <asp:Label ID="lblList" SkinID="LabelNormal" runat="server" Text="List"></asp:Label>
                            </td>
                            <td class="inputcontrols">
                                <asp:DropDownList ID="ddlList" runat="server" AutoPostBack="True" OnSelectedIndexChanged="ddlList_OnSelectIndexChanged"
                                    SkinID="DropDownListNormal" TabIndex="1">
                                </asp:DropDownList>
                            </td>
                            <td class="label">
                                <asp:Label ID="lblbranchcode" SkinID="LabelNormal" runat="server" Text="Branch Code"></asp:Label>
                            </td>
                            <td class="inputcontrols">
                                <asp:DropDownList ID="ddlBranchCode" runat="server" AutoPostBack="true" SkinID="DropDownListNormal"
                                    TabIndex="2" OnSelectedIndexChanged="ddlBranchCode_SelectedIndexChanged">
                                </asp:DropDownList>
                            </td>
                            <td class="label">
                                <asp:Label ID="lblsuppliercode" runat="server" SkinID="LabelNormal" Text="Supplier Code"></asp:Label>
                            </td>
                            <td class="inputcontrols">
                                <asp:DropDownList ID="ddlSupplierCode" runat="server" SkinID="DropDownListNormal"
                                    OnSelectedIndexChanged="ddlSupplierCode_SelectedIndexChanged" TabIndex="3">
                                </asp:DropDownList>
                            </td>
                        </tr>
                        <tr id="SegmentStock" style="display: none">
                            <td class="label">
                                <asp:Label ID="lblProduct" SkinID="LabelNormal" runat="server" Text="Prodcut"></asp:Label>
                            </td>
                            <td class="inputcontrols">
                                <asp:DropDownList ID="ddlProduct" runat="server" AutoPostBack="True" SkinID="DropDownListNormal"
                                    OnSelectedIndexChanged="ddlProduct_SelectedIndexChanged" TabIndex="4">
                                </asp:DropDownList>
                            </td>
                            <td class="label">
                                <asp:Label ID="lblSegment" SkinID="LabelNormal" runat="server" Text="Segment"></asp:Label>
                            </td>
                            <td class="inputcontrols">
                                <asp:DropDownList ID="ddlSegment" runat="server" AutoPostBack="true" SkinID="DropDownListNormal"
                                    OnSelectedIndexChanged="ddlSegment_SelectedIndexChanged" TabIndex="5">
                                </asp:DropDownList>
                            </td>
                            <td class="label">
                                <asp:Label ID="lblVehicle" runat="server" SkinID="LabelNormal" Text="Vehicle Type"></asp:Label>
                            </td>
                            <td class="inputcontrols">
                                <asp:DropDownList ID="ddlVehicle" runat="server" SkinID="DropDownListNormal" TabIndex="6">
                                </asp:DropDownList>
                            </td>
                        </tr>
                        <tr>
                            <td class="label">
                                <asp:Label ID="Label1" runat="server" Text="Aging" SkinID="LabelNormal"></asp:Label>
                            </td>
                            <td class="inputcontrols">
                                <asp:DropDownList ID="ddlAging" runat="server" SkinID="DropDownListNormal" TabIndex="7">
                                </asp:DropDownList>
                            </td>
                            <td class="label">
                                <asp:Label ID="lblreporttype" runat="server" Text="ReportType" SkinID="LabelNormal"></asp:Label>
                            </td>
                            <td class="inputcontrols">
                                <asp:DropDownList ID="ddlReportType" runat="server" SkinID="DropDownListNormal" TabIndex="7">
                                </asp:DropDownList>
                            </td>
                        </tr>
                    </table>
                </div>
            </asp:Panel>
            <div class="reportButtons">
                <asp:Button ID="btnReport" SkinID="ButtonViewReport" Text="Generate Report" runat="server"
                    TabIndex="5" OnClick="btnReport_Click" />
                <asp:Button ID="btnReportPDF" runat="server" Text="PDF Report" TabIndex="4" SkinID="ButtonViewReport"
                    OnClientClick="javaScript:return fnShowHideBtns();" OnClick="btnReportPDF_Click" Style="display: none" />
                <asp:Button ID="btnReportExcel" runat="server" Text="Excel Report" TabIndex="4" SkinID="ButtonViewReport"
                    OnClientClick="javaScript:return fnShowHideBtns();" OnClick="btnReportExcel_Click" Style="display: none" />
                <asp:Button ID="btnReportRTF" runat="server" Text="Word Report" TabIndex="4" SkinID="ButtonViewReport"
                    OnClientClick="javaScript:return fnShowHideBtns();" OnClick="btnReportRTF_Click" Style="display: none" />
                <asp:Button ID="btnBack" SkinID="ButtonNormal" runat="server" Text="Back" OnClick="btnBack_Click" Style="display: none" />
            </div>
            <div class="reportViewerHolder" id="reportViewerHolder" runat="server">
                <uc1:crystalreport id="crstocklist" enableviewstate="true" onunload="crstocklist_Unload" runat="server" />
            </div>
            <asp:ObjectDataSource ID="ODBranchCode" runat="server" SelectMethod="GetAllBranch"
                TypeName="IMPALLibrary.Branches"></asp:ObjectDataSource>
            <asp:ObjectDataSource ID="ODSupplierCode" runat="server" SelectMethod="GetAllSuppliers"
                TypeName="IMPALLibrary.Suppliers"></asp:ObjectDataSource>
        </ContentTemplate>
    </asp:UpdatePanel>
</asp:Content>
