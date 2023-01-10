using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Security;
using System.Web.SessionState;

namespace BackOfficeUI
{
    public partial class Global : System.Web.HttpApplication
    {
        protected void Application_Start(object sender, EventArgs e)
        {
        }
        void Session_Start(object sender, EventArgs e)
        {
            //string sSessionID = Session.SessionID;
            // Code that runs when a new session is started
            //if (Session["QueryString"] == null)
            //{
            //    string currentQuery = Request.Url.Query.ToString();
            //    Session["QueryString"] = currentQuery;
            //    //this is to prevent session timeout error on refresh
            //    Response.Redirect("ExternalAppsMain.aspx");
            //}
        }


    }
}