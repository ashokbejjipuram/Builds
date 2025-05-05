<%@ LANGUAGE="VBSCRIPT" %>
<%option explicit%>
<%'response.buffer%>
<%on error resume next
if Request.Form("hidSecurity") <> "Impal" or session("branch") = "" then
Response.Redirect("home.asp")
end if%>
<%server.ScriptTimeout = 60000
  session.Timeout = 360 %>
<!--#include file="connection1.inc"-->
<%
	'for each item in Request.Form
	'	Response.Write item & Request.Form(item) &"<p>"
	'	next
	'Response.End
	dim reportname
	Dim path
	dim iLen
	dim Rs
	set Rs =server.CreateObject ("ADODB.Recordset")	
	Dim sqlstr
	dim rs1
	set Rs1 =server.CreateObject ("ADODB.Recordset")	
	Dim sqlstr1
	Dim i

	dim suppfield,suppvalue
	Dim Item_Count  'Added By Umamahes 09.12.2005
	
	Dim cnn1,adConnect, rstobj
    Dim cmdObj
    
    set cnn1	= server.CreateObject  ("ADODB.Connection")
	adConnect	= "driver={SQL Server};server=IMPALSER;uid=sa;pwd=impalser$123;database=IMPAL"
	cnn1.Open adConnect
	
	suppfield = Request.Form("hid_field")
	suppvalue = Request.Form("cbosupp")  
%>

	
<%

reportname = Request.Form("hid_RptName")
'	Response.Write "rep:" & reportname
'	Response.End

If Not IsObject (session("oApp")) Then                              
	Set session("oApp") = Server.CreateObject("Crystal.CRPE.Application")
End If                                                               

'Path = Request.ServerVariables("PATH_TRANSLATED")                     
'Response.Write path
'While (Right(Path, 1) <> "\" And Len(Path) <> 0)                      
'iLen = Len(Path) - 1                                                  
'Path = Left(Path, iLen)                                               
'Wend                                                                  

'path=Request.ServerVariables("Appl_Physical_Path") &  "IMPAL\" & "Reports\"
%>	
<!-- #include file = "path.inc" -->
<%
'Response.Write path

If IsObject(session("oRpt")) then
	Set session("oRpt") = nothing
End if

Set session("oRpt") = session("oApp").OpenReport(path & reportname, 1)

%>
<!-- #include file = "crp_db_change.asp" -->
<%

set session("oRptOptions") = Session("oRpt").Options
session("oRptOptions").MorePrintEngineErrorMessages = 0

Dim value,field,field1, operator_symbol,selection_formula,value1,per,hhidvalarray,val
Dim generateIndentFlag
Dim bi_mui,segment

value = Request.Form("hid_value")
value1 = Request.Form("hid_item")
field = Request.Form("hid_field")
field1 = Request.Form("hid_field1")
per = Request.Form("hidper")
bi_mui = Request.Form("hid_bimui")
segment = Request.Form("hid_segment")
'Response.Write bi_mui
'Response.Write "per:" & per
operator_symbol = "="
hhidvalarray = Request.Form("hidvalarray")
val=split(hhidvalarray,",",-1,1)
'response.Write hhidvalarray
if segment = "segment" then
	hhidvalarray = ""
	hhidvalarray = Request.Form("sel")
	val = split(hhidvalarray,",",-1,1)
end if

'Response.write "val:" & hhidvalarray
'generateIndentFlag = trim(Request.Form("chkgenerateindent"))

generateIndentFlag = "Y"

'Response.Write "generateIndentFlag : " & generateIndentFlag

if hhidvalarray <> "" and segment = "segment" and (reportname <> "Worksheet1_Special.rpt" and reportname <> "Worksheet1_ABCFMS.rpt" and reportname <> "Worksheet1_ABCFMS_Nil.rpt" and reportname <> "Worksheet1_SFFMS.rpt") then
			sqlquery = "delete from item_Worksheet Where substring(item_code,1,3) = '" & suppvalue & "' and branch_Code = '" & session("branch") & "'"
			call sqlexec(sqlquery)
			set adors = nothing
	for i = 0 to ubound(val) step 1
		if val(i) <> "" then
		  	sqlquery = "usp_Worksheet_New1_segments '" & cstr(value) & "','" & session("branch") & "','','','" & cstr(trim(val(i))) & "'"
