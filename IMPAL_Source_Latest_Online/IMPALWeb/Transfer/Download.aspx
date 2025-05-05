<%@ Page Title="" Language="C#" MasterPageFile="~/Main.Master" AutoEventWireup="true"
    CodeBehind="Download.aspx.cs" Inherits="IMPALWeb.Transfer.Download" %>

<asp:Content ID="cntDownload" ContentPlaceHolderID="CPHDetails" runat="server">
<script type="text/javascript">
    function DownLoadTXTFile(uid) {
        window.location.href = uid;
    }

</script>
    <div id="DivTop" runat="server">                                     
        <asp:Timer ID="tmrDownload" runat="server" Enabled="false" Interval="2000" 
            ontick="tmrDownload_Tick">
        </asp:Timer>
        <asp:UpdatePanel ID="UpdpanelTop" runat="server" UpdateMode="Always">
            <Triggers>
                <asp:AsyncPostBackTrigger ControlID="tmrDownload" EventName="Tick" />
            </Triggers>
            <ContentTemplate>
                <div id="divDownload" runat="server">
                        <asp:Literal ID="ltrProgressBar" runat="server"></asp:Literal>
                    <div class="subFormTitle">
                        Download</div>
                    <table class="subFormTable">
                        <tr>
                            <td class="label">
                            </td>
                            <td class="inputcontrols">
                                <asp:Button ID="btnDownload" runat="server" Text="Submit" 
                                    SkinID="ButtonNormalBig" onclick="btnDownload_Click" />
                            </td>
                            <td class="label">
                            </td>
                            <td class="inputcontrols">
                            </td>
                            <td class="label">
                            </td>
                            <td class="inputcontrols">
                            </td>
                        </tr>
                        <tr>
                            <td class="label">
                            </td>
                            <td class="inputcontrols">
                            </td>
                            <td class="label">
                            </td>
                            <td class="inputcontrols">
                            </td>
                            <td class="label">
                            </td>
                            <td class="inputcontrols">
                            </td>
                        </tr>
                        <tr>
                            <td class="label">
                            </td>
                            <td class="inputcontrols">
                            </td>
                            <td class="label">
                                <asp:Label ID="lblDownloadMessage" runat="server" Text="" SkinID="LabelNormal" Visible="false"></asp:Label>
                                <asp:Label ID="lblDownloadError" runat="server" Text="" SkinID="Error" Visible="false"></asp:Label>
                            </td>
                            <td class="inputcontrols">
                            </td>
                            <td class="label">
                            </td>
                            <td class="inputcontrols">
                            </td>
                        </tr>
                    </table>
                </div>
            </ContentTemplate>
        </asp:UpdatePanel>
    </div>
    
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
</asp:Content>
