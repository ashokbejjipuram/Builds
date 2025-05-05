<%@ Page Language="C#" MasterPageFile="~/Main.Master" AutoEventWireup="true" CodeBehind="BackOrder.aspx.cs"
    Inherits="IMPALWeb.Reports.Ordering.Listing.BackOrder" %>

<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="ajaxToolkit" %>
<asp:Content ID="Content1" ContentPlaceHolderID="CPHDetails" runat="server">

    <script language="javascript" type="text/javascript">
        function Validate() {
            var txtFromDate = document.getElementById('<%=txtFromDate.ClientID%>');
            var txtToDate = document.getElementById('<%=txtToDate.ClientID%>');
            return ValidateDate(txtFromDate, txtToDate, null, null);
        }
    </script>

    <div class="reportFormTitle">
        Back Order</div>
    <div class="reportFilters">
        <table id="reportFiltersTable" class="reportFiltersTable" runat="server">
            <tr>
                <td class="label">
                    <asp:Label ID="lblFromDate" Text="From Date" runat="server" SkinID="LabelNormal"></asp:Label>
                    <span class="asterix">*</span>
                </td>
                <td class="inputcontrols">
                    <asp:TextBox ID="txtFromDate" runat="server" SkinID="TextBoxCalendarExtenderNormal"
                        TabIndex="8"></asp:TextBox>
                    <ajaxToolkit:CalendarExtender ID="calFromdate" Format="dd/MM/yyyy" runat="server"
                        TargetControlID="txtFromDate">
                    </ajaxToolkit:CalendarExtender>
                    <asp:HiddenField ID="hidFromDate" runat="server" />
                </td>
                <td class="label">
                    <asp:Label ID="lblToDate" Text="To Date" runat="server" SkinID="LabelNormal"></asp:Label>
                    <span class="asterix">*</span>
                </td>
                <td class="inputcontrols">
                    <asp:TextBox ID="txtToDate" runat="server" SkinID="TextBoxCalendarExtenderNormal"
                        TabIndex="9"></asp:TextBox>
                    <ajaxToolkit:CalendarExtender ID="calTodate" Format="dd/MM/yyyy" runat="server" TargetControlID="txtToDate">
                    </ajaxToolkit:CalendarExtender>
                    <asp:HiddenField ID="hidToDate" runat="server" />
                </td>
                <td class="label">
                    <asp:Label ID="lblCustomer" runat="server" Text="Customer" SkinID="LabelNormal"></asp:Label>
                </td>
                <td class="inputcontrols">
                    <asp:DropDownList ID="ddlCustomerName" runat="server" AutoPostBack="true" DropDownStyle="DropDownList"
                        DataTextField="CustomerName" DataValueField="CustomerCode" SkinID="DropDownListNormal" TabIndex="2"
                        OnSelectedIndexChanged="ddlCustomerName_SelectedIndexChanged">
                    </asp:DropDownList>
                </td>
            </tr>
            <tr>
                <td class="label">
                    <asp:Label ID="Label1" SkinID="LabelNormal" Text="Supplier" runat="server"></asp:Label>
                </td>
                <td class="inputcontrols">
                    <asp:DropDownList ID="ddlSupplier" runat="server" TabIndex="3" SkinID="DropDownListNormal"
                        DataSourceID="ODLine" DataTextField="SupplierName" DataValueField="SupplierCode" />
                    <asp:ObjectDataSource ID="ODLine" runat="server" SelectMethod="GetAllSuppliers" TypeName="IMPALLibrary.Suppliers" />
                </td>
                <td class="label">
                    <asp:Label ID="lblReportType" runat="server" SkinID="LabelNormal" Text="Report Type"></asp:Label>
                </td>
                <td class="inputcontrols">
                    <asp:DropDownList ID="ddlReportType" runat="server" SkinID="DropDownListNormal" AutoPostBack="true"
                        TabIndex="3" OnSelectedIndexChanged="ddlReportType_SelectIndexChanged">
                    </asp:DropDownList>
                </td>
                <td class="label">
                    <asp:Label ID="lblSubReportType" runat="server" SkinID="LabelNormal" Text="Sub Report"></asp:Label>
                </td>
                <td class="inputcontrols">
                    <asp:DropDownList ID="ddlSubReportType" runat="server" SkinID="DropDownListNormal"
                        TabIndex="3">
                    </asp:DropDownList>
                </td>
            </tr>
        </table>
        <div id="divCustomerInfo" style="display: none" runat="server">
            <div class="reportFormTitle">
                Customer Information</div>
            <table class="reportFiltersTable">
                <tr>
                    <td class="label">
                        <asp:Label runat="server" ID="lblCustomerCode" Text="Customer Code" SkinID="LabelNormal" />
                    </td>
                    <td class="inputcontrols">
                        <asp:TextBox ID="txtCustomerCode" runat="server" SkinID="TextBoxCalendarExtenderNormal"
                            ReadOnly="true" />
                    </td>
                    <td class="label">
                        <asp:Label Text="Address1" SkinID="LabelNormal" runat="server" ID="lblAddress1" />
                    </td>
                    <td class="inputcontrols">
                        <asp:TextBox ID="txtAddress1" runat="server" SkinID="TextBoxCalendarExtenderNormal"
                            ReadOnly="true" />
                    </td>
                    <td class="label">
                        <asp:Label runat="server" ID="lblAddress2" Text="Address2" SkinID="LabelNormal" />
                    </td>
                    <td class="inputcontrols">
                        <asp:TextBox ID="txtAddress2" runat="server" SkinID="TextBoxCalendarExtenderNormal"
                            ReadOnly="true" />
                    </td>
                </tr>
                <tr>
                    <td class="label">
                        <asp:Label runat="server" ID="lblAddress3" Text="Address3" SkinID="LabelNormal" />
                    </td>
                    <td class="inputcontrols">
                        <asp:TextBox ID="txtAddress3" runat="server" SkinID="TextBoxCalendarExtenderNormal"
                            ReadOnly="true" />
                    </td>
                    <td class="label">
                        <asp:Label Text="Address4" SkinID="LabelNormal" runat="server" ID="lblAddress4" />
                    </td>
                    <td class="inputcontrols">
                        <asp:TextBox ID="txtAddress4" runat="server" SkinID="TextBoxCalendarExtenderNormal"
                            ReadOnly="true" />
                    </td>
                    <td class="label">
                        <asp:Label runat="server" ID="lblLocation" Text="Location" SkinID="LabelNormal" />
                    </td>
                    <td class="inputcontrols">
                        <asp:TextBox ID="txtLocation" runat="server" SkinID="TextBoxCalendarExtenderNormal"
                            ReadOnly="true" />
                    </td>
                </tr>
            </table>
        </div>
        <div>
            <asp:Label ID="lblMessage" runat="server" SkinID="GridViewLabel" ForeColor="Red"
                Font-Bold="true" Font-Size="Medium"></asp:Label>
        </div>
        <div class="reportButtons">
            <asp:Button ID="btnReport" runat="server" Text="Generate Report" SkinID="ButtonViewReport"
                TabIndex="5" OnClick="btnReport_Click" OnClientClick="return Validate();" />
        </div>
    </div>
</asp:Content>