'			Response.Write "<br>" & sqlquery 
			call sqlexec(sqlquery)
	 	end if
	next

elseif hhidvalarray <> "" and segment = "" and (reportname <> "Worksheet1_Special.rpt" and reportname <> "Worksheet1_ABCFMS.rpt" and reportname <> "Worksheet1_ABCFMS_Nil.rpt" and reportname <> "Worksheet1_SFFMS.rpt") then

dim partnum1,partnum

'	for i=0 to ubound(val) step 1
 '		if val(i) <> "" then
'			if i  < (ubound(val) -1 ) then
 '				partnum1 = "'" & val(i) &"',"	
'				partnum =  partnum1 + partnum 
'			elseif i = (ubound(val)-1) then
 '				partnum1 = "'" & val(i) &"'"	
'				partnum =  partnum + partnum1 
'			end if	
'		end if	
 '    next			
'  			sqlquery = "delete from item_worksheet where substring(item_code,1,3) = '" & suppvalue & "' and supplier_part_number not in (" & cstr(partnum) & ") and branch_code ='" & session("branch") & "'"
'  Response.Write sqlquery 			
			sqlquery = "delete from item_worksheet where substring(item_code,1,3) ='" & suppvalue & "' and branch_code ='" &session("branch") &"'"
	 		call sqlexec(sqlquery)
			set adors = nothing
	for i = 0 to ubound(val) step 1
	   if val(i) <> "" then	
			sqlquery =  "usp_worksheet_new1 " & "'" & cstr(value) & "','" & session("branch") & "','" & cstr(val(i)) & "'"
		'	Response.Write "<br><br>sql1:" & sqlquery
			call sqlexec(sqlquery)
	 	end if
	next

elseif reportname = "Worksheet1_Special.rpt" then
	 	sqlquery =  "usp_WorkSheetNew1_Special " & "'" & cstr(value) & "','" & session("branch") & "','" & cstr(value1) & "','" & cstr(bi_mui) & "'"
'	 	Response.Write "<br>" & sqlquery
		call sqlexec(sqlquery)
		set adors = nothing
		
		sqlstr="SELECT Item_Code FROM Item_WorkSheet_special where substring(item_code,1,3) = '" & suppvalue & "' Order by Supplier_Part_Number"
'Response.Write "<br>" & sqlstr
 		Rs.open sqlstr, adocon		
		While not Rs.EOF 
			sqlstr1= "usp_WorkSheetnew_special '" & session("branch") & "','" & Rs(0) & "'" 
'Response.Write "<br> " & sqlstr1			
			Rs1.Open sqlstr1, adocon
			Rs.MoveNext()
		wend
		Rs.Close
elseif reportname = "Worksheet1_SFFMS.rpt" then
    set adors=nothing

	'Get Table Name and number of columns
	set cmdObj	= server.CreateObject ("ADODB.Command")
	cmdObj.ActiveConnection		= adConnect
	cmdObj.CommandType			= adCmdText
	cmdObj.CommandTimeout		= 0
    sqlquery = "usp_WorkSheetnew1_SFFMS " & "'" & cstr(value) & "','" & session("branch") & "','" & cstr(value1) & "'"			
    'response.Write "<br>" & sqlquery     
    'call sqlexec(sqlquery)
    cmdObj.CommandText	= SqlQuery
    Set rstObj			= cmdObj.Execute()
    set adors=nothing      
elseif reportname = "Worksheet1_ABCFMS.rpt" then
    set adors=nothing

	'Get Table Name and number of columns
	set cmdObj	= server.CreateObject ("ADODB.Command")
	cmdObj.ActiveConnection		= adConnect
	cmdObj.CommandType			= adCmdText
	cmdObj.CommandTimeout		= 0
	if hhidvalarray<>"" then
	    for i = 0 to ubound(val) step 1
	       if val(i) <> "" then	
	            sqlquery = "insert into Temp_Supplier_Part_Number values ('"& suppvalue & "','" & val(i) & "')"
	            'response.Write sqlquery
	            call sqlexec(sqlquery)                
           end if
	    next   
	end if
	 
    sqlquery = "usp_WorkSheetNew1_ABCFMS " & "'" & cstr(value) & "','" & session("branch") & "','" & cstr(value1) & "'"
    'response.Write sqlquery
    cmdObj.CommandText	= SqlQuery
    Set rstObj			= cmdObj.Execute()  
	set adors=nothing      
     
