<%@ Page Title="" Language="C#" MasterPageFile="~/Main.Master" AutoEventWireup="true" CodeBehind="Home.aspx.cs" Inherits="IMPALWeb.Home" %>
<asp:Content ID="cntHome" ContentPlaceHolderID="CPHDetails" runat="server">
        <script>
            $(function() {
                $('ul#ImpalDetails').roundabout({
                    btnPrev: ".prev",
                    btnNext: ".next",
                    duration: 2000,
                    reflect: false,
                    easing: "easeInSine",
                    clickToFocus: true,
                    startingChild: 0,
                    autoplay: true,
                    autoplayDuration: 7000,
                    autoplayPauseOnHover: true
                  });
              });

              function noBack() {
                  window.history.forward();
              }
              noBack();
              window.onload = noBack;
              window.onpageshow = function(evt) { if (evt.persisted) noBack() }
              window.onunload = function() { void (0) }
        </script> 
        <table id="indiaMapIMPAL">
            <tr>
                <td class="indiaMapIMPALHolder">
                    <ul id="ImpalDetails">
                <li>
                    <span>
                        <img src="images/Image_Large_1.png"alt="First Scroll Image" />
                    </span>
                    <p>
                        India Motor Parts & Accessories Limited (IMPAL), is promoted by TVS group.
                        One of the largest automobile component manufactures in India.<br />
                        <a href="#">More...</a>
                    </p>
                </li>
                <li>
                    <span>
                        <img src="images/Image_Large_2.png" alt="Second Scroll Image" />
                    </span>
                    <p>
                        Products<br />
                        Automotive tyres and tubes, Oil,<br />
                        Air and fuel filters.<br />
                        <a href="#">More...</a>
                    </p>
                </li>
                <li>
                    <span>
                        <img src="images/Image_Large_3.png" alt="Third Scroll Image" />
                    </span>
                    <p>
                        Products<br />
                        Hydraulic brake parts, Brake pads, 
                        <br />Brake fluids and radiator coolant.<br />
                        <a href="#">More...</a>
                    </p>                    
                </li>
                <!--<li>
                    <span>
                        <img src="images/Image_Large_4.png" alt="Last Scroll Image" />
                    </span>
                    <p>
                        Products<br />
                        Automotive tyres and tubes, Oil,<br />
                        Air and fuel filters.<br />
                        <a href="#">More...</a>
                    </p>
                </li>-->
            </ul>
                </td>
            </tr>
            <tr>
                <td class="prevNextButtonsHolder">
                    <a href="#" class="prev"><span></span></a>
                    <a href="#" class="next"><span></span></a>
                </td>
            </tr>
        </table>
</asp:Content>
