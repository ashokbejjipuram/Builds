<%@ Page Language="C#" AutoEventWireup="true" MasterPageFile="~/Main.Master" CodeBehind="Stock_DiversionBulk.aspx.cs"
    Inherits="IMPALWeb.Transactions.Inventory.Stock_DiversionBulk" %>

<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="ajaxToolkit" %>
<asp:Content ID="Content3" ContentPlaceHolderID="CPHDetails" runat="server">

    <script type="text/javascript">
        var CtrlIdPrefix = "ctl00_CPHDetails_";

		function Validate() {
            var supp = document.getElementById(CtrlIdPrefix + "ddlSupplierName");
            var fromtran = document.getElementById(CtrlIdPrefix + "ddlFromTransactionType");
            var totran = document.getElementById(CtrlIdPrefix + "ddlToTransactionType");

		    if (supp.value == "" || supp.value == "0" || supp.value == null) {
		        alert('Please select a Supplier');
		        supp.focus();
		        return false;
            }

            if (fromtran.value == "" || fromtran.value == "0" || fromtran.value == null) {
		        alert('Please select From Transaction Type');
		        fromtran.focus();
		        return false;
            }

            if (totran.value == "" || totran.value == "0" || totran.value == null) {
		        alert('Please select To Transaction Type');
		        totran.focus();
		        return false;
		    }
		}
    </script>
    <asp:UpdatePanel ID="UpdatePanel1" runat="server">
        <ContentTemplate>
            <div id="DivOuter" runat="server">
                <div>
                    <div class="subFormTitle">
                        STOCK DIVERSION - BULK
                    </div>
                    <table class="subFormTable">
                        <tr>
                            <td class="label">
                                <asp:Label ID="Label4" runat="server" SkinID="LabelNormal" Text="Supplier"></asp:Label>
                                <span class="asterix">*</span>
                            </td>
                            <td class="inputcontrols">
                                <asp:DropDownList ID="ddlSupplierName" runat="server" SkinID="DropDownListNormal"
                                    DataSourceID="ODS_Suppliers" DataTextField="SupplierName" DataValueField="SupplierCode">
                                </asp:DropDownList>
                            </td>
                        </tr>
                        <tr>
                            <td class="labelColSpan2">
                                <asp:Label ID="lblFromTransactionType" runat="server" SkinID="LabelNormal" Text="From Transaction Type"></asp:Label>
                                <span class="asterix">*</span></td>
                            <td class="inputcontrolsColSpan2">
                                <asp:DropDownList ID="ddlFromTransactionType" TabIndex="5" SkinID="DropDownListNormalBig" runat="server">
                                </asp:DropDownList>
                            </td>
                        </tr>
                        <tr>
                            <td class="labelColSpan2">
                                <asp:Label ID="lblToTransactionType" runat="server" SkinID="LabelNormal" Text="To Transaction Type"></asp:Label>
                                <span class="asterix">*</span></td>
                            <td class="inputcontrolsColSpan2">
                                <asp:DropDownList ID="ddlToTransactionType" TabIndex="6" SkinID="DropDownListNormalBig" runat="server">
                                </asp:DropDownList>
                            </td>
                        </tr>
                        <tr>
                            <td class="labelColSpan2" colspan="2">
                                <asp:Label ID="lblError" runat="server" SkinID="LabelNormal" Font-Size="18"></asp:Label>
                            </td>
                        </tr>
                    </table>
                    <div class="transactionButtons">
                        <div class="transactionButtonsHolder">
                            <asp:Button ID="BtnSubmit" runat="server" TabIndex="15" OnClick="BtnSubmit_Click" OnClientClick="return Validate();" SkinID="ButtonNormal"
                                Text="Submit" />
                            <asp:Button ID="btnReset" runat="server" TabIndex="16" Text="Reset" SkinID="ButtonNormal"
                                OnClick="btnReset_Click" />
                        </div>
                    </div>
                </div>
            </div>
            <asp:ObjectDataSource ID="ODS_Suppliers" runat="server" SelectMethod="GetAllSuppliersGST"
                TypeName="IMPALLibrary.Suppliers" DataObjectTypeName="IMPALLibrary.Suppliers"></asp:ObjectDataSource>
        </ContentTemplate>
    </asp:UpdatePanel>
</asp:Content>
