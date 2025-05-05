<%@ Page Language="C#" MasterPageFile="~/Main.Master" AutoEventWireup="true" CodeBehind="ChartOfAccReport.aspx.cs" Inherits="IMPALWeb.Masters.GeneralLedger.ChartOfAccReport" %>

<%@ Register Src="~/UserControls/CrystalReport.ascx" TagName="CrystalReport" TagPrefix="UC" %>
<%@ PreviousPageType VirtualPath = "~/Masters/GeneralLedger/ChartOfAccountMaster.aspx" %>
<%--<asp:Content ID="Content2" ContentPlaceHolderID="CPH_Header" runat="server">
</asp:Content>--%>
<asp:Content ID="CPHDetails" ContentPlaceHolderID="CPHDetails" runat="server">
    <div id="DivOuter">
        <div class="subFormTitle">
            Chart Of Account</div>
           
        <asp:UpdatePanel ID="UpdPnlChartOfAccountMasterReport" runat="server">
            <ContentTemplate>
             <div class="reportButtons">
                    <asp:Button ID="btnBack" runat="server" SkinID="ButtonNormal" Text="Back" OnClick="btnBack_Click" />
                </div>
                <table>
                    <tr>
                        <td>
                            <UC:CrystalReport ID="crGLChartOfAccMaster" runat="server" ReportName="GLChartOfAccMaster" />
                        </td>
                    </tr>
                </table>
                
            </ContentTemplate>
            <Triggers>
                <asp:PostBackTrigger ControlID="crGLChartOfAccMaster" />
            </Triggers>
        </asp:UpdatePanel>
    </div>
</asp:Content>
