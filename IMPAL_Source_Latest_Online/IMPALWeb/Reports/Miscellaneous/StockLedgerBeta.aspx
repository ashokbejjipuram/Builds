<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="StockLedgerBeta.aspx.cs" Inherits="IMPALWeb.Reports.Miscellaneous.StockLedgerBeta"
    MasterPageFile="~/Main.Master" %>

<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="ajaxToolkit" %>
<asp:Content ID="Content" ContentPlaceHolderID="CPHDetails" runat="server">

    <script language="javascript" type="text/javascript">
        function Validate() {
            var txtSupplier = document.getElementById('<%=ddlSupplier.ClientID%>');

            if (txtSupplier.value.trim() == "0" || txtSupplier.value.trim() == "") {
                alert("Please Select a Supplier.");
                txtSupplier.focus();
                return false;
            }
            
            var txtDate = document.getElementById('<%=txtDate.ClientID%>');
            
            var oSysDate = new Date();
            var oDateVal = txtDate.value.trim();
            var oDate = oDateVal.split("/");
            var oDateFormatted = oDate[1] + "/" + oDate[0] + "/" + oDate[2];
            
            if (oDateVal == null || oDateVal == "") {
                alert("Date should not be null!");
                txtDate.focus();
                return false;
            }
            else if (oDateVal != null && fnIsDate(txtDate.value) == false) {
                txtDate.value = "";
                txtDate.focus();
                return false;
            }
            else if (oSysDate < new Date(oDateFormatted)) {
                alert("Date should not be greater than System Date");
                txtDate.value = "";
                txtDate.focus();
                return false;
            }
        }
    </script>

    <div class="reportFormTitle">
        Stock Ledger Report
    </div>
    <div class="reportFilters">
        <table id="reportFiltersTable" class="reportFiltersTable" runat="server">
            <tr>
                <td class="label">
                    <asp:Label ID="lblSupplierName" Text="Supplier Name" runat="server" SkinID="LabelNormal"></asp:Label>
                    <span class="asterix">*</span>
                </td>
                <td class="inputcontrols">
                    <asp:DropDownList ID="ddlSupplier" runat="server" TabIndex="3" SkinID="DropDownListNormal"
                        DataSourceID="ODLine" DataTextField="SupplierName" DataValueField="SupplierCode" />
                </td>
                <td class="label">
                    <asp:Label ID="lblSupplierPartNo" SkinID="LabelNormal" runat="server" Text="Supplier Part Number"></asp:Label>
                </td>
                <td class="inputcontrols">
                    <asp:TextBox ID="txtSupplierPartNo" runat="server" SkinID="TextBoxNormal"></asp:TextBox>
                    <asp:DropDownList ID="ddlSupplierPartNo" runat="server" Visible="false" SkinID="DropDownListNormal">
                    </asp:DropDownList>                    
                    <asp:Button ID="btnSearch" runat="server" OnClick="btnSearch_Click" Text="Search" SkinID="ButtonNormal" />
                </td>
                <td class="label">
                    <%--<asp:Label ID="lblMonthYear" runat="server" Text="Month & Year" SkinID="LabelNormal"></asp:Label>--%>
                    <asp:Label ID="lblDate" runat="server" Text="Date" SkinID="LabelNormal"></asp:Label>
                    <span class="asterix">*</span>
                </td>
                <td class="inputcontrols">
                    <%--<asp:DropDownList ID="ddlMonthYear" runat="server" TabIndex="4" SkinID="DropDownListNormal">
                    </asp:DropDownList>--%>
                    <asp:TextBox ID="txtDate" runat="server" Width="80" SkinID="TextBoxNormal" TabIndex="2"></asp:TextBox>
                    <ajaxToolkit:CalendarExtender ID="calTodate" Format="dd/MM/yyyy" runat="server" TargetControlID="txtDate">
                    </ajaxToolkit:CalendarExtender>
                </td>
            </tr>
        </table>
        <asp:ObjectDataSource ID="ODLine" runat="server" SelectMethod="GetAllSuppliersGST"
            TypeName="IMPALLibrary.Suppliers" DataObjectTypeName="IMPALLibrary.Suppliers">
        </asp:ObjectDataSource>
        <div class="reportButtons">
            <asp:Button ID="btnReport" runat="server" OnClick="btnReport_Click" OnClientClick="javaScript:return Validate();"
                Text="Generate Report" SkinID="ButtonViewReport" TabIndex="3" />
        </div>
    </div>
    <div class="reportViewerHolder" id="reportViewerHolder" runat="server">
    </div>
</asp:Content>
