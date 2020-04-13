using System.Net;
using System.Text.Json;
using System.Text.Json.Serialization;
using System.Collections.Generic;
using Core;

namespace APIControllers
{
    class Auth
    {

        // Logs a User into the Control Panel
        // @param ref HttpListenerRequest req
        // @param ref HttpListenerResponse resp
        // @return string
        public string Login (ref HttpListenerRequest req, ref HttpListenerResponse resp) 
        {
            // Set response document type to JSON
            resp.ContentType = "application/json";

            // Create response JSON dictionary
            Dictionary<string, string> response = new Dictionary<string, string>();


            // Check if the user login details are in the memory cache
            string username  = Query.Data["username"];
            string password  = Query.Data["password"];
            string password1 = MemoryCache.Universe.Get("LoginDetails_" + username);


            // Check user is authed
            if ((password  != null) && 
                (password1 != null) && 
                (password  == password1)) {


                // Create session cookie
                bool s = Authentication.CreateSession(ref req, ref resp, username);

                // Check session could be created, if not then error out.
                if (!s) {
                    resp.StatusCode = 500;
                    response.Add("status","SESSION COULD NOT BE CREATED");
                    return JsonSerializer.Serialize(response);
                }


                // Return an OK
                resp.StatusCode = 200;

                // Build response
                response.Add("status","OK");
                return JsonSerializer.Serialize(response);


            // Not Authed
            } else {

                // Set the response document type to JSON
                resp.ContentType = "application/json";

                // Return NULL as user is not authenticated
                resp.StatusCode = 403;

                // Build response
                response.Add("status","INCORRECT LOGIN CREDENTIALS");
                return JsonSerializer.Serialize(response);  
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

            // Create session cookie
            Authentication.DestroySession(ref req, ref resp);

            // Return an OK
            resp.StatusCode = 200;

            // Create response JSON dictionary
            Dictionary<string, string> response = new Dictionary<string, string>();
            response.Add("status","OK");

            // Send response
            return JsonSerializer.Serialize(response);           
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

            // Create response JSON dictionary
            Dictionary<string, string> response = new Dictionary<string, string>();
            response.Add("status","OK");

            // Send response
            return JsonSerializer.Serialize(response); 
        }
        


    }
}

