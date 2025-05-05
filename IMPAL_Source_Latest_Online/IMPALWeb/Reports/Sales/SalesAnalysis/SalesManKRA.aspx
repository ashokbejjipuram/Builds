<%@ Page Language="C#" MasterPageFile="~/Main.Master" AutoEventWireup="true" CodeBehind="SalesManKRA.aspx.cs"
    Inherits="IMPALWeb.Reports.Sales.SalesListing.SalesManKRA" %>

<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="ajaxToolkit" %>
<asp:Content ID="Content1" ContentPlaceHolderID="CPHDetails" runat="server">

    <script language="javascript" type="text/javascript">
        function fnValidate() {
            var txtFromDate = document.getElementById('<%=txtFromDate.ClientID%>');

        }

        function Disablebtns() {
            document.getElementById('<%=btnReport.ClientID%>').style.display = "none";
            document.getElementById('<%=btnReportPDF.ClientID%>').style.display = "none";
            document.getElementById('<%=btnBack.ClientID%>').style.display = "inline";
        }
    </script>

    <div class="reportFormTitle">
        Impal - Salesman Performance</div>
    <div class="reportFilters">
        <table id="reportFiltersTable" class="reportFiltersTable" runat="server">
            <tr>
                <td class="label">
                    <asp:Label ID="lblfromdate" Text="Date" SkinID="LabelNormal" runat="server">
                    </asp:Label>
                </td>
                <td class="inputcontrols">
                    <asp:TextBox ID="txtFromDate" SkinID="TextBoxCalendarExtenderNormal" TabIndex="1"
                        runat="server" onblur="return checkDateForValidDate(this.id);"> </asp:TextBox>
                    <ajaxToolkit:CalendarExtender ID="calFromdate" Format="dd/MM/yyyy" runat="server"
                        TargetControlID="txtFromDate">
                    </ajaxToolkit:CalendarExtender>
                </td>
            </tr>
        </table>
        <div class="reportButtons">
            <asp:Button SkinID="ButtonViewReport" ID="btnReport" runat="server" Text="Excel Report"
                TabIndex="3" OnClick="btnReport_Click" OnClientClick="javascript:Disablebtns();" />
            <asp:Button SkinID="ButtonViewReport" ID="btnReportPDF" runat="server" Text="PDF Report"
                TabIndex="3" OnClick="btnReportPDF_Click" OnClientClick="javascript:Disablebtns();" />
            <asp:Button ID="btnBack" runat="server" SkinID="ButtonNormal" Text="Back" OnClick="btnBack_Click" />
        </div>
    </div>
</asp:Content>
