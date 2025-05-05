<%@ Page Language="C#" AutoEventWireup="true" MasterPageFile="~/Main.Master" CodeBehind="OS_LS_Updation.aspx.cs"
    Inherits="IMPALWeb.Transactions.Inventory.OS_LSUpdation" %>

<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="ajaxToolkit" %>
<asp:Content ID="Content3" ContentPlaceHolderID="CPHDetails" runat="server">
 <script src="../../Javascript/DirectPOcommon.js" type="text/javascript"></script>
  <div id="DivTop" runat="server">
    <asp:UpdatePanel ID="UpdatePanel1" runat="server">
        <ContentTemplate>             
            <div class="subFormTitle">
                OS/LS Updation</div>
            <table class="subFormTable">
                <tr>
                    <td class="labelColSpan2" >
                        <asp:Label ID="lblUpdationType" SkinID="LabelNormal" runat="server" Text="Updation Type"></asp:Label>
                    </td>
                    <td class="inputcontrolsColSpan2">
                        <asp:DropDownList ID="ddlUpdationType" SkinID="DropDownListNormalBig" runat="server" AutoPostBack="True"
                            OnSelectedIndexChanged="ddlUpdationType_SelectedIndexChanged">
                            <asp:ListItem Value="G" Text="GRN"></asp:ListItem>
                            <asp:ListItem Value="S" Text="Stock Transfer"></asp:ListItem>
                        </asp:DropDownList>
                    </td>
                </tr>
                <tr>
                    <td class="labelColSpan2" >
                        <asp:Label ID="lblInward_STDNNo" SkinID="LabelNormal" runat="server" Text="Inward/STDN No"></asp:Label>
                    </td>
                    <td class="inputcontrolsColSpan2">
                        <asp:DropDownList ID="ddlInward_STDNNo" SkinID="DropDownListNormalBig" runat="server" AutoPostBack="True"
                            OnSelectedIndexChanged="ddlInward_STDNNo_SelectedIndexChanged">
                        </asp:DropDownList>
                    </td>
                </tr>
                <tr id="TRSerialNo" runat="server">
                    <td class="labelColSpan2" >
                        <asp:Label ID="lblSerialNo" SkinID="LabelNormal" runat="server" Text="Serial No"></asp:Label>
                    </td>
                    <td class="inputcontrolsColSpan2">
                        <asp:DropDownList ID="ddlSerialNo" SkinID="DropDownListNormalBig" runat="server" AutoPostBack="True"
                            OnSelectedIndexChanged="ddlSerialNo_SelectedIndexChanged">
                        </asp:DropDownList>
                    </td>
                </tr>
                <tr>
                    <td class="labelColSpan2" >
                        <asp:Label ID="lblSupplierPartNo" SkinID="LabelNormal" runat="server" Text="Supplier Part Number"></asp:Label>
                    </td>
                    <td class="inputcontrolsColSpan2">
                        <asp:DropDownList ID="ddlSupplierPartNo" SkinID="DropDownListNormalBig" runat="server" AutoPostBack="True"
                            OnSelectedIndexChanged="ddlSupplierPartNo_SelectedIndexChanged">
                        </asp:DropDownList>
                    </td>
                </tr>
                <tr>
                    <td class="labelColSpan2" >
                        <asp:Label ID="lblOS_LS" SkinID="LabelNormal" runat="server" Text="OS/LS"></asp:Label>
                    </td>
                    <td class="inputcontrolsColSpan2">
                        <asp:DropDownList ID="ddlOS_LS" SkinID="DropDownListNormalBig" runat="server">
                            <asp:ListItem Value="0" Text=""></asp:ListItem>
                            <asp:ListItem Value="O" Text="OS"></asp:ListItem>
                            <asp:ListItem Value="L" Text="LS"></asp:ListItem>
                        </asp:DropDownList>
                    </td>
                </tr>
                <tr>
                    <td class="labelColSpan2" colspan="2">
                        <asp:Label ID="lblError" runat="server" SkinID="LabelNormal" ForeColor="Red"></asp:Label>
                    </td>
                </tr>
            </table>
            <div class="transactionButtons">
                <div class="transactionButtonsHolder">
                    <asp:Button ID="BtnSubmit" runat="server" SkinID="ButtonNormalBig" OnClientClick="return funOSLSSubmitValidation();" OnClick="BtnSubmit_Click"
                        Text="Submit" />
                    <asp:Button ID="btnReset" runat="server"  SkinID="ButtonNormalBig" OnClick="btnReset_Click"
                        Text="Reset" />
                </div>
            </div>
        </ContentTemplate>
    </asp:UpdatePanel>
  </div>
</asp:Content>
