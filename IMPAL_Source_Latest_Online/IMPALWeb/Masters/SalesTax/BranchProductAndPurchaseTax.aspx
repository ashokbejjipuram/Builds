<%@ Page Language="C#" AutoEventWireup="true" MasterPageFile="~/Main.Master" CodeBehind="BranchProductAndPurchaseTax.aspx.cs" Inherits="IMPALWeb.Masters.SalesTax.BranchProductAndPurchaseTax" %>
<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="ajaxToolkit" %>
<asp:content id="CPHDetails" contentplaceholderid="CPHDetails" runat="server">
<script src="../../Javascript/DirectPOcommon.js"   type="text/javascript"></script>
<script type="text/javascript">
    function pageLoad(sender, args) {
        //gridViewFixedHeader(gridViewID, gridViewWidth, gridViewHeight)
        gridViewFixedHeader('<%=GV_BrnProductPurchaseTax.ClientID%>', '1200', '800');
    }
    </script>

    <div id="DivOuter">
        <div class="subFormTitle subFormTitleExtender300"> BRANCH PRODUCT AND SALES TAX  </div>
        <asp:UpdatePanel ID="UpdatePanel1" runat="server">
            <ContentTemplate>
             <div class="gridViewScrollFullPage"> 
                <asp:GridView ID="GV_BrnProductPurchaseTax" runat="server" AllowPaging="false" ShowFooter="True"
                            AutoGenerateColumns="False" SkinID="GridViewScroll" 
                            OnPageIndexChanging="GV_BrnProductPurchaseTax_PageIndexChanging"                           
                            onrowdatabound="GV_BrnProductPurchaseTax_RowDataBound" 
                    onrowediting="GV_BrnProductPurchaseTax_RowEditing" 
                    onrowupdating="GV_BrnProductPurchaseTax_RowUpdating"                      
                    onrowcancelingedit="GV_BrnProductPurchaseTax_RowCancelingEdit">
                            <EmptyDataTemplate>
                                <asp:Label ID="lblEmptySearch" runat="server" SkinID="GridViewLabel">No Results Found</asp:Label>
                            </EmptyDataTemplate>
                            <Columns>
                                <asp:TemplateField HeaderText="SerialNumber" SortExpression="SerialNumber">
                                    <ItemTemplate>
                                        <asp:Label ID="lblSerialNumber" runat="server" Text='<%# Bind("SerialNumber") %>'
                                            SkinID="GridViewLabel">
                                        </asp:Label>
                                    </ItemTemplate>
                                    <EditItemTemplate>
                                        <asp:Label ID="lblSerialNumber" runat="server" Text='<%# Bind("SerialNumber") %>'
                                            SkinID="GridViewLabel">
                                        </asp:Label>
                                    </EditItemTemplate>
                                </asp:TemplateField>
                                
                                  <asp:TemplateField HeaderText="Branch" SortExpression="GVBranchCode">
                                    <ItemTemplate>
                                        <asp:Label ID="lblGVBranchName" runat="server" Text='<%# Bind("GVBranchName") %>'
                                            SkinID="GridViewLabel">
                                        </asp:Label>
                                    </ItemTemplate>
                                    
                                       <edititemtemplate>                
                                                  <asp:DropDownList ID="ddlBranch" Enabled="false" runat="server" SkinID="GridViewDropDownListFooter">
                                                  </asp:DropDownList>
                                                  
                                                   <asp:RequiredFieldValidator ID="ReqddlBranch" ValidationGroup="btnAdd" ForeColor="Red" SkinID="Error" 
                                                        ControlToValidate="ddlBranch" Display="None" InitialValue="0" runat="server"
                                                        ErrorMessage="Please select Branch.">
                                                    </asp:RequiredFieldValidator>
                                                    <ajaxToolkit:ValidatorCalloutExtender ID="ValidatorCalloutExtender1" TargetControlID="ReqddlBranch"
                                                        PopupPosition="Right" runat="server">
                                                    </ajaxToolkit:ValidatorCalloutExtender>     
                                        </edititemtemplate>
                                        
                                     <FooterTemplate>                             
                                        <edititemtemplate>                
                                                  <asp:DropDownList ID="ddlBranch" runat="server" SkinID="GridViewDropDownListFooter">
                                                  </asp:DropDownList>
                                                  
                                                   <asp:RequiredFieldValidator ID="ReqddlBranch" ValidationGroup="btnAdd" ForeColor="Red" SkinID="Error" 
                                                        ControlToValidate="ddlBranch" Display="None" InitialValue="0" runat="server"
                                                        ErrorMessage="Please select Branch.">
                                                    </asp:RequiredFieldValidator>
                                                    <ajaxToolkit:ValidatorCalloutExtender ID="ValidatorCalloutExtender1" TargetControlID="ReqddlBranch"
                                                        PopupPosition="Right" runat="server">
                                                    </ajaxToolkit:ValidatorCalloutExtender>     
                                        </edititemtemplate>
                                        </FooterTemplate>
                                </asp:TemplateField>
                                
                                
                                  <asp:TemplateField HeaderText="Product Group" SortExpression="Product_Group_Desc">
                                    <ItemTemplate>
                                        <asp:Label ID="lblProduct_Group_Desc" runat="server" Text='<%# Bind("Product_Group_Desc") %>'
                                            SkinID="GridViewLabel">
                                        </asp:Label>
                                    </ItemTemplate>
                                        <edititemtemplate>                
                                                  <asp:DropDownList ID="ddlProduct" Enabled="false" runat="server" SkinID="GridViewDropDownListFooter">                                            </asp:DropDownList>
                                                  <asp:RequiredFieldValidator ID="ReqddlProduct" ValidationGroup="btnAdd" ForeColor="Red" SkinID="Error" 
                                                        ControlToValidate="ddlProduct" Display="None" InitialValue="0" runat="server"
                                                        ErrorMessage="Please Select Product.">
                                                    </asp:RequiredFieldValidator>
                                                    <ajaxToolkit:ValidatorCalloutExtender ID="ValidatorCalloutExtender2" TargetControlID="ReqddlProduct"
                                                        PopupPosition="Right" runat="server">
                                                    </ajaxToolkit:ValidatorCalloutExtender>      
                                        </edititemtemplate>
                                     <FooterTemplate>                             
                                        <edititemtemplate>                
                                                  <asp:DropDownList ID="ddlProduct" runat="server" SkinID="GridViewDropDownListFooter">                                            </asp:DropDownList>
                                                  <asp:RequiredFieldValidator ID="ReqddlProduct" ValidationGroup="btnAdd" ForeColor="Red" SkinID="Error" 
                                                        ControlToValidate="ddlProduct" Display="None" InitialValue="0" runat="server"
                                                        ErrorMessage="Please Select Product.">
                                                    </asp:RequiredFieldValidator>
                                                    <ajaxToolkit:ValidatorCalloutExtender ID="ValidatorCalloutExtender2" TargetControlID="ReqddlProduct"
                                                        PopupPosition="Right" runat="server">
                                                    </ajaxToolkit:ValidatorCalloutExtender>      
                                        </edititemtemplate>
                                        </FooterTemplate>
                                </asp:TemplateField>
                                
                                  
                                 <asp:TemplateField HeaderText="Sales Tax" SortExpression="Sales_Tax_Desc">
                                    <ItemTemplate>
                                        <asp:Label ID="lblSales_Tax_Desc" runat="server" Text='<%# Bind("Sales_Tax_Desc") %>'
                                            SkinID="GridViewLabel">
                                        </asp:Label>
                                    </ItemTemplate>
                                            <edititemtemplate>                
                                                  <asp:DropDownList ID="ddlSalesTax" runat="server" 
                                                      SkinID="GridViewDropDownListFooter" onselectedindexchanged="ddlSalesTax_SelectedIndexChanged"
                                                      AutoPostBack="True"
                                                  >
                                                  </asp:DropDownList>                                                   
                                                    <asp:RequiredFieldValidator ID="ReqddlSalesTax" ValidationGroup="btnAdd" ForeColor="Red" SkinID="Error" 
                                                        ControlToValidate="ddlSalesTax" Display="None" InitialValue="0" runat="server"
                                                        ErrorMessage="Please select Sales Tax.">
                                                    </asp:RequiredFieldValidator>
                                                    <ajaxToolkit:ValidatorCalloutExtender ID="ValidatorCalloutExtender3" TargetControlID="ReqddlSalesTax"
                                                        PopupPosition="Right" runat="server">
                                                    </ajaxToolkit:ValidatorCalloutExtender>      
                                        </edititemtemplate>
                                      <FooterTemplate>                             
                                        <edititemtemplate>                
                                                  <asp:DropDownList ID="ddlSalesTax" runat="server" 
                                                      SkinID="GridViewDropDownListFooter" onselectedindexchanged="ddlSalesTax_SelectedIndexChanged"
                                                      AutoPostBack="True"
                                                  >
                                                  </asp:DropDownList>                                                   
                                                    <asp:RequiredFieldValidator ID="ReqddlSalesTax" ValidationGroup="btnAdd" ForeColor="Red" SkinID="Error" 
                                                        ControlToValidate="ddlSalesTax" Display="None" InitialValue="0" runat="server"
                                                        ErrorMessage="Please select Sales Tax.">
                                                    </asp:RequiredFieldValidator>
                                                    <ajaxToolkit:ValidatorCalloutExtender ID="ValidatorCalloutExtender3" TargetControlID="ReqddlSalesTax"
                                                        PopupPosition="Right" runat="server">
                                                    </ajaxToolkit:ValidatorCalloutExtender>      
                                        </edititemtemplate>
                                        </FooterTemplate>
                                </asp:TemplateField>
                                
                                
                                 <asp:TemplateField HeaderText="Percentage" SortExpression="Sales_Tax_Percentage">
                                    <ItemTemplate>
                                        <asp:Label ID="lblSales_Tax_Percentage" runat="server" Text='<%# Bind("Sales_Tax_Percentage") %>'
                                            SkinID="GridViewLabel">
                                        </asp:Label>
                                    </ItemTemplate>
                                    <edititemtemplate>
                                                    <asp:TextBox ID="txtPercentage" ReadOnly="true" SkinID="GridViewTextBox" Text='<%# Bind("Sales_Tax_Percentage") %>' runat="server" />
                                    </edititemtemplate>
                                       <FooterTemplate>
                                           <edititemtemplate>
                                                    <asp:TextBox ID="txtPercentage" ReadOnly="true" SkinID="GridViewTextBox" runat="server" />
                                            </edititemtemplate>
                                        </FooterTemplate>
                                </asp:TemplateField>
                                
                                  <asp:TemplateField HeaderText="Indicator" SortExpression="Sales_Tax_Indicator">
                                    <ItemTemplate>
                                        <asp:Label ID="lblSales_Tax_Indicator" runat="server" Text='<%# Bind("Sales_Tax_Indicator") %>'
                                            SkinID="GridViewLabel">
                                        </asp:Label>
                                    </ItemTemplate>
                                     <edititemtemplate>
                                                    <asp:TextBox ID="txtIndicator" ReadOnly="true" SkinID="GridViewTextBox"  Text='<%# Bind("Sales_Tax_Indicator") %>' runat="server" />
                                     </edititemtemplate>
                                     <FooterTemplate>
                                           <edititemtemplate>
                                                    <asp:TextBox ID="txtIndicator" ReadOnly="true" SkinID="GridViewTextBox" runat="server" />
                                            </edititemtemplate>
                                        </FooterTemplate>
                                </asp:TemplateField>
                                
                                <asp:TemplateField HeaderText="OS / LS" SortExpression="OS_LS_Indicator">
                                    <ItemTemplate>
                                        <asp:Label ID="lblOS_LS_Indicator" runat="server" Text='<%# Bind("OS_LS_Indicator") %>'
                                            SkinID="GridViewLabel">
                                        </asp:Label>
                                    </ItemTemplate>
                                        <edititemtemplate>                
                                                  <asp:DropDownList ID="ddlOSLS" runat="server" SkinID="GridViewDropDownListFooter">
                                                      <asp:ListItem Value="O" Text="OS"></asp:ListItem>
                                                      <asp:ListItem Value="L" Text="LS"></asp:ListItem>
                                                  </asp:DropDownList> 
                                        </edititemtemplate>
                                    <FooterTemplate>                             
                                        <edititemtemplate>                
                                                  <asp:DropDownList ID="ddlOSLS" runat="server" SkinID="GridViewDropDownListFooter">
                                                      <asp:ListItem Value="O" Text="OS"></asp:ListItem>
                                                      <asp:ListItem Value="L" Text="LS"></asp:ListItem>
                                                  </asp:DropDownList> 
                                        </edititemtemplate>
                                        </FooterTemplate>
                                </asp:TemplateField>
                                
                                 <asp:TemplateField HeaderText="Party Type" SortExpression="Party_Type_Desc">
                                    <ItemTemplate>
                                        <asp:Label ID="lblParty_Type_Desc" runat="server" Text='<%# Bind("Party_Type_Desc") %>'
                                            SkinID="GridViewLabel">
                                        </asp:Label>
                                    </ItemTemplate>
                                    <edititemtemplate>                
                                                  <asp:DropDownList ID="ddlPartType" runat="server" SkinID="GridViewDropDownListFooter">
                                                  </asp:DropDownList>     
                                     </edititemtemplate>
                                     <FooterTemplate>                             
                                        <edititemtemplate>                
                                                  <asp:DropDownList ID="ddlPartType" runat="server" SkinID="GridViewDropDownListFooter">
                                                  </asp:DropDownList>     
                                        </edititemtemplate>
                                        </FooterTemplate>
                                </asp:TemplateField>
                                
                               <asp:TemplateField HeaderText="Sale Tax" SortExpression="Sales_Tax_Text">
                                    <ItemTemplate>
                                        <asp:Label ID="lblSales_Tax_Text" runat="server" Text='<%# Bind("Sales_Tax_Text") %>'
                                            SkinID="GridViewLabel">
                                        </asp:Label>
                                    </ItemTemplate>
                                    <edititemtemplate>
                                                    <asp:TextBox ID="txtSaleTaxText" SkinID="GridViewTextBox" Text='<%# Bind("Sales_Tax_Text") %>' runat="server" />
                                    </edititemtemplate>
                                     <FooterTemplate>
                                           <edititemtemplate>
                                                    <asp:TextBox ID="txtSaleTaxText" SkinID="GridViewTextBox" runat="server" />
                                            </edititemtemplate>
                                        </FooterTemplate>
                                </asp:TemplateField>
                              
                                <asp:TemplateField HeaderText="Form Name" SortExpression="Form_Name_Text">
                                    <ItemTemplate>
                                        <asp:Label ID="lblForm_Name_Text" runat="server" Text='<%# Bind("Form_Name_Text") %>'
                                            SkinID="GridViewLabel">
                                        </asp:Label>
                                    </ItemTemplate>
                                      <edititemtemplate>
                                                    <asp:TextBox ID="txtFormNameText" SkinID="GridViewTextBox" Text='<%# Bind("Form_Name_Text") %>' runat="server" />
                                     </edititemtemplate>
                                        <FooterTemplate>
                                           <edititemtemplate>
                                                    <asp:TextBox ID="txtFormNameText" SkinID="GridViewTextBox" runat="server" />
                                            </edititemtemplate>
                                        </FooterTemplate>
                                </asp:TemplateField>
                                
                                 <asp:TemplateField HeaderText="Status" SortExpression="Status">
                                    <ItemTemplate>
                                        <asp:Label ID="lblStatus" runat="server" Text='<%# Bind("Status") %>'
                                            SkinID="GridViewLabel">
                                        </asp:Label>
                                    </ItemTemplate>
                                     <edititemtemplate>                
                                                  <asp:DropDownList ID="ddlStatus" runat="server" SkinID="GridViewDropDownListFooter">
                                                     <asp:ListItem Value="A" Text="Active"></asp:ListItem>
                                                     <asp:ListItem Value="I" Text="InActive"></asp:ListItem>
                                                  </asp:DropDownList>     
                                        </edititemtemplate>
                                    <FooterTemplate>                             
                                        <edititemtemplate>                
                                                  <asp:DropDownList ID="ddlStatus" runat="server" SkinID="GridViewDropDownListFooter">
                                                     <asp:ListItem Value="A" Text="Active"></asp:ListItem>
                                                     <asp:ListItem Value="I" Text="InActive"></asp:ListItem>
                                                  </asp:DropDownList>     
                                        </edititemtemplate>
                                        </FooterTemplate>
                                </asp:TemplateField>
                                <asp:TemplateField ShowHeader="true">
                                    <FooterTemplate>
                                        <itemtemplate>
                                                <asp:Button ID="btnAdd" ValidationGroup="btnAdd" SkinID="ButtonNormal" 
                                                 runat="server" CommandName="Add" Text="Add" onclick="btnAdd_Click"  />
                                         </itemtemplate>
                                    </FooterTemplate>
                                    <ItemStyle HorizontalAlign="Center" />
                                </asp:TemplateField>
                                <asp:CommandField ButtonType="Button"    ShowEditButton="True" />
                            </Columns>
                        </asp:GridView>
                        
            </div>     
            </ContentTemplate>
       </asp:UpdatePanel>
       <div class="transactionButtons">
                    <div class="transactionButtonsHolder">
                        <asp:Button ID="btnReport" runat="server" Text="Generate Report" SkinID="ButtonNormalBig" OnClick="btnReport_Click" />
                    </div>
                </div>
    </div>
</asp:content>
