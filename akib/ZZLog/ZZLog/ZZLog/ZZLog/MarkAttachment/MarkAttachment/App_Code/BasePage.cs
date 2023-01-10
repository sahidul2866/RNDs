using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.IO;
using System.Web.UI;
using System.Web.UI.HtmlControls;
using System.Web.UI.WebControls;
using System.Data;
using System.Text;
using System.Configuration;
using System.ComponentModel;
using System.Text.RegularExpressions;
using System.Reflection;
using NLog;
using System.DirectoryServices.AccountManagement;
using System.Web.Script.Serialization;

public class BasePage : Page
{
    public static string _env = ConfigurationManager.AppSettings["Environment"].ToString().Trim();
    public static string _sConn = ConfigurationManager.ConnectionStrings[string.Format("Conn_{0}", _env)].ToString();

    public static Logger _logger = LogManager.GetLogger("BasePage");
    public string _NT_Authentication = string.Empty;
    public string sQueryString = "";
    //To implement user validation, windows authentication must be the only one enabled in IIS
    //IIS binding should not have Host Name
    //Following 4 fields required to validate user and user access
    public string sValidateURL = ""; // Get from web.config
    public string sAppID = "";       // Get from web.config. Must be defined in OC_EXTERNALAPPS
    public string sNTUserID = "";    // Will be returned from CallValidateUser
    public string sAccessLevel = ""; // Will be returned from CallValidateUser. 0 - no access. 1 - read only, 2 - can modify, 3 - admin
    public CallValidateUser _validateUser = new CallValidateUser();


    public enum ApplicationEnvironments
    {
        Localhost,
        Development,
        Test,
        Production
    }

    /// <summary>
    /// Returns the current application environment
    /// </summary>
    public static ApplicationEnvironments? ApplicationEnvironment
    {
        get
        {
            ApplicationEnvironments? returnVal = null;

            switch (ConfigurationManager.AppSettings.Get("Environment").ToUpper())
            {
                case "LOCALDEV":
                    returnVal = ApplicationEnvironments.Localhost;
                    break;
                case "DEVELOPMENT":
                    returnVal = ApplicationEnvironments.Development;
                    break;
                case "TESTING":
                    returnVal = ApplicationEnvironments.Test;
                    break;
                case "PRODUCTION":
                    returnVal = ApplicationEnvironments.Production;
                    break;
                default:
                    break;
            }

            return returnVal;
        }
    }

    public static string DataTableToJSONWithJavaScriptSerializer(DataTable table)
    {
        JavaScriptSerializer jsSerializer = new JavaScriptSerializer();
        jsSerializer.MaxJsonLength = 2147483644;
        List<Dictionary<string, object>> parentRow = new List<Dictionary<string, object>>();
        Dictionary<string, object> childRow;
        foreach (DataRow row in table.Rows)
        {
            childRow = new Dictionary<string, object>();
            foreach (DataColumn col in table.Columns)
            {
                childRow.Add(col.ColumnName, Convert.ToString(row[col]));
            }
            parentRow.Add(childRow);
        }
        return jsSerializer.Serialize(parentRow);
    }

    //public Logger _logger1 = LogManager.GetLogger("Page");

    protected virtual void Page_Load(object sender, EventArgs e)
    {
        //string test = "";  //Delete
    }

    public static void SetFavBarMenu(Page p, Dictionary<string, string> param, string CurrentLnk = "", string OtherMenu = "")
    {
        Control divNavigate = p.Master.FindControl("divNavigate") as Control;
        divNavigate.Visible = true;

        Label lblNavigate = p.Master.FindControl("lblNavigate") as Label;

        string aHomeLnk = "<a href='Default.aspx'>Home</a>";
        string aUserLnk = "<a href='UserList.aspx'>Users</a>";
        string aLkLnk = "<a href='LookupList.aspx'>Lookup Items</a>";
        string aHolidayLnk = "<a href='Maintenance.aspx'>Maintenance</a>";
        string aContractLnk = "<a href='ContractList.aspx'>Contracts</a>";

        string sInputLinks = "";
        string sSeperate = " <span style='color:gray;'>/</span> ";
        string sCurrent = " <span style='color:gray;'> " + CurrentLnk + "</span> ";

        foreach (var item in param)
        {
            sInputLinks = sInputLinks + string.Format(@"{0} <a href='{1}'>{2}</a> ", sSeperate, item.Value, item.Key);

        }

        if (OtherMenu == "USER")
        {
            lblNavigate.Text = aUserLnk + sInputLinks + sSeperate + sCurrent;
        }
        else if (OtherMenu == "LOOKUP")
        {
            lblNavigate.Text = aLkLnk + sInputLinks + sSeperate + sCurrent;
        }
        else if (OtherMenu == "HOLIDAY")
        {
            lblNavigate.Text = aHolidayLnk + sInputLinks + sSeperate + sCurrent;
        }
        else if (OtherMenu == "CONTRACT")
        {
            lblNavigate.Text = aContractLnk + sInputLinks + sSeperate + sCurrent;
        }
        else
        {
            lblNavigate.Text = aHomeLnk + sInputLinks + sSeperate + sCurrent;
        }




    }

