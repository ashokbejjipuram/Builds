<%@ Page Language="C#" MasterPageFile="~/Main.Master" AutoEventWireup="true" CodeBehind="LWOrderPlacedCRP.aspx.cs"
    Inherits="IMPALWeb.Reports.Ordering.Listing.LWOrderPlacedCRP" %>

<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="ajax" %>
<asp:Content ID="Content1" ContentPlaceHolderID="CPHDetails" runat="server">

    <script language="javascript" type="text/javascript">
        function fnValidate() {
            var txtFromDate = document.getElementById('<%=txtFromDate.ClientID%>');
            var txtToDate = document.getElementById('<%=txtToDate.ClientID%>');

            //var ddlSupplierCode = document.getElementById('<%=ddlSupplierCode.ClientID %>');

            //if (ddlSupplierCode.value == "0") {
            //    alert("Please Select Supplier");
            //    return false;
            //}
            //else
            return ValidateOrderDate(txtFromDate, txtToDate);

            fnShowHideBtns();
        }

        function fnShowHideBtns() {
            document.getElementById('<%=btnReport.ClientID%>').style.display = "none";
            document.getElementById('<%=btnBack.ClientID%>').style.display = "inline";
        }
    </script>

    <div class="reportFormTitle">
        Linewise Order Placed
    </div>
    <asp:UpdatePanel ID="upHeader" runat="server" UpdateMode="Conditional">
        <ContentTemplate>
            <asp:Panel ID="PanelHeaderDtls" runat="server">
                <div class="reportFilters">
                    <table id="reportFiltersTable" class="reportFiltersTable" runat="server">
                        <tr>
                            <td class="label">
                                <asp:Label ID="lblSupplierCode" runat="server" Text="Supplier" SkinID="LabelNormal"></asp:Label>
                            </td>
                            <td class="inputcontrols">
                                <asp:DropDownList ID="ddlSupplierCode" runat="server" DataSourceID="ODSuppliers" DataTextField="SupplierName"
                                    DataValueField="SupplierCode" TabIndex="1" SkinID="DropDownListNormal">
                                </asp:DropDownList>
                                <asp:ObjectDataSource ID="ODSuppliers" runat="server" SelectMethod="GetlinewiseorderCRP"
                                    TypeName="IMPALLibrary.Suppliers"></asp:ObjectDataSource>
                            </td>
                            <td class="label">
                                <asp:Label SkinID="LabelNormal" ID="lblBranchCode" runat="server" Text="Branch Code"></asp:Label>
                            </td>
                            <td class="inputcontrols">
                                <asp:DropDownList SkinID="DropDownListNormal" ID="ddlBranchCode" runat="server" TabIndex="1"
                                    DataSourceID="ODBranch" DataTextField="BranchName" DataValueField="BranchCode">
                                </asp:DropDownList>
                                <asp:ObjectDataSource ID="ODBranch" runat="server" SelectMethod="OrderbranchcodesCRP"
                                    TypeName="IMPALLibrary.Branches"></asp:ObjectDataSource>
                            </td>
                        </tr>
                        <tr>
                            <td class="label">
                                <asp:Label ID="lblFromDate" runat="server" Text="From Date" SkinID="LabelNormal"></asp:Label>
                            </td>
                            <td class="inputcontrols">
                                <asp:TextBox ID="txtFromDate" runat="server" TabIndex="3" SkinID="TextBoxCalendarExtenderNormal"></asp:TextBox>
                                <ajax:CalendarExtender ID="calFromDate" Format="dd/MM/yyyy" TargetControlID="txtFromDate"
                                    runat="server">
                                </ajax:CalendarExtender>
                            </td>
                            <td class="label">
                                <asp:Label ID="lblToDate" runat="server" Text="To Date" SkinID="LabelNormal"></asp:Label>
                            </td>
                            <td class="inputcontrols">
                                <asp:TextBox ID="txtToDate" runat="server" TabIndex="4" SkinID="TextBoxCalendarExtenderNormal"></asp:TextBox>
                                <ajax:CalendarExtender ID="calToDate" Format="dd/MM/yyyy" TargetControlID="txtToDate"
                                    runat="server">
                                </ajax:CalendarExtender>
                            </td>
                        </tr>
                    </table>
                </div>
            </asp:Panel>
            <div class="reportButtons">
                <asp:Button ID="btnReport" runat="server" Text="Generate Report" OnClientClick="javaScript:return fnValidate();"
                    TabIndex="6" SkinID="ButtonViewReport" OnClick="btnReport_Click" />
                <asp:Button ID="btnBack" SkinID="ButtonNormal" runat="server" Text="Back" OnClick="btnBack_Click" Style="display: none" />
            </div>
        </ContentTemplate>
        <Triggers>
            <asp:PostBackTrigger ControlID="btnReport" />
        </Triggers>
    </asp:UpdatePanel>
</asp:Content>
