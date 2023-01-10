using Oracle.ManagedDataAccess.Client;
using System;
using System.Activities.Expressions;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;

/// <summary>
/// Summary description for MainController
/// </summary>
public class MainController
{
    public MainController()
    {
        //
        // TODO: Add constructor logic here
        //
    }
    public DataTable GetMarkedAttachments(string _sConn, string search="")
    {
        DataTable dt = new DataTable();
        dt.Columns.Add("OC_ATTACHMENTS_MISSING_LOG_ID");
        dt.Columns.Add("attachmentrsn");
        DataRow _ravi = dt.NewRow();
        _ravi["OC_ATTACHMENTS_MISSING_LOG_ID"] = "ravi";
        _ravi["attachmentrsn"] = "500";
        dt.Rows.Add(_ravi);
        return dt;
        String SQL = @"SELECT  OC_ATTACHMENTS_MISSING_LOG.OC_ATTACHMENTS_MISSING_LOG_ID, OC_ATTACHMENTS_MISSING_LOG.attachmentrsn, to_char(OC_ATTACHMENTS_MISSING_LOG.rundate, 'MM/DD/YYYY HH:MI:SS') RUNDATE, DECODE(OC_ATTACHMENTS_MISSING_LOG.recoverystatus, 'U', 'UNRECOVERABLE', 'N/A') RECOVERYSTATUS,  ATTACHMENT.DOSPATH,  ATTACHMENT.TABLENAME,  to_char(ATTACHMENT.STAMPDATE, 'MM/DD/YYYY HH:MI:SS') STAMPDATE,  ATTACHMENT.STAMPUSER
                       FROM OC_ATTACHMENTS_MISSING_LOG INNER JOIN ATTACHMENT ON OC_ATTACHMENTS_MISSING_LOG.attachmentrsn = ATTACHMENT.attachmentrsn
                       WHERE ROWNUM <= 100 AND OC_ATTACHMENTS_MISSING_LOG.attachmentrsn LIKE '%" + search + @"%'
                       ORDER BY OC_ATTACHMENTS_MISSING_LOG.OC_ATTACHMENTS_MISSING_LOG_ID DESC";
        //String vSQL = "SELECT * ";
        //vSQL += " FROM ZZ_LOG ";
        DataTable appList = DataAccess.GetDataFromDB(SQL, _sConn);
        return appList;
    }
    public void runMarkAttachmentProcedure(string rsn, string _sConn)
    {
        String SQL = "Begin OC_MARK_UNRECOVERABLE_ATTCH('" + rsn.Trim() + "'); end;";
        DataAccess.RunSQL_cmd(SQL, _sConn);
    }
    public void runUnMarkAttachmentProcedure(string rsn, string _sConn)
    {
        String SQL = "Begin OC_UNMARK_UNRECOVERABLE_ATTCH('" + rsn.Trim() + "'); end;";
        DataAccess.RunSQL_cmd(SQL, _sConn);
    }
}