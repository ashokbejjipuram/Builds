<%@ Page Language="C#" MasterPageFile="~/Main.Master" AutoEventWireup="true" CodeBehind="HoPaymentReport.aspx.cs"
    Inherits="IMPALWeb.Reports.Finance.Accounts_Payable.HoPaymentReport" %>

<%@ Register Src="~/UserControls/CrystalReport.ascx" TagName="CrystalReport" TagPrefix="uc1" %>
<asp:Content ID="ContentHoPaymentReport" ContentPlaceHolderID="CPHDetails" runat="server">

    <script language="javascript" type="text/javascript">
        function fnValidate() {
            var ddlCCWHNumber = document.getElementById('<%=ddlCCWHNumber.ClientID%>');
            var oCCWHNumberVal = ddlCCWHNumber.value.trim();
            if (oCCWHNumberVal == null || oCCWHNumberVal == "") {
                alert("CCWH Number should not be null");
                ddlCCWHNumber.focus();
                return false;
            }
        }        
    </script>
    <div class="reportButtons" runat="server" id="DivBackbtn" style="display:none">
        <asp:Button ID="btnBack" runat="server" SkinID="ButtonViewReport" Text="Back" OnClick="btnBack_Click" />
    </div>
    <div class="reportFormTitle">
        Head Office Payment - Report</div>
    <div class="reportFilters" runat="server" id="divSelection">
        <table id="reportFiltersTable" class="reportFiltersTable" runat="server">
            <tr>
                <td class="label">
                    <asp:Label ID="lblCCWHNumber" SkinID="LabelNormal" Text="CCWH Number" runat="server"></asp:Label><span
                        class="asterix">*</span>
                </td>
                <td class="inputcontrols">
                    <asp:DropDownList ID="ddlCCWHNumber" SkinID="DropDownListNormal" TabIndex="2" runat="server">
                    </asp:DropDownList>
                </td>
            </tr>
        </table>
        <div class="reportButtons" runat="server" id="divButton" style="float: left">
            <asp:Button ID="btnReport" runat="server" Text="Generate Report" SkinID="ButtonViewReport"
                TabIndex="3" OnClientClick="javaScript:return fnValidate();" />
        </div>
    </div>
    <div class="reportViewerHolder" id="reportViewerHolder" runat="server">
        <uc1:CrystalReport ID="crHoPaymentReport" runat="server" OnUnload="crHoPaymentReport_Unload" />
    </div>
</asp:Content>
