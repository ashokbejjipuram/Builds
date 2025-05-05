<%@ Page Title="" Language="C#" MasterPageFile="~/Main.Master" AutoEventWireup="true"
    CodeBehind="TrialBalanceHoLive.aspx.cs" Inherits="IMPALWeb.Reports.Finance.General_Ledger.TrialBalanceHoLive" %>

<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="ajaxToolkit" %>
<asp:Content ID="Content1" ContentPlaceHolderID="CPHDetails" runat="server">

    <script type="text/javascript">
        function Validate() {
            var fromdate = document.getElementById('<%=txtFromDate.ClientID %>');
            var todate = document.getElementById('<%=txtToDate.ClientID %>');
            var flag = ValidateDates(fromdate, todate);
            return (flag);
            
        }
        
        function UnCheckBox(id) {
            var CtrlIdPrefix = "ctl00_CPHDetails_";
            document.getElementById(CtrlIdPrefix + 'chkZone').checked = false;
            document.getElementById(CtrlIdPrefix + 'chkState').checked = false;
            document.getElementById(CtrlIdPrefix + 'chkBranch').checked = false;
           
           if (id == CtrlIdPrefix + "ddlZone" || id == CtrlIdPrefix + "ddlState"){
                __doPostBack(id, 0);
           }
            
            return true;
        }
    </script>

    <div class="reportFormTitle">
        Trial Balance - HO - Live
    </div>
    <div class="reportFilters">
        <table id="reportFiltersTable" class="reportFiltersTable" runat="server">
            <tr>
                <td class="label">
                    <asp:CheckBox ID="chkZone" runat="server" Text="" />
                    <asp:Label ID="Label1" Text="Zone" runat="server" SkinID="LabelNormal"></asp:Label>
                    <span class="asterix">*</span>
                </td>
                <td class="inputcontrols">
                    <asp:DropDownList ID="ddlZone" runat="server" DataTextField="ZoneName" DataValueField="ZoneCode"
                        TabIndex="1" OnSelectedIndexChanged="ddlZone_OnSelectedIndexChanged" AutoPostBack="True"
                        Enabled="false" SkinID="DropDownListNormal" onchange="return UnCheckBox(id);" />
                </td>
                <td class="label">
                    <asp:CheckBox ID="chkState" runat="server" Text="" />
                    <asp:Label ID="lblState" Text="State" runat="server" SkinID="LabelNormal"></asp:Label>
                    <span class="asterix">*</span>
                </td>
                <td class="inputcontrols">
                    <asp:DropDownList ID="ddlState" runat="server" DataTextField="StateName" DataValueField="StateCode"
                        OnSelectedIndexChanged="ddlState_OnSelectedIndexChanged" TabIndex="2" Enabled="false"
                        AutoPostBack="True" SkinID="DropDownListNormal" onchange="return UnCheckBox(id);" />
                </td>
                <td class="label">
                    <asp:CheckBox ID="chkBranch" runat="server" Text="" />
                    <asp:Label ID="lblBranch" Text="Branch" runat="server" SkinID="LabelNormal"></asp:Label>
                    <span class="asterix">*</span>
                </td>
                <td class="inputcontrols">
                    <asp:DropDownList ID="ddlBranch" runat="server" DataTextField="BranchName" DataValueField="BranchCode"
                        TabIndex="3" Enabled="false" SkinID="DropDownListNormal" onchange="return UnCheckBox(id);" />
                </td>
            </tr>
            <tr>
                <td class="label">
                    <asp:Label ID="lblFromDate" runat="server" Text="From Date" SkinID="LabelNormal"></asp:Label>
                    <span class="asterix">*</span>
                </td>
                <td class="inputcontrols">
                    <asp:TextBox ID="txtFromDate" runat="server" SkinID="TextBoxCalendarExtenderNormal"
                        TabIndex="1" />
                    <ajaxToolkit:CalendarExtender ID="calFromDate" runat="server" TargetControlID="txtFromDate"
                        Format="dd/MM/yyyy" />
                </td>
                <td class="label">
                    <asp:Label ID="lblToDate" runat="server" Text="To Date" SkinID="LabelNormal"></asp:Label>
                    <span class="asterix">*</span>
                </td>
                <td class="inputcontrols">
                    <asp:TextBox ID="txtToDate" runat="server" SkinID="TextBoxCalendarExtenderNormal"
                        TabIndex="2" />
                    <ajaxToolkit:CalendarExtender ID="calToDate" runat="server" TargetControlID="txtToDate"
                        Format="dd/MM/yyyy" />
                </td>
                <td class="label">
                    <asp:Label ID="lblReportType" runat="server" Text="Report Type" SkinID="LabelNormal"></asp:Label>
                </td>
                <td class="inputcontrols">
                    <asp:DropDownList ID="ddlReportType" runat="server" TabIndex="3" SkinID="DropDownListNormal">
                    </asp:DropDownList>
                </td>
            </tr>
        </table>
        <div class="reportButtons">
            <asp:Button ID="btnReport" Text="Generate Report" runat="server" SkinID="ButtonViewReport"
                TabIndex="4" OnClientClick="javaScript:return Validate();" OnClick="btnReport_Click" />
        </div>
    </div>
    <div class="reportViewerHolder" id="reportViewerHolder" runat="server">        
    </div>
</asp:Content>
