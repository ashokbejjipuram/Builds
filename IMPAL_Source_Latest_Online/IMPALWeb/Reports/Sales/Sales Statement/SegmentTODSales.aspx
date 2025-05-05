<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="SegmentTODSales.aspx.cs" MasterPageFile="~/Main.Master"
    Inherits="IMPALWeb.Reports.Sales.SalesStatement.SegmentTODSales" %>

<%@ Register Src="~/UserControls/CrystalReport.ascx" TagName="CrystalReport" TagPrefix="uc" %>
<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="ajaxToolkit" %>
<asp:Content ID="Content" ContentPlaceHolderID="CPHDetails" runat="server">

    <script language="javascript" type="text/javascript">
        function Validate() {
            var txtFromDate = document.getElementById('<%=txtFromDate.ClientID%>');
            var txtToDate = document.getElementById('<%=txtToDate.ClientID%>');
            var hidFromDate = document.getElementById('<%=hidFromDate.ClientID%>');
            var hidToDate = document.getElementById('<%=hidToDate.ClientID%>');
            return ValidateDate(txtFromDate, txtToDate, hidFromDate, hidToDate);
        }
    </script>

    <div class="reportFormTitle reportFormTitleExtender250">
        Customer/Segement/Date Wise
    </div>
    <div class="reportFilters">
        <table id="reportFiltersTable" class="reportFiltersTable" runat="server">
            <tr>
                <td>
                    <table id="reportFiltersTable1" class="reportFiltersTable" runat="server">
                        <tr>
                            <td class="label">
                                <asp:Label ID="lblCustomer" runat="server" Text="Customer" SkinID="LabelNormal"></asp:Label>
                            </td>
                            <td class="inputcontrols">
                                <asp:DropDownList ID="cbCustomerName" runat="server" AutoPostBack="true" DropDownStyle="DropDownList"
                                    SkinID="DropDownListNormal" TabIndex="1" OnSelectedIndexChanged="cbCustomerName_SelectedIndexChanged">
                                </asp:DropDownList>
                            </td>
                            <td class="label">
                                <asp:Label ID="lblPlant" runat="server" Text="Segment" SkinID="LabelNormal"></asp:Label>
                            </td>
                            <td class="inputcontrols">
                                <asp:DropDownList ID="ddlPlant" runat="server" SkinID="DropDownListNormal" TabIndex="2" DataTextField="DisplayText" DataValueField="DisplayValue">
                                </asp:DropDownList>
                            </td>
                        </tr>
                        <tr>
                        <td style="height:7px"></td>
                        </tr>
                        <tr>
                            <td class="label">
                                <asp:Label ID="lblFromDate" Text="From Date" runat="server" SkinID="LabelNormal"></asp:Label>
                                <span class="asterix">*</span>
                            </td>
                            <td class="inputcontrols">
                                <asp:TextBox ID="txtFromDate" runat="server" SkinID="TextBoxCalendarExtenderNormal"
                                    TabIndex="3"></asp:TextBox>
                                <ajaxToolkit:CalendarExtender ID="calFromdate" Format="dd/MM/yyyy" runat="server"
                                    TargetControlID="txtFromDate">
                                </ajaxToolkit:CalendarExtender>
                                <asp:HiddenField ID="hidFromDate" runat="server" />
                            </td>
                            <td class="label">
                                <asp:Label ID="lblToDate" Text="To Date" runat="server" SkinID="LabelNormal"></asp:Label>
                                <span class="asterix">*</span>
                            </td>
                            <td class="inputcontrols">
                                <asp:TextBox ID="txtToDate" runat="server" SkinID="TextBoxCalendarExtenderNormal"
                                    TabIndex="4"></asp:TextBox>
                                <ajaxToolkit:CalendarExtender ID="calTodate" Format="dd/MM/yyyy" runat="server" TargetControlID="txtToDate">
                                </ajaxToolkit:CalendarExtender>
                                <asp:HiddenField ID="hidToDate" runat="server" />
                            </td>                            
                        </tr>
                    </table>
                    <div id="divCustomerInfo" style="display: none" runat="server">
                        <div class="reportFormTitle">
                            Customer Information</div>
                        <table class="reportFiltersTable">
                            <tr>
                                <td class="label">
                                    <asp:Label runat="server" ID="lblCustomerCode" Text="Customer Code" SkinID="LabelNormal" />
                                </td>
                                <td class="inputcontrols">
                                    <asp:TextBox ID="txtCustomerCode" runat="server" SkinID="TextBoxCalendarExtenderNormal"
                                        ReadOnly="true" />
                                </td>
                                <td class="label">
                                    <asp:Label Text="Address1" SkinID="LabelNormal" runat="server" ID="lblAddress1" />
                                </td>
                                <td class="inputcontrols">
                                    <asp:TextBox ID="txtAddress1" runat="server" SkinID="TextBoxCalendarExtenderNormal"
                                        ReadOnly="true" />
                                </td>
                                <td class="label">
                                    <asp:Label runat="server" ID="lblAddress2" Text="Address2" SkinID="LabelNormal" />
                                </td>
                                <td class="inputcontrols">
                                    <asp:TextBox ID="txtAddress2" runat="server" SkinID="TextBoxCalendarExtenderNormal"
                                        ReadOnly="true" />
                                </td>
                            </tr>
                            <tr>
                                <td class="label">
                                    <asp:Label runat="server" ID="lblAddress3" Text="Address3" SkinID="LabelNormal" />
                                </td>
                                <td class="inputcontrols">
                                    <asp:TextBox ID="txtAddress3" runat="server" SkinID="TextBoxCalendarExtenderNormal"
                                        ReadOnly="true" />
                                </td>
                                <td class="label">
                                    <asp:Label Text="Address4" SkinID="LabelNormal" runat="server" ID="lblAddress4" />
                                </td>
                                <td class="inputcontrols">
                                    <asp:TextBox ID="txtAddress4" runat="server" SkinID="TextBoxCalendarExtenderNormal"
                                        ReadOnly="true" />
                                </td>
                                <td class="label">
                                    <asp:Label runat="server" ID="lblLocation" Text="Location" SkinID="LabelNormal" />
                                </td>
                                <td class="inputcontrols">
                                    <asp:TextBox ID="txtLocation" runat="server" SkinID="TextBoxCalendarExtenderNormal"
                                        ReadOnly="true" />
                                </td>
                            </tr>
                        </table>
                    </div>
                </td>
            </tr>
        </table>
        <div class="reportButtons">
            <asp:Button ID="btnReport" runat="server" OnClick="btnReport_Click" OnClientClick="javaScript:return Validate();"
                Text="Generate Report" SkinID="ButtonViewReport" TabIndex="6" />
        </div>
    </div>
    <div class="reportViewerHolder" id="reportViewerHolder" runat="server">
        <uc:CrystalReport ID="crSegmentTOD" runat="server" OnUnload="crSegmentTOD_Unload" ReportName="Segmentwise-TOD" />
    </div>
</asp:Content>
