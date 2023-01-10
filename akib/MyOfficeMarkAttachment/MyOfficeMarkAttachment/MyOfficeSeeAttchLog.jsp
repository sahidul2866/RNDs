<%@ page language="java" buffer="8kb"
import="java.util.ArrayList, java.io.*, java.util.*,
com.csdcsystems.amanda.common.AmandaPath, com.csdcsystems.amanda.common.CsdcData,
com.csdcsystems.amanda.common.CsdcUtility;"
autoFlush="true" isThreadSafe="true" isErrorPage="false" session="false"%>

<jsp:useBean id="JspAide" scope="page" class="com.csdcsystems.amanda.common.AmandaJspBean" />

<% if (JspAide.init("MyOfficeSeeAttchLog", request, response, out)) { return; } %>

<%!
public String isN(String inString)
{
	if (inString == null)
		return "";
	else
		return inString.trim();
}
%>

<%

String sAction = request.getParameter("Action");
String SearchTable = "";
String sWhere = "";
String sLogDate ="";


CsdcData cdSearch = JspAide.createNewCsdcData();
cdSearch.setSelect_SELECT(" OC_ATTACHMENTS_MISSING_LOG.OC_ATTACHMENTS_MISSING_LOG_ID, OC_ATTACHMENTS_MISSING_LOG.attachmentrsn, to_char(OC_ATTACHMENTS_MISSING_LOG.rundate, 'MM/DD/YYYY HH:MI:SS') RUNDATE, DECODE(OC_ATTACHMENTS_MISSING_LOG.recoverystatus, 'U', 'UNRECOVERABLE', 'N/A') RECOVERYSTATUS,  ATTACHMENT.DOSPATH,  ATTACHMENT.TABLENAME,  to_char(ATTACHMENT.STAMPDATE, 'MM/DD/YYYY HH:MI:SS') STAMPDATE,  ATTACHMENT.STAMPUSER ");
cdSearch.setSelect_FROM("OC_ATTACHMENTS_MISSING_LOG, ATTACHMENT");
cdSearch.setSelect_WHERE("OC_ATTACHMENTS_MISSING_LOG.attachmentrsn = ATTACHMENT.attachmentrsn");
cdSearch.setSelect_ORDER_BY("OC_ATTACHMENTS_MISSING_LOG.OC_ATTACHMENTS_MISSING_LOG_ID DESC");
cdSearch = JspAide.retrieve(cdSearch);

StringBuilder sbSearchRows = new StringBuilder("<table border=1>");
	
sbSearchRows.append("<tr><td colspan=8 align=center><h3>" + "Total Rows = " + Integer.toString(cdSearch.rowCount) + "</h3></td></tr>");

sbSearchRows.append("<tr>");
sbSearchRows.append("<td>LOG ID</td>");
sbSearchRows.append("<td>ATTACHMENT RSN</td>");
sbSearchRows.append("<td>RUN DATE</td>");
sbSearchRows.append("<td>RECOVERY<br />STATUS</td>");
sbSearchRows.append("<td>DOSPATH</td>");
sbSearchRows.append("<td>TABLENAME</td>");
sbSearchRows.append("<td>STAMPDATE</td>");
sbSearchRows.append("<td>STAMPUSER</td>");
sbSearchRows.append("</tr>");

for(int count=1; count<=cdSearch.rowCount; count++) 
{	sbSearchRows.append("<tr>");
	sbSearchRows.append("<td>" + isN(cdSearch.getItemString(count, "OC_ATTACHMENTS_MISSING_LOG_ID")) + "</td>");
	sbSearchRows.append("<td>" + isN(cdSearch.getItemString(count, "attachmentrsn")) + "</td>");
	sbSearchRows.append("<td>" + isN(cdSearch.getItemString(count, "rundate")) + "</td>");
	sbSearchRows.append("<td>" + isN(cdSearch.getItemString(count, "RECOVERYSTATUS")) + "</td>");
	sbSearchRows.append("<td>" + isN(cdSearch.getItemString(count, "DOSPATH")) + "</td>");
	sbSearchRows.append("<td>" + isN(cdSearch.getItemString(count, "TABLENAME")) + "</td>");
	sbSearchRows.append("<td>" + isN(cdSearch.getItemString(count, "STAMPDATE")) + "</td>");
	sbSearchRows.append("<td>" + isN(cdSearch.getItemString(count, "STAMPUSER")) + "</td>");
	sbSearchRows.append("</tr>");
}

sbSearchRows.append("</table>");


%>

<html>
<head>    
</head>

<body>
<center>
  <form name="theform" action="MyOfficeZZLogSearch.jsp" method="post">
	  <input type="hidden" name="lid" id="lid" value="<%=JspAide.getLidString()%>">
	  <h2 align="center">Missing Attachments Log</h2>
	  
	  <%=sbSearchRows%>
  </form>
</center>
</body>   
</html>






