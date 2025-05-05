<%@ Page Language="C#" MasterPageFile="~/Main.Master" AutoEventWireup="true" CodeBehind="SalesManClassifiedOutstanding.aspx.cs"
    Inherits="IMPALWeb.Reports.Finance.Cash_and_Bank.SalesManClassifiedOutstanding" %>

<%@ Register Src="~/UserControls/CrystalReport.ascx" TagName="CrystalReport" TagPrefix="UC" %>
<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="ajaxToolkit" %>

<asp:Content ID="Content1" ContentPlaceHolderID="CPHDetails" runat="server">

    <script language="javascript" type="text/javascript">
        function fnValidate() {
            var ddlBranch = document.getElementById('<%=ddlBranch.ClientID%>');
            var cboFromCustomer = document.getElementById('<%=cboFromCustomer.ClientID%>_cboFromCustomer_TextBox');
            var cboToCustomer = document.getElementById('<%=cboToCustomer.ClientID%>_cboToCustomer_TextBox');
            var txtFromDate = document.getElementById('<%=txtDate.ClientID%>');

            if (cboToCustomer.value != "" && cboToCustomer.value != null) {
                if (txtFromDate.value == null || txtFromDate.value == "") {
                    alert("Date should not be null");
                    txtFromDate.focus();
                    return false;
                }
                else 
                {
                return true;
                }
            }
            else if (cboFromCustomer.value == null || cboFromCustomer.value == "") 
                return true;
            else
                return fnValidateFinance(ddlBranch, cboFromCustomer, cboToCustomer, txtFromDate);
            
        }
    </script>

    <div class="reportFormTitle">
        Sales Man Classified Outstanding</div>
    <div class="reportFilters">
        <table id="reportFiltersTable" runat="server">
            <tr>
                <td>
                    <table id="reportFiltersTable2" class="reportFiltersTable" runat="server">
                        <tr>
                            <td class="label">
                                <asp:Label ID="lblBranch" runat="server" Text="Branch" SkinID="LabelNormal"></asp:Label><%--<div id="astHide" visible="true" runat="server">--%><span
                                    id="astHide" class="asterix" visible="true" runat="server">*</span><%--</div> --%>
                            </td>
                            <td class="inputcontrols">
                                <asp:DropDownList ID="ddlBranch" runat="server" TabIndex="1" SkinID="DropDownListNormal"
                                    AutoPostBack="true" OnSelectedIndexChanged="ddlBranch_SelectedIndexChanged">
                                </asp:DropDownList>
                            </td>
                            <td class="label">
                                <asp:Label ID="lblSalesMan" runat="server" Text="Sales Man" SkinID="LabelNormal"></asp:Label><span
                                    id="astFromCust" class="asterix" runat="server" visible="false">*</span>
                            </td>
                            <td class="inputcontrols">
                                <asp:DropDownList SkinID="DropDownListNormal" ID="cboSalesMan" runat="server"
                                    DropDownStyle="DropDownList" AutoPostBack="true" TabIndex="2" OnSelectedIndexChanged="cboSalesMan_SelectedIndexChanged">
                                </asp:DropDownList>
                            </td>
                            <td class="label">
                                <asp:Label ID="lblDate" runat="server" Text="Date" SkinID="LabelNormal"></asp:Label><span
                                    id="astDate" class="asterix" runat="server" visible="false">*</span>
                            </td>
                            <td class="inputcontrols">
                                <asp:TextBox ID="txtDate" runat="server" SkinID="TextBoxCalendarExtenderNormal" TabIndex="4"></asp:TextBox>
                                <ajaxToolkit:CalendarExtender ID="calDate" Format="dd/MM/yyyy" TargetControlID="txtDate"
                                    runat="server">
                                </ajaxToolkit:CalendarExtender>
                            </td>
                        </tr>                        
                    </table>                    
                </td>
            </tr>
        </table>
        <div class="reportButtons">
            <asp:Button ID="btnReport" runat="server" Text="Generate Report" TabIndex="7" SkinID="ButtonViewReport"
                OnClientClick="javaScript:return fnValidate();" OnClick="btnReport_Click" />
        </div>
    </div>
    <div class="reportViewerHolder" id="reportViewerHolder" runat="server">
        <UC:CrystalReport ID="crSMClassifiedOutstanding_Report" runat="server" OnUnload="crSMClassifiedOutstanding_Report_Unload" ReportName="ClassifiedOutstanding_Report" />
    </div>
</asp:Content>
