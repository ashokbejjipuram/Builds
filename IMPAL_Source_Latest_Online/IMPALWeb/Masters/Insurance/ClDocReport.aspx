<%@ Page Language="C#" MasterPageFile="~/Main.Master" AutoEventWireup="true" CodeBehind="ClDocReport.aspx.cs"
    Inherits="IMPALWeb.Masters.Insurance.ClDocReport" %>

<%@ Register Src="~/UserControls/CrystalReport.ascx" TagName="CrystalReport" TagPrefix="UC" %>
<%--<asp:Content ID="Content2" ContentPlaceHolderID="CPH_Header" runat="server">
</asp:Content>--%>
<asp:Content ID="CPHDetails" ContentPlaceHolderID="CPHDetails" runat="server">
    <div id="DivOuter">
        <div class="subFormTitle subFormTitleExtender250">
            Classification Documents</div>
        <asp:UpdatePanel ID="UpdPnlClassDoc" runat="server">
            <ContentTemplate>
                <div class="reportButtons">
                    <asp:Button ID="btnBack" runat="server" SkinID="ButtonNormal" Text="Back" OnClick="btnBack_Click" />
                </div>
                <table>
                    <tr>
                        <td>
                            <UC:CrystalReport ID="crClDocReport" runat="server" ReportName="ClDocReport" />
                        </td>
                    </tr>
                </table>
            </ContentTemplate>
            <Triggers>
                <asp:PostBackTrigger ControlID="crClDocReport" />
            </Triggers>
        </asp:UpdatePanel>
    </div>
</asp:Content>
