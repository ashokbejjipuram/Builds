<%@ Page Language="C#" AutoEventWireup="true" MasterPageFile="~/Main.Master" CodeBehind="gpstatementforlines.aspx.cs"
    Inherits="IMPALWeb.Reports.SLB.gpstatementforlines" %>

<%@ Register Src="~/UserControls/CrystalReport.ascx" TagName="CrystalReport" TagPrefix="UC" %>
<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="ajax" %>
<asp:Content ID="MainContent" ContentPlaceHolderID="CPHDetails" runat="server">

    <script type="text/javascript">
 function Validate() {

     var ddlZone = document.getElementById('<%=ddlZone.ClientID%>');
     var ddlbranchcode=document.getElementById('<%=ddlbranchcode.ClientID%>');
     var ddllinecode=document.getElementById('<%=ddllinecode.ClientID%>');

     if (ddlZone.value == "") {
         alert("Zone should not be null");
         return false;
     }
     else if (ddlbranchcode.value == "") {
         alert("Branch code should not be null");
         return false;
     }
     else if (ddllinecode.value == "") {
         alert("Line code should not be null");
         return false;
     }
     else {
         return true;
     }
        }
    </script>

    <div class="reportFormTitle reportFormTitleExtender350">
        GP Statement for Selected Lines
    </div>
    <div class="reportFilters">
        <table id="reportFiltersTable" class="reportFiltersTable" runat="server">
            <tr>
                <td class="label">
                    <asp:Label ID="lblzone" Text="Zone" runat="server" SkinID="LabelNormal"></asp:Label>
                    <span class="asterix">*</span>
                </td>
                <td class="inputcontrols">
                    <asp:DropDownList ID="ddlZone" runat="server" DataTextField="ZoneName" DataValueField="ZoneName"
                        TabIndex="1" AutoPostBack="True" OnSelectedIndexChanged="ddlZone_OnSelectedIndexChanged"
                        SkinID="DropDownListNormal" />
                </td>
                <td class="label">
                    <asp:Label ID="lblbranchcode" Text="Branch Code" runat="server" SkinID="LabelNormal"></asp:Label>
                    <span class="asterix">*</span>
                </td>
                <td class="inputcontrols">
                    <asp:DropDownList ID="ddlbranchcode" runat="server" DataTextField="BranchName" DataValueField="BranchCode"
                        TabIndex="2" AutoPostBack="True" OnSelectedIndexChanged="ddlbranchcode_OnSelectedIndexChanged"
                        SkinID="DropDownListNormal" />
                </td>
            </tr>
            <tr>
                <td class="label">
                    <asp:Label ID="lbllinecode" Text="Line Code" runat="server" SkinID="LabelNormal"></asp:Label>
                    <span class="asterix">*</span>
                </td>
                <td class="inputcontrols">
                    <asp:DropDownList ID="ddllinecode" runat="server" TabIndex="3" SkinID="DropDownListNormal" />
                </td>
            </tr>
        </table>
        <div class="reportButtons">
            <%--   <asp:Button ID="btnReport" runat="server" OnClick="btnReport_Click" Text="Report"
                                    OnClientClick="javaScript:return Validate();" TabIndex="6" SkinID="ButtonViewReport" />--%>
            <asp:Button ID="btnReport" Text="Generate Report" OnClick="btnReport_Click" runat="server"
                TabIndex="4" SkinID="ButtonViewReport" OnClientClick="javaScript:return Validate();" />
        </div>
    </div>
    <div class="reportViewerHolder" id="reportViewerHolder" runat="server">
        <UC:CrystalReport ID="crslbgpstateforlines" runat="server" ReportName="StockAdjustment" OnUnload="crslbgpstateforlines_Unload" />
    </div>
</asp:Content>
