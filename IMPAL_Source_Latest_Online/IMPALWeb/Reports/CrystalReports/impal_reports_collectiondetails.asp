<%@ Language=VBScript %>
<%option explicit
on error resume next
if Request.Form("hidSecurity") <> "Impal" or session("branch") = "" then
Response.Redirect("home.asp")
end if%>
<!--#include file="dropdownmenu.inc"-->
<%
	Dim hhidval      
	Dim hhidcode     
	Dim hidfrcustomer
	Dim hidtocustomer
	dim customertext
	dim customertextto
	
	hhidval       = Request.Form("hidval")					
	hhidcode      = Request.Form("hidcode")
	hidfrcustomer = Request.Form("cbofromcustomername")	
	hidtocustomer = Request.Form("cbotocustomername")
	if hhidval = "cust" then
		customertext = Request.Form("cbofromcustomername")
	end if 
	
	if hhidval = "cust1" then
		customertext = Request.Form("customertext")
	end if 	
	if hhidval = "tocust1" then
		customertextto = Request.Form("cbotocustomername")
	end if 
	if hhidval = "tocust1" then
		customertextto = Request.Form("customertextto")
	end if

%>

<HTML>
<HEAD>
<TITLE>Impal - Collection Details </TITLE>
<CENTER>
	<FONT color=white face=Arial size=4 style="BACKGROUND-COLOR: #336699">
		<STRONG>Reports - Finance - Cash & Bank</STRONG>
	</FONT>
</CENTER>

<BR>
<STRONG>
	<FONT color=white face=Arial style="BACKGROUND-COLOR: #336699"> 
		Branch:<%Response.Write session("branch")%>&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp; 
		&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp; 
		&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp; 
		Date:<%=dat%>
	</FONT>
