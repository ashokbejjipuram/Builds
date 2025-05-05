<%@ Page Language="C#" AutoEventWireup="true" MasterPageFile="~/Main.Master" CodeBehind="SLBDetails.aspx.cs"
    Inherits="IMPALWeb.Masters.SLB.SLBDetails" %>

<%@ Register Src="~/UserControls/ItemCodePartNumber.ascx" TagName="ItemCodePartNumber"
    TagPrefix="UCItem" %>
<%@ Register Namespace="AjaxControlToolkit" Assembly="AjaxControlToolkit" TagPrefix="ajaxToolkit" %>
<asp:Content ID="CPHDetails" ContentPlaceHolderID="CPHDetails" runat="server">

    <script language="javascript" type="text/javascript">

        function fnValidateSLB(sender, args) {
            var vSLBCodeValue;
            var vSLBCod;
            var varraySLBCode = sender.controltovalidate.split("_");
            var cControl = sender.controltovalidate;
            vSLBCode = varraySLBCode[0] + "_" + varraySLBCode[1] + "_" + varraySLBCode[2] + "_" + varraySLBCode[3] + "_" + varraySLBCode[4] + "_lblSLBCode";
            if (document.getElementById(vSLBCode) == null) {
                vSLBCode = varraySLBCode[0] + "_" + varraySLBCode[1] + "_" + varraySLBCode[2] + "_" + varraySLBCode[3] + "_" + varraySLBCode[4] + "_ddlNewSLB";
                vSLBCodeValue = document.getElementById(vSLBCode).value;

            }

            else
                vSLBCodeValue = document.getElementById(vSLBCode).outerText;
            //            if (cControl.indexOf("txtNewOS") > 0 || cControl.indexOf("txtAddNewOS") > 0)
            //                vControlCaption = "New OS";
            //            else if (cControl.indexOf("txtOldOS") > 0 || cControl.indexOf("txtAddOldOS") > 0)
            //                vControlCaption = "Old OS";
            //            else if (cControl.indexOf("txtNewLS") > 0 || cControl.indexOf("txtAddNewLS") > 0)
            //                vControlCaption = "New LS";
            //            else if (cControl.indexOf("txtOldLS") > 0 || cControl.indexOf("txtAddOldLS") > 0)
            //                vControlCaption = "Old LS";
            //            else if (cControl.indexOf("txtNewFDO") > 0 || cControl.indexOf("txtAddNewFDO") > 0)
            //                vControlCaption = "New FDO";
            //            else if (cControl.indexOf("txtOldFDO") > 0 || cControl.indexOf("txtAddOldFDO") > 0)
            //                vControlCaption = "Old FDO";
            //            else if (cControl.indexOf("txtNewLRTransfer") > 0 || cControl.indexOf("txtAddNewLRTransfer") > 0)
            //                vControlCaption = "New LR Transfer";
            //            else if (cControl.indexOf("txtOldLRTransfer") > 0 || cControl.indexOf("txtAddOldLRTransfer") > 0)
            //                vControlCaption = "Old LR Tranfer";


            if (isNaN(args.Value)) {
                //sender.errormessage = vControlCaption + " value should be numeric";
                //sender.text = vControlCaption + " value should be numeric";
                //alert(vControlCaption + " value should be numeric");
                args.IsValid = false;

            }
            else {
                args.IsValid = true;
            }
        }


        function fnValidateSLBSecond(sender, args) {
            var vSLBCodeValue;
            var vSLBCod;
            var varraySLBCode = sender.controltovalidate.split("_");
            var cControl = sender.controltovalidate;
            vSLBCode = varraySLBCode[0] + "_" + varraySLBCode[1] + "_" + varraySLBCode[2] + "_" + varraySLBCode[3] + "_" + varraySLBCode[4] + "_lblSLBCode";
            if (document.getElementById(vSLBCode) == null) {
                vSLBCode = varraySLBCode[0] + "_" + varraySLBCode[1] + "_" + varraySLBCode[2] + "_" + varraySLBCode[3] + "_" + varraySLBCode[4] + "_ddlNewSLB";
                vSLBCodeValue = document.getElementById(vSLBCode).value;

            }

            else
                vSLBCodeValue = document.getElementById(vSLBCode).outerText;

            if (isNaN(args.Value)) {
                args.IsValid = true;
            }
            else if ((parseInt(vSLBCodeValue) <= 50) && ((parseFloat(args.Value) > 100) || (parseFloat(args.Value) < -100))) {
                //alert(vControlCaption + " value should be between -100 and 100");
                args.IsValid = false;
            }
            else {
                args.IsValid = true;
            }
        }

        function fnValidateSLBThird(sender, args) {
            var vSLBCodeValue;
            var vSLBCod;
            var varraySLBCode = sender.controltovalidate.split("_");
            var cControl = sender.controltovalidate;
            vSLBCode = varraySLBCode[0] + "_" + varraySLBCode[1] + "_" + varraySLBCode[2] + "_" + varraySLBCode[3] + "_" + varraySLBCode[4] + "_lblSLBCode";
            if (document.getElementById(vSLBCode) == null) {
                vSLBCode = varraySLBCode[0] + "_" + varraySLBCode[1] + "_" + varraySLBCode[2] + "_" + varraySLBCode[3] + "_" + varraySLBCode[4] + "_ddlNewSLB";
                vSLBCodeValue = document.getElementById(vSLBCode).value;

            }

            else
                vSLBCodeValue = document.getElementById(vSLBCode).outerText;

            if (isNaN(args.Value)) {
                args.IsValid = true;

            }
            else if ((parseInt(vSLBCodeValue) <= 50) && ((parseFloat(args.Value) > 100) || (parseFloat(args.Value) < -100))) {
                args.IsValid = true;
            }
            else if ((parseInt(vSLBCodeValue) > 50) && ((parseFloat(args.Value) <= 0))) {
                //alert(vControlCaption + " Value should be greater than zero");
                args.IsValid = false;
            }
            else {
                args.IsValid = true;
            }
        }

      
       
    </script>

    <div id="DivOuter">
        <div class="subFormTitle">
            SLB Detail</div>
        <asp:UpdatePanel ID="UpdatePanel1" runat="server">
            <ContentTemplate>
                <table class="subFormTable">
                    <tr>
                        <td class="label">
                            <asp:Label ID="lblSLB" Text="SLB" SkinID="LabelNormal" runat="server"></asp:Label>
                        </td>
                        <td class="inputcontrols">
                            <asp:DropDownList ID="ddlSLB" SkinID="DropDownListNormal" AutoPostBack="true" runat="server"
                                OnSelectedIndexChanged="ddlSLB_SelectedIndexChanged">
                            </asp:DropDownList>
                        </td>
                    </tr>
                    <tr>
                        <td class="label">
                            <asp:Label ID="lblBranch" Text="Branch" SkinID="LabelNormal" runat="server"></asp:Label>
                        </td>
                        <td class="inputcontrols">
                            <asp:DropDownList ID="ddlBranch" SkinID="DropDownListNormal" AutoPostBack="true"
                                runat="server" OnSelectedIndexChanged="ddlBranch_SelectedIndexChanged">
                            </asp:DropDownList>
                        </td>
                        <td class="label">
                        </td>
                        <td class="inputcontrols">
                        </td>
                        <td class="label">
                        </td>
                        <td class="inputcontrols">
                        </td>
                    </tr>
                    <tr>
                        <td class="label">
                            <asp:Label ID="lblSupplierPart" runat="server" SkinID="LabelNormal" Text="Supplier Part #"></asp:Label>
                            <span class="asterix">*</span>
                        </td>
                        <td class="inputcontrolsColSpan2">
                            <asp:TextBox ID="txtSupplierPartNo" SkinID="TextBoxNormal" runat="server"></asp:TextBox>
                        </td>
                        <td class="labelColSpan2">
                        <asp:Button ID="BtnSubmit" SkinID="ButtonNormal" runat="server" onclick="BtnSubmit_Click" 
                    Text="Go" />
                        </td>
                        <td class="inputcontrolsColSpan2">
                        
                        </td> 
                    </tr>  
                </table> 
         <Triggers>  
            <asp:AsyncPostBackTrigger ControlID="btnReset" EventName="Click" /> 
         </Triggers>   
                
                <div class="subFormTitle">
                    SLB Details</div>
                <div class="gridViewScrollFullPage">
                    <asp:GridView ID="GV_SBLDetail" runat="server" AllowPaging="True" ShowFooter="True"
                        AutoGenerateColumns="False" SkinID="GridViewScroll" OnPageIndexChanging="GV_SBLDetail_PageIndexChanging"
                        OnRowCancelingEdit="GV_SBLDetail_RowCancelingEdit" OnRowCreated="GV_SBLDetail_RowCreated"
                        OnRowEditing="GV_SBLDetail_RowEditing" OnRowUpdating="GV_SBLDetail_RowUpdating"
                        OnRowCommand="GV_SBLDetail_RowCommand">
                        <EmptyDataTemplate>
                            <asp:Label ID="lblEmptySearch" runat="server" SkinID="GridViewLabel">No Results Found</asp:Label>
                        </EmptyDataTemplate>
                        <Columns>
                            <asp:TemplateField HeaderText="">
                                <ItemTemplate>
                                    <asp:Label ID="lblSLBCode" runat="server" Text='<%# Bind("SLB_Code") %>' SkinID="GridViewLabel">
                                    </asp:Label>
                                </ItemTemplate>
                                <ControlStyle Width="0px" />
                                <FooterStyle Width="0px" />
                                <HeaderStyle Width="0px" />
                            </asp:TemplateField>
                            <asp:TemplateField HeaderText="SLB" SortExpression="SLB">
                                <ItemTemplate>
                                    <asp:Label ID="lblSLB" runat="server" Text='<%# Bind("SLB_Description") %>' SkinID="GridViewLabel">
                                    </asp:Label>
                                </ItemTemplate>
                                <FooterTemplate>
                                    <asp:DropDownList ID="ddlNewSLB" runat="server" SkinID="GridViewDropDownList">
                                    </asp:DropDownList>
                                    <asp:RequiredFieldValidator ID="rvSLB" Text="." InitialValue="" ForeColor="White"
                                        runat="server" ControlToValidate="ddlNewSLB" ErrorMessage="Select a valid SLB"
                                        ValidationGroup="BranchAddGroup" SkinID="GridViewLabelError">
                                    </asp:RequiredFieldValidator>
                                    <ajaxToolkit:ValidatorCalloutExtender ID="ValidatorCalloutExtender10" TargetControlID="rvSLB"
                                        PopupPosition="TopLeft" runat="server">
                                    </ajaxToolkit:ValidatorCalloutExtender>
                                </FooterTemplate>
                            </asp:TemplateField>
                            <asp:TemplateField Visible="false">
                                <ItemTemplate>
                                    <asp:Label ID="lblBranchCode" runat="server" Text='<%# Bind("Branch_Code") %>' SkinID="GridViewLabel">
                                    </asp:Label>
                                </ItemTemplate>
                            </asp:TemplateField>
                            <asp:TemplateField HeaderText="Branch" SortExpression="Branch_Name">
                                <ItemTemplate>
                                    <asp:Label ID="lblBranch" runat="server" Text='<%# Bind("Branch_Name") %>' SkinID="GridViewLabel">
                                    </asp:Label>
                                </ItemTemplate>
                                <FooterTemplate>
                                    <asp:DropDownList ID="ddlNewBranch" runat="server" SkinID="GridViewDropDownList">
                                    </asp:DropDownList>
                                    <asp:RequiredFieldValidator ID="rvNewSLB" Text="." InitialValue="0" ForeColor="White"
                                        runat="server" ControlToValidate="ddlNewBranch" ErrorMessage="Select a valid branch"
                                        ValidationGroup="BranchAddGroup" SkinID="GridViewLabelError">
                                    </asp:RequiredFieldValidator>
                                    <ajaxToolkit:ValidatorCalloutExtender ID="ValidatorCalloutExtender7" TargetControlID="rvNewSLB"
                                        PopupPosition="TopLeft" runat="server">
                                    </ajaxToolkit:ValidatorCalloutExtender>
                                </FooterTemplate>
                            </asp:TemplateField>
                            <asp:TemplateField HeaderText="Item Code">
                                <ItemTemplate>
                                    <asp:Label ID="lblItemCode" runat="server" Text='<%# Bind("Item_Code") %>' SkinID="GridViewLabel">
                                    </asp:Label>
                                </ItemTemplate>
                                <FooterTemplate>
                                    <asp:TextBox ID="txtItemCode" runat="server" SkinID="TextBoxDisabledBig" ReadOnly="True" Enabled="False" />
                                    <UCItem:ItemCodePartNumber runat="server" ID="ItemPopUp" Mode="3" SupplierType="1"
                                        Disable="false" OnSearchImageClicked="ItemPopUp_ImageClicked"></UCItem:ItemCodePartNumber>
                                    <asp:RequiredFieldValidator ID="rvNewItemCode" Text="." runat="server" ControlToValidate="txtItemCode"
                                        ErrorMessage="Please enter a valid Item code" ValidationGroup="BranchAddGroup"
                                        SkinID="GridViewLabelError">
                                    </asp:RequiredFieldValidator>
                                    <ajaxToolkit:ValidatorCalloutExtender ID="ValidatorCalloutExtender8" TargetControlID="rvNewItemCode"
                                        PopupPosition="TopLeft" runat="server">
                                    </ajaxToolkit:ValidatorCalloutExtender>
                                </FooterTemplate>
                            </asp:TemplateField>
                            <asp:TemplateField HeaderText="New OS">
                                <EditItemTemplate>
                                    <asp:TextBox ID="txtNewOS" runat="server" Text='<%#Eval("os_value", "{0:0.00}")%>'
                                        SkinID="GridViewTextBox">
                                    </asp:TextBox>
                                    <asp:RequiredFieldValidator ID="rvNewOS" runat="server" Text="." ControlToValidate="txtNewOS"
                                        ErrorMessage="Please enter a New OS" ValidationGroup="BranchEditGroup" SkinID="GridViewLabelError">
                                    </asp:RequiredFieldValidator>
                                    <ajaxToolkit:ValidatorCalloutExtender ID="VCE1" TargetControlID="rvNewOS" PopupPosition="TopLeft"
                                        runat="server">
                                    </ajaxToolkit:ValidatorCalloutExtender>
                                    <asp:CustomValidator ID="CVNewOS" Text="." ErrorMessage="New OS value should be numeric"
                                        runat="server" ControlToValidate="txtNewOS" SetFocusOnError="true" ValidationGroup="BranchEditGroup"
                                        SkinID="GridViewLabelError" EnableClientScript="true" ClientValidationFunction="fnValidateSLB"></asp:CustomValidator>
                                    <ajaxToolkit:ValidatorCalloutExtender ID="ValidatorCalloutExtender82" TargetControlID="CVNewOS"
                                        PopupPosition="TopLeft" runat="server">
                                    </ajaxToolkit:ValidatorCalloutExtender>
                                    <asp:CustomValidator ID="CustomValidator1" Text="." ErrorMessage="New OS value should be between -100 and 100"
                                        runat="server" ControlToValidate="txtNewOS" SetFocusOnError="true" ValidationGroup="BranchEditGroup"
                                        SkinID="GridViewLabelError" EnableClientScript="true" ClientValidationFunction="fnValidateSLBSecond"></asp:CustomValidator>
                                    <ajaxToolkit:ValidatorCalloutExtender ID="ValidatorCalloutExtender1" TargetControlID="CustomValidator1"
                                        PopupPosition="TopLeft" runat="server">
                                    </ajaxToolkit:ValidatorCalloutExtender>
                                    <asp:CustomValidator ID="CustomValidator2" Text="." ErrorMessage="New OS value should be greater than zero"
                                        runat="server" ControlToValidate="txtNewOS" SetFocusOnError="true" ValidationGroup="BranchEditGroup"
                                        SkinID="GridViewLabelError" EnableClientScript="true" ClientValidationFunction="fnValidateSLBThird"></asp:CustomValidator>
                                    <ajaxToolkit:ValidatorCalloutExtender ID="ValidatorCalloutExtender2" TargetControlID="CustomValidator2"
                                        PopupPosition="TopLeft" runat="server">
                                    </ajaxToolkit:ValidatorCalloutExtender>
                                </EditItemTemplate>
                                <FooterTemplate>
                                    <asp:TextBox ID="txtAddNewOS" runat="server" Text='<%#Eval("os_value", "{0:0.00}")%>'
                                        SkinID="GridViewTextBox">
                                    </asp:TextBox>
                                    <asp:RequiredFieldValidator ID="rvAddNewOS" Text="." runat="server" ControlToValidate="txtAddNewOS"
                                        ErrorMessage="Please enter a New OS" ValidationGroup="BranchAddGroup" SkinID="GridViewLabelError">
                                    </asp:RequiredFieldValidator>
                                    <ajaxToolkit:ValidatorCalloutExtender ID="VCE2" TargetControlID="rvAddNewOS" PopupPosition="TopLeft"
                                        runat="server">
                                    </ajaxToolkit:ValidatorCalloutExtender>
                                    <asp:CustomValidator ID="CVAddNewOS" Text="." ErrorMessage="New OS value should be numeric"
                                        runat="server" ControlToValidate="txtAddNewOS" SetFocusOnError="true" ValidationGroup="BranchAddGroup"
                                        SkinID="GridViewLabelError" EnableClientScript="true" ClientValidationFunction="fnValidateSLB"></asp:CustomValidator>
                                    <ajaxToolkit:ValidatorCalloutExtender ID="ValidatorCalloutExtender9" TargetControlID="CVAddNewOS"
                                        PopupPosition="TopLeft" runat="server">
                                    </ajaxToolkit:ValidatorCalloutExtender>
                                    <asp:CustomValidator ID="CustomValidator7" Text="." ErrorMessage="New OS value should be between -100 and 100"
                                        runat="server" ControlToValidate="txtAddNewOS" SetFocusOnError="true" ValidationGroup="BranchAddGroup"
                                        SkinID="GridViewLabelError" EnableClientScript="true" ClientValidationFunction="fnValidateSLBSecond"></asp:CustomValidator>
                                    <ajaxToolkit:ValidatorCalloutExtender ID="ValidatorCalloutExtender101" TargetControlID="CustomValidator7"
                                        PopupPosition="TopLeft" runat="server">
                                    </ajaxToolkit:ValidatorCalloutExtender>
                                    <asp:CustomValidator ID="CustomValidator8" Text="." ErrorMessage="New OS value should be greater than zero"
                                        runat="server" ControlToValidate="txtAddNewOS" SetFocusOnError="true" ValidationGroup="BranchAddGroup"
                                        SkinID="GridViewLabelError" EnableClientScript="true" ClientValidationFunction="fnValidateSLBThird"></asp:CustomValidator>
                                    <ajaxToolkit:ValidatorCalloutExtender ID="ValidatorCalloutExtender112" TargetControlID="CustomValidator8"
                                        PopupPosition="TopLeft" runat="server">
                                    </ajaxToolkit:ValidatorCalloutExtender>
                                </FooterTemplate>
                                <FooterStyle Wrap="false" />
                                <ItemTemplate>
                                    <asp:Label ID="lblNewOS" runat="server" Text='<%#Eval("os_value", "{0:0.00}")%>'
                                        SkinID="GridViewLabel">
                                    </asp:Label>
                                </ItemTemplate>
                            </asp:TemplateField>
                            <asp:TemplateField HeaderText="Old OS">
                                <EditItemTemplate>
                                    <asp:TextBox ID="txtOldOS" runat="server" Text='<%#Eval("old_os_value", "{0:0.00}")%>'
                                        SkinID="GridViewTextBox">
                                    </asp:TextBox>
                                    <asp:RequiredFieldValidator ID="rvOldOS" runat="server" Text="." ControlToValidate="txtOldOS"
                                        ErrorMessage="Please enter a Old OS" ValidationGroup="BranchEditGroup" SkinID="GridViewLabelError">
                                    </asp:RequiredFieldValidator>
                                    <ajaxToolkit:ValidatorCalloutExtender ID="VCE3" TargetControlID="rvOldOS" PopupPosition="TopLeft"
                                        runat="server">
                                    </ajaxToolkit:ValidatorCalloutExtender>
                                    <asp:CustomValidator ID="CVOldOS" Text="." ErrorMessage="Old OS value should be numeric"
                                        runat="server" ControlToValidate="txtOldOS" SetFocusOnError="true" ValidationGroup="BranchEditGroup"
                                        SkinID="GridViewLabelError" EnableClientScript="true" ClientValidationFunction="fnValidateSLB"></asp:CustomValidator>
                                    <ajaxToolkit:ValidatorCalloutExtender ID="ValidatorCalloutExtender8" TargetControlID="CVOldOS"
                                        PopupPosition="TopLeft" runat="server">
                                    </ajaxToolkit:ValidatorCalloutExtender>
                                    <asp:CustomValidator ID="CustomValidator3" Text="." ErrorMessage="Old OS value should be between -100 and 100"
                                        runat="server" ControlToValidate="txtOldOS" SetFocusOnError="true" ValidationGroup="BranchEditGroup"
                                        SkinID="GridViewLabelError" EnableClientScript="true" ClientValidationFunction="fnValidateSLBSecond"></asp:CustomValidator>
                                    <ajaxToolkit:ValidatorCalloutExtender ID="ValidatorCalloutExtender3" TargetControlID="CustomValidator3"
                                        PopupPosition="TopLeft" runat="server">
                                    </ajaxToolkit:ValidatorCalloutExtender>
                                    <asp:CustomValidator ID="CustomValidator4" Text="." ErrorMessage="Old OS value should be greater than zero"
                                        runat="server" ControlToValidate="txtOldOS" SetFocusOnError="true" ValidationGroup="BranchEditGroup"
                                        SkinID="GridViewLabelError" EnableClientScript="true" ClientValidationFunction="fnValidateSLBThird"></asp:CustomValidator>
                                    <ajaxToolkit:ValidatorCalloutExtender ID="ValidatorCalloutExtender4" TargetControlID="CustomValidator4"
                                        PopupPosition="TopLeft" runat="server">
                                    </ajaxToolkit:ValidatorCalloutExtender>
                                </EditItemTemplate>
                                <FooterTemplate>
                                    <asp:TextBox ID="txtAddOldOS" runat="server" Text='<%#Eval("old_os_value", "{0:0.00}")%>'
                                        SkinID="GridViewTextBox">
                                    </asp:TextBox>
                                    <asp:RequiredFieldValidator ID="rvAddOldOS" Text="." runat="server" ControlToValidate="txtAddOldOS"
                                        ErrorMessage="Please enter a Old OS" ValidationGroup="BranchAddGroup" SkinID="GridViewLabelError">
                                    </asp:RequiredFieldValidator>
                                    <ajaxToolkit:ValidatorCalloutExtender ID="VCE4" TargetControlID="rvAddOldOS" PopupPosition="TopLeft"
                                        runat="server">
                                    </ajaxToolkit:ValidatorCalloutExtender>
                                    <asp:CustomValidator ID="CVAddOldOS" Text="." ErrorMessage="Old OS value should be numeric"
                                        runat="server" ControlToValidate="txtAddOldOS" SetFocusOnError="true" ValidationGroup="BranchEditGroup"
                                        SkinID="GridViewLabelError" EnableClientScript="true" ClientValidationFunction="fnValidateSLB"></asp:CustomValidator>
                                    <ajaxToolkit:ValidatorCalloutExtender ID="ValidatorCalloutExtender88" TargetControlID="CVAddOldOS"
                                        PopupPosition="TopLeft" runat="server">
                                    </ajaxToolkit:ValidatorCalloutExtender>
                                    <asp:CustomValidator ID="CustomValidator5" Text="." ErrorMessage="Old OS value should be between -100 and 100"
                                        runat="server" ControlToValidate="txtAddOldOS" SetFocusOnError="true" ValidationGroup="BranchEditGroup"
                                        SkinID="GridViewLabelError" EnableClientScript="true" ClientValidationFunction="fnValidateSLBSecond"></asp:CustomValidator>
                                    <ajaxToolkit:ValidatorCalloutExtender ID="ValidatorCalloutExtender38" TargetControlID="CustomValidator5"
                                        PopupPosition="TopLeft" runat="server">
                                    </ajaxToolkit:ValidatorCalloutExtender>
                                    <asp:CustomValidator ID="CustomValidator6" Text="." ErrorMessage="Old OS value should be greater than zero"
                                        runat="server" ControlToValidate="txtAddOldOS" SetFocusOnError="true" ValidationGroup="BranchEditGroup"
                                        SkinID="GridViewLabelError" EnableClientScript="true" ClientValidationFunction="fnValidateSLBThird"></asp:CustomValidator>
                                    <ajaxToolkit:ValidatorCalloutExtender ID="ValidatorCalloutExtender48" TargetControlID="CustomValidator6"
                                        PopupPosition="TopLeft" runat="server">
                                    </ajaxToolkit:ValidatorCalloutExtender>
                                </FooterTemplate>
                                <FooterStyle Wrap="false" />
                                <ItemTemplate>
                                    <asp:Label ID="lblOldOS" runat="server" Text='<%#Eval("old_os_value", "{0:0.00}")%>'
                                        SkinID="GridViewLabel">
                                    </asp:Label>
                                </ItemTemplate>
                            </asp:TemplateField>
                            <asp:TemplateField HeaderText="New LS">
                                <EditItemTemplate>
                                    <asp:TextBox ID="txtNewLS" runat="server" Text='<%#Eval("ls_value", "{0:0.00}")%>'
                                        SkinID="GridViewTextBox">
                                    </asp:TextBox>
                                    <asp:RequiredFieldValidator ID="rvNewLS" runat="server" Text="." ControlToValidate="txtNewLS"
                                        ErrorMessage="Please enter a New LS" ValidationGroup="BranchEditGroup" SkinID="GridViewLabelError">
                                    </asp:RequiredFieldValidator>
                                    <ajaxToolkit:ValidatorCalloutExtender ID="VCE5" TargetControlID="rvNewLS" PopupPosition="TopLeft"
                                        runat="server">
                                    </ajaxToolkit:ValidatorCalloutExtender>
                                    <asp:CustomValidator ID="CVNewLS" Text="." ErrorMessage="New LS value should be numeric"
                                        runat="server" ControlToValidate="txtNewLS" SetFocusOnError="true" ValidationGroup="BranchEditGroup"
                                        SkinID="GridViewLabelError" EnableClientScript="true" ClientValidationFunction="fnValidateSLB"></asp:CustomValidator>
                                    <ajaxToolkit:ValidatorCalloutExtender ID="VC1" TargetControlID="CVNewLS" PopupPosition="TopLeft"
                                        runat="server">
                                    </ajaxToolkit:ValidatorCalloutExtender>
                                    <asp:CustomValidator ID="CustomValidator9" Text="." ErrorMessage="New LS value should be between -100 and 100"
                                        runat="server" ControlToValidate="txtNewLS" SetFocusOnError="true" ValidationGroup="BranchAddGroup"
                                        SkinID="GridViewLabelError" EnableClientScript="true" ClientValidationFunction="fnValidateSLBSecond"></asp:CustomValidator>
                                    <ajaxToolkit:ValidatorCalloutExtender ID="VC2" TargetControlID="CustomValidator9"
                                        PopupPosition="TopLeft" runat="server">
                                    </ajaxToolkit:ValidatorCalloutExtender>
                                    <asp:CustomValidator ID="CustomValidator10" Text="." ErrorMessage="New LS value should be greater than zero"
                                        runat="server" ControlToValidate="txtNewLS" SetFocusOnError="true" ValidationGroup="BranchAddGroup"
                                        SkinID="GridViewLabelError" EnableClientScript="true" ClientValidationFunction="fnValidateSLBThird"></asp:CustomValidator>
                                    <ajaxToolkit:ValidatorCalloutExtender ID="VC3" TargetControlID="CustomValidator10"
                                        PopupPosition="TopLeft" runat="server">
                                    </ajaxToolkit:ValidatorCalloutExtender>
                                </EditItemTemplate>
                                <FooterTemplate>
                                    <asp:TextBox ID="txtAddNewLS" runat="server" Text='<%#Eval("ls_value", "{0:0.00}")%>'
                                        SkinID="GridViewTextBox">
                                    </asp:TextBox>
                                    <asp:RequiredFieldValidator ID="rvAddNewLS" Text="." runat="server" ControlToValidate="txtAddNewLS"
                                        ErrorMessage="Please enter a New LS" ValidationGroup="BranchAddGroup" SkinID="GridViewLabelError">
                                    </asp:RequiredFieldValidator>
                                    <ajaxToolkit:ValidatorCalloutExtender ID="VCE6" TargetControlID="rvAddNewLS" PopupPosition="TopLeft"
                                        runat="server">
                                    </ajaxToolkit:ValidatorCalloutExtender>
                                    <asp:CustomValidator ID="CVAddNewLS" Text="." ErrorMessage="New LS value should be numeric"
                                        runat="server" ControlToValidate="txtAddNewLS" SetFocusOnError="true" ValidationGroup="BranchAddGroup"
                                        SkinID="GridViewLabelError" EnableClientScript="true" ClientValidationFunction="fnValidateSLB"></asp:CustomValidator>
                                    <ajaxToolkit:ValidatorCalloutExtender ID="VC4" TargetControlID="CVAddNewLS" PopupPosition="TopLeft"
                                        runat="server">
                                    </ajaxToolkit:ValidatorCalloutExtender>
                                    <asp:CustomValidator ID="CustomValidator11" Text="." ErrorMessage="New LS value should be between -100 and 100"
                                        runat="server" ControlToValidate="txtAddNewLS" SetFocusOnError="true" ValidationGroup="BranchAddGroup"
                                        SkinID="GridViewLabelError" EnableClientScript="true" ClientValidationFunction="fnValidateSLBSecond"></asp:CustomValidator>
                                    <ajaxToolkit:ValidatorCalloutExtender ID="VC5" TargetControlID="CustomValidator11"
                                        PopupPosition="TopLeft" runat="server">
                                    </ajaxToolkit:ValidatorCalloutExtender>
                                    <asp:CustomValidator ID="CustomValidator12" Text="." ErrorMessage="New LS value should be greater than zero"
                                        runat="server" ControlToValidate="txtAddNewLS" SetFocusOnError="true" ValidationGroup="BranchAddGroup"
                                        SkinID="GridViewLabelError" EnableClientScript="true" ClientValidationFunction="fnValidateSLBThird"></asp:CustomValidator>
                                    <ajaxToolkit:ValidatorCalloutExtender ID="VC6" TargetControlID="CustomValidator12"
                                        PopupPosition="TopLeft" runat="server">
                                    </ajaxToolkit:ValidatorCalloutExtender>
                                </FooterTemplate>
                                <FooterStyle Wrap="false" />
                                <ItemTemplate>
                                    <asp:Label ID="lblNewLS" runat="server" Text='<%#Eval("ls_value", "{0:0.00}")%>'
                                        SkinID="GridViewLabel">
                                    </asp:Label>
                                </ItemTemplate>
                            </asp:TemplateField>
                            <asp:TemplateField HeaderText="Old LS">
                                <EditItemTemplate>
                                    <asp:TextBox ID="txtOldLS" runat="server" Text='<%#Eval("old_ls_value", "{0:0.00}")%>'
                                        SkinID="GridViewTextBox">
                                    </asp:TextBox>
                                    <asp:RequiredFieldValidator ID="rvOldLS" runat="server" Text="." ControlToValidate="txtOldLS"
                                        ErrorMessage="Please enter a Old LS" ValidationGroup="BranchEditGroup" SkinID="GridViewLabelError">
                                    </asp:RequiredFieldValidator>
                                    <ajaxToolkit:ValidatorCalloutExtender ID="VCE7" TargetControlID="rvOldLS" PopupPosition="TopLeft"
                                        runat="server">
                                    </ajaxToolkit:ValidatorCalloutExtender>
                                    <asp:CustomValidator ID="CVOldLS" Text="." ErrorMessage="Old LS value should be numeric"
                                        runat="server" ControlToValidate="txtOldLS" SetFocusOnError="true" ValidationGroup="BranchEditGroup"
                                        SkinID="GridViewLabelError" EnableClientScript="true" ClientValidationFunction="fnValidateSLB"></asp:CustomValidator>
                                    <ajaxToolkit:ValidatorCalloutExtender ID="VC7" TargetControlID="CVOldLS" PopupPosition="TopLeft"
                                        runat="server">
                                    </ajaxToolkit:ValidatorCalloutExtender>
                                    <asp:CustomValidator ID="CustomValidator13" Text="." ErrorMessage="Old LS value should be between -100 and 100"
                                        runat="server" ControlToValidate="txtOldLS" SetFocusOnError="true" ValidationGroup="BranchEditGroup"
                                        SkinID="GridViewLabelError" EnableClientScript="true" ClientValidationFunction="fnValidateSLBSecond"></asp:CustomValidator>
                                    <ajaxToolkit:ValidatorCalloutExtender ID="VC8" TargetControlID="CustomValidator13"
                                        PopupPosition="TopLeft" runat="server">
                                    </ajaxToolkit:ValidatorCalloutExtender>
                                    <asp:CustomValidator ID="CustomValidator14" Text="." ErrorMessage="Old LS value should be greater than zero"
                                        runat="server" ControlToValidate="txtOldLS" SetFocusOnError="true" ValidationGroup="BranchEditGroup"
                                        SkinID="GridViewLabelError" EnableClientScript="true" ClientValidationFunction="fnValidateSLBThird"></asp:CustomValidator>
                                    <ajaxToolkit:ValidatorCalloutExtender ID="VC9" TargetControlID="CustomValidator14"
                                        PopupPosition="TopLeft" runat="server">
                                    </ajaxToolkit:ValidatorCalloutExtender>
                                </EditItemTemplate>
                                <FooterTemplate>
                                    <asp:TextBox ID="txtAddOldLS" runat="server" Text='<%#Eval("old_ls_value", "{0:0.00}")%>'
                                        SkinID="GridViewTextBox">
                                    </asp:TextBox>
                                    <asp:RequiredFieldValidator ID="rvAddOldLS" Text="." runat="server" ControlToValidate="txtAddOldLS"
                                        ErrorMessage="Please enter a Old LS" ValidationGroup="BranchAddGroup" SkinID="GridViewLabelError">
                                    </asp:RequiredFieldValidator>
                                    <ajaxToolkit:ValidatorCalloutExtender ID="VCE8" TargetControlID="rvAddOldLS" PopupPosition="TopLeft"
                                        runat="server">
                                    </ajaxToolkit:ValidatorCalloutExtender>
                                    <asp:CustomValidator ID="CVNewOldLS" Text="." ErrorMessage="Old LS value should be numeric"
                                        runat="server" ControlToValidate="txtAddOldLS" SetFocusOnError="true" ValidationGroup="BranchAddGroup"
                                        SkinID="GridViewLabelError" EnableClientScript="true" ClientValidationFunction="fnValidateSLB"></asp:CustomValidator>
                                    <ajaxToolkit:ValidatorCalloutExtender ID="VC10" TargetControlID="CVNewOldLS" PopupPosition="TopLeft"
                                        runat="server">
                                    </ajaxToolkit:ValidatorCalloutExtender>
                                    <asp:CustomValidator ID="CustomValidator15" Text="." ErrorMessage="Old LS value should be between -100 and 100"
                                        runat="server" ControlToValidate="txtAddOldLS" SetFocusOnError="true" ValidationGroup="BranchAddGroup"
                                        SkinID="GridViewLabelError" EnableClientScript="true" ClientValidationFunction="fnValidateSLBSecond"></asp:CustomValidator>
                                    <ajaxToolkit:ValidatorCalloutExtender ID="VC11" TargetControlID="CustomValidator15"
                                        PopupPosition="TopLeft" runat="server">
                                    </ajaxToolkit:ValidatorCalloutExtender>
                                    <asp:CustomValidator ID="CustomValidator16" Text="." ErrorMessage="Old LS value should be greater than zero"
                                        runat="server" ControlToValidate="txtAddOldLS" SetFocusOnError="true" ValidationGroup="BranchAddGroup"
                                        SkinID="GridViewLabelError" EnableClientScript="true" ClientValidationFunction="fnValidateSLBThird"></asp:CustomValidator>
                                    <ajaxToolkit:ValidatorCalloutExtender ID="VC12" TargetControlID="CustomValidator16"
                                        PopupPosition="TopLeft" runat="server">
                                    </ajaxToolkit:ValidatorCalloutExtender>
                                </FooterTemplate>
                                <FooterStyle Wrap="false" />
                                <ItemTemplate>
                                    <asp:Label ID="lblOldLS" runat="server" Text='<%#Eval("old_ls_value", "{0:0.00}")%>'
                                        SkinID="GridViewLabel">
                                    </asp:Label>
                                </ItemTemplate>
                            </asp:TemplateField>
                            <asp:TemplateField HeaderText="New FDO">
                                <EditItemTemplate>
                                    <asp:TextBox ID="txtNewFDO" runat="server" Text='<%#Eval("fdo_value", "{0:0.00}")%>'
                                        SkinID="GridViewTextBox">
                                    </asp:TextBox>
                                    <asp:RequiredFieldValidator ID="rvNewFDO" runat="server" Text="." ControlToValidate="txtNewFDO"
                                        ErrorMessage="Please enter a New FDO" ValidationGroup="BranchEditGroup" SkinID="GridViewLabelError">
                                    </asp:RequiredFieldValidator>
                                    <ajaxToolkit:ValidatorCalloutExtender ID="VCE9" TargetControlID="rvNewFDO" PopupPosition="TopLeft"
                                        runat="server">
                                    </ajaxToolkit:ValidatorCalloutExtender>
                                    <asp:CustomValidator ID="CVNewFDO" Text="." ErrorMessage="New FDO value should be numeric"
                                        runat="server" ControlToValidate="txtNewFDO" SetFocusOnError="true" ValidationGroup="BranchEditGroup"
                                        SkinID="GridViewLabelError" EnableClientScript="true" ClientValidationFunction="fnValidateSLB"></asp:CustomValidator>
                                    <ajaxToolkit:ValidatorCalloutExtender ID="VC13" TargetControlID="CVNewFDO" PopupPosition="TopLeft"
                                        runat="server">
                                    </ajaxToolkit:ValidatorCalloutExtender>
                                    <asp:CustomValidator ID="CustomValidator17" Text="." ErrorMessage="New FDO value should be between -100 and 100"
                                        runat="server" ControlToValidate="txtNewFDO" SetFocusOnError="true" ValidationGroup="BranchEditGroup"
                                        SkinID="GridViewLabelError" EnableClientScript="true" ClientValidationFunction="fnValidateSLBSecond"></asp:CustomValidator>
                                    <ajaxToolkit:ValidatorCalloutExtender ID="VC14" TargetControlID="CustomValidator17"
                                        PopupPosition="TopLeft" runat="server">
                                    </ajaxToolkit:ValidatorCalloutExtender>
                                    <asp:CustomValidator ID="CustomValidator18" Text="." ErrorMessage="New FDO value should be greater than zero"
                                        runat="server" ControlToValidate="txtNewFDO" SetFocusOnError="true" ValidationGroup="BranchEditGroup"
                                        SkinID="GridViewLabelError" EnableClientScript="true" ClientValidationFunction="fnValidateSLBThird"></asp:CustomValidator>
                                    <ajaxToolkit:ValidatorCalloutExtender ID="VC15" TargetControlID="CustomValidator18"
                                        PopupPosition="TopLeft" runat="server">
                                    </ajaxToolkit:ValidatorCalloutExtender>
                                </EditItemTemplate>
                                <FooterTemplate>
                                    <asp:TextBox ID="txtAddNewFDO" runat="server" Text='<%#Eval("fdo_value", "{0:0.00}")%>'
                                        SkinID="GridViewTextBox">
                                    </asp:TextBox>
                                    <asp:RequiredFieldValidator ID="rvAddNewFDO" Text="." runat="server" ControlToValidate="txtAddNewFDO"
                                        ErrorMessage="Please enter a New FDO" ValidationGroup="BranchAddGroup" SkinID="GridViewLabelError">
                                    </asp:RequiredFieldValidator>
                                    <ajaxToolkit:ValidatorCalloutExtender ID="VCE10" TargetControlID="rvAddNewFDO" PopupPosition="TopLeft"
                                        runat="server">
                                    </ajaxToolkit:ValidatorCalloutExtender>
                                    <asp:CustomValidator ID="CVAddNewFDO" Text="." ErrorMessage="New FDO value should be numeric"
                                        runat="server" ControlToValidate="txtAddNewFDO" SetFocusOnError="true" ValidationGroup="BranchAddGroup"
                                        SkinID="GridViewLabelError" EnableClientScript="true" ClientValidationFunction="fnValidateSLB"></asp:CustomValidator>
                                    <ajaxToolkit:ValidatorCalloutExtender ID="VC16" TargetControlID="CVAddNewFDO" PopupPosition="TopLeft"
                                        runat="server">
                                    </ajaxToolkit:ValidatorCalloutExtender>
                                    <asp:CustomValidator ID="CustomValidator17" Text="." ErrorMessage="New FDO value should be between -100 and 100"
                                        runat="server" ControlToValidate="txtAddNewFDO" SetFocusOnError="true" ValidationGroup="BranchAddGroup"
                                        SkinID="GridViewLabelError" EnableClientScript="true" ClientValidationFunction="fnValidateSLBSecond"></asp:CustomValidator>
                                    <ajaxToolkit:ValidatorCalloutExtender ID="VC17" TargetControlID="CustomValidator17"
                                        PopupPosition="TopLeft" runat="server">
                                    </ajaxToolkit:ValidatorCalloutExtender>
                                    <asp:CustomValidator ID="CustomValidator18" Text="." ErrorMessage="New FDO value should be greater than zero"
                                        runat="server" ControlToValidate="txtAddNewFDO" SetFocusOnError="true" ValidationGroup="BranchAddGroup"
                                        SkinID="GridViewLabelError" EnableClientScript="true" ClientValidationFunction="fnValidateSLBThird"></asp:CustomValidator>
                                    <ajaxToolkit:ValidatorCalloutExtender ID="VC18" TargetControlID="CustomValidator18"
                                        PopupPosition="TopLeft" runat="server">
                                    </ajaxToolkit:ValidatorCalloutExtender>
                                </FooterTemplate>
                                <FooterStyle Wrap="false" />
                                <ItemTemplate>
                                    <asp:Label ID="lblNewFDO" runat="server" Text='<%#Eval("fdo_value", "{0:0.00}")%>'
                                        SkinID="GridViewLabel">
                                    </asp:Label>
                                </ItemTemplate>
                            </asp:TemplateField>
                            <asp:TemplateField HeaderText="Old FDO">
                                <EditItemTemplate>
                                    <asp:TextBox ID="txtOldFDO" runat="server" Text='<%#Eval("old_fdo_value", "{0:0.00}")%>'
                                        SkinID="GridViewTextBox">
                                    </asp:TextBox>
                                    <asp:RequiredFieldValidator ID="rvOldFDO" runat="server" Text="." ControlToValidate="txtOldFDO"
                                        ErrorMessage="Please enter a Old FDO" ValidationGroup="BranchEditGroup" SkinID="GridViewLabelError">
                                    </asp:RequiredFieldValidator>
                                    <ajaxToolkit:ValidatorCalloutExtender ID="VCE11" TargetControlID="rvOldFDO" PopupPosition="TopLeft"
                                        runat="server">
                                    </ajaxToolkit:ValidatorCalloutExtender>
                                    <asp:CustomValidator ID="CVOldFDO" Text="." ErrorMessage="Old FDO value should be numeric"
                                        runat="server" ControlToValidate="txtOldFDO" SetFocusOnError="true" ValidationGroup="BranchEditGroup"
                                        SkinID="GridViewLabelError" EnableClientScript="true" ClientValidationFunction="fnValidateSLB"></asp:CustomValidator>
                                    <ajaxToolkit:ValidatorCalloutExtender ID="VC19" TargetControlID="CVOldFDO" PopupPosition="TopLeft"
                                        runat="server">
                                    </ajaxToolkit:ValidatorCalloutExtender>
                                    <asp:CustomValidator ID="CustomValidator19" Text="." ErrorMessage="Old FDO value should be between -100 and 100"
                                        runat="server" ControlToValidate="txtOldFDO" SetFocusOnError="true" ValidationGroup="BranchEditGroup"
                                        SkinID="GridViewLabelError" EnableClientScript="true" ClientValidationFunction="fnValidateSLBSecond"></asp:CustomValidator>
                                    <ajaxToolkit:ValidatorCalloutExtender ID="VC20" TargetControlID="CustomValidator19"
                                        PopupPosition="TopLeft" runat="server">
                                    </ajaxToolkit:ValidatorCalloutExtender>
                                    <asp:CustomValidator ID="CustomValidator20" Text="." ErrorMessage="Old FDO value should be greater than zero"
                                        runat="server" ControlToValidate="txtOldFDO" SetFocusOnError="true" ValidationGroup="BranchEditGroup"
                                        SkinID="GridViewLabelError" EnableClientScript="true" ClientValidationFunction="fnValidateSLBThird"></asp:CustomValidator>
                                    <ajaxToolkit:ValidatorCalloutExtender ID="VC21" TargetControlID="CustomValidator20"
                                        PopupPosition="TopLeft" runat="server">
                                    </ajaxToolkit:ValidatorCalloutExtender>
                                </EditItemTemplate>
                                <FooterTemplate>
                                    <asp:TextBox ID="txtAddOldFDO" runat="server" Text='<%#Eval("old_fdo_value", "{0:0.00}")%>'
                                        SkinID="GridViewTextBox">
                                    </asp:TextBox>
                                    <asp:RequiredFieldValidator ID="rvAddOldFDO" Text="." runat="server" ControlToValidate="txtAddOldFDO"
                                        ErrorMessage="Please enter a Old FDO" ValidationGroup="BranchAddGroup" SkinID="GridViewLabelError">
                                    </asp:RequiredFieldValidator>
                                    <ajaxToolkit:ValidatorCalloutExtender ID="VCE12" TargetControlID="rvAddOldFDO" PopupPosition="TopLeft"
                                        runat="server">
                                    </ajaxToolkit:ValidatorCalloutExtender>
                                    <asp:CustomValidator ID="CVAddOldFDO" Text="." ErrorMessage="Old FDO value should be numeric"
                                        runat="server" ControlToValidate="txtAddOldFDO" SetFocusOnError="true" ValidationGroup="BranchAddGroup"
                                        SkinID="GridViewLabelError" EnableClientScript="true" ClientValidationFunction="fnValidateSLB"></asp:CustomValidator>
                                    <ajaxToolkit:ValidatorCalloutExtender ID="VC19" TargetControlID="CVAddOldFDO" PopupPosition="TopLeft"
                                        runat="server">
                                    </ajaxToolkit:ValidatorCalloutExtender>
                                    <asp:CustomValidator ID="CustomValidator21" Text="." ErrorMessage="Old FDO value should be between -100 and 100"
                                        runat="server" ControlToValidate="txtAddOldFDO" SetFocusOnError="true" ValidationGroup="BranchAddGroup"
                                        SkinID="GridViewLabelError" EnableClientScript="true" ClientValidationFunction="fnValidateSLBSecond"></asp:CustomValidator>
                                    <ajaxToolkit:ValidatorCalloutExtender ID="VC20" TargetControlID="CustomValidator21"
                                        PopupPosition="TopLeft" runat="server">
                                    </ajaxToolkit:ValidatorCalloutExtender>
                                    <asp:CustomValidator ID="CustomValidator22" Text="." ErrorMessage="Old FDO value should be greater than zero"
                                        runat="server" ControlToValidate="txtAddOldFDO" SetFocusOnError="true" ValidationGroup="BranchAddGroup"
                                        SkinID="GridViewLabelError" EnableClientScript="true" ClientValidationFunction="fnValidateSLBThird"></asp:CustomValidator>
                                    <ajaxToolkit:ValidatorCalloutExtender ID="VC21" TargetControlID="CustomValidator22"
                                        PopupPosition="TopLeft" runat="server">
                                    </ajaxToolkit:ValidatorCalloutExtender>
                                </FooterTemplate>
                                <FooterStyle Wrap="false" />
                                <ItemTemplate>
                                    <asp:Label ID="lblOldFDO" runat="server" Text='<%#Eval("old_fdo_value", "{0:0.00}")%>'
                                        SkinID="GridViewLabel">
                                    </asp:Label>
                                </ItemTemplate>
                            </asp:TemplateField>
                            <asp:TemplateField HeaderText="New LR Transfer">
                                <EditItemTemplate>
                                    <asp:TextBox ID="txtNewLRTransfer" runat="server" Text='<%#Eval("lr_value", "{0:0.00}")%>'
                                        SkinID="GridViewTextBox">
                                    </asp:TextBox>
                                    <asp:RequiredFieldValidator ID="rvNewLRTransfer" runat="server" Text="." ControlToValidate="txtNewLRTransfer"
                                        ErrorMessage="Please enter a New LR Transfer" ValidationGroup="BranchEditGroup"
                                        SkinID="GridViewLabelError">
                                    </asp:RequiredFieldValidator>
                                    <ajaxToolkit:ValidatorCalloutExtender ID="VCE13" TargetControlID="rvNewLRTransfer"
                                        PopupPosition="TopLeft" runat="server">
                                    </ajaxToolkit:ValidatorCalloutExtender>
                                    <asp:CustomValidator ID="CVNewLRTransfer" Text="." ErrorMessage="New LR Transfer value should be numeric"
                                        runat="server" ControlToValidate="txtNewLRTransfer" SetFocusOnError="true" ValidationGroup="BranchEditGroup"
                                        SkinID="GridViewLabelError" EnableClientScript="true" ClientValidationFunction="fnValidateSLB"></asp:CustomValidator>
                                    <ajaxToolkit:ValidatorCalloutExtender ID="VC25" TargetControlID="CVNewLRTransfer"
                                        PopupPosition="TopLeft" runat="server">
                                    </ajaxToolkit:ValidatorCalloutExtender>
                                    <asp:CustomValidator ID="CustomValidator24" Text="." ErrorMessage="New LR Transfer value should be between -100 and 100"
                                        runat="server" ControlToValidate="txtNewLRTransfer" SetFocusOnError="true" ValidationGroup="BranchEditGroup"
                                        SkinID="GridViewLabelError" EnableClientScript="true" ClientValidationFunction="fnValidateSLBSecond"></asp:CustomValidator>
                                    <ajaxToolkit:ValidatorCalloutExtender ID="VC26" TargetControlID="CustomValidator24"
                                        PopupPosition="TopLeft" runat="server">
                                    </ajaxToolkit:ValidatorCalloutExtender>
                                    <asp:CustomValidator ID="CustomValidator25" Text="." ErrorMessage="New LR Transfer value should be greater than zero"
                                        runat="server" ControlToValidate="txtNewLRTransfer" SetFocusOnError="true" ValidationGroup="BranchEditGroup"
                                        SkinID="GridViewLabelError" EnableClientScript="true" ClientValidationFunction="fnValidateSLBThird"></asp:CustomValidator>
                                    <ajaxToolkit:ValidatorCalloutExtender ID="VC27" TargetControlID="CustomValidator25"
                                        PopupPosition="TopLeft" runat="server">
                                    </ajaxToolkit:ValidatorCalloutExtender>
                                </EditItemTemplate>
                                <FooterTemplate>
                                    <asp:TextBox ID="txtAddNewLRTransfer" runat="server" Text='<%#Eval("lr_value", "{0:0.00}")%>'
                                        SkinID="GridViewTextBox">
                                    </asp:TextBox>
                                    <asp:RequiredFieldValidator ID="rvAddNewLRTransfer" Text="." runat="server" ControlToValidate="txtAddNewLRTransfer"
                                        ErrorMessage="Please enter a New LR Transfer" ValidationGroup="BranchAddGroup"
                                        SkinID="GridViewLabelError">
                                    </asp:RequiredFieldValidator>
                                    <ajaxToolkit:ValidatorCalloutExtender ID="VCE14" TargetControlID="rvAddNewLRTransfer"
                                        PopupPosition="TopLeft" runat="server">
                                    </ajaxToolkit:ValidatorCalloutExtender>
                                    <asp:CustomValidator ID="CVAddNewLRTransfer" Text="." ErrorMessage="New LR Transfer value should be numeric"
                                        runat="server" ControlToValidate="txtAddNewLRTransfer" SetFocusOnError="true"
                                        ValidationGroup="BranchAddGroup" SkinID="GridViewLabelError" EnableClientScript="true"
                                        ClientValidationFunction="fnValidateSLB"></asp:CustomValidator>
                                    <ajaxToolkit:ValidatorCalloutExtender ID="VC28" TargetControlID="CVAddNewLRTransfer"
                                        PopupPosition="TopLeft" runat="server">
                                    </ajaxToolkit:ValidatorCalloutExtender>
                                    <asp:CustomValidator ID="CustomValidator25" Text="." ErrorMessage="New LR Transfer value should be between -100 and 100"
                                        runat="server" ControlToValidate="txtAddNewLRTransfer" SetFocusOnError="true"
                                        ValidationGroup="BranchAddGroup" SkinID="GridViewLabelError" EnableClientScript="true"
                                        ClientValidationFunction="fnValidateSLBSecond"></asp:CustomValidator>
                                    <ajaxToolkit:ValidatorCalloutExtender ID="VC29" TargetControlID="CustomValidator25"
                                        PopupPosition="TopLeft" runat="server">
                                    </ajaxToolkit:ValidatorCalloutExtender>
                                    <asp:CustomValidator ID="CustomValidator26" Text="." ErrorMessage="New LR Transfer value should be greater than zero"
                                        runat="server" ControlToValidate="txtAddNewLRTransfer" SetFocusOnError="true"
                                        ValidationGroup="BranchAddGroup" SkinID="GridViewLabelError" EnableClientScript="true"
                                        ClientValidationFunction="fnValidateSLBThird"></asp:CustomValidator>
                                    <ajaxToolkit:ValidatorCalloutExtender ID="VC30" TargetControlID="CustomValidator26"
                                        PopupPosition="TopLeft" runat="server">
                                    </ajaxToolkit:ValidatorCalloutExtender>
                                </FooterTemplate>
                                <FooterStyle Wrap="false" />
                                <ItemTemplate>
                                    <asp:Label ID="lblNewLRTransfer" runat="server" Text='<%#Eval("lr_value", "{0:0.00}")%>'
                                        SkinID="GridViewLabel">
                                    </asp:Label>
                                </ItemTemplate>
                            </asp:TemplateField>
                            <asp:TemplateField HeaderText="Old LR Transfer">
                                <EditItemTemplate>
                                    <asp:TextBox ID="txtOldLRTransfer" runat="server" Text='<%#Eval("old_lr_value", "{0:0.00}")%>'
                                        SkinID="GridViewTextBox">
                                    </asp:TextBox>
                                    <asp:RequiredFieldValidator ID="rvOldLRTransfer" runat="server" Text="." ControlToValidate="txtOldLRTransfer"
                                        ErrorMessage="Please enter a Old LR Transfer" ValidationGroup="BranchEditGroup"
                                        SkinID="GridViewLabelError">
                                    </asp:RequiredFieldValidator>
                                    <ajaxToolkit:ValidatorCalloutExtender ID="VCE15" TargetControlID="rvOldLRTransfer"
                                        PopupPosition="TopLeft" runat="server">
                                    </ajaxToolkit:ValidatorCalloutExtender>
                                    <asp:CustomValidator ID="CVOldLRTransfer" Text="." ErrorMessage="Old LR Transfer value should be numeric"
                                        runat="server" ControlToValidate="txtOldLRTransfer" SetFocusOnError="true" ValidationGroup="BranchEditGroup"
                                        SkinID="GridViewLabelError" EnableClientScript="true" ClientValidationFunction="fnValidateSLB"></asp:CustomValidator>
                                    <ajaxToolkit:ValidatorCalloutExtender ID="VC31" TargetControlID="CVOldLRTransfer"
                                        PopupPosition="TopLeft" runat="server">
                                    </ajaxToolkit:ValidatorCalloutExtender>
                                    <asp:CustomValidator ID="CustomValidator27" Text="." ErrorMessage="Old LR Transfer value should be between -100 and 100"
                                        runat="server" ControlToValidate="txtOldLRTransfer" SetFocusOnError="true" ValidationGroup="BranchEditGroup"
                                        SkinID="GridViewLabelError" EnableClientScript="true" ClientValidationFunction="fnValidateSLBSecond"></asp:CustomValidator>
                                    <ajaxToolkit:ValidatorCalloutExtender ID="VC32" TargetControlID="CustomValidator27"
                                        PopupPosition="TopLeft" runat="server">
                                    </ajaxToolkit:ValidatorCalloutExtender>
                                    <asp:CustomValidator ID="CustomValidator28" Text="." ErrorMessage="Old LR Transfer value should be greater than zero"
                                        runat="server" ControlToValidate="txtOldLRTransfer" SetFocusOnError="true" ValidationGroup="BranchEditGroup"
                                        SkinID="GridViewLabelError" EnableClientScript="true" ClientValidationFunction="fnValidateSLBThird"></asp:CustomValidator>
                                    <ajaxToolkit:ValidatorCalloutExtender ID="VC33" TargetControlID="CustomValidator28"
                                        PopupPosition="TopLeft" runat="server">
                                    </ajaxToolkit:ValidatorCalloutExtender>
                                </EditItemTemplate>
                                <FooterTemplate>
                                    <asp:TextBox ID="txtAddOldLRTransfer" runat="server" Text='<%#Eval("old_lr_value", "{0:0.00}")%>'
                                        SkinID="GridViewTextBox">
                                    </asp:TextBox>
                                    <asp:RequiredFieldValidator ID="rvAddOldLRTransfer" Text="." runat="server" ControlToValidate="txtAddOldLRTransfer"
                                        ErrorMessage="Please enter a Old LR Transfer" ValidationGroup="BranchAddGroup"
                                        SkinID="GridViewLabelError">
                                    </asp:RequiredFieldValidator>
                                    <ajaxToolkit:ValidatorCalloutExtender ID="VCE16" TargetControlID="rvAddOldLRTransfer"
                                        PopupPosition="TopLeft" runat="server">
                                    </ajaxToolkit:ValidatorCalloutExtender>
                                    <asp:CustomValidator ID="CVAddOldLRTransfer" Text="." ErrorMessage="Old LR Transfer value should be numeric"
                                        runat="server" ControlToValidate="txtAddOldLRTransfer" SetFocusOnError="true"
                                        ValidationGroup="BranchAddGroup" SkinID="GridViewLabelError" EnableClientScript="true"
                                        ClientValidationFunction="fnValidateSLB"></asp:CustomValidator>
                                    <ajaxToolkit:ValidatorCalloutExtender ID="VC34" TargetControlID="CVAddOldLRTransfer"
                                        PopupPosition="TopLeft" runat="server">
                                    </ajaxToolkit:ValidatorCalloutExtender>
                                    <asp:CustomValidator ID="CustomValidator29" Text="." ErrorMessage="Old LR Transfer value should be between -100 and 100"
                                        runat="server" ControlToValidate="txtAddOldLRTransfer" SetFocusOnError="true"
                                        ValidationGroup="BranchAddGroup" SkinID="GridViewLabelError" EnableClientScript="true"
                                        ClientValidationFunction="fnValidateSLBSecond"></asp:CustomValidator>
                                    <ajaxToolkit:ValidatorCalloutExtender ID="VC35" TargetControlID="CustomValidator29"
                                        PopupPosition="TopLeft" runat="server">
                                    </ajaxToolkit:ValidatorCalloutExtender>
                                    <asp:CustomValidator ID="CustomValidator30" Text="." ErrorMessage="Old LR Transfer value should be greater than zero"
                                        runat="server" ControlToValidate="txtAddOldLRTransfer" SetFocusOnError="true"
                                        ValidationGroup="BranchAddGroup" SkinID="GridViewLabelError" EnableClientScript="true"
                                        ClientValidationFunction="fnValidateSLBThird"></asp:CustomValidator>
                                    <ajaxToolkit:ValidatorCalloutExtender ID="VC36" TargetControlID="CustomValidator30"
                                        PopupPosition="TopLeft" runat="server">
                                    </ajaxToolkit:ValidatorCalloutExtender>
                                </FooterTemplate>
                                <FooterStyle Wrap="false" />
                                <ItemTemplate>
                                    <asp:Label ID="lblOldLRTransfer" runat="server" Text='<%#Eval("old_lr_value", "{0:0.00}")%>'
                                        SkinID="GridViewLabel">
                                    </asp:Label>
                                </ItemTemplate>
                            </asp:TemplateField>
                            <asp:TemplateField ShowHeader="False">
                                <ItemTemplate>
                                    <asp:LinkButton ID="btEdit" runat="server" CausesValidation="False" CommandName="Edit"
                                        SkinID="GridViewLinkButton">
                                        <asp:Image ID="imgFolder" runat="server" ImageUrl="~/images/iGrid_Edit.png" SkinID="GridViewImageEdit" />
                                    </asp:LinkButton>
                                </ItemTemplate>
                                <EditItemTemplate>
                                    <asp:LinkButton ID="btUpdate" runat="server" CausesValidation="True" CommandName="Update"
                                        ValidationGroup="BranchEditGroup" SkinID="GridViewLinkButton">
                                        <asp:Image ID="imgFolder1" runat="server" ImageUrl="~/images/iGrid_Ok.png" SkinID="GridViewImageEdit" />
                                    </asp:LinkButton>
                                    <asp:LinkButton ID="btCancel" runat="server" CausesValidation="False" CommandName="Cancel"
                                        SkinID="GridViewLinkButton">
                                        <asp:Image ID="imgFolder2" runat="server" ImageUrl="~/images/iGrid_Cancel.png" SkinID="GridViewImageEdit" />
                                    </asp:LinkButton>
                                </EditItemTemplate>
                                <FooterTemplate>
                                    <asp:Button ID="btAdd" runat="server" Text="Add" CommandName="Insert" ValidationGroup="BranchAddGroup"
                                        SkinID="GridViewButton" />
                                </FooterTemplate>
                            </asp:TemplateField>
                        </Columns>
                    </asp:GridView>
                </div>
                <div class="transactionButtons">
                    <div class="transactionButtonsHolder">
                        <asp:Button SkinID="ButtonViewReport" ID="btnReport" runat="server" Text="Generate Report"
                            OnClick="btnReport_Click" Visible="false" />
                        <asp:Button SkinID="ButtonViewReport" ID="btnReset" runat="server" Text="Reset"
                            OnClick="btnReset_Click" />
                    </div>
                </div>
            </ContentTemplate>
            <Triggers>
                <asp:PostBackTrigger ControlID="btnReport" />
            </Triggers>
        </asp:UpdatePanel>
    </div>
    <script type="text/javascript">
        function pageLoad(sender, args) {
            gridViewFixedHeader('<%=GV_SBLDetail.ClientID%>', 1024, 400);
        }
    </script>
</asp:Content>
