<%@ Page Language="C#" MasterPageFile="~/Main.Master" AutoEventWireup="true" CodeBehind="OutstandingDaysReport.aspx.cs"
    Inherits="IMPALWeb.OutstandingDaysReport" %>

<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="ajaxToolkit" %>
<%@ Register Src="~/UserControls/CrystalReport.ascx" TagName="CrystalReport" TagPrefix="UC" %>
<asp:Content ID="Content3" ContentPlaceHolderID="CPHDetails" runat="server">
    <div id="DivTop" runat="server">
        <asp:UpdatePanel ID="updPnlOutstanding" runat="server">
            <Triggers>
                <asp:PostBackTrigger ControlID="crOutstanding" />
            </Triggers>
            <ContentTemplate>
                <div id="DivOuter" runat="server">
                    <div class="subFormTitle">
                        Outstanding Details
                    </div>
                    <div class="reportButtons">
                        <asp:Button SkinID="ButtonViewReport" ID="btnBack" runat="server" Text="Back" OnClick="btnBack_Click" />
                    </div>
                    <table class="subFormTable">
                        <tr>
                            <td>
                                <UC:CrystalReport runat="server" ID="crOutstanding" ReportName="OustandingDays" />
                            </td>
                        </tr>
                    </table>
                </div>
            </ContentTemplate>
        </asp:UpdatePanel>
    </div>
</asp:Content>
