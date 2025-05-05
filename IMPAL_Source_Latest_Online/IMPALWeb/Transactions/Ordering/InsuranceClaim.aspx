<%@ Page Title="" Language="C#" MasterPageFile="~/Main.Master" AutoEventWireup="true"
    CodeBehind="InsuranceClaim.aspx.cs" Inherits="IMPALWeb.Ordering.InsuranceClaim" %>

<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="ajaxToolkit" %>
<asp:Content ID="cntInsuranceClaim" ContentPlaceHolderID="CPHDetails" runat="server">
    <div id="DivTop" runat="server">
        <asp:UpdatePanel ID="UpdpanelTop" runat="server">
            <ContentTemplate>
                <div id="divInsuranceClaim" runat="server">
                    <div class="subFormTitle">
                        INSURANCE CLAIM</div>
                    <table class="subFormTable">
                        <tr>
                            <td class="label">
                                <asp:Label ID="lblClaimNumber" runat="server" Text="Claim Number" SkinID="LabelNormal"></asp:Label>
                            </td>
                            <td class="inputcontrols">
                                <asp:TextBox ID="txtClaimNumber" runat="server" SkinID="TextBoxDisabled"></asp:TextBox>
                                <asp:DropDownList ID="ddlClaimNumber" runat="server" AutoPostBack="True" 
                                    SkinID="DropDownListNormal" 
                                    onselectedindexchanged="ddlClaimNumber_SelectedIndexChanged" >
                                </asp:DropDownList>
                                <asp:ImageButton ID="imgButtonQuery" ImageUrl="~/Images/ifind.png" alt="Query" runat="server" 
                                    SkinID="ImageButtonSearch" onclick="imgButtonQuery_Click"/>
                            </td>
                            <td class="label">
                                <asp:Label ID="lblReferenceDate" runat="server" Text="Reference Date" SkinID="LabelNormal"></asp:Label>
                                <span class="asterix">*</span>
                            </td>
                            <td class="inputcontrols">
                                <asp:TextBox ID="txtReferenceDate" runat="server" SkinID="TextBoxNormal" contentEditable="false" ></asp:TextBox>
                                <asp:ImageButton ID="imgBtnReferenceDate" ImageUrl="~/Images/Calendar.png" alt="Calendar" runat="server" SkinID="ImageButtonCalendar" />
                                <ajaxToolkit:CalendarExtender ID="calExtReferenceDate" EnableViewState="true"
                                    PopupPosition="BottomLeft" PopupButtonID="imgBtnReferenceDate" 
                                    TargetControlID="txtReferenceDate" Format="dd/MM/yyyy" runat="server">
                                </ajaxToolkit:CalendarExtender>
                            </td>
                            <td class="label">
                                <asp:Label ID="lblClaimType" runat="server" Text="Claim Type" SkinID="LabelNormal"></asp:Label>
                                <span class="asterix">*</span>
                            </td>
                            <td class="inputcontrols">
                                <asp:DropDownList ID="ddlClaimType" runat="server" AutoPostBack="true" SkinID="DropDownListNormal">
                                </asp:DropDownList>
                            </td>
                        </tr>
                        <tr>
                            <td class="label">
                                <asp:Label ID="lblSupplier" runat="server" Text="Supplier" SkinID="LabelNormal"></asp:Label>
                                <span class="asterix">*</span>
                            </td>
                            <td class="inputcontrols">
                                <asp:DropDownList ID="ddlSupplier" runat="server" AutoPostBack="true" SkinID="DropDownListNormal">
                                </asp:DropDownList>
                            </td>
                            <td class="label">
                                <asp:Label ID="lblClaimBranch" runat="server" Text="Claim Branch" SkinID="LabelNormal"></asp:Label>
                                <span class="asterix">*</span>
                            </td>
                            <td class="inputcontrols">
                                <asp:TextBox ID="txtClaimBranch" runat="server" SkinID="TextBoxDisabled" contentEditable="false" ></asp:TextBox>
                            </td>
                            <td class="label">
                                <asp:Label ID="lblInvoiceNumber" runat="server" Text="Invoice Number" SkinID="LabelNormal"></asp:Label>
                                <span class="asterix">*</span>
                            </td>
                            <td class="inputcontrols">
                                <asp:DropDownList ID="ddlInvoiceNumber" runat="server" AutoPostBack="true" SkinID="DropDownListNormal">
                                </asp:DropDownList>
                            </td>
                        </tr>
                        <tr>
                            <td class="label">
                                <asp:Label ID="lblInvoiceDate" runat="server" Text="Invoice Date" SkinID="LabelNormal"></asp:Label>
                                <span class="asterix">*</span>
                            </td>
                            <td class="inputcontrols">
                                <asp:TextBox ID="txtInvoiceDate" runat="server" SkinID="TextBoxCalendarExtenderNormal" contentEditable="false" ></asp:TextBox>
                                <asp:ImageButton ID="imgBtnInvoiceDate" ImageUrl="~/Images/Calendar.png" alt="Calendar" runat="server" SkinID="ImageButtonCalendar" />
                                <ajaxToolkit:CalendarExtender ID="calExtInvoiceDate" EnableViewState="true"
                                    PopupPosition="BottomLeft" PopupButtonID="imgBtnInvoiceDate" 
                                    TargetControlID="txtInvoiceDate" Format="dd/MM/yyyy" runat="server">
                                </ajaxToolkit:CalendarExtender>
                            </td>
                            <td class="label">
                                <asp:Label ID="lblInvoiceValue" runat="server" Text="Invoice Value" SkinID="LabelNormal"></asp:Label>
                                <span class="asterix">*</span>
                            </td>
                            <td class="inputcontrols">
                                <asp:TextBox ID="txtInvoiceValue" runat="server" SkinID="TextBoxDisabled" contentEditable="false" ></asp:TextBox>
                            </td>
                            <td class="label">
                                <asp:Label ID="lblClaimValue" runat="server" Text="Claim Value" SkinID="LabelNormal"></asp:Label>
                                <span class="asterix">*</span>
                            </td>
                            <td class="inputcontrols">
                                <asp:TextBox ID="txtClaimValue" runat="server" SkinID="TextBoxNormal"></asp:TextBox>
                            </td>
                        </tr>
                        <tr>
                            <td class="label">
                                <asp:Label ID="lblRemarks" runat="server" Text="Remarks" SkinID="LabelNormal"></asp:Label>
                            </td>
                            <td class="inputcontrols">
                                <asp:TextBox ID="txtRemarks" runat="server" SkinID="TextBoxNormal"></asp:TextBox>
                            </td>
                            <td class="label">
                                <asp:Label ID="lblReasons" runat="server" Text="Reasons" SkinID="LabelNormal"></asp:Label>
                            </td>
                            <td class="inputcontrols">
                                <asp:DropDownList ID="ddlReasons" runat="server" AutoPostBack="true" SkinID="DropDownListNormal">
                                </asp:DropDownList>
                            </td>
                            <td class="label">
                                <asp:Label ID="lblStatus" runat="server" Text="Status" SkinID="LabelNormal"></asp:Label>
                            </td>
                            <td class="inputcontrols">
                                <asp:DropDownList ID="ddlStatus" runat="server" AutoPostBack="true" SkinID="DropDownListNormal">
                                </asp:DropDownList>
                            </td>
                        </tr>
                    </table>
                    <div class="subFormTitle subFormTitleExtender300">
                        INSURANCE CARRIER INFORMATION</div>
                    <table class="subFormTable">
                        <tr>
                            <td class="label">
                                <asp:Label ID="lblLRNumber" runat="server" Text="LR Number" SkinID="LabelNormal"></asp:Label>
                            </td>
                            <td class="inputcontrols">
                                <asp:TextBox ID="txtLRNumber" runat="server" SkinID="TextBoxNormal"></asp:TextBox>
                            </td>
                            <td class="label">
                                <asp:Label ID="lblLRDate" runat="server" Text="LR Date" SkinID="LabelNormal"></asp:Label>
                            </td>
                            <td class="inputcontrols">
                                <asp:TextBox ID="txtLRDate" runat="server" SkinID="TextBoxNormal"></asp:TextBox>
                            </td>
                            <td class="label">
                            </td>
                            <td class="inputcontrols">
                            </td>
                        </tr>
                        <tr>
                            <td class="label">
                                <asp:Label ID="lblCarrier" runat="server" Text="Carrier" SkinID="LabelNormal"></asp:Label>
                            </td>
                            <td class="inputcontrols">
                                <asp:TextBox ID="txtCarrier" runat="server" SkinID="TextBoxNormal"></asp:TextBox>
                            </td>
                            <td class="label">
                                <asp:Label ID="lblDestination" runat="server" Text="Destination" SkinID="LabelNormal"></asp:Label>
                            </td>
                            <td class="inputcontrols">
                                <asp:TextBox ID="txtDestination" runat="server" SkinID="TextBoxNormal"></asp:TextBox>
                            </td>
                            <td class="label">
                            </td>
                            <td class="inputcontrols">
                            </td>
                        </tr>
                    </table>
                    <div class="subFormTitle subFormTitleExtender300">
                        INSURANCE SURVEYOR INFORMATION</div>
                    <table class="subFormTable">
                        <tr>
                            <td class="label">
                                <asp:Label ID="lblSurveyorName" runat="server" Text="Surveyor Name" SkinID="LabelNormal"></asp:Label>
                            </td>
                            <td class="inputcontrols">
                                <asp:TextBox ID="txtSurveyorName" runat="server" SkinID="TextBoxNormal"></asp:TextBox>
                            </td>
                            <td class="label">
                                <asp:Label ID="lblReportDate" runat="server" Text="Report Date" SkinID="LabelNormal"></asp:Label>
                            </td>
                            <td class="inputcontrols">
                                <asp:TextBox ID="txtReportDate" runat="server" SkinID="TextBoxNormal"></asp:TextBox>
                            </td>
                            <td class="label">
                            </td>
                            <td class="inputcontrols">
                            </td>
                        </tr>
                    </table>
                    <div class="subFormTitle">
                        CLAIM DATES</div>
                    <table class="subFormTable">
                        <tr>
                            <td class="label">
                                <asp:Label ID="lblSubmissionDate" runat="server" Text="Submission Date" SkinID="LabelNormal"></asp:Label>
                            </td>
                            <td class="inputcontrols">
                                <asp:TextBox ID="txtSubmissionDate" runat="server" SkinID="TextBoxNormal"></asp:TextBox>
                            </td>
                            <td class="label">
                                <asp:Label ID="lblSettlementDate" runat="server" Text="Settlement Date" SkinID="LabelNormal"></asp:Label>
                            </td>
                            <td class="inputcontrols">
                                <asp:TextBox ID="txtSettlementDate" runat="server" SkinID="TextBoxNormal"></asp:TextBox>
                            </td>
                            <td class="label">
                            </td>
                            <td class="inputcontrols">
                            </td>
                        </tr>
                    </table>
                    <div class="subFormTitle">
                        SETTLEMENT DETAILS</div>
                    <table class="subFormTable">
                        <tr>
                            <td class="label">
                                <asp:Label ID="lblSettlementAmount" runat="server" Text="Settlement Amount" SkinID="LabelNormal"></asp:Label>
                            </td>
                            <td class="inputcontrols">
                                <asp:TextBox ID="txtSettlementAmount" runat="server" SkinID="TextBoxNormal"></asp:TextBox>
                            </td>
                            <td class="label">
                                <asp:Label ID="lblSalvageToBeSent" runat="server" Text="Salvage To Be Sent" SkinID="LabelNormal"></asp:Label>
                            </td>
                            <td class="inputcontrols">
                                <asp:DropDownList ID="ddlSalvageToBeSent" runat="server" AutoPostBack="true" SkinID="DropDownListNormal">
                                </asp:DropDownList>
                            </td>
                            <td class="label">
                            </td>
                            <td class="inputcontrols">
                            </td>
                        </tr>
                    </table>
                    <div class="subFormTitle subFormTitleExtender250">
                        SALVAGE CARRIER INFORMATION</div>
                    <table class="subFormTable">
                        <tr>
                            <td class="label">
                                <asp:Label ID="lblLRNumberSCI" runat="server" Text="LR Number" SkinID="LabelNormal"></asp:Label>
                            </td>
                            <td class="inputcontrols">
                                <asp:TextBox ID="txtLRNumberSCI" runat="server" SkinID="TextBoxNormal"></asp:TextBox>
                            </td>
                            <td class="label">
                                <asp:Label ID="lblLRDateSCI" runat="server" Text="LR Date" SkinID="LabelNormal"></asp:Label>
                            </td>
                            <td class="inputcontrols">
                                <asp:TextBox ID="txtLRDateSCI" runat="server" SkinID="TextBoxNormal"></asp:TextBox>
                            </td>
                            <td class="label">
                            </td>
                            <td class="inputcontrols">
                            </td>
                        </tr>
                        <tr>
                            <td class="label">
                                <asp:Label ID="lblCarrierSCI" runat="server" Text="Carrier" SkinID="LabelNormal"></asp:Label>
                            </td>
                            <td class="inputcontrols">
                                <asp:TextBox ID="txtCarrierSCI" runat="server" SkinID="TextBoxNormal"></asp:TextBox>
                            </td>
                            <td class="label">
                                <asp:Label ID="lblDestinationSCI" runat="server" Text="Destination" SkinID="LabelNormal"></asp:Label>
                            </td>
                            <td class="inputcontrols">
                                <asp:TextBox ID="txtDestinationSCI" runat="server" SkinID="TextBoxNormal"></asp:TextBox>
                            </td>
                            <td class="label">
                            </td>
                            <td class="inputcontrols">
                            </td>
                        </tr>
                    </table>
                    <div id="divItemDetails" runat="server">
                        <div class="subFormTitle">
                            Claim Documents</div>
                        <asp:UpdatePanel ID="UpdPanelGrid" runat="server" UpdateMode="Conditional">
                            <ContentTemplate>
                                <div class="gridViewScroll">
                                    <asp:GridView ID="grvInsuranceClaim" runat="server" AutoGenerateColumns="False" 
                                        SkinID="GridViewTransaction" onrowdatabound="grvInsuranceClaim_RowDataBound" 
                                        onrowdeleting="grvInsuranceClaim_RowDeleting">                                        
                                        <EmptyDataTemplate>
                                            <asp:Label ID="lblEmptySearch" runat="server" SkinID="GridViewLabel">No Results Found</asp:Label>
                                        </EmptyDataTemplate>
                                        <Columns>
                                            <asp:BoundField DataField="SNo" HeaderText="S.No" />   
                                            <asp:TemplateField HeaderText="Column 1">
                                                <ItemTemplate>
                                                    <asp:TextBox ID="TextBox1" runat="server" SkinID="GridViewTextBox"></asp:TextBox>
                                                </ItemTemplate>
                                                <FooterTemplate></FooterTemplate>
                                            </asp:TemplateField> 
                                            <asp:TemplateField HeaderText="Column 2">
                                                <ItemTemplate>
                                                    <asp:TextBox ID="TextBox1" runat="server" SkinID="GridViewTextBox"></asp:TextBox>
                                                </ItemTemplate>
                                                <FooterTemplate></FooterTemplate>
                                            </asp:TemplateField> 
                                            <asp:TemplateField HeaderText="Column 3">
                                                <ItemTemplate>
                                                    <asp:TextBox ID="TextBox1" runat="server" SkinID="GridViewTextBox"></asp:TextBox>
                                                </ItemTemplate>
                                                <FooterTemplate></FooterTemplate>
                                            </asp:TemplateField> 
                                            <asp:TemplateField HeaderText="Column 4">
                                                <ItemTemplate>
                                                    <asp:TextBox ID="TextBox1" runat="server" SkinID="GridViewTextBox"></asp:TextBox>
                                                </ItemTemplate>
                                                <FooterTemplate></FooterTemplate>
                                            </asp:TemplateField> 
                                            <asp:TemplateField HeaderText="Column 5">
                                                <ItemTemplate>
                                                    <asp:TextBox ID="TextBox1" runat="server" SkinID="GridViewTextBox"></asp:TextBox>
                                                </ItemTemplate>
                                                <FooterTemplate></FooterTemplate>
                                            </asp:TemplateField>
                                        </Columns>
                                    </asp:GridView>
                                </div>                                
                                <asp:TextBox ID="txtHdnGridCtrls" runat="server" type="hidden" Visible="false"></asp:TextBox>
                                <input id="hdnRowCnt" type="hidden" runat="server" /> 
                            </ContentTemplate>
                        </asp:UpdatePanel>
                    </div>
                    <div class="transactionButtons">
                        <div class="transactionButtonsHolder">
                            <asp:Button ID="BtnSubmit" runat="server" Text="Submit" SkinID="ButtonNormal" 
                                onclick="BtnSubmit_Click" />
                            <asp:Button ID="btnReset" runat="server" Text="Reset" SkinID="ButtonNormal" 
                                onclick="btnReset_Click" />
                            <asp:Button ID="btnCancel" runat="server" Text="Cancel" SkinID="ButtonNormal" 
                                onclick="btnCancel_Click" />
                        </div>
                    </div>                    
                    <input id="hdnScreenMode" type="hidden" runat="server" />
                </div>
            </ContentTemplate>
        </asp:UpdatePanel>
    </div>
</asp:Content>
