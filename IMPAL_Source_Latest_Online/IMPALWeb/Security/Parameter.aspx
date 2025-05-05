<%@ Page Language="C#" MasterPageFile="~/Main.Master" AutoEventWireup="true" CodeBehind="Parameter.aspx.cs"
    Inherits="IMPALWeb.Security.Parameter" %>

<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="ajaxToolkit" %>
<asp:Content ID="Content3" ContentPlaceHolderID="CPHDetails" runat="server">

    <script language="javascript" type="text/javascript">


        function checkDate(sender, args) {


            var strSender = sender._id;
            var idToDate = strSender.replace("ceToDate", "txtToDate");
            var idFromDate = strSender.replace("ceToDate", "txtFromDate");
            var fDate = document.getElementById(idFromDate).value.split("/");

            var day = parseInt(fDate[0]);
            var month = parseInt(fDate[1] - 1);
            var year = parseInt(fDate[2]);

            var frmDate = new Date();
            frmDate.setDate(day);
            frmDate.setMonth(month);
            frmDate.setFullYear(year);

            if (sender._selectedDate < frmDate) {
                alert("To Date should  be greater than From Date");
                document.getElementById(idToDate).value = "";
            }

        }
    </script>

    <asp:UpdatePanel ID="upHeader" runat="server" UpdateMode="Conditional">
        <ContentTemplate>
            <div id="DivTop" runat="server">
                <div>
                    <table class="subFormTable" style="float: right !important; margin-right: 400px !important;">
                        <tr>
                            <td>
                                <div class="subFormTitle">
                                    PARAMETER
                                </div>
                            </td>
                        </tr>
                        <tr>
                            <td colspan="2" class="label">
                                <asp:Label ID="lblHeaderMessage" Text="" runat="server" SkinID="Error"></asp:Label>
                            </td>
                        </tr>
                        <tr>
                            <td class="label">
                                <asp:Label ID="lblFrom" runat="server" Text="From Date" SkinID="LabelNormal"></asp:Label>
                            </td>
                            <td class="label">
                                <asp:TextBox ID="txtFromDate" runat="server" SkinID="TextBoxNormal" Enabled="false"></asp:TextBox>
                                <%-- <asp:ImageButton ID="imgFromDate" ImageUrl="~/Images/Calendar.png" runat="server"
                                    SkinID="ImageButtonCalendar" />
                                <ajaxToolkit:CalendarExtender ID="ceFromDate" PopupButtonID="imgFromDate" Format="dd/MM/yyyy"
                                    runat="server" TargetControlID="txtFromDate" />--%>
                            </td>
                        </tr>
                        <tr>
                            <td class="label">
                                <asp:Label ID="lblToDate" runat="server" Text="To Date" SkinID="LabelNormal"></asp:Label>
                            </td>
                            <td class="label">
                                <asp:TextBox ID="txtToDate" runat="server" SkinID="TextBoxNormal" Enabled="false"></asp:TextBox>
                                <%--<asp:ImageButton ID="ImgToDate" ImageUrl="~/Images/Calendar.png" runat="server" SkinID="ImageButtonCalendar" />
                                <ajaxToolkit:CalendarExtender ID="ceToDate" PopupButtonID="imgToDate" Format="dd/MM/yyyy"
                                    runat="server" TargetControlID="txtToDate" OnClientDateSelectionChanged="checkDate" />--%>
                            </td>
                        </tr>
                        <tr style="float: right !important; margin-right: -35px !important;">
                            <td class="label">
                                <asp:Label ID="lblError" runat="server" SkinID="LabelNormal" ForeColor="Red" ></asp:Label>
                            </td>
                            
                        </tr>
                        <tr style="float: right !important; margin-right: 0px !important;">
                            <td>
                                <asp:Button ID="BtnSubmit" runat="server" ValidationGroup="BtnSubmit" SkinID="ButtonNormal"
                                    CausesValidation="true" Text="Submit" OnClick="BtnSubmit_Click" />
                            </td>
                        </tr>
                    </table>
                </div>
            </div>
        </ContentTemplate>
        <Triggers>
            <asp:AsyncPostBackTrigger ControlID="BtnSubmit" EventName="Click" />
        </Triggers>
    </asp:UpdatePanel>
</asp:Content>
