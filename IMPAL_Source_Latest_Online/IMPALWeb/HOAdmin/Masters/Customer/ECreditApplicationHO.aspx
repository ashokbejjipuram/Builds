<%@ Page Title="" Language="C#" MasterPageFile="~/Main.Master" AutoEventWireup="true"
    CodeBehind="ECreditApplicationHO.aspx.cs" Inherits="IMPALWeb.ECreditApplicationHO" %>

<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="ajaxToolkit" %>
<asp:Content ID="Content1" ContentPlaceHolderID="CPHDetails" runat="server">
    <link href="../../../App_Themes/Styles/ECreditApplication.css" rel="stylesheet" type="text/css" />
    <script src="../../../Javascript/EcreditApplication.js" type="text/javascript"></script>
    <div>
        <asp:UpdatePanel ID="UpdateCustomer" runat="server">
            <Triggers>
                <asp:PostBackTrigger ControlID="btnPrint" />
                <asp:PostBackTrigger ControlID="btnPrint1" />
            </Triggers>
            <ContentTemplate>
                <asp:Panel ID="pnlEcreditSel" runat="server" Style="width: 800px; text-align: center">
                    <table id="tblEcreditSel" runat="server" border="1" style="width: 800px; text-align: center">
                        <tr>
                            <td>
                                <asp:Button ID="btnReset" runat="server" Text="Reset" SkinID="ButtonNormal" OnClick="btnReset_OnClick" />
                                <asp:Button ID="btnPrint" SkinID="ButtonNormal" runat="server" Text="Print" OnClientClick="javascript:return validateprint();" OnClick="btnPrint_Click" />
                            </td>
                        </tr>
                    </table>
                </asp:Panel>
                <asp:Panel ID="pnlEcreditForm" runat="server">
                    <table id="tblEcredit" style="width: 1040px;">
                        <tr class='x27'>
                            <td colspan='5' class='x192' style='border-right: 2px solid windowtext;'>INDIA MOTOR PARTS AND ACCESSORIES LIMITED</td>
                        </tr>
                        <tr class='x27'>
                            <td colspan='5' class='x194' style='border-right: 2px solid windowtext; border-bottom: 2px solid windowtext;'>Credit Application Request Form - [Note : Yellow color cells to be filled in from dropdown options]</td>
                        </tr>
                        <tr class='x27'>
                            <td colspan='2' class='x51' style="width: 305px">Branch:<asp:DropDownList ID="ddlBranch" runat="server" SkinID="DropDownListNormal" Style="background: #ffff99; height: 25px; width: 150px; font-size: 14px" AutoPostBack="true" OnSelectedIndexChanged="ddlBranch_SelectedIndexChanged">
                            </asp:DropDownList></td>
                            <td class='x52' style="width: 280px">Application #:<asp:DropDownList ID="ddlApplicationNo" runat="server" Style="color: Green; font-weight: bold; height: 25px; width: 160px; font-size: 16px" SkinID="TextBoxNormal" AutoPostBack="true" OnSelectedIndexChanged="ddlApplicationNo_SelectedIndexChanged"></asp:DropDownList>
                            </td>
                            <td class='x51' style="width: 250px">Application Date:<asp:TextBox ID="txtApplicationDate" runat="server" Style="color: Green; font-weight: bold; height: 25px; font-size: 16px; width: 95px" SkinID="TextBoxNormal"></asp:TextBox>
                                <span id="divdealerCode" runat="server">
                                    <br />
                                    <br />
                                    Dealer:
                                    <asp:DropDownList ID="ddlCustomerCode" runat="server" SkinID="DropDownListNormal" Style="background: #ffff99; height: 25px; width: 160px; font-size: 14px"></asp:DropDownList></span>
                            </td>
                            <td class='x51' style="width: 205px"><b>Distributor Name:IMPAL</b><br />
                                <span runat="server" id="indspan">
                                    <asp:Label ID="DealerIndicator" runat="server" Width="200"></asp:Label></span>
                                <span id="divdealerCode1" runat="server">
                                    <br />
                                    Dealer Code:<asp:TextBox ID="txtCustCode" runat="server" SkinID="TextBoxNormal" Style="color: Green; font-weight: bold; height: 25px; width: 90px; font-size: 15px" Enabled="false"></asp:TextBox></span>
                            </td>
                        </tr>
                    </table>
                    <div class='subdiv'>
                        <a id="link1" runat="server" onclick="javascript:return ShowHidePanel('div1')" style="vertical-align: middle">
                            <p>A) Dealer KYC</p>
                        </a>
                    </div>
                    <div id="div1" runat="server">
                        <asp:Panel ID="Panel1" runat="server">
                            <table style="width: 1040px;">
                                <tr class='x27'>
                                    <td class='x56'>1</td>
                                    <td class='x59'>Name of Dealer/Firm/STU <span class="asterix">(*)</span></td>
                                    <td colspan='3' class='x219'>
                                        <asp:TextBox ID="txtCustName" runat="server" SkinID="TextBoxNormal" Style="background: #ffffff; height: 25px; width: 500px; font-size: 14px" onblur="return ValidateAlphaCode();"></asp:TextBox>
                                        <asp:HiddenField ID="hdnIndicator" runat="server" />
                                    </td>
                                </tr>
                                <tr class='x27'>
                                    <td class='x58'>2</td>
                                    <td class='x59'>Address of Dealer/Firm/STU <span class="asterix">(*)</span></td>
                                    <td colspan='3' class='x221'>
                                        <table width='100%'>
                                            <tr class='x27'>
                                                <td>
                                                    <asp:Label ID="lblAdd1" runat="server" Style="font-size: 12pt;" Text="Address 1"></asp:Label>
                                                </td>
                                                <td class="inputcontrols">
                                                    <asp:TextBox ID="txtAdd1" runat="server" SkinID="TextBoxNormal" Style="width: 220px; height: 25px; font-size: 14px"></asp:TextBox>
                                                </td>
                                                <td class="label">
                                                    <asp:Label ID="lblAdd2" runat="server" Style="font-size: 12pt;" Text="Address 2"></asp:Label>
                                                </td>
                                                <td class="inputcontrols">
                                                    <asp:TextBox ID="txtAdd2" runat="server" SkinID="TextBoxNormal" Style="width: 220px; height: 25px; font-size: 14px"></asp:TextBox>
                                                </td>
                                            </tr>
                                            <tr>
                                                <td></td>
                                            </tr>
                                            <tr class='x27'>
                                                <td class="label">
                                                    <asp:Label ID="lblAdd3" runat="server" Style="font-size: 12pt;" Text="Address 3"></asp:Label>
                                                </td>
                                                <td class="inputcontrols">
                                                    <asp:TextBox ID="txtAdd3" runat="server" SkinID="TextBoxNormal" Style="width: 220px; height: 25px; font-size: 14px"></asp:TextBox>
                                                </td>
                                                <td class="label">
                                                    <asp:Label ID="lblAdd4" runat="server" Style="font-size: 12pt;" Text="Address 4"></asp:Label>
                                                </td>
                                                <td class="inputcontrols">
                                                    <asp:TextBox ID="txtAdd4" runat="server" SkinID="TextBoxNormal" Style="width: 220px; height: 25px; font-size: 14px"></asp:TextBox>
                                                </td>
                                            </tr>
                                        </table>
                                    </td>
                                </tr>
                                <tr>
                                    <td class='x58'>3</td>
                                    <td class='x59'>Name of the Proprietor / Director /
                                    Partner / Authorised Person <span class="asterix">(*)</span></td>
                                    <td colspan='3' class='x223'>
                                        <table width='100%'>
                                            <tr>
                                                <td>
                                                    <asp:TextBox ID="txtPropName" runat="server" SkinID="TextBoxNormal" Style="background: #ffffff; height: 25px; width: 250px; font-size: 14px"></asp:TextBox></td>
                                                <td>Mobile No. of Proprietor <span class="asterix">(*)</span>
                                                    <asp:TextBox ID="txtPropMobile" runat="server" SkinID="TextBoxNormal" Style="background: #ffffff; height: 25px; width: 150px; font-size: 14px" MaxLength="10" onkeypress="return IntegerValueOnly();" ondrag="return IntegerValueOnly();" onpaste="return IntegerValueOnly();"></asp:TextBox></td>
                                            </tr>
                                            <tr>
                                                <td colspan='2'><span style="text-align: right; font-size: 12px">
                                                    <asp:CheckBox runat="server" ID="chkBox1" onClick="return ValidateChkBox1(this.id);" />
                                                    Check if Proprietor & Contact Person are same</span></td>
                                            </tr>
                                        </table>
                                    </td>
                                </tr>
                                <tr>
                                    <td class='x58'>4</td>
                                    <td class='x200'>If Any Group / Sister Customer exists within IMPAL</td>
                                    <td colspan='3' class='x61'>
                                        <asp:GridView ID="grvGroupDealers" Width="100%" runat="server" AutoGenerateColumns="False" ShowFooter="false" OnRowDataBound="grvGroupDealers_OnRowDataBound">
                                            <Columns>
                                                <asp:TemplateField HeaderText="SNo" HeaderStyle-Font-Bold="false">
                                                    <ItemTemplate>
                                                        <asp:Label ID="lblSNo" runat="server" SkinID="LabelNormal" Text='<%# Container.DataItemIndex + 1 %>'></asp:Label>
                                                    </ItemTemplate>
                                                </asp:TemplateField>
                                                <asp:TemplateField HeaderText="Branch" HeaderStyle-Font-Bold="false">
                                                    <ItemTemplate>
                                                        <asp:DropDownList ID="ddlSisCustBranch" runat="server" Style="background: #ffff99; height: 25px; width: 120px; font-size: 14px">
                                                        </asp:DropDownList>
                                                    </ItemTemplate>
                                                </asp:TemplateField>
                                                <asp:TemplateField HeaderText="Dealer Name" HeaderStyle-Font-Bold="false">
                                                    <ItemTemplate>
                                                        <asp:DropDownList ID="ddlSisCust" runat="server" Style="background: #ffff99; height: 25px; width: 200px; font-size: 14px">
                                                        </asp:DropDownList>
                                                    </ItemTemplate>
                                                </asp:TemplateField>
                                                <asp:TemplateField HeaderText="Dealer Code" HeaderStyle-Font-Bold="false">
                                                    <ItemTemplate>
                                                        <asp:TextBox ID="txtSisCustCode" runat="server" Enabled="false" SkinID="GridViewTextBox" Style="height: 25px; font-size: 14px; width: 100px"></asp:TextBox>
                                                    </ItemTemplate>
                                                </asp:TemplateField>
                                                <asp:TemplateField HeaderText="Credit Limit (Rs.)" HeaderStyle-Font-Bold="false">
                                                    <ItemTemplate>
                                                        <asp:TextBox ID="txtSisCustCrLimit" runat="server" Enabled="false" SkinID="GridViewTextBox" Style="height: 25px; font-size: 14px; width: 100px"></asp:TextBox>
                                                    </ItemTemplate>
                                                </asp:TemplateField>
                                                <asp:CommandField ShowDeleteButton="True" ButtonType="Button" />
                                            </Columns>
                                        </asp:GridView>
                                    </td>
                                </tr>
                                <tr>
                                    <td class='x56'>5</td>
                                    <td class='x200'>Dealer migration from one branch to another</td>
                                    <td colspan='3' class='x127' style='border-right: 2px solid windowtext;'>
                                        <table width='100%'>
                                            <tr>
                                                <td align='center'>Parent Branch and Dealer Code<br />
                                                    <asp:DropDownList ID="ddlParentBranch" runat="server" SkinID="DropDownListNormal" Style="background: #ffff99; height: 25px; width: 140px; font-size: 14px" AutoPostBack="false"></asp:DropDownList>
                                                    <asp:DropDownList ID="ddlParentCustomer" runat="server" SkinID="DropDownListNormal" Style="background: #ffff99; height: 25px; width: 140px; font-size: 14px" AutoPostBack="false"></asp:DropDownList></td>
                                                <td align='center'>Migration Branch and Dealer Code<br />
                                                    <asp:DropDownList ID="ddlMigrateBranch" runat="server" SkinID="DropDownListNormal" Style="background: #ffff99; height: 25px; width: 140px; font-size: 14px" AutoPostBack="false"></asp:DropDownList>
                                                    <asp:DropDownList ID="ddlMigrateCustomer" runat="server" SkinID="DropDownListNormal" Style="background: #ffff99; height: 25px; width: 140px; font-size: 14px" AutoPostBack="false"></asp:DropDownList>
                                                </td>
                                            </tr>
                                        </table>
                                    </td>
                                </tr>
                                <tr class='x27'>
                                    <td class='x56' rowspan='5' style='width: 40px'>6</td>
                                    <td class='x71' style="width: 255px">Year of establishment</td>
                                    <td class='x72' style="width: 290px">
                                        <asp:TextBox ID="txtYearofEstbl" runat="server" SkinID="TextBoxNormal" Style="background: #ffffff; height: 25px; width: 150px; font-size: 14px"></asp:TextBox></td>
                                    <td class='x71' style="width: 250px">Contact Person Name <span class="asterix">(*)</span></td>
                                    <td class='x73a' style="width: 205px">
                                        <asp:TextBox ID="txtContactPersonName" runat="server" SkinID="TextBoxNormal" Style="background: #ffffff; height: 25px; width: 180px; font-size: 14px"></asp:TextBox></td>
                                </tr>
                                <tr class='x27'>
                                    <td class='x74'>Dealer State <span class="asterix">(*)</span></td>
                                    <td class='x75'>
                                        <asp:DropDownList ID="ddlDealerState" runat="server" SkinID="DropDownListNormal" Style="background: #ffff99; height: 25px; width: 200px; font-size: 14px" AutoPostBack="false"></asp:DropDownList>
                                        <asp:HiddenField ID="hdnStateCode" runat="server" />
                                        <asp:HiddenField ID="hdnAccPeriodCode" runat="server" />
                                    </td>
                                    <td class='x74'>Contact Person Mobile <span class="asterix">(*)</span></td>
                                    <td class='x76'>
                                        <asp:TextBox ID="txtContactPersonMobNo" runat="server" SkinID="TextBoxNormal" Style="background: #ffffff; height: 25px; width: 180px; font-size: 14px" MaxLength="10" onkeypress="return IntegerValueOnly();" ondrag="return IntegerValueOnly();" onpaste="return IntegerValueOnly();"></asp:TextBox></td>
                                </tr>
                                <tr class='x27'>
                                    <td class='x74'>Dealer District <span class="asterix">(*)</span></td>
                                    <td class='x75'>
                                        <asp:DropDownList ID="ddlDealerDistrict" runat="server" SkinID="DropDownListNormal" Style="background: #ffff99; height: 25px; width: 150px; font-size: 14px" AutoPostBack="false"></asp:DropDownList></td>
                                    <td class='x74'>E-mail id <span class="asterix">(*)</span></td>
                                    <td class='x75'>
                                        <asp:TextBox ID="txtEmailid" runat="server" SkinID="TextBoxNormal" Style="background: #ffffff; height: 25px; width: 180px; font-size: 14px"></asp:TextBox></td>
                                </tr>
                                <tr class='x27'>
                                    <td class='x74'>Dealer Town <span class="asterix">(*)</span></td>
                                    <td class='x75'>
                                        <asp:DropDownList ID="ddlDealerTown" runat="server" SkinID="DropDownListNormal" Style="background: #ffff99; height: 25px; width: 150px; font-size: 14px" AutoPostBack="false"></asp:DropDownList></td>
                                    <td class='x74'>Dealer Location <span class="asterix">(*)</span></td>
                                    <td class='x76'>
                                        <asp:TextBox ID="txtLocation" runat="server" SkinID="TextBoxNormal" Style="background: #ffffff; height: 25px; width: 180px; font-size: 14px"></asp:TextBox></td>
                                </tr>
                                <tr class='x27'>
                                    <td class='x59a'>Town Location <span class="asterix">(*)</span></td>
                                    <td class='x78'>
                                        <asp:DropDownList ID="ddlTownClassification" runat="server" SkinID="DropDownListNormal" Style="background: #ffff99; height: 25px; width: 150px; font-size: 14px" AutoPostBack="false"
                                            DataSourceID="ODS_TownClassification" DataTextField="Desc" DataValueField="Value">
                                        </asp:DropDownList></td>
                                    <td class='x59a'>Dealer Postal Pincode <span class="asterix">(*)</span></td>
                                    <td class='x79'>
                                        <asp:TextBox ID="txtPinCode" runat="server" SkinID="TextBoxNormal" Style="background: #ffffff; height: 25px; width: 80px; font-size: 14px" onkeypress="return IntegerValueOnly();" ondrag="return IntegerValueOnly();" onpaste="return IntegerValueOnly();"></asp:TextBox></td>
                                </tr>
                                <tr class='x27'>
                                    <td class='x58' rowspan='2' style='border-bottom: 2px solid windowtext;'>7</td>
                                    <td class='x74'>Type of Firm <span class="asterix">(*)</span></td>
                                    <td class='x80'>
                                        <asp:DropDownList ID="ddlFirmType" runat="server" SkinID="DropDownListNormal" DataSourceID="ODS_DealerFirmType" DataTextField="Desc"
                                            DataValueField="Value" Style="background: #ffff99; height: 25px; width: 250px; font-size: 14px" AutoPostBack="false">
                                        </asp:DropDownList></td>
                                    <td class='x81'>GST Registration Number <span class="asterix">(*)</span></td>
                                    <td class='x73a'>
                                        <asp:TextBox ID="txtGSTIN" runat="server" SkinID="TextBoxNormal" Style="background: #ffffff; height: 25px; width: 150px; font-size: 14px" MaxLength="15"></asp:TextBox></td>
                                </tr>
                                <tr>
                                    <td class='x82'>Type of Registration <span class="asterix">(*)</span></td>
                                    <td class='x83'>
                                        <asp:DropDownList ID="ddlRegnType" runat="server" SkinID="DropDownListNormal" Style="background: #ffff99; height: 25px; width: 150px; font-size: 14px" AutoPostBack="false"
                                            DataSourceID="ODS_DealerGSTRegType" DataTextField="Desc" DataValueField="Value">
                                        </asp:DropDownList></td>
                                    <td class='x84'>GSTIN Local or Outside State</td>
                                    <td class='x85'>
                                        <asp:DropDownList ID="ddlGSTINlocation" runat="server" SkinID="DropDownListNormal" Style="background: #ffff99; height: 25px; width: 200px; font-size: 14px" AutoPostBack="false"
                                            DataSourceID="ODS_DealerGSTINlocation" DataTextField="Desc" DataValueField="Value">
                                        </asp:DropDownList></td>
                                </tr>
                                <tr class='x27'>
                                    <td class='x86'>8</td>
                                    <td class='x59'>Distance from Branch to Dealer</td>
                                    <td class='x87'>
                                        <asp:TextBox ID="txtDistance" runat="server" SkinID="TextBoxNormal" Style="background: #ffffff; height: 25px; width: 120px; font-size: 14px"></asp:TextBox>Kms</td>
                                    <td class='x88'>Dealer Overall Stock Value</td>
                                    <td class='x89'>
                                        <asp:TextBox ID="txtOverAllStockValue" runat="server" SkinID="TextBoxNormal" Style="background: #ffffff; height: 25px; width: 120px; font-size: 14px"></asp:TextBox></td>
                                </tr>
                                <tr class='x27'>
                                    <td class='x86'>9</td>
                                    <td class='x88'>Dealer Annual Turonover</td>
                                    <td class='x87'>
                                        <asp:TextBox ID="txtAnnlTunOver" runat="server" SkinID="TextBoxNormal" Style="background: #ffffff; height: 25px; width: 120px; font-size: 14px"></asp:TextBox></td>
                                    <td class='x57'>Dealer IMPAL Lines Sales Turnover</td>
                                    <td class='x91'>
                                        <asp:TextBox ID="txtLineSaleTurnOver" runat="server" SkinID="TextBoxNormal" Style="background: #ffffff; height: 25px; width: 120px; font-size: 14px"></asp:TextBox></td>
                                </tr>
                                <tr class='x27'>
                                    <td class='x86'>10</td>
                                    <td class='x71'>Dealer Serviced from Branch or Res Rep ?</td>
                                    <td class='x93'>
                                        <asp:DropDownList ID="ddlDealerServicedBy" runat="server" SkinID="DropDownListNormal" Style="background: #ffff99; height: 25px; width: 200px; font-size: 14px" AutoPostBack="false"
                                            DataSourceID="ODS_DealerServicedBy" DataTextField="Desc" DataValueField="Value">
                                        </asp:DropDownList></td>
                                    <td class='x94'>Distance from RR Location to Dealer
                            <td class='x95'>
                                <asp:TextBox ID="txtDistanceSM" runat="server" SkinID="TextBoxNormal" Style="background: #ffffff; height: 25px; width: 120px; font-size: 14px"></asp:TextBox>Kms</td>
                                </tr>
                                <tr class='x27'>
                                    <td class='x86'>11</td>
                                    <td class='x71'>Classified as Day Travel or Outstation ?</td>
                                    <td class='x96'>
                                        <asp:DropDownList ID="ddlDayTravelOS" runat="server" SkinID="DropDownListNormal" Style="background: #ffff99; height: 25px; width: 150px; font-size: 14px" AutoPostBack="false"
                                            DataSourceID="ODS_SalesManTour" DataTextField="Desc" DataValueField="Value">
                                        </asp:DropDownList></td>
                                    <td class='x57'>Salesman/RR assigned to the Dealer <span class="asterix">(*)</span></td>
                                    <td class='x97'>
                                        <asp:DropDownList ID="ddlAssignedSMRR" runat="server" SkinID="DropDownListNormal" Style="background: #ffff99; height: 25px; width: 200px; font-size: 14px" AutoPostBack="false">
                                        </asp:DropDownList>
                                    </td>
                                </tr>
                                <tr class='x27'>
                                    <td class='x56'>12</td>
                                    <td class='x88'>Periodicity of Dealer Visit</td>
                                    <td class='x93'>
                                        <asp:DropDownList ID="ddlVisitPlan" runat="server" SkinID="DropDownListNormal" Style="background: #ffff99; height: 25px; width: 150px; font-size: 14px" AutoPostBack="false"
                                            DataSourceID="ODS_DealerVisitPlan" DataTextField="Desc" DataValueField="Value">
                                        </asp:DropDownList></td>
                                    <td class='x57'>Dealer Monthly Target <span class="asterix">(*)</span></td>
                                    <td class='x97'>
                                        <asp:TextBox ID="txtDealerTarget" runat="server" SkinID="TextBoxNormal" Style="background: #ffffff; height: 25px; width: 150px; font-size: 14px" onkeypress="return CurrencyNumberOnly();" onchange="return CreditLimitChange()" onpaste="return false;" ondragstart="return false;" ondrop="return false;"></asp:TextBox></td>
                                </tr>
                            </table>
                        </asp:Panel>
                    </div>
                    <div class='subdiv'>
                        <a id="link2" runat="server" onclick="javascript:return ShowHidePanel('div2')" style="vertical-align: middle">
                            <p>B) Dealer Profile</p>
                        </a>
                    </div>
                    <div id="div2" runat="server">
                        <asp:Panel ID="Panel2" runat="server">
                            <table style="width: 1040px;">
                                <tr>
                                    <td class='x56' style='width: 40px'>13</td>
                                    <td class='x215' style='border-top: 2px solid windowtext; width: 255px'>Dealer Classification <span class="asterix">(*)</span><br />
                                        <br />
                                        Dealer Business Segment <span class="asterix">(*)</span>
                                    </td>
                                    <td class='x99' style="width: 290px">
                                        <asp:DropDownList ID="ddlDealerClassification" runat="server" SkinID="DropDownListNormal" Style="background: #ffff99; height: 25px; width: 200px; font-size: 14px" AutoPostBack="false"
                                            DataSourceID="ODS_DealerClassification" DataTextField="Desc" DataValueField="Value">
                                        </asp:DropDownList>
                                        <br />
                                        <br />
                                        <asp:DropDownList ID="ddlDealerSegment" runat="server" SkinID="DropDownListNormal" Style="background: #ffff99; height: 25px; width: 200px; font-size: 14px" AutoPostBack="false"
                                            DataSourceID="ODS_DealerSegmentsType" DataTextField="Desc" DataValueField="Value">
                                        </asp:DropDownList>
                                    </td>
                                    <td class='x100' style="width: 250px; border-right: 2px solid windowtext;">Dealers with multiple branches if any. 
                                    If other towns also- then name towns</td>
                                    <td class='x101' style="width: 205px">
                                        <asp:DropDownList ID="ddlDealersMultipleTown" runat="server" SkinID="DropDownListNormal" Style="background: #ffff99; height: 25px; width: 200px; font-size: 14px" AutoPostBack="false"
                                            DataSourceID="ODS_DealerBusinessPlace" DataTextField="Desc" DataValueField="Value">
                                        </asp:DropDownList>
                                        <br />
                                        <br />
                                        <asp:TextBox ID="txtTownName" runat="server" SkinID="TextBoxNormal" Style="background: #ffffff; height: 25px; width: 100px; font-size: 14px"></asp:TextBox>
                                    </td>
                                </tr>
                                <tr>
                                    <td class='x56'>14</td>
                                    <td class='x226'>List of key lines directly purchased from Manufacturers</td>
                                    <td colspan='3' class='x107'>
                                        <table width='100%'>
                                            <tr>
                                                <td>1)
                                    <asp:TextBox ID="txtKeyLine1" runat="server" SkinID="TextBoxNormal" Style="background: #ffffff; height: 25px; width: 200px; font-size: 14px"></asp:TextBox></td>
                                                <td>2)
                                    <asp:TextBox ID="txtKeyLine2" runat="server" SkinID="TextBoxNormal" Style="background: #ffffff; height: 25px; width: 200px; font-size: 14px"></asp:TextBox></td>
                                                <td>3)
                                    <asp:TextBox ID="txtKeyLine3" runat="server" SkinID="TextBoxNormal" Style="background: #ffffff; height: 25px; width: 200px; font-size: 14px"></asp:TextBox></td>
                                            </tr>
                                            <tr>
                                                <td>4)
                                    <asp:TextBox ID="txtKeyLine4" runat="server" SkinID="TextBoxNormal" Style="background: #ffffff; height: 25px; width: 200px; font-size: 14px"></asp:TextBox></td>
                                                <td>5)
                                    <asp:TextBox ID="txtKeyLine5" runat="server" SkinID="TextBoxNormal" Style="background: #ffffff; height: 25px; width: 200px; font-size: 14px"></asp:TextBox></td>
                                                <td>6)
                                    <asp:TextBox ID="txtKeyLine6" runat="server" SkinID="TextBoxNormal" Style="background: #ffffff; height: 25px; width: 200px; font-size: 14px"></asp:TextBox></td>
                                            </tr>
                                        </table>
                                    </td>
                                </tr>
                                <tr>
                                    <td class='x56' style='border-bottom: 2px solid windowtext;'>15</td>
                                    <td class='x226' style='border-bottom: 2px solid windowtext;'>Is the dealer at present dealing with any TVS Group companies? If so details</td>

                                    <td class='x111'>
                                        <asp:DropDownList ID="ddlDealerDealingGroups" runat="server" SkinID="DropDownListNormal" Style="background: #ffff99; height: 25px; width: 200px; font-size: 14px" AutoPostBack="false"
                                            DataSourceID="ODS_LogicalAnswers" DataTextField="Desc" DataValueField="Value">
                                        </asp:DropDownList></td>
                                    <td class='x112' style='border-right: 2px solid windowtext;'>Any additional information</td>
                                    <td class='x113'>
                                        <asp:TextBox ID="txtAddlInfo" TextMode="MultiLine" runat="server" SkinID="TextBoxNormal" Style="background: #ffffff; height: 60px; width: 200px; font-size: 14px"></asp:TextBox></td>
                                </tr>
                                <tr>
                                    <td class='x58'>16</td>
                                    <td class='x50'>Transporter Name <span class="asterix">(*)</span></td>
                                    <td class='x117'>
                                        <asp:TextBox ID="txtTransporterName" runat="server" SkinID="TextBoxNormal" Style="background: #ffffff; height: 25px; width: 200px; font-size: 14px"></asp:TextBox>
                                    </td>
                                    <td colspan='2' class='x217' style='border-right: 2px solid windowtext;'>Details of lines as ASC</td>
                                </tr>
                                <tr>
                                    <td class='x58' style='border-bottom: 2px solid windowtext;'>17</td>
                                    <td class='x205' style='border-bottom: 2px solid windowtext;'>Addl Info on Dealer, if any</td>
                                    <td class='x207' style='border-bottom: 2px solid windowtext;'>
                                        <asp:TextBox ID="txtAddlInfo1" TextMode="MultiLine" runat="server" SkinID="TextBoxNormal" Style="background: #ffffff; height: 50px; width: 260px; font-size: 14px"></asp:TextBox></td>
                                    <td class='x118' colspan='2'>
                                        <table width='100%'>
                                            <tr>
                                                <td>1)
                                                <asp:TextBox ID="txtLinesASC1" runat="server" SkinID="TextBoxNormal" Style="background: #ffffff; height: 25px; width: 180px; font-size: 14px"></asp:TextBox></td>
                                                <td>2)
                                                <asp:TextBox ID="txtLinesASC2" runat="server" SkinID="TextBoxNormal" Style="background: #ffffff; height: 25px; width: 180px; font-size: 14px"></asp:TextBox></td>
                                            </tr>
                                            <tr>
                                                <td>3)
                                                <asp:TextBox ID="txtLinesASC3" runat="server" SkinID="TextBoxNormal" Style="background: #ffffff; height: 25px; width: 180px; font-size: 14px"></asp:TextBox></td>
                                                <td>4)
                                                <asp:TextBox ID="txtLinesASC4" runat="server" SkinID="TextBoxNormal" Style="background: #ffffff; height: 25px; width: 180px; font-size: 14px"></asp:TextBox></td>
                                            </tr>
                                        </table>
                                    </td>
                                </tr>
                                <tr>
                                    <td class='x122'>18</td>
                                    <td class='x123'>Brand Details as Authorised Service Dealer</td>
                                    <td class='x281' colspan='2'>WABCO
                                    <asp:CheckBox ID="ChkBoxWABCO" runat="server" SkinID="CheckBoxNormal" />/ Rane TRW
                                    <asp:CheckBox ID="CheckBoxRaneTRW" runat="server" SkinID="CheckBoxNormal" />
                                        / Turbo
                                    <asp:CheckBox ID="CheckBoxTurbo" runat="server" SkinID="CheckBoxNormal" />/ Lucas<asp:CheckBox ID="CheckBoxLucas" runat="server" SkinID="CheckBoxNormal" />
                                        &nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;Others Specify:										  
                                    <br />
                                        Authorised Service Centres:
                                    <asp:TextBox ID="txtAuthServiceDetls" runat="server" SkinID="TextBoxNormal" Style="background: #ffffff; height: 25px; width: 250px; font-size: 14px" Text="Multi Vehicle Repair garages"></asp:TextBox></td>
                                    <td class='x281'>
                                        <asp:TextBox ID="txtOtherAuthDetails" runat="server" SkinID="TextBoxNormal" TextMode="MultiLine" Style="background: #ffffff; height: 40px; width: 200px; font-size: 14px"></asp:TextBox></td>
                                </tr>
                            </table>
                        </asp:Panel>
                    </div>
                    <div class='subdiv'>
                        <a id="link3" runat="server" onclick="javascript:return ShowHidePanel('div3')" style="vertical-align: middle">
                            <p>C) Commercial Matters</p>
                        </a>
                    </div>
                    <div id="div3" runat="server">
                        <asp:Panel ID="Panel3" runat="server">
                            <table style="width: 1040px;">
                                <tr>
                                    <td class='x58' style='border-bottom: 2px solid windowtext; width: 40px'>19</td>
                                    <td class='x200' style='border-bottom: 2px solid windowtext; border-top: 2px solid windowtext; width: 245px'>If any cash purchase in last three months ( Specify)</td>
                                    <td colspan='3' class='x266' style='border-right: 2px solid windowtext; border-bottom: 2px solid windowtext; width: 290px'>
                                        <asp:TextBox ID="txtCashPurchase" TextMode="MultiLine" runat="server" SkinID="TextBoxNormal" Style="background: #ffffff; height: 40px; width: 600px; font-size: 14px"></asp:TextBox></td>
                                </tr>
                                <tr>
                                    <td class='x58' style='border-bottom: 2px solid windowtext;'>20</td>
                                    <td class='x200' style='border-bottom: 2px solid windowtext;'>Expected sales detail of major lines per month</td>
                                    <td colspan='3' class='x107'>
                                        <table width='100%' align='center'>
                                            <tr>
                                                <td>1)<asp:TextBox ID="txtExpSalesDtls1" runat="server" SkinID="TextBoxNormal" Style="background: #ffffff; height: 25px; width: 180px; font-size: 14px"></asp:TextBox></td>
                                                <td>2)<asp:TextBox ID="txtExpSalesDtls2" runat="server" SkinID="TextBoxNormal" Style="background: #ffffff; height: 25px; width: 180px; font-size: 14px"></asp:TextBox></td>
                                                <td>3)<asp:TextBox ID="txtExpSalesDtls3" runat="server" SkinID="TextBoxNormal" Style="background: #ffffff; height: 25px; width: 180px; font-size: 14px"></asp:TextBox></td>
                                            </tr>
                                            <tr>
                                                <td>4)<asp:TextBox ID="txtExpSalesDtls4" runat="server" SkinID="TextBoxNormal" Style="background: #ffffff; height: 25px; width: 180px; font-size: 14px"></asp:TextBox></td>
                                                <td>5)<asp:TextBox ID="txtExpSalesDtls5" runat="server" SkinID="TextBoxNormal" Style="background: #ffffff; height: 25px; width: 180px; font-size: 14px"></asp:TextBox></td>
                                                <td>6)<asp:TextBox ID="txtExpSalesDtls6" runat="server" SkinID="TextBoxNormal" Style="background: #ffffff; height: 25px; width: 180px; font-size: 14px"></asp:TextBox></td>
                                            </tr>
                                            <tr>
                                                <td>7)<asp:TextBox ID="txtExpSalesDtls7" runat="server" SkinID="TextBoxNormal" Style="background: #ffffff; height: 25px; width: 180px; font-size: 14px"></asp:TextBox></td>
                                                <td>8)<asp:TextBox ID="txtExpSalesDtls8" runat="server" SkinID="TextBoxNormal" Style="background: #ffffff; height: 25px; width: 180px; font-size: 14px"></asp:TextBox></td>
                                                <td>9)<asp:TextBox ID="txtExpSalesDtls9" runat="server" SkinID="TextBoxNormal" Style="background: #ffffff; height: 25px; width: 180px; font-size: 14px"></asp:TextBox></td>
                                            </tr>
                                        </table>
                                    </td>
                                </tr>
                                <tr>
                                    <td class='x35'>21</td>
                                    <td class='x146' colspan="4">
                                        <table width='100%'>
                                            <tr>
                                                <td>Existing Credit Limit Rs.</td>
                                                <td>
                                                    <asp:TextBox ID="txtExistingCrLimit" runat="server" SkinID="TextBoxNormal" Style="background: #ffffff; height: 25px; width: 100px; font-size: 14px"></asp:TextBox></td>
                                                <td>Outstanding Amount</td>
                                                <td>
                                                    <asp:TextBox ID="txtOsAmt" runat="server" SkinID="TextBoxNormal" Style="background: #ffffff; height: 25px; width: 100px; font-size: 14px"></asp:TextBox></td>
                                                <td colspan="2">Enhanced Credit Limit Requested Rs.</td>
                                                <td>
                                                    <asp:TextBox ID="txtEnhCrLimtReq" runat="server" SkinID="TextBoxNormal" Style="background: #ffffff; height: 25px; width: 100px; font-size: 14px" onkeypress="return CurrencyNumberOnly()" onkeyup="return toIndianCurrency(this.id)" onchange="return CreditLimitChange()" onpaste="return false;" ondragstart="return false;" ondrop="return false;"></asp:TextBox></td>
                                            </tr>
                                            <tr>
                                                <td>Credit Limit Indicator: <span class="asterix">(*)</span></td>
                                                <td>
                                                    <asp:DropDownList ID="ddlCrLimitIndicator" runat="server" SkinID="DropDownListNormal" Style="background: #ffff99; height: 25px; width: 110px; font-size: 14px">
                                                        <asp:ListItem Value="">--Select--</asp:ListItem>
                                                        <asp:ListItem Value="CD">Cash DisCount</asp:ListItem>
                                                        <asp:ListItem Value="CR">Credit</asp:ListItem>
                                                    </asp:DropDownList>
                                                    <asp:HiddenField ID="hdnCrLimitInd" runat="server" />
                                                </td>
                                                <td>Validity Indicator:</td>
                                                <td>
                                                    <asp:DropDownList ID="ddlValidityIndicator" runat="server" SkinID="DropDownListNormal" Enabled="false" Style="background: #ffff99; height: 25px; width: 100px; font-size: 14px"
                                                        onchange="return CreditLimitValidity()">
                                                        <asp:ListItem Value="">--Select--</asp:ListItem>
                                                        <asp:ListItem Value="T">Temporary</asp:ListItem>
                                                        <asp:ListItem Value="P">Permanent</asp:ListItem>
                                                    </asp:DropDownList></td>
                                                <td>Validity Due Date:</td>
                                                <td>
                                                    <asp:TextBox ID="txtCrlimitDueDate" runat="server" SkinID="TextBoxNormal" Enabled="false" Style="background: #ffffff; height: 25px; width: 100px; font-size: 14px" onchange="return CheckValidDateCrLimit();" onpaste="return false;" ondragstart="return false;" ondrop="return false;"></asp:TextBox>
                                                    <ajaxToolkit:CalendarExtender ID="ceCrlimitDueDate" PopupButtonID="imgCrlimitDueDate"
                                                        Format="dd/MM/yyyy" runat="server" TargetControlID="txtCrlimitDueDate" />
                                                </td>
                                            </tr>
                                        </table>
                                    </td>
                                </tr>
                                <tr>
                                    <td class='x35'>22</td>
                                    <td class='x142' colspan='4'>
                                        <table width='100%'>
                                            <tr>
                                                <td style='vertical-align: middle' width="25%">Freight Indicator <span class="asterix">(*)</span></td>
                                                <td style='vertical-align: middle' width="35%">
                                                    <asp:DropDownList ID="ddlFreightIndicator" runat="server" SkinID="DropDownListNormal" Style="background: #ffff99; height: 25px; width: 100px; font-size: 14px"
                                                        DataSourceID="ODS_FreightIndicator" DataTextField="Desc" DataValueField="Value">
                                                    </asp:DropDownList>
                                                </td>
                                                <td style='vertical-align: middle' width="35%">First Time Credit Amount Request Rs.</td>
                                                <td style='vertical-align: middle' width="5%">
                                                    <asp:TextBox ID="txtFirstCrLimitReq" runat="server" SkinID="TextBoxNormal" Style="background: #ffffff; height: 25px; width: 150px; font-size: 14px"></asp:TextBox></td>
                                            </tr>
                                        </table>
                                    </td>
                                </tr>
                                <tr>
                                    <td class='x58' style='border-bottom: 2px solid windowtext;'>23</td>
                                    <td class='x274' style='border-bottom: 2px solid windowtext;'>Dealer Bank Account Details</td>
                                    <td colspan='3' class='x110'>
                                        <table width='100%'>
                                            <tr>
                                                <td style='text-align: center'>Bank Name<br />
                                                    <asp:TextBox ID="txtBankName" runat="server" SkinID="TextBoxNormal" Style="background: #ffffff; height: 25px; width: 200px; font-size: 14px"></asp:TextBox></td>
                                                <td style='text-align: center'>Bank Branch Name<br />
                                                    <asp:TextBox ID="txtBankBranch" runat="server" SkinID="TextBoxNormal" Style="background: #ffffff; height: 25px; width: 200px; font-size: 14px"></asp:TextBox></td>
                                                <td style='text-align: center'>Bank Account Number<br />
                                                    <asp:TextBox ID="txtBankAccountNo" runat="server" SkinID="TextBoxNormal" Style="background: #ffffff; height: 25px; width: 200px; font-size: 14px" onkeypress="return IntegerValueOnly();"></asp:TextBox></td>
                                            </tr>
                                            <tr>
                                                <td style='text-align: center'>IFSC Code<br />
                                                    <asp:TextBox ID="txtIFSCcode" runat="server" SkinID="TextBoxNormal" Style="background: #ffffff; height: 25px; width: 200px; font-size: 14px" MaxLength="11"></asp:TextBox></td>
                                                <td style='text-align: center'>Name of the Bank A/C Holder<br />
                                                    <asp:TextBox ID="txtAccountName" runat="server" SkinID="TextBoxNormal" Style="background: #ffffff; height: 25px; width: 200px; font-size: 14px"></asp:TextBox></td>
                                                <td style='text-align: center'>Card No. and Expiry Date<br />
                                                    <asp:TextBox ID="txtCarNoExpDate" runat="server" SkinID="TextBoxNormal" Style="background: #ffffff; height: 25px; width: 200px; font-size: 14px"></asp:TextBox></td>
                                            </tr>
                                        </table>
                                    </td>
                                </tr>
                                <tr>
                                    <td class='x34'>24</td>
                                    <td class='x123' style='border-top: 2px solid windowtext;'>Mode of Payment</td>
                                    <td class='x152'>
                                        <asp:DropDownList ID="ddlPaymentMode" runat="server" SkinID="DropDownListNormal" Style="background: #ffff99; height: 25px; width: 180px; font-size: 14px" AutoPostBack="false"
                                            DataSourceID="ODS_ModeOfPayment" DataTextField="Desc" DataValueField="Value">
                                        </asp:DropDownList></td>
                                    <td colspan='2' rowspan='2' class='x260' style='border-right: 2px solid windowtext; border-bottom: 2px solid windowtext;'>Dealer Official Seal</td>
                                </tr>
                                <tr>
                                    <td class='x35'>25</td>
                                    <td class='x123'>Authorized Signature with Name of Dealer</td>
                                    <td class='x109'></td>
                                </tr>
                            </table>
                        </asp:Panel>
                    </div>
                    <div class='subdiv'>
                        <a id="link4" runat="server" onclick="javascript:return ShowHidePanel('div4')" style="vertical-align: middle">
                            <p>D) Review and Approval at Branch Level</p>
                        </a>
                    </div>
                    <div id="div4" runat="server">
                        <asp:Panel ID="Panel4" runat="server">
                            <table style="width: 1040px;">
                                <tr>
                                    <td class='x58' style='border-bottom: 2px solid windowtext; width: 40px'>26</td>
                                    <td class='x156' colspan='4' style='border-top: 2px solid windowtext; border-bottom: 2px solid windowtext;'>
                                        <table width='100%'>
                                            <tr>
                                                <td width='33%' style='text-align: left'>Branch Sales Executive's Name</td>
                                                <td width='33%' style='text-align: left'>Recommended By Manager Name</td>
                                                <td width='34%' style='text-align: left'>Authorised by Area Manager Name</td>
                                            </tr>
                                            <tr>
                                                <td style='text-align: left'>
                                                    <asp:TextBox ID="txtSalesManName" runat="server" SkinID="TextBoxNormal" Style="background: #ffffff; height: 25px; width: 180px; font-size: 14px"></asp:TextBox></td>
                                                <td style='text-align: left'>
                                                    <asp:TextBox ID="txtBranchManagerName" runat="server" SkinID="TextBoxNormal" Style="background: #ffffff; height: 25px; width: 180px; font-size: 14px"></asp:TextBox></td>
                                                <td style='text-align: left'>
                                                    <asp:TextBox ID="txtAreaManagerName" runat="server" SkinID="TextBoxNormal" Style="background: #ffffff; height: 25px; width: 180px; font-size: 14px"></asp:TextBox></td>
                                            </tr>
                                            <tr style='height: 35.1pt'>
                                                <td style='text-align: left'>Signature &amp; Date</td>
                                                <td style='text-align: left'>Signature &amp; Date</td>
                                                <td style='text-align: left'>Signature &amp; Date</td>
                                            </tr>
                                        </table>
                                    </td>
                                </tr>
                            </table>
                        </asp:Panel>
                    </div>
                    <div class='subdiv'>
                        <a id="link5" runat="server" onclick="javascript:return ShowHidePanel('div5')" style="vertical-align: middle">
                            <p>E) Review and Approval at Head Office</p>
                        </a>
                    </div>
                    <div id="div5" runat="server">
                        <asp:Panel ID="Panel5" runat="server">
                            <table style="width: 1040px;">
                                <tr>
                                    <td class='x58' rowspan='2' style='border-bottom: 2px solid windowtext; width: 40px'>27</td>
                                    <td class='x284' colspan='4' style='vertical-align: middle'>
                                        <table width='100%' id='pymtpatterngridtable' runat="server">
                                            <tr>
                                                <td>
                                                    <asp:GridView ID="grvPaymentPattern" Width="100%" runat="server" AutoGenerateColumns="false" OnRowDataBound="grvPaymentPattern_OnRowDataBound">
                                                        <Columns>
                                                            <asp:TemplateField HeaderText="Month" HeaderStyle-HorizontalAlign="Center">
                                                                <ItemTemplate>
                                                                    <asp:Label ID="lblOsMonth" runat="server" Style="font-weight: bold" />
                                                                </ItemTemplate>
                                                            </asp:TemplateField>
                                                            <asp:TemplateField HeaderText="Credit Limit" HeaderStyle-HorizontalAlign="Center" ItemStyle-HorizontalAlign="Right">
                                                                <ItemTemplate>
                                                                    <asp:Label ID="lblOsCrLimit" runat="server" />
                                                                </ItemTemplate>
                                                            </asp:TemplateField>
                                                            <asp:TemplateField HeaderText="Total OS" HeaderStyle-HorizontalAlign="Center" ItemStyle-HorizontalAlign="Right">
                                                                <ItemTemplate>
                                                                    <asp:Label ID="lblOsTotal" runat="server" />
                                                                </ItemTemplate>
                                                            </asp:TemplateField>
                                                            <asp:TemplateField HeaderText="0-30" HeaderStyle-HorizontalAlign="Center" ItemStyle-HorizontalAlign="Right">
                                                                <ItemTemplate>
                                                                    <asp:Label ID="lblOsCurBal" runat="server" />
                                                                </ItemTemplate>
                                                            </asp:TemplateField>
                                                            <asp:TemplateField HeaderText="31-60" HeaderStyle-HorizontalAlign="Center" ItemStyle-HorizontalAlign="Right">
                                                                <ItemTemplate>
                                                                    <asp:Label ID="lblOsAbove30" runat="server" />
                                                                </ItemTemplate>
                                                            </asp:TemplateField>
                                                            <asp:TemplateField HeaderText="61-90" HeaderStyle-HorizontalAlign="Center" ItemStyle-HorizontalAlign="Right" FooterStyle-Wrap="false">
                                                                <ItemTemplate>
                                                                    <asp:Label ID="lblOsAbove60" runat="server" />
                                                                </ItemTemplate>
                                                            </asp:TemplateField>
                                                            <asp:TemplateField HeaderText="> 90" HeaderStyle-HorizontalAlign="Center" ItemStyle-HorizontalAlign="Right" FooterStyle-Wrap="false">
                                                                <ItemTemplate>
                                                                    <asp:Label ID="lblOsAbove90" runat="server" />
                                                                </ItemTemplate>
                                                            </asp:TemplateField>
                                                            <asp:TemplateField HeaderText="> 180" HeaderStyle-HorizontalAlign="Center" ItemStyle-HorizontalAlign="Right" FooterStyle-Wrap="false">
                                                                <ItemTemplate>
                                                                    <asp:Label ID="lblOsAbove180" runat="server" />
                                                                </ItemTemplate>
                                                            </asp:TemplateField>
                                                            <asp:TemplateField HeaderText="Coll. %" HeaderStyle-HorizontalAlign="Center" ItemStyle-HorizontalAlign="Right" FooterStyle-Wrap="false">
                                                                <ItemTemplate>
                                                                    <asp:Label ID="lblOsCollPer" runat="server" />
                                                                </ItemTemplate>
                                                            </asp:TemplateField>
                                                        </Columns>
                                                    </asp:GridView>
                                                    <b style="color: red">No. of Cheque Returns:</b>
                                                    <asp:Label ID="lblOsChequeReturns" runat="server" Style="color: red; font-weight: bold" />
                                                </td>
                                            </tr>
                                        </table>
                                    </td>
                                </tr>
                                <tr style='height: 40.5pt'>
                                    <td class='x161' style='border-top: 2px solid windowtext; vertical-align: middle; width: 255px'>Approved Credit Limit Rs.</td>
                                    <td class='x251' style='border-bottom: 2px solid windowtext; vertical-align: middle; width: 290px'>
                                        <asp:TextBox ID="txtApprovedCrLimit" runat="server" SkinID="TextBoxNormal" Style="background: #ffffff; height: 25px; width: 120px; font-size: 14px" onkeypress="return CurrencyNumberOnly();" onkeyup="return toIndianCurrency(this.id)" onchange="return CreditLimitChange()" onpaste="return false;" ondragstart="return false;" ondrop="return false;"></asp:TextBox>&nbsp;Date:
                                    <asp:TextBox ID="txtCrLimitApprovalDate" runat="server" SkinID="TextBoxNormal" Style="background: #ffffff; height: 25px; width: 80px; font-size: 14px" Enabled="false"></asp:TextBox>
                                        <ajaxToolkit:CalendarExtender ID="ceApprCrlimitDueDate" PopupButtonID="imgApprCrlimitDueDate"
                                            Format="dd/MM/yyyy" runat="server" TargetControlID="txtCrLimitApprovalDate" />
                                    </td>
                                    <td class='x228' style='border-bottom: 2px solid windowtext; width: 250px;'>Zonal Head Signature</td>
                                    <td class='x230' style='border-bottom: 2px solid windowtext; width: 205px'></td>
                                </tr>
                                <tr style='height: 55.75pt'>
                                    <td class='x58'>28</td>
                                    <td class='x226' style='border-bottom: 2px solid windowtext;'>Accounts &amp; IT Department for Creation of Customer Master</td>
                                    <td class='x248' style='border-bottom: 2px solid windowtext;'>Customer code :
                                <asp:TextBox ID="txtCustomerCode" runat="server" SkinID="TextBoxNormal" Style="color: Green; font-weight: bold; height: 25px; width: 100px; font-size: 15px" Enabled="false"></asp:TextBox></td>
                                    <td class='x229' style='border-bottom: 2px solid windowtext; width: 90px;'>DMD Signature (Digital)</td>
                                    <td class='x230' style='border-bottom: 2px solid windowtext;'></td>
                                </tr>
                                <tr>
                                    <td class='x58' style='border-bottom: 2px solid windowtext; border-top: 2px solid windowtext;'>29</td>
                                    <td class='x245' style='border-bottom: 2px solid windowtext;'>Attachments Required</td>
                                    <td colspan='3' class='x163' style='border-right: 2px solid windowtext;'>
                                        <table>
                                            <tr>
                                                <td>1) GST Registration Certificate including the type of GST <span class="asterix">(*)</span></td>
                                                <td>
                                                    <asp:CheckBox ID="CheckBoxGSTCertificate" runat="server" SkinID="CheckBoxNormal" Enabled="false" /></td>
                                            </tr>
                                            <tr>
                                                <td>2) Business card of the dealership <span class="asterix">(*)</span></td>
                                                <td>
                                                    <asp:CheckBox ID="CheckBoxBusinessCard" runat="server" SkinID="CheckBoxNormal" Enabled="false" /></td>
                                            </tr>
                                            <tr>
                                                <td>3) Cancelled cheque leaf <span class="asterix">(*)</span></td>
                                                <td>
                                                    <asp:CheckBox ID="CheckBoxCancelledCheq" runat="server" SkinID="CheckBoxNormal" Enabled="false" /></td>
                                            </tr>
                                        </table>
                                    </td>
                                </tr>
                                <tr>
                                    <td class='x131' colspan='5' style='text-align: left; border-right: 2px solid windowtext; border-bottom: 2px solid windowtext;'>
                                        <span class="asterix">&nbsp;&nbsp;(*)</span> marked fields are Mandatory&nbsp;&nbsp;&nbsp;&nbsp;
                                    <asp:CheckBox ID="CheckBoxMandatory" runat="server" SkinID="CheckBoxNormal" Enabled="false" />&nbsp;&nbsp;To be ticked (√) wherever applicable</td>
                                </tr>
                            </table>
                        </asp:Panel>
                    </div>
                    <div class='subdiv'>
                        <a id="link6" runat="server" onclick="javascript:return ShowHidePanel('div6')" style="vertical-align: middle">
                            <p>F) Closure of Business with Dealer (with closure of sister concern accounts)</p>
                        </a>
                    </div>
                    <div id="div6" runat="server">
                        <asp:Panel ID="Panel6" runat="server">
                            <table style="width: 1040px;">
                                <tr>
                                    <td class='x30' rowspan="2" style='width: 40px'>30</td>
                                    <td class='x167' style='border-top: 2px solid windowtext; width: 255px'>Reason for Closure of business</td>
                                    <td class='x168' style='width: 290px'>
                                        <asp:DropDownList ID="ddlReasonForClosure" runat="server" SkinID="DropDownListNormal" Style="background: #ffff99; height: 25px; width: 250px; font-size: 14px" AutoPostBack="false"
                                            DataSourceID="ODS_ReasonForClosure" DataTextField="Desc" DataValueField="Value">
                                        </asp:DropDownList></td>
                                    <td class='x169' style='width: 250px'>Write off amount if any Rs.</td>
                                    <td class='x170' style='width: 205px'>
                                        <asp:TextBox ID="txtWriteOffAmout" runat="server" SkinID="TextBoxNormal" Style="background: #ffffff; height: 25px; width: 120px; font-size: 14px"></asp:TextBox></td>
                                </tr>
                                <tr style='height: 55.75pt'>
                                    <td class='x171'>CFO Signature (Digital)</td>
                                    <td class='x172'></td>
                                    <td class='x173'>DMD Signature (Digital)</td>
                                    <td class='x174'></td>
                                </tr>
                            </table>
                        </asp:Panel>
                    </div>
                </asp:Panel>
                <div class="transactionButtons" runat="server" id="divtrans">
                    <div class="transactionButtonsHolder">
                        <asp:Button ID="btnSubmit" SkinID="ButtonNormal" runat="server" Style="background-color: Green; color: White" Text="Approve" OnClick="btnSubmit_Click" />
                        <asp:Button ID="btnReject" SkinID="ButtonNormal" runat="server" Style="background-color: Red; color: White" Text="Reject" OnClick="btnReject_Click" />
                        <asp:Button ID="btnPrint1" SkinID="ButtonNormal" runat="server" Text="Print" OnClientClick="javascript:return validateprint();" OnClick="btnPrint_Click" />
                        <asp:Button ID="btnReset1" SkinID="ButtonNormal" runat="server" Text="Reset" OnClick="btnReset_OnClick" />
                    </div>
                </div>
            </ContentTemplate>
        </asp:UpdatePanel>
    </div>
    <asp:XmlDataSource ID="ODS_DealerFirmType" runat="server" DataFile="~/XML/EcreditApplication.xml"
        XPath="/Root/ECreditApplication/DealerFirmType"></asp:XmlDataSource>
    <asp:XmlDataSource ID="ODS_SalesManTour" runat="server" DataFile="~/XML/EcreditApplication.xml"
        XPath="/Root/ECreditApplication/SalesManTour"></asp:XmlDataSource>
    <asp:XmlDataSource ID="ODS_DealerBusinessPlace" runat="server" DataFile="~/XML/EcreditApplication.xml"
        XPath="/Root/ECreditApplication/DealerBusinessPlace"></asp:XmlDataSource>
    <asp:XmlDataSource ID="ODS_DealerVisitPlan" runat="server" DataFile="~/XML/EcreditApplication.xml"
        XPath="/Root/ECreditApplication/DealerVisitPlan"></asp:XmlDataSource>
    <asp:XmlDataSource ID="ODS_LogicalAnswers" runat="server" DataFile="~/XML/EcreditApplication.xml"
        XPath="/Root/ECreditApplication/LogicalAnswers"></asp:XmlDataSource>
    <asp:XmlDataSource ID="ODS_DealerClassification" runat="server" DataFile="~/XML/EcreditApplication.xml"
        XPath="/Root/ECreditApplication/DealerClassification"></asp:XmlDataSource>
    <asp:XmlDataSource ID="ODS_DealerGSTRegType" runat="server" DataFile="~/XML/EcreditApplication.xml"
        XPath="/Root/ECreditApplication/DealerGSTRegType"></asp:XmlDataSource>
    <asp:XmlDataSource ID="ODS_DealerGSTINlocation" runat="server" DataFile="~/XML/EcreditApplication.xml"
        XPath="/Root/ECreditApplication/DealerGSTINlocation"></asp:XmlDataSource>
    <asp:XmlDataSource ID="ODS_DealerBankAccType" runat="server" DataFile="~/XML/EcreditApplication.xml"
        XPath="/Root/ECreditApplication/DealerBankAccType"></asp:XmlDataSource>
    <asp:XmlDataSource ID="ODS_DealerSegmentsType" runat="server" DataFile="~/XML/EcreditApplication.xml"
        XPath="/Root/ECreditApplication/DealerSegmentsType"></asp:XmlDataSource>
    <asp:XmlDataSource ID="ODS_ModeOfPayment" runat="server" DataFile="~/XML/EcreditApplication.xml"
        XPath="/Root/ECreditApplication/ModeOfPayment"></asp:XmlDataSource>
    <asp:XmlDataSource ID="ODS_ReasonForClosure" runat="server" DataFile="~/XML/EcreditApplication.xml"
        XPath="/Root/ECreditApplication/ReasonForClosure"></asp:XmlDataSource>
    <asp:XmlDataSource ID="ODS_TownClassification" runat="server" DataFile="~/XML/EcreditApplication.xml"
        XPath="/Root/ECreditApplication/TownClassification"></asp:XmlDataSource>
    <asp:XmlDataSource ID="ODS_DealerServicedBy" runat="server" DataFile="~/XML/EcreditApplication.xml"
        XPath="/Root/ECreditApplication/DealerServicedBy"></asp:XmlDataSource>
    <asp:XmlDataSource ID="ODS_FreightIndicator" runat="server" DataFile="~/XML/EcreditApplication.xml"
        XPath="/Root/ECreditApplication/FreightIndicator"></asp:XmlDataSource>
</asp:Content>
