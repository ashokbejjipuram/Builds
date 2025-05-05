<%@ Page Language="C#" MasterPageFile="~/Main.Master" AutoEventWireup="true" CodeBehind="Restore.aspx.cs"
    Inherits="IMPALWeb.SysAdmin.Restore" %>

<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="ajaxToolkit" %>
<asp:Content ID="Content3" ContentPlaceHolderID="CPHDetails" runat="server">
    <asp:UpdatePanel ID="upHeader" runat="server" UpdateMode="Conditional">
        <ContentTemplate>
            <div id="DivTop" runat="server">
                <div>
                    <div align="center">
                        <h1>
                            RESTORE</h1>
                        <p class="inputcontrols">
                            <asp:TextBox ID="txtRestoreDate" runat="server" SkinID="TextBoxNormal" Enabled="false"></asp:TextBox>
                            <asp:ImageButton ID="imgResDate" ImageUrl="~/Images/Calendar.png" runat="server"
                                SkinID="ImageButtonCalendar" />
                            <ajaxToolkit:CalendarExtender ID="ceResDate" PopupButtonID="imgResDate" Format="dd/MM/yyyy"
                                runat="server" TargetControlID="txtRestoreDate" OnClientDateSelectionChanged="checkDate" />
                        </p>
                        <p>
                            <asp:Button ID="BtnSubmit" runat="server" ValidationGroup="BtnSubmit" SkinID="ButtonNormal"
                                CausesValidation="true" Text="Submit" OnClick="BtnSubmit_Click" />
                        </p>
                    </div>
                </div>
            </div>
        </ContentTemplate>
        <Triggers>
            <asp:AsyncPostBackTrigger ControlID="BtnSubmit" EventName="Click" />
        </Triggers>
    </asp:UpdatePanel>
</asp:Content>
