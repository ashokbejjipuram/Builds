<%@ Page Title="" Language="C#" MasterPageFile="~/Main.Master" AutoEventWireup="true"
    CodeBehind="CustomerSlab.aspx.cs" Inherits="IMPALWeb.Masters.Others.CustomerSlab" %>

<%@ Register Namespace="AjaxControlToolkit" Assembly="AjaxControlToolkit" TagPrefix="ajax" %>
<asp:Content ID="Content1" ContentPlaceHolderID="CPHDetails" runat="server">
    <div id="DivOuter">
        <div class="subFormTitle">
            Customer Slab Master</div>
        <asp:UpdatePanel ID="UpdatePanel1" runat="server">
            <ContentTemplate>
                <table>
                    <tr>
                        <td>
                            <%--<asp:ObjectDataSource ID="ODSCustomerSlab" runat="server" 
                                InsertMethod="AddNewCustomerSlabMaster" oninserting="ODSCustomerSlab_Inserting" 
                                SelectMethod="GetAllCustomerSlabMasterList" 
                                TypeName="IMPALLibrary.CustomerSlabMaster" 
                                UpdateMethod="UpdateCustomerSlabMaster">
                                <UpdateParameters>
                                    <asp:Parameter Name="StateCode" Type="String" />
                                    <asp:Parameter Name="PartyTypeCode" Type="String" />
                                    <asp:Parameter Name="SlabCode" Type="String" />
                                    <asp:Parameter Name="SupplierLineCode" Type="String" />
                                </UpdateParameters>
                                <InsertParameters>
                                    <asp:Parameter Name="StateCode" Type="String" />
                                    <asp:Parameter Name="PartyTypeCode" Type="String" />
                                    <asp:Parameter Name="SlabCode" Type="String" />
                                    <asp:Parameter Name="SupplierLineCode" Type="String" />
                                </InsertParameters>
                            </asp:ObjectDataSource>--%>
                            <asp:ObjectDataSource ID="objDSStateList" runat="server" SelectMethod="GetStateList"
                                TypeName="IMPALLibrary.CustomerSlabMaster"></asp:ObjectDataSource>
                            <asp:ObjectDataSource ID="objDSPartyTypeList" runat="server" SelectMethod="GetPartyTypeList"
                                TypeName="IMPALLibrary.CustomerSlabMaster"></asp:ObjectDataSource>
                            <asp:ObjectDataSource ID="objDSSlabList" runat="server" SelectMethod="GetSlabList"
                                TypeName="IMPALLibrary.CustomerSlabMaster"></asp:ObjectDataSource>
                            <asp:ObjectDataSource ID="objDSSupplierLineList" runat="server" SelectMethod="GetSupplierLineList"
                                TypeName="IMPALLibrary.CustomerSlabMaster"></asp:ObjectDataSource>
                        </td>
                    </tr>
                    <tr>
                        <td>
                            <div class="subFormTitle">
                                CUSTOMER SLB</div>
                        </td>
                    </tr>
                    <tr>
                        <td class="label">
                            <asp:Label ID="lblStateCode" runat="server" Text="State" SkinID="LabelNormal"></asp:Label>
                        </td>
                        <td class="inputcontrols">
                            <asp:DropDownList ID="ddlState" runat="server" DataSourceID="objDSStateList" DataTextField="StateName"
                                DataValueField="StateCode" SkinID="DropDownListNormal"> 
                            </asp:DropDownList>
                        </td>
                    </tr>
                    <tr>
                        <td class="label">
                            <asp:Label ID="Label1" runat="server" Text="Party Type" SkinID="LabelNormal"></asp:Label>
                        </td>
                        <td class="inputcontrols">
                            <asp:DropDownList ID="ddlPartyType" runat="server" DataSourceID="objDSPartyTypeList"
                                DataTextField="PartyTypeName" DataValueField="PartyTypeCode" SkinID="DropDownListNormal">
                            </asp:DropDownList>
                        </td>
                    </tr>
                    <tr>
                        <td class="label">
                            <asp:Label ID="Label2" runat="server" Text="SLB" SkinID="LabelNormal"></asp:Label>
                        </td>
                        <td class="inputcontrols">
                            <asp:DropDownList ID="ddlSlab" runat="server" DataSourceID="objDSSlabList" DataTextField="SlabName"
                                DataValueField="SlabCode" SkinID="DropDownListNormal">
                            </asp:DropDownList>
                        </td>
                    </tr>
                    <tr>
                        <td class="label">
                            <asp:Label ID="Label3" runat="server" Text="Supplier line" SkinID="LabelNormal"></asp:Label>
                        </td>
                        <td class="inputcontrols">
                            <asp:DropDownList ID="ddlSupplierLine" runat="server" DataSourceID="objDSSupplierLineList"
                                DataTextField="SupplierLineName" DataValueField="SupplierLineCode" SkinID="DropDownListNormal">
                            </asp:DropDownList>
                        </td>
                    </tr>
                </table>
            </ContentTemplate>
        </asp:UpdatePanel>
        <div class="transactionButtons">
            <div class="transactionButtonsHolder">
                <asp:Button ID="BtnSubmit" runat="server" Text="Submit" SkinID="ButtonNormal" Enabled="false" />
                <asp:Button ID="btnReset" runat="server" Text="Reset" SkinID="ButtonNormal"  Enabled="false"/>
                <asp:Button ID="btnReport" runat="server" Text="Report" SkinID="ButtonNormal" Enabled="false"   />
            </div>
        </div>
    </div>
</asp:Content>
