<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="Login.aspx.cs" Inherits="IMPALWeb.Login" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head id="header1" runat="server">
    <title>IMPAL - India Motor Parts & Accessories Limited</title>
    <meta http-equiv="cache-control" content="no-cache" />
    <meta http-equiv="expires" content="0" />
    <meta http-equiv="pragma" content="no-cache" />
    <link rel="icon" type="image/png" href="images/favicon.png" />
</head>
<body style="margin-top: 0px; margin-bottom: 0px; background-color: #EFEFEF;">
    <form id="frmLogin" runat="server" defaultbutton="btSignIn">

    <script type="text/javascript">
        $('.button').click(function () {
            $.ajax({
                url: "",
                context: document.body,
                success: function (s, x) {

                    $('html[manifest=saveappoffline.appcache]').attr('content', '');
                    $(this).html(s);
                }
            });
        });

        function AlphaNumericOnly() {
            var AsciiValue = event.keyCode;

            if ((AsciiValue >= 48 && AsciiValue <= 57) || (AsciiValue == 8 || AsciiValue == 127) || (AsciiValue >= 65 && AsciiValue <= 90)
            || (AsciiValue >= 97 && AsciiValue <= 122))
                event.returnValue = true;
            else {
                event.returnValue = false;
            }
        }

        function AlphaNumericPwdOnly() {
            var AsciiValue = event.keyCode;

            if ((AsciiValue >= 48 && AsciiValue <= 57) || (AsciiValue == 8 || AsciiValue == 127) || (AsciiValue >= 65 && AsciiValue <= 90)
            || (AsciiValue >= 97 && AsciiValue <= 122) || (AsciiValue >= 35 && AsciiValue <= 38) || AsciiValue == 42 || AsciiValue == 61 || AsciiValue == 64)
                event.returnValue = true;
            else {
                event.returnValue = false;
            }
        }

        function ReplaceAlphaNumericnly() {
            var txtUserId = document.getElementById("txtUserId");

            if (txtUserId.value != "") {
                var regx = "/[^A-Za-z0-9]/g";
                txtUserId.value = txtUserId.value.replace(eval(regx), '');

                return true;
            }
        }

        function ReplaceAlphaNumericPwdnly() {
            var txtPwd = document.getElementById("txtPassword");

            if (txtPwd.value != "") {
                var regx = "/[^A-Za-z0-9*%$@&#=]/g";
                txtPwd.value = txtPwd.value.replace(eval(regx), '');

                return true;
            }
        }

        function ChecktheLogins() {
            var txtUserId = document.getElementById("txtUserId");
            var txtPwd = document.getElementById("txtPassword");

            if (txtUserId.value.trim() == "" || txtUserId.value.trim() == null) {
                alert('Please Enter an Userid.');
                txtUserId.focus();
                return false;
            }

            if (txtPwd.value.trim() == "" || txtPwd.value.trim() == null) {
                alert('Please Enter Password.');
                txtPwd.focus();
                return false;
            }
        }
    </script>

    <div id="loginHeader">
        <div class="logo">
            <a href="#">
                <img id="logo" runat="server" src="~/images/LogoIMPAL_Login.png" alt="IMPALLogo"
                    title="IMPAL" />
            </a>
        </div>
        <div class="CSCLogo">
            <a href="#">
                <img id="CSCLogo" runat="server" src="~/images/dxc_logo.png" alt="DXCLogo"
                    title="DXC" />
            </a>
        </div>
    </div>
    <div id="loginContent">
        <table id="login">
            <tr>
                <td class="loginLeft">
                    <div id="sliderContent">
                        <h3>
                            Abstract of Impal</h3>
                        <p>
                            India Motor Parts & Accessories Limited (IMPAL) a TVS Group Company was incorporated
                            on 12th July 1954. The Company engages in the distribution of automobile spare parts
                            and accessories through its 85+ branch network representing over 50 manufactures.
                            IMPAL is one of the few all India distributors of motor parts and deals in engine
                            group components, brake systems, fasteners, radiators, suspensions, axles, auto
                            electricals, wheels, steering linkages, instrument clusters etc.
                            <br />
                            <br />
                            <a href="#">Learn more.....</a>
                        </p>
                    </div>
                </td>
                <td class="loginRight">
                    <h2>
                        sign in</h2>
                    <p>
                        <asp:Label ID="lblBranch" runat="server" Text="User Id" SkinID="Login"></asp:Label>
                        <br />
                        <asp:TextBox ID="txtUserId" runat="server" SkinID="Login" onkeypress="return AlphaNumericOnly();" onkeyup="return AlphaNumericOnly();"
                        ondrop="return ReplaceAlphaNumericnly();" onpaste="return ReplaceAlphaNumericnly();" onblur="return ReplaceAlphaNumericnly();"></asp:TextBox>
                        <br />
                        <asp:Label ID="lblMessage1" runat="server" Text="" SkinID="Error"></asp:Label>
                    </p>
                    <p>
                        <asp:Label ID="lblPassword" runat="server" Text="Password" SkinID="Login"></asp:Label>
                        <br />
                        <asp:TextBox ID="txtPassword" runat="server" TextMode="Password" SkinID="Login" onkeypress="return AlphaNumericPwdOnly();"
                        onkeyup="return AlphaNumericPwdOnly();" ondrop="return ReplaceAlphaNumericPwdnly();" onpaste="return ReplaceAlphaNumericPwdnly();"
                        onblur="return ReplaceAlphaNumericPwdnly();" OnUnload="txtPassword_Unload"></asp:TextBox>
                        <br />
                        <asp:Label ID="lblMessage" runat="server" Text="" SkinID="Error"></asp:Label>
                    </p>
                    <p class="signin">
                        <asp:Button ID="btSignIn" runat="server" OnClick="btSignIn_Click" OnCommand="btSignIn_Command"
                            Text="Sign In" SkinID="Login" OnClientClick="return ChecktheLogins();" />
                    </p>
                </td>
            </tr>
        </table>
    </div>
    <div id="footer" class="flogin">
        <table width="100%">
            <tr>
                <td>
                    <div class="copyright">
                        <%--<img id="copyright" runat="server" src="~/images/CSC_FooterLogo.png" alt="CSC" />
                        <span>Copyright 2012 Computer Science Corporation - All rights reserved</span>--%>
                        <span>© DXC Technology Company 2020 - All rights reserved</span>
                    </div>
                </td>
                <td>
                    <div class="feedback">
                        <img id="feedback" runat="server" src="~/images/ifeedback.png" alt="Feedback" /><span>Feedback</span></div>
                    <div class="helpcenter">
                        <img id="helpcenter" runat="server" src="~/images/iHelp_n.png" alt="Help" /><span>Help
                            Centre</span></div>
                </td>
                <td align="right" width="30%">
                    <div class="theme">
                        <asp:Button ID="btnThemeWhite" runat="server" Text="" CssClass="buttonThemeWhite"
                            Enabled="false" />
                        <asp:Button ID="btnThemeGray" runat="server" Text="" CssClass="buttonThemeGray" Enabled="false" />
                        <span style="font-size: 12px;">Build Version :
                            <asp:Label ID="BuildButton" runat="server" Text="" SkinID="Login"></asp:Label></span>
                    </div>
                </td>
            </tr>
        </table>
    </div>
    </form>
</body>
</html>
