<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="SSRSReport.ascx.cs" Inherits="IMPALWeb.UserControls.SSRSReport" %>
<%@ Register Assembly="Microsoft.ReportViewer.WebForms, Version=9.0.0.0, Culture=neutral, PublicKeyToken=b03f5f7f11d50a3a"
    Namespace="Microsoft.Reporting.WebForms" TagPrefix="rsweb" %>
<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="ajax" %>

<style type="text/css">
    .unselectable
    {
        -moz-user-select: -moz-none;
        -khtml-user-select: none;
        -webkit-user-select: none;
        -ms-user-select: none;
    }
    .selector_cat
    {
        border: none;
        width: 487px;
        height: 95px;
    }
    .FontChange
    {   
    	font-style:normal;
    	font-family:Courier;
    	font-size:8px;
    }
</style>

<script type="text/javascript">
function PagesNumberOnly() {
    var AsciiValue = event.keyCode;

    if ((AsciiValue >= 48 && AsciiValue <= 57) || (AsciiValue == 8 || AsciiValue == 44 || AsciiValue == 45))
        event.returnValue = true;
    else
        event.returnValue = false;    
}
</script>
<table>
<tr>
<td>
     <asp:DropDownList runat="server" ID="drp" Height="16px"  Visible="false"          
            Width="114px" >
         <asp:ListItem>Excel</asp:ListItem>
         <asp:ListItem Value="Word">Word</asp:ListItem>
        </asp:DropDownList>
        <asp:Button ID="btnExport" runat="server" Text="Export"  Visible="false"  
         onclick="btnExport_Click" />
        
</td>
</tr>
</table>
<rsweb:ReportViewer ID="ReportViewer1" runat="server" Width="1280px" 
    Height="670px" style="overflow:auto;" ShowExportControls="false" 
    BackColor="#999999" ShowBackButton="True" ToolBarItemBorderColor="Cyan" 
    ToolBarItemPressedBorderColor="AliceBlue" 
    ToolBarItemPressedHoverBackColor="Azure" 
    ExportContentDisposition="AlwaysInline">       
</rsweb:ReportViewer>



