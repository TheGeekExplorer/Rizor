using System;
using System.IO;
using System.Text;
using System.Net;
using Core;
using MemoryCache;


namespace Controllers
{
    class Auth
    {
        public string Login (ref HttpListenerRequest req, ref HttpListenerResponse resp) 
        {
            // Check if the user login details are in the memory cache
            string username  = Query.Data["username"];
            string password  = Query.Data["password"];
            string password1 = MemoryCache.Universe.Get("LoginDetails_" + username);

            // Check user is authed
            if ((password  != null) && 
                (password1 != null) && 
                (password  == password1)) {

                // Create session cookie
                Cookie myCookie = new Cookie();
                myCookie.Expires = DateTime.MinValue;
                myCookie.Name = "SESSION";
                myCookie.Value = req.QueryString.Get("username");

                // Set response document type to JSON
                resp.ContentType = "application/json";

                // Return an OK
                return "{'status':'OK'}";


            // Not Authed
            } else {

                // Set the response document type to JSON
                resp.ContentType = "application/json";

                // Return NULL as user is not authenticated
                return "{'status':'NOT OK'}";
            }

        }

        
        public string SetPassword (ref HttpListenerRequest req, ref HttpListenerResponse resp) 
        {
            // Get username and password provided from the data layer
            string username  = Query.Data["username"];
            string password  = Query.Data["password"];

            // Set the username and password in the memory cache
            MemoryCache.Universe.Add("LoginDetails_" + username, password);

            // Set the response document type to JSON
            resp.ContentType = "application/json";

            // Return status
            return "{'status':'OK'}";
        }
    }
}

