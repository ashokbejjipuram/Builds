<%@ Page Language="C#" MasterPageFile="~/Main.Master" AutoEventWireup="true" CodeBehind="DynamicLockingReport.aspx.cs" Inherits="IMPALWeb.Masters.Others.DynamicLockingReport" %>
<%@ PreviousPageType VirtualPath = "~/Masters/Others/DynamicLocking.aspx" %>
<%@ Register Src="~/UserControls/CrystalReport.ascx" TagName="CrystalReport" TagPrefix="UC" %>

<asp:Content ID="CPHDetails" ContentPlaceHolderID="CPHDetails" runat="server">
    <div id="DivOuter">
        <div class="subFormTitle">
            Dynamic Locking Master
        </div>
        <asp:UpdatePanel ID="updPnlDynLockingReport" runat="server">
            <ContentTemplate>
             <div class="reportButtons">
                    <asp:Button class="reportButtons" SkinID="ButtonViewReport" ID="btnBack" runat="server"
                        Text="Back" OnClick="btnBack_Click" />
                </div>
                <table>
                    <tr>
                        <td>
                            <UC:CrystalReport ID="crDynLocking" runat="server" ReportName="DynLocking" />
                        </td>
                    </tr>
                </table>
               
            </ContentTemplate>
            <Triggers>
                <asp:PostBackTrigger ControlID="crDynLocking" />
            </Triggers>
        </asp:UpdatePanel>
    </div>
</asp:Content>

