<%@ Page Language="C#" MasterPageFile="~/Main.Master" AutoEventWireup="true" CodeBehind="DirectPurchaseOrderReprintHO.aspx.cs"
    Inherits="IMPALWeb.Reports.Ordering.Listing.DirectPurchaseOrderReprintHO" %>

<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="ajaxToolkit" %>
<asp:Content ID="Content3" ContentPlaceHolderID="CPHDetails" runat="server">
    <div id="DivTop" runat="server">
        <asp:UpdatePanel ID="UpdatePanel1" runat="server" UpdateMode="Conditional">
            <ContentTemplate>
                <div id="DivOuter" runat="server">
                    <div>
                        <div class="subFormTitle subFormTitleExtender450">
                            Purchase Order Reprint - HO
                        </div>
                        <table class="subFormTable">
                            <tr>
                                <td class="label">
                                    <asp:Label ID="lblOrdBranch" runat="server" SkinID="LabelNormal" Text="Branch"></asp:Label>
                                </td>
                                <td class="inputcontrols">
                                    <asp:DropDownList ID="ddlOrdBranchName" runat="server" AutoPostBack="true" SkinID="DropDownListNormal"
                                        OnSelectedIndexChanged="ddlOrdBranchName_SelectedIndexChanged">
                                    </asp:DropDownList>
                                </td>
                                <td class="label">
                                    <asp:Label ID="lblHeaderMessage" runat="server" Text="PO Number" SkinID="LabelNormal"></asp:Label>
                                </td>
                                <td class="inputcontrols">
                                    <asp:TextBox ID="txtOrd_PONumber" SkinID="TextBoxDisabled" ReadOnly="true" runat="server"
                                        TabIndex="1"></asp:TextBox>
                                    <asp:DropDownList ID="ddlOrd_PONumber" SkinID="DropDownListNormal" runat="server"
                                        AutoPostBack="True" OnSelectedIndexChanged="ddlOrd_PONumber_SelectedIndexChanged"
                                        TabIndex="2">
                                    </asp:DropDownList>
                                    <asp:ImageButton ID="ImgButtonQuery" ImageUrl="~/images/ifind.png" alt="Query" runat="server"
                                        SkinID="ImageButtonSearch" OnClick="ImgButtonQuery_Click" TabIndex="1" />
                                </td>
                                <td class="label">
                                    <asp:Label ID="lblOrdIndentDate" runat="server" SkinID="LabelNormal" Text="PO date"></asp:Label>
                                </td>
                                <td class="inputcontrols">
                                    <asp:TextBox ID="txtOrdIndentDate" SkinID="TextBoxDisabled" ReadOnly="true" runat="server"
                                        TabIndex="3"></asp:TextBox>
                                </td>
                                <td class="label">
                                    <asp:Label ID="lblOrdTransactionType" runat="server" SkinID="LabelNormal" Text="Transaction type"></asp:Label>
                                </td>
                                <td class="inputcontrols">
                                    <asp:DropDownList ID="ddlOrdTransactionType" runat="server" AutoPostBack="True" SkinID="DropDownListNormal"
                                        OnSelectedIndexChanged="ddlOrdTransactionType_SelectedIndexChanged" TabIndex="3">
                                    </asp:DropDownList>
                                </td>
                            </tr>
                            <tr>
                                <td class="label">
                                    <asp:Label ID="lblOrdSupplier" runat="server" SkinID="LabelNormal" Text="Supplier"></asp:Label>
                                </td>
                                <td class="inputcontrols">
                                    <asp:DropDownList ID="ddlOrdSupplier" runat="server" AutoPostBack="True" SkinID="DropDownListNormal"
                                        OnSelectedIndexChanged="ddlOrdSupplier_SelectedIndexChanged" TabIndex="4">
                                    </asp:DropDownList>
                                </td>
                                <td class="label">
                                    <asp:Label ID="lblPOType" runat="server" SkinID="LabelNormal" Text="PO Type"></asp:Label>
                                </td>
                                <td class="inputcontrols">
                                    <asp:TextBox ID="txtPOType" SkinID="TextBoxDisabled" ReadOnly="true" runat="server"
                                        TabIndex="3"></asp:TextBox>
                                </td>
                                <td class="label">
                                    <asp:Label ID="lblCustomer" SkinID="LabelNormal" runat="server" Text="Customer"></asp:Label>
                                </td>
                                <td class="inputcontrols">
                                    <asp:DropDownList ID="ddlOrdCustomer" runat="server" AutoPostBack="True" SkinID="DropDownListNormal"
                                        OnSelectedIndexChanged="ddlOrdCustomer_SelectedIndexChanged" TabIndex="5">
                                    </asp:DropDownList>
                                </td>
                                <td class="label">&nbsp;
                                </td>
                                <td class="inputcontrols">&nbsp;
                                </td>
                            </tr>
                        </table>
                        <div class="subFormTitle">
                            CUSTOMER INFORMATION
                        </div>
                        <table class="subFormTable">
                            <tr>
                                <td class="label">
                                    <asp:Label ID="lblOrdCustomerCode" SkinID="LabelNormal" runat="server" Text="Customer Code"></asp:Label>
                                </td>
                                <td class="inputcontrols">
                                    <asp:TextBox ID="txtOrdCustomerCode" SkinID="TextBoxDisabled" ReadOnly="true" runat="server"></asp:TextBox>
                                </td>
                                <td class="label">
                                    <asp:Label ID="lblOrdCustomerAddress1" SkinID="LabelNormal" runat="server" Text="Address1"></asp:Label>
                                </td>
                                <td class="inputcontrols">
                                    <asp:TextBox ID="txtOrdCustomerAddress1" SkinID="TextBoxDisabled" ReadOnly="true"
                                        runat="server"></asp:TextBox>
                                </td>
                                <td class="label">
                                    <asp:Label ID="lblOrdCustomerAddress2" SkinID="LabelNormal" runat="server" Text="Address2"></asp:Label>
                                </td>
                                <td class="inputcontrols">
                                    <asp:TextBox ID="txtOrdCustomerAddress2" SkinID="TextBoxDisabled" ReadOnly="true"
                                        runat="server"></asp:TextBox>
                                </td>
                                <td class="label">
                                    <asp:Label ID="lblTin_NoAddress3" SkinID="LabelNormal" runat="server" Text="Address3"></asp:Label>
                                </td>
                                <td class="inputcontrols">
                                    <asp:TextBox ID="txtOrdAddress3" SkinID="TextBoxDisabled" ReadOnly="true" runat="server"></asp:TextBox>
                                </td>
                            </tr>
                            <tr>
                                <td class="label">
                                    <asp:Label ID="lblOrdCustomerAddress4" runat="server" SkinID="LabelNormal" Text="Address4"></asp:Label>
                                </td>
                                <td class="inputcontrols">
                                    <asp:TextBox ID="txtOrdCustomerAddress4" SkinID="TextBoxDisabled" ReadOnly="true"
                                        runat="server"></asp:TextBox>
                                </td>
                                <td class="label">
                                    <asp:Label ID="lblOrdCustomerLocation" SkinID="LabelNormal" runat="server" Text="Location"></asp:Label>
                                </td>
                                <td class="inputcontrols">
                                    <asp:TextBox ID="txtOrdCustomerLocation" SkinID="TextBoxDisabled" ReadOnly="true"
                                        runat="server"></asp:TextBox>
                                </td>
                            </tr>
                        </table>
                    </div>
                </div>
            </ContentTemplate>
            <Triggers>
                <asp:PostBackTrigger ControlID="ddlOrd_PONumber" />
                <asp:AsyncPostBackTrigger ControlID="btnReset" EventName="Click" />
            </Triggers>
        </asp:UpdatePanel>
        <div class="transactionButtons">
            <div class="transactionButtonsHolder">                
                <asp:Button SkinID="ButtonViewReport" ID="BtnSupplierExcelFile" runat="server" Text="Supplier Excel"
                    TabIndex="3" OnClick="BtnSupplierExcelFile_Click" Visible="false" />
                <asp:Button SkinID="ButtonViewReport" ID="BtnBranchExcelFile" runat="server" Text="Branch Excel"
                    TabIndex="3" OnClick="BtnBranchExcelFile_Click" Visible="false" />
                <asp:Button ID="btnReset" runat="server" OnClientClick="return resetAction();" CausesValidation="true"
                    SkinID="ButtonNormal" OnClick="btnReset_Click" Text="Reset" />
            </div>
        </div>
        <input id="hdnRowCnt" type="hidden" runat="server" />
        <input id="hdnFreezeRowCnt" type="hidden" runat="server" />
        <asp:HiddenField ID="hdnJSonExcelData" runat="server" />
    </div>
</asp:Content>