    public static string GetNTUserName(string sNTUserID)
    {
        string _givenName = "";
        string _surName = "";
        using (PrincipalContext context = new PrincipalContext(ContextType.Domain, "OCGOV"))
        {
            UserPrincipal _user = UserPrincipal.FindByIdentity(context, IdentityType.SamAccountName, sNTUserID);
            string _emailAddress = _user.EmailAddress.Trim();
            string _displayName = _user.DisplayName.Trim();
            _givenName = _user.GivenName.Trim();
            _surName = _user.Surname.Trim();
        }

        return _givenName + " " + _surName;
    }
    public void GetAppSettings()
    {
        _env = ConfigurationManager.AppSettings["Environment"];
        _sConn = ConfigurationManager.ConnectionStrings[string.Format("Conn_{0}", _env)].ToString();
        _logger.Info(" Envoronment: " + _env);
        _NT_Authentication = ConfigurationManager.AppSettings["NT_Authentication"];

        Session["sNTUserID"] = "asarda01";
        Session["sAccessLevel"] = 3;


        try
        {
            sNTUserID = Session["sNTUserID"].ToString().ToLower();
            sAccessLevel = Session["sAccessLevel"].ToString();
            if (!string.IsNullOrEmpty(sNTUserID) && !string.IsNullOrEmpty(sAccessLevel))
                return;
        }
        catch
        {
            // user info not in session
            // proceed to validate
        }

        if (_NT_Authentication == "Y")
        {
            //********************************************************************
            sValidateURL = ConfigurationManager.AppSettings["ValidateURL_" + _env];
            sAppID = ConfigurationManager.AppSettings["AppID"];
            _logger.Log(LogLevel.Info, "sValidateURL: " + sValidateURL);
            _logger.Log(LogLevel.Info, "sAppID: " + sAppID);

            _validateUser.GetUserID_AccessLvl(sValidateURL, sAppID, out sNTUserID, out sAccessLevel);
            sNTUserID = sNTUserID.ToLower();
            _logger.Log(LogLevel.Info, "sNTUserID: " + sNTUserID);
            _logger.Log(LogLevel.Info, "sAccessLevel: " + sAccessLevel);
            //********************************************************************
        }
        else
        {
            _logger.Log(LogLevel.Info, "*** NT AUTHENTICATION DISABLED ******" + sAccessLevel);
            sNTUserID = Request.QueryString["amandaUser"];
            sAccessLevel = "1";
            _logger.Log(LogLevel.Info, "Using Amanda user id: " + sNTUserID);
        }
        Session["sNTUserID"] = sNTUserID;
        Session["sAccessLevel"] = sAccessLevel;

    }
    public void CheckPageAccess(string _pageName, out string _pageAccess)
    {
        //********************************************************************
        sValidateURL = ConfigurationManager.AppSettings["ValidateURL_" + _env];
        string _userID = "";
        _logger.Log(LogLevel.Info, "Page access sAppID: " + _pageName);

        _validateUser.GetUserID_AccessLvl(sValidateURL, _pageName, out _userID, out _pageAccess);
        sNTUserID = sNTUserID.ToLower();
        _logger.Log(LogLevel.Info, "_userID: " + _userID);
        _logger.Log(LogLevel.Info, "_pageAccess: " + _pageAccess);
        //********************************************************************

        Session["_pageName"] = _pageAccess;

    }
    public static string RemoveSpecialCharacters(string str)
    {
        return Regex.Replace(str, "[^a-zA-Z0-9_.]+", "", RegexOptions.Compiled);
    }
}