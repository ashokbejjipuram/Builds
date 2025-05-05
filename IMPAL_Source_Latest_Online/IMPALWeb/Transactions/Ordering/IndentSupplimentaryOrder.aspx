<%@ Page Language="C#" MasterPageFile="~/Main.Master" AutoEventWireup="true" CodeBehind="IndentSupplimentaryOrder.aspx.cs"
    Inherits="IMPALWeb.Ordering.IndentSupplimentaryOrder" %>

<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="ajaxToolkit" %>
<asp:Content ID="Content3" ContentPlaceHolderID="CPHDetails" runat="server">

    <script src="../../Javascript/DirectPOcommon.js" type="text/javascript"></script>

    <script type="text/javascript">
        function pageLoad(sender, args) {

            iRowCount = document.getElementById(CtrlIdPrefix + "hdnFreezeRowCnt").value.trim();
            //gridViewFixedHeader(gridViewID, gridViewWidth, gridViewHeight)
            if (parseInt(iRowCount) > 0)
                gridViewFixedHeader('<%=GridView1.ClientID%>', '1000', '200');
        }        
    </script>

    <div id="DivTop" runat="server">
        <asp:UpdatePanel ID="UpdatePanel1" runat="server" UpdateMode="Conditional">
            <ContentTemplate>
                <div id="DivOuter" runat="server">
                    <div>
                        <div class="subFormTitle subFormTitleExtender250">
                            INDENT/SUPPLIMENTARY ORDER</div>
                        <table class="subFormTable">
                            <tr>
                                <td class="label">
                                    <asp:Label ID="lblHeaderMessage" runat="server" Text="Number" SkinID="LabelNormal"></asp:Label>
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
                                    <asp:Label ID="lblOrdIndentDate" runat="server" SkinID="LabelNormal" Text="Indent date"></asp:Label>
                                </td>
                                <td class="inputcontrols">
                                    <asp:TextBox ID="txtOrdIndentDate" SkinID="TextBoxDisabled" ReadOnly="true" runat="server"
                                        TabIndex="3"></asp:TextBox>
                                </td>
                                <td class="label">
                                    &nbsp;
                                </td>
                                <td class="inputcontrols">
                                    &nbsp;
                                </td>
                            </tr>
                            <tr>
                                <td class="label">
                                    <asp:Label ID="lblOrdTransactionType" runat="server" SkinID="LabelNormal" Text="Transaction type"></asp:Label>
                                    <span class="asterix">*</span>
                                </td>
                                <td class="inputcontrols">
                                    <asp:DropDownList ID="ddlOrdTransactionType" runat="server" AutoPostBack="True" SkinID="DropDownListNormal"
                                        OnSelectedIndexChanged="ddlOrdTransactionType_SelectedIndexChanged" TabIndex="3">
                                    </asp:DropDownList>
                                </td>
                                <td class="label">
                                    <asp:Label ID="lblOrdBranch" runat="server" SkinID="LabelNormal" Text="Branch"></asp:Label>
                                </td>
                                <td class="inputcontrols">
                                    <asp:TextBox ID="txtOrdBranch" runat="server" ReadOnly="true" SkinID="TextBoxDisabled"
                                        TabIndex="5"></asp:TextBox>
                                    <asp:DropDownList ID="ddlOrdBranchName" runat="server" AutoPostBack="true" SkinID="DropDownListNormal"
                                        OnSelectedIndexChanged="ddlOrdBranchName_SelectedIndexChanged">
                                    </asp:DropDownList>
                                </td>
                                <td class="label">
                                    &nbsp;
                                </td>
                                <td class="inputcontrols">
                                    &nbsp;
                                </td>
                            </tr>
                            <tr>
                                <td class="label">
                                    <asp:Label ID="lblOrdSupplier" runat="server" SkinID="LabelNormal" Text="Supplier"></asp:Label>
                                    <span class="asterix">*</span>
                                </td>
                                <td class="inputcontrols">
                                    <asp:DropDownList ID="ddlOrdSupplier" runat="server" AutoPostBack="True" SkinID="DropDownListNormal"
                                        OnSelectedIndexChanged="ddlOrdSupplier_SelectedIndexChanged" TabIndex="4">
                                    </asp:DropDownList>
                                </td>
                                <td class="label">
                                    <asp:Label ID="lblCustomer" SkinID="LabelNormal" runat="server" Text="Customer"></asp:Label>
                                    <span id="reqCustomer" runat="server" class="asterix">*</span>
                                </td>
                                <td class="inputcontrols">
                                    <asp:DropDownList ID="ddlOrdCustomer" runat="server" AutoPostBack="True" SkinID="DropDownListNormal"
                                        OnSelectedIndexChanged="ddlOrdCustomer_SelectedIndexChanged" TabIndex="5">
                                    </asp:DropDownList>
                                </td>
                                <td class="label">
                                    &nbsp;
                                </td>
                                <td class="inputcontrols">
                                    &nbsp;
                                </td>
                            </tr>
                        </table>
                        <div class="subFormTitle">
                            INDENT REFERENCE</div>
                        <table class="subFormTable">
                            <tr>
                                <td class="label">
                                    <asp:Label ID="lblOrdIndentNumber" SkinID="LabelNormal" runat="server" Text="Number"></asp:Label>
                                </td>
                                <td class="inputcontrols">
                                    <asp:TextBox ID="txtOrdRefIndentNumber" SkinID="TextBoxNormal" runat="server"
                                        MaxLength="40" TabIndex="6"></asp:TextBox>
                                </td>
                                <td class="label">
                                    <asp:Label ID="lblOrdRefIndentDate" SkinID="LabelNormal" runat="server" Text="Date"></asp:Label>
                                </td>
                                <td class="inputcontrols">
                                    <asp:TextBox ID="txtOrdRefIndentDate" onblur="return CheckValidDate(this.id, true, 'Order Reference indent Date');"
                                        SkinID="TextBoxNormal" runat="server" TabIndex="7"></asp:TextBox>
                                    <%--<asp:ImageButton ID="ImgRefIndDateCalendar" ImageUrl="~/Images/Calendar.png" alt="Calendar"
                                        Width="18" Height="18" runat="server" SkinID="ImageButtonCalendar" />--%>
                                    <ajaxToolkit:CalendarExtender ID="RefIndDateCalendarExtender" EnableViewState="true"
                                        PopupPosition="BottomLeft" PopupButtonID="ImgRefIndDateCalendar" TargetControlID="txtOrdRefIndentDate"
                                        Format="dd/MM/yyyy" runat="server">
                                    </ajaxToolkit:CalendarExtender>
                                </td>
                                <td class="label">
                                    &nbsp;
                                </td>
                                <td class="inputcontrols">
                                    &nbsp;
                                </td>
                            </tr>
                        </table>
                        <div class="subFormTitle">
                            ROAD PERMIT</div>
                        <table class="subFormTable">
                            <tr>
                                <td class="label">
                                    <asp:Label ID="lblOrdRdPermitNumber" SkinID="LabelNormal" runat="server" Text="Number"></asp:Label>
                                </td>
                                <td class="inputcontrols">
                                    <asp:TextBox ID="txtOrdRdPermitNumber" SkinID="TextBoxNormal" runat="server" MaxLength="40"
                                        TabIndex="8"></asp:TextBox>
                                </td>
                                <td class="label">
                                    <asp:Label ID="lblOrdRdPermitDate" runat="server" SkinID="LabelNormal" Text="Date"></asp:Label>
                                </td>
                                <td class="inputcontrols">
                                    <asp:TextBox ID="txtOrdRdPermitDate" runat="server" onblur="return CheckValidDate(this.id, true, 'Order Road Permit Date');"
                                        SkinID="TextBoxNormal" TabIndex="9"></asp:TextBox>
                                    <ajaxToolkit:CalendarExtender ID="RefRdPermitDateCalendarExtender" PopupPosition="BottomLeft"
                                        EnableViewState="true" TargetControlID="txtOrdRdPermitDate" PopupButtonID="ImgRdPermitDate"
                                        Format="dd/MM/yyyy" runat="server">
                                    </ajaxToolkit:CalendarExtender>
                                    <%--<asp:ImageButton ID="ImgRdPermitDate" ImageUrl="~/Images/Calendar.png" alt="Calender"
                                        Width="18" Height="18" runat="server" SkinID="ImageButtonCalendar" />--%>
                                </td>
                                <td class="label">
                                    &nbsp;
                                </td>
                                <td class="inputcontrols">
                                    &nbsp;
                                </td>
                            </tr>
                        </table>
                        <div class="subFormTitle">
                            CARRIER INFORMATION</div>
                        <table class="subFormTable">
                            <tr>
                                <td class="label">
                                    <asp:Label ID="lblOrdCarrier" runat="server" SkinID="LabelNormal" Text="Carrier"></asp:Label>
                                </td>
                                <td class="inputcontrols">
                                    <asp:TextBox ID="txtOrdCarrier" SkinID="TextBoxNormal" runat="server" MaxLength="40"
                                        TabIndex="10"></asp:TextBox>
                                </td>
                                <td class="label">
                                    <asp:Label ID="lblOrdDestination" SkinID="LabelNormal" runat="server" Text="Destination"></asp:Label>
                                </td>
                                <td class="inputcontrols">
                                    <asp:TextBox ID="txtOrdDestination" SkinID="TextBoxNormal" runat="server" MaxLength="40"
                                        TabIndex="11"></asp:TextBox>
                                </td>
                                <td class="label">
                                    &nbsp;
                                </td>
                                <td class="inputcontrols">
                                    &nbsp;
                                </td>
                            </tr>
                            <tr>
                                <td class="label">
                                    <asp:Label ID="lblOrdCarrierInfoRemarks" SkinID="LabelNormal" runat="server" Text="Remarks"></asp:Label>
                                </td>
                                <td class="inputcontrols">
                                    <asp:TextBox ID="txtOrdCarrierInfoRemarks" MaxLength="200" SkinID="TextBoxNormal"
                                        TabIndex="12" runat="server"></asp:TextBox>
                                </td>
                                <td class="label">
                                    <asp:Label ID="lblStatus" runat="server" SkinID="LabelNormal" Text="Status" TabIndex="1"></asp:Label>
                                </td>
                                <td class="inputcontrols">
                                    <asp:DropDownList ID="ddlOrdStatus" SkinID="DropDownListNormal" runat="server"
                                        AutoPostBack="True" TabIndex="13" OnSelectedIndexChanged="ddlOrdStatus_SelectedIndexChanged">
                                        <asp:ListItem Value="A" Text="Active"></asp:ListItem>
                                        <asp:ListItem Value="I" Text="InActive"></asp:ListItem>
                                    </asp:DropDownList>
                                </td>
                                <td class="label">
                                    &nbsp;
                                </td>
                                <td class="inputcontrols">
                                    &nbsp;
                                </td>
                            </tr>
                        </table>
                        <div class="subFormTitle">
                            CUSTOMER INFORMATION</div>
                        <table class="subFormTable">
                            <tr>
                                <td class="label">
                                    <asp:Label ID="lblOrdCustomerCode" SkinID="LabelNormal" runat="server" Text="Customer Code"></asp:Label>
                                </td>
                                <td class="inputcontrols">
                                    <asp:TextBox ID="txtOrdCustomerCode" SkinID="TextBoxDisabled" ReadOnly="true"
                                        runat="server"></asp:TextBox>
                                </td>
                                <td class="label">
                                    <asp:Label ID="lblOrdCustomerAddress1" SkinID="LabelNormal" runat="server" Text="Address1"></asp:Label>
                                </td>
                                <td class="inputcontrols">
                                    <asp:TextBox ID="txtOrdCustomerAddress1" SkinID="TextBoxDisabled" ReadOnly="true"
                                        runat="server"></asp:TextBox>
                                </td>
                                <td class="label">
                                    &nbsp;
                                </td>
                                <td class="inputcontrols">
                                    &nbsp;
                                </td>
                            </tr>
                            <tr>
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
                                <td class="label">
                                    &nbsp;
                                </td>
                                <td class="inputcontrols">
                                    &nbsp;
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
                                <td class="label">
                                    &nbsp;
                                </td>
                                <td class="inputcontrols">
                                    &nbsp;
                                </td>
                            </tr>
                        </table>
                    </div>
                </div>
            </ContentTemplate>
            <Triggers>
                <asp:AsyncPostBackTrigger ControlID="BtnSubmit" EventName="Click" />
                <asp:AsyncPostBackTrigger ControlID="btnReset" EventName="Click" />
            </Triggers>
        </asp:UpdatePanel>
        <div id="divItemDetails" runat="server">
            <div class="subFormTitle">
                ITEM DETAILS</div>
            <asp:UpdatePanel ID="UpdPanelGrid" runat="server" UpdateMode="Conditional">
                <ContentTemplate>
                    <div class="gridViewScrollFullPage"  style="overflow: auto; -ms-overflow-y: hidden; margin-left: 20px;
                                    width: 1024px; z-index: 1040;">
                        <asp:GridView ID="GridView1" runat="server" ShowFooter="True" AutoGenerateColumns="False"
                            OnRowDeleting="GridView1_RowDeleting" OnRowDataBound="GridView1_RowDataBound"
                            SkinID="GridViewScroll">
                            <EmptyDataTemplate>
                                <asp:Label ID="lblEmptySearch" SkinID="GridViewLabel" runat="server">No Results Found</asp:Label>
                            </EmptyDataTemplate>
                            <Columns>
                                <asp:BoundField DataField="S_No" HeaderText="S.No" Visible="false" />
                                <asp:TemplateField HeaderText="S.No">
                                    <ItemTemplate>
                                        <%# Container.DataItemIndex + 1 %>
                                    </ItemTemplate>
                                </asp:TemplateField>
                                <asp:TemplateField HeaderText="Supplier Part Number">
                                    <ItemTemplate>
                                        <asp:Label ID="lblgrdSupplier_Part_Number" SkinID="GridViewLabel" runat="server"></asp:Label>
                                        <div class="itemResetHolder">
                                            <div class="itemReset">
                                                <asp:TextBox ID="txtOrdSupplierPartNo" SkinID="GridViewTextBox" TabIndex="14" runat="server" />
                                                <asp:DropDownList ID="ddlSupplierPartNumber" runat="server" Visible="false" TabIndex="16"
                                                    AutoPostBack="True" SkinID="GridViewDropDownListFooter" OnSelectedIndexChanged="ddlSupplierPartNumber_SelectedIndexChanged">
                                                </asp:DropDownList>
                                            </div>
                                            <div class="itemResetButton">
                                                <asp:Button ID="btnSearch" SkinID="ButtonNormal" runat="server" TabIndex="15" Text="Search"
                                                    OnClick="btnSearch_Click" />
                                            </div>
                                        </div>
                                        <asp:Image ID="imgViewSupplierLine" ImageUrl="~/Images/ifind.png" runat="server"
                                            SkinID="GridViewImageEdit" ImageAlign="Right" />
                                        <ajaxToolkit:HoverMenuExtender ID="hme2" runat="Server" TargetControlID="imgViewSupplierLine"
                                            PopupControlID="PnlViewSupplierDetails" HoverCssClass="popupHover" PopupPosition="Right"
                                            OffsetX="0" OffsetY="0" PopDelay="50" />
                                        <asp:Panel ID="PnlViewSupplierDetails" Style="z-index: 101;" runat="server">
                                            <table id="tblviewdeails" border="1" width="200px">
                                                <tr>
                                                    <td>
                                                        <asp:Label ID="lblSupplier_Line" SkinID="GridViewLabel" Text="Supplier Line" runat="server"></asp:Label>
                                                    </td>
                                                    <td>
                                                        <asp:Label ID="lblSupplier_Line_Value" SkinID="GridViewLabel" runat="server"></asp:Label>
                                                    </td>
                                                </tr>
                                                <tr>
                                                    <td>
                                                        <asp:Label ID="lblApplication_Segment" SkinID="GridViewLabel" Text="Application Segment"
                                                            runat="server"></asp:Label>
                                                    </td>
                                                    <td>
                                                        <asp:Label ID="lblApplication_Segment_Value" SkinID="GridViewLabel" runat="server"></asp:Label>
                                                    </td>
                                                </tr>
                                                <tr>
                                                    <td>
                                                        <asp:Label ID="lblVehichleTypeDescription" SkinID="GridViewLabel" Text="Vehichle Type Description"
                                                            runat="server"></asp:Label>
                                                    </td>
                                                    <td>
                                                        <asp:Label ID="lblVehichleTypeDescriptionValue" SkinID="GridViewLabel" runat="server"></asp:Label>
                                                    </td>
                                                </tr>
                                            </table>
                                        </asp:Panel>
                                    </ItemTemplate>
                                    <FooterTemplate>
                                        <asp:Button ID="btnAdd" ValidationGroup="btnAdd" SkinID="ButtonNormal" runat="server"
                                            CausesValidation="true" CommandName="Add" Text="Add" TabIndex="23" OnClick="btnAdd_Click" />
                                    </FooterTemplate>
                                </asp:TemplateField>
                                <asp:TemplateField HeaderText="Item Code" Visible="false">
                                    <ItemTemplate>
                                        <asp:TextBox ID="txtOrdItemCode" ReadOnly="true" SkinID="GridViewTextBox" runat="server" />
                                        <asp:Label ID="lblOrdItemCode" SkinID="GridViewLabel" runat="server"></asp:Label>
                                    </ItemTemplate>
                                    <ItemStyle HorizontalAlign="Center" />
                                </asp:TemplateField>
                                <asp:TemplateField HeaderText="Application Segment Code" Visible="false">
                                    <ItemTemplate>
                                        <asp:TextBox ID="txtOrdApplicationSegmentCode" ReadOnly="true" SkinID="GridViewTextBoxSmall"
                                            runat="server" />
                                        <asp:Label ID="lblOrdApplicationSegmentCode" SkinID="GridViewLabel" runat="server"></asp:Label>
                                    </ItemTemplate>
                                    <ItemStyle HorizontalAlign="Center" />
                                </asp:TemplateField>
                                <asp:TemplateField HeaderText="Vehicle Type Code" Visible="false">
                                    <ItemTemplate>
                                        <asp:TextBox ID="txtOrdVehicleTypeCode" ReadOnly="true" SkinID="GridViewTextBox"
                                            runat="server" />
                                        <asp:Label ID="lblOrdVehicleTypeCode" SkinID="GridViewLabel" runat="server"></asp:Label>
                                    </ItemTemplate>
                                    <ItemStyle HorizontalAlign="Center" />
                                </asp:TemplateField>
                                <asp:TemplateField HeaderText="Packing Quantity">
                                    <ItemTemplate>
                                        <asp:TextBox ID="txtOrdPackingQuantity" ReadOnly="true" SkinID="GridViewTextBoxSmall"
                                            runat="server" />
                                        <asp:Label ID="lblOrdPackingQuantity" SkinID="GridViewLabel" runat="server"></asp:Label>
                                    </ItemTemplate>
                                    <ItemStyle HorizontalAlign="Center" />
                                </asp:TemplateField>
                                <asp:TemplateField HeaderText="Order Quantity">
                                    <ItemTemplate>
                                        <asp:Label ID="lblgrdOrderItem_PO_Quantity" SkinID="GridViewLabel" runat="server"></asp:Label>
                                        <asp:TextBox ID="txtOrderItem_PO_Quantity" MaxLength="6" onkeypress="return IntegerValueOnly();"
                                            TabIndex="17" SkinID="GridViewTextBoxSmall" runat="server" />
                                    </ItemTemplate>
                                    <ItemStyle HorizontalAlign="Center" />
                                </asp:TemplateField>
                                <asp:TemplateField HeaderText="Order Status" Visible="false">
                                    <ItemTemplate>
                                        <asp:Label ID="lblgrdOrderItem_Status" SkinID="GridViewLabel" runat="server"></asp:Label>
                                        <asp:DropDownList ID="ddlOrdItemStatus" runat="server" TabIndex="18" SkinID="GridViewDropDownListFooter">
                                            <asp:ListItem Value="A" Text="Active"></asp:ListItem>
                                            <asp:ListItem Value="I" Text="InActive"></asp:ListItem>
                                        </asp:DropDownList>
                                    </ItemTemplate>
                                    <ItemStyle HorizontalAlign="Center" />
                                </asp:TemplateField>
                                <asp:TemplateField HeaderText="Schedule Month">
                                    <ItemTemplate>
                                        <asp:Label ID="lblgrdSchedule_Date" SkinID="GridViewLabel" runat="server"></asp:Label>
                                        <asp:TextBox ID="txtSchedule_Date" ReadOnly="true" SkinID="GridViewTextBox" TabIndex="-1"
                                            runat="server" />
                                    </ItemTemplate>
                                    <ItemStyle HorizontalAlign="Center" />
                                </asp:TemplateField>
                                <asp:TemplateField HeaderText="Valid Days" Visible="false">
                                    <ItemTemplate>
                                        <asp:Label ID="lblgrdValid_Days" SkinID="GridViewLabel" runat="server"></asp:Label>
                                        <asp:TextBox ID="txtValid_Days" MaxLength="2" onkeypress="return IntegerValueOnly();"
                                            TabIndex="19" SkinID="GridViewTextBoxSmall" runat="server" />
                                    </ItemTemplate>
                                    <ItemStyle HorizontalAlign="Center" />
                                </asp:TemplateField>
                                <asp:TemplateField HeaderText="Schedule Status" Visible="false">
                                    <ItemTemplate>
                                        <asp:Label ID="lblgrdSchedule_Status" SkinID="GridViewLabel" runat="server"></asp:Label>
                                        <asp:DropDownList ID="ddlOrdScheduleStatus" runat="server" TabIndex="20" SkinID="GridViewDropDownList">
                                            <asp:ListItem Value="A" Text="Active"></asp:ListItem>
                                        </asp:DropDownList>
                                    </ItemTemplate>
                                    <ItemStyle HorizontalAlign="Center" />
                                </asp:TemplateField>
                                <asp:TemplateField HeaderText="Indent Branch" Visible="false">
                                    <ItemTemplate>
                                        <asp:Label ID="lblgrdIndent_Branch" SkinID="GridViewLabel" runat="server"></asp:Label>
                                        <asp:DropDownList ID="ddlIndentBranch" SkinID="GridViewDropDownListFooter" TabIndex="21"
                                            runat="server">
                                        </asp:DropDownList>
                                    </ItemTemplate>
                                    <ItemStyle HorizontalAlign="Center" />
                                </asp:TemplateField>
                                <asp:TemplateField ShowHeader="False">
                                    <ItemTemplate>
                                        <asp:LinkButton ID="lnkDelete" runat="server" CausesValidation="False" TabIndex="22"
                                            CommandName="Delete" Text="Delete"></asp:LinkButton>
                                    </ItemTemplate>
                                </asp:TemplateField>
                            </Columns>
                        </asp:GridView>
                    </div>
                    <div class="transactionButtons">
                        <div class="transactionButtonsHolder">
                            <asp:Button ID="BtnSubmit" runat="server" ValidationGroup="BtnSubmit" OnClientClick="return funDirectPOBtnSubmitRow();"
                                SkinID="ButtonNormal" OnClick="BtnSubmit_Click" CausesValidation="true" Text="Submit" />
                            <asp:Button ID="btnReset" runat="server" OnClientClick="return resetAction();" CausesValidation="true"
                                SkinID="ButtonNormal" OnClick="btnReset_Click" Text="Reset" />
                        </div>
                    </div>
                    <input id="hdnRowCnt" type="hidden" runat="server" />
                    <input id="hdnFreezeRowCnt" type="hidden" runat="server" />
                    <div id="divReportPopUp" class="reportViewerHolder" style="width: 80%;">
                        <table style="width: 100%; height: 20px;" border="0">
                            <tr>
                                <td align="right" style="width: 90%">
                                    <asp:ImageButton ID="imgClose" runat="server" ImageUrl="~/images/iCloseBlack.png"
                                        OnClientClick="funCloseMEP();" />
                                </td>
                            </tr>
                        </table>
                    </div>
                    <div>
                        <asp:Button ID="imgPopupHidden" Style="display: none" runat="server" />
                        <ajaxToolkit:ModalPopupExtender ID="MPE_FYRecRcpt" CancelControlID="imgClose" runat="server"
                            TargetControlID="imgPopupHidden" PopupControlID="divReportPopUp" DropShadow="false"
                            BackgroundCssClass="modalBackground">
                        </ajaxToolkit:ModalPopupExtender>
                    </div>
                </ContentTemplate>
                <Triggers>
                    <asp:AsyncPostBackTrigger ControlID="ddlOrdSupplier" EventName="SelectedIndexChanged" />
                    <asp:AsyncPostBackTrigger ControlID="ddlOrd_PONumber" EventName="SelectedIndexChanged" />
                </Triggers>
            </asp:UpdatePanel>
        </div>
    </div>
</asp:Content>
