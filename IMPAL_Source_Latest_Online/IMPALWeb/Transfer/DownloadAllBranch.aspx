<%@ Page Title="" Language="C#" MasterPageFile="~/Main.Master" AutoEventWireup="true"
    CodeBehind="DownloadAllBranch.aspx.cs" Inherits="IMPALWeb.Transfer.DownloadAllBranch" %>

<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="ajaxToolkit" %>
<asp:Content ID="cntDownload" ContentPlaceHolderID="CPHDetails" runat="server">
    <!-- Ajax Update Panel Progress Loader 
            To disable the Master Page Ajax Loader
    -->
    
    <script type="text/javascript">    
        // Get the instance of PageRequestManager.
        var prm = Sys.WebForms.PageRequestManager.getInstance();
        // Add initializeRequest and endRequest
        prm.add_initializeRequest(prm_InitializeRequest);
        prm.add_endRequest(prm_EndRequest);

        // Called when async postback begins
        function prm_InitializeRequest(sender, args) {
            // get the divAjaxImageLoader and hide it again
            var panelShadow = $get('divShadow');
            var panelProgressLoader = $get('divAjaxImageLoader');

            panelShadow.style.display = 'none';
            panelProgressLoader.style.display = 'none';
        }

        // Called when async postback ends
        function prm_EndRequest(sender, args) {
            // get the divAjaxImageLoader and hide it again
            var panelShadow = $get('divShadow');

            panelShadow.style.display = 'none';
        }

        function showShadowBar() {        
            // get the divAjaxProgressBarLoader and hide it again
            var panelShadow = $get('divShadowForBar');
            var panelProgressBarLoader = $get('divAjaxProgressBarLoader');

            panelShadow.style.display = 'block';
            panelProgressBarLoader.style.display = 'block';
        }

        function hideShadowBar() {        
            // get the divAjaxProgressBarLoader and hide it again
            var panelShadow = $get('divShadowForBar');
            var panelProgressBarLoader = $get('divAjaxProgressBarLoader');

            panelShadow.style.display = 'none';
            panelProgressBarLoader.style.display = 'none';
        }

    </script>

    <div id="DivTop" runat="server">
        <asp:Timer ID="tmrDownload" runat="server" Enabled="false" Interval="2000" OnTick="tmrDownload_Tick">
        </asp:Timer>
        <asp:UpdatePanel ID="UpdpanelTop" runat="server" UpdateMode="Always">
            <Triggers>
                <asp:AsyncPostBackTrigger ControlID="tmrDownload" EventName="Tick" />
                <asp:PostBackTrigger ControlID="btnDownload" />
            </Triggers>
            <ContentTemplate>
                <div id="divDownload" runat="server">
                    <asp:Literal ID="ltrProgressBar" runat="server"></asp:Literal>
                    <div class="subFormTitle">
                        Download</div>
                    <table class="subFormTable">
                        <tr>
                            <td class="inputcontrols">
                                <asp:Button ID="btnDownload" runat="server" Text="Submit" SkinID="ButtonNormalBig"
                                    OnClick="btnDownload_Click" OnClientClick="return showShadowBar();" />
                            </td>
                        </tr>
                        <tr>
                            <td class="label">
                                <asp:Label ID="lblDownloadMessage" runat="server" Text="" SkinID="LabelNormal" Visible="false"></asp:Label>
                                <asp:Label ID="lblDownloadError" runat="server" Text="" SkinID="Error" Visible="false"></asp:Label>
                            </td>
                        </tr>
                    </table>
                </div>
            </ContentTemplate>
        </asp:UpdatePanel>
    </div>
</asp:Content>