elseif reportname = "Worksheet1_ABCFMS_Nil.rpt" then
    set adors=nothing

	'Get Table Name and number of columns
	set cmdObj	= server.CreateObject ("ADODB.Command")
	cmdObj.ActiveConnection		= adConnect
	cmdObj.CommandType			= adCmdText
	cmdObj.CommandTimeout		= 0
    sqlquery = "usp_WorkSheetnew1_ABCFMS_Nil " & "'" & cstr(value) & "','" & session("branch") & "','" & cstr(value1) & "'"			
'response.Write "<br>" & sqlquery     
    'call sqlexec(sqlquery)
    cmdObj.CommandText	= SqlQuery
    Set rstObj			= cmdObj.Execute()
    set adors=nothing 
else

' wait for process Changed by Umamahes 09.12.2005
'	sqlquery = "Truncate table Item_Worksheet"
'	call sqlexec(sqlquery)
' for Cheching Purpose
' Added by Umamahes for Removing the Existing the Records(Duplicate).
' Process Starting
	
	sqlstr = "SELECT count(*) FROM Item_WorkSheet where substring(item_code,1,3) = '" & suppvalue & "'"
    rs.Open sqlstr,adocon
    while not rs.EOF 
		Item_Count =  rs(0)
		rs.MoveNext() 	
    wend
    rs.Close 
    if Item_Count > 0 then
		sqlquery = "delete from item_worksheet where substring(item_code,1,3) = '" & suppvalue & "'"
		call sqlexec(sqlquery)
	end if	
	
	sqlquery =  "usp_worksheet_new1 " & "'" & cstr(value) & "','" & session("branch") & "','" & cstr(value1) & "','" & cstr(bi_mui) & "'"
	call sqlexec(sqlquery)

	' End of the Process
end if 'End of Else Condition 

'if cstr(bi_mui) <> "" then      
'	sqlquery =  "usp_worksheet_new1 " & "'" & cstr(value) & "','" & session("branch") & "','" & cstr(value1) & "','" & cstr(bi_mui) & "'"
'	call sqlexec(sqlquery)
'end if	

set adors = Nothing

sqlstr="SELECT Item_Code FROM Item_WorkSheet where substring(item_code,1,3) = '" & suppvalue & "' Order by Supplier_Part_Number"
'Response.Write  "<br>" & sqlstr 
Rs.open sqlstr, adocon		
'dim ic
'ic = rs(0)
while not rs.EOF 
	sqlstr1= "usp_worksheet_new '" & session("branch") & "','" & rs(0) & "'" 
'	Response.Write "<br> s:" & sqlstr1
	rs1.Open sqlstr1, adocon
	rs.MoveNext()
wend
Rs.Close
if (reportname <> "NilWorksheet.rpt") and (per = "80" or per = "20")then
	sqlquery = "usp_WorkSheet8020_new  '" & session("branch")&"','" & suppvalue & "'" 'Used for 80/20 Worksheet
	call sqlexec(sqlquery)
end if
' Added By Mahesh on 6th february 2004 for worksheet item locking
sqlquery = "usp_WSItemlocking " & "'" & session("branch") & "'"
call sqlexec(sqlquery)

dim IndentNo

