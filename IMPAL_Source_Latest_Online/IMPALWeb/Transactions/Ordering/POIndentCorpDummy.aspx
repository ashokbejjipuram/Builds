<%@ Page Language="C#" MasterPageFile="~/Main.Master" AutoEventWireup="true" CodeBehind="POIndentCorpDummy.aspx.cs" Inherits="IMPALWeb.Ordering.POIndentCorpDummy" %>

<asp:Content ID="CPHDetails" ContentPlaceHolderID="CPHDetails" runat="server">   
    <script type="text/javascript">
        function CheckQty(id)
        {
            var Id = "ctl00_CPHDetails_grvItemDetails_";
            var newId = id.split(Id);
            var newId1 = newId[1].split("_");
            var stdnQty = document.getElementById(Id + newId1[0] + "_txtSTDNQuantity").value;
            var OrdetQty = document.getElementById(Id + newId1[0] + "_txtEditHOAccepted").value;           
            if (Number(stdnQty) > 0 && Number(OrdetQty) > Number(stdnQty)) {
                    alert("Ho Quantity should not Greater Then Stdn Qty");
                    return false;
            }
            else
                return true;
        }
    </script>
    <div id="Div2" runat="server">
       <asp:UpdatePanel ID="UpdatePanel2" runat="server" UpdateMode="Conditional">
            <ContentTemplate>
             <div id="DivOuter" runat="server">
                <table class="subFormTable">                
                    <tr>
                        <td class="label">
                            <asp:Label ID="LblAllotedAmt" runat="server" kinID="LabelNormal" 
                                Text="Alloted Amount :" SkinID="LabelNormal"></asp:Label>                                    
                        </td>                   
                        <td class="label">
                            <asp:Label ID="LblAllotedAmtValue" runat="server" SkinID="LabelNormal" ></asp:Label>                                    
                        </td>
                        <td class="label">
                            &nbsp;
                        </td>
                        <td class="label">
                            &nbsp;
                        </td>
                   </tr>
                    <tr>
                        <td class="label">
                            <asp:Label ID="LblCanBill" runat="server" SkinID="LabelNormal" Text="Can Bill UPTO :"></asp:Label>                                    
                        </td>                   
                        <td class="label">
                            <asp:Label ID="LblCanBillValue" runat="server" SkinID="LabelNormal" ></asp:Label>                                    
                        </td>
                        <td class="label">
                            &nbsp;
                        </td>
                        <td class="label">
                            &nbsp;
                        </td>
                     </tr>
                    <tr>
                        <td class="label">
                            <asp:Label ID="LblTotalItem" runat="server" SkinID="LabelNormal" Text="Total Items :"></asp:Label>                                    
                        </td>                      
                        <td class="label">
                            <asp:Label ID="LblTotalItemValue" runat="server" SkinID="LabelNormal"></asp:Label>                                    
                        </td>
                        <td class="label">
                            &nbsp;
                        </td>
                        <td class="label">
                            &nbsp;
                        </td>
                     </tr>
                    <tr>
                        <td class="label">
                            <asp:Label ID="LblRecItems" runat="server" SkinID="LabelNormal" Text="Received Items :"></asp:Label>                                    
                        </td>                      
                        <td class="label">
                            <asp:Label ID="LblRecItemsValue" runat="server" SkinID="LabelNormal" ></asp:Label>                                    
                        </td>
                        <td class="label">
                            &nbsp;
                        </td>
                        <td class="label">
                            &nbsp;
                        </td>
                      </tr>
                    <tr>
                        <td class="label">
                            <asp:Label ID="LblToBeOrdered" runat="server" SkinID="LabelNormal" Text="To Be Ordered Items :"></asp:Label>                                    
                        </td>                       
                        <td class="label">
                            <asp:Label ID="LblToBeOrderedValue" runat="server" SkinID="LabelNormal"></asp:Label>                                    
                        </td>
                        <td class="label">
                            &nbsp;
                        </td>
                        <td class="label">
                            &nbsp;
                        </td>
                     </tr> 
                    <tr>
                        <td class="label">
                            <asp:Label ID="LblPONumber" runat="server" SkinID="LabelNormal" Text="PO NUmber :"></asp:Label>                                    
                        </td>                        
                        <td class="label">
                            <asp:Label ID="LblPONumberValue" runat="server" SkinID="LabelNormal" ></asp:Label>                                    
                        </td>                       
                        <td class="label">
                            <asp:Label ID="LblPONumbersheet" runat="server" SkinID="LabelNormal" ></asp:Label>                                    
                        </td>  
                        <td class="label">
                            &nbsp;
                        </td>
                        <td class="label">
                            &nbsp;
                        </td>                      
                    </tr>                     
                 </table>
               </div>
            </ContentTemplate>            
        </asp:UpdatePanel>     
       <asp:UpdatePanel ID="updateGrd" runat="server">
            <ContentTemplate>               
                <table>                
                    <tr>
                        <td>       
                            <div class="subFormTitle subFormTitleExtender750">
                                Dummy Details - For Corporate (If the accepted quantity is greater than To-Order quantity
                            </div>                     
                            <div class="gridViewScrollFullPage"  style="overflow: auto; -ms-overflow-y: hidden; margin-left: 10px;
                                    width: 1200px; z-index: 1040;">                            
                             <asp:GridView ID="grvItemDetails" runat="server" ShowFooter="True" AutoGenerateColumns="False" 
                                        OnRowDataBound="grvItemDetails_RowDataBound"
                                        SkinID="GridViewScroll">                        
                                    <EmptyDataTemplate>
                                        <asp:Label ID="lblEmptySearch" runat="server" SkinID="GridViewLabel">No Results Found</asp:Label>
                                    </EmptyDataTemplate>
                                    <Columns>                                                 
                                        <asp:TemplateField HeaderText="Part #">
                                            <ItemTemplate>
                                                <asp:Label ID="lblSuppPartNo" Text='<%#  Bind("supplier_part_number") %>' SkinID="GridViewLabel"
                                                    runat="server"></asp:Label>
                                            </ItemTemplate>  
                                            <ItemStyle Width="12px" />
                                            <HeaderStyle  Width="12px" />  
                                        </asp:TemplateField>
                                        <asp:TemplateField HeaderText="Stock <br /> On <br /> Hand">
                                            <ItemTemplate>
                                                <asp:Label ID="lblStockonhand" Text='<%#  Bind("stk_on_hand") %>' SkinID="GridViewLabel"
                                                    runat="server"></asp:Label>
                                            </ItemTemplate>
                                            <ItemStyle Width="10px" />
                                            <HeaderStyle  Width="10px" />                                           
                                        </asp:TemplateField>    
                                        <asp:TemplateField HeaderText="Pending <br /> Order">
                                            <ItemTemplate>
                                                <asp:Label ID="lblPendingOrder" Text='<%#  Bind("pending_order") %>' SkinID="GridViewLabel"
                                                    runat="server"></asp:Label>
                                            </ItemTemplate>
                                            <ItemStyle Width="10px" />
                                            <HeaderStyle  Width="10px" />                                          
                                        </asp:TemplateField> 
                                        <asp:TemplateField HeaderText="Average <br /> sales">
                                            <ItemTemplate>
                                                <asp:Label ID="lblAveragesales" Text='<%#  Bind("avg_sales") %>' SkinID="GridViewLabel"
                                                    runat="server"></asp:Label>
                                            </ItemTemplate>
                                            <ItemStyle Width="10px" />
                                            <HeaderStyle  Width="10px" />                                           
                                        </asp:TemplateField>  
                                        <asp:TemplateField HeaderText="Current <br /> month <br /> sales">
                                            <ItemTemplate>
                                                <asp:Label ID="lblcurrentmonthsales" Text='<%#  Bind("currmonth_sales") %>' SkinID="GridViewLabel"
                                                    runat="server"></asp:Label>
                                            </ItemTemplate>
                                             <ItemStyle Width="10px" />
                                            <HeaderStyle  Width="10px" />                                           
                                        </asp:TemplateField>                            
                                        <asp:TemplateField HeaderText="Ordered <br /> Qty By <br /> Branch">
                                            <ItemTemplate>
                                                <asp:Label ID="lblOrderQty" Text='<%#  Bind("order_qty") %>' SkinID="GridViewLabel"
                                                    runat="server"></asp:Label>
                                            </ItemTemplate>
                                            <ItemStyle Width="10px" />
                                            <HeaderStyle  Width="10px" />                                           
                                        </asp:TemplateField>               
                                        <asp:TemplateField HeaderText="STDN <br /> Quantity">
                                            <ItemTemplate>
                                               <asp:TextBox ID="txtSTDNQuantity" Text='<%#  Bind("stdn_qty") %>' SkinID="GridViewTextBox" runat="server" Enabled = "false"></asp:TextBox>
                                            </ItemTemplate>
                                           <ItemStyle Width="5px" />
                                            <HeaderStyle  Width="5px" />                                          
                                        </asp:TemplateField>        
                                        <asp:TemplateField HeaderText="STDN <br /> Branch">
                                            <ItemTemplate>
                                                <asp:Label ID="lblSTDNBranch" Text='<%#  Bind("stdn_branch") %>' SkinID="GridViewLabel"
                                                    runat="server"></asp:Label>
                                            </ItemTemplate>
                                             <ItemStyle Width="7px" />
                                            <HeaderStyle  Width="7px" />                                           
                                        </asp:TemplateField>
                                        <asp:TemplateField HeaderText="HO <br /> Accepted">                                           
                                            <ItemStyle Width="7px" />
                                            <HeaderStyle  Width="7px" />
                                            <ItemTemplate>
                                                <asp:TextBox ID="txtEditHOAccepted" SkinID="GridViewTextBox" runat="server" onblur="return CheckQty(this.id)" Text='<%# Bind("indent_qty") %>'></asp:TextBox>                                               
                                                <asp:RegularExpressionValidator Display="Dynamic" ControlToValidate="txtEditHOAccepted"
                                                    ErrorMessage="HO accepted qty must be numeric" ID="regMaxValEdit" runat="server"
                                                    SetFocusOnError="true" ValidationGroup="DescEditGroup" SkinID="GridViewLabelError"
                                                ValidationExpression="^[0-9]\d*(\.\d+)?$"></asp:RegularExpressionValidator>                                               
                                            </ItemTemplate>                                                                                                                                                                                                               
                                        </asp:TemplateField>                                                   
                                        <asp:TemplateField HeaderText="HO Unit <br /> Price">
                                            <ItemTemplate>
                                                <asp:Label ID="lblHOUnitPrice" Text='<%#  Bind("cost_price1") %>' SkinID="GridViewLabel"
                                                    runat="server"></asp:Label>
                                            </ItemTemplate>
                                             <ItemStyle Width="7px" />
                                            <HeaderStyle  Width="7px" />                                                                                   
                                        </asp:TemplateField>
                                        <asp:TemplateField HeaderText="Stock <br /> Aging (in <br /> months)">
                                            <ItemTemplate>
                                                <asp:Label ID="lblStockaging" Text='<%#  Bind("aging") %>' SkinID="GridViewLabel"
                                                    runat="server"></asp:Label>
                                            </ItemTemplate>
                                             <ItemStyle Width="7px" />
                                            <HeaderStyle  Width="7px" />                                           
                                        </asp:TemplateField>   
                                        <asp:TemplateField HeaderText="Pack <br /> Qty">
                                            <ItemTemplate>
                                                <asp:Label ID="lblPackQty" Text='<%#  Bind("Packing_quantity") %>' SkinID="GridViewLabel"
                                                    runat="server"></asp:Label>
                                            </ItemTemplate>
                                            <ItemStyle Width="7px" />
                                            <HeaderStyle  Width="7px" />                                           
                                        </asp:TemplateField>                                                                               
                                        <asp:TemplateField HeaderText="">
                                            <ItemTemplate>
                                                <asp:HiddenField ID="lblItemCode" runat="server" value='<%#Bind("item_code") %>' />
                                            </ItemTemplate>                                                                                     
                                        </asp:TemplateField>  
                                        <asp:TemplateField HeaderText="">
                                        <ItemTemplate>
                                                <asp:HiddenField ID="hdngrnno" runat="server" Value='<%#Bind("Inward_number") %>' />
                                        </ItemTemplate> 
                                        </asp:TemplateField> 
                                        <asp:TemplateField HeaderText="">
                                         <ItemTemplate>
                                                <asp:HiddenField ID="hdngrsrno" runat="server" Value='<%#Bind("consignment_sr_no") %>' />
                                        </ItemTemplate> 
                                        </asp:TemplateField>                                                                                                                                
                                    </Columns>
                                </asp:GridView>
                            </div>
                        </td>
                    </tr>
                </table>                   
             </ContentTemplate>                             
        </asp:UpdatePanel>         
       <div class="transactionButtons">
        <asp:UpdatePanel ID="UpdatePanel1" runat="server" UpdateMode="Conditional">
            <ContentTemplate>
                <div class="transactionButtonsHolder">
                    <asp:Button ID="BtnSubmit" runat="server" Text="Process" SkinID="ButtonNormal" TabIndex="6" OnClick="BtnSubmit_Click" />
                    <asp:Button ID="Button1" runat="server" Text="Reset" SkinID="ButtonNormal" TabIndex="7" OnClick="btnReset_Click" />                    
                </div>
            </ContentTemplate>                     
        </asp:UpdatePanel>         
        </div>
     </div>        
</asp:Content>

