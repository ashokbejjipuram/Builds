<%@ Page Language="C#" MasterPageFile="~/Main.Master" AutoEventWireup="true" CodeBehind="DateWiseSalesValueHO.aspx.cs"
    Inherits="IMPALWeb.Reports.Sales.SalesListing.DateWiseSalesValueHO" %>

<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="ajaxToolkit" %>
<asp:Content ID="Content1" ContentPlaceHolderID="CPHDetails" runat="server">
    <link href="../../../App_Themes/Styles/GridStyle.css" rel="stylesheet" type="text/css" />
    <script language="javascript" type="text/javascript">
        function fnValidate() {
            var txtFromDate = document.getElementById('<%=txtFromDate.ClientID%>');
            var txtToDate = document.getElementById('<%=txtToDate.ClientID%>');
            return ValidateDates(txtFromDate, txtToDate);
        }

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
        Datewise Sales Value - HO
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
                <tr style="display: none">
                    <td class="label">
                        <asp:Label ID="lblfromdate" Text="From Date" SkinID="LabelNormal" runat="server" Style="display: none"></asp:Label><span
                            class="asterix" style="display: none">*</span>
                    </td>
                    <td class="inputcontrols">
                        <asp:TextBox ID="txtFromDate" SkinID="TextBoxCalendarExtenderNormal" TabIndex="1"
                            runat="server" Enabled="false" Style="display: none"></asp:TextBox>
                        <ajaxToolkit:CalendarExtender ID="calFromdate" Format="dd/MM/yyyy" runat="server"
                            TargetControlID="txtFromDate" Enabled="false">
                        </ajaxToolkit:CalendarExtender>
                    </td>
                    <td class="label">
                        <asp:Label ID="lblToDate" Text="To Date" SkinID="LabelNormal" runat="server"></asp:Label><span
                            class="asterix">*</span>
                    </td>
                    <td class="inputcontrols">
                        <asp:TextBox ID="txtToDate" SkinID="TextBoxCalendarExtenderNormal" TabIndex="2" runat="server" Enabled="false" Style="display: none"></asp:TextBox>
                        <ajaxToolkit:CalendarExtender ID="calTodate" Format="dd/MM/yyyy" runat="server" TargetControlID="txtToDate" Enabled="false">
                        </ajaxToolkit:CalendarExtender>
                    </td>
                </tr>
            </table>
        </asp:Panel>
        <div class="reportButtons">
            <asp:Button SkinID="ButtonViewReport" ID="btnReport" runat="server" Text="Generate Report"
                TabIndex="3" OnClientClick="javaScript:return fnValidate();" OnClick="btnReport_Click" />
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
                        <asp:ListItem Text="1000" Selected="True" Value="1000"></asp:ListItem>
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
        <asp:GridView ID="grdHODatewiseValue" runat="server" Width="100%" AutoGenerateColumns="true" OnRowDataBound="grdHODatewiseValue_RowDataBound"
            AllowPaging="true" CssClass="Grid" AlternatingRowStyle-CssClass="alt" OnPageIndexChanging="grdHODatewiseValue_PageIndexChanging"
            PagerStyle-CssClass="pgr" ShowHeaderWhenEmpty="True" EmptyDataText="No Records found.">
            <HeaderStyle BackColor="Red" ForeColor="White"></HeaderStyle>
            <AlternatingRowStyle BackColor="#FFFFCC" />
            <PagerSettings FirstPageText="First" LastPageText="Last" NextPageText="Next" PreviousPageText="Previous" Position="Top" PageButtonCount="5" Mode="NumericFirstLast" />
        </asp:GridView>
    </div>
</asp:Content>
