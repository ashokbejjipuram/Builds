<%@ Page Language="C#" MasterPageFile="~/Main.Master" AutoEventWireup="true" CodeBehind="OutStandingDetailsList.aspx.cs" Inherits="IMPALWeb.Dashboard.FinanceDashboard.OutStandingDetailsList" Title="Untitled Page" %>
<%@ Register Src="~/UserControls/SSRSReport.ascx" TagName="SSRSReport" TagPrefix="UC" %>
<asp:Content ID="Content1" ContentPlaceHolderID="CPHDetails" runat="server">  
            <div class="reportFormTitle reportFormTitleExtender350">
               Finance Dashboard - OutStanding Details 
             </div>
            <div>               
               <table id="dashReportTable" runat="server" style="margin-left:100px; width:75px; height: 200px;"> 
               
                  <tr>
                     <td class ="label" style="width: 100px">
                        <asp:Label ID="lblReportType" runat="server" Text="Report Type" 
                           SkinID="LabelNormal" Font-Bold="True"></asp:Label>
                     </td>
                     <td class="inputcontrols">
                        <asp:DropDownList ID="ddlReportType" runat="server" 
                           SkinID="DropDownListNormal" TabIndex="1" AutoPostBack="false" Height="29px" Width="150px">
                           <asp:ListItem>-- Select Report type --</asp:ListItem>
                           <asp:ListItem>Above 90</asp:ListItem>
                           <asp:ListItem>Above 180</asp:ListItem>
                        </asp:DropDownList>
                     </td>
                  </tr> 
                  <tr>
                     <td class ="label">
                        <asp:Label ID="lblZone" runat="server" Text="Zone" SkinID="LabelNormal" 
                           Font-Bold="True"></asp:Label>
                     </td>
                     <td class="inputcontrols">
                        <asp:DropDownList ID="ddlZone" runat="server" 
                           SkinID="DropDownListNormal" TabIndex="2" AutoPostBack="True" Height="29px" 
                           Width="150px" onselectedindexchanged="ddlZone_SelectedIndexChanged">
                           <asp:ListItem>-- Select Zone --</asp:ListItem>
                        </asp:DropDownList>
                     </td>
                  </tr>  
                  <tr>
                     <td class ="label">
                        <asp:Label ID="lblState" runat="server" Text="State" SkinID="LabelNormal" 
                           Font-Bold="True"></asp:Label>
                     </td>
                     <td class="inputcontrols">
                        <asp:DropDownList ID="ddlState" runat="server" 
                           SkinID="DropDownListNormal" TabIndex="3" AutoPostBack="True" Height="29px" 
                           Width="150px" onselectedindexchanged="ddlState_SelectedIndexChanged">
                           <asp:ListItem>-- Select State --</asp:ListItem>
                        </asp:DropDownList>
                     </td>
                  </tr>    
                  <tr>
                     <td class ="label">
                        <asp:Label ID="lblBranch" runat="server" Text="Branch" SkinID="LabelNormal" 
                           Font-Bold="True"></asp:Label>
                     </td>
                     <td class="inputcontrols">
                        <asp:DropDownList ID="ddlBranch" runat="server" 
                           SkinID="DropDownListNormal" TabIndex="4" AutoPostBack="True" Height="29px" Width="150px">
                           <asp:ListItem>-- Select Branch --</asp:ListItem>
                        </asp:DropDownList>
                     </td>
                  </tr>                  
               </table>
               <div>
                  <asp:Button ID="btnReport1" runat="server" Text="Generate Report" 
                     SkinID="ButtonViewReport" TabIndex="5" Height="29px" onclick="btnReport1_Click"/>
               </div>
            </div>
            <div class="dashReportHolder" id="dashReportHolder" runat="server">
               <UC:SSRSReport ID="dashOutStandingDetailsList" runat="server" />
            </div>
           <%-- </div>--%>
      <%-- </ContentTemplate>
     </asp:UpdatePanel>
   </div>--%>
</asp:Content>