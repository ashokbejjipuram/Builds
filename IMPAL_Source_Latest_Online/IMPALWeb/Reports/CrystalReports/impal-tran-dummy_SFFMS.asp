                                                                                                                                                                                                                                                                                                                                                                             <%@ Language=VBScript %>
<%option explicit%>
<%'Response.Buffer = true%>

<%
'on error resume next
'Response.Write "child: " & Request.form("hidchild")
'Response.End
if Request.Form("hidsecurity") <> "Impal" or session("branch") = "" then
		Response.Redirect("home.asp")
end if%>


	<!--
********************************************************************************************
	Asp File Name			:	impal-tran-brconsignment.asp
	Purpose					:	To capture theconsignment dettials
	Description				:	Details are queried using the INWARD number
	ASP files dependent on	:	dropdownmenu.inc,status.inc,connection.inc,date.inc,
								validate.inc
	Database used			:	IMPAL(MS-SQL SERVER version 7.0 standard edition)
	Table used				:	Inward_header,Inward_Detail and Item_Master
	Remarks					:	hhidval is null when the screen is opened or at reset
								hhidval is query when STDN number is selected 								
--------------------------------------------------------------------------------------------
Date					Author					Reviewer					Comment	


********************************************************************************************-->

<!--Below - Include files-->
<!--#include file="dropdownmenu.inc"-->
<!--#include file="connection.inc"-->
<head>
<title>Impal - Dummy [SFFMS]</title>
<body>
<center>
	<font color="white" face="Arial" size="4" style="BACKGROUND-COLOR: #336699">
	<strong>INVENTORY - Dummy [ SFFMS ]</strong>
	</font>
</center>

<br>

<strong>
	<font color="white" face="Arial" style="BACKGROUND-COLOR: #336699">
	Branch:<%Response.Write session("branch")%>&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;Date    <%=dat%>	</font>
</strong>

<hr>

<br>

<center>
<form name="frmconsignment" method="post">

<% 	DIM hhidval
	DIM item_code
	Dim sup_part
	Dim Bal_Qty
	DIM Branch_code
	DIM num
	DIM i
	DIM location
	DIM supplier
	Dim list_price
	Dim SLB_Value
	Dim adors1, objConn1, adoCon1
	Dim OS_LS,sqlins,sqlupd
	hhidval=Request.Form("hidval")						'hidden value used to find the status of screen   
	item_code=Request.Form ("cboitemcode")				'Getting the Item code number selected into a variable
	Branch_code=Request.Form ("cbobranchcode")			'Getting the Branch code in to a variable
	supplier = Request.Form ("hidsupplier")

	dim ponumber1,c1,d1,r1,r2,t1,t2
	ponumber1 = Request.Form("cboIndNum")

	if trim(ponumber1) = "" then
		ponumber1 = Request.Form("hidpartnumber1")
	end if

	if Request.form("cbosupplierpartnumber") <> "" then
		sqlquery="select Indent_Number from Item_Worksheet_ABCFMS"
		call sqlexec(sqlquery)
		
		dim indentnumber,suplinecode,sqldel
		
		indentnumber = ponumber1
		'Response.Write indentnumber
		suplinecode = mid(indentnumber,11,3)
		'Response.Write suplinecode
'		sqlins = "select count(item_code) from item_worksheet where item_code = '" & Request.Form("txtitcode") & "'"
		sqlins = "select count(supplier_part_number) from Item_Worksheet_ABCFMS where supplier_part_number = '" & Request.form("cbosupplierpartnumber") & "'"
'		Response.Write sqlins
		call sqlexec(sqlins)
		dim cnt1,sqlqueryins
		cnt1 = adors(0)
		'Response.Write cnt1
		'Response.End
'		if cnt1 > 0 then		
'			sqlupd = "update item_worksheet set to_order_qty = '" & Request.Form("txtaccepted")  & "' where supplier_part_number = '" & Request.Form("cbosupplierpartnumber") & "' and indent_number = '" & ponumber1 & "'"
			'sqlupd = "update item_worksheet set to_order_qty = 0 where item_code = '" & Request.Form("txtitcode") & "' and indent_number = '" & ponumber1 & "'"
'			Response.Write sqlupd
			'sqldel = "Delete from item_worksheet where supplier_part_number = '" & Request.Form("cbosupplierpartnumber") & "'"
			'call sqlexec(sqldel)
'		else

    dim wscount
    dim sqlquerynil
    set adors=nothing
    sqlquerynil ="select isnull(count(*),0) from Item_Worksheet_ABCFMS where item_code = '" & Request.form("txtitcode") & "' and Supplier_Part_Number ='" & request.form("cbosupplierpartnumber") & "' and " & _
             "indent_Number ='" & indentnumber & "' and branch_code ='" & session("branch") & "'"     
    call sqlexec(sqlquerynil)
    wscount = 0
    wscount = adors(0)
    set adors = nothing

    if wscount = 0 then
			sqlqueryins="Insert into Item_Worksheet_ABCFMS(Item_Code,Item_Short_Description,"
			sqlqueryins=sqlqueryins & " Supplier_Part_Number, Doc_On_Hand,(isnull(Less3mth,0) + isnull(Above3mth,0)),"
			sqlqueryins=sqlqueryins & " Po_Qty, Avg_sales, Curr_Month, To_Order_qty, Indent_Number,branch_code)"
			sqlqueryins=sqlqueryins & " values('" & Request.form("txtitcode") & "', '" & Request.Form("txtapplicationsegment") & "',"
			sqlqueryins=sqlqueryins & " '" & request.form("cbosupplierpartnumber") & "',"
			sqlqueryins=sqlqueryins & " '" & Request.form("hidtotalstock") & "', "
			sqlqueryins=sqlqueryins & " '" & Request.form("hidPending_Order_IND") & "', "
			sqlqueryins=sqlqueryins & " '" & Request.form("hidAvrg_Sales_Oth") & "', '" & Request.form("hidCurr_Sales_Oth") & "', "
			sqlqueryins=sqlqueryins & " '" & Request.Form("txtaccepted") & "','" & indentnumber & "','" & session("branch") & "')"
	'Response.Write sqlqueryins			
			call sqlexec(sqlqueryins)
			set adors=nothing			
	end if	'Count nil Checking
'		end if	
	end if
	'Response.End
%>

<table WIDTH="60%">
<tr>
	<TD ALIGN="middle" BGCOLOR="#336699" COLSPAN="9">
		<strong><font color="white" face="Arial" size="2">Dummy Details (Accepted quantity should be Less than or Equal to To-order quantity)</font></strong>
	</TD>
</tr>
<TR ALIGN=CENTER>
	<TD  bgColor=gold width=12%>
		<FONT size=2 face=arial><STRONG>Part #</STRONG></FONT> 
	</TD>
	<TD  bgColor=gold width=12%>
		<FONT size=2 face=arial><STRONG>Description</STRONG></FONT> 
	</TD>
	<TD   bgColor=gold width=3%>
		<FONT size=2 face=arial><STRONG>Stock On Hand</STRONG></FONT> 
	</TD>
	<TD   bgColor=gold width=3%>
		<FONT size=2 face=arial><STRONG>Pending Order</STRONG></FONT> 
	</TD>
	<TD   bgColor=gold width=12%>
		<FONT size=2 face=arial><STRONG>Doc. On Hand</STRONG></FONT> 
	</TD>
	<TD   bgColor=gold width=3%>
		<FONT size=2 face=arial><STRONG>Average Sales</FONT> 
	</TD>
	
	<TD   bgColor=gold width=3%>
		<FONT size=2 face=arial><STRONG>Current Month Sales</FONT> 
	</TD>

	<TD   bgColor=gold width=3%>
		<FONT size=2 face=arial><STRONG>To Order Qty</FONT> 
	</TD>
    <TD   bgColor=gold width=3%>
		<FONT size=2 face=arial><STRONG>Pack. Qty</FONT> 
	</TD>
	<TD   bgColor=gold width=3%>
		<FONT size=2 face=arial><STRONG>Accepted Qty</FONT> 
	</TD>
<!--
	<TD   bgColor=gold width=10%>
		<FONT size=2 face=arial><STRONG>Reason</FONT> 
	</TD> -->

</TR>
	
<%
		dim indnum
		dim a,b,j
		indnum = mid(Request.Form("cboIndNum"),11,3)
		adoCon1="Driver={SQL Server};Server=IMPALSER;database=impal;uid=sa;commandtimeout=60000;pwd=impalser$123"
		
		set objConn1 = server.CreateObject ("ADODB.Connection")
		objConn1.open adoCon1
		set adors1 = server.CreateObject ("ADODB.Recordset")
		sqlquery = "select Vehicle_Type_Description from V_Itemworksheet_ABCFMS_Vehicle where to_order_qty >0 and indent_number = '" & ponumber1 & "' group by Vehicle_Type_Description"
		adors1.ActiveConnection = objConn1		
		adors1.Source = SQLQuery
		adors1.Open()
		
	
	while not adors1.eof
		
		sqlquery = "select count(*) from V_Itemworksheet_ABCFMS_Vehicle a inner join item_master b on a.item_code=b.item_code and b.FSN_Classification='W' and a.to_order_qty >0 and a.indent_number = '" & ponumber1 & "' and a.Vehicle_Type_Description='" & adors1(0)& "'"
' response.Write "<br> " & sqlquery 		
		call sqlexec(sqlquery)
		dim reccount
		reccount = adors(0)
		if reccount>0 then%>
		
			<tr>
			<td align="center" colspan="12"><b><font face="arial"><%=adors1(0) %></font></b></td></tr>
		<%
		i = 0
		for i = 0 to reccount
			if Request.form("hidpartnumber1") = "" then
'				sqlquery = "select supplier_part_number, item_short_description,"
'				sqlquery = sqlquery & " Stock, "
'				sqlquery = sqlquery & " po_Qty as pendingorder," 
'				sqlquery = sqlquery & " Doc_On_Hand as docOnhand," 
'				sqlquery = sqlquery & " Avg_Sales as averagesales, "
'				sqlquery = sqlquery & " Curr_Month as currentmthsales, "
'				sqlquery = sqlquery & " 'To_Order_Qty'= "
'				sqlquery = sqlquery & " case when ABCFMS_Status in('AF','BF','CF') then To_Order_qty "
'				sqlquery = sqlquery & " when ABCFMS_Status in('AM','BM','CM') then To_Order_qty * (75/100) "
'				sqlquery = sqlquery & " when ABCFMS_Status in('AS','BS','CS') then To_Order_qty * (50/100) end,	"
'				sqlquery = sqlquery & " packing_quantity,0 as accepted,'',item_code from Item_Worksheet_ABCFMS "
'				sqlquery = sqlquery & " where To_Order_Qty > 0 and indent_number = '" & ponumber1 & "' order by supplier_part_number"

			    sqlquery = "select supplier_part_number, item_short_description,"
				sqlquery = sqlquery & " Stock, "
				sqlquery = sqlquery & " po_Qty as pendingorder," 
				sqlquery = sqlquery & " Doc_On_Hand as docOnhand," 
				sqlquery = sqlquery & " Avg_Sales as averagesales, "
				sqlquery = sqlquery & " Curr_Month as currentmthsales, "
				sqlquery = sqlquery & " To_Order_qty, "
				sqlquery = sqlquery & " packing_quantity,0 as accepted,'',item_code ,ABCFMS_Status from V_Itemworksheet_ABCFMS_Vehicle "
'				sqlquery = sqlquery & " where To_Order_Qty > 0 and indent_number = '" & ponumber1 & "'  order by supplier_part_number"
				sqlquery = sqlquery & " where To_Order_Qty > 0 and indent_number = '" & ponumber1 & "' and Vehicle_Type_Description='" & adors1(0)& "' order by Vehicle_Type_Description, supplier_part_number"
				'Response.Write "<br> one " & sqlquery				
				call sqlexec(sqlquery)
			else
				sqlquery = "select supplier_part_number, item_short_description,"
				sqlquery = sqlquery & " Stock, "
				sqlquery = sqlquery & " po_Qty as pendingorder," 
				sqlquery = sqlquery & " Doc_On_Hand as docOnhand," 
				sqlquery = sqlquery & " Avg_Sales as averagesales, "
				sqlquery = sqlquery & " Curr_Month as currentmthsales, "
				sqlquery = sqlquery & " To_Order_qty, "
			    sqlquery = sqlquery & " Packing_quantity,0 as accepted,'',item_code,ABCFMS_Status from V_Itemworksheet_ABCFMS_Vehicle "
				sqlquery = sqlquery & " where To_Order_Qty > 0 and indent_number = '" & Request.Form("hidpartnumber1") & "' and Vehicle_Type_Description='" & adors1(0)& "' order by Vehicle_Type_Description, supplier_part_number"
			'	Response.Write "<br> two " & sqlquery
				call sqlexec(sqlquery)
			end if
		
			dim partnumber, description, stockonhand, pendingorder, Avgsales, currentmthsales
			dim toorder, accepted, reason,itemcode
			dim dochand,packqty

		while not adors.eof
		    toorder = 0
			partnumber = adors(0)
			description = adors(1)
			stockonhand = adors(2)
			dochand = adors(3)
			pendingorder = adors(4)
			Avgsales = adors(5)
			currentmthsales = adors(6)
	   if (adors("ABCFMS_Status") = "AF" or adors("ABCFMS_Status") = "BF" or adors("ABCFMS_Status") = "CF") then		
	   	    toorder = adors(7)
	   elseif (adors("ABCFMS_Status") = "AM" or adors("ABCFMS_Status") = "BM" or adors("ABCFMS_Status") = "CM") then
    	    toorder = adors(7) 
    	    toorder = round(cdbl(toorder)*(75/100),0)
	   elseif (adors("ABCFMS_Status") = "AS" or adors("ABCFMS_Status") = "BS" or adors("ABCFMS_Status") = "CS") then
    	    toorder = adors(7) 
    	    toorder = round(cdbl(toorder)*(51/100),0)
'response.Write "<br>" &  (50/100)
'response.Write "<br>" &  round(cdbl(75/100),0)   	    
	   end if
			'toorder = adors(7)
			'reason = adors(8)
'response.Write "<br>" & toorder			
			packqty = adors("packing_quantity")
			itemcode = adors("item_code")
			
		a = Request.form("hidacceptedqty") &"," & 0
'		Response.Write a & "<br>"
		b = split(a,",",-1,1)

		c1 = Request.Form("hidpartnumbers") &"," & Request.Form("cbosupplierpartnumber")
'		Response.write c1
		d1 = split(c1,",",-1,1)			
		
		r1 = Request.Form("hidreason")&","
		r2 = split(r1,",",-1,1)
		
		t1 = Request.Form("hidtoorder")& "," & toorder

'		Response.Write t1
'		Response.End
		t2 = split(t1,",",-1,1)

			%>
		<TR ALIGN=CENTER>
			<TD width= "25%">
				<INPUT id=text1 name=text1 size=18 value="<%=partnumber%>" readonly> 
			</TD>
			<TD width= "25%">
				<INPUT id=text2 name=text2 size=17   value="<%=description%>" readonly> 
			</TD>
			<TD width= "5%">
				<INPUT id=text3 name=text3 size=3  style="TEXT-ALIGN: right"  value="<%=stockonhand%>" readonly> 
			</TD>
			<TD width= "5%">
				<INPUT id=text11 name=text11 size=3 style="TEXT-ALIGN: right"  value="<%=dochand%>" readonly> 
			</TD>
			<TD width= "5%">
				<INPUT id=text4 name=text4 size=3  style="TEXT-ALIGN: right"  value=<%=pendingorder%> readonly> 
			</TD>
			<TD width= "5%">
				<INPUT id=text5 name=text5 size=3 style="TEXT-ALIGN: right"  value="<%=Avgsales%>" readonly> 
			</TD>
			<TD width= "5%">
				<INPUT id=text6 name=text6 size=3 style="TEXT-ALIGN: right"  value="<%=currentmthsales%>" readonly> 
			</TD>
			<TD width= "5%">
				<%
				if Request.form("hidadditem") <> "" then
					if partnumber = Request.form("cbosupplierpartnumber") then
'						Response.Write (i)
											
					%>
						<INPUT id=text7 name=text7 size=6 style="TEXT-ALIGN: right" value=0 onblur="valacceptedqty(<%=i%>)" Maxlength=6 onkeypress="doNumericCheck()" >
					<%else
						if t2(i) > 0  then%>
								<INPUT id=text7 name=text7 size=6 style="TEXT-ALIGN: right" value="<%=toorder%>" onblur="valacceptedqty(<%=i%>)" Maxlength=6 onkeypress="doNumericCheck()" > 
							<%else%>
								<INPUT id=text7 name=text7 size=6 style="TEXT-ALIGN: right" value=0 onblur="valacceptedqty(<%=i%>)" Maxlength=6 onkeypress="doNumericCheck()" > 
							<%end if%>	
					<%end if%>	
				<%else%>
					<INPUT id=text7 name=text7 size=6 style="TEXT-ALIGN: right" value="<%=toorder%>" onblur="valacceptedqty(<%=i%>)" Maxlength=6 onkeypress="doNumericCheck()" readonly>
				<%end if%>
			<TD width= "5%">
				<INPUT id=text9 name=text9 size=3 style="TEXT-ALIGN: right"  value="<%=packqty%>" readonly> 
			</TD>
			<TD width= "5%">
				<%
				
				if Request.form("hidadditem") <> "" then
					if partnumber = Request.form("cbosupplierpartnumber") then%>
						<INPUT id=text8 name=text8 size=6 style="TEXT-ALIGN: right" value="<%=Request.Form("txtaccepted")%>" onblur="valacceptedqty(<%=i%>)" Maxlength=6 onkeypress="doNumericCheck()" >
					<%else
						if partnumber <> Request.form("cbosupplierpartnumber") then	%>	
							<INPUT id=text8 name=text8 size=6 style="TEXT-ALIGN: right" value="<%=b(i)%>" onblur="valacceptedqty(<%=i%>)" Maxlength=6 onkeypress="doNumericCheck()"> 
						<%end if%>
					<%end if%>	
				<%else%>
					<INPUT id=text8 name=text8 size=6 style="TEXT-ALIGN: right" value=0 onblur="valacceptedqty(<%=i%>)" Maxlength=6 onkeypress="doNumericCheck()">
				<%end if%>
			</TD>

			<TD width= "20%">
				<INPUT type = "hidden" id=text10 name=text10 size=15 style="TEXT-ALIGN: right" value="<%=itemcode%>"> 
			</TD>
				
			</TR>
	<%	
		i=i+1
		adors.MoveNext()
	 wend
	next
	
	end if
	
	adors1.MoveNext()
	wend	
'	Response.Write "i : " & i
	set adors=nothing
	sqlquery = "delete from Item_Worksheet_ABCFMS where to_order_qty = 0 and indent_number = '" & Request.Form("hidpartnumber1") & "'"
	call sqlexec(sqlquery) 
%>
</CENTER>
</TABLE>
<BR>

<CENTER>
	<tr>
<!-- 	
<td>
<%if reccount > 0 then %>		
			<input TYPE="button" VALUE="ADD ITEM" name="cmdadditem" onclick="addItem()">
<%Elseif reccount = 0 then %>			
		<input TYPE="button" VALUE="ADD ITEM" name="cmdadditem" onclick="addItem()" Disabled>
<%end if %> 
</td>
-->
	<td>
		<input TYPE="button" VALUE="PROCESS" name="cmdprocess" onclick="process()">
	</td>
	<td>
		<input TYPE="button" VALUE="RESET" name="cmdreset" onclick="resetFrm()">
	</td>
    </tr>
</TABLE>
</CENTER>

    <input type="hidden" id="hidval" name="hidval">
    <input type="hidden" id="hidsupplier" name="hidsupplier" value="<%=supplier%>">
    <input type="hidden" id="hidadditem" name="hidadditem">
    <Input type="hidden" name=hidSecurity value="Impal">
    <Input type="hidden" name=hidform value="frmconsignment">
    <Input type="hidden" id="hidprocess" name="hidprocess">
    <Input type="hidden" name=hidcnt value=<%=i%>>
    <Input type="hidden" name=hidpartnumber value=<%=ponumber1%>>
    <Input type="hidden" name=hidindnum value=<%=indnum%>>
    <input type="hidden" name="hid_RptName" value="Impal-dummy.rpt" >
    <input type="hidden" name="hid_CallFile" value="impal-tran-dummy_SFFMS.asp">
    <input type="hidden" name="hidcount" value="<%=i%>">
</form>

