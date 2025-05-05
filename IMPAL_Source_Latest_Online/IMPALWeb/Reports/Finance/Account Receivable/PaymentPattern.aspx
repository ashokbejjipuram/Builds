<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="PaymentPattern.aspx.cs"
    Inherits="IMPALWeb.Reports.Finance.Account_Receivable.PaymentPattern" MasterPageFile="~/Main.Master" %>

<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="ajaxToolkit" %>
<asp:Content ID="MainContent" ContentPlaceHolderID="CPHDetails" runat="server">

    <script language="javascript" type="text/javascript">
        function Validate() {
            var ddlAccPeriod = document.getElementById('<%=ddlAccPeriod.ClientID%>');
            if (validateacc(ddlAccPeriod) == false) {
                return false;
            }                        
        }
    </script>

    <div class="reportFormTitle">
        Payment Pattern</div>
    <div class="reportFilters">
        <table id="reportFiltersTable" class="reportFiltersTable" runat="server">
            <tr>
                <td class="label">
                    <asp:Label ID="lblAccPeriod" runat="server" Text="Accounting Period" SkinID="LabelNormal"></asp:Label>
                    <span class="asterix">*</span>
                </td>
                <td class="inputcontrols">
                    <asp:DropDownList ID="ddlAccPeriod" TabIndex="1" runat="server" SkinID="DropDownListNormal">
                    </asp:DropDownList>
                </td>
                <td class="label">
                    <asp:Label ID="lblCustomerName" Text="Customer" runat="server" SkinID="LabelNormal"></asp:Label>
                </td>
                <td class="inputcontrols">
                    <asp:DropDownList ID="ddlCustomer" runat="server" TabIndex="3" SkinID="DropDownListNormal" />
                </td>               
            </tr>
        </table>
        <div class="reportButtons">
            <asp:Button ID="btnReport" runat="server" OnClick="btnReport_Click" OnClientClick="javaScript:return Validate();"
                Text="Generate Report" TabIndex="6" SkinID="ButtonViewReport"></asp:Button>
        </div>
    </div>
</asp:Content>
