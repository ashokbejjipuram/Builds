<%@ Page Language="C#" MasterPageFile="~/Main.Master" AutoEventWireup="true" CodeBehind="BudgetMasterReport.aspx.cs" Inherits="IMPALWeb.Masters.GeneralLedger.BudgetMasterReport" %>

<%@ Register Src="~/UserControls/CrystalReport.ascx" TagName="CrystalReport" TagPrefix="UC" %>
<%@ PreviousPageType VirtualPath = "~/Masters/GeneralLedger/BudgetMaster.aspx" %>
<%--<asp:Content ID="Content2" ContentPlaceHolderID="CPH_Header" runat="server">
</asp:Content>--%>
<asp:Content ID="CPHDetails" ContentPlaceHolderID="CPHDetails" runat="server">
    <div id="DivOuter">
        <div class="subFormTitle">
            Budget Master</div>
           
        <asp:UpdatePanel ID="UpdPnlBudgetMasterReport" runat="server">
            <ContentTemplate>
             <div class="reportButtons">
                    <asp:Button ID="btnBack" runat="server" SkinID="ButtonNormal" Text="Back" OnClick="btnBack_Click" />
                </div>
                <table>
                    <tr>
                        <td>
                            <UC:CrystalReport ID="crGLBudgetMaster" runat="server" ReportName="GLBudgetMaster" />
                        </td>
                    </tr>
                </table>
                
            </ContentTemplate>
            <Triggers>
                <asp:PostBackTrigger ControlID="crGLBudgetMaster" />
            </Triggers>
        </asp:UpdatePanel>
    </div>
</asp:Content>
