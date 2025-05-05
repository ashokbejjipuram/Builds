<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="CrystalReportA4.ascx.cs"
    Inherits="IMPALWeb.CrystalReportA4" %>
<%@ Register Assembly="CrystalDecisions.Web, Version=13.0.4000.0, Culture=neutral, PublicKeyToken=692fbea5521e1304"
    Namespace="CrystalDecisions.Web" TagPrefix="CR" %>
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
        font-style: normal;
        font-family: Courier;
        font-size: 8px;
    }
</style>

<CR:CrystalReportViewer ID="crvImpalReportsViewer" runat="server" AutoDataBind="True"
    OnInit="crvImpalReports_Init" OnUnload="crvImpalReportsViewer_Unload" Height="100%" Width="100%" />

