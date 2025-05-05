<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="AreaWiseStock.aspx.cs"
    Inherits="IMPALWeb.Reports.Inventory.AreaWiseStock" MasterPageFile="~/Main.Master" %>

<%@ Register Src="~/UserControls/CrystalReport.ascx" TagName="CrystalReport" TagPrefix="UC" %>
<asp:Content ID="MainContent" ContentPlaceHolderID="CPHDetails" runat="server">
    <script language="javascript" type="text/javascript">
        function Validate() {
            var ddlZone = document.getElementById('<%=ddlZone.ClientID%>');
            var ddlState = document.getElementById('<%=ddlState.ClientID%>');
            var ddlBranch = document.getElementById('<%=ddlBranch.ClientID%>');
            var ddlSupplier = document.getElementById('<%=ddlSupplier.ClientID%>');
            var ddlItemCode = document.getElementById('<%=ddlItemCode.ClientID%>');
            if (ddlZone.value == "") {
                alert("Zone Code shouldn't be empty");
                ddlZone.focus();
                return false;
            }
            else if (ddlState.value == "") {
                alert("State shouldn't be empty");
                ddlState.focus();
                return false;
            }
            else if (ddlBranch.value == "") {
                alert("Branch shouldn't be empty");
                ddlBranch.focus();
                return false;
            }
            else if (ddlSupplier.value == "0") {
                alert("Supplier Line Code shouldn't be empty");
                ddlSupplier.focus();
                return false;
            }
            else if (ddlSupplier.value == "ALL" && ddlItemCode.value == "") {
                alert("Item Code shouldn't be empty");
                ddlItemCode.focus();
                return false;
            }
        }
    </script>

    <div class="reportFormTitle reportFormTitleExtender350">
        Zone/Branch/State/Age Full Stock</div>
    <div class="reportFilters">
        <table id="reportFiltersTable" class="reportFiltersTable" runat="server">
            <tr>
                <td class="label">
                    <asp:Label ID="lblZone" Text="Zone" runat="server" SkinID="LabelNormal"></asp:Label>
                    <span class="asterix">*</span>
                </td>
                <td class="inputcontrols">
                    <asp:DropDownList ID="ddlZone" runat="server" DataTextField="ZoneName" DataValueField="ZoneCode"
                        TabIndex="1" OnSelectedIndexChanged="ddlZone_OnSelectedIndexChanged" AutoPostBack="True"
                        Enabled="false" SkinID="DropDownListNormal" />
                </td>
                <td class="label">
                    <asp:Label ID="lblState" Text="State" runat="server" SkinID="LabelNormal"></asp:Label>
                    <span class="asterix">*</span>
                </td>
                <td class="inputcontrols">
                    <asp:DropDownList ID="ddlState" runat="server" DataTextField="StateName" DataValueField="StateCode"
                        OnSelectedIndexChanged="ddlState_OnSelectedIndexChanged" TabIndex="2" Enabled="false"
                        AutoPostBack="True" SkinID="DropDownListNormal" />
                </td>
                <td class="label">
                    <asp:Label ID="lblBranch" Text="Branch" runat="server" SkinID="LabelNormal"></asp:Label>
                    <span class="asterix">*</span>
                </td>
                <td class="inputcontrols">
                    <asp:DropDownList ID="ddlBranch" runat="server" DataTextField="BranchName" DataValueField="BranchCode"
                        TabIndex="3" Enabled="false" SkinID="DropDownListNormal" />
                </td>
            </tr>
            <tr>
                <td class="label">
                    <asp:Label ID="lblSupplier" Text="Supplier" runat="server" SkinID="LabelNormal"></asp:Label>
                    <span class="asterix">*</span>
                </td>
                <td class="inputcontrols">
                    <asp:DropDownList ID="ddlSupplier" runat="server" DataTextField="SupplierName" DataValueField="SupplierCode"
                        OnSelectedIndexChanged="ddlSupplier_OnSelectedIndexChanged" TabIndex="4" AutoPostBack="True"
                        SkinID="DropDownListNormal" />
                </td>
                <td class="label">
                    <asp:Label ID="lblItemCode" Text="ItemCode" runat="server" SkinID="LabelNormal"></asp:Label>
                </td>
                <td class="inputcontrols">
                    <asp:DropDownList ID="ddlItemCode" runat="server" DataTextField="Name" DataValueField="Code"
                        TabIndex="5" Enabled="false" SkinID="DropDownListNormal" />
                </td>
            </tr>
        </table>
        <div class="reportButtons">
            <asp:Button ID="btnReport" runat="server" Text="Generate Report" OnClick="btnReport_Click"
                OnClientClick="javaScript:return Validate();" TabIndex="6" SkinID="ButtonViewReport" />
        </div>
    </div>
    <div class="reportViewerHolder" id="reportViewerHolder" runat="server">
        <UC:CrystalReport ID="crAreaWiseStock" runat="server" OnUnload="crAreaWiseStock_Unload" ReportName="AreaWiseStock" />
    </div>
</asp:Content>