</STRONG>
<HR>
<BR>
<CENTER>
<FORM name="frmoutstandingdetails" method="post">
 <TABLE >
	<TR>
		<TD ALIGN=CENTER bgcolor=#336699 colspan=2>
			<STRONG>
				<FONT color=white size=2 face=Arial> Collection Details</FONT>
			</STRONG>
		</TD>
	</TR>  
	<TR>
		<TD  bgColor=gold>
			<FONT size=2 face=Arial>
				<STRONG>Branch</STRONG>
			</FONT>
		</TD>
		<%if hhidval ="" then%>
		<TD>
			<%sqlquery="select branch_code,branch_name from branch_master order by branch_name"
			call sqlexec(sqlquery)%>
			<SELECT id=cbobranch name=cbobranch style="HEIGHT: 22px; WIDTH: 224px; " onchange ="subupdt(1)">
				<OPTION value="<%=""%>"><%=""%></OPTION>
				   <%while not adors.EOF 
					if adors(0) = session("branch") then%>
				<option value="<%=adors(0)%>" selected> <%=adors(1)%></option>
				<%else%>
				<option value="<%=adors(0)%>"><%=adors(1)%></option>
				<%end if%>
					<%adors.MoveNext()
				  wend%>
			</SELECT>
			<td>
			<img src="star.gif" WIDTH="12" HEIGHT="11">
			</td>
		</TD>
		<%elseif hhidval ="branch" or hhidval ="cust" or hhidval ="cust1" or hhidval ="tocust" or hhidval ="tocust1" then
		sqlquery="select Branch_Code,Branch_Name from Branch_Master"
		call sqlexec(sqlquery)%>
		<TD>
			<select id="cbobranch" name="cbobranch" style="HEIGHT: 22px; WIDTH: 224px" onchange="subupdt(1)"> 
			<%while not adors.EOF
				if adors(0) = Request.Form("cbobranch") then%>
					<option selected value="<%=adors(0)%>"><%=adors(1)%></option>
				<%else %>
					<option value="<%=adors(0)%>"><%=adors(1)%></option>
				<%end if
				adors.MoveNext()
			  wend%>
			</select>
		</TD>
		<%end if%>
	</TR>
	
	
	<TR>
		<TD  bgColor=gold>
			<FONT size=2 face=Arial>
				<STRONG>From Customer</STRONG>
			</FONT>
		</TD>
		<td>

		<%if hhidval ="" or hhidval ="branch" then %>
		<INPUT id=cbofromcustomername name=cbofromcustomername style="HEIGHT: 22px; WIDTH: 224px">
		</td>
		<TD>
		<img src="help.gif" alt="Help"  id="imgsearch" name="imgsearch" WIDTH="18" HEIGHT="17" onclick="subupdt(2)">	
		</TD>	
		<TD>
		<%elseif hhidval ="cust" or hhidval ="cust1" or hhidval ="tocust" or hhidval ="tocust1" then
			dim custname
			
			custname = trim(Request.Form("cbofromcustomername"))
			'Response.Write "<br>custname: " & custname
			
			sqlquery = "Select customer_code,Customer_Name,Status from customer_Master where"
			if session("branch") <> "CRP" or( Request.Form("cbobranch")<> "CRP" and session("branch") = "CRP" )then
				sqlquery = sqlquery & " branch_code = '" & Request.Form("cbobranch") & "'and "
			end if	
			if hhidval = "cust" then
				sqlquery = sqlquery & " customer_name like '" & custname & "%'"
			else
				sqlquery = sqlquery & " customer_name like '" & customertext & "%'"
			end if
			sqlquery = sqlquery & "order by customer_name"
			
			'Response.Write sqlquery
			'sqlquery ="SELECT Customer_Code, Customer_Name,Status FROM Customer_Master where Branch_Code=" & "'"&session("branch")&"'"  
			call sqlexec(sqlquery)%>
			<SELECT id=cbofromcustomername name=cbofromcustomername style="HEIGHT: 22px; WIDTH: 224px" onchange = "subupdt(3)">
			<option></option>
			<%while not adors.eof
				if adors(0) = trim(Request.Form("cbofromcustomername")) then %>
				<OPTION selected value ="<%=adors(0)%>"><%=adors(1)%></OPTION>
			<% else %>
				<OPTION value="<%=adors(0)%>"><%=adors(1)%></OPTION>
			<% end if
			adors.MoveNext()
		wend%>
		</TD>
		<td>
			<img src="star.gif" WIDTH="12" HEIGHT="11">
		</td>
	<%end if%>
	<input type="hidden" name="customertext" value ="<%=customertext%>">
	</TR>
	</TR>
		<TR>
		<TD bgColor=gold>
			<FONT size=2 face=Arial>
				<STRONG>To Customer</STRONG>
			</FONT>
		</TD>
		<td>
		<%if hhidval ="" or hhidval ="branch" or hhidval ="cust" or hhidval ="cust1" then %>
			<INPUT id=cbotocustomername name=cbotocustomername style="HEIGHT: 22px; WIDTH: 224px">
			</td>
			<TD>
				<img src="help.gif" alt="Help"  id="imgsearch" name="imgsearch" WIDTH="18" HEIGHT="17" onclick="subupdt(4)">	
			</TD>
		<%elseif hhidval ="tocust" or hhidval ="tocust1" then
			dim custname1
			
			custname1 = trim(Request.Form("cbotocustomername"))
			'Response.Write "<br>custname: " & custname
			
			sqlquery = "Select customer_code,Customer_Name,Status from customer_Master where"
			if session("branch") <> "CRP" or( Request.Form("cbobranch")<> "CRP" and session("branch") = "CRP" )then
				sqlquery = sqlquery & " branch_code = '" & Request.Form("cbobranch") & "'and "
			end if	
			if hhidval = "tocust" then
				sqlquery = sqlquery & " customer_name like '" & custname1 & "%'"
			else
				sqlquery = sqlquery & " customer_name like '" & customertextto & "%'"
			end if
			sqlquery = sqlquery & "order by customer_name"
			
			'Response.Write sqlquery
			'sqlquery ="SELECT Customer_Code, Customer_Name,Status FROM Customer_Master where Branch_Code=" & "'"&session("branch")&"'"  
			call sqlexec(sqlquery)%>
			<SELECT id=cbotocustomername name=cbotocustomername style="HEIGHT: 22px; WIDTH: 224px" onchange = "subupdt(5)">
			<option></option>
			<%while not adors.eof
				if adors(0) = trim(Request.Form("cbotocustomername")) then %>
				<OPTION selected value ="<%=adors(0)%>"><%=adors(1)%></OPTION>
			<% else %>
				<OPTION value="<%=adors(0)%>"><%=adors(1)%></OPTION>
			<% end if
			adors.MoveNext()
		wend%>
		</TD>
		<td>
			<img src="star.gif" WIDTH="12" HEIGHT="11">
		</td>
	<%end if%>
	<input type="hidden" name="customertextto" value ="<%=customertextto%>">
		</tr>
	<tr>
	<TD  bgColor=gold>
		<FONT size=2 face=Arial>
			<STRONG>Date</STRONG>
		</FONT>
	</TD>
	<td>
		<INPUT id=txtfromdate name=txtfromdate style="HEIGHT: 22px; WIDTH: 224px" onkeypress ="PR_callkeypress()" maxlength=10 >
		</TD>
		<td>
			<img src="star.gif" WIDTH="12" HEIGHT="11">
			</td>
		
	</tr>
</table>
	<% if hhidval = "cust1" or hhidval ="tocust1" then 

	dim customerstatus, customercode, Address1, Address2, Address3, Address4, Location
if hhidval = "cust1" then
	sqlquery="select status ,address1,address2,address3,address4,location from customer_master where customer_code = '" & trim(Request.Form("cbofromcustomername")) & "'"
	Call sqlexec(sqlquery)
	customercode = trim(Request.Form("cbofromcustomername"))
elseif hhidval = "tocust1" then
	sqlquery="select status ,address1,address2,address3,address4,location from customer_master where customer_code = '" & trim(Request.Form("cbotocustomername")) & "'"
	Call sqlexec(sqlquery)
	customercode = trim(Request.Form("cbotocustomername"))
end if
	if not adors.eof then
	customerstatus = adors(0)
	Address1 = adors(1)	
	Address2 = adors(2)
	Address3 = adors(3)
	Address4 = adors(4)
	Location = adors(5)
	end if
%>
	<TABLE>

	<TR>
		<TD ALIGN=middle bgcolor=#336699 colspan=6>
		<STRONG><FONT color=white size=2 face=Arial>CUSTOMER INFORMATION</FONT></STRONG>
		</TD>
	</TR>

    <TR>
	<TD WIDTH="50%" bgColor=gold>
	<FONT size=2 face=arial><STRONG>Customer Code</STRONG></FONT> 
	</TD>    
	<TD>
		<INPUT id=txtcustname name=txtcustname  maxlength = 7 style="HEIGHT: 22px; WIDTH: 194px"  style="BACKGROUND-COLOR: lightgrey" readonly value="<%=customercode%>">
	</TD>
	<TD WIDTH="50%" bgColor=gold>
	<FONT size=2 face=arial><STRONG>Address1</STRONG></FONT> 
	</TD>	
	<TD>
	<INPUT type=text id=txtAddress1 name=txtAddress1 style="HEIGHT: 22px; WIDTH: 194px;BACKGROUND-COLOR: lightgrey" maxlength=20 readonly value="<%=Address1%>">    
	</TD>
	
	<TR>
	<TD WIDTH="50%" bgColor=gold>
	<FONT size=2 face=arial><STRONG>Address2</STRONG></FONT> 
	</TD>	
	<TD>
	<INPUT type=text id=txtAddress2 name=txtAddress2 style="HEIGHT: 22px; WIDTH: 194px;BACKGROUND-COLOR: lightgrey" maxlength=20 readonly value="<%=Address2%>">
	</TD>
	<TD WIDTH="50%" bgColor=gold>
	<FONT size=2 face=arial><STRONG>Address3</STRONG></FONT> 
	</TD>	
	<TD>
	<INPUT type=text id=txtAddress3 name=txtAddress3 style="HEIGHT: 22px; WIDTH: 194px;BACKGROUND-COLOR: lightgrey" maxlength=20 readonly value="<%=Address3%>">	
	</TD>
	</TR>
	
	<TR>
	<TD WIDTH="50%" bgColor=gold>
	<FONT size=2 face=arial><STRONG>Address4</STRONG></FONT> 
	</TD>
	<TD>
	<INPUT type=text id=txtAddress4 name=txtAddress4 style="HEIGHT: 22px; WIDTH: 194px;BACKGROUND-COLOR: lightgrey" maxlength=20 readonly value="<%=Address4%>">
	</TD>

	<TD WIDTH="50%" bgColor=gold>
	<FONT size=2 face=arial><STRONG>Location</STRONG></FONT> 
	</TD>
	<TD>
	<INPUT type=text id=txtLocation name=txtLocation style="HEIGHT: 22px; WIDTH: 194px;BACKGROUND-COLOR: lightgrey" maxlength=20 readonly value="<%=Location%>">
	</TD>
	</TR>
	
	</TABLE>
<% end if %>
<table align ="center">

	<TR>
		<TD>&nbsp</TD>
		<TD>
			<input TYPE="button" VALUE="REPORT" name="cmdreport1" onclick="javaScript:FN_callReport()" >
		</TD>	
		
		<TD>
			<input TYPE="button" VALUE="DOCUMENTWISE" name="cmdreport1" onclick="javaScript:FN_callReport1()" >
		</TD>	

		<TD>
			<input TYPE="button" VALUE="RESET" name="cmdreset" onclick="subupdt(6)" >
		</TD>
	</TR>
</TABLE>
	<input type="hidden" id="hidval" name="hidval">
	<input type="hidden" name=hidSecurity value="Impal">
	<input type="hidden" name="hid_RptName" value="impal_reports_collectiondetails.rpt" >
	<input type="hidden" name="hid_field" value="{collection_aging.customer_code}" >
	<input type="hidden" name="hidfrcustomer">
	<input type="hidden" name="hidtocustomer">
	<input type="hidden" id="hidbranch1" name="hidbranch1">
	<input type="hidden" id="hidbranch" name="hidbranch" value="<%=session("branch")%>">
	<input type="hidden" name="hid_comfield" value="{collection_aging.branch_code}" >
	<input type="hidden" name="hid_CallFile" value="impal_reports_collectiondetails.asp">	
	<input type="hidden" id="hiddate" name="hiddate">
	<input type="hidden" name="hid_proc" value="usp_collection_temp">

