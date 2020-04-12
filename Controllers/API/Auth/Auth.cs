using System.Net;
using Core;

namespace Controllers
{
    class Auth
    {

        // Logs a User into the Control Panel
        // @param ref HttpListenerRequest req
        // @param ref HttpListenerResponse resp
        // @return string
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
                Cookies.CreateSession(ref req, ref resp, username);

                // Set response document type to JSON
                resp.ContentType = "application/json";

                // Return an OK
                resp.StatusCode = 200;
                return "{'status':'OK'}";


            // Not Authed
            } else {

                // Set the response document type to JSON
                resp.ContentType = "application/json";

                // Return NULL as user is not authenticated
                resp.StatusCode = 403;
                return "{'status':'NOT OK'}";
            }

        }



        // Logs a user out and destroys a session
        // @param ref HttpListenerRequest req
        // @param ref HttpListenerResponse resp
        // @return string
        public string Logout (ref HttpListenerRequest req, ref HttpListenerResponse resp) 
        {
            // Set response document type to JSON
            resp.ContentType = "application/json";

            // Return an OK
            resp.StatusCode = 200;
            return "{'status':'OK'}";            
        }

        
        
        // Creates a new user
        // @param ref HttpListenerRequest req
        // @param ref HttpListenerResponse resp
        // @return string
        public string CreateUser (ref HttpListenerRequest req, ref HttpListenerResponse resp) 
        {
            // Get username and password provided from the data layer
            string username  = Query.Data["username"];
            string password  = Query.Data["password"];

            // Set the username and password in the memory cache
            MemoryCache.Universe.Add("LoginDetails_" + username, password);

            // Set the response document type to JSON
            resp.ContentType = "application/json";

            // Return status
            resp.StatusCode = 200;
            return "{'status':'OK'}";
        }
        


    }
}

