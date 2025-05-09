﻿var ALERT_TITLE = "Alert";
var ALERT_BUTTON_TEXT = "Ok";

function createCustomAlert(txt) {
    d = document;
    
	if(d.getElementById("modalContainer")) return;
 
	mObj = d.getElementsByTagName("body")[0].appendChild(d.createElement("div"));
	mObj.id = "modalContainer";
	mObj.style.height = d.documentElement.scrollHeight + "px";
 
	alertObj = mObj.appendChild(d.createElement("div"));
	alertObj.id = "alertBox";
	if(d.all && !window.opera) alertObj.style.top = document.documentElement.scrollTop + "px";
	alertObj.style.left = (d.documentElement.scrollWidth - alertObj.offsetWidth)/10 + "px";
	alertObj.style.visiblity="visible";
 
	ah1 = alertObj.appendChild(d.createElement("ah1"));
	ah1.appendChild(d.createTextNode(ALERT_TITLE));
	
	ahr = alertObj.appendChild(d.createElement("div"));
    
	msg = alertObj.appendChild(d.createElement("p"));
	msg.innerHTML = "<br>" + txt;
 
	btn = alertObj.appendChild(d.createElement("a"));
	btn.id = "closeBtn";
	btn.appendChild(d.createTextNode(ALERT_BUTTON_TEXT));
	btn.href = "#";
	btn.focus();
	btn.onclick = function() { removeCustomAlert();return false; }
 
	alertObj.style.display = "block";
 
}
 
function removeCustomAlert() {
	document.getElementsByTagName("body")[0].removeChild(document.getElementById("modalContainer"));
}