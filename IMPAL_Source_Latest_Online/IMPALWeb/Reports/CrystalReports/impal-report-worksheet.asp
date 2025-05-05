<%@ Language=VBScript %>
<%option explicit
'on error resume next

if Request.Form("hidsecurity") <> "Impal" or session("branch") = "" then
		Response.Redirect("home.asp")
end if
%>

<!--
********************************************************************************************
	Asp File Name			:	impal-master-Datetest.asp
	Purpose					:	To Display from & to date
	Description				:	This Screen is useful for the reports passing the Date parameters
								from front end, like From Date and To Date
	ASP files dependent on	:	dropdownmenu.inc,status.inc,connection.inc,date.inc,
								validate.inc
	Database used			:	IMPAL(MS-SQL SERVER version 7.0 standard edition)
	Table used				:	
	Remarks					:	hhidval is null when the screen is opened or at reset
								hhidval is query when find icon is pressed
								hhidval is ins when submit button is pressed for insert
								of new record.
								hhidval is updt when submit button is pressed for updation 
								of record.
--------------------------------------------------------------------------------------------
Date					Author					Reviewer					Comment	


********************************************************************************************-->

<!--Below - Include files-->
<!--#include file="dropdownmenu.inc"-->

<%
set adors=server.CreateObject("ADODB.RECORDSET")	'Instantiating the Recordset

'	hbankname = Request.Form("hidbankname")			'To get the Bank name
'	hhidval=Request.Form("hidval")					'Getting the value of hidden to change the status of the screen
	Dim hhidcode
	Dim hhiddoc
	Dim hiddate
	dim item,cnt,val,val1,value,value1,i,j,index
	Dim hhidval,suppline,hhidvalarray,hhidval2
	hhidval= Request.Form("hidval")
	hhidcode = Request.Form("hidcode")
	hhiddoc  = Request.Form("hiddoc")
	hiddate= Request.Form("cboacc")	
	suppline	= Request.Form("cbosupp")
	item		= Request.Form("hiditem")
	hhidvalarray	=Request.Form("hidvalarray")
'	Response.Write hhidvalarray
	'Response.Write "sel:" & Request.Form("sel")
	'Response.Write "hhidval:" & hhidval
	if hhidval = "nextitem" then
		sqlquery = "select count(*) from Item_Master where Supplier_part_number = " & "'" &  Request.Form("cboitemcode") &"' and substring(item_code,1,3) = "&"'" & Request.Form("cbosupp") &"'"
		call sqlexec(sqlquery)
		cnt = adors(0)
		if cnt < 1 then%>
		<script>
			alert("Invalid Item")
		</script>	
		<%end if
	end if

%>

<HTML>
<HEAD>
<title>Impal - Worksheet</title>

</br>
<center>
	<FONT color=white face=Arial size=4 style="BACKGROUND-COLOR: #336699">
		<STRONG>Reports - Ordering - Stock</STRONG>
	</FONT>
</center>

<BR>
<STRONG>
	<FONT color=white face=Arial style="BACKGROUND-COLOR: #336699"> 
		Branch:<%Response.Write session("branch")%>&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp; 
		&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp; 
		&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp; 
		Date:<%=dat%></FONT>
</STRONG>
<center>

<body onload="generateIndent()">
<form name="frmstate" method="post">
<TABLE align =center">
<tr>
	<td ALIGN=CENTER bgcolor=#336699 colspan=2>
		<STRONG>
			<FONT color=white size=2 face=Arial>Worksheet</FONT>
		</STRONG>
	</TD>
