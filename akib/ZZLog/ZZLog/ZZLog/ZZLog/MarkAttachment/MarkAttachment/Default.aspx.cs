using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;
using System.Web.Services;
using NLog;

namespace BackOfficeUI
{
    public partial class Default : BasePage
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
                        DataTable dt = controller.GetMarkedAttachments(_sConn, txtSearch.Text);
                        rptMarkAttachmentRows.DataSource = dt;
                        rptMarkAttachmentRows.DataBind();
                    }
                }
            }
            catch (Exception ex)
            {
                myLogger.Error(ex);

            }
        }


        [WebMethod]
        public static object MarkRow(string dID)
        {
            var controller = new MainController();
            try
            {
                controller.runMarkAttachmentProcedure(dID, _sConn);
            }
            catch (Oracle.ManagedDataAccess.Client.OracleException ex)
            {
                return new { issuccess = false };
            }
            return new { issuccess = true };
        }

        [WebMethod]
        public static object UnmarkRow(string dID)
        {
            var controller = new MainController();
            try
            {
                controller.runUnMarkAttachmentProcedure(dID, _sConn);
            }
            catch (Oracle.ManagedDataAccess.Client.OracleException ex)
            {
                return new { issuccess = false };
            }
            return new { issuccess = true };
        }

        protected void btnSearch_Click(object sender, EventArgs e)
        {
            DataTable dt = controller.GetMarkedAttachments(_sConn, txtSearch.Text);
            rptMarkAttachmentRows.DataSource = dt;
            rptMarkAttachmentRows.DataBind();
        }
    }

}