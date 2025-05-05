<%@ Page Language="C#" AutoEventWireup="true" MasterPageFile="~/Main.Master" CodeBehind="ConsignmentBalance.aspx.cs"
    Inherits="IMPALWeb.Transactions.Inventory.Query.ConsignmentBalance" %>

<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="ajaxToolkit" %>
<asp:Content ID="Content3" ContentPlaceHolderID="CPHDetails" runat="server">

    <script type="text/javascript">
        function pageLoad(sender, args) {
            //gridViewFixedHeader(gridViewID, gridViewWidth, gridViewHeight)
            gridViewFixedHeader('<%=grdConsignmentBalance.ClientID%>', '850', '400');
        }

        function AlphaNumericWithDashPercentile() {
            var AsciiValue = event.keyCode
            if ((AsciiValue >= 48 && AsciiValue <= 57) || (AsciiValue == 8 || AsciiValue == 127 || AsciiValue == 37 || AsciiValue == 45) || (AsciiValue >= 65 && AsciiValue <= 90) || (AsciiValue >= 97 && AsciiValue <= 122))
                event.returnValue = true;
            else
                event.returnValue = false;
        }
    </script>

    <asp:UpdatePanel ID="UpdatePanel1" runat="server">
        <ContentTemplate>
            <div class="subFormTitle">
                Balance Details
            </div>
            <table class="subFormTable">
                <tr>
                    <td class="label">
                        <asp:Label ID="lblSupplierPart" runat="server" Text="Supplier Part #" SkinID="LabelNormal"></asp:Label>
                        <span class="asterix">*</span>
                    </td>
                    <td class="inputcontrols">
                        <asp:TextBox ID="txtSupplierPartNo" SkinID="TextBoxNormal" runat="server" onkeypress="return AlphaNumericWithDashPercentile();"></asp:TextBox>
                        <asp:RequiredFieldValidator ID="RequiredFieldValidator1" runat="server" ForeColor="Red"
                            ControlToValidate="txtSupplierPartNo" SetFocusOnError="true" ErrorMessage="Supplier Part Number is required"
                            Text="."></asp:RequiredFieldValidator>
                        <ajaxToolkit:ValidatorCalloutExtender ID="ValCalloutExtender1" TargetControlID="RequiredFieldValidator1"
                            PopupPosition="BottomLeft" runat="server">
                        </ajaxToolkit:ValidatorCalloutExtender>
                    </td>
                    <td class="label">
                        <asp:Button ID="BtnSubmit" SkinID="ButtonNormal" runat="server" OnClick="BtnSubmit_Click"
                            Text="Go" />
                    </td>
                    <td class="inputcontrols">&nbsp;
                    </td>
                    <td class="inputcontrols">&nbsp;
                    </td>
                </tr>
                <tr>
                    <td class="labelColSpan2" colspan="3">
                        <asp:Label ID="lblNoRecord" SkinID="Error" ForeColor="Red" runat="server"></asp:Label>
                    </td>
                </tr>
            </table>
        </ContentTemplate>
        <Triggers>
            <asp:AsyncPostBackTrigger ControlID="btnReset" EventName="Click" />
        </Triggers>
    </asp:UpdatePanel>
    <div id="divItemDetails" runat="server">
        <asp:UpdatePanel ID="UpdPanelGrid" runat="server" UpdateMode="Conditional">
            <ContentTemplate>
                <div class="subFormTitle" id="divheading">
                    ITEM DETAILS
                </div>
                <div class="gridViewScrollFullPage">
                    <asp:GridView ID="grdConsignmentBalance" runat="server" PageSize="50" SkinID="GridViewScroll"
                        AutoGenerateColumns="False" AllowPaging="True" OnPageIndexChanging="grdConsignmentBalance_PageIndexChanging" ShowFooter="false">
                        <Columns>
                            <asp:TemplateField HeaderText="Part #">
                                <ItemTemplate>
                                    <asp:TextBox ID="txtPart_Number" ReadOnly="true" SkinID="GridViewTextBox" Text='<%# Bind("Supplier_Part_Number") %>'
                                        runat="server" />
                                </ItemTemplate>
                            </asp:TemplateField>
                            <asp:TemplateField HeaderText="Line">
                                <ItemTemplate>
                                    <asp:TextBox ID="txtSupplierName" ReadOnly="true" SkinID="GridViewTextBoxSmall" Text='<%# Bind("Supplier_Name") %>'
                                        runat="server" />
                                </ItemTemplate>
                            </asp:TemplateField>
                            <asp:TemplateField HeaderText="Description">
                                <ItemTemplate>
                                    <asp:TextBox ID="txtDescription" ReadOnly="true" SkinID="GridViewTextBoxBig" Text='<%# Bind("Item_short_Description") %>'
                                        runat="server" />
                                </ItemTemplate>
                            </asp:TemplateField>
                            <asp:TemplateField HeaderText="Bal. Qty">
                                <ItemTemplate>
                                    <asp:TextBox ID="txtBal_Qty" ReadOnly="true" Style="text-align: right" SkinID="GridViewTextBoxSmall"
                                        Text='<%# Bind("Balance_Quantity") %>' runat="server" />
                                </ItemTemplate>
                            </asp:TemplateField>
                            <asp:TemplateField HeaderText="SLB">
                                <ItemTemplate>
                                    <asp:TextBox ID="txtSLB" Style="text-align: right" ReadOnly="true" SkinID="GridViewTextBoxSmall"
                                        Text='<%# string.Format("{0:#.00}",System.Math.Round(Convert.ToDouble(DataBinder.Eval(Container.DataItem,"SLB")),2)) %>'
                                        runat="server" />
                                </ItemTemplate>
                            </asp:TemplateField>
                            <asp:TemplateField HeaderText="List Price">
                                <ItemTemplate>
                                    <asp:TextBox ID="txtList_Price" Style="text-align: right" ReadOnly="true" SkinID="GridViewTextBoxSmall"
                                        Text='<%# System.Math.Round(Convert.ToDouble(DataBinder.Eval(Container.DataItem,"List_Price")),2) %>'
                                        runat="server" />
                                </ItemTemplate>
                            </asp:TemplateField>
                            <asp:TemplateField HeaderText="MRP">
                                <ItemTemplate>
                                    <asp:TextBox ID="txtMRP" Style="text-align: right" ReadOnly="true" SkinID="GridViewTextBoxSmall"
                                        Text='<%# System.Math.Round(Convert.ToDouble(DataBinder.Eval(Container.DataItem,"MRP")),2) %>'
                                        runat="server" />
                                </ItemTemplate>
                            </asp:TemplateField>
                            <asp:TemplateField HeaderText="Tax %">
                                <ItemTemplate>
                                    <asp:TextBox ID="txtTax" Style="text-align: right" ReadOnly="true" SkinID="GridViewTextBoxSmall"
                                        Text='<%# System.Math.Round(Convert.ToDouble(DataBinder.Eval(Container.DataItem,"Tax")),2) %>'
                                        runat="server" />
                                </ItemTemplate>
                            </asp:TemplateField>
                            <asp:TemplateField HeaderText="Segment">
                                <ItemTemplate>
                                    <asp:TextBox ID="txtSegment" Style="text-align: left" ReadOnly="true" SkinID="GridViewTextBoxSmall"
                                        Text='<%# Bind("Application_Segment") %>'
                                        runat="server" />
                                </ItemTemplate>
                            </asp:TemplateField>
                            <asp:TemplateField HeaderText="Vehicle Type">
                                <ItemTemplate>
                                    <asp:TextBox ID="txtVehicleType" Style="text-align: left" ReadOnly="true" SkinID="GridViewTextBox"
                                        Text='<%# Bind("Vehicle_Type") %>'
                                        runat="server" />
                                </ItemTemplate>
                            </asp:TemplateField>
                            <asp:TemplateField HeaderText="Coupon">
                                <ItemTemplate>
                                    <asp:TextBox ID="txtCoupon" Style="text-align: right" ReadOnly="true" SkinID="GridViewTextBoxSmall"
                                        Text='<%# System.Math.Round(Convert.ToDouble(DataBinder.Eval(Container.DataItem,"Coupon")),2) %>' runat="server" />
                                </ItemTemplate>
                            </asp:TemplateField>
                            <asp:TemplateField HeaderText="Po Qty">
                                <ItemTemplate>
                                    <asp:TextBox ID="txtPo_Qty" Style="text-align: right" ReadOnly="true" SkinID="GridViewTextBoxSmall"
                                        Text='<%# Bind("PO_Quantity") %>' runat="server" />
                                </ItemTemplate>
                            </asp:TemplateField>
                            <asp:TemplateField HeaderText="Doc. On<br />Hand">
                                <ItemTemplate>
                                    <asp:TextBox ID="txtDoc_On_Hand" Style="text-align: right" ReadOnly="true" SkinID="GridViewTextBoxSmall"
                                        Text='<%# Bind("Document_Quantity") %>' runat="server" />
                                </ItemTemplate>
                            </asp:TemplateField>
                            <asp:TemplateField HeaderText="Location">
                                <ItemTemplate>
                                    <asp:TextBox ID="txtLocation" Style="text-align: left" ReadOnly="true" Width="100px" SkinID="GridViewTextBox"
                                        Text='<%# Bind("Location") %>' runat="server" />
                                </ItemTemplate>
                            </asp:TemplateField>
                            <asp:TemplateField HeaderText="OE Part #">
                                <ItemTemplate>
                                    <asp:TextBox ID="txtOEreference" Style="text-align: left" ReadOnly="true" Width="100px" SkinID="GridViewTextBox"
                                        Text='<%# Bind("OE_Reference") %>' runat="server" />
                                </ItemTemplate>
                            </asp:TemplateField>
                        </Columns>
                    </asp:GridView>
                    <br />
                    <div class="subFormTitle" id="divheading1" runat="server">
                        ALTERNATE ITEM DETAILS
                    </div>
                    <asp:GridView ID="grdConsignmentBalanceAlt" runat="server" PageSize="50" SkinID="GridViewScroll"
                        AutoGenerateColumns="False" AllowPaging="False" ShowFooter="false">
                        <Columns>
                            <asp:TemplateField HeaderText="Part #">
                                <ItemTemplate>
                                    <asp:TextBox ID="txtPart_Number" ReadOnly="true" SkinID="GridViewTextBox" Text='<%# Bind("Supplier_Part_Number") %>'
                                        runat="server" />
                                </ItemTemplate>
                            </asp:TemplateField>
                            <asp:TemplateField HeaderText="Line">
                                <ItemTemplate>
                                    <asp:TextBox ID="txtSupplierName" ReadOnly="true" SkinID="GridViewTextBoxSmall" Text='<%# Bind("Supplier_Name") %>'
                                        runat="server" />
                                </ItemTemplate>
                            </asp:TemplateField>
                            <asp:TemplateField HeaderText="Description">
                                <ItemTemplate>
                                    <asp:TextBox ID="txtDescription" ReadOnly="true" SkinID="GridViewTextBoxBig" Text='<%# Bind("Item_short_Description") %>'
                                        runat="server" />
                                </ItemTemplate>
                            </asp:TemplateField>
                            <asp:TemplateField HeaderText="Bal. Qty">
                                <ItemTemplate>
                                    <asp:TextBox ID="txtBal_Qty" ReadOnly="true" Style="text-align: right" SkinID="GridViewTextBoxSmall"
                                        Text='<%# Bind("Balance_Quantity") %>' runat="server" />
                                </ItemTemplate>
                            </asp:TemplateField>
                            <asp:TemplateField HeaderText="SLB">
                                <ItemTemplate>
                                    <asp:TextBox ID="txtSLB" Style="text-align: right" ReadOnly="true" SkinID="GridViewTextBoxSmall"
                                        Text='<%# string.Format("{0:#.00}",System.Math.Round(Convert.ToDouble(DataBinder.Eval(Container.DataItem,"SLB")),2)) %>'
                                        runat="server" />
                                </ItemTemplate>
                            </asp:TemplateField>
                            <asp:TemplateField HeaderText="List Price">
                                <ItemTemplate>
                                    <asp:TextBox ID="txtList_Price" Style="text-align: right" ReadOnly="true" SkinID="GridViewTextBoxSmall"
                                        Text='<%# System.Math.Round(Convert.ToDouble(DataBinder.Eval(Container.DataItem,"List_Price")),2) %>'
                                        runat="server" />
                                </ItemTemplate>
                            </asp:TemplateField>
                            <asp:TemplateField HeaderText="MRP">
                                <ItemTemplate>
                                    <asp:TextBox ID="txtMRP" Style="text-align: right" ReadOnly="true" SkinID="GridViewTextBoxSmall"
                                        Text='<%# System.Math.Round(Convert.ToDouble(DataBinder.Eval(Container.DataItem,"MRP")),2) %>'
                                        runat="server" />
                                </ItemTemplate>
                            </asp:TemplateField>
                            <asp:TemplateField HeaderText="Tax %">
                                <ItemTemplate>
                                    <asp:TextBox ID="txtTax" Style="text-align: right" ReadOnly="true" SkinID="GridViewTextBoxSmall"
                                        Text='<%# System.Math.Round(Convert.ToDouble(DataBinder.Eval(Container.DataItem,"Tax")),2) %>'
                                        runat="server" />
                                </ItemTemplate>
                            </asp:TemplateField>
                            <asp:TemplateField HeaderText="Segment">
                                <ItemTemplate>
                                    <asp:TextBox ID="txtSegment" Style="text-align: left" ReadOnly="true" SkinID="GridViewTextBoxSmall"
                                        Text='<%# Bind("Application_Segment") %>'
                                        runat="server" />
                                </ItemTemplate>
                            </asp:TemplateField>
                            <asp:TemplateField HeaderText="Vehicle Type">
                                <ItemTemplate>
                                    <asp:TextBox ID="txtVehicleType" Style="text-align: left" ReadOnly="true" SkinID="GridViewTextBox"
                                        Text='<%# Bind("Vehicle_Type") %>'
                                        runat="server" />
                                </ItemTemplate>
                            </asp:TemplateField>
                            <asp:TemplateField HeaderText="Coupon">
                                <ItemTemplate>
                                    <asp:TextBox ID="txtCoupon" Style="text-align: right" ReadOnly="true" SkinID="GridViewTextBoxSmall"
                                        Text='<%# System.Math.Round(Convert.ToDouble(DataBinder.Eval(Container.DataItem,"Coupon")),2) %>' runat="server" />
                                </ItemTemplate>
                            </asp:TemplateField>
                            <asp:TemplateField HeaderText="Po Qty">
                                <ItemTemplate>
                                    <asp:TextBox ID="txtPo_Qty" Style="text-align: right" ReadOnly="true" SkinID="GridViewTextBoxSmall"
                                        Text='<%# Bind("PO_Quantity") %>' runat="server" />
                                </ItemTemplate>
                            </asp:TemplateField>
                            <asp:TemplateField HeaderText="Doc. On<br />Hand">
                                <ItemTemplate>
                                    <asp:TextBox ID="txtDoc_On_Hand" Style="text-align: right" ReadOnly="true" SkinID="GridViewTextBoxSmall"
                                        Text='<%# Bind("Document_Quantity") %>' runat="server" />
                                </ItemTemplate>
                            </asp:TemplateField>
                            <asp:TemplateField HeaderText="Location">
                                <ItemTemplate>
                                    <asp:TextBox ID="txtLocation" Style="text-align: left" ReadOnly="true" Width="100px" SkinID="GridViewTextBox"
                                        Text='<%# Bind("Location") %>' runat="server" />
                                </ItemTemplate>
                            </asp:TemplateField>
                            <asp:TemplateField HeaderText="Reference">
                                <ItemTemplate>
                                    <asp:TextBox ID="txtReference" Style="text-align: left; color: red; font-size: 16px; font-weight: bold" ReadOnly="true" Width="100px" SkinID="GridViewTextBox"
                                        Text='<%# Bind("SuperceededStatus") %>' runat="server" />
                                </ItemTemplate>
                            </asp:TemplateField>
                            <asp:TemplateField HeaderText="Alternate Part #">
                                <ItemTemplate>
                                    <asp:TextBox ID="txtAltPartNo" Style="text-align: left; color: red; font-size: 16px; font-weight: bold" ReadOnly="true"
                                        Width="100px" SkinID="GridViewTextBox" Text='<%# Bind("AltPartNo") %>' runat="server" />
                                </ItemTemplate>
                            </asp:TemplateField>
                            <asp:TemplateField HeaderText="OE Part #">
                                <ItemTemplate>
                                    <asp:TextBox ID="txtOEreference" Style="text-align: left" ReadOnly="true" Width="100px" SkinID="GridViewTextBox"
                                        Text='<%# Bind("OE_Reference") %>' runat="server" />
                                </ItemTemplate>
                            </asp:TemplateField>
                        </Columns>
                    </asp:GridView>
                </div>
                <div class="transactionButtons">
                    <div class="transactionButtonsHolder">
                        <asp:Button ID="btnReset" runat="server" CausesValidation="false" SkinID="ButtonNormalBig"
                            OnClick="btnReset_Click" Text="Reset" />
                    </div>
                </div>
            </ContentTemplate>
            <Triggers>
                <asp:AsyncPostBackTrigger ControlID="BtnSubmit" EventName="Click" />
            </Triggers>
        </asp:UpdatePanel>
    </div>
</asp:Content>
