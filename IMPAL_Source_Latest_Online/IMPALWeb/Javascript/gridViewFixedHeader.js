
// For GridView Scroll.
// The Width size and Height size needs to be modified as required for each page.
// Do not alter any other properties except Width and Height
function gridViewFixedHeader(gridViewID, gridViewWidth, gridViewHeight) {
}

function gridViewFixedHeader_Dummy(gridViewID, gridViewWidth, gridViewHeight) {

    //Do not change this value.
    var scrollBarWidth = 17;

    if (gridViewHeight == null) {
        // To avoid Vertical scroll bar change the GridView height
        // If you unsure, please do not change the height.
        var gridViewHeight = 480;
    }

    if (gridViewWidth == null) {
        // To avoid Horizontal Scroll for the GridView
        // Comment the Second "var gridViewWidth" statment.
        // If Horizontal Scroll is required for the GridView,
        // uncomment the "var gridViewWidth" statement and comment the first "var gridViewWidth" statement
        var gridViewWidth = parseInt(jQuery('#' + gridViewID).width()) + parseInt(scrollBarWidth);
        //var gridViewWidth = 900;
    }

    //Do not change anything in the below function
    jQuery('#' + gridViewID).gridviewScroll({
        width: gridViewWidth,
        height: gridViewHeight,
        arrowsize: 30,
        varrowtopimg: "http://" + document.location.host + "/images/arrowvt.png",
        varrowbottomimg: "http://" + document.location.host + "/images/arrowvb.png",
        harrowleftimg: "http://" + document.location.host + "/images/arrowhl.png",
        harrowrightimg: "http://" + document.location.host + "/images/arrowhr.png",
        startHorizontal: 0,
        wheelstep: 10
    });
}