<%@ Page Language="C#" MasterPageFile="~/Main.Master" AutoEventWireup="true" CodeBehind="DateWiseSalesValueNewHO.aspx.cs"
    Inherits="IMPALWeb.Reports.Sales.SalesListing.DateWiseSalesValueNewHO" %>

<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="ajaxToolkit" %>
<asp:Content ID="Content1" ContentPlaceHolderID="CPHDetails" runat="server">
    <link href="../../../App_Themes/Styles/GridStyle.css" rel="stylesheet" type="text/css" />
    <script language="javascript" type="text/javascript">
        function fnValidate() {
            var ddlBranch = document.getElementById('<%=ddlBranch.ClientID%>');

            if (ddlBranch.value.trim() == "" || ddlBranch.value.trim() == "0") {
                alert("Please Select Branch");
                ddlBranch.focus();
                return false;
            }

            var txtFromDate = document.getElementById('<%=txtFromDate.ClientID%>');
            var txtToDate = document.getElementById('<%=txtToDate.ClientID%>');
            return ValidateDates(txtFromDate, txtToDate);
        }
    </script>

    <div class="reportFormTitle">
        Datewise Sales Value - New - HO
    </div>
    <div class="reportFilters">
        <asp:Panel ID="PnlreportFilters" runat="server">
            <table id="reportFiltersTable" class="reportFiltersTable" runat="server">
                <tr>
                    <td class="label">
                        <asp:Label ID="lblBranch" Text="Branch" runat="server" SkinID="LabelNormal"></asp:Label>
                        <span class="asterix">*</span>
                    </td>
                    <td class="inputcontrols">
                        <asp:DropDownList ID="ddlBranch" runat="server" DataSourceID="ODS_AllBranch" DataTextField="BranchName" DataValueField="BranchCode" TabIndex="3" SkinID="DropDownListNormal" />
                    </td>
                    <td class="label">
                        <asp:Label ID="lblfromdate" Text="From Date" SkinID="LabelNormal" runat="server"></asp:Label>
                        <span class="asterix">*</span>
                    </td>
                    <td class="inputcontrols">
                        <asp:TextBox ID="txtFromDate" SkinID="TextBoxCalendarExtenderNormal" TabIndex="1" runat="server"></asp:TextBox>
                        <ajaxToolkit:CalendarExtender ID="calFromdate" Format="dd/MM/yyyy" runat="server" TargetControlID="txtFromDate">
                        </ajaxToolkit:CalendarExtender>
                    </td>
                    <td class="label">
                        <asp:Label ID="lblToDate" Text="To Date" SkinID="LabelNormal" runat="server"></asp:Label>
                        <span class="asterix">*</span>
                    </td>
                    <td class="inputcontrols">
                        <asp:TextBox ID="txtToDate" SkinID="TextBoxCalendarExtenderNormal" TabIndex="2" runat="server"></asp:TextBox>
                        <ajaxToolkit:CalendarExtender ID="calTodate" Format="dd/MM/yyyy" runat="server" TargetControlID="txtToDate">
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
    <asp:ObjectDataSource ID="ODS_AllBranch" runat="server" SelectMethod="GetAllBranch"
        TypeName="IMPALLibrary.Branches" DataObjectTypeName="IMPALLibrary.Branches"></asp:ObjectDataSource>
</asp:Content>
