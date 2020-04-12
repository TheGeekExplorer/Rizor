using System;
using System.Net;
using Core;
using MemoryCache;

namespace Core
{
    class Cookies
    {

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

            // Create session cookie
            Cookie myCookie = new Cookie();
            myCookie.Expires = DateTime.MinValue;
            myCookie.Name = "SESSION";
            myCookie.Value = hash;
            myCookie.HttpOnly = false;
            myCookie.Domain = "localhost";
            myCookie.Secure = false;
            myCookie.Path = "/";

            // Send cookie to browser
            resp.SetCookie(myCookie);

            // Return confirmation
            return true;
        }


        // Retrieves the cookie, and returns the value
        // @param ref HttpListenerRequest req
        // @param ref HttpListenerResponse resp
        // @param string username
        // @return bool
        public static Cookie GetCookie (ref HttpListenerRequest req, ref HttpListenerResponse resp, string cookieName) 
        {
            // Check the cookie exists
            if (req.Cookies["SESSION"] == null)
                throw new Exception("No Cookie Exists");

            // Get the cookie
            return req.Cookies["SESSION"];
        }

    }
}