</TR>  
	<tr>
	<TD  bgColor=gold>
		<FONT size=2 face=Arial>
			<STRONG>Supplier Code</STRONG>
		</FONT>
	</TD>
	<%if hhidval = "" then%>
	<TD>
	<%sqlquery="select distinct supplier_code,supplier_name  from supplier_master order by supplier_name "
	call sqlexec(sqlquery)%>
	
		<SELECT id=cbosupp name=cbosupp style="HEIGHT: 22px; WIDTH: 234px;" onchange="subupdt(1)">
		<option></option>
				<%while not adors.EOF %>
					<option value=<%=adors(0)%>><%=adors(1)%></option>
					<%adors.MoveNext()
				wend%>
		</SELECT>
	</TD>
	<%elseif trim(hhidval) = "branch" or hhidval = "nextitem" or hhidval = "remove" then%>
	<TD><%sqlquery="select distinct supplier_code,supplier_name  from supplier_master order by supplier_name "
	call sqlexec(sqlquery)%>
		<select id=cbosupp name=cbosupp style="HEIGHT: 22px; WIDTH: 234px;" onchange="subupdt(1)"> 
			<%while not adors.EOF
								
				if cstr(trim(adors(0))) = cstr(trim(Request.Form("cbosupp"))) then%>
				
					<option  value="<%=adors(0)%>" selected><%=adors(1)%></option>
				<%else %>
					<option value="<%=adors(0)%>"><%=adors(1)%></option>
				<%end if
				adors.MoveNext()
			  wend%>
			</select>
	</TD>
	<%end if%>
	<TD><img src="star.gif" WIDTH="12" HEIGHT="11"></TD>
	
	</tr>
	<tr>
		<TD  bgColor=gold  >
			<FONT size=2 face=Arial>
				<STRONG>Item Code</STRONG>
			</FONT>
		</TD>
		<%if hhidval = "" then%>
		<TD>
			<INPUT id=cboitemcode name=cboitemcode style="HEIGHT: 22px; WIDTH: 234px" Readonly>
		</TD>
		<%elseif hhidval= "branch" or hhidval = "nextitem" or hhidval = "remove" then%>
		<TD>
		<INPUT id=cboitemcode name=cboitemcode style="HEIGHT: 22px; WIDTH: 234px" >
	</TD>   
		<!--<TD>
		<img src="help.gif" alt="Help"  id="imgsearch" name="imgsearch" WIDTH="18" HEIGHT="17" onclick="doselect()">	
	</TD>
		
	<TD>
			<%sqlquery="select distinct item_code,supplier_part_number from item_master where substring(Supplier_Line_Code,1,3)="&"'"& Request.Form("cbosupp")&"'"&" order by supplier_part_number"
			call sqlexec(sqlquery)%>
			<select id="cboitemcode" name="cboitemcode" style="HEIGHT: 22px; WIDTH: 194px" > 
				<option></option>
				<%while not adors.EOF %>
					<option value=<%=adors(0)%>><%=adors(1)%></option>
					<%adors.MoveNext()
				wend%>
			</select>
		</TD>-->
		
	<%end if%>	
	</tr>
	<TR>
		<TD bgColor=gold>
			<FONT size=2 face=Arial>
			<STRONG>Supp. Part #</STRONG> 
			</FONT>
		</TD>
		<%if item = "" or hhidval2 = "" or hhidval2 = "query" then%>
		<TD>
			<INPUT id=txtsupplierpart name=txtsupplierpart  style="HEIGHT: 22px; WIDTH: 234px;BACKGROUND-COLOR: lightgrey" disabled> 
		</TD>
		<%elseif hhidval <> ""  then
		sqlquery = "Select Supplier_Part_Number from Item_Master where item_code = " & "'" & item & "'"
		call sqlexec(sqlquery)%>
		<TD>
			<INPUT id=txtsupplierpart name=txtsupplierpart  style="HEIGHT: 22px; WIDTH: 234px;BACKGROUND-COLOR: lightgrey" disabled value = "<%=adors(0)%>"> 
		</TD>
