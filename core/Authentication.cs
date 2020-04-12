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


        // Destroys a session
        // @param ref HttpListenerRequest req
        // @param ref HttpListenerResponse resp
        // @return string
        public static string DestroySession (ref HttpListenerRequest req, ref HttpListenerResponse resp)
        {  
            return null;
        }

    }
}

