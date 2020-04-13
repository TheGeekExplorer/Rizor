using System;
using System.Net;


namespace Core
{
    class Authentication
    {

        // Verifies if a session is OK
        // @param ref HttpListenerRequest req
        // @param ref HttpListenerResponse resp
        // @return string
        public static string VerifySession (ref HttpListenerRequest req, ref HttpListenerResponse resp)
        {   
            // Stop browser caching
            resp.Headers.Add("Cache-Control", "no-cache");

            // Initialise cookie object
             Cookie sessionCookie = new Cookie();

            // Get the session cookie if it exists
            try {
                sessionCookie = Cookies.GetCookie(ref req, ref resp, "SESSION");
            } catch {
                return null;
            }

            // Get cookie values
            string cookieName = sessionCookie.Name;
            string cookieValue = sessionCookie.Value;

            // Check if Cookie is valid
            if (sessionCookie.Expired == true)
                return null;

            // Check cookie is still in cache, not invalidated server side
            string cacheValue = MemoryCache.Universe.Get("Session_" + cookieValue);
            if (cacheValue == null)
                return null;
            
            // Return username
            return cacheValue;
        }


        // Creates a new session, including a session cookie, and a new
        // Hash which is stored in the session cookie, and in the memory
        // cache under the session universe.
        // @param ref HttpListenerRequest req
        // @param ref HttpListenerResponse resp
        // @param string username
        // @return bool
        public static bool CreateSession (ref HttpListenerRequest req, ref HttpListenerResponse resp, string username) 
        {
            // Create a unique hash  
            string hash = Hash.CreateSHA256(DateTime.Now.ToString());

            // Save SessionID in Memory Cache
            MemoryCache.Universe.Add("Session_" + hash, username);

            
            if (!Cookies.CreateCookie(ref req, ref resp, "SESSION", hash, DateTime.MinValue, "localhost", "/", false, false))
                return false;
            

            // Return confirmation
            return true;
        }


        // Destroys a session
        // @param ref HttpListenerRequest req
        // @param ref HttpListenerResponse resp
        // @return string
        public static string DestroySession (ref HttpListenerRequest req, ref HttpListenerResponse resp)
        {   
            // Initialise cookie object
            Cookie sessionCookie = new Cookie();

            // Get the session cookie if it exists
            try {
                sessionCookie = Cookies.GetCookie(ref req, ref resp, "SESSION");
            } catch {
                return null;
            }

            // Get cookie values
            string cookieName = sessionCookie.Name;
            string cookieValue = sessionCookie.Value;

            // Destroy session in memory
            MemoryCache.Universe.Delete("Session_" + cookieValue);

            // Destroy cookie
            sessionCookie.Expired = true;
            sessionCookie.Expires = DateTime.Now;
            resp.SetCookie(sessionCookie);

            return null;
        }

    }
}