<%end if%>	
	</TR>	

	<TR>
		<TD bgColor=gold>
			<FONT size=2 face=Arial>
			<STRONG>Generate Indent</STRONG> 
			</FONT>
		</TD>
		
		<TD>
			<INPUT type="checkbox" id="chkgenerateindent" name="chkgenerateindent" style="HEIGHT: 22px;" > 
		</TD>
	</TR>	
	<%if hhidval = "nextitem" then
			if cnt > 0 then val=Request.Form("cboitemcode") end if
			'Response.Write hhidvalarray
		value=split(val,",",-1,1)
		value1=split(hhidvalarray,",",-1,1)
		hhidvalarray=""
		for i=0 to ubound(value1) step 1
			if value1(i) <>"" and instr(hhidvalarray,value1(i))=0 then hhidvalarray=trim(hhidvalarray) & trim(value1(i)) & "," end if
		next
		for i=0 to ubound(value) step 1
			if(value(i)<>"") and instr(hhidvalarray,value(i))=0 then hhidvalarray=trim(hhidvalarray) & trim(value(i)) & "," end if
		next
		value=split(hhidvalarray,",",-1,1)%>
		<Th align = center>
			<Select TYPE=LISTBOX style="HEIGHT: 139px; WIDTH: 220px"  id=sel name=sel multiple>
			<%for i=0 to ubound(value) step 1
		if(value(i)<>"") then%>
		<OPTION selected value="<%=value(i)%>"><%=value(i)%></OPTION>
		<%end if%>
		<%next%>
		</Th>
		<%elseif hhidval = "remove" then
		val=Request.Form("sel")
		value=split(val,",",-1,1)
		val1=split(hhidvalarray,",",-1,1)
		for i=0 to ubound(val1) step 1
			for j=0 to ubound(value) step 1
				index="0"
				if(trim(val1(i))=trim(value(j))) then
					index="1"
					exit for
				end if
			next
			if index="1" then val1(i)="" end if
		next
		hhidvalarray=""
		for i=0 to ubound(val1) step 1
			if(val1(i)<>"") then hhidvalarray = trim(hhidvalarray) & trim(val1(i)) & "," end if
		next
		val1=split(hhidvalarray,",",-1,1)%>
		<TH>
		<SELECT TYPE=LISTBOX style="HEIGHT: 139px; WIDTH: 220px"  id=sel name=sel multiple>
		<%for i=0 to ubound(val1) step 1
			if trim(val1(i))<>"" then%> 
				<OPTION selected value="<%=trim(val1(i))%>"><%=val1(i)%></OPTION>
			<%end if%>
		<%next%>
		</SELECT>
		</TH>
		
		<%end if%>
	</table>
<br>
	
	
<table align="center" border=1>
<%if hhidval = "" then%>
<tr align = center>

<td align = center>
		<input TYPE="button" VALUE="NEXT ITEM" name="cmdnext" style="HEIGHT: 22px; WIDTH: 130px" disabled >
</td>
<td>
		<input TYPE="button" VALUE="REMOVE" name="cmdremove" style="HEIGHT: 22px; WIDTH: 140px" disabled >
</td>
<td >
		<input TYPE="button" VALUE="Full Worksheet" name="cmdreport" style="HEIGHT: 22px; WIDTH: 130px" onclick="FN_callReport()" disabled>
</td>
</tr>
<tr align = center>
<td>
	<input TYPE="button" VALUE="A-B-C-Worksheet" name="cmd80"  style="HEIGHT: 22px; WIDTH: 130px" onclick="FN_callReport2()" disabled>
</td>
<!--<td>
		<input TYPE="button" VALUE="20-Worksheet" name="cmd20"  style="HEIGHT: 22px; WIDTH: 101px" onclick="FN_callReport3()">
</td>-->

<td>
<input type="button" value="BI-MUI WorkSheet" name="bi_mui" style="HEIGHT:22px;WIDTH :140px" onClick="FN_BIMUIcallReport()" disabled>
</td>
<td><input TYPE="button" VALUE="NIL STOCK" name="cmdnil"  style="HEIGHT: 22px; WIDTH: 130px" onclick="FN_callReport1()" disabled></td>	
</TR>
<tr>	
<TD> <Input Type="button" Value="Safety WorkSheet" name="cmdsafety" Style="Height: 22px; Width: 130px" onClick="FN_SaftycallReport()" disabled> </TD>
<TD> <Input Type="button" Value="CD TVS USP" name="cmdcdTvs" Style="Height: 22px; Width: 140px" onClick="FN_CdTvscallReport()" disabled> </TD>
<td> <Input Type ="button" Value ="Special Worksheet" name="cmdSpecial" Style="Height: 22px; Width: 130px" onClick="FN_SpecialReport()" disabled> </td>
</tr>
<tr>
   <td><Input Type ="button" Value ="SF FSN WorkSheet " name="cmdsffns" Style="Height: 22px; Width: 140px" onClick="FN_SFFMSReport()"></td>
	<TD>
	<Input Type ="button" Value ="ABC FMS WorkSheet " name="cmdAbcFms" Style="Height: 22px; Width: 140px" onClick="FN_AbcFmsReport()"> 
    </TD>
	<td>
	   <Input Type ="button" Value ="ABC FMS NIL STOCK " name="cmdAbcFmsNS" Style="Height: 22px; Width: 140px" onClick="FN_AbcFmsNilReport()"> 
	</td>
