<%@ Page Title="" Language="C#" MasterPageFile="~/Main.Master" AutoEventWireup="true" CodeBehind="iErrorPage.aspx.cs" Inherits="IMPALWeb.Errors.iErrorPage" %>
<asp:Content ID="cntErrorPage" ContentPlaceHolderID="CPHDetails" runat="server">
    <div class="errorContent" style="width:650px">
        <div class="issue">
            <h2>
                <asp:Label ID="lblErrorContentDisplay" runat="server" Text="Multiple Users are Running the Same Report. Please Re-Generate the Report in a While"></asp:Label>
            </h2>
            <h5>
                <asp:Label ID="lblErrorContentDisplaySubText" runat="server" Text="" Font-Bold="false"></asp:Label>
            </h5>               
        </div>
        <div class="clear-all"></div>
        <div class="contact">
            <h3>
                <asp:Label ID="lblErrorRightSubText" runat="server" Text="Please Contact HO."></asp:Label>
            </h3>     
        </div>
        <div class="clear-all"></div>
    </div>
    <div class="clear-all"></div>
</asp:Content>