<script>
    function dopart() {
        if (document.frmconsignment.cboitemcode.value == "") {
            alert("Item Code cannot be null")
            document.frmconsignment.cboitemcode.focus()
            return false;
        }
        document.frmconsignment.hidval.value = "part"
        document.frmconsignment.action = "impal-tran-dummy_SFFMS.asp"
        document.frmconsignment.method = "post"
        document.frmconsignment.submit("impal-tran-dummy_SFFMS.asp")
    }

    function subupdt(inp1) {
        /*******************************************************************************************
        Name			:	subupdt
        Purpose			:	Based on parameter change the status of the screen on update.
        Parameters in	:	inp of type int
        Parameters out	:	not applicable
        Called functions:	not applicable
        --------------------------------------------------------------------------------------------
        Revision Log
        WHO					WHEN					ACTION					COMMENTS
        ********************************************************************************************/
        document.frmconsignment.action = "impal-tran-dummy_SFFMS.asp"
        document.frmconsignment.method = "post"
        if (inp1 == 1) {
            document.frmconsignment.hidval.value = "query1"
        }
        else if (inp1 == 3) {
            document.frmconsignment.hidval.value = ""
        }
        document.frmconsignment.submit("impal-tran-dummy_SFFMS.asp")
    }
    function addItem() {

        document.frmconsignment.action = "impal-tran-dummy_SFFMS.asp"
        document.frmconsignment.submit();

    }

    function valacceptedqty(j) {
        var acceptedqty;
        var toorderqty;
        var reason;
        var pqty;
        var moq;
        var moqpercent;
        var calc_value;
        var upd;

        upd = document.frmconsignment.item("text8", j).value;
        upd = upd.replace(" ","");

        if (upd == "" || upd == null) {
            document.frmconsignment.item("text8", j).value = 0;
            upd = 0;
        }
        
        toorderqty = parseInt(frmconsignment.item("text7", j).value);
        acceptedqty = upd;
        pqty  = parseInt(document.frmconsignment.item("text9", j).value);
        
        if (Math.round(pqty) == 0)
            { moq = 1; }
        else if (Math.round(pqty) != 0)
            { moq = pqty; } 
                
        moqpercent = (Math.round(acceptedqty)%Math.round(moq))
        calc_value = (parseInt(toorderqty / pqty) + 1) * parseInt(pqty);

        if (acceptedqty > calc_value) {
            alert('Accepted Quantity Should be Equal to To Order Qty ' + parseInt(pqty) + ' Or Equal to ' + ((parseInt(toorderqty / pqty) + 1) * parseInt(pqty)))
            document.frmconsignment.item("text8", j).focus();
        }
        else {
            if (toorderqty < pqty) 
            {
             if ((acceptedqty !=0 ) && (acceptedqty%pqty !=0))
             {
                alert('Accepted Quantity Should be Equal to ' + ((parseInt(toorderqty / pqty) + 1) * parseInt(pqty)))
                document.frmconsignment.item("text8", j).focus();
             }
            }         
             else if (((toorderqty > pqty) && (acceptedqty % pqty != 0)) && (acceptedqty != 0))
            {    
                alert('Accepted Quantity Should be Equal to Packing Qty ' + parseInt(pqty) + ' Or Equal to ' + ((parseInt(toorderqty / pqty) + 1) * parseInt(pqty)))
                document.frmconsignment.item("text8", j).focus();
            }
        }
    }
     function test1(j, k, m) {
        var acceptedqty;
        var toorderqty;
        var reason;
        var flag;
        flag = true;
        toorderqty = document.frmconsignment.item("text7", j).value;
        acceptedqty = document.frmconsignment.item("text8", j).value;
     }

    function resetFrm() {
        document.frmconsignment.action = "impal-tran-dummy_SFFMS.asp";
        document.frmconsignment.submit();
    }
   function process() {
        var i, flag
        flag = true;
        
        for (i = 0; i < document.frmconsignment.text8.length; i++) {
            toorderqty = document.frmconsignment.item("text7", i).value;
            acceptedqty = document.frmconsignment.item("text8", i).value;
        }
        if (flag) {
            document.frmconsignment.action = "impal-tran-POIndent_nbrBranches_new1_AbcFms.asp"
            document.frmconsignment.target = "_self";
            document.frmconsignment.hidprocess.value = "P"
            document.frmconsignment.submit();
        }
    }
  </script>
</BODY>
</HTML>
