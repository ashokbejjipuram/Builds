<%@ Page Title="" Language="C#" MasterPageFile="~/Main.Master" AutoEventWireup="true"
    CodeBehind="DataTransferToBI.aspx.cs" Inherits="IMPALWeb.Reports.Miscellaneous.DataTransferToBI" %>

<asp:Content ID="Content1" ContentPlaceHolderID="CPHDetails" runat="server">

    <div class="reportFormTitle reportFormTitleExtender250">
        Sales & Stock Data Transfer to BI
    </div>
    <asp:UpdatePanel ID="upHeader" runat="server" UpdateMode="Conditional">
        <ContentTemplate>
            <div class="reportButtons">
                <asp:Button ID="btnReport" Text="Transfer Data" runat="server" TabIndex="8" SkinID="ButtonViewReport"  OnClick="btnReport_Click" />
                <asp:Button ID="btnReportSalesData" Text="Transfer Sales Data" runat="server" TabIndex="8" SkinID="ButtonViewReport"  OnClick="btnReportSalesData_Click" />
            </div>
        </ContentTemplate>
    </asp:UpdatePanel>
</asp:Content>
