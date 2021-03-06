using System;
using System.IO;
using System.Text;
using System.Net;
using Core;
using MemoryCache;


namespace Controllers
{
    class HomeController
    {

        // Home Controller
        // @param ref HttpListenerRequest req
        // @param ref HttpListenerResponse resp
        // @return string html
        public string HomePage (ref HttpListenerRequest req, ref HttpListenerResponse resp) 
        {
            // Define the path to the content
            string path = Config.Paths.BasePath + "\\html\\public\\index.html";

            // Load the HTML page into a string
            string html = File.ReadAllText(path);

            // Send to browser
            return html;
        }
    }
}

