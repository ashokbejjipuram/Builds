<%@ Page Language="C#" MasterPageFile="~/Main.Master" AutoEventWireup="true" CodeBehind="MonthEnd_Collection.aspx.cs" Inherits="IMPALWeb.Dashboard.FinanceDashboard.MonthEnd_Collection" Title="Untitled Page" %>
<%@ Register Src="~/UserControls/SSRSReport.ascx" TagName="SSRSReport" TagPrefix="UC" %>
<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="ajaxToolkit" %>
<asp:Content ID="Content1" ContentPlaceHolderID="CPHDetails" runat="server">
    <div class="reportFormTitle reportFormTitleExtender350">
        Finance Dashboard - Month End Collections
    </div>
    <div>
        <table id="dashReportTable" class="dashReportTable" runat="server" 
            style="width:581px; height: 75px;">
            <tr>
                <td class="label" style="height: 75px; width: 47px;">
                    <asp:Label ID="Date1" runat="server" SkinID="LabelNormal" Text="Date"></asp:Label>
                    <span class="asterix">*</span>
                </td>
                <td class="inputcontrols" style="width: 99px; height: 75px">
                    <asp:TextBox ID="txtDatePick" runat="server" 
                        SkinID="TextBoxCalendarExtenderNormal" 
                        onblur="return CheckValidDate(this.id, true, 'DatePick');" Height="25px" 
                        Width="165px"></asp:TextBox>
                    <ajaxToolkit:CalendarExtender ID="CalendarExtender" PopupButtonID="imgDate" 
                        Format="dd/MM/yyyy" runat="server" TargetControlID="txtDatePick" />        
                </td>
                <td class="label" style="height: 144px; width: 79px;">
                    <asp:Label ID="lblReportType1" runat="server" SkinID="LabelNormal" 
                        Text="Report Type"></asp:Label>
                    <span class="asterix">*</span></td>
                <td class="inputcontrols" style="width: 140px; height: 144px">
                    <asp:DropDownList ID="ddlReportList1" runat="server" 
                        SkinID="DropDownListNormal" TabIndex="1" AutoPostBack="True" Height="29px" 
                        Width="179px" 
                       >
                        <asp:ListItem> --Select Report Type-- </asp:ListItem>
                        <asp:ListItem>REMIITANCE</asp:ListItem>
                        <asp:ListItem>COLLECTION ZONE WISE</asp:ListItem>
                        <asp:ListItem>CUMULATIVE COLLECTION %</asp:ListItem>
                        <asp:ListItem>MONTH END COLLECTION ASC</asp:ListItem>
                        <asp:ListItem>CUMULATIVE COLLECTION ASC</asp:ListItem>
                    </asp:DropDownList>
                </td>
            </tr>
        </table>
        <div class="reportButtons">
            <asp:Button ID="btnReport1" runat="server" Text="Generate Report" 
                SkinID="ButtonViewReport" TabIndex="3" OnClick="btnReport_Click" 
                OnClientClick="javaScript:return validateMonthYear();" />
        </div>
    </div>
    <div class="dashReportHolder" id="dashReportHolder1" runat="server">
        <UC:SSRSReport ID="DashMonthEndCollection1" runat="server" />
    </div>
</asp:Content>
