using System;
using System.IO;
using System.Text;
using System.Net;
using Core;
using MemoryCache;


namespace Controllers
{
    class RizorActionsController
    {
        // Index Controller for the Control Panel
        // @param ref HttpListenerRequest req
        // @param ref HttpListenerResponse resp
        // @return string html
        public string Index (ref HttpListenerRequest req, ref HttpListenerResponse resp) 
        {
            // Check session is active
            string username = Authentication.VerifySession(ref req, ref resp);
            if (username == null)
                resp.Redirect("/login/");

            // Define the path to the content
            string path = Config.Paths.BasePath + "\\html\\public\\auth\\control-panel\\actions\\index.html";

            // Load the HTML page into a string
            string html = File.ReadAllText(path);

            // Send to browser
            return html;
        }
    }
}

