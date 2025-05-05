<%@ Page Title="" Language="C#" MasterPageFile="~/Main.Master" AutoEventWireup="true"
    CodeBehind="ItemPosting.aspx.cs" Inherits="IMPALWeb.Masters.Others.ItemPosting" %>

<%@ Register Namespace="AjaxControlToolkit" Assembly="AjaxControlToolkit" TagPrefix="ajax" %>
<%@ Register Src="~/UserControls/ItemCodePartNumber.ascx" TagName="ItemCodePartNumber"
    TagPrefix="User" %>
<asp:Content ID="Content1" ContentPlaceHolderID="CPHDetails" runat="server">
    <div>
        <div class="subFormTitle">
            Item Master</div>
        <asp:UpdatePanel ID="UpdatePanel1" runat="server" UpdateMode="Conditional">
            <ContentTemplate>
                <table>
                    <tr>
                        <td>
                            <asp:ObjectDataSource ID="ODSOPBranch" runat="server" SelectMethod="GetAllBranch"
                                TypeName="IMPALLibrary.Branches"></asp:ObjectDataSource>
                            <asp:ObjectDataSource ID="objDSSupplierLineList" runat="server" SelectMethod="GetSupplierLineList"
                                TypeName="IMPALLibrary.CustomerSlabMaster"></asp:ObjectDataSource>
                        </td>
                    </tr>
                    <tr>
                        <td>
                            <div class="subFormTitle">
                                ITEM DETAILS</div>
                        </td>
                    </tr>
                    <tr>
                        <td>
                            <asp:Label ID="lblStateCode" runat="server" Text="State" SkinID="LabelNormal"></asp:Label>
                        </td>
                        <td>
                            <asp:DropDownList ID="ddlState" runat="server" DataSourceID="ODSOPBranch" DataTextField="BranchName"
                                DataValueField="BranchCode" SkinID="DropDownListNormal">
                            </asp:DropDownList>
                            <br />
                            <asp:RequiredFieldValidator ID="RequiredFieldState" ValidationGroup="Validate" runat="server"
                                ForeColor="Red" ControlToValidate="ddlState" SetFocusOnError="true" ErrorMessage="State should not be null"
                                InitialValue="0"></asp:RequiredFieldValidator>
                        </td>
                        <td>
                            <asp:Label ID="lblLineCode" runat="server" Text="Line Code" SkinID="LabelNormal"></asp:Label>
                        </td>
                        <td>
                            <asp:DropDownList ID="ddlLineCode" runat="server" AutoPostBack="true" DataSourceID="objDSSupplierLineList"
                                DataTextField="SupplierLineName" DataValueField="SupplierLineCode" SkinID="DropDownListNormal"
                                OnSelectedIndexChanged="ddlLineCode_SelectedIndexChanged">
                            </asp:DropDownList>
                            <br />
                            <asp:RequiredFieldValidator ID="RequiredFieldLineCode" ValidationGroup="Validate"
                                runat="server" ForeColor="Red" ControlToValidate="ddlLineCode" SetFocusOnError="true"
                                ErrorMessage="Line Code should not be null" InitialValue="0"></asp:RequiredFieldValidator>
                        </td>
                    </tr>
                    <tr>
                        <td>
                            <asp:Label ID="lblItemCode" runat="server" Text="Item Code" SkinID="LabelNormal"></asp:Label>
                        </td>
                        <td>
                            <asp:TextBox ID="txtItemCode" SkinID="TextBoxNormal" runat="server"></asp:TextBox>
                            <br />
                            <User:ItemCodePartNumber ID="supLineCtl" runat="server" Mode="2" Disable="false"
                                OnSearchImageClicked="ucSupplierPartNumber_SearchImageClicked" />
                            <br />
                            <asp:RequiredFieldValidator ID="RequiredFieldItemCode" ValidationGroup="Validate"
                                runat="server" ForeColor="Red" ControlToValidate="txtItemCode" SetFocusOnError="true"
                                ErrorMessage="Item Code should not be null"></asp:RequiredFieldValidator>
                        </td>
                        <td>
                            <asp:Label ID="Label1" runat="server" Text="Supp. Part # " SkinID="LabelNormal"></asp:Label>
                        </td>
                        <td>
                            <asp:TextBox ID="txtSupPartNo" SkinID="TextBoxNormal" Enabled="false" runat="server"></asp:TextBox>
                        </td>
                    </tr>
                    <tr>
                        <td>
                            <asp:Label ID="Label2" runat="server" Text="List Price" SkinID="LabelNormal"></asp:Label>
                        </td>
                        <td>
                            <asp:TextBox ID="txtListPrice" SkinID="TextBoxNormal" runat="server"></asp:TextBox>
                            <br />
                            <asp:RequiredFieldValidator ID="RequiredFieldListPrice" ValidationGroup="Validate"
                                runat="server" ForeColor="Red" ControlToValidate="txtListPrice" SetFocusOnError="true"
                                ErrorMessage="List Price should not be null"></asp:RequiredFieldValidator>
                        </td>
                        <td>
                            <asp:Label ID="Label3" runat="server" Text="Selling Price" SkinID="LabelNormal"></asp:Label>
                        </td>
                        <td>
                            <asp:TextBox ID="txtSellingPrice" SkinID="TextBoxNormal" Enabled="false" runat="server"></asp:TextBox>
                            <br />
                            <asp:RequiredFieldValidator ID="RequiredFieldSellingPrice" ValidationGroup="Validate"
                                runat="server" ForeColor="Red" ControlToValidate="txtSellingPrice" SetFocusOnError="true"
                                ErrorMessage="Selling Price should not be null"></asp:RequiredFieldValidator>                            
                        </td>
                    </tr>
                    <tr>
                        <td>
                            <asp:Label ID="Label4" runat="server" Text="Tax" SkinID="LabelNormal"></asp:Label>
                        </td>
                        <td>
                            <asp:TextBox ID="txtTax" runat="server" SkinID="TextBoxNormal"></asp:TextBox>
                            <br />
                            <asp:RequiredFieldValidator ID="RequiredFieldTax" ValidationGroup="Validate"
                                runat="server" ForeColor="Red" ControlToValidate="txtTax" SetFocusOnError="true"
                                ErrorMessage="Tax should not be null"></asp:RequiredFieldValidator>                            
                        </td>
                        <td>
                            <asp:Label ID="Label5" runat="server" Text="SurCharge" SkinID="LabelNormal"></asp:Label>
                        </td>
                        <td>
                            <asp:TextBox ID="txtSurCharge" Enabled="false" SkinID="TextBoxNormal" runat="server"></asp:TextBox>
                            <br />
                            <asp:RequiredFieldValidator ID="RequiredFieldSurCharge" ValidationGroup="Validate"
                                runat="server" ForeColor="Red" ControlToValidate="txtSurCharge" SetFocusOnError="true"
                                ErrorMessage="SurCharge should not be null"></asp:RequiredFieldValidator>                               
                        </td>
                    </tr>
                    <tr>
                        <td>
                            <asp:Label ID="Label6" runat="server" Text="Addl.Surcharge" SkinID="LabelNormal"></asp:Label>
                        </td>
                        <td>
                            <asp:TextBox ID="txtAddlSurcharge" SkinID="TextBoxNormal" runat="server"></asp:TextBox>
                            <br />
                            <asp:RequiredFieldValidator ID="RequiredFieldAddlSurcharge" ValidationGroup="Validate"
                                runat="server" ForeColor="Red" ControlToValidate="txtAddlSurcharge" SetFocusOnError="true"
                                ErrorMessage="Addl. Surcharge should not be null"></asp:RequiredFieldValidator>                                 
                        </td>
                        <td>
                            <asp:Label ID="Label7" runat="server" Text="Entry Tax" SkinID="LabelNormal"></asp:Label>
                        </td>
                        <td>
                            <asp:TextBox ID="txtEntryTax" SkinID="TextBoxNormal" Enabled="false" runat="server"></asp:TextBox>
                            <br />
                            <asp:RequiredFieldValidator ID="RequiredFieldEntryTax" ValidationGroup="Validate"
                                runat="server" ForeColor="Red" ControlToValidate="txtEntryTax" SetFocusOnError="true"
                                ErrorMessage="Entry Tax should not be null"></asp:RequiredFieldValidator>                              
                        </td>
                    </tr>
                    <tr>
                        <td>
                            <asp:Label ID="Label8" runat="server" Text="Coupon Charges" SkinID="LabelNormal"></asp:Label>
                        </td>
                        <td>
                            <asp:TextBox ID="txtCouponCharges" SkinID="TextBoxNormal" runat="server"></asp:TextBox>
                            <br />
                            <asp:RequiredFieldValidator ID="RequiredFieldCouponCharges" ValidationGroup="Validate"
                                runat="server" ForeColor="Red" ControlToValidate="txtCouponCharges" SetFocusOnError="true"
                                ErrorMessage="Coupon charges should not be null"></asp:RequiredFieldValidator>                             
                        </td>
                        <td>
                            <asp:Label ID="Label9" runat="server" Text="Resale Tax" SkinID="LabelNormal"></asp:Label>
                        </td>
                        <td>
                            <asp:TextBox ID="txtResaleTax" SkinID="TextBoxNormal" Enabled="false" runat="server"></asp:TextBox>
                            <br />
                            <asp:RequiredFieldValidator ID="RequiredFieldResaleTax" ValidationGroup="Validate"
                                runat="server" ForeColor="Red" ControlToValidate="txtResaleTax" SetFocusOnError="true"
                                ErrorMessage="Resale Tax should not be null"></asp:RequiredFieldValidator>                               
                        </td>
                    </tr>
                    <tr>
                        <td>
                            <asp:Label ID="Label10" runat="server" Text="Addl. Discount" SkinID="LabelNormal"></asp:Label>
                        </td>
                        <td>
                            <asp:TextBox ID="txtAddlDiscount" SkinID="TextBoxNormal" runat="server"></asp:TextBox>
                            <br />
                            <asp:RequiredFieldValidator ID="RequiredFieldAddlDiscount" ValidationGroup="Validate"
                                runat="server" ForeColor="Red" ControlToValidate="txtAddlDiscount" SetFocusOnError="true"
                                ErrorMessage="Additional discount should not be null"></asp:RequiredFieldValidator>                              
                        </td>
                        <td>
                            <asp:Label ID="Label11" runat="server" Text="Other Charges" SkinID="LabelNormal"></asp:Label>
                        </td>
                        <td>
                            <asp:TextBox ID="txtOtherCharges" SkinID="TextBoxNormal" Enabled="false" runat="server"></asp:TextBox>
                            <br />
                            <asp:RequiredFieldValidator ID="RequiredFieldOtherCharges" ValidationGroup="Validate"
                                runat="server" ForeColor="Red" ControlToValidate="txtOtherCharges" SetFocusOnError="true"
                                ErrorMessage="Other charges should not be null"></asp:RequiredFieldValidator>                               
                        </td>
                    </tr>
                    <tr>
                        <td>
                            <div class="subFormTitle">
                                SLB DETAILS</div>
                        </td>
                    </tr>
                    <tr>
                        <td>
                            <asp:Label ID="Label12" runat="server" Text="SLB" SkinID="LabelNormal"></asp:Label>
                        </td>
                        <td>
                            <asp:TextBox ID="txtSLB" runat="server" SkinID="TextBoxNormal"></asp:TextBox>
                            <br />
                            <asp:RequiredFieldValidator ID="RequiredFieldSLB" ValidationGroup="Validate"
                                runat="server" ForeColor="Red" ControlToValidate="txtSLB" SetFocusOnError="true"
                                ErrorMessage="SLB should not be null"></asp:RequiredFieldValidator>                                 
                        </td>
                        <td>
                            <asp:Label ID="Label13" runat="server" Text="OS Value" SkinID="LabelNormal"></asp:Label>
                        </td>
                        <td>
                            <asp:TextBox ID="txtOSValue" Enabled="false" runat="server" SkinID="TextBoxNormal"></asp:TextBox>
                            <br />
                            <asp:RequiredFieldValidator ID="RequiredFieldOSValue" ValidationGroup="Validate"
                                runat="server" ForeColor="Red" ControlToValidate="txtOSValue" SetFocusOnError="true"
                                ErrorMessage="OS Value should not be null"></asp:RequiredFieldValidator>
                        </td>
                    </tr>
                    <tr>
                        <td>
                            <asp:Label ID="Label14" runat="server" Text="LS Value" SkinID="LabelNormal"></asp:Label>
                        </td>
                        <td>
                            <asp:TextBox ID="txtLSValue" runat="server" SkinID="TextBoxNormal"></asp:TextBox>
                            <br />
                            <asp:RequiredFieldValidator ID="RequiredFieldLSValue" ValidationGroup="Validate"
                                runat="server" ForeColor="Red" ControlToValidate="txtLSValue" SetFocusOnError="true"
                                ErrorMessage="LS Value should not be null"></asp:RequiredFieldValidator>                            
                        </td>
                        <td>
                            <asp:Label ID="Label15" runat="server" Text="FDO Value" SkinID="LabelNormal"></asp:Label>
                        </td>
                        <td>
                            <asp:TextBox ID="txtFDOValue" Enabled="false" runat="server" SkinID="TextBoxNormal"></asp:TextBox>
                            <br />
                            <asp:RequiredFieldValidator ID="RequiredFieldFDOValue" ValidationGroup="Validate"
                                runat="server" ForeColor="Red" ControlToValidate="txtFDOValue" SetFocusOnError="true"
                                ErrorMessage="FDO Value should not be null"></asp:RequiredFieldValidator>
                        </td>
                    </tr>
                    <tr>
                        <td>
                            <asp:Label ID="Label16" runat="server" Text="LR Value" SkinID="LabelNormal"></asp:Label>
                        </td>
                        <td>
                            <asp:TextBox ID="txtLRValue" runat="server" SkinID="TextBoxNormal"></asp:TextBox>
                            <br />
                            <asp:RequiredFieldValidator ID="RequiredFieldLRValue" ValidationGroup="Validate"
                                runat="server" ForeColor="Red" ControlToValidate="txtLRValue" SetFocusOnError="true"
                                ErrorMessage="LR Value should not be null"></asp:RequiredFieldValidator>
                        </td>
                    </tr>
                </table>
            </ContentTemplate>
            <Triggers>
                <asp:AsyncPostBackTrigger ControlID="ddlLineCode" EventName="SelectedIndexChanged" />
            </Triggers>
        </asp:UpdatePanel>
        <div class="transactionButtons">
            <div class="transactionButtonsHolder">
                <asp:Button ID="BtnSubmit" runat="server" Text="Submit" SkinID="ButtonNormal" Enabled="false" ValidationGroup="Validate"/>
                <asp:Button ID="btnReset" runat="server" Text="Reset" SkinID="ButtonNormal" Enabled="false" />
            </div>
        </div>
    </div>
</asp:Content>
