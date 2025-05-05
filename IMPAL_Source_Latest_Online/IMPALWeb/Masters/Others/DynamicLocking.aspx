<%@ Page Title="" Language="C#" MasterPageFile="~/Main.Master" AutoEventWireup="true"
    CodeBehind="DynamicLocking.aspx.cs" Inherits="IMPALWeb.Masters.Others.DynamicLocking" %>

<%@ Register Namespace="AjaxControlToolkit" Assembly="AjaxControlToolkit" TagPrefix="ajax" %>
<%@ Register Src="~/UserControls/ItemCodePartNumber.ascx" TagName="ItemCodePartNumber"
    TagPrefix="User" %>
<asp:Content ID="Content1" ContentPlaceHolderID="CPHDetails" runat="server">
    <div>
        <div class="subFormTitle">
            Dynamic Locking</div>
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
                            <asp:Label ID="lblBranch" runat="server" Text="Branch" SkinID="LabelNormal"></asp:Label>
                        </td>
                        <td>
                            <asp:DropDownList ID="ddlBranch" runat="server" DataSourceID="ODSOPBranch" DataTextField="BranchName"
                                DataValueField="BranchCode" SkinID="DropDownListNormal">
                            </asp:DropDownList>
                        </td>
                    </tr>
                    <tr>
                        <td>
                            <asp:Label ID="lblSupplier" runat="server" Text="Supplier" SkinID="LabelNormal"></asp:Label>
                        </td>
                        <td>
                            <asp:DropDownList ID="ddlLineCode" runat="server" AutoPostBack="true" DataSourceID="objDSSupplierLineList"
                                DataTextField="SupplierLineName" DataValueField="SupplierLineCode" SkinID="DropDownListNormal">
                            </asp:DropDownList>
                        </td>
                    </tr>
                    <tr>
                        <td>
                            <asp:Label ID="Label1" runat="server" Text="Locking Days" SkinID="LabelNormal"></asp:Label>
                        </td>
                        <td>
                            <asp:TextBox ID="txtLockingDays" SkinID="TextBoxNormal" runat="server"></asp:TextBox>
                        </td>
                    </tr>
                    <tr>
                        <td>
                            <asp:Label ID="lblStateCode" runat="server" Text="Auto Lock" SkinID="LabelNormal"></asp:Label>
                        </td>
                        <td>
                            <asp:DropDownList ID="ddlAutoLoack" runat="server" SkinID="DropDownListNormal" Height="18px"
                                Width="120px">
                                <asp:ListItem Selected="True" Value="Y">Yes</asp:ListItem>
                                <asp:ListItem Value="N">No</asp:ListItem>
                            </asp:DropDownList>
                        </td>
                    </tr>
                </table>
            </ContentTemplate>
        </asp:UpdatePanel>
        <div class="transactionButtons">
            <div class="transactionButtonsHolder">
                <asp:Button ID="BtnSubmit" runat="server" Text="Submit" SkinID="ButtonNormal" Enabled="false" />
                <asp:Button ID="btnReset" runat="server" Text="Reset" SkinID="ButtonNormal" Enabled="false" />
                 <asp:Button ID="btnReport" runat="server" Text="Generate Report" SkinID="ButtonViewReport" 
                    onclick="btnReport_Click" />
            </div>
        </div>
    </div>
</asp:Content>
