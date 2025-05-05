<%@ Page Language="C#" MasterPageFile="~/Main.Master" AutoEventWireup="true" CodeBehind="TravellingExpenses.aspx.cs"
    Inherits="IMPALWeb.Reports.Finance.Cash_and_Bank.TravellingExpenses" %>

<asp:Content ID="CPHDetails" ContentPlaceHolderID="CPHDetails" runat="server">

    <script language="javascript" type="text/javascript">
        function fnValidate() {
            var ddlMonthYear = document.getElementById('<%=ddlMonthYear.ClientID%>');
            return validatemonthyear(ddlMonthYear);
        }
    </script>

    <div class="reportFormTitle">
        Travelling Expenses</div>
    <div class="reportFilters">
        <table id="reportFiltersTable" class="reportFiltersTable">
            <tr>
                <td class="label">
                    <asp:Label SkinID="LabelNormal" ID="lblMonthYear" runat="server" Text="Month Year"></asp:Label><span
                        class="asterix">*</span>
                </td>
                <td class="inputcontrols">
                    <asp:DropDownList SkinID="DropDownListNormal" ID="ddlMonthYear" TabIndex="1" runat="server">
                    </asp:DropDownList>
                </td>
            </tr>
        </table>
        <div class="reportButtons">
            <asp:Button ID="btnReport" runat="server" Text="Report" OnClientClick="javaScript:return fnValidate();"
                TabIndex="2" SkinID="ButtonViewReport" />
        </div>
    </div>
</asp:Content>
