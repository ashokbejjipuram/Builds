<%@ Page Language="C#" MasterPageFile="~/Main.Master" AutoEventWireup="true" CodeBehind="SLBDetailsReport.aspx.cs" Inherits="IMPALWeb.Masters.SLB.SLBDetailsReport" %>

<%@ Register Src="~/UserControls/CrystalReport.ascx" TagName="CrystalReport" TagPrefix="UC" %>
<%@ PreviousPageType VirtualPath = "~/Masters/SLB/SLBDetails.aspx" %>
<%--<asp:Content ID="Content2" ContentPlaceHolderID="CPH_Header" runat="server">
</asp:Content>--%>
<asp:Content ID="CPHDetails" ContentPlaceHolderID="CPHDetails" runat="server">
    <div id="DivOuter">
        <div class="subFormTitle">
            SLB Detail</div>
        <asp:UpdatePanel ID="UpdPnlSLBDetailReport" runat="server">
            <ContentTemplate>
             <div class="reportButtons">
                    <asp:Button ID="btnBack" runat="server" SkinID="ButtonNormal" Text="Back" OnClick="btnBack_Click" />
                </div>
                <table>
                    <tr>
                    <td>
                    <UC:CrystalReport ID="crSLBDetailReport" runat="server" ReportName="SLBDetailReport" />
                    </td>
                    </tr>
                </table>
            </ContentTemplate>
            <Triggers>
                <asp:PostBackTrigger ControlID="crSLBDetailReport" />
            </Triggers>
        </asp:UpdatePanel>
    </div>
</asp:Content>