<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="SupplierPartNumberSearch.ascx.cs"
    Inherits="IMPALWeb.UserControls.SupplierPartNumberSearch" %>
<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="ajaxToolkit" %>
<table class="subFormTable">
    <tr>
        <td class="labelColSpan2">
            <asp:Label ID="lblSupplierPartNumber" SkinID="LabelNormal" runat="server" Text="Supplier Part Number"></asp:Label>
            <span class="asterix">*</span>
        </td>
        <td class="inputcontrolsColSpan2">
            <asp:DropDownList ID="ddlSupplierPartNumber" SkinID="DropDownListNormalBig" runat="server"
                AutoPostBack="True" OnSelectedIndexChanged="ddlSupplierPartNumber_SelectedIndexChanged">
            </asp:DropDownList>
            <asp:TextBox ID="txtSupplierPartNumber" SkinID="TextBoxNormalBig" runat="server"
                onkeypress="return AlphaNumericWithDashPercentile();"></asp:TextBox>
            <asp:ImageButton ID="ImgButtonQuery" ImageUrl="~/images/ifind.png" alt="Query" runat="server"
                SkinID="ImageButtonSearch" OnClick="ImgButtonQuery_Click" OnClientClick="javscript:return RemoveSpecialChars();" />
            <asp:RequiredFieldValidator ID="ReqItemCode" runat="server" ForeColor="Red" ControlToValidate="txtSupplierPartNumber"
                SetFocusOnError="true" ErrorMessage="Supplier Part Number is required." Display="None"></asp:RequiredFieldValidator>
            <ajaxToolkit:ValidatorCalloutExtender ID="ValCalloutExtender1" TargetControlID="ReqItemCode"
                PopupPosition="BottomLeft" runat="server">
            </ajaxToolkit:ValidatorCalloutExtender>
        </td>
        <td class="labelColSpan2">
            <asp:Label ID="lblItemCode" SkinID="LabelNormal" runat="server" Text="Item Code"></asp:Label>
        </td>
        <td class="inputcontrolsColSpan2">
            <asp:TextBox ID="txtItemCode" SkinID="TextBoxDisabledBig" ReadOnly="true" runat="server"></asp:TextBox>
        </td>
    </tr>
</table>

<script type="text/javascript">      
      function AlphaNumericWithDashPercentile() {
        var AsciiValue = event.keyCode
        if ((AsciiValue >= 48 && AsciiValue <= 57) || (AsciiValue == 8 || AsciiValue == 127 || AsciiValue == 37 || AsciiValue == 45) || (AsciiValue >= 65 && AsciiValue <= 90) || (AsciiValue >= 97 && AsciiValue <= 122))
            event.returnValue = true;
        else
            event.returnValue = false;
    }

    function RemoveSpecialChars() {
        var txtSuppPartNumber = document.getElementById('<%=txtSupplierPartNumber.ClientID%>').value;
        var val = txtSuppPartNumber.replace(/[^a-zA-Z0-9]/g, '');
        document.getElementById('<%=txtSupplierPartNumber.ClientID%>').value = val;
    }
</script>

