<%@ Page Title="" Language="C#" MasterPageFile="~/Main.Master" AutoEventWireup="true" CodeBehind="GLAccountSetupReport.aspx.cs" Inherits="IMPALWeb.Masters.GeneralLedger.GLAccountSetupReport" %>
<%@ Register src="~/UserControls/CrystalReport.ascx" tagname="CrystalReport" tagprefix="UC" %>
<%@ PreviousPageType VirtualPath="~/Masters/GeneralLedger/GLAccountSetup.aspx" %>
<asp:Content ID="Content1" ContentPlaceHolderID="CPHDetails" runat="server">
    <div id="DivOuter">
        <div class="subFormTitle subFormTitleExtender250">
            GL Account Setup Report
        </div>
        <asp:UpdatePanel ID="UpdGLAcctSetupRpt" runat="server">
            <ContentTemplate>
                <div class="reportButtons">
                    <asp:Button SkinID="ButtonViewReport" ID="btnBack" runat="server" Text="Back" OnClick="btnBack_Click" />
                </div>
                <table>
                    <tr>
                        <td>
                            <UC:CrystalReport ID="crGLAccountSetup" runat="server" ReportName="AccountSetup" />
                        </td>
                    </tr>
                </table>
            </ContentTemplate>
            <Triggers>
                <asp:PostBackTrigger ControlID="crGLAccountSetup" />
            </Triggers>
        </asp:UpdatePanel>
    </div>

</asp:Content>
