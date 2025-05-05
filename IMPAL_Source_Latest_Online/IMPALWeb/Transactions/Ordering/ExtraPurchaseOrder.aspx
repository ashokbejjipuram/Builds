<%@ Page Title="PO Indent-CWH" Language="C#" MasterPageFile="~/Main.Master" AutoEventWireup="true"
    CodeBehind="ExtraPurchaseOrder.aspx.cs" Inherits="IMPALWeb.ExtraPurchaseOrder" %>

<%@ Register Src="~/UserControls/CrystalReportExport.ascx" TagName="CrystalReport" TagPrefix="UC" %>
<asp:Content ID="Content1" ContentPlaceHolderID="CPHDetails" runat="server">
    <style type="text/css">
        .headstyle {
            background-color: #666;
            color: #ffffff;
            padding: 5px 5px 5px 5px;
            border: none;
            font-family: Tahoma, Helvetica, sans-serif;
            font-size: 12px;
            font-weight: bold;
            text-transform: capitalize;
            text-align: center;
        }

        .FixedHeader {
            position: absolute;
            font-weight: bold;
        }
    </style>

    <script language="javascript" type="text/javascript" src="../../JavaScript/ExtraPurchaseOrder.js"></script>

    <div id="DivTop" runat="server" style="width: 100%">
        <asp:UpdatePanel ID="UpdPanelHeader" runat="server" UpdateMode="Conditional">
            <ContentTemplate>
                <asp:Panel ID="PanelHeaderDtls" runat="server">
                    <div class="subFormTitle">
                        EXTRA PURCHASE ORDER
                    </div>
                    <div class="subFormTitle">
                        INDENT DETAILS
                    </div>
                    <table class="subFormTable">
                        <tr>
                            <td class="label" style="display: none">
                                <asp:Label ID="lblIndentType" runat="server" SkinID="LabelNormal" Text="Indent Type"></asp:Label>
                                <span class="asterix">*</span>
                            </td>
                            <td class="inputcontrols" style="display: none">
                                <asp:DropDownList ID="ddlIndentType" runat="server" SkinID="DropDownListNormal" OnSelectedIndexChanged="ddlIndentType_SelectedIndexChanged"
                                    AutoPostBack="true" TabIndex="1">
                                </asp:DropDownList>
                            </td>
                            <td class="label">
                                <asp:Label ID="lblIndentNumber" runat="server" SkinID="LabelNormal" Text="PO Number"></asp:Label>
                                <span class="asterix">*</span>
                            </td>
                            <td class="inputcontrols">
                                <asp:DropDownList ID="ddlIndentNumber" runat="server" SkinID="DropDownListNormal"
                                    AutoPostBack="true" Width="150px" OnSelectedIndexChanged="ddlIndentNumber_SelectedIndexChanged">
                                </asp:DropDownList>
                            </td>
                            <td class="label">
                                <asp:Label ID="Label1" runat="server" SkinID="LabelNormal" Text="EPO Type"></asp:Label>
                                <span class="asterix">*</span>
                            </td>
                            <td class="inputcontrols">
                                <asp:DropDownList ID="ddlEPOtype" runat="server" SkinID="DropDownListNormal" AutoPostBack="true" OnSelectedIndexChanged="ddlEPOtype_SelectedIndexChanged">
                                </asp:DropDownList>
                            </td>
                            <td class="label">
                                <asp:Label ID="Label2" runat="server" SkinID="LabelNormal" Text="EPO Sub Type"></asp:Label>
                                <asp:Label ID="Label3" SkinID="LabelNormal" runat="server" Text="Customer" Style="display: none"></asp:Label>
                                <span class="asterix">*</span>
                            </td>
                            <td class="inputcontrols">
                                <asp:DropDownList ID="ddlEPOsubType" runat="server" SkinID="DropDownListNormal" Width="180px">
                                </asp:DropDownList>
                                <asp:DropDownList ID="ddlCustomer" runat="server" SkinID="DropDownListNormal" Style="display: none">
                                </asp:DropDownList>
                            </td>
                            <td class="label">
                                <asp:Label ID="lblSupplierPartNo" Text="PO Part#" SkinID="LabelNormal" runat="server"></asp:Label>
                            </td>
                            <td class="inputcontrols">
                                <asp:DropDownList ID="ddlItemCode" runat="server" SkinID="DropDownListNormal" TabIndex="2" DropDownStyle="DropDownList">
                                </asp:DropDownList>
                                <asp:Button ID="btnAddItem" ValidationGroup="btnAdd" SkinID="ButtonNormal" runat="server" CausesValidation="true" OnClick="btnAddItem_OnClick" CommandName="Add" Text="Add" TabIndex="23" OnClientClick="return ValidateAdd();" />
                            </td>
                        </tr>
                    </table>
                </asp:Panel>
                <asp:UpdatePanel ID="UpdPanelGrid" runat="server" UpdateMode="Conditional">
                    <ContentTemplate>
                        <div class="gridViewScrollFullPage">
                            <asp:GridView ID="grdPOIndent" runat="server" SkinID="GridViewScroll" AutoGenerateColumns="False"
                                AllowPaging="false" ShowFooter="true" OnRowDeleting="grdPOIndent_RowDeleting">
                                <EmptyDataTemplate>
                                    <asp:Label ID="lblEmptySearch" runat="server" SkinID="GridViewLabel">No Results Found</asp:Label>
                                </EmptyDataTemplate>
                                <Columns>
                                    <asp:TemplateField HeaderText="Part #">
                                        <ItemTemplate>
                                            <asp:TextBox ID="txtSupplierPartNo" runat="server" Width="120px" SkinID="GridViewTextBox"></asp:TextBox>
                                            <asp:DropDownList ID="ddlSupplierPartNo" runat="server" SkinID="GridViewDropDownList" AutoPostBack="true"
                                                OnSelectedIndexChanged="ddlSupplierPartNo_OnSelectedIndexChanged">
                                            </asp:DropDownList>
                                            <asp:Button ID="btnSearch" runat="server" OnClick="btnSearch_Click" Text="Search"
                                                SkinID="GridViewButton" Width="50px" />
                                        </ItemTemplate>
                                        <FooterTemplate>
                                            <asp:Button ID="btnAddDpoItem" ValidationGroup="btnAdd" SkinID="ButtonNormalBig" runat="server" CausesValidation="true" CommandName="Add"
                                                Text="Add DPO Item" TabIndex="23" Width="90px" OnClick="btnAddDpoItem_OnClick" OnClientClick="return ValidateSubmit(1);" />
                                        </FooterTemplate>
                                    </asp:TemplateField>
                                    <asp:TemplateField HeaderText="Description">
                                        <ItemTemplate>
                                            <asp:TextBox ID="txtItemDesc" runat="server" Height="15px" Width="150px" Enabled="false" SkinID="GridViewTextBoxDisabled"></asp:TextBox>
                                        </ItemTemplate>
                                    </asp:TemplateField>
                                    <asp:TemplateField HeaderText="Vehicle Appln.">
                                        <ItemTemplate>
                                            <asp:TextBox ID="txtVehicleAppln" runat="server" Height="15px" Width="150px" Enabled="false" SkinID="GridViewTextBoxDisabled"></asp:TextBox>
                                        </ItemTemplate>
                                    </asp:TemplateField>
                                    <asp:TemplateField HeaderText="Pack. Qty">
                                        <ItemTemplate>
                                            <asp:Label ID="lblPackQty" runat="server" Height="15px" Width="60px"></asp:Label>
                                        </ItemTemplate>
                                    </asp:TemplateField>
                                    <asp:TemplateField HeaderText="Stock On Hand">
                                        <ItemTemplate>
                                            <asp:Label ID="lblStockOnHand" runat="server" Height="15px" Width="60px"></asp:Label>
                                        </ItemTemplate>
                                    </asp:TemplateField>
                                    <asp:TemplateField HeaderText="Doc. On Hand">
                                        <ItemTemplate>
                                            <asp:Label ID="lblDocOnHand" runat="server" Height="15px" Width="60px"></asp:Label>
                                        </ItemTemplate>
                                    </asp:TemplateField>
                                    <asp:TemplateField HeaderText="Avg. Sales">
                                        <ItemTemplate>
                                            <asp:Label ID="lblAvgSales" runat="server" Height="15px" Width="60px"></asp:Label>
                                        </ItemTemplate>
                                    </asp:TemplateField>
                                    <asp:TemplateField HeaderText="Curr. Sales">
                                        <ItemTemplate>
                                            <asp:Label ID="lblCurMonthSales" runat="server" Height="15px" Width="60px"></asp:Label>
                                        </ItemTemplate>
                                    </asp:TemplateField>
                                    <asp:TemplateField HeaderText="Prev. Req. Qty">
                                        <ItemTemplate>
                                            <asp:Label ID="lblPreviousReqQty" runat="server" Height="15px" Width="60px"></asp:Label>
                                        </ItemTemplate>
                                    </asp:TemplateField>
                                    <asp:TemplateField HeaderText="PO. Qty">
                                        <ItemTemplate>
                                            <asp:Label ID="lblPoQty" runat="server" Height="15px" Width="60px"></asp:Label>
                                        </ItemTemplate>
                                    </asp:TemplateField>
                                    <asp:TemplateField HeaderText="Item Code" Visible="false">
                                        <ItemTemplate>
                                            <asp:Label ID="lblItemCode" runat="server" Height="15px" Width="84px"></asp:Label>
                                        </ItemTemplate>
                                    </asp:TemplateField>
                                    <asp:TemplateField HeaderText="Extra Qty">
                                        <ItemTemplate>
                                            <asp:TextBox ID="txtExtraQty" runat="server" Height="15px" Width="60px" onkeypress="return IntegerValueOnly();" onChange="return CheckQty(this.id);" onpaste="return false;" ondragstart="return false;" ondrop="return false;">0</asp:TextBox>
                                            <asp:HiddenField ID="hdnIndicator" runat="server" />
                                            <asp:HiddenField ID="hdnSurplusQty" runat="server" />
                                        </ItemTemplate>
                                    </asp:TemplateField>
                                    <asp:CommandField ShowDeleteButton="True" ButtonType="Button" />
                                </Columns>
                            </asp:GridView>
                        </div>
                    </ContentTemplate>
                </asp:UpdatePanel>
                <div class="transactionButtons">
                    <div class="transactionButtonsHolder">
                        <asp:Button ID="BtnSubmit" runat="server" Text="Process" SkinID="ButtonNormal" OnClick="BtnSubmit_Click" OnClientClick="return ValidateSubmit(2);" />
                        <asp:Button ID="btnReportPDF" runat="server" Text="PDF Report" TabIndex="4" SkinID="ButtonViewReport"
                            OnClientClick="javaScript:return fnShowHideBtns();" OnClick="btnReportPDF_Click" />
                        <asp:Button ID="btnReportExcel" runat="server" Text="Excel Report" TabIndex="4" SkinID="ButtonViewReport"
                            OnClientClick="javaScript:return fnShowHideBtns();" OnClick="btnReportExcel_Click" />
                        <asp:Button ID="btnReportRTF" runat="server" Text="Word Report" TabIndex="4" SkinID="ButtonViewReport"
                            OnClientClick="javaScript:return fnShowHideBtns();" OnClick="btnReportRTF_Click" />
                        <asp:Button ID="btnReset" runat="server" Text="Reset" SkinID="ButtonNormal" OnClick="btnReset_Click" />
                    </div>
                </div>
                <div class="reportViewerHolder" id="reportViewerHolder" runat="server">
                    <UC:CrystalReport runat="server" ID="crPurchaseOrderWorkSheet" ReportName="Purchase_worksheet" />
                </div>
            </ContentTemplate>
            <Triggers>
                <asp:AsyncPostBackTrigger ControlID="ddlIndentType" EventName="SelectedIndexChanged" />
                <asp:AsyncPostBackTrigger ControlID="BtnSubmit" EventName="Click" />
                <asp:AsyncPostBackTrigger ControlID="btnReset" EventName="Click" />
                <asp:PostBackTrigger ControlID="btnReportPDF" />
                <asp:PostBackTrigger ControlID="btnReportExcel" />
                <asp:PostBackTrigger ControlID="btnReportRTF" />
            </Triggers>
        </asp:UpdatePanel>
    </div>
</asp:Content>
