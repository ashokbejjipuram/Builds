<%@ Language=VBScript %>
<%option explicit%>
<%'on error resume next%>
<%if Request.Form("hidSecurity") <> "Impal" or session("branch") = "" then
Response.Redirect("home.asp")
end if%>
<%'if session("branch") <> "CRP"   then	
  '	Response.Redirect "home.asp"
  '	end if
  Response.Buffer =false 

  %> 

<!--
********************************************************************************************
	Asp File Name			:	impal-tran-poindentmain.asp
	Purpose					:	To display the Indent numbers in the dropdown box
	Description				:	Get the Indent numbers from the database and display in 
								the combo box.
	ASP files dependent on	:	dropdownmenu.inc,status.inc,connection.inc,date.inc,
								validate.inc
	Database used			:	IMPAL(MS-SQL SERVER version 7.0 standard edition)
	Table used				:	Purchase_Order_Header
	Remarks					:	
	
	
--------------------------------------------------------------------------------------------
Date					Author					Reviewer					Comment	
Nov 22, 2001		V. Ramesh Raju	

********************************************************************************************
-->

<!--Below - Include files-->
<!--#include file="dropdownmenu.inc"-->

<HTML>
<HEAD>
<TITLE>Impal - Convert To Purchase Order</TITLE>

<SCRIPT Language="JavaScript">
function callPage(page)
{
	form_menu.action =page;
	form_menu.submit();
	//window.location.replace(page)
}

function call()
{
frm1.action="impal-tran-indent2.htm"
frm1.method="post"
frm1.submit 
}

function fnNilWs() {
    document.form_main.cmdsubmit.disabled = true;
    document.form_main.action = "impal-tran-dummy_NilWs.asp";
    document.form_main.method = "post";
    document.form_main.submit()

}

function fnSFWs() {
    document.form_main.cmdsubmit.disabled = true;
    document.form_main.action = "impal-tran-dummy_SFFMS.asp";
    document.form_main.method = "post";
    document.form_main.submit()

}

function fnAbcFms() {
    document.form_main.cmdsubmit.disabled = true;
    document.form_main.action = "impal-tran-dummy_ABCFMS.asp";
    document.form_main.method = "post";
    document.form_main.submit()
}

function fnAbcFms_Nil() {
    document.form_main.cmdsubmit.disabled = true;
    document.form_main.action = "impal-tran-dummy_ABCFMS_Nil.asp";
    document.form_main.method = "post";
    document.form_main.submit()
}

function fnSubmit()
{
	document.form_main.cmdsubmit.disabled = true;
	document.form_main.action = "impal-tran-dummy.asp";
	document.form_main.method = "post";
	document.form_main.submit()
}

function fnSubmit_Safety()
{
	document.form_main.cmdsubmit.disabled = true;
	document.form_main.cmdSafety.disabled = true;
	document.form_main.action = "impal-tran-dummy_safety.asp";
	//document.form_main.method = "post";
 	document.form_main.submit();
}
</SCRIPT>

</HEAD>

<BODY background = "BlowBlueSand.jpg">

<FORM  method="post" id=form_main name=form_main>
	
<BR>
<CENTER>
	<FONT color=#ffffff face=Arial size=4 style="BACKGROUND-COLOR: #336699"><STRONG>ORDERING</STRONG></FONT>
</CENTER>
<BR>
		<STRONG>
		<FONT color=#ffffff face=Arial style="BACKGROUND-COLOR: #336699">
		Branch:<%Response.Write session("branch")%>&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;Date:<%=dat%>&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp; 
		</FONT>
		</STRONG>		
<HR>
			
<CENTER>
<TABLE  width="40%">
<TR>
	<TD ALIGN=middle bgcolor=#336699 colspan="2">
	<STRONG><FONT color=white size=2 face=Arial>INDENT DETAIL</FONT></STRONG>
	</TD>
</TR>
<TR>
	<TD WIDTH="50%" bgColor=gold>
		<FONT size=2 face=arial><STRONG>Indent Number</STRONG></FONT> 
	</TD>
	<TD>
<%
    Dim sAction

    sAction = trim(Request.Form("hidAction"))
    if sAction = "cancel" then
    	Call ClearSessionObject()	'destroy the Object
    end if

    sqlquery = "select po_number from Purchase_Order_Header where upper(isnull(status,'A')) = 'A'" 
    sqlquery = sqlquery & "	and Branch_Code = '" & session("branch") & "'"
    sqlquery = sqlquery & " and substring(Reference_Number,1,3) in ('WRK','ABC','NIL','SFF') "
    sqlquery = sqlquery & "	and PO_Indent_Date is Null"
    sqlquery = sqlquery & "	and PO_Date is Null and PO_Number in"
    sqlquery = sqlquery & "	(select distinct(Indent_Number) from Item_Worksheet_ABCFMS union all" 
    sqlquery = sqlquery & "	select distinct(Indent_Number) from Item_Worksheet_ABCFMS_Nil) order by indent_Date desc"

    call SqlExec(sqlquery) 'this will return a recordset by name adors
%>
		<select name="cboIndNum" id="cboIndNum"  style="HEIGHT: 22px; WIDTH: 174px">
		<option></option>
		<% Do While not adors.eof %>
			<option value="<%=adors(0)%>"><%=adors(0)%></option>
		<% 
			adors.movenext()
		   Loop
		   set adors = nothing
		%>
		</select> 			
	</TD>
		
	<td>
		&nbsp
	</td>
</table>
</CENTER>
<BR>

<CENTER>
<table border=0>
<tr>
<td>
	<INPUT type="Button"  id="cmdsubmit" name="cmdsubmit" VALUE="PROCESS" onClick="fnSubmit()" disabled>
</td>
<td>
    <INPUT type="button" id="cmdsfWs" name="cmdsfWs" value="SF WorkSheet" onclick ="fnSFWs()">
</td>
<td>
    <INPUT TYPE="BUTTON"  id="cmdSafety" name="cmdSafety" value="Safety" Style="Width: 70px" onClick ="fnSubmit_Safety()"  disabled>
</td>

</tr>
<tr>
<td>
    <INPUT type="button" id="cmdABCFMSNIL" name="cmdABCFMSNIL" value="NIL - ABC FMS " Style="Width:125px" onclick ="fnAbcFms_Nil()" >
</td>
<td>
    <INPUT type="button" id="cmdABCFMS" name="cmdAbcFms" value="ABC FMS " Style="Width:125px" onclick ="fnAbcFms()" >
</td>
<td>
<INPUT type="reset" value="RESET">
</td>
</tr>
</table>
	<INPUT type="hidden" id="hidSecurity" name="hidSecurity" value="Impal">
</CENTER>
</FORM>

<script>
<!-- START HIDE
// Set all needed variables
var curmes = 0
var a = 0
var counter = 0
var message = new Array()
message[0] = "Developed by Covansys India, Chennai"
var temp = ""
function typew()
{
a = a + 1
check()
window.status = message[curmes].substring(0, a)
if(a == message[curmes].length + 5)
{
curmes = curmes + 1
a = 0
}
if(curmes > 0)
{
curmes = 0
}
counter = setTimeout("typew()", 100)
}
function check()
{
if(a <= message[curmes].length)
{
if(message[curmes].substring(a, a + 1) == "")
{
a = a + 1
check()
}
}
}
// STOP HIDE -->
</script>
<script>
	typew();
</script>


</BODY>
</HTML>
<%
Sub ClearSessionObject()
	set session("objRsStore") = Nothing
	set session("objRsStore") = server.CreateObject("ADODB.Recordset")
	session("storecount") = 0
End Sub
%>

