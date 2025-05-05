<%@ Page Title="" Language="C#" MasterPageFile="~/Main.Master" AutoEventWireup="true"
    CodeBehind="GSTInwardRegisterHO.aspx.cs" Inherits="IMPALWeb.Reports.Finance.GSTReports.GSTInwardRegisterHO" %>

<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="ajaxToolkit" %>
<asp:Content ID="Content1" ContentPlaceHolderID="CPHDetails" runat="server">

    <script language="javascript" type="text/javascript">
        function Validate(e) {

            var txtFromDate = document.getElementById('<%=txtFromDate.ClientID%>');
            var txtToDate = document.getElementById('<%=txtToDate.ClientID%>');
            return ValidateDatesGST(txtFromDate, txtToDate);
        }

        function ValidateDatesGST(txtFromDate, txtToDate) {
            var oFromDateVal = txtFromDate.value.trim();
            var oToDateVal = txtToDate.value.trim();
            var oSysDate = new Date();
            var oFromDate = oFromDateVal.split("/");
            var oToDate = oToDateVal.split("/");
            var oFromDateFormatted = oFromDate[1] + "/" + oFromDate[0] + "/" + oFromDate[2];
            var oToDateFormatted = oToDate[1] + "/" + oToDate[0] + "/" + oToDate[2];

            if (oFromDateVal == null || oFromDateVal == "") {
                alert("From Date should not be null!");
                txtFromDate.focus();
                return false;
            }
            else if (oToDateVal == null || oToDateVal == "") {
                alert("To Date should not be null!");
                txtToDate.focus();
                return false;
            }
            else if (oFromDateVal != null && fnIsDate(txtFromDate.value) == false) {
                txtFromDate.value = "";
                txtFromDate.focus();
                return false;

            }
            else if (oToDateVal != null && fnIsDate(txtToDate.value) == false) {
                txtToDate.value = "";
                txtToDate.focus();
                return false;
            }
            else if (oSysDate < new Date(oFromDateFormatted)) {
                alert("From Date should not be greater than System Date");
                txtFromDate.value = "";
                txtFromDate.focus();
                return false;
            }
            else if (oSysDate < new Date(oToDateFormatted)) {
                alert("To Date should not be greater than System Date");
                txtToDate.value = "";
                txtToDate.focus();
                return false;
            }
            else if (new Date(oFromDateFormatted) > new Date(oToDateFormatted)) {
                alert("To Date should be greater than or equal to From Date");
                txtToDate.value = "";
                txtToDate.focus();
                return false;
            }
            else if (new Date(oFromDateFormatted) <= new Date('6/30/2017')) {
                alert("From Date should be greater than or equal to 1st July 2017");
                txtFromDate.value = "";
                txtFromDate.focus();
                return false;
            }
            else if (new Date(oToDateFormatted) <= new Date('6/30/2017')) {
                alert("To Date should be greater than or equal to 1st July 2017");
                txtToDate.value = "";
                txtToDate.focus();
                return false;
            }
        }

        function UnCheckBox(id) {
            var CtrlIdPrefix = "ctl00_CPHDetails_";
            //document.getElementById(CtrlIdPrefix + 'chkZone').checked = false;
            //document.getElementById(CtrlIdPrefix + 'chkState').checked = false;
            //document.getElementById(CtrlIdPrefix + 'chkBranch').checked = false;
           
           if (id == CtrlIdPrefix + "ddlZone" || id == CtrlIdPrefix + "ddlState"){
                __doPostBack(id, 0);
           }
            
            return true;
        }
    </script>

    <div class="reportFormTitle">
        GST INWARD REGISTER
    </div>
    <div class="reportFilters">
        <table id="reportFiltersTable" class="reportFiltersTable" runat="server">
            <tr>
                <td class="label">
                    <asp:CheckBox ID="chkZone" runat="server" Text="" Visible="false" />
                    <asp:Label ID="lblZone" Text="Zone" runat="server" SkinID="LabelNormal"></asp:Label>
                    <span class="asterix">*</span>
                </td>
                <td class="inputcontrols">
                    <asp:DropDownList ID="ddlZone" runat="server" DataTextField="ZoneName" DataValueField="ZoneCode"
                        TabIndex="1" OnSelectedIndexChanged="ddlZone_OnSelectedIndexChanged" AutoPostBack="True"
                        Enabled="false" SkinID="DropDownListNormal" onchange="return UnCheckBox(id);" />
                </td>
                <td class="label">
                    <asp:CheckBox ID="chkState" runat="server" Text="" Visible="false" />
                    <asp:Label ID="lblState" Text="State" runat="server" SkinID="LabelNormal"></asp:Label>
                    <span class="asterix">*</span>
                </td>
                <td class="inputcontrols">
                    <asp:DropDownList ID="ddlState" runat="server" DataTextField="StateName" DataValueField="StateCode"
                        OnSelectedIndexChanged="ddlState_OnSelectedIndexChanged" TabIndex="2" Enabled="false"
                        AutoPostBack="True" SkinID="DropDownListNormal" onchange="return UnCheckBox(id);" />
                </td>
                <td class="label">
                    <asp:CheckBox ID="chkBranch" runat="server" Checked="true" Text="" Visible="false" />
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
                    <asp:Label ID="lblFromDate" runat="server" SkinID="LabelNormal" Text="From date"></asp:Label>
                    <span class="asterix">*</span>
                </td>
                <td class="inputcontrols">
                    <asp:TextBox ID="txtFromDate" runat="server" SkinID="TextBoxCalendarExtenderNormal"
                        TabIndex="1"></asp:TextBox>
                    <ajaxToolkit:CalendarExtender ID="calFromdate" runat="server" Format="dd/MM/yyyy"
                        TargetControlID="txtFromDate">
                    </ajaxToolkit:CalendarExtender>
                </td>
                <td class="label">
                    <asp:Label ID="lblTodate" runat="server" SkinID="LabelNormal" Text="To date"></asp:Label>
                    <span class="asterix">*</span>
                </td>
                <td class="inputcontrols">
                    <asp:TextBox ID="txtToDate" runat="server" SkinID="TextBoxCalendarExtenderNormal"
                        TabIndex="2"></asp:TextBox>
                    <ajaxToolkit:CalendarExtender ID="calToDate" runat="server" Format="dd/MM/yyyy" TargetControlID="txtToDate">
                    </ajaxToolkit:CalendarExtender>
                </td>
                <td class="label">
                    <asp:Label ID="lblReportType" runat="server" SkinID="LabelNormal" Text="Report Type"></asp:Label>
                </td>
                <td class="inputcontrols">
                    <asp:DropDownList ID="ddlReportType" runat="server" SkinID="DropDownListNormal" TabIndex="3">
                        <asp:ListItem Text="GST Inward Register" Value="GST Inward Register"></asp:ListItem>
                    </asp:DropDownList>
                </td>
            </tr>
        </table>
        <div class="reportButtons">
            <asp:Button ID="btnReport" runat="server" Text="Generate Report" SkinID="ButtonViewReport"
                TabIndex="4" OnClientClick="javaScript:return Validate(this);" OnClick="btnReport_Click" />
        </div>
    </div>
</asp:Content>
