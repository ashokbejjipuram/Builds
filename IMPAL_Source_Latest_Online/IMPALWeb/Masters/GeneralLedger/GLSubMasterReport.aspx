<%@ Page Language="C#" MasterPageFile="~/Main.Master" AutoEventWireup="true" CodeBehind="GLSubMasterReport.aspx.cs"
    Inherits="IMPALWeb.Masters.GeneralLedger.GLSubMasterReport" %>
<%@ PreviousPageType VirtualPath="~/Masters/GeneralLedger/GLSubMaster.aspx" %>
<%@ Register Src="~/UserControls/CrystalReport.ascx" TagName="CrystalReport" TagPrefix="UC" %>
<%--<asp:Content ID="Content2" ContentPlaceHolderID="CPH_Header" runat="server">
</asp:Content>--%>
<asp:Content ID="CPHDetails" ContentPlaceHolderID="CPHDetails" runat="server">
    <div id="DivOuter">
        <div class="subFormTitle">
            GL Sub Master</div>
        <asp:UpdatePanel ID="UpdatePanel1" runat="server">
            <ContentTemplate>
                <div class="reportButtons">
                    <asp:Button ID="btnGLSubMasterBack" runat="server" Text="Back" SkinID="ButtonViewReport"
                        OnClick="btnGLSubMasterBack_Click" />
                </div>
                <table>
                    <tr>
                        <td>
                            <UC:CrystalReport ID="crGLSubMaster" runat="server" ReportName="GLSubMaster" />
                        </td>
                    </tr>
                </table>
            </ContentTemplate>
            <Triggers>
                <asp:PostBackTrigger ControlID="crGLSubMaster" />
            </Triggers>
        </asp:UpdatePanel>
    </div>
</asp:Content>
