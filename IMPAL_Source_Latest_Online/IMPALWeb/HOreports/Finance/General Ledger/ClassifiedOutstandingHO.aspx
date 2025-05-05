<%@ Page Title="" Language="C#" MasterPageFile="~/Main.Master" AutoEventWireup="true"
    CodeBehind="ClassifiedOutstandingHO.aspx.cs" Inherits="IMPALWeb.Reports.Finance.General_Ledger.ClassifiedOutstandingHO" %>

<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="ajaxToolkit" %>
<asp:Content ID="Content1" ContentPlaceHolderID="CPHDetails" runat="server">
    <script language="javascript" type="text/javascript">
        function fnValidate() {
            var txtToDate = document.getElementById('<%=txtToDate.ClientID%>');
            //return ValidateDates(txtToDate, txtToDate);
        }

        function UnCheckBox(id) {
            var CtrlIdPrefix = "ctl00_CPHDetails_";
            document.getElementById(CtrlIdPrefix + 'chkZone').checked = false;
            document.getElementById(CtrlIdPrefix + 'chkState').checked = false;
            document.getElementById(CtrlIdPrefix + 'chkBranch').checked = false;

            if (id == CtrlIdPrefix + "ddlZone" || id == CtrlIdPrefix + "ddlState") {
                __doPostBack(id, 0);
            }

            return true;
        }
    </script>

    <div class="reportFormTitle">
        Classified Outstanding - HO
    </div>
    <div class="reportFilters">
        <table id="reportFiltersTable" class="reportFiltersTable" runat="server">
            <tr>
                <td class="label">
                    <asp:CheckBox ID="chkZone" runat="server" Text="" />
                    <asp:Label ID="lblZone" Text="Zone" runat="server" SkinID="LabelNormal"></asp:Label>
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
            <tr style="display: none">
                <td class="label">
                    <asp:Label ID="lblToDate" Text="To Date" SkinID="LabelNormal" runat="server"></asp:Label><span
                        class="asterix">*</span>
                </td>
                <td class="inputcontrols">
                    <asp:TextBox ID="txtToDate" SkinID="TextBoxCalendarExtenderNormal" TabIndex="2" runat="server" Enabled="false" Style="display: none"></asp:TextBox>
                    <ajaxToolkit:CalendarExtender ID="calTodate" Format="dd/MM/yyyy" runat="server" TargetControlID="txtToDate" Enabled="false">
                    </ajaxToolkit:CalendarExtender>
                </td>
            </tr>
        </table>
        <div class="reportButtons">
            <asp:Button SkinID="ButtonViewReport" ID="btnReport" runat="server" Text="Generate Report"
                TabIndex="3" OnClientClick="javaScript:return fnValidate();" OnClick="btnReport_Click" />
        </div>
    </div>
    <div class="reportViewerHolder" id="reportViewerHolder" runat="server">
    </div>
</asp:Content>