</tr>

</table>
<table>
</tr>
<TR>
<!--<TD> <Input Type="button" Value="Safety WorkSheet" name="cmdsafety" Style="Height: 22px; Width: 120px" onClick="FN_SaftycallReport()"> </TD>
<TD> <Input Type="button" Value="CD TVS USP" name="cmdcdTvs" Style="Height: 22px; Width: 120px" onClick="FN_CdTvscallReport()"> </TD> -->
</TR>
<Table border=0>
<TR border=1>
</TR>
</Table>
</table>
<table>
<%elseif hhidval = "branch" or hhidval = "nextitem" or hhidval = "remove" then%>
<tr align = center>
	
<td>
		<input TYPE="button" VALUE="NEXT ITEM" name="cmdnext" style="HEIGHT: 22px; WIDTH: 130px" onclick="subupdt(4)">
</td>
<td>
		<input TYPE="button" VALUE="REMOVE" name="cmdremove"  style="HEIGHT: 22px; WIDTH: 140px" onclick="subupdt(5)">
</td>
<td>
		<input TYPE="button" VALUE="Full Worksheet" name="cmdreport" style="HEIGHT: 22px; WIDTH: 130px" onclick="FN_callReport()" disabled>
</td>

</tr>
<tr align = center>
<td>
		<input TYPE="button" VALUE="A-B-C-Worksheet" name="cmd80"  style="HEIGHT: 22px; WIDTH: 130px" onclick="FN_callReport2()" disabled>
</td>
<!--<td>
		<input TYPE="button" VALUE="20-Worksheet" name="cmd20"  style="HEIGHT: 22px; WIDTH: 101px" onclick="FN_callReport3()" disabled>
</td>-->
	<td align=center>
	<input type="button" value="BI-MUI WorkSheet" name="bi_mui" style="HEIGHT:22px;WIDTH :140px" onClick="FN_BIMUIcallReport()" disabled>
	</td>
	<td colspan=2>	<input TYPE="button" VALUE="NIL STOCK" name="cmdnil" style="HEIGHT: 22px; WIDTH: 130px" onclick="FN_callReport1()" disabled> </td>

</tr>
<TR>
<TD> <Input Type="button" Value="Safety WorkSheet" name="cmdsafety" Style="Height: 22px; Width: 130px" onClick="FN_SaftycallReport()" disabled> </TD>
<TD> <Input Type="button" Value="CD TVS USP" name="cmdcdTvs" Style="Height: 22px; Width: 140px" onClick="FN_CdTvscallReport()" disabled> </TD>
<TD> <Input Type ="button" Value ="Special Worksheet" name="cmdSpecial" Style="Height: 22px; Width: 130px" onClick="FN_SpecialReport()" disabled> </TD>
</TR>
<tr>
   <td><Input Type ="button" Value ="SF FSN WorkSheet " name="cmdsffns" Style="Height: 22px; Width: 140px" onClick="FN_SFFMSReport()"> </td>
	<TD>
	   <Input Type ="button" Value ="ABC FMS WorkSheet " name="cmdAbcFms" Style="Height: 22px; Width: 140px" onClick="FN_AbcFmsReport()"> 
    </TD>
	<td>
	   <Input Type ="button" Value ="ABC FMS NIL STOCK " name="cmdAbcFmsNS" Style="Height: 22px; Width: 140px" onClick="FN_AbcFmsNilReport()"> 
	</td>
