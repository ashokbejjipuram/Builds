<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="ErrorPage.aspx.cs" Inherits="IMPALWeb.Errors.ErrorPage" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head id="errHeader" runat="server">
    <title>IMPAL - India Motor Parts & Accessories Limited</title>
</head>
<body>
    <form id="frmError" runat="server">
    <div id="errorHeader">
        <div class="logo">
            <a href="#">
                <img id="logo" runat="server" src="~/images/LogoIMPAL_Login.png" alt="IMPALLogo"
                    title="IMPAL" />
            </a>
        </div>
        <div class="CSCLogo">
            <a href="#">
                <img id="CSCLogo" runat="server" src="~/images/CSC_FooterLogo.png" alt="CSCLogo"
                    title="CSC" />
            </a>
        </div>
    </div>
    <div id="errorContent">
        <table id="error">
            <tr>
                <td class="errorLeft">
                    <div id="errorContentDisplay">
                        <h3>
                            <asp:Label ID="lblErrorContentDisplay" runat="server" Text="Multiple Users are Dealing with the Same Screen. Please Re-try in a While"></asp:Label>
                        </h3>
                        <p>
                            <asp:Label ID="lblErrorContentDisplaySubText" runat="server" Text="System has crashed due to an unexpected error."></asp:Label>
                        </p>
                    </div>
                </td>
                <td class="errorRight">
                    <h2>
                        <asp:Label ID="lblErrorRight" runat="server" Text="Contact Info!"></asp:Label>
                    </h2>
                    <p>
                        <asp:Label ID="lblErrorRightSubText" runat="server" Text="Please contact System Administrator."></asp:Label>
                    </p>
                </td>
            </tr>
        </table>
    </div>
    <div id="footer" class="ferror">
        <div class="copyright">
            <img id="copyright" runat="server" src="~/images/CSC_FooterLogo.png" alt="CSC" />
            <span>Copyright 2012 Computer Science Corporation - All rights reserved</span>
        </div>
        <div class="feedback">
            <img id="feedback" runat="server" src="~/images/ifeedback.png" alt="Feedback" /><span>Feedback</span></div>
        <div class="helpcenter">
            <img id="helpcenter" runat="server" src="~/images/iHelp_n.png" alt="Help" /><span>Help
                Centre</span></div>
    </div>
    </form>
</body>
</html>
