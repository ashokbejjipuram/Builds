<%@ Page Language="C#" MasterPageFile="~/Main.Master" AutoEventWireup="true" CodeBehind="SupplierInvoicesUpload.aspx.cs"
    Inherits="IMPALWeb.HOAdmin.Item.SupplierInvoicesUpload" %>

<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="ajaxToolkit" %>
<asp:Content ID="Content1" ContentPlaceHolderID="CPHDetails" runat="server">
    <script language="javascript" type="text/javascript">
        var CtrlIdPrefix = "ctl00_CPHDetails_";

        function Validate() {
            var ddlSupplierLine = document.getElementById(CtrlIdPrefix + "ddlSupplierLine");
            var btnUpload = document.getElementById(CtrlIdPrefix + "btnFileUpload");

            if (ddlSupplierLine.value.trim() == "0" || ddlSupplierLine.value.trim() == "") {
                alert('Please select a Supplier Line.');
                ddlSupplierLine.focus();
                return false;
            }

            if (btnUpload.value == "" || btnUpload.value == null) {
                alert("Please Select a File");
                btnUpload.focus();
                return false;
            }

            if (btnUpload.value.slice(btnUpload.value.length - 4) != ".xls") {
                alert("Excel file should be 97-2003 (.xls) WorkBook");
                return false;
            }
        }
    </script>
    <div class="reportFormTitle">
        Supplier Invoices - Upload
    </div>
    <div class="reportFilters">
        <table id="reportFiltersTable" class="reportFiltersTable" runat="server">
            <tr id="tbluploadFile">
                <td class="label">
                    <asp:Label SkinID="LabelNormal" ID="lblFrmline" runat="server" Text="Supplier Line"></asp:Label>
                </td>
                <td class="inputcontrols">
                    <asp:DropDownList SkinID="DropDownListNormal" ID="ddlSupplierLine" runat="server" TabIndex="1"
                        DataSourceID="ODSuppliers" DataTextField="SupplierName" DataValueField="SupplierCode">
                    </asp:DropDownList>
                </td>
                <td class="label">
                    <asp:Label ID="lblDate" runat="server" Text="Select File" SkinID="LabelNormal"></asp:Label>
                    <span class="asterix">*</span>
                </td>
                <td class="inputcontrols">
                    <asp:FileUpload runat="server" ID="btnFileUpload" />
                </td>
                <td>
                    <asp:Button ID="btnUploadExcel" runat="server" Text="Upload File" SkinID="ButtonNormal"
                        OnClick="btnUploadExcel_Click" OnClientClick="javascript: return Validate();" />
                    <asp:Button ID="btnReset" runat="server" CausesValidation="false" SkinID="ButtonNormal"
                        OnClick="btnReset_Click" Text="Reset" />
                </td>
            </tr>
            <tr>
                <td class="label" colspan="4">
                    <asp:Label ID="lblUploadMessage" runat="server" Text="" SkinID="LabelNormal" Visible="false"></asp:Label>
                </td>
            </tr>
        </table>
    </div>
    <asp:ObjectDataSource ID="ODSuppliers" runat="server" SelectMethod="GetAllSuppliersAutoGRN"
        TypeName="IMPALLibrary.Suppliers"></asp:ObjectDataSource>
</asp:Content>
