<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="CancelOrder.aspx.cs" Inherits="IMPALWeb.Transactions.Ordering.CancelOrder"
    MasterPageFile="~/Main.Master" %>

<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="ajaxToolkit" %>
<asp:Content ID="MainContent" ContentPlaceHolderID="CPHDetails" runat="server">

    <script language="javascript" type="text/javascript">

        function ListItemClick(radType) {
            var ddlSupplier = document.getElementById('<%=ddlSupplier.ClientID %>');
            if (ddlSupplier.options[0].selected == true) {
                alert('Select Supplier Line Code!');
            }
        }
        function Reset() {
            var ddlSupplier = document.getElementById('<%=ddlSupplier.ClientID %>');
            ddlSupplier.options[0].selected = true;
            ddlSupplier.disabled = false;
            Clear();
        }
        function Clear() {
            var lblPartNo = document.getElementById('<%=lblPartNo.ClientID %>');
            lblPartNo.style.display = 'none';

            var divPartNo = document.getElementById('<%=divPartNo.ClientID %>');
            divPartNo.style.display = 'none';

            var lblOrderNo = document.getElementById('<%=lblOrderNo.ClientID %>');
            lblOrderNo.style.display = 'none';
            var ddlOrderNo = document.getElementById('<%=ddlOrderNo.ClientID %>');
            ddlOrderNo.style.display = 'none';

            var radType = document.getElementById('<%=radType.ClientID %>');
            var listItems = radType.getElementsByTagName("input");
            for (var i = 0; i < listItems.length; i++) {
                listItems[i].checked = false;
            }

            var divOrderDetails = document.getElementById('<%=divOrderDetails.ClientID %>');
            divOrderDetails.style.display = 'none';
            var btnUpdate = document.getElementById('<%=btnUpdate.ClientID %>');
            btnUpdate.style.display = 'none';
            var divMessage = document.getElementById('<%=divMessage.ClientID %>');
            divMessage.style.display = 'none';
        }

        function numericFilter(txt) {
            txt.value = txt.value.replace(/[^\0-9]/ig, "");
        }

        function CheckRows() {
            var cnt = 0;
            var gridview = document.getElementById("tblGridViewScrollStyle");
            for (i = 1; i <= gridview.rows.length - 1; i++) {
                var row = gridview.rows[i];
                chkBox = row.cells[0].children[0];
                PoQty = row.cells[4].children[0];
                BalQty = row.cells[6].children[0];
                if (chkBox.checked) {
                    if (PoQty.innerHTML < BalQty.value) {
                        alert('Balance Qty should not exceed PO Qty.');
                        BalQty.value = "0";
                        BalQty.focus();
                        return false;
                    }
                    cnt++;
                }
            }

            if (cnt == 0) {
                alert('Select records to update');
                return false;
            }
        }

        function EnableAllCheckboxes(id) {
            var chkAllBox = document.getElementById(id);
            var gridview = document.getElementById("tblGridViewScrollStyle");
            
            if (chkAllBox.checked) {
                for (i = 1; i <= gridview.rows.length - 1; i++) {
                    var row = gridview.rows[i];
                    chkBox = row.cells[0].children[0];                    
                    chkBox.checked = true;
                }
            }
            else {
                for (i = 1; i <= gridview.rows.length - 1; i++) {
                    var row = gridview.rows[i];
                    chkBox = row.cells[0].children[0];
                    chkBox.checked = false;
                }
            }

            return true;
        }

        function ValidateQty(id) {
            var PoQtyid = id.replace("txtBalQty", "lblPOQty");
            var PoQty = document.getElementById(PoQtyid);
            var BalQty = document.getElementById(id);

            if (PoQty.innerHTML < BalQty.value) {
                alert('Balance Qty should not exceed PO Qty.');
                BalQty.value = "0";
                BalQty.focus();
                return false;
            }
        }

    </script>

    <div id="DivTop" runat="server">
        <asp:UpdatePanel ID="up" runat="server">
            <ContentTemplate>
                <div id="divCancelOrder" runat="server">
                    <div class="subFormTitle subFormTitleExtender250">
                        Pending Order Cancellation
                    </div>
                    <table class="subFormTable">
                        <tr>
                            <td class="label">
                                <asp:Label ID="lblSupplier" SkinID="LabelNormal" runat="server" Text="Supplier Line Code"></asp:Label>
                                <span class="asterix">*</span>
                            </td>
                            <td class="inputcontrols">
                                <asp:DropDownList ID="ddlSupplier" runat="server" TabIndex="1" SkinID="DropDownListNormal"
                                    DataSourceID="ODLine" DataTextField="SupplierName" DataValueField="SupplierCode"
                                    onchange="javaScript:return Clear();" />
                                <asp:ObjectDataSource ID="ODLine" runat="server" SelectMethod="GetAllSuppliers" TypeName="IMPALLibrary.Suppliers" />
                            </td>
                            <td class="inputcontrolsColSpan2">
                                <asp:RadioButtonList ID="radType" runat="server" RepeatDirection="Horizontal" SkinID="RadioButtonNormal"
                                    TabIndex="2" AutoPostBack="true" OnSelectedIndexChanged="radType_SelectedIndexChanged"
                                    onclick="javaScript:ListItemClick(this);">
                                    <asp:ListItem Text="PartNo#" Value="PartNo" />
                                    <asp:ListItem Text="OrderNo#" Value="OrderNo" />
                                    <asp:ListItem Text="Above 3Mth" Value="Month" />
                                </asp:RadioButtonList>
                                <%--<asp:RadioButton Text="PartNo#"  runat="server" ValidationGroup="rdio" onclick="javaScript:return ListItemClick(this);"/>
                                <asp:RadioButton Text="OrderNo#"   runat="server" ValidationGroup="rdio"/>
                                <asp:RadioButton Text="Above 3Mth#"  runat="server" ValidationGroup="rdio"/>--%>
                            </td>
                        </tr>
                        <tr>
                            <td class="label">
                                <asp:Label ID="lblPartNo" SkinID="LabelNormal" runat="server" Text="Supplier Part #"
                                    Style="display: none"></asp:Label>
                                <asp:Label ID="lblOrderNo" SkinID="LabelNormal" runat="server" Text="Purchase Order #"
                                    Style="display: none"></asp:Label>
                            </td>
                            <td class="inputcontrols">
                                <%--<asp:DropDownList ID="ddlPartNo" runat="server" TabIndex="3" SkinID="DropDownListNormal"
                                    Style="display: none" OnSelectedIndexChanged="ddlPartNo_IndexChanged" AutoPostBack="true" />--%>
                                <asp:DropDownList ID="ddlOrderNo" runat="server" TabIndex="3" SkinID="DropDownListNormal"
                                    Style="display: none" OnSelectedIndexChanged="ddlOrderNo_IndexChanged" AutoPostBack="true" />
                                <div id="divPartNo" runat="server" style="display: none">
                                    <asp:DropDownList ID="ddlPartNo" runat="server" AutoPostBack="true" DropDownStyle="DropDownList"
                                        SkinID="DropDownListNormal" TabIndex="3" OnSelectedIndexChanged="ddlPartNo_IndexChanged">
                                    </asp:DropDownList>
                                </div>
                                <%--<ajaxToolkit:ComboBox ID="ddlOrderNo" runat="server" AutoPostBack="true" DropDownStyle="DropDown"
                                            SkinID="ComboBoxNormal" AutoCompleteMode="Suggest" CaseSensitive="False" TabIndex="1"
                                            OnSelectedIndexChanged="cbCustomerName_SelectedIndexChanged">
                                        </ajaxToolkit:ComboBox>--%>
                            </td>
                        </tr>
                    </table>
                    <div id="divMessage" runat="server" style="display: none; text-align: center">
                        <asp:Label ID="lblMessage" Text="No records found." runat="server" SkinID="GridViewLabel" ForeColor="Red" Font-Bold="true" Font-Size="Large" />
                    </div>
                    <div id="divOrderDetails" runat="server" style="display: none">
                        <div class="gridViewFullPage">
                            <asp:Repeater ID="rOrderDetails" runat="server">
                                <HeaderTemplate>
                                    <table class="GridViewScrollStyle" id="tblGridViewScrollStyle">
                                        <tbody>
                                            <tr class="GridviewScrollHeader">
                                                <th>
                                                    <asp:CheckBox ID="chkSelectAll" runat="server" OnClick="return EnableAllCheckboxes(this.id);" />
                                                </th>
                                                <th>
                                                    <asp:Label ID="lblOrderNo" SkinID="LabelNormal" runat="server" Text="Order No#"></asp:Label>
                                                </th>
                                                <th>
                                                    <asp:Label ID="lblOrderDate" SkinID="LabelNormal" runat="server" Text="Order Date"></asp:Label>
                                                </th>
                                                <th>
                                                    <asp:Label ID="lblPartNo" SkinID="LabelNormal" runat="server" Text="Part No#"></asp:Label>
                                                </th>
                                                <th>
                                                    <asp:Label ID="lblPOQty" SkinID="LabelNormal" runat="server" Text="PO Quantity"></asp:Label>
                                                </th>
                                                <th>
                                                    <asp:Label ID="lblRecdQty" SkinID="LabelNormal" runat="server" Text="Recd. Qty"></asp:Label>
                                                </th>
                                                <th>
                                                    <asp:Label ID="lblBalQty" SkinID="LabelNormal" runat="server" Text="Bal Qty"></asp:Label>
                                                </th>
                                            </tr>
                                        </tbody>
                                </HeaderTemplate>
                                <ItemTemplate>
                                    <tr class="GridviewScrollRow">
                                        <td>
                                            <asp:CheckBox ID="chkSelect" runat="server" /><asp:HiddenField ID="hidSerialNum"
                                                runat="server" Value='<%# DataBinder.Eval(Container.DataItem, "SerialNum")%>' />
                                        </td>
                                        <td>
                                            <asp:Label ID="lblOrderNo" SkinID="LabelNormal" runat="server" Text='<%# DataBinder.Eval(Container.DataItem, "OrderNum")%>' />
                                        </td>
                                        <td>
                                            <asp:Label ID="lblOrderDate" SkinID="LabelNormal" runat="server" Text='<%# DataBinder.Eval(Container.DataItem, "OrderDate")%>' />
                                        </td>
                                        <td>
                                            <asp:Label ID="lblPartNum" SkinID="LabelNormal" runat="server" Text='<%# DataBinder.Eval(Container.DataItem, "PartNum")%>' />
                                        </td>
                                        <td>
                                            <asp:Label ID="lblPOQty" SkinID="LabelNormal" runat="server" Text='<%# DataBinder.Eval(Container.DataItem, "OrderQty")%>' />
                                        </td>
                                        <td>
                                            <asp:Label ID="lblRecdQty" SkinID="LabelNormal" runat="server" Text='<%# DataBinder.Eval(Container.DataItem, "ReceivedQty")%>' />
                                        </td>
                                        <td>
                                            <asp:TextBox ID="txtBalQty" SkinID="TextBoxNormalSmall" runat="server" Text='<%# DataBinder.Eval(Container.DataItem, "BalanceQty")%>'
                                                onKeyUp="numericFilter(this);" onchange="return ValidateQty(this.id)" />
                                        </td>
                                    </tr>
                                </ItemTemplate>
                                <FooterTemplate>
                                    </table>
                                </FooterTemplate>
                            </asp:Repeater>
                        </div>
                    </div>
                    <div class="transactionButtons">
                        <div class="transactionButtonsHolder">
                            <asp:Button ID="btnUpdate" runat="server" Text="Update" TabIndex="6" SkinID="ButtonNormal"
                                Style="display: none" OnClick="btnUpdate_OnClick" OnClientClick="javaScript:return CheckRows();" />
                            <asp:Button ID="btnReset" runat="server" Text="Reset" OnClientClick="javaScript:return Reset();"
                                TabIndex="7" SkinID="ButtonNormal" OnClick="btnReset_OnClick" />
                        </div>
                    </div>
                </div>
            </ContentTemplate>
        </asp:UpdatePanel>
    </div>
</asp:Content>
