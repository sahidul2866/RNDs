using NLog;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace BackOfficeUI
{
    public partial class Home : BasePage
    {
        MainController controller = new MainController();
        private Logger myLogger = LogManager.GetLogger("MarkAttachment");
        protected override void Page_Load(object sender, EventArgs e)
        {
            ValidationSettings.UnobtrusiveValidationMode = UnobtrusiveValidationMode.None;
            try
            {

                // Content here 
                if (!Page.IsPostBack)
                {
                    GetAppSettings();
                    //Label lblHome = this.Master.FindControl("btnHome") as Label;
                    if (string.IsNullOrEmpty(sNTUserID) || sAccessLevel == "0")
                    {
                        mainPanel.Visible = false;
                        errPanel.Visible = true;

                    }
                    else
                    {
                        mainPanel.Visible = true;
                        errPanel.Visible = false;
                        DataTable dt = controller.GetMarkedAttachments(_sConn);
                        SetData(dt);
                    }
                }
            }
            catch (Exception ex)
            {
                myLogger.Error(ex);

            }
        }

        protected void btnMarkAttachment_Click(object sender, EventArgs e)
        {
            var text = txtAttachmentRSN.Text;
            controller.runMarkAttachmentProcedure(text, _sConn);
            lblErr.Text = "Logs Deleted Successfully";
            lblErr.ForeColor = System.Drawing.Color.Green;
            errPanel.Visible = true;
            DataTable dt = controller.GetMarkedAttachments(_sConn);
            SetData(dt);
        }

        protected void txtAttachmentRSN_TextChanged(object sender, EventArgs e)
        {
            errPanel.Visible = false;
        }

        private void SetData(DataTable dt)
        {

            //Building an HTML string.
            StringBuilder html = new StringBuilder();

            html.Append("<div id='myTable' style=\"background-color:white\">");
            html.Append("<table id='tblLogs' class=\"table table-responsive table-striped table-bordered text-left dataTable no-footer\" style=\"width:100%\"\"");
            html.Append("<thead>");
            html.Append("<tr style='background-color:darkcyan;color:white'>");
            html.Append("<th style='text-align:left;font-size:12pt;padding-left:5px;'>LOG ID</th>");
            html.Append("<th style='text-align:left;font-size:12pt;padding-left:5px;'>ATTACHMENT RSN</th>");
            html.Append("<th style='text-align:left;font-size:12pt;padding-left:5px;'>RUN DATE</th>");
            html.Append("<th style='text-align:left;font-size:12pt;padding-left:5px;'>RECOVERY STATUS</th>");
            html.Append("<th style='text-align:left;font-size:12pt;padding-left:5px;'>DOSPATH</th>");
            html.Append("<th style='text-align:left;font-size:12pt;padding-left:5px;'>TABLENAME</th>");
            html.Append("<th style='text-align:left;font-size:12pt;padding-left:5px;'>STAMPDATE</th>");
            html.Append("<th style='text-align:left;font-size:12pt;padding-left:5px;'>STAMPUSER</th>");
            html.Append("</tr> </thead>");

            if (dt.Rows.Count > 0)
            {
                foreach (DataRow row in dt.Rows)
                {
                    html.Append("<tr  >");
                    html.Append("<td  style='text-align:left;font-size:11pt;padding-left:5px;'   >" + row["OC_ATTACHMENTS_MISSING_LOG_ID"] + "\"> Details_" + row["ZZ_LOG_ID"] + "</a></td>");
                    html.Append("<td  style='text-align:left;font-size:11pt;padding-left:5px;'  >" + row["attachmentrsn"] + "</td>");
                    html.Append("<td  style='text-align:left;font-size:11pt;padding-left:5px;'   >" + row["rundate"] + "</td>");
                    html.Append("<td  style='text-align:left;font-size:11pt;padding-left:5px;'  >" + row["RECOVERYSTATUS"] + "</td>");
                    html.Append("<td  style='text-align:left;font-size:11pt;padding-left:5px;'  >" + row["DOSPATH"] + "</td>");
                    html.Append("<td  style='text-align:left;font-size:11pt;padding-left:5px;'  >" + row["TABLENAME"] + "</td>");
                    html.Append("<td  style='text-align:left;font-size:11pt;padding-left:5px;'  >" + row["STAMPDATE"] + "</td>");
                    html.Append("<td  style='text-align:left;font-size:11pt;padding-left:5px;'  >" + row["STAMPUSER"] + "</td>");
                    html.Append("</tr>");
                }
            }



            else
            {
                html.Append("<tr  >");
                html.Append("<td colspan=2 style='text-align:left;font-size:12pt;padding-left:5px;'   ></br> No Data Found. </td>");
                html.Append("</tr>");
            }

            html.Append("</table>");
            html.Append("</div>");

            //Append the HTML string to Placeholder.
            this.ListDetails.Controls.Add(new Literal { Text = html.ToString() });
            html.Clear();
            html.Length = 0;
            html = null;

        }
    }
}
