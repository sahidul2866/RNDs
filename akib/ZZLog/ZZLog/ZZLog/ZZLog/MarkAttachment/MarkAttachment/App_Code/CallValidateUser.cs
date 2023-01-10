using System;
using System.Collections.Generic;
using System.Net;
using System.Text;
using System.Web;
using System.Web.UI;
using System.IO;
using System.Xml;
using NLog;
using System.Data; 

public class CallValidateUser : Page
{ 

    private Logger _myLogger = LogManager.GetLogger("CallValidateUser"); 

    public CallValidateUser()
    {
        //_url = "";  
    }
    public string GetUserID_AccessLvl(string _url, string _appid, out string _ntid, out string _accesslevel)
    {
        _accesslevel = "0";
        _ntid = "";
        //Sample to HTTP POST to other page
        //var url = "https://localhost.ocfl.net/MyElementsAccess/ValidateUser.aspx";
        //var _form = Request.Form.AllKeys;
        // need this to enable TLS 1.2 on .NET 4.5
        ServicePointManager.SecurityProtocol = SecurityProtocolType.Tls12;
        XmlDocument xDoc = new XmlDocument();
        string responseData = "";
        string tmpNTID = "";
        string tmpAppID = "";
        _ntid = GetLoggedInWindowsUser();

        ServicePointManager.ServerCertificateValidationCallback = delegate { return true; };

        _myLogger.Info(" SessionID: " + Session.SessionID);

        string _string = "NTID=" + _ntid + "&AppID=" + _appid + "&SessionID=" + Session.SessionID;
        byte[] _bytes = Encoding.UTF8.GetBytes(_string);
        Uri _vURI = new Uri(_url);

        var myReq = WebRequest.Create(_vURI);
        myReq.Method = "POST";
        myReq.ContentType = "application/x-www-form-urlencoded";
        myReq.ContentLength = _bytes.Length;
        myReq.UseDefaultCredentials = true;
        myReq.PreAuthenticate = true;
        myReq.Credentials = CredentialCache.DefaultNetworkCredentials;
        
        var reqStream = myReq.GetRequestStream();
        reqStream.Write(_bytes, 0, _bytes.Length);

        var response = myReq.GetResponse();
        Console.WriteLine(((HttpWebResponse)response).StatusDescription);
        try
        {
            var respStream = response.GetResponseStream();

            var streamReader = new StreamReader(respStream);
        
            try
            {
                responseData = streamReader.ReadToEnd();
                Console.WriteLine(responseData);
            }
            finally
            {
                streamReader.Close();
            }
        }
        finally
        {
            response.Close();
        } 

        xDoc.LoadXml(responseData);
        XmlNodeList _list = xDoc.GetElementsByTagName("label");
        foreach (XmlNode xn in _list)
        {
            string id = xn.Attributes["id"].Value; //Get attribute-id 
            string text = xn.InnerText; //Get Text Node
            if (!string.IsNullOrEmpty(id))
            {
                if (id == "lbl_AccessLvl")
                    _accesslevel = text.Trim();
                if (id == "lbl_AppID")
                    tmpAppID = text.Trim();
                if (id == "lbl_ID")
                    tmpNTID = text.Trim();
            }
        }
         
        return _ntid;
    }
     
    public string GetLoggedInWindowsUser()
    {
        string _envUserName = "";
        string _userGetName = "";
        string _Name = "";
        string _httpName = "";
        string _NTID = "";

        try
        {
            _envUserName = Environment.UserName;
            _userGetName = System.Security.Principal.WindowsIdentity.GetCurrent().Name;
            _Name = HttpContext.Current.Request.LogonUserIdentity.Name;
            _httpName = HttpContext.Current.User.Identity.Name.ToString();
            _httpName = _httpName.Substring((_httpName.IndexOf("\\")) + 1);

            _myLogger.Info(" Environment.UserName: " + _envUserName);
            _myLogger.Info(" WindowsIdentity.GetCurrent().Name: " + _userGetName);
            _myLogger.Info(" LogonUserIdentity.Name: " + _Name);

            if (!_userGetName.Contains("IIS APPPOOL"))
            {
                _NTID = _envUserName;
            }
            else if (!_Name.Contains("NT AUTHORITY"))
            {
                _NTID = _Name.Substring(6);
            }
            else
            {
                _myLogger.Error("Windows Authentication in IIS Must be the only one Enabled ");
                _NTID = ""; 
            }

            if (string.IsNullOrEmpty(_NTID))
            {
                _myLogger.Info(" Logged in User: : Error Getting User ID");
            }
            else
            { 
                _myLogger.Info(" Logged in User: " + _NTID);
            } 
        }
        catch (Exception ex)
        {
            _myLogger.Error("Error: Getting user id");
            _myLogger.Error(ex); 
        }

        return _NTID;
        //return "asarda01";
    }
}