<%@ Page Language="C#" MasterPageFile="~/Main.Master" AutoEventWireup="true" CodeBehind="GLAccMasterReport.aspx.cs"
    Inherits="IMPALWeb.Masters.GeneralLedger.GLAccMasterReport" %>
<%@ Register Src="~/UserControls/CrystalReport.ascx" TagName="CrystalReport" TagPrefix="UC" %>
<%@ PreviousPageType VirtualPath = "~/Masters/GeneralLedger/GLAccMaster.aspx" %>

<asp:Content ID="CPHDetails" ContentPlaceHolderID="CPHDetails" runat="server">
    <div id="DivOuter">
        <div class="subFormTitle">
            GL Account Master
        </div>
        <asp:UpdatePanel ID="UpdatePanel1" runat="server">
            <ContentTemplate>
             <div class="reportButtons">
                    <asp:Button class="reportButtons" SkinID="ButtonViewReport" ID="btnBack" runat="server"
                        Text="Back" OnClick="btnBack_Click" />
                </div>
                <table>
                    <tr>
                        <td>
                            <UC:CrystalReport ID="crGLAccMaster" runat="server" ReportName="GLAccMaster" />
                        </td>
                    </tr>
                </table>
               
            </ContentTemplate>
            <Triggers>
                <asp:PostBackTrigger ControlID="crGLAccMaster" />
            </Triggers>
        </asp:UpdatePanel>
    </div>
</asp:Content>