if generateIndentFlag = "Y" then
  if reportname <> "Worksheet1_ABCFMS.rpt" and reportname <> "Worksheet1_ABCFMS_Nil.rpt" and reportname <> "Worksheet1_SFFMS.rpt" then 
	    sqlstr1= "usp_AddWorkSheetIndent  '" & session("branch") & "','" & suppvalue & "'"
  elseif reportname = "Worksheet1_ABCFMS.rpt" then
  		sqlstr1= "usp_AddWorkSheetIndent_ABCFMS  '" & session("branch") & "','" & suppvalue & "'"
  elseif reportname = "Worksheet1_SFFMS.rpt" then
  		sqlstr1= "usp_AddWorkSheetIndent_SFFMS  '" & session("branch") & "','" & suppvalue & "'"
  elseif reportname = "Worksheet1_ABCFMS_Nil.rpt" then
  		sqlstr1= "usp_AddWorkSheetIndent_ABCFMS_Nil  '" & session("branch") & "','" & suppvalue & "'"
  end if	 
	call sqlexec(sqlstr1)
 	if reportname <> "Worksheet1_Special.rpt" and reportname <> "Worksheet1_ABCFMS_Nil.rpt" and reportname <> "Worksheet1_SFFMS.rpt" then
   		sqlstr1 = "UPDATE Item_WorkSheet SET Indent_Number = '" & adors(0)  & "' where supplier_line_code = '" & suppvalue & "'"
   		IndentNo = adors(0)
 		call sqlexec(sqlstr1)
		set adors = Nothing
	    sqlstr1 = "UpDate Item_Worksheet_ABCFMS set Indent_Number='" & IndentNo & "' Where substring(Item_Code,1,3) = '" & suppvalue &"'"
	    call sqlexec(sqlstr1)
	    set adors = Nothing
	    
	elseif reportname = "Worksheet1_ABCFMS_Nil.rpt" then
   		sqlstr1 = "UPDATE Item_WorkSheet SET Indent_Number = '" & adors(0)  & "' where supplier_line_code = '" & suppvalue & "'"
   		IndentNo = adors(0)
 		call sqlexec(sqlstr1)
		set adors = Nothing
	    sqlstr1 = "UpDate Item_Worksheet_ABCFMS_Nil set Indent_Number='" & IndentNo & "' Where substring(Item_Code,1,3) = '" & suppvalue &"'"
	    call sqlexec(sqlstr1)
	    set adors = nothing
	
	elseif reportname = "Worksheet1_SFFMS.rpt" then
   		sqlstr1 = "UPDATE Item_WorkSheet SET Indent_Number = '" & adors(0)  & "' where supplier_line_code = '" & suppvalue & "'"
   		IndentNo = adors(0)
 		call sqlexec(sqlstr1)
		set adors = Nothing
	    sqlstr1 = "UpDate Item_Worksheet_ABCFMS set Indent_Number='" & IndentNo & "' Where substring(Item_Code,1,3) = '" & suppvalue &"'"
	    call sqlexec(sqlstr1)
	    set adors = nothing
	    
	Elseif reportname = "Worksheet1_Special.rpt" then
		sqlstr1 = "UPDATE Item_WorkSheet_Special SET Indent_Number = '" & adors(0)  & "' where substring(item_code,1,3) = '" & suppvalue & "'"
		IndentNo = adors(0)
 		call sqlexec(sqlstr1)
		set adors = Nothing
   	End if	
   	sqlstr1 = "UPDATE Item_WorkSheet SET Indent_Number = '" & IndentNo  & "' where supplier_line_code = '" & suppvalue & "'"
	call sqlexec(sqlstr1)
		
end if

dim selection_formula1
if reportname = "NilWorksheet.rpt" then
'*******************
'******************

set adors=nothing
    sqlquery = "Delete from item_worksheet_Nil Where substring(item_code,1,3)='" & suppvalue & "' and branch_code = '" & session("branch") & "'"
    call sqlexec(sqlquery)
set adors=nothing

    sqlquery = "insert into Item_worksheet_Nil select * from item_Worksheet Where substring(item_code,1,3) = '" & suppvalue & "' and branch_Code = '" & session("branch") & "'"
	call sqlexec(sqlquery)
	set adors = nothing

'****************
'**************
'************


	if value1="" then
		selection_formula1 = cstr(field) & cstr(operator_symbol) & " " & "'" & cstr(value) &"'"
	else
		selection_formula1 = cstr(field) & "='"  & cstr(value) & "' and " & cstr(field1) & "="  & "'" & cstr(value1) &"'"
	end if
	Selection_formula = selection_formula1 & " and ({Item_WorkSheet.Pending_Order_STK} > 0 or {Item_WorkSheet.Pending_Order_STU} > 0 or{Item_WorkSheet.Pending_Order_OTH} > 0 or {Item_WorkSheet.Pending_Order_Ind} > 0 or{Item_WorkSheet.Avrg_Sales_Stk} > 0 or {Item_WorkSheet.Avrg_Sales_STU} > 0 or{Item_WorkSheet.Avrg_Sales_Oth} > 0 or {Item_WorkSheet.Curr_Sales_Stk} > 0 or{Item_WorkSheet.Curr_Sales_STU} > 0 or{Item_WorkSheet.Curr_Sales_Oth} > 0)and{Item_WorkSheet.Total_Stock} < 1 " 
    'Response.Write selection_formula
    'Response.End
