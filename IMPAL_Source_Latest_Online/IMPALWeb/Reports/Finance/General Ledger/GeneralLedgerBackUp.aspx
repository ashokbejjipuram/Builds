<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="GeneralLedgerBackUp.aspx.cs"
    Inherits="IMPALWeb.Reports.Finance.General_Ledger.GeneralLedgerBackUp" MasterPageFile="~/Main.Master" %>

<%@ Register Src="~/UserControls/CrystalReportExport.ascx" TagName="CrystalReport" TagPrefix="uc" %>
<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="ajaxToolkit" %>
<asp:Content ID="Content" ContentPlaceHolderID="CPHDetails" runat="server">

    <script language="javascript" type="text/javascript">
        function fnValidate() {
            var ddlAccPeriod = document.getElementById('<%=ddlAccPeriod.ClientID%>');
            var ddlBranch = document.getElementById('<%=ddlBranch.ClientID%>');
            var ddlGLClassification = document.getElementById('<%=ddlGLClassification.ClientID%>');
            var ddlGLGroup = document.getElementById('<%=ddlGLGroup.ClientID%>');
            var ddlGLMain = document.getElementById('<%=ddlGLMain.ClientID%>');
            var ddlGLSub = document.getElementById('<%=ddlGLSub.ClientID%>');

            if (ddlAccPeriod.value <= 0) {
                alert("Please Select Accounting Period!");
                ddlAccPeriod.focus();
                return false;
            }

            if (ddlBranch.value == "") {
                alert("Please Select Branch");
                ddlBranch.focus();
                return false;
            }

            if (ddlGLClassification.value == "") {
                alert("Please Select GL Classification");
                ddlGLClassification.focus();
                return false;
            }

            if (ddlGLGroup.value == "") {
                alert("Please Select GL Group");
                ddlGLGroup.focus();
                return false;
            }

            let str = "coruser3"; //,coruser1,coruser2
            if (!(str.includes('<%=Session["UserID"]%>'))) {
                if (ddlGLMain.value == "") {
                    alert("Please Select GL Main");
                    ddlGLMain.focus();
                    return false;
                }
            }

            //if (ddlGLSub.value == "") {
            //    alert("Please Select GL Sub");
            //    ddlGLSub.focus();
            //    return false;
            //}

            var txtFromDate = document.getElementById('<%=txtFromDate.ClientID%>');
            var txtToDate = document.getElementById('<%=txtToDate.ClientID%>');
            var hidFromDate = document.getElementById('<%=hidFromDate.ClientID%>');
            var hidToDate = document.getElementById('<%=hidToDate.ClientID%>');

            var oFromDateVal = txtFromDate.value.trim();
            var oToDateVal = txtToDate.value.trim();
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
            if (CheckDates(txtFromDate, txtToDate) == false)
                return false;
            else {
                var oFromDate = oFromDateVal.split("/");
                var oToDate = oToDateVal.split("/");
                var oFromDateFormatted = oFromDate[1] + "/" + oFromDate[0] + "/" + oFromDate[2];
                var oToDateFormatted = oToDate[1] + "/" + oToDate[0] + "/" + oToDate[2];
                if (hidFromDate != null)
                    hidFromDate.value = oFromDateFormatted;
                if (hidToDate != null)
                    hidToDate.value = oToDateFormatted;
            }
        }

        function fnShowHideBtns() {
            document.getElementById('<%=btnReportPDF.ClientID%>').style.display = "none";
            document.getElementById('<%=btnReportExcel.ClientID%>').style.display = "none";
            document.getElementById('<%=btnReportRTF.ClientID%>').style.display = "none";
            document.getElementById('<%=btnBack.ClientID%>').style.display = "inline";

            window.document.forms[0].target = '_blank';
        }
    </script>

    <div class="reportFormTitle">
        General Ledger
    </div>
    <asp:UpdatePanel ID="upHeader" runat="server" UpdateMode="Conditional">
        <Triggers>
            <asp:PostBackTrigger ControlID="btnReportPDF" />
            <asp:PostBackTrigger ControlID="btnReportExcel" />
            <asp:PostBackTrigger ControlID="btnReportRTF" />
            <asp:PostBackTrigger ControlID="rptCrystal" />
        </Triggers>
        <ContentTemplate>
            <asp:Panel ID="PanelHeaderDtls" runat="server">
                <div class="reportFilters">
                    <table id="reportFiltersTable" class="reportFiltersTable" runat="server">
                        <tr>
                            <td>
                                <table id="reportFiltersTable1" class="reportFiltersTable">
                                    <tr>
                                        <td class="label">
                                            <asp:Label ID="lblAccPeriod" runat="server" Text="Accounting Period" SkinID="LabelNormal"></asp:Label>
                                            <span class="asterix">*</span>
                                        </td>
                                        <td class="inputcontrols">
                                            <asp:DropDownList ID="ddlAccPeriod" TabIndex="1" runat="server" SkinID="DropDownListNormal"
                                                AutoPostBack="true" OnSelectedIndexChanged="ddlAccPeriod_SelectedIndexChanged">
                                            </asp:DropDownList>
                                        </td>
                                        <td class="label">
                                            <asp:Label ID="lblGLClassification" runat="server" Text="GL Classification" SkinID="LabelNormal"></asp:Label>
                                            <span class="asterix">*</span>
                                        </td>
                                        <td class="inputcontrols">
                                            <asp:DropDownList ID="ddlGLClassification" TabIndex="2" runat="server" SkinID="DropDownListNormal"
                                                AutoPostBack="True" OnSelectedIndexChanged="GLClassification_IndexChanged">
                                            </asp:DropDownList>
                                        </td>
                                        <td class="label">
                                            <asp:Label ID="lblGLGroup" runat="server" Text="GL Group" SkinID="LabelNormal"></asp:Label>
                                            <span class="asterix">*</span>
                                        </td>
                                        <td class="inputcontrols">
                                            <asp:DropDownList ID="ddlGLGroup" TabIndex="3" runat="server" SkinID="DropDownListNormal"
                                                AutoPostBack="True" Enabled="False" OnSelectedIndexChanged="ddlGLGroup_IndexChanged">
                                            </asp:DropDownList>
                                        </td>
                                    </tr>
                                    <tr>
                                        <td class="label">
                                            <asp:Label ID="lblGLMain" runat="server" Text="GL Main" SkinID="LabelNormal"></asp:Label>
                                            <span class="asterix">*</span>
                                        </td>
                                        <td class="inputcontrols">
                                            <asp:DropDownList ID="ddlGLMain" TabIndex="4" runat="server" AutoPostBack="True"
                                                Enabled="False" SkinID="DropDownListNormal" OnSelectedIndexChanged="ddlGLMain_IndexChanged">
                                            </asp:DropDownList>
                                        </td>
                                        <td class="label">
                                            <asp:Label ID="lblGLSub" runat="server" Text="GL Sub" SkinID="LabelNormal"></asp:Label>
                                        </td>
                                        <td class="inputcontrols">
                                            <asp:DropDownList ID="ddlGLSub" TabIndex="5" runat="server" AutoPostBack="True" Enabled="False"
                                                SkinID="DropDownListNormal" OnSelectedIndexChanged="ddlGLSub_IndexChanged">
                                            </asp:DropDownList>
                                        </td>
                                        <td class="label">
                                            <asp:Label ID="lblGLAcc" runat="server" Text="GL Account" SkinID="LabelNormal"></asp:Label>
                                        </td>
                                        <td class="inputcontrols">
                                            <asp:DropDownList ID="cbGLAcc" TabIndex="5" runat="server" AutoPostBack="True" Enabled="False"
                                                SkinID="DropDownListNormal" OnSelectedIndexChanged="cbGLAcc_IndexChanged">
                                            </asp:DropDownList>
                                        </td>
                                    </tr>
                                    <tr>
                                        <td class="label">
                                            <asp:Label ID="lblBranch" runat="server" Text="Branch" SkinID="LabelNormal"></asp:Label>
                                        </td>
                                        <td class="inputcontrols">
                                            <asp:DropDownList ID="ddlBranch" TabIndex="7" runat="server" SkinID="DropDownListNormal"
                                                Enabled="False">
                                            </asp:DropDownList>
                                        </td>
                                        <td class="label">
                                            <asp:Label ID="lblFromDate" Text="From Date" runat="server" SkinID="LabelNormal"></asp:Label>
                                            <span class="asterix">*</span>
                                        </td>
                                        <td class="inputcontrols">
                                            <asp:TextBox ID="txtFromDate" runat="server" SkinID="TextBoxCalendarExtenderNormal"
                                                TabIndex="8"></asp:TextBox>
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
                                                TabIndex="9"></asp:TextBox>
                                            <ajaxToolkit:CalendarExtender ID="calTodate" Format="dd/MM/yyyy" runat="server" TargetControlID="txtToDate">
                                            </ajaxToolkit:CalendarExtender>
                                            <asp:HiddenField ID="hidToDate" runat="server" />
                                        </td>
                                    </tr>
                                    <tr>
                                        <td class="label">
                                            <asp:Label ID="lblReportType" runat="server" Text="Report Type" SkinID="LabelNormal"></asp:Label>
                                        </td>
                                        <td class="inputcontrols">
                                            <asp:DropDownList ID="ddlReportType" TabIndex="10" runat="server" SkinID="DropDownListNormal"
                                                DataTextField="DisplayText" DataValueField="DisplayValue">
                                            </asp:DropDownList>
                                        </td>
                                    </tr>
                                </table>
                                <div id="divCustomerInfo" style="display: none" runat="server">
                                    <div class="reportFormTitle">
                                        Customer Information
                                    </div>
                                    <table class="reportFiltersTable">
                                        <tr>
                                            <td class="label">
                                                <asp:Label runat="server" ID="lblCustomerCode" Text="Customer Code" SkinID="LabelNormal" />
                                            </td>
                                            <td class="inputcontrols">
                                                <asp:TextBox ID="txtCustomerCode" runat="server" SkinID="TextBoxDisabled" ReadOnly="true" />
                                            </td>
                                            <td class="label">
                                                <asp:Label Text="Address1" SkinID="LabelNormal" runat="server" ID="lblAddress1" />
                                            </td>
                                            <td class="inputcontrols">
                                                <asp:TextBox ID="txtAddress1" runat="server" SkinID="TextBoxDisabled" ReadOnly="true" />
                                            </td>
                                            <td class="label">
                                                <asp:Label runat="server" ID="lblAddress2" Text="Address2" SkinID="LabelNormal" />
                                            </td>
                                            <td class="inputcontrols">
                                                <asp:TextBox ID="txtAddress2" runat="server" SkinID="TextBoxDisabled" ReadOnly="true" />
                                            </td>
                                        </tr>
                                        <tr>
                                            <td class="label">
                                                <asp:Label runat="server" ID="lblAddress3" Text="Address3" SkinID="LabelNormal" />
                                            </td>
                                            <td class="inputcontrols">
                                                <asp:TextBox ID="txtAddress3" runat="server" SkinID="TextBoxDisabled" ReadOnly="true" />
                                            </td>
                                            <td class="label">
                                                <asp:Label Text="Address4" SkinID="LabelNormal" runat="server" ID="lblAddress4" />
                                            </td>
                                            <td class="inputcontrols">
                                                <asp:TextBox ID="txtAddress4" runat="server" SkinID="TextBoxDisabled" ReadOnly="true" />
                                            </td>
                                            <td class="label">
                                                <asp:Label runat="server" ID="lblLocation" Text="Location" SkinID="LabelNormal" />
                                            </td>
                                            <td class="inputcontrols">
                                                <asp:TextBox ID="txtLocation" runat="server" SkinID="TextBoxDisabled" ReadOnly="true" />
                                            </td>
                                        </tr>
                                    </table>
                                </div>
                            </td>
                        </tr>
                    </table>
                </div>
            </asp:Panel>
            <div class="reportButtons">
                <asp:Button ID="btnReport" runat="server" OnClick="btnReport_Click" OnClientClick="javaScript:return fnValidate();"
                    Text="Generate Report" SkinID="ButtonViewReport" TabIndex="11" />
                <asp:Button ID="btnReportPDF" runat="server" Text="PDF Report" TabIndex="4" SkinID="ButtonViewReport"
                    OnClientClick="javaScript:return fnShowHideBtns();" OnClick="btnReportPDF_Click" Style="display: none" />
                <asp:Button ID="btnReportExcel" runat="server" Text="Excel Report" TabIndex="4" SkinID="ButtonViewReport"
                    OnClientClick="javaScript:return fnShowHideBtns();" OnClick="btnReportExcel_Click" Style="display: none" />
                <asp:Button ID="btnReportRTF" runat="server" Text="Word Report" TabIndex="4" SkinID="ButtonViewReport"
                    OnClientClick="javaScript:return fnShowHideBtns();" OnClick="btnReportRTF_Click" Style="display: none" />
                <asp:Button ID="btnBack" SkinID="ButtonNormal" runat="server" Text="Back" OnClick="btnBack_Click" Style="display: none" />
            </div>
            <div class="reportViewerHolder" id="reportViewerHolder" runat="server">
                <uc:CrystalReport ID="rptCrystal" runat="server" OnUnload="rptCrystal_Unload" />
            </div>
        </ContentTemplate>
    </asp:UpdatePanel>
</asp:Content>
