using System;
using System.Collections.Generic;
using System.Reflection;
using System.IO;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Configuration;

namespace BackOfficeUI
{
    public partial class Site : MasterPage
    {
        public string currentQuery = "";

        protected string VersionDate
        {
            get
            {
                if (Convert.ToString(Application["VersionDate"]) == "")
                {
                    Application["VersionDate"] = File.GetLastWriteTime(Assembly.GetExecutingAssembly().Location).ToShortDateString();
                }
                return Convert.ToString(Application["VersionDate"]);
            }
        }

        public string loggedUserName
        {
            get { return lblUserFullName.Text; }   // get method
            set { lblUserFullName.Text = value; }  // set method
        }

        public string PageTitle
        {
            get { return lblMasterTitle.Text; }   // get method
            set { lblMasterTitle.Text = value; }  // set method
        }

        protected void bttnpdf_Click(object sender, EventArgs e)
        {
            string FilePath = Server.MapPath("Help.pdf");
            System.Net.WebClient User = new System.Net.WebClient();
            Byte[] FileBuffer = User.DownloadData(FilePath);
            if (FileBuffer != null)
            {
                Response.ContentType = "application/pdf";
                Response.AddHeader("content-length", FileBuffer.Length.ToString());
                Response.BinaryWrite(FileBuffer);
            }
        }

        protected void Page_Load(object sender, EventArgs e)
        {

            BasePage controller = new BasePage();
            controller.GetAppSettings();


          //  loggedUserName = BasePage.GetNTUserName(controller.sNTUserID) + " (" + controller.sNTUserID.ToUpper() + ")";

            lblEnvBanner.Text = ConfigurationManager.AppSettings["Environment"].ToString().ToUpper();
            lblEnvBanner.Visible = BasePage.ApplicationEnvironment != BasePage.ApplicationEnvironments.Production ? true : false;

            
 
        }
    }
}