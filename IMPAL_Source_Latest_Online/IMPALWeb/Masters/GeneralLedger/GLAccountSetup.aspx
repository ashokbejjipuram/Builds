<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="GLAccountSetup.aspx.cs"
    MasterPageFile="~/Main.Master" Inherits="IMPALWeb.GLAccountSetup" %>

<%@ Register Src="~/UserControls/ChartAccount.ascx" TagName="ChartAccount" TagPrefix="User" %>
<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="ajaxToolkit" %>
<asp:Content ID="Content1" ContentPlaceHolderID="CPHDetails" runat="server">

    <script language="javascript" type="text/javascript">

        function isspecialchar(c) {
            myArray = ['!', '#', '$', '%', '^', '&', '*', '(', ')', '-', '+', '=', '_', '`', '~', ']', '[', '|', '@', '/', '"', ':', ';', '{', '}', ',', "'", '.', '?', '\\'];
            for (var j = 0; j < myArray.length; j++) {
                if (c == myArray[j]) {
                    return true;
                }
            }
            return false;
        }

        function validatespl(inpval, fldname) {
            var Stat;
            var firstchr = inpval.substring(0, 1, inpval);
            if (firstchr == " ") {
                alert("First character of " + fldname + " should not be blank");
                return Stat = "No";
            }
            else if (isspecialchar(firstchr)) {
                alert("First character of " + fldname + " should be alphabet or number");
                return Stat = "No";
            }

            for (i = 0; i < inpval.length; i++) {
                firstchr = inpval.substring(i, i + 1, inpval);
                if (firstchr == "") {
                    alert("First character of " + fldname + " should not be blank");
                    return Stat = "No";
                }
                else if (isspecial1(firstchr)) {
                    alert("Characters in " + fldname + " should be alphabet or number");
                    return Stat = "No";
                }
            }
            return Stat = "Yes";
        }


        function ValidationSubmit() {

            var CtrlIdPrefix = "ctl00_CPHDetails_";

            var transactionType = document.getElementById(CtrlIdPrefix + "ddlTransactionType");
            if (transactionType.value == "0") {
                alert("Transaction type should not be null");
                transactionType.focus();
                return false;
            }


            var description = document.getElementById(CtrlIdPrefix + "txtDescription");
            if (description.value == "") {
                alert("Description should not be null");
                description.focus();
                return false;
            }

            var stat = validatespl(description.value, "Description");

            if (stat == "No") {
                return false
            }


            var ddlDGLMain = document.getElementById(CtrlIdPrefix + "ddlDGLMain");
            if (ddlDGLMain.value != "0") {
                var ddlDGLSub = document.getElementById(CtrlIdPrefix + "ddlDGLSub");
                if (ddlDGLSub.value == "0") {
                    alert("GL Sub Debit should not be null");
                    ddlDGLSub.focus();
                    return false;
                }

                var ddlDGLAccount = document.getElementById(CtrlIdPrefix + "ddlDGLAccount");
                if (ddlDGLAccount.value == "0") {
                    alert("GL Account for Debit should not be null");
                    ddlDGLSub.focus();
                    return false;
                }
            }


            var ddlCGLMain = document.getElementById(CtrlIdPrefix + "ddlCGLMain");
            if (ddlCGLMain.value != "0") {
                var ddlCGLSub = document.getElementById(CtrlIdPrefix + "ddlCGLSub");
                if (ddlCGLSub.value == "0") {
                    alert("GL Sub Credit should not be null");
                    ddlCGLSub.focus();
                    return false;
                }

                var ddlCGLAccount = document.getElementById(CtrlIdPrefix + "ddlCGLAccount");
                if (ddlCGLAccount.value == "0") {
                    alert("GL Account for Credit should not be null");
                    ddlCGLSub.focus();
                    return false;
                }
            }
        }
    </script>

    <div>
        <asp:UpdatePanel ID="updateSupplier" runat="server">
            <ContentTemplate>
                <div>
                    <table class="subFormTable">
                        <tr>
                            <td>
                                <div class="subFormTitle">
                                    GL Account Setup
                                </div>
                            </td>
                        </tr>
                        <tr>
                            <td colspan="4" class="label">
                                <asp:Label ID="lblHeaderMessage" Text="" runat="server" SkinID="Error"></asp:Label>
                            </td>
                        </tr>
                        <tr>
                            <td class="label">
                                <asp:Label ID="lblNumber" runat="server" Text="Number" SkinID="LabelNormal"></asp:Label>
                            </td>
                            <td class="inputcontrols">
                                <asp:TextBox ID="txtNumber" runat="server" ReadOnly="true" SkinID="TextBoxNormalBig"></asp:TextBox>
                                <asp:DropDownList ID="ddlNumber" runat="server" AutoPostBack="True" SkinID="DropDownListNormalBig"
                                    OnSelectedIndexChanged="ddlNumber_SelectedIndexChanged">
                                    <asp:ListItem Selected="True" Text="-- Select --" Value="0"></asp:ListItem>
                                </asp:DropDownList>
                                <asp:ImageButton ID="imgEditToggle" ImageUrl="~/images/ifind.png" SkinID="ImageButtonSearch"
                                    runat="server" OnClick="imgEditToggle_Click" />
                            </td>
                            <td class="label">
                                <asp:Label ID="lblSerialNo" runat="server" SkinID="LabelNormal" Text="Serial Number"></asp:Label>
                            </td>
                            <td class="inputcontrols">
                                <asp:TextBox ID="txtSerialNo" runat="server" ReadOnly="true" SkinID="TextBoxNormal"></asp:TextBox>
                                <asp:DropDownList ID="ddlSerialNo" runat="server" AutoPostBack="True" SkinID="DropDownListNormalBig"
                                    OnSelectedIndexChanged="ddlSerialNo_SelectedIndexChanged">
                                </asp:DropDownList>
                                <%--<asp:RequiredFieldValidator ID="RequiredFieldValidator1" InitialValue="" ValidationGroup="validate"
                                    runat="server" ControlToValidate="ddlSerialNo" SetFocusOnError="true" ErrorMessage="Serial Number is required"
                                    SkinID="GridViewLabelError"></asp:RequiredFieldValidator>--%>
                            </td>
                        </tr>
                        <tr>
                            <td class="label">
                                <asp:Label ID="lblTransType" runat="server" SkinID="LabelNormal" Text="Transaction Type"></asp:Label>
                                <span class="asterix">*</span>
                            </td>
                            <td class="inputcontrols">
                                <asp:DropDownList ID="ddlTransactionType" SkinID="DropDownListNormalBig" runat="server">
                                </asp:DropDownList>
                                <asp:RequiredFieldValidator ID="RequiredFieldType" InitialValue="" ValidationGroup="validate"
                                    runat="server" ControlToValidate="ddlTransactionType" SetFocusOnError="true"
                                    ErrorMessage="Transaction Type is required" SkinID="GridViewLabelError"></asp:RequiredFieldValidator>
                            </td>
                            <td class="label">
                                <asp:Label ID="lblDesc" runat="server" SkinID="LabelNormal" Text="Description"></asp:Label>
                                <span class="asterix">*</span>
                            </td>
                            <td>
                                <asp:TextBox ID="txtDescription" runat="server" Text='' SkinID="TextBoxNormalBig"></asp:TextBox>
                            </td>
                        </tr>
                        <tr>
                            <td align="center" colspan="2">
                                <div class="subFormTitle">
                                    Debit
                                </div>
                            </td>
                            <td align="center" colspan="2">
                                <div class="subFormTitle">
                                    Credit
                                </div>
                            </td>
                        </tr>
                        <tr>
                            <td class="label">
                                <asp:Label ID="lbldrglMain" runat="server" SkinID="LabelNormal" Text="GL Main"></asp:Label>
                            </td>
                            <td class="inputcontrols">
                                <asp:DropDownList ID="ddlDGLMain" runat="server" SkinID="DropDownListNormalBig" OnSelectedIndexChanged="ddlDGLMain_SelectedIndexChanged"
                                    AutoPostBack="True">
                                </asp:DropDownList>
                            </td>
                            <td class="label">
                                <asp:Label ID="lblcrglMain" runat="server" SkinID="LabelNormal" Text="GL Main"></asp:Label>
                            </td>
                            <td class="inputcontrols">
                                <asp:DropDownList ID="ddlCGLMain" runat="server" SkinID="DropDownListNormalBig" OnSelectedIndexChanged="ddlCGLMain_SelectedIndexChanged"
                                    AutoPostBack="True">
                                </asp:DropDownList>
                            </td>
                        </tr>
                        <tr>
                            <td class="label">
                                <asp:Label ID="Label1" runat="server" SkinID="LabelNormal" Text="GL Sub"></asp:Label>
                            </td>
                            <td class="inputcontrols">
                                <asp:DropDownList ID="ddlDGLSub" runat="server" SkinID="DropDownListNormalBig" OnSelectedIndexChanged="ddlDGLSub_SelectedIndexChanged"
                                    AutoPostBack="True">
                                </asp:DropDownList>
                            </td>
                            <td class="label">
                                <asp:Label ID="Label2" runat="server" SkinID="LabelNormal" Text="GL Sub"></asp:Label>
                            </td>
                            <td class="inputcontrols">
                                <asp:DropDownList ID="ddlCGLSub" runat="server" SkinID="DropDownListNormalBig" OnSelectedIndexChanged="ddlCGLSub_SelectedIndexChanged"
                                    AutoPostBack="True">
                                </asp:DropDownList>
                            </td>
                        </tr>
                        <tr>
                            <td class="label">
                                <asp:Label ID="lbldrGlAccount" runat="server" SkinID="LabelNormal" Text="GL Account"></asp:Label>
                            </td>
                            <td class="inputcontrols">
                                <asp:DropDownList ID="ddlDGLAccount" runat="server" SkinID="DropDownListNormalBig">
                                </asp:DropDownList>
                            </td>
                            <td class="label">
                                <asp:Label ID="lblcrglAccount" runat="server" SkinID="LabelNormal" Text="GL Account"></asp:Label>
                            </td>
                            <td class="inputcontrols">
                                <asp:DropDownList ID="ddlCGLAccount" runat="server" SkinID="DropDownListNormalBig">
                                </asp:DropDownList>
                            </td>
                        </tr>
                    </table>
                    <div class="transactionButtons">
                        <div class="transactionButtonsHolder">
                            <asp:Button ID="BtnSubmit" runat="server" ValidationGroup="validate" Text="Submit"
                                SkinID="ButtonNormal" OnClick="BtnSubmit_Click" />
                            <asp:Button ID="btnReset" runat="server" Text="Reset" SkinID="ButtonNormal" OnClick="btnReset_Click" />
                            <asp:Button ID="btnReport" runat="server" Text="Report" SkinID="ButtonNormal" OnClick="btnReport_Click" />
                        </div>
                    </div>
                </div>
            </ContentTemplate>
             <Triggers>
                <asp:PostBackTrigger ControlID="btnReport" />
            </Triggers>
        </asp:UpdatePanel>
    </div>
</asp:Content>
