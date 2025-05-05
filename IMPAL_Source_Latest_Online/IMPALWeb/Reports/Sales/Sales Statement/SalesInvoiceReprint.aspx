<%@ Page Language="C#" MasterPageFile="~/Main.Master" AutoEventWireup="true" CodeBehind="SalesInvoiceReprint.aspx.cs"
    Inherits="IMPALWeb.Reports.Sales.Sales_Statement.SalesInvoiceReprint" %>

<%@ Register Src="~/UserControls/CrystalReportExportA4.ascx" TagName="CrystalReport" TagPrefix="uc1" %>
<asp:Content ID="ContentSalesInvoiceReprint" ContentPlaceHolderID="CPHDetails" runat="server">

    <script language="javascript" type="text/javascript">
        function fnValidate() {
            var ddlPONumber = document.getElementById('<%=ddlInvoiceSTDN.ClientID%>');
            var oPONumberVal = ddlPONumber.value.trim();
            if (oPONumberVal == null || oPONumberVal == "") {
                alert("Invoice/STDN Number should not be null");
                ddlPONumber.focus();
                return false;
            }

            window.document.forms[0].target = '_blank';
        }

        if (!(document.getElementById('<%=hdnTaxCnt.ClientID%>').value == ""
            || document.getElementById('<%=hdnTaxCnt.ClientID%>').value == "0"
            || document.getElementById('<%=hdnTaxCnt.ClientID%>').value == "1")) {
            alert('This Sales Request has Multiple Tax. Submit the Invoice First, Take Print and Repeat the Same Process for All the Taxes of This Sales Request.');
        }

        if (document.getElementById('<%=hdnEwayBillInd.ClientID%>').value == "Customer"
            || document.getElementById('<%=hdnEwayBillInd.ClientID%>').value == "Branch") {
            alert('Generate EWay-Bill for All Invoices for this ' + document.getElementById('<%=hdnEwayBillInd.ClientID%>').value);
        }
    </script>

    <div class="reportButtons" runat="server" id="DivBackbtn" style="display: none">
        <asp:Button ID="btnBack" runat="server" SkinID="ButtonViewReport" Text="Back" OnClick="btnBack_Click" />
        <input id="hdnTaxCnt" type="hidden" runat="server" />
        <input id="hdnEwayBillInd" type="hidden" runat="server" />
    </div>
    <div class="reportFormTitle">
        Sales Invoice - Reprint
    </div>
    <asp:UpdatePanel ID="upHeader" runat="server" UpdateMode="Conditional">
        <Triggers>
            <asp:PostBackTrigger ControlID="btnReport" />
        </Triggers>
        <ContentTemplate>
            <asp:Panel ID="PanelHeaderDtls" runat="server">
                <div class="reportFilters" runat="server" id="divSelection">
                    <table id="reportFiltersTable" class="reportFiltersTable" runat="server">
                        <tr>
                            <td class="label">
                                <asp:Label ID="lblInvoiceType" Text="Invoice Type" SkinID="LabelNormal" runat="server"></asp:Label>
                            </td>
                            <td class="inputcontrols">
                                <asp:DropDownList ID="ddlInvoiceType" SkinID="DropDownListNormal" TabIndex="1" runat="server"
                                    AutoPostBack="True" OnSelectedIndexChanged="ddlInvoiceType_SelectedIndexChanged">
                                </asp:DropDownList>
                            </td>
                            <td class="label">
                                <asp:Label ID="lblInvoiceSTDN" SkinID="LabelNormal" Text="Invoice/STDN Number" runat="server"></asp:Label><span
                                    class="asterix">*</span>
                            </td>
                            <td class="inputcontrols">
                                <asp:DropDownList ID="ddlInvoiceSTDN" SkinID="DropDownListNormal" TabIndex="2" runat="server">
                                </asp:DropDownList>
                            </td>
                        </tr>
                    </table>
                </div>
            </asp:Panel>
            <div class="reportButtons" runat="server" id="divButton" style="float: left">
                <asp:Button ID="btnReport" runat="server" Text="Generate Report" SkinID="ButtonViewReport"
                    TabIndex="3" OnClick="btnReport_Click" OnClientClick="javaScript:return fnValidate();" />
            </div>
            <div class="reportViewerHolder" id="reportViewerHolder" runat="server">
                <uc1:CrystalReport ID="crySalesInvoiceReprint" OnUnload="crySalesInvoiceReprint_Unload" runat="server" />
            </div>
        </ContentTemplate>
    </asp:UpdatePanel>
</asp:Content>
