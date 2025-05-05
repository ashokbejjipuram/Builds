<%@ Page Language="C#" MasterPageFile="~/Main.Master" AutoEventWireup="true" CodeBehind="GLMasterReport.aspx.cs"
    Inherits="IMPALWeb.Masters.GeneralLedger.GLMasterReport" %>
<%@ PreviousPageType VirtualPath = "~/Masters/GeneralLedger/GLMaster.aspx" %>
<%@ Register Src="~/UserControls/CrystalReport.ascx" TagName="CrystalReport" TagPrefix="UC" %>

<%--<asp:Content ID="Content2" ContentPlaceHolderID="CPH_Header" runat="server">
</asp:Content>--%>
<asp:Content ID="CPHDetails" ContentPlaceHolderID="CPHDetails" runat="server">
    <div id="DivOuter">
        <div class="subFormTitle">
            GL Master</div>
        <asp:UpdatePanel ID="UpdatePanel1" runat="server">
            <ContentTemplate>
                <div class="reportButtons">
                    <asp:Button SkinID="ButtonViewReport" ID="btnGLMasterBack" runat="server" Text="Back"
                        OnClick="btnGLMasterBack_Click" />
                </div>
                <table>
                    <tr>
                        <td>
                            <UC:CrystalReport ID="crGLMaster" runat="server" ReportName="GLMaster" />
                        </td>
                    </tr>
                </table>
            </ContentTemplate>
            <Triggers>
                <asp:PostBackTrigger ControlID="crGLMaster" />
            </Triggers>
        </asp:UpdatePanel>
    </div>
</asp:Content>