</tr>
<tr>
<!--	<td align=center>
	<input type="button" value="BI-MUI WorkSheet" name="bi_mui" style="HEIGHT:22px;WIDTH :120px" onClick="FN_BIMUIcallReport()">
	</td>
	<td colspan=2>	<input TYPE="button" VALUE="NIL STOCK" name="cmdnil" style="HEIGHT: 22px; WIDTH: 110px" onclick="FN_callReport1()" > </td> -->
</tr>	
</table>
<table>
<tr align = center>

 </tr> <!--
<TR>
<TD> <Input Type="button" Value="Safety WorkSheet" name="cmdsafety" Style="Height: 22px; Width: 120px" onClick="FN_SaftycallReport()"> </TD>
<TD> <Input Type="button" Value="CD TVS USP" name="cmdcdTvs" Style="Height: 22px; Width: 120px" onClick="FN_CdTvscallReport()"> </TD>
<TD> <Input Type ="button" Value ="Special Worksheet" name="cmdSpecial" Style="Height: 22px; Width: 140px" onClick="FN_SpecialReport()"> </TD>
</TR> -->
<Table border=0>
<TR border=1>
<!--	<TD colspan = 2>
		<Input Type ="button" Value ="Special Worksheet" name="cmdSpecial" Style="Height: 22px; Width: 140px" onClick="FN_SpecialReport()"> </TD>
	</TD> -->
</TR>
</Table>
 
</table>
<%end if%>
</table>
	
        <input type="hidden"	id="hidval"			name="hidval">
        <input type="hidden"	id="hidbankname"	name="hidbankname">
        <Input type="hidden"	name=hidSecurity	value="Impal">
        <input type="hidden" id="hidsuppline"	name="hidsuppline"		value=<%=suppline%>>
        <input type="hidden" id="hiditem"		name="hiditem"			value=<%=item%>>
        <INPUT type="hidden" id="hidvalarray" name="hidvalarray" value="<%=hhidvalarray%>">
        <input type="hidden"	name="hid_RptName"	value="worksheet1.rpt" >
        <input type="hidden"	name="hid_field"	value=mid({item_worksheet.supplier_line_code},1,3)>
        <input type="hidden"	name="hid_field1"	value="{item_worksheet.Item_Code}">
        <input type="hidden"	id="hidbranch"		name="hidbranch"	value=<%=session("branch")%>>
        <input type="hidden"	name="hid_comfield" value="{item_worksheet1.branch_code}" >
        <input type="hidden"	name="hid_CallFile" value="impal-report-worksheet.asp">	
        <input type="hidden"	id="hid_value"		name="hid_value">
        <input type="hidden"	id="hid_item"		name="hid_item">
        <input type="hidden"	id="hidper"		    name="hidper">
        <input type="hidden"    id="hid_bimui"      name="hid_bimui" >
</form>	

<script LANGUAGE="javascript">
	function subupdt(inp1)
{
		document.frmstate.action="impal-report-worksheet.asp"
		document.frmstate.method="post"
		
		if (inp1 == 1)			//getting the screen with comobox   
		{
			document.frmstate.hidval.value = "branch"
			
		}
		else if (inp1 == 2)		//getting into query mode  
		{
			document.frmstate.hidval.value="query"	//query indicates the status for searcing zone name corresponding to selected zone code	
		}
		else if (inp1 == 3)	//getting the starting screen
		{
			document.frmstate.hidval.value=""		
		}
		else if (inp1 == 4)	//getting the starting screen
		{
			if (validate(document.frmstate.cboitemcode.value,"Item code "))
			{
				document.frmstate.cboitemcode.focus()	
				return false
			}
			else
			{
				document.frmstate.hidval.value="nextitem"		
			}
		}
		else if (inp1 == 5)
		{
			document.frmstate.hidval.value = "remove"
		}
		document.frmstate.submit()
}		

function FN_callReport()
{
	if (validate(document.frmstate.cbosupp.value,"Supplier code "))
		{
		document.frmstate.cbosupp.focus()
		}
    else if (document.frmstate.cbosupp.value != "")
			{
			document.frmstate.action= "selectionformulaw-new.asp";
			document.frmstate.hid_value.value = document.frmstate.cbosupp.value;  
			document.frmstate.hid_item.value = document.frmstate.cboitemcode.value;  
			document.frmstate.method="post";
			document.frmstate.submit();
			}
}

