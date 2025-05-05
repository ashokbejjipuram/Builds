<%@ Page Title="" Language="C#" MasterPageFile="~/Main.Master" AutoEventWireup="true"
    CodeBehind="StockValueHO.aspx.cs" Inherits="IMPALWeb.Reports.Ordering.Stock.StockValueHO" %>

<asp:Content ID="Content1" ContentPlaceHolderID="CPHDetails" runat="server">
    <link href="../../../App_Themes/Styles/GridStyle.css" rel="stylesheet" type="text/css" />
    <script language="javascript" type="text/javascript">
        function UnCheckBox(id) {
            var CtrlIdPrefix = "ctl00_CPHDetails_";
            document.getElementById(CtrlIdPrefix + 'chkZone').checked = false;
            document.getElementById(CtrlIdPrefix + 'chkState').checked = false;
            document.getElementById(CtrlIdPrefix + 'chkBranch').checked = false;

            if (id == CtrlIdPrefix + "ddlZone" || id == CtrlIdPrefix + "ddlState") {
                __doPostBack(id, 0);
            }

            return true;
        }
    </script>

    <div class="reportFormTitle">
        Stock Value
    </div>
    <div class="reportFilters">
        <asp:Panel ID="PnlreportFilters" runat="server">
            <table id="reportFiltersTable" class="reportFiltersTable" runat="server">
                <tr>
                    <td class="label">
                        <asp:CheckBox ID="chkZone" runat="server" Text="" />
                        <asp:Label ID="lblZone" Text="Zone" runat="server" SkinID="LabelNormal"></asp:Label>
                        <span class="asterix">*</span>
                    </td>
                    <td class="inputcontrols">
                        <asp:DropDownList ID="ddlZone" runat="server" DataTextField="ZoneName" DataValueField="ZoneCode"
                            TabIndex="1" OnSelectedIndexChanged="ddlZone_OnSelectedIndexChanged" AutoPostBack="True"
                            Enabled="false" SkinID="DropDownListNormal" onchange="return UnCheckBox(id);" />
                    </td>
                    <td class="label">
                        <asp:CheckBox ID="chkState" runat="server" Text="" />
                        <asp:Label ID="lblState" Text="State" runat="server" SkinID="LabelNormal"></asp:Label>
                        <span class="asterix">*</span>
                    </td>
                    <td class="inputcontrols">
                        <asp:DropDownList ID="ddlState" runat="server" DataTextField="StateName" DataValueField="StateCode"
                            OnSelectedIndexChanged="ddlState_OnSelectedIndexChanged" TabIndex="2" Enabled="false"
                            AutoPostBack="True" SkinID="DropDownListNormal" onchange="return UnCheckBox(id);" />
                    </td>
                    <td class="label">
                        <asp:CheckBox ID="chkBranch" runat="server" Text="" />
                        <asp:Label ID="lblBranch" Text="Branch" runat="server" SkinID="LabelNormal"></asp:Label>
                        <span class="asterix">*</span>
                    </td>
                    <td class="inputcontrols">
                        <asp:DropDownList ID="ddlBranch" runat="server" DataTextField="BranchName" DataValueField="BranchCode"
                            OnSelectedIndexChanged="ddlBranch_OnSelectedIndexChanged" TabIndex="3" Enabled="false"
                            SkinID="DropDownListNormal" onchange="return UnCheckBox(id);" />
                    </td>
                </tr>
            </table>
        </asp:Panel>
        <div class="reportButtons">
            <asp:Button TabIndex="2" SkinID="ButtonViewReport" ID="btnReport" runat="server"
                Text="Generate Report" OnClick="btnReport_Click" />
            <asp:Button ID="btnExport" SkinID="ButtonExportReport" Text="Export Report" runat="server"
                OnClick="btnExport_Click" CssClass="buttonViewReport reportViewerHolder" />
        </div>
    </div>
    <div class="reportViewerHolder" id="reportViewerHolder" runat="server">
        <table id="Table1" class="reportFiltersTable" runat="server">
            <tr>
                <td class="label" style="display: none">
                    <asp:Label ID="Label2" SkinID="LabelNormal" runat="server" Text="Rows per page"></asp:Label>
                </td>
                <td class="inputcontrols" style="display: none">
                    <asp:DropDownList ID="ddlpagelimit" runat="server" AutoPostBack="true" OnSelectedIndexChanged="ddlpagelimit_SelectedIndexChanged" SkinID="DropDownListNormal">
                        <asp:ListItem Text="1000" Value="1000" Selected="True"></asp:ListItem>
                    </asp:DropDownList>
                </td>
                <td class="label" style="display: none">
                    <asp:Label ID="Label3" SkinID="LabelNormal" runat="server" Text="Total No of Records"></asp:Label>

                </td>
                <td class="inputcontrols" style="display: none">
                    <asp:Label ID="lblTotalRecords" SkinID="LabelNormal" runat="server" Text="0" Font-Bold="true"></asp:Label>
                </td>
            </tr>
        </table>
        <asp:GridView ID="grdStockValue" runat="server" Width="100%" AutoGenerateColumns="true" OnRowDataBound="grdStockValue_RowDataBound"
            AllowPaging="true" CssClass="Grid" AlternatingRowStyle-CssClass="alt" OnPageIndexChanging="grdStockValue_PageIndexChanging"
            PagerStyle-CssClass="pgr" ShowHeaderWhenEmpty="True" EmptyDataText="No Records found.">
            <HeaderStyle BackColor="Red" ForeColor="White"></HeaderStyle>
            <AlternatingRowStyle BackColor="#FFFFCC" />
            <PagerSettings FirstPageText="First" LastPageText="Last" NextPageText="Next" PreviousPageText="Previous" Position="Top" PageButtonCount="5" Mode="NumericFirstLast" />
        </asp:GridView>
    </div>
    <asp:ObjectDataSource ID="ODBranch" runat="server" SelectMethod="GetAllBranch" TypeName="IMPALLibrary.Branches"></asp:ObjectDataSource>
</asp:Content>
