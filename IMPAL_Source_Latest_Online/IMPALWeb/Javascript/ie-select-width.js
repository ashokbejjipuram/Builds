
// Add the Dropdown ID from the view source page to apply the 
// Fix for IE Dropdown Fixed with Issue.
function IEDropDownFixedWidthIssueFix() {
    $('select.dropDownListNormal, select.dropDownListNormalSmall, select.dropDownListNormalBig, select.dropDownListNormalPopup, select.gridviewDropDownList, select.gridviewDropDownListFooter').each(function() {
        ieSelectFix($(this));
    });
}

/* Do Not Change anything on the below script */
$(function() {
    IEDropDownFixedWidthIssueFix();
});

function ieSelectFix(id) {
    $(id).ieSelectWidth
    ({
        containerClassName: 'select-container',
        overlayClassName: 'select-overlay'
    });
}