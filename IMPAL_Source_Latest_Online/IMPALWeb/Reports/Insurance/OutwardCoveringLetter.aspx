<%@ Page Language="C#" MasterPageFile="~/Main.Master" AutoEventWireup="true" CodeBehind="OutwardCoveringLetter.aspx.cs"
    Inherits="IMPALWeb.Reports.Insurance.OutwardCoveringLetter" %>

<%@ Register Src="~/UserControls/CrystalReport.ascx" TagName="CrystalReport" TagPrefix="UC" %>
<asp:Content ID="CPHDetails" ContentPlaceHolderID="CPHDetails" runat="server">
    <div class="reportViewerHolder">
        <UC:CrystalReport runat="server" ID="crOutwardCoveringLetter" OnUnload="crOutwardCoveringLetter_Unload" ReportName="OutwardCoveringLetter" />
    </div>
</asp:Content>