</FORM>	
<script type="text/vbscript">

sub PR_callkeypress
	Dim L_KeyIn_Char
	L_KeyIn_Char = UCase(Chr(window.event.keyCode))
	
	If Not IsNumeric(Chr(window.event.keyCode)) Then				
		 if  L_KeyIn_Char <> "/"  then
			window.event.returnValue = False	
		 end if	
	End If	
end sub

</script>

<script type="text/javascript">
function subupdt(inp1)
{
    document.frmoutstandingdetails.action = "impal_reports_collectiondetails.asp"
		document.frmoutstandingdetails.method="post"
		if (inp1 == 1)			
		{
			document.frmoutstandingdetails.hidval.value = "branch"
		}
		else if (inp1 == 2)			
		{
			document.frmoutstandingdetails.hidval.value = "cust"
		}
		else if (inp1 == 3)			
		{
			document.frmoutstandingdetails.hidval.value = "cust1"
		}
		else if (inp1 == 4)			
		{
			document.frmoutstandingdetails.hidval.value = "tocust"
		}
		else if (inp1 == 5)			
		{
			document.frmoutstandingdetails.hidval.value = "tocust1"
		}
		else if (inp1 == 6)			
		{
			document.frmoutstandingdetails.hidval.value = ""
		}
		
		document.frmoutstandingdetails.submit()
}

function FN_callReport()
{
		
	if (document.frmoutstandingdetails.cbobranch.value=="")
	{
		alert("Branch should not be null")
		document.frmoutstandingdetails.cbobranch.focus();
	}	
	else if (document.frmoutstandingdetails.cbotocustomername.value < document.frmoutstandingdetails.cbofromcustomername.value)
	{
	alert("To Customer should be greater");
	document.frmoutstandingdetails.cbotocustomername.focus();
	
	}		
	else 
    {  document.frmoutstandingdetails.action = "Selectionos_New1.asp";
	   document.frmoutstandingdetails.hidfrcustomer.value = document.frmoutstandingdetails.cbofromcustomername.value;
	   document.frmoutstandingdetails.hidtocustomer.value = document.frmoutstandingdetails.cbotocustomername.value;  
	   document.frmoutstandingdetails.hidbranch1.value = document.frmoutstandingdetails.cbobranch.value;  
	   document.frmoutstandingdetails.hiddate.value =document.frmoutstandingdetails.txtfromdate.value;
	   document.frmoutstandingdetails.hid_RptName.value = "impal_reports_collectiondetails.rpt"
	   document.frmoutstandingdetails.hid_field.value = "{collection_aging.customer_code}"
	   document.frmoutstandingdetails.hid_comfield.value = "{collection_aging.branch_code}"
	   document.frmoutstandingdetails.hid_proc.value = "usp_collection_temp"
	   document.frmoutstandingdetails.method="post";
	   document.frmoutstandingdetails.target="_self";
	   document.frmoutstandingdetails.submit();
	}
}

function FN_callReport1() {

    if (document.frmoutstandingdetails.cbobranch.value == "") {
        alert("Branch should not be null")
        document.frmoutstandingdetails.cbobranch.focus();
    }
    else if (document.frmoutstandingdetails.cbotocustomername.value < document.frmoutstandingdetails.cbofromcustomername.value) {
        alert("To Customer should be greater");
        document.frmoutstandingdetails.cbotocustomername.focus();

    }
    else {
        document.frmoutstandingdetails.action = "Selectionos_New1.asp";
        document.frmoutstandingdetails.hidfrcustomer.value = document.frmoutstandingdetails.cbofromcustomername.value;
        document.frmoutstandingdetails.hidtocustomer.value = document.frmoutstandingdetails.cbotocustomername.value;
        document.frmoutstandingdetails.hidbranch1.value = document.frmoutstandingdetails.cbobranch.value;
        document.frmoutstandingdetails.hiddate.value = document.frmoutstandingdetails.txtfromdate.value;
        document.frmoutstandingdetails.hid_RptName.value = "Collection_aging_docwise.rpt"
        document.frmoutstandingdetails.hid_field.value = "{Collection_temp.cust_code}"
        document.frmoutstandingdetails.hid_comfield.value = "{Branch_Master.branch_code}"
        document.frmoutstandingdetails.hid_proc.value = "usp_collection_temp"
        document.frmoutstandingdetails.method = "post";
        document.frmoutstandingdetails.target = "_self";
        document.frmoutstandingdetails.submit();
    }
}
</script>
</body>
</HTML>