function FN_SpecialReport()
{
	if (validate(document.frmstate.cbosupp.value ," Supplier Code"))
	{
		document.frmstate.cbosupp.focus()
	}
	else if(document.frmstate.cbosupp.value != "")
		{
			document.frmstate.action ="selectionformulaw-new.asp";
			document.frmstate.hid_RptName.value = "Worksheet1_Special.rpt"
			document.frmstate.hid_field.value = "mid({item_worksheet.supplier_line_code},1,3)"
			document.frmstate.hid_comfield.value = "{item_worksheet1.branch_code}"
			document.frmstate.hid_value.value = document.frmstate.cbosupp.value;
			document.frmstate.hid_item.value = document.frmstate.cboitemcode.value;
			document.frmstate.method = "post";
			document.frmstate.submit();
		}
}

function FN_SFFMSReport() {
    if (validate(document.frmstate.cbosupp.value, " Supplier Code")) {
        document.frmstate.cbosupp.focus()
    }
    else if (document.frmstate.cbosupp.value != "") {
        document.frmstate.action = "selectionformulaw-new.asp";
        document.frmstate.hid_RptName.value = "Worksheet1_SFFMS.rpt"
        document.frmstate.hid_field.value = "mid({V_Itemworksheet_ABCFMS_Vehicle.Item_code},1,3)"
        document.frmstate.hid_comfield.value = "{V_Itemworksheet_ABCFMS_Vehicle.branch_code}"
        document.frmstate.hid_value.value = document.frmstate.cbosupp.value;
        document.frmstate.hid_item.value = document.frmstate.cboitemcode.value;
        document.frmstate.method = "post";
        document.frmstate.submit();
    }
}

function FN_AbcFmsReport() {
    if (validate(document.frmstate.cbosupp.value, " Supplier Code")) {
        document.frmstate.cbosupp.focus()
    }
    else if (document.frmstate.cbosupp.value != "") {
        document.frmstate.action = "selectionformulaw-new.asp";
        document.frmstate.hid_RptName.value = "Worksheet1_ABCFMS.rpt"
        document.frmstate.hid_field.value = "mid({V_Itemworksheet_ABCFMS_Vehicle.Item_code},1,3)"
        document.frmstate.hid_comfield.value = "{V_Itemworksheet_ABCFMS_Vehicle.branch_code}"
        document.frmstate.hid_value.value = document.frmstate.cbosupp.value;
        document.frmstate.hid_item.value = document.frmstate.cboitemcode.value;
        document.frmstate.method = "post";
        document.frmstate.submit();
    }
}

function FN_AbcFmsNilReport() {
    if (validate(document.frmstate.cbosupp.value, " Supplier Code")) {
        document.frmstate.cbosupp.focus()
    }
    else if (document.frmstate.cbosupp.value != "") {
        document.frmstate.action = "selectionformulaw-new.asp";
        document.frmstate.hid_RptName.value = "Worksheet1_ABCFMS_Nil.rpt"
        document.frmstate.hid_field.value = "mid({V_Itemworksheet_ABCFMS_Nil_Vehicle.Item_code},1,3)"
        document.frmstate.hid_comfield.value = "{V_Itemworksheet_ABCFMS_Nil_Vehicle.branch_code}"
        document.frmstate.hid_value.value = document.frmstate.cbosupp.value;
        document.frmstate.hid_item.value = document.frmstate.cboitemcode.value;
        document.frmstate.method = "post";
        document.frmstate.submit();
    }
}

function FN_callReport1()
{
	if (validate(document.frmstate.cbosupp.value,"Supplier code "))
		{
		document.frmstate.cbosupp.focus()
		}
    else if (document.frmstate.cbosupp.value != "")
			{
			document.frmstate.action= "selectionformulaw-new.asp";
			document.frmstate.hid_RptName.value  = "NilWorksheet.rpt"
			document.frmstate.hid_value.value = document.frmstate.cbosupp.value;  
			document.frmstate.hid_item.value = document.frmstate.cboitemcode.value;  
			document.frmstate.method="post";
			document.frmstate.submit();
			}
}


