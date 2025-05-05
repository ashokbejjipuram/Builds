<%@ Page Language="C#" AutoEventWireup="true" MasterPageFile="~/Main.Master" CodeBehind="BranchReconcilation.aspx.cs"
    Inherits="IMPALWeb.Reports.Finance.General_Ledger.BranchReconcilation" %>

<%@ Register Src="~/UserControls/CrystalReport.ascx" TagName="CrystalReport" TagPrefix="uc" %>
<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="ajaxToolkit" %>
<asp:Content ID="Content" ContentPlaceHolderID="CPHDetails" runat="server">
    <div class="reportFormTitle">
        Branch - Reconsilation</div>
    <div class="reportFilters">
        <asp:UpdatePanel ID="upBrReconsilation" runat="server">
            <ContentTemplate>
                <table id="reportFiltersTable" class="reportFiltersTable" runat="server">
                    <tr>
                        <td>
                            <table id="reportFiltersTable1" class="reportFiltersTable">
                                <tr>
                                    <td class="label">
                                        <asp:Label ID="lblBranchCode" runat="server" Text="Branch" SkinID="LabelNormal"></asp:Label>
                                    </td>
                                    <td class="inputcontrols">
                                        <asp:DropDownList ID="ddlBranch" runat="server" DataTextField="BranchName" DataValueField="BranchCode"
                                            TabIndex="1" Enabled="false" SkinID="DropDownListNormal" />
                                    </td>
                                </tr>
                            </table>                            
                        </td>
                    </tr>
                </table>
            </ContentTemplate>
        </asp:UpdatePanel>
        <div class="reportButtons">
            <asp:Button ID="btnReport" runat="server" Text="Generate Report" OnClick="btnReport_Click"
                SkinID="ButtonViewReport" OnClientClick="javaScript:return FnValidate();" TabIndex="3" />
            <asp:Button ID="btnReset" runat="server" Text="Reset" SkinID="ButtonViewReport" TabIndex="4"
                OnClientClick="javaScript:return Reset();" Style="display: none" />
        </div>
    </div>
    <div class="reportViewerHolder" id="reportViewerHolder" runat="server">
        <uc:CrystalReport ID="crBranchReconcilation" runat="server" OnUnload="crBranchReconcilation_Unload" ReportName="CustomerOutstanding" />
    </div>
</asp:Content>