else
	selection_formula = cstr(suppfield) & "=" & "'" & cstr(suppvalue) &"'"	
end if
'response.write  selection_formula 
'Response.End
session("oRpt").DiscardSavedData
session("oRpt").RecordSelectionFormula = (selection_formula)

'
'
'====================================================================================
' Retrieve the Records and Create the "Page on Demand" Engine Object
'====================================================================================

On Error Resume Next                                                  
session("oRpt").ReadRecords 
  ' Response.Write "junk"&err.number                                       
If Err.Number <> 0 Then                                               
  Response.Write "An Error has occured on the server in attempting to access the data source"
Else

  If IsObject(session("oPageEngine")) Then                              
  	set session("oPageEngine") = nothing
  End If
set session("oPageEngine") = session("oRpt").PageEngine
End If %>
<%

if reportname = "NilWorksheet.rpt" then
    sqlquery ="delete from item_worksheet Where substring(item_code,1,3)='" & suppvalue & "' and branch_code = '" & session("branch") & "'"
    call sqlexec(sqlquery)
end if

%>
<%                                                               

' INSTANTIATE THE CRYSTAL REPORTS SMART VIEWER
'
'When using the Crystal Reports automation server in an ASP environment, we use
'the same page on demand "Smart Viewers" used with the Crystal Web Report Server.
'The are four Crystal Reports Smart Viewers:
'
'1.  ActiveX Smart Viewer
'2.  Java Smart Viewer
'3.  HTML Frame Smart Viewer
'4.  HTML Page Smart Viewer
'
'The Smart Viewer that you use will based on the browser's display capablities.
'For Example, you would not want to instantiate the Java viewer if the browser
'Line 200
'did not support Java applets.  For purposes on this demo, we have chosen to
'define a viewer.  You can through code determine the support capabilities of
'the requesting browser.  However that functionality is inherent in the Crystal
'Reports automation server and is beyond the scope of this demonstration app.
'
'We have chosen to leverage the server side include functionality of ASP
'for simplicity sake.  So you can use the SmartViewer*.asp files to instantiate
'the smart viewer that you wish to send to the browser.  Simply replace the line
'below with the Smart Viewer asp file you wish to use.
'
'The choices are SmartViewerActiveX.asp, SmartViewerJave.asp,
'SmartViewerHTMLFrame.asp, and SmartViewerHTMLPAge.asp.
'Note that to use this include you must have the appropriate .asp file in the 
'same virtual directory as the main ASP page.
'
'*NOTE* For SmartViewerHTMLFrame and SmartViewerHTMLPage, you must also have
'the files framepage.asp and toolbar.asp in your virtual directory.

viewer = Request.Form("Viewer")

'This line collects the value passed for the viewer to be used, and stores
'it in the "viewer" variable.
viewer="ActiveX (IE Only)"
If "ActiveX (IE Only)" = "ActiveX (IE Only)" then	
%>
<!-- #include file="SmartViewerActiveX.asp" -->
<%
ElseIf cstr(viewer) = "Java" then
%>
<!-- #include file="SmartViewerJava.asp" -->
<%
ElseIf cstr(viewer) = "HTML Frame" then
%>
<!-- #include file="SmartViewerHTMLFrame.asp" -->
<%
Else
%>
<!-- #include file="SmartViewerHTMLPage.asp" -->
<%
End If
'The above If/Then/Else structure is designed to test the value of the "viewer" varaible
'and based on that value, send down the appropriate Crystal Smart Viewer.
%>
<html>
<!-- <meta name="Microsoft Theme" content="none, default"> -->
<script language=vbscript>
	sub PR_back_onclick()
		document.form1.target= ""
		document.form1.ACTION="<%=Request.Form("hid_CallFile")%>?<%=Request.Form("hid_url")%>"
		document.form1.Method="Post"
		document.form1.submit
	end sub
</script>
<body>
<form name="form1">
<table align="right">
<input TYPE="Button" name=cmd_back value="Back" onclick="PR_back_onclick()" tabindex="4">
<input type="hidden" id="hidSecurity" name="hidSecurity" value="Impal">
<input type="hidden" id="hidbranch" name="hidbranch" value=<%session("branch")%>>

</table>
</form>
</body>
</html>



