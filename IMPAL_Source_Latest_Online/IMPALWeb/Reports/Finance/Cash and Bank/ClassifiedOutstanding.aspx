<%@ Page Language="C#" MasterPageFile="~/Main.Master" AutoEventWireup="true" CodeBehind="ClassifiedOutstanding.aspx.cs"
    Inherits="IMPALWeb.Reports.Finance.Cash_and_Bank.ClassifiedOutstanding" %>

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
        Classified Outstanding</div>
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
                                <asp:Label ID="lblFromCustomer" runat="server" Text="From Customer" SkinID="LabelNormal"></asp:Label><span
                                    id="astFromCust" class="asterix" runat="server" visible="false">*</span>
                            </td>
                            <td class="inputcontrols">
                                <asp:DropDownList SkinID="DropDownListNormal" ID="cboFromCustomer" runat="server"
                                    DropDownStyle="DropDownList" AutoPostBack="true" TabIndex="2" OnSelectedIndexChanged="cboFromCustomer_SelectedIndexChanged">
                                </asp:DropDownList>
                            </td>
                            <td class="label">
                                <asp:Label ID="lblToCustomer" runat="server" Text="To Customer" SkinID="LabelNormal"></asp:Label><span
                                    id="astToCust" class="asterix" runat="server" visible="false">*</span>
                            </td>
                            <td class="inputcontrols">
                                <asp:DropDownList SkinID="DropDownListNormal" ID="cboToCustomer" runat="server" AutoPostBack="true"
                                    TabIndex="3" DropDownStyle="DropDownList" OnSelectedIndexChanged="cboToCustomer_SelectedIndexChanged">
                                </asp:DropDownList>
                            </td>
                        </tr>
                        <tr>
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
                            <td class="label">
                                <asp:Label ID="lblTown" runat="server" Text="Town" SkinID="LabelNormal"></asp:Label>
                            </td>
                            <td class="inputcontrols">
                                <asp:DropDownList ID="ddlTown" runat="server" TabIndex="5" SkinID="DropDownListNormal"
                                    AutoPostBack="true" Enabled="false" OnSelectedIndexChanged="ddlTown_SelectedIndexChanged">
                                </asp:DropDownList>
                            </td>
                            <td class="label">
                                <asp:Label ID="lblReportType" runat="server" Text="Report Type" SkinID="LabelNormal"></asp:Label>
                            </td>
                            <td class="inputcontrols">
                                <asp:DropDownList ID="ddlReportType" runat="server" TabIndex="6" SkinID="DropDownListNormal">
                                </asp:DropDownList>
                            </td>
                        </tr>
                    </table>
                    <%--</div>--%>
                    <div id="CustInfo" visible="false" runat="server">
                        <div class="reportFormTitle">
                            Customer Information</div>
                        <%--<div class="reportFilters">--%>
                        <table id="reportFiltersTable1" class="reportFiltersTable" runat="server">
                            <tr>
                                <td class="label">
                                    <asp:Label ID="lblCustCode" runat="server" Text="Customer Code" SkinID="LabelNormal"></asp:Label>
                                </td>
                                <td class="inputcontrols">
                                    <asp:TextBox ID="txtCustCode" runat="server" SkinID="TextBoxNormal" ReadOnly="True"></asp:TextBox>
                                </td>
                                <td class="label">
                                    <asp:Label ID="lblAddress1" runat="server" Text="Address1" SkinID="LabelNormal"></asp:Label>
                                </td>
                                <td class="inputcontrols">
                                    <asp:TextBox ID="txtAddress1" runat="server" SkinID="TextBoxNormal" TextMode="MultiLine"
                                        Height="62px" ReadOnly="True" Width="150px"></asp:TextBox>
                                </td>
                                <td class="label">
                                    <asp:Label ID="lblAddress2" runat="server" Text="Address2" SkinID="LabelNormal"></asp:Label>
                                </td>
                                <td class="inputcontrols">
                                    <asp:TextBox ID="txtAddress2" runat="server" SkinID="TextBoxNormal" Height="62px"
                                        TextMode="MultiLine" ReadOnly="True" Width="150px"></asp:TextBox>
                                </td>
                            </tr>
                            <tr>
                                <td class="label">
                                    <asp:Label ID="lblAddress3" runat="server" Text="Address3" SkinID="LabelNormal"></asp:Label>
                                </td>
                                <td class="inputcontrols">
                                    <asp:TextBox ID="txtAddress3" runat="server" SkinID="TextBoxNormal" Height="62px"
                                        ReadOnly="True" TextMode="MultiLine" Width="150px"></asp:TextBox>
                                </td>
                                <td class="label">
                                    <asp:Label ID="lblAddress4" runat="server" Text="Address4" SkinID="LabelNormal"></asp:Label>
                                </td>
                                <td class="inputcontrols">
                                    <asp:TextBox ID="txtAddress4" runat="server" SkinID="TextBoxNormal" Height="62px"
                                        ReadOnly="True" Width="150px"></asp:TextBox>
                                </td>
                                <td class="label">
                                    <asp:Label ID="lblLocation" runat="server" Text="Location" SkinID="LabelNormal"></asp:Label>
                                </td>
                                <td class="inputcontrols">
                                    <asp:TextBox ID="txtLocation" runat="server" SkinID="TextBoxNormal" ReadOnly="True"></asp:TextBox>
                                </td>
                            </tr>
                        </table>
                        <%--</div>--%>
                    </div>
                </td>
            </tr>
        </table>
        <div class="reportButtons">
            <asp:Button ID="btnReport" runat="server" Text="Generate Report" TabIndex="7" SkinID="ButtonViewReport"
                OnClientClick="javaScript:return fnValidate();" OnClick="btnReport_Click" />
        </div>
    </div>
    <div class="reportViewerHolder" id="reportViewerHolder" runat="server">
        <UC:CrystalReport ID="crClassifiedOutstanding_Report" runat="server" OnUnload="crClassifiedOutstanding_Report_Unload" ReportName="ClassifiedOutstanding_Report" />
    </div>
</asp:Content>
