using System;
using System.Net;
using Core;
using MemoryCache;

namespace Core
{
    class Cookies
    {


        // Creates the cookie, and returns true
        // @param ref HttpListenerRequest req
        // @param ref HttpListenerResponse resp
        // @param string username
        // @return bool
        public static bool CreateCookie (ref HttpListenerRequest req, ref HttpListenerResponse resp, 
            string CookieName,
            string CookieValue,
            DateTime CookieExpires,
            string CookieDomain,
            string CookiePath,
            bool CookieHTTPOnly,
            bool CookieSecure
        ) {
            // Create session cookie
            Cookie myCookie = new Cookie();
            myCookie.Expires = CookieExpires;
            myCookie.Name = CookieName;
            myCookie.Value = CookieValue;
            myCookie.HttpOnly = CookieHTTPOnly;
            myCookie.Domain = CookieDomain;
            myCookie.Secure = CookieSecure;
            myCookie.Path = CookiePath;

            // Send cookie to browser
            resp.SetCookie(myCookie);

            // Get the cookie
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

