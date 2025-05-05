<%@ Page Title="" Language="C#" MasterPageFile="~/Main.Master" AutoEventWireup="true"
    CodeBehind="SLB.aspx.cs" Inherits="IMPALWeb.SLB" %>

<asp:Content ID="CPHDetails" ContentPlaceHolderID="CPHDetails" runat="server">
    <div id="DivOuter">
        <div class="subFormTitle">
            SLB</div>
        <asp:UpdatePanel ID="UpdPnlSLB" runat="server">
            <ContentTemplate>
                <table>
                    <tr>
                        <td>
                            <asp:GridView ID="GV_SLB" runat="server" AutoGenerateColumns="False" DataSourceID="ODSSLB"
                                AllowPaging="True" HorizontalAlign="Left" BackColor="White" BorderStyle="None"
                                BorderWidth="1px" CellPadding="3" CaptionAlign="Left" OnDataBinding="GV_SLB_DataBinding"
                                ShowFooter="True" OnRowCommand="GV_SLB_RowCommand" SkinID="GridView" PageSize="25">
                                <Columns>
                                    <asp:TemplateField HeaderText="SLB Code" SortExpression="SLBCode">
                                        <EditItemTemplate>
                                            <asp:Label ID="lblSlbCode" runat="server" Text='<%# Bind("SLBCode") %>' Wrap="False"
                                                Width="100px"></asp:Label>
                                            <br />
                                        </EditItemTemplate>
                                        <FooterTemplate>
                                            <asp:TextBox ID="txtNewSlbCode" runat="server" Wrap="False" Width="125px" MaxLength="4"></asp:TextBox>
                                            <br />
                                            <asp:RequiredFieldValidator ID="rvNewSlbCode" runat="server" ControlToValidate="txtNewSlbCode"
                                                ErrorMessage="Please enter a valid SlbCode " ValidationGroup="SLBAddGroup">
                                            </asp:RequiredFieldValidator>
                                            <br />
                                            <asp:RegularExpressionValidator Display="Dynamic" ControlToValidate="txtNewSlbCode"
                                                ErrorMessage="Code must be Numeric" ID="regtxtNewSlbCode" runat="server" SetFocusOnError="true"
                                                ValidationGroup="SLBAddGroup" SkinID="GridViewLabelError" ValidationExpression="^[0-9]\d*(\.\d+)?$"></asp:RegularExpressionValidator>
                                        </FooterTemplate>
                                        <FooterStyle Wrap="False" />
                                        <ItemTemplate>
                                            <asp:Label ID="txtSlbCode" runat="server" Text='<%# Bind("SLBCode") %>'></asp:Label>
                                        </ItemTemplate>
                                    </asp:TemplateField>
                                    <asp:TemplateField HeaderText="Description">
                                        <EditItemTemplate>
                                            <asp:TextBox ID="txtEditSlbDesc" runat="server" Text='<%# Bind("Description") %>'
                                                Wrap="False"></asp:TextBox>
                                            <br />
                                            <asp:RequiredFieldValidator ID="rvEditSlbDesc" runat="server" ControlToValidate="txtEditSlbDesc"
                                                ErrorMessage="Enter Desc" ValidationGroup="SLBEditGroup">
                                            </asp:RequiredFieldValidator>
                                        </EditItemTemplate>
                                        <FooterTemplate>
                                            <%-- <asp:DropDownList ID="ddSLBGroup" runat="server" DataSourceID="ObjectDataSLB" DataTextField="Desc"
                                                DataValueField="Code" Width="214px">
                                            </asp:DropDownList>--%>
                                            <asp:TextBox ID="txtNewSlbDesc" runat="server" Text='<%# Bind("Description") %>'
                                                Wrap="False"></asp:TextBox>
                                            <br />
                                            <asp:RequiredFieldValidator ID="rvNewSlbDesc" runat="server" ControlToValidate="txtNewSlbDesc"
                                                ErrorMessage="Enter Desc" ValidationGroup="SLBAddGroup">
                                            </asp:RequiredFieldValidator>
                                        </FooterTemplate>
                                        <ItemTemplate>
                                            <asp:Label ID="lblDesc" runat="server" Text='<%# Bind("Description") %>'>
                                            </asp:Label>
                                        </ItemTemplate>
                                    </asp:TemplateField>
                                    <asp:TemplateField HeaderText="Indicator">
                                        <EditItemTemplate>
                                            <asp:RadioButton ID="rbNewQtyIndicator" runat="server" GroupName="Indicator" Text="Quantity"
                                                Checked="True" />
                                            <br />
                                            <asp:RadioButton ID="rbNewValueIndicator" runat="server" GroupName="Indicator" Text="Value" />
                                        </EditItemTemplate>
                                        <FooterTemplate>
                                            <asp:RadioButton ID="rbQtyIndicator" runat="server" GroupName="Indicator" Text="Quantity"
                                                Checked="True" />
                                            <br />
                                            <asp:RadioButton ID="rbValueIndicator" runat="server" GroupName="Indicator" Text="Value" />
                                        </FooterTemplate>
                                        <ItemTemplate>
                                            <asp:Label ID="lblIndicator" runat="server" Text='<%#Bind("Indicator") %>'>
                                            </asp:Label>
                                        </ItemTemplate>
                                    </asp:TemplateField>
                                    <asp:TemplateField HeaderText="Min Value" SortExpression="MinValue">
                                        <EditItemTemplate>
                                            <asp:TextBox ID="txtMinValue" runat="server" Text='<%#Bind("MinValue") %>' Wrap="False"
                                                Width="100px" OnTextChanged="txtMinValue_TextChanged"></asp:TextBox>
                                            <br />
                                            <asp:RequiredFieldValidator Display="Dynamic" SetFocusOnError="true" SkinID="GridViewLabelError"
                                                ID="rvMinValue" runat="server" ControlToValidate="txtMinValue" ErrorMessage="Enter Minimum Value"
                                                ValidationGroup="SLBEditGroup"> </asp:RequiredFieldValidator>
                                            <br />
                                            <asp:RegularExpressionValidator Display="Dynamic" ControlToValidate="txtMinValue"
                                                ErrorMessage="Minimum must be Numeric" ID="regtxtMinValue" runat="server" SetFocusOnError="true"
                                                ValidationGroup="SLBEditGroup" SkinID="GridViewLabelError" ValidationExpression="^[0-9]\d*(\.\d+)?$"></asp:RegularExpressionValidator>
                                        </EditItemTemplate>
                                        <FooterTemplate>
                                            <asp:TextBox ID="txtNewMinValue" runat="server" Wrap="False" Width="129px"></asp:TextBox>
                                            <br />
                                            <asp:RequiredFieldValidator Display="Dynamic" SetFocusOnError="true" SkinID="GridViewLabelError"
                                                ID="rvNewMinValue" runat="server" ControlToValidate="txtNewMinValue" ErrorMessage="Enter Minimum Value"
                                                ValidationGroup="SLBAddGroup"> </asp:RequiredFieldValidator>
                                            <asp:RegularExpressionValidator Display="Dynamic" ControlToValidate="txtNewMinValue"
                                                ErrorMessage="Minimum value must be Numeric" ID="regtxtNewMinValue" runat="server"
                                                SetFocusOnError="true" ValidationGroup="SLBAddGroup" SkinID="GridViewLabelError"
                                                ValidationExpression="^[0-9]\d*(\.\d+)?$"></asp:RegularExpressionValidator>
                                        </FooterTemplate>
                                        <FooterStyle Wrap="False" />
                                        <ItemTemplate>
                                            <asp:Label ID="lblMinValue" runat="server" Text='<%# Bind("MinValue") %>'></asp:Label>
                                        </ItemTemplate>
                                    </asp:TemplateField>
                                    <asp:TemplateField HeaderText="Max Value" SortExpression="MaxValue">
                                        <EditItemTemplate>
                                            <asp:TextBox ID="txtMaxValue" runat="server" Text='<%# Bind("MaxValue") %>' Wrap="False"
                                                Width="100px"></asp:TextBox>
                                            <br />
                                            <asp:RequiredFieldValidator Display="Dynamic" SetFocusOnError="true" SkinID="GridViewLabelError"
                                                ID="rvMaxValue" runat="server" ControlToValidate="txtMaxValue" ErrorMessage="Enter Maximum Value"
                                                ValidationGroup="SLBEditGroup"> </asp:RequiredFieldValidator>
                                            <asp:RegularExpressionValidator Display="Dynamic" ControlToValidate="txtMaxValue"
                                                ErrorMessage="Maximum value must be Numeric" ID="regtxtMaxValue" runat="server"
                                                SetFocusOnError="true" ValidationGroup="SLBEditGroup" SkinID="GridViewLabelError"
                                                ValidationExpression="^[0-9]\d*(\.\d+)?$"></asp:RegularExpressionValidator>
                                        </EditItemTemplate>
                                        <FooterTemplate>
                                            <asp:TextBox ID="txtNewMaxValue" runat="server" Wrap="False" Width="129px"></asp:TextBox>
                                            <br />
                                            <asp:RequiredFieldValidator Display="Dynamic" SetFocusOnError="true" SkinID="GridViewLabelError"
                                                ID="rvNewMaxValue" runat="server" ControlToValidate="txtNewMaxValue" ErrorMessage="Enter Maximum Value"
                                                ValidationGroup="SLBAddGroup"> </asp:RequiredFieldValidator>
                                            <br />
                                            <asp:RegularExpressionValidator Display="Dynamic" ControlToValidate="txtNewMaxValue"
                                                ErrorMessage="Maximum value must be Numeric" ID="regtxtMaxValue" runat="server"
                                                SetFocusOnError="true" ValidationGroup="SLBAddGroup" SkinID="GridViewLabelError"
                                                ValidationExpression="^[0-9]\d*(\.\d+)?$"></asp:RegularExpressionValidator>
                                        </FooterTemplate>
                                        <FooterStyle Wrap="False" />
                                        <ItemTemplate>
                                            <asp:Label ID="lblMaxValue" runat="server" Text='<%# Bind("MaxValue") %>'></asp:Label>
                                        </ItemTemplate>
                                    </asp:TemplateField>
                                    <asp:TemplateField ShowHeader="False">
                                        <ItemTemplate>
                                            <asp:LinkButton ID="btEdit" runat="server" CausesValidation="False" CommandName="Edit">
                                                <asp:Image ID="imgFolder" runat="server" ImageUrl="~/images/iGrid_Edit.png" />
                                            </asp:LinkButton>
                                        </ItemTemplate>
                                        <EditItemTemplate>
                                            <asp:LinkButton ID="btUpdate" runat="server" CausesValidation="True" CommandName="Update"
                                                ValidationGroup="SLBEditGroup">
                                                <asp:Image ID="imgFolder1" runat="server" ImageUrl="~/images/iGrid_Ok.png" />
                                            </asp:LinkButton>
                                            <asp:LinkButton ID="btCancel" runat="server" CausesValidation="False" CommandName="Cancel">
                                                <asp:Image ID="imgFolder2" runat="server" ImageUrl="~/images/iGrid_Cancel.png" />
                                            </asp:LinkButton>
                                        </EditItemTemplate>
                                        <FooterTemplate>
                                            <asp:Button ID="btAdd" runat="server" Text="Add" CommandName="Insert" ValidationGroup="SLBAddGroup" />
                                        </FooterTemplate>
                                    </asp:TemplateField>
                                </Columns>
                                <PagerStyle BackColor="#E7E7FF" ForeColor="#4A3C8C" HorizontalAlign="Right" />
                            </asp:GridView>
                        </td>
                    </tr>
                </table>
                <div class="transactionButtons">
                    <div class="transactionButtonsHolder">
                        <asp:Button SkinID="ButtonViewReport" ID="btnReport" runat="server" Text="Generate Report"
                            OnClick="btnReport_Click" />
                    </div>
                </div>
                <asp:ObjectDataSource ID="ODSSLB" runat="server" InsertMethod="AddNewSLBs" SelectMethod="GetALLSLBs"
                    TypeName="IMPALLibrary.SLBs" OnInserting="ODSSLB_Inserting" OnUpdating="ODSSLB_Updating"
                    UpdateMethod="UpdateSLBs">
                    <UpdateParameters>
                        <asp:Parameter Name="SLBCode" Type="String" />
                        <asp:Parameter Name="Description" Type="String" />
                        <asp:Parameter Name="Indicator" Type="String" />
                        <asp:Parameter Name="MinValue" Type="String" />
                        <asp:Parameter Name="MaxValue" Type="String" />
                    </UpdateParameters>
                    <InsertParameters>
                        <asp:Parameter Name="SLBCode" Type="String" />
                        <asp:Parameter Name="Description" Type="String" />
                        <asp:Parameter Name="Indicator" Type="String" />
                    </InsertParameters>
                </asp:ObjectDataSource>
                <asp:ObjectDataSource ID="ObjectDataSLB" runat="server" SelectMethod="GetALLSLBNames"
                    TypeName="IMPALLibrary.SLBGroups"></asp:ObjectDataSource>
            </ContentTemplate>
        </asp:UpdatePanel>
    </div>
</asp:Content>