function FN_callReport2()
{
	if (validate(document.frmstate.cbosupp.value,"Supplier code "))
		{
		document.frmstate.cbosupp.focus()
		}
    else if (document.frmstate.cbosupp.value != "")
			{
			document.frmstate.action= "selectionformulaw-new.asp";
			document.frmstate.hid_RptName.value  = "Worksheet1_top.rpt"
			document.frmstate.hidper.value = "80"
			document.frmstate.hid_value.value = document.frmstate.cbosupp.value;  
			document.frmstate.hid_item.value = document.frmstate.cboitemcode.value;  
			document.frmstate.method="post";
			document.frmstate.submit();
			}
 }
 
function FN_SaftycallReport()
{
	if (validate(document.frmstate.cbosupp.value,"Supplier code "))
		{
		document.frmstate.cbosupp.focus()
		}
    else if (document.frmstate.cbosupp.value != "")
			{
			document.frmstate.action= "selectionformulaw-safety.asp";
			document.frmstate.hid_RptName.value  = "Worksheet1-safety.rpt"
			document.frmstate.hid_field.value = "mid({item_worksheet_Safety.supplier_line_code},1,3)"
			document.frmstate.hid_field1.value = "{item_worksheet_Safety.Item_Code}"
			document.frmstate.hid_value.value = document.frmstate.cbosupp.value;  
			document.frmstate.hid_item.value = document.frmstate.cboitemcode.value;  
			document.frmstate.hidSecurity.value = "Impal"
			document.frmstate.method="post";
			document.frmstate.submit();
			}
}

function FN_CdTvscallReport()
{
	/*if (validate(document.frmstate.cbosupp.value,"Supplier code "))
		{
		document.frmstate.cbosupp.focus()
		}
    else if (document.frmstate.cbosupp.value != "")
			{*/
			document.frmstate.action= "SelectionFormulaw-Safety.asp";
			document.frmstate.hid_RptName.value  = "Worksheet1.rpt"
			document.frmstate.hidper.value = "450"
			document.frmstate.hid_value.value = document.frmstate.cbosupp.value;  
			document.frmstate.hid_item.value = document.frmstate.cboitemcode.value;  
			document.frmstate.method="post";
			document.frmstate.submit()
			//} 
}

 
function FN_callReport3()
{
	if (validate(document.frmstate.cbosupp.value,"Supplier code "))
		{
		document.frmstate.cbosupp.focus()
		}
    else if (document.frmstate.cbosupp.value != "")
			{
			document.frmstate.action= "selectionformulaw-new.asp";
			//document.frmstate.hid_RptName.value  = "Worksheet.rpt"
			document.frmstate.hidper.value = "20"
			document.frmstate.hid_value.value = document.frmstate.cbosupp.value;  
			document.frmstate.hid_item.value = document.frmstate.cboitemcode.value;  
			document.frmstate.method="post";
			document.frmstate.submit();
			}
	
}

function FN_BIMUIcallReport()
{
	if (validate(document.frmstate.cbosupp.value,"Supplier code "))
		{
		document.frmstate.cbosupp.focus()
		}
    else if (document.frmstate.cbosupp.value != "")
			{
			document.frmstate.action= "selectionformulaw-new.asp";
			document.frmstate.hid_RptName.value  = "Worksheet1_BiMui.rpt"
			document.frmstate.hid_bimui.value = "165"
			document.frmstate.hid_value.value = document.frmstate.cbosupp.value;  
			document.frmstate.hid_item.value = document.frmstate.cboitemcode.value;  
			document.frmstate.method="post";
			document.frmstate.submit();
			}
	
}
function doselect()
	{

		window.open ("impal-master-itemquery_sales1.asp?frm=frmstate&hidsuppline=<%=suppline%>",'',"top=0,left=0,bottom=0,width=790,height=550");
		
	}	

function generateIndent()
{
	if (document.frmstate.chkgenerateindent.checked == false)
	{
		document.frmstate.chkgenerateindent.checked = true;
		document.frmstate.chkgenerateindent.disabled = true;
	}
}

</script>
</body>
</HTML>





