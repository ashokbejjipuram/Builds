<%@ Page Language="C#" MasterPageFile="~/Main.Master" AutoEventWireup="true" CodeBehind="OutstandIndus.aspx.cs" Inherits="IMPALWeb.Dashboard.FinanceDashboard.OutstandIndus"%>
<%@ Register Src="~/UserControls/SSRSReport.ascx" TagName="SSRSReport" TagPrefix="UC" %>
<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="ajaxToolkit" %>
<asp:Content ID="Content1" ContentPlaceHolderID="CPHDetails" runat="server">
<div class="reportFormTitle reportFormTitleExtender350">
        INDUSTRIAL DEALERS OUTSTANDING 
    </div>
     <div>
        <table id="dashReportTable" class="dashReportTable" runat="server" 
            style="width:581px; height: 75px;">
           <%-- <tr>
                <td class="label" style="height: 75px; width: 48px;">
                    <asp:Label ID="lblDatePick" runat="server" SkinID="LabelNormal" Text="Date"></asp:Label>
                    <span class="asterix">*</span>
                </td>
                <td class="inputcontrols" style="width: 99px; height: 75px">
                    <asp:TextBox ID="txtDatePick" runat="server" 
                        SkinID="TextBoxCalendarExtenderNormal" 
                        onblur="return CheckValidDate(this.id, true, 'DatePick');" Height="25px" 
                        Width="165px" Visible="False"></asp:TextBox>
                    <ajaxToolkit:CalendarExtender ID="ddlDatePick" PopupButtonID="imgDate" Format="dd/MM/yyyy" runat="server" TargetControlID="txtDatePick" />        
                </td>                
            </tr>--%>
        </table>
       <%-- <div class="reportButtons">
            <asp:Button ID="btnReport" runat="server" Text="Generate Report" SkinID="ButtonViewReport" TabIndex="3" OnClick="btnReport_Click" OnClientClick="javaScript:return validateMonthYear();" />
        </div>--%>
    </div>
    <div class="dashReportHolder" id="dashReportHolder" runat="server">
        <UC:SSRSReport ID="DashIndus" runat="server" />
    </div>
</asp:Content>


