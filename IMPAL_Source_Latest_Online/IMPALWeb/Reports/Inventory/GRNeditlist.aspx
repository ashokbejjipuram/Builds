<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="GRNeditlist.aspx.cs"
    Inherits="IMPALWeb.Reports.Inventory.GRNeditlist" MasterPageFile="~/Main.Master" %>

<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="ajaxToolkit" %>
<asp:Content ID="MainContent" ContentPlaceHolderID="CPHDetails" runat="server">

    <script language="javascript" type="text/javascript">
        function Validate() {        
//            var SuppCode = document.getElementById('<%=ddlSupplier.ClientID%>');
//            
//            if (SuppCode.selectedIndex <= 0) {
//                alert("Supplier Code should not be null");
//                SuppCode.focus();
//                return false;
//            }
            
            var txtFromDate = document.getElementById('<%=txtFromDate.ClientID%>');
            var txtToDate = document.getElementById('<%=txtToDate.ClientID%>');
            var hidFromDate = document.getElementById('<%=hidFromDate.ClientID%>');
            var hidToDate = document.getElementById('<%=hidToDate.ClientID%>');
            return ValidateDate(txtFromDate, txtToDate, hidFromDate, hidToDate);                    
        }        
    </script>

    <div class="reportFormTitle">
        GRN Edit List</div>
    <div class="reportFilters">
        <table id="reportFiltersTable" class="reportFiltersTable" runat="server">
            <tr>
                <td class="label">
                    <asp:Label ID="Label12" runat="server" SkinID="LabelNormal" Text="Branch"></asp:Label>
                    <span class="asterix">*</span>
                </td>
                <td class="inputcontrols">
                    <asp:DropDownList ID="ddlBranch" runat="server" SkinID="DropDownListDisabled"
                        DataSourceID="ODS_AllBranch" DataTextField="BranchName" DataValueField="BranchCode">
                    </asp:DropDownList>
                </td>
                <td class="label">
                    <asp:Label ID="lblSupplierName" Text="Supplier Name" runat="server" SkinID="LabelNormal"></asp:Label>
                </td>
                <td class="inputcontrols">
                    <asp:DropDownList ID="ddlSupplier" runat="server" TabIndex="3" SkinID="DropDownListNormal"
                        DataSourceID="ODLine" DataTextField="SupplierName" DataValueField="SupplierCode" />                    
                </td>
                <td class="label">
                
                    <asp:Label ID="lblFromDate" Text="From Date" runat="server" SkinID="LabelNormal"></asp:Label>
                    <span class="asterix">*</span>
                </td>
                <td class="inputcontrols">
                    <asp:TextBox ID="txtFromDate" runat="server" TabIndex="1" SkinID="TextBoxCalendarExtenderNormal" />
                    <asp:HiddenField ID="hidFromDate" runat="server" />
                    <ajaxToolkit:CalendarExtender ID="calFromDate" runat="server" TargetControlID="txtFromDate"
                        Format="dd/MM/yyyy" />
                </td>
                <td class="label">
                    <asp:Label ID="lblToDate" Text="To Date" runat="server" SkinID="LabelNormal"></asp:Label>
                    <span class="asterix">*</span>
                </td>
                <td class="inputcontrols">
                    <asp:TextBox ID="txtToDate" runat="server" TabIndex="2" SkinID="TextBoxCalendarExtenderNormal" />
                    <asp:HiddenField ID="hidToDate" runat="server" />
                    <ajaxToolkit:CalendarExtender ID="calToDate" runat="server" TargetControlID="txtToDate"
                        Format="dd/MM/yyyy" />
                </td>
            </tr>
        </table>
        <asp:ObjectDataSource ID="ODS_AllBranch" runat="server" SelectMethod="GetAllBranch"
            TypeName="IMPALLibrary.Branches" DataObjectTypeName="IMPALLibrary.Branches">
        </asp:ObjectDataSource>
        <asp:ObjectDataSource ID="ODLine" runat="server" SelectMethod="GetInventoryBasedSupplier" TypeName="IMPALLibrary.Suppliers">
            <SelectParameters>
                <asp:ControlParameter Name="strBranchCode" ControlID="ddlBranch" />
            </SelectParameters>
        </asp:ObjectDataSource>
        <div class="reportButtons">
            <asp:Button ID="btnReport" runat="server" OnClick="btnReport_Click" OnClientClick="javaScript:return Validate();"
                Text="Generate Report" TabIndex="5" SkinID="ButtonViewReport"></asp:Button>
        </div>
    </div>
    <div class="reportViewerHolder" id="reportViewerHolder" runat="server">
    </div>
</asp:Content>
