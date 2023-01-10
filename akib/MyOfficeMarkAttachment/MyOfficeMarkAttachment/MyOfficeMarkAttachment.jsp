<%@ page language="java" buffer="8kb"
import="java.util.ArrayList, java.io.*, java.text.*, java.util.*,
com.csdcsystems.amanda.common.AmandaPath, com.csdcsystems.amanda.common.CsdcData,
com.csdcsystems.amanda.common.CsdcUtility;"
autoFlush="true" isThreadSafe="true" isErrorPage="false" session="false"%>

<jsp:useBean id="JspAide" scope="page" class="com.csdcsystems.amanda.common.AmandaJspBean" />

<% if (JspAide.init("MyOfficeMarkAttachment", request, response, out)) { return; } %>

<%

String sAction = request.getParameter("Action");
String sAttachmentRSN;
Boolean sIsPaymentProcessed;
String sReferenceFiles;
Integer iRowCount = 0;
String sUserMessage = "";

if (sAction != null && sAction.equals("Mark Attachment"))
{
	/*
		create or replace PROCEDURE  OC_MARK_UNRECOVERABLE_ATTCH
		(
		  p_AttachmentRSN  in  varchar2
		)
	*/
	
	sAttachmentRSN = request.getParameter("AttachmentRSN");
	
	//out.println("<p>" + sAttachmentRSN + "</p>");

	CsdcData dwProcCall = JspAide.createNewCsdcData();
	dwProcCall.set_FROM_StoredProcedure("OC_MARK_UNRECOVERABLE_ATTCH");
	dwProcCall.set_StoredProcedure_ARG("p_AttachmentRSN", sAttachmentRSN);
	dwProcCall = JspAide.update(dwProcCall);
	
	//out.println("<p>" + dwProcCall.errorFlag + "</p>");
	//out.println("<p>" + CsdcUtility.getErrorMessageForDisplay(dwProcCall) + "</p>");
	
	sUserMessage = "";
	if (dwProcCall.errorFlag) 
	{ 
		sUserMessage = CsdcUtility.getErrorMessageForDisplay(dwProcCall);
	}  
	else
	{
		sUserMessage = "The OC_ATTACHMENTS_MISSING_LOG table has been updated.";
	}
	
}



%>

<html xmlns="http://www.w3.org/1999/xhtml">
<head>    
	<style type="text/css">@import url("<%=AmandaPath.URL_CUSTOM%>Elements/MyOfficeMarkAttachment/MyOfficeMarkAttachment.css");</style>

	<script language="javascript">
		function submitFORM(path, method, params) 
		{   
			var form = document.createElement("form");    
			form.setAttribute("method", method);    
			form.setAttribute("action", path);     
			form.setAttribute("target", "MyWindow");
			form.setAttribute("onsubmit", "window.open('', this.target, 'width=300,height=500,resizable,scrollbars=yes');return true;");
			
			//Move the submit function to another variable    
			//so that it doesn't get overwritten.    
			form._submit_function_ = form.submit;     

			for(var key in params) {        
				if(params.hasOwnProperty(key)) {            
					var hiddenField = document.createElement("input");            
					hiddenField.setAttribute("type", "hidden");            
					hiddenField.setAttribute("name", key);            
					hiddenField.setAttribute("value", params[key]);             
					form.appendChild(hiddenField);         
				}    
			}     

			document.body.appendChild(form);    
			form._submit_function_();
		}
	</script
</head>

<body>
<center>

  <form name="theform" action="MyOfficeMarkAttachment.jsp" method="post" >
  	  <input type="hidden" name="lid" id="lid" value="<%=JspAide.getLidString()%>">

	  <div class="fieldSetDiv" id="projectDiv" style="width:320px">
		<fieldset><legend><span class="fieldsetHeading"><%=JspAide.getText("LABEL_PROJECT", "Mark Unrecoverable Attachment")%></span></legend>
		  <table cellspacing="1">
			<tr>
				<td align="left">Put in the attachmentrsn you wish to mark as unrecoverable.</td>
			</tr>
			<tr>
				<td align="left">AttachmentRSN: <input type="text" name="AttachmentRSN" style="width:80px" /></td>
			</tr>
			<tr><td><input type="submit" name="Action" value="Mark Attachment" style="width:200px"/></td></tr>
			
			<tr><td><br/><span style="color: red; font-weight: bold;"><%=sUserMessage%></span></td></tr>
			
			<tr><td><hr width="95%"/></td></tr>
			
			<tr><td><input type="Button" name="Action" value="See Missing Attachments Log" onclick="JavaScript:submitFORM('MyOfficeSeeAttchLog.jsp', 'Post', {'lid':'<%=JspAide.getLidString()%>'})" style="width:200px"/></td></tr>
			
			
			
		  </table>
		</fieldset>
	  </div>
  </form>

 </center>

</body>
</html>