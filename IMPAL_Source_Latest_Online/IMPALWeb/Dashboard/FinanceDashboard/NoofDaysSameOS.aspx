<%@ Page Language="C#" MasterPageFile="~/Main.Master" AutoEventWireup="true" CodeBehind="NoofDaysSameOS.aspx.cs" Inherits="IMPALWeb.Dashboard.FinanceDashboard.NoofDaysSameOS"%>
<%@ Register Src="~/UserControls/SSRSReport.ascx" TagName="SSRSReport" TagPrefix="UC" %>
<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="ajaxToolkit" %>
<asp:Content ID="Content1" ContentPlaceHolderID="CPHDetails" runat="server">
<div class="reportFormTitle reportFormTitleExtender350">
        Same month O/S / Same month Sales
    </div>
     <div>
        <table id="dashReportTable" class="dashReportTable" runat="server" 
            style="width:581px; height: 75px;">
            <tr>
                <td class="label" style="height: 75px; width: 48px;">
                    <asp:Label ID="lblDatePick" runat="server" SkinID="LabelNormal" Text="Date"></asp:Label>
                    <span class="asterix">*</span>
                </td>
                <td class="inputcontrols" style="width: 99px; height: 75px">
                    <asp:TextBox ID="txtDatePick" runat="server" 
                        SkinID="TextBoxCalendarExtenderNormal" 
                        onblur="return CheckValidDate(this.id, true, 'DatePick');" Height="25px" 
                        Width="165px"></asp:TextBox>
                    <ajaxToolkit:CalendarExtender ID="ddlDatePick" PopupButtonID="imgDate" Format="dd/MM/yyyy" runat="server" TargetControlID="txtDatePick" />        
                </td>
                <td class="label" style="height: 144px; width: 79px;">
                    <asp:Label ID="lblReportType" runat="server" SkinID="LabelNormal" Text="Report Type"></asp:Label>
                    <span class="asterix">*</span></td>
                <td class="inputcontrols" style="width: 140px; height: 144px">
                    <asp:DropDownList ID="ddlReportList" runat="server" SkinID="DropDownListNormal" TabIndex="1" AutoPostBack="True" Height="29px" Width="179px">
                        <asp:ListItem> -- Select Report Type -- </asp:ListItem>
                        <asp:ListItem>Same Month OS Now</asp:ListItem>
                        <asp:ListItem>Same Month OS With STU</asp:ListItem>
                        <asp:ListItem>Same Month OS Without STU</asp:ListItem>
                        <asp:ListItem>Zone Wise OS</asp:ListItem>
                    </asp:DropDownList>
                </td>
            </tr>
        </table>
        <div class="reportButtons">
            <asp:Button ID="btnReport" runat="server" Text="Generate Report" SkinID="ButtonViewReport" TabIndex="3" OnClick="btnReport_Click" OnClientClick="javaScript:return validateMonthYear();" />
        </div>
    </div>
    <div class="dashReportHolder" id="dashReportHolder" runat="server">
        <UC:SSRSReport ID="DashGovtTran" runat="server" />
    </div>
</asp:Content>
