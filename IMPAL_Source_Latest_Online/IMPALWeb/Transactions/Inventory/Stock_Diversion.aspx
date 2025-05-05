<%@ Page Language="C#" AutoEventWireup="true" MasterPageFile="~/Main.Master" CodeBehind="Stock_Diversion.aspx.cs"
    Inherits="IMPALWeb.Transactions.Inventory.Stock_Diversion" %>

<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="ajaxToolkit" %>
<asp:Content ID="Content3" ContentPlaceHolderID="CPHDetails" runat="server">
 
  <script src="../../Javascript/DirectPOcommon.js"   type="text/javascript"></script>
    <asp:UpdatePanel ID="UpdatePanel1" runat="server">
        <ContentTemplate>
          
            <div id="DivOuter" runat="server">
                <div>
                    <div class="subFormTitle">
                        STOCK DIVERSION</div>
                    <table class="subFormTable">
                    
                        <tr>
                            <td class="labelColSpan2">
                                <asp:Label ID="lblDiversionNumber" runat="server" SkinID="LabelNormal" Text="Diversion Number"></asp:Label>
                                <td class="inputcontrolsColSpan2">
                                    <asp:TextBox ID="txtDiversionNumber" TabIndex="1" SkinID="TextBoxDisabledBig" ReadOnly="true" runat="server"></asp:TextBox>
                                    <asp:DropDownList ID="ddlDiversionNumber" TabIndex="2" runat="server" AutoPostBack="True" SkinID="DropDownListNormalBig" OnSelectedIndexChanged="ddlDiversionNumber_SelectedIndexChanged">
                                    </asp:DropDownList>
                                    <asp:ImageButton ID="ImgButtonQuery" TabIndex="3" ImageUrl="~/images/ifind.png" alt="Query" SkinID="ImageButtonSearch"
                                        runat="server" CausesValidation="true" OnClick="ImgButtonQuery_Click" />
                                </td>
                                </tr>
                        <tr>
                            <td class="labelColSpan2">
                                <asp:Label ID="lblDiversionDate" runat="server"  SkinID="LabelNormal" Text="Diversion Date"></asp:Label>
                            </td>
                            <td class="inputcontrolsColSpan2">
                                <asp:TextBox ID="txtDiversionDate" SkinID="TextBoxDisabledBig" TabIndex="4" ReadOnly="true" runat="server"></asp:TextBox>
                            </td>
                        </tr>
                        <tr>
                            <td class="labelColSpan2">
                                <asp:Label ID="lblFromTransactionType" runat="server" SkinID="LabelNormal" Text="From Transaction Type"></asp:Label>
                                <span class="asterix">*</span></td>
                            <td class="inputcontrolsColSpan2">
                                <asp:DropDownList ID="ddlFromTransactionType" TabIndex="5" SkinID="DropDownListNormalBig"  runat="server"
                                    AutoPostBack="True" OnSelectedIndexChanged="ddlFromTransactionType_SelectedIndexChanged">
                                </asp:DropDownList>
                            </td>
                        </tr>
                        <tr>
                            <td class="labelColSpan2">
                                <asp:Label ID="lblToTransactionType" runat="server" SkinID="LabelNormal" Text="To Transaction Type"></asp:Label>
                                <span class="asterix">*</span></td>
                            <td class="inputcontrolsColSpan2">
                                <asp:DropDownList ID="ddlToTransactionType" TabIndex="6" 
                                    SkinID="DropDownListNormalBig" runat="server" AutoPostBack="True" 
                                    onselectedindexchanged="ddlToTransactionType_SelectedIndexChanged">
                                </asp:DropDownList>
                            </td>
                        </tr>
                        <tr>
                            <td class="labelColSpan2">
                                <asp:Label ID="lblSupplierPartNumber" runat="server" SkinID="LabelNormal" Text="Supplier Part Number"></asp:Label>
                                 <span class="asterix">*</span></td>
                            </td>
                            <td class="inputcontrolsColSpan2">
                                <asp:TextBox ID="txtSupplierPartNumber" SkinID="TextBoxNormalBig" TabIndex="7"  runat="server"></asp:TextBox>
                                <asp:DropDownList ID="ddlSupplierPartNumber" SkinID="DropDownListNormalBig" TabIndex="8"  runat="server" AutoPostBack="True"
                                     OnSelectedIndexChanged="ddlSupplierPartNumber_SelectedIndexChanged">
                                </asp:DropDownList>
                                
                                 
                                <asp:ImageButton ID="ImagebtnSupplier" TabIndex="9"  ImageUrl="~/images/ifind.png" SkinID="ImageButtonSearch"
                                    alt="Query" runat="server" CausesValidation="true" OnClick="ImagebtnSupplier_Click" />
                            </td>
                        </tr>
                        <tr>
                            <td class="labelColSpan2">
                                <asp:Label ID="lblItemCode" runat="server" SkinID="LabelNormal" Text="Item Code"></asp:Label>
                            </td>
                            <td class="inputcontrolsColSpan2">
                                <asp:TextBox ID="txtItemCode" ReadOnly="true" contentEditable="false" TabIndex="10" SkinID="TextBoxDisabledBig" runat="server"></asp:TextBox>
                                <asp:Image ID="imgViewSupplierLine" ImageUrl="~/images/ifind.png" 
                                    SkinID="ImageEdit"  runat="server" Height="16px" />
                                  <ajaxToolkit:HoverMenuExtender ID="hme2" runat="Server" TargetControlID="imgViewSupplierLine"
                                                PopupControlID="PnlViewSupplierDetails" HoverCssClass="popupHover" PopupPosition="Right" OffsetX="0"
                                                OffsetY="0" PopDelay="50" />
                            </td>
                        </tr>
                        <tr>
                            <td class="labelColSpan2">
                                <asp:Label ID="lblInwardNumber" runat="server" SkinID="LabelNormal" Text="Inward number"></asp:Label>
                                <span class="asterix">*</span></td>
                            <td class="inputcontrolsColSpan2">
                                <asp:DropDownList ID="ddlInwardNumber" TabIndex="11" SkinID="DropDownListNormalBig" runat="server" AutoPostBack="True"
                                    OnSelectedIndexChanged="ddlInwardNumber_SelectedIndexChanged">
                                </asp:DropDownList>
                            </td>
                        </tr>
                        <tr id="trSerialNo" runat="server">
                            <td class="labelColSpan2">
                                <asp:Label ID="Label1" runat="server" SkinID="LabelNormal" Text="Serial No"></asp:Label>
                            </td>
                            <td class="inputcontrolsColSpan2">
                                <asp:DropDownList ID="ddlSerailNo" SkinID="DropDownListNormalBig" runat="server" AutoPostBack="True"
                                    TabIndex="12" OnSelectedIndexChanged="ddlSerailNo_SelectedIndexChanged">
                                </asp:DropDownList>
                            </td>
                        </tr>
                        <tr>
                            <td class="labelColSpan2">
                                <asp:Label ID="lblQuantity" runat="server" SkinID="LabelNormal" Text="Quantity"></asp:Label>
                                <span class="asterix">*</span></td>
                            <td class="inputcontrolsColSpan2">
                                <asp:TextBox ID="txtQuantity" TabIndex="13" MaxLength="6" SkinID="TextBoxNormalBig" onkeypress="return IntegerValueOnly();" runat="server"></asp:TextBox>
                            </td>
                        </tr>
                        <tr>
                            <td class="labelColSpan2">
                                <asp:Label ID="lblReason" runat="server" SkinID="LabelNormal" Text="Reason"></asp:Label>
                            </td>
                            <td class="inputcontrolsColSpan2">
                                <asp:TextBox ID="txtReason" TabIndex="14" SkinID="TextBoxNormalBig" runat="server"></asp:TextBox>
                            </td>
                        </tr>
                        <tr>
                            <td class="labelColSpan2" colspan="2">
                                <asp:Label ID="lblError" runat="server" SkinID="LabelNormal" ForeColor="Red"></asp:Label>
                            </td>
                        </tr>
                        <tr>
                            <td class="labelColSpan2">
                                
                            </td>
                            <td class="inputcontrolsColSpan2">
                                
                                     <asp:HiddenField ID="HdnOrgQuantity" runat="server" />
                            </td>
                        </tr>
                    </table>
                    
                     <div class="transactionButtons">
                         <div class="transactionButtonsHolder">
                                    <asp:Button ID="BtnSubmit" runat="server" TabIndex="15" OnClick="BtnSubmit_Click" OnClientClick="return funStockDiversionSubmitValidation();" SkinID="ButtonNormalBig"
                                    Text="Submit" />
                                    <asp:Button ID="btnReset" runat="server" TabIndex="16" Text="Reset" SkinID="ButtonNormalBig"
                                    OnClick="btnReset_Click" />
                         </div>
                    </div>
                    
                        <asp:Panel ID="PnlViewSupplierDetails" style="z-index:101;" CssClass="popupMenu"  runat="server">
                          <table id="tblviewdeails" border="1" width="200px">
                            <tr>
                                <td> 
                                    <asp:Label ID="lblSupplier_Line" SkinID="LabelNormal" Text="Supplier Line" runat="server"></asp:Label>
                                </td> 
                                <td>
                                    <asp:Label ID="lblSupplier_Line_Value" SkinID="LabelNormal" runat="server"></asp:Label>
                                </td>
                            </tr>
                             <tr>
                                <td> 
                                    <asp:Label ID="lblApplication_Segment" SkinID="LabelNormal" Text="Application Segment" runat="server"></asp:Label>
                                </td> 
                                <td>
                                    <asp:Label ID="lblApplication_Segment_Value" SkinID="LabelNormal" runat="server"></asp:Label>
                                </td>
                            </tr>
                             <tr>
                                <td> 
                                    <asp:Label ID="lblVehichleTypeDescription" SkinID="LabelNormal" Text="Vehichle Type Description" runat="server"></asp:Label>
                                </td> 
                                <td>
                                    <asp:Label ID="lblVehichleTypeDescriptionValue" SkinID="LabelNormal" runat="server"></asp:Label>
                                </td>
                            </tr>
                          </table>
                        </asp:Panel>
                </div>
            </div>
        </ContentTemplate>
    </asp:UpdatePanel>
</asp:Content>
