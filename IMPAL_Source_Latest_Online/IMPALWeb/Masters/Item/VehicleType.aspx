<%@ Page Title="" Language="C#" MasterPageFile="~/Main.Master" AutoEventWireup="true"
    CodeBehind="VehicleType.aspx.cs" Inherits="IMPALWeb.VehicleType" %>

<%@ Register Namespace="AjaxControlToolkit" Assembly="AjaxControlToolkit" TagPrefix="ajax" %>
<asp:Content ID="CPHDetails" ContentPlaceHolderID="CPHDetails" runat="server">

    <script type="text/javascript">
        function validateFields(Id, Desc) {
            var TxtGLAcctCode = Id.value;
            if (TxtGLAcctCode != "") {
                firstchr = TxtGLAcctCode.substring(0, 1);
                if (isspecialchar(firstchr)) {
                    alert("First character of " + Desc + " field should be Alphabet or Number");
                    Id.value = "";
                    Id.focus();
                    return false;
                }
                else if (firstchr == " ") {
                    alert("First character of " + Desc + " field should not be blank");
                    Id.value = "";
                    Id.focus();
                    return false;
                }
                else {
                    return true;
                }
            }
        }

    </script>

    <div id="DivOuter">
        <div class="subFormTitle">
            Vehicle Type
        </div>
        <asp:UpdatePanel ID="UpdatePanel1" runat="server">
            <ContentTemplate>
                <table>
                    <tr>
                        <td>
                            <asp:GridView ID="GV_VehicleType" runat="server" AutoGenerateColumns="False" DataSourceID="ObjectDataVT"
                                OnDataBinding="GV_VehicleType_DataBinding" OnSelectedIndexChanged="GV_VehicleType_SelectedIndexChanged"
                                OnRowCommand="GV_VehicleType_RowCommand" OnRowUpdating="GV_VehicleType_RowUpdating"
                                PageSize="20" SkinID="GridView">
                                <EmptyDataTemplate>
                                    <asp:Label ID="lblEmpty" runat="server" SkinID="GridViewLabel" Text="No Rows Returned"></asp:Label>
                                </EmptyDataTemplate>
                                <Columns>
                                    <asp:TemplateField HeaderText="Code">
                                        <ItemTemplate>
                                            <asp:Label ID="lblVehicleTypeCode" runat="server" Text='<%# Bind("VehicleTypeCode") %>'
                                                SkinID="GridViewLabel"></asp:Label>
                                        </ItemTemplate>
                                        <FooterTemplate>
                                            <asp:TextBox ID="txtNewVechileTypeCode" MaxLength="6" runat="server" Wrap="False"
                                                onblur="validateFields(this, 'Code');" SkinID="GridViewTextBoxFooter"></asp:TextBox>
                                            <br />
                                            <asp:RequiredFieldValidator ID="rvNewVechileTypeCode" runat="server" ControlToValidate="txtNewVechileTypeCode"
                                                ErrorMessage="Please Enter Code" ValidationGroup="VTAddGroup" Display="Dynamic"
                                                SkinID="GridViewLabelError" SetFocusOnError="true">
                                            </asp:RequiredFieldValidator>
                                        </FooterTemplate>
                                    </asp:TemplateField>
                                    <asp:TemplateField HeaderText="Description">
                                        <ItemTemplate>
                                            <asp:Label ID="lblVehicleTypeDescription" runat="server" Text='<%# Bind("VehicleTypeDescription") %>'
                                                SkinID="GridViewLabel"></asp:Label>
                                        </ItemTemplate>
                                        <EditItemTemplate>
                                            <asp:TextBox ID="txtVehicleTypeDescription" runat="server" Text='<%# Bind("VehicleTypeDescription") %>'
                                                onblur="validateFields(this, 'Description');" MaxLength="40" Width="241px" SkinID="GridViewTextBox"></asp:TextBox>
                                            <br />
                                            <asp:RequiredFieldValidator ID="rvVehicleTypeDescription" Display="Dynamic" runat="server"
                                                SetFocusOnError="true" ControlToValidate="txtVehicleTypeDescription" ErrorMessage="Please Enter VehicleType Description"
                                                ValidationGroup="VTEditGroup" SkinID="GridViewLabelError">
                                            </asp:RequiredFieldValidator>
                                        </EditItemTemplate>
                                        <FooterTemplate>
                                            <asp:TextBox Width="241px" ID="txtNewVehicleTypeDescription" runat="server" MaxLength="40"
                                                onblur="validateFields(this, 'Description');" SkinID="GridViewTextBoxFooter"></asp:TextBox>
                                            <br />
                                            <asp:RequiredFieldValidator ID="rvNewVehicleTypeDesc" runat="server" ControlToValidate="txtNewVehicleTypeDescription"
                                                ErrorMessage="Please Enter VehicleType Description" ValidationGroup="VTAddGroup"
                                                Display="Dynamic" SetFocusOnError="true" SkinID="GridViewLabelError">
                                            </asp:RequiredFieldValidator>
                                        </FooterTemplate>
                                    </asp:TemplateField>
                                    <asp:TemplateField ShowHeader="False">
                                        <ItemTemplate>
                                            <asp:LinkButton ID="btEdit" runat="server" CausesValidation="False" CommandName="Edit"
                                                SkinID="GridViewLinkButton">
                                                <asp:Image ID="imgFolder" runat="server" ImageUrl="~/images/iGrid_Edit.png" />
                                            </asp:LinkButton>
                                        </ItemTemplate>
                                        <EditItemTemplate>
                                            <asp:LinkButton ID="btUpdate" runat="server" CausesValidation="True" CommandName="Update"
                                                ValidationGroup="VTEditGroup">
                                                <asp:Image ID="imgFolder1" runat="server" ImageUrl="~/images/iGrid_Ok.png" />
                                            </asp:LinkButton>
                                            <asp:LinkButton ID="btCancel" runat="server" CausesValidation="False" CommandName="Cancel">
                                                <asp:Image ID="imgFolder2" runat="server" ImageUrl="~/images/iGrid_Cancel.png" SkinID="GridViewImageEdit" />
                                            </asp:LinkButton>
                                        </EditItemTemplate>
                                        <FooterTemplate>
                                            <asp:Button ID="btAdd" runat="server" Text="Add" CommandName="Insert" ValidationGroup="VTAddGroup"
                                                SkinID="GridViewButtonFooter" />
                                        </FooterTemplate>
                                    </asp:TemplateField>
                                </Columns>
                            </asp:GridView>
                        </td>
                    </tr>
                </table>
                <asp:ObjectDataSource ID="ObjectDataVT" runat="server" InsertMethod="AddNewVehilcleType"
                    SelectMethod="GetAllVehilcleTypes" TypeName="IMPALLibrary.VehilcleTypes" OnInserting="ODSVT_Inserting"
                    UpdateMethod="UpdateVehilcleType">
                    <UpdateParameters>
                        <asp:Parameter Name="VehicleTypeCode" Type="String" />
                        <asp:Parameter Name="VehicleTypeDescription" Type="String" />
                    </UpdateParameters>
                    <InsertParameters>
                        <asp:Parameter Name="VehicleTypeCode" Type="String" />
                        <asp:Parameter Name="VehicleTypeDescription" Type="String" />
                    </InsertParameters>
                </asp:ObjectDataSource>
            </ContentTemplate>
        </asp:UpdatePanel>
        <div class="transactionButtons">
            <div class="transactionButtonsHolder">
                <asp:Button SkinID="ButtonViewReport" ID="btnReport" runat="server" Text="Generate Report"
                    OnClick="btnReport_Click" />
            </div>
        </div>
    </div>
</asp:Content>
