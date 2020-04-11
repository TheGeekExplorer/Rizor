using System;
using System.IO;
using System.Text;
using System.Net;
using Core;
using MemoryCache;


namespace Controllers
{
    class LoginController
    {
        public string LoginPage (ref HttpListenerRequest req, ref HttpListenerResponse resp) 
        {
            // Define the path to the content
            string path = "D:\\Development\\C#\\Rizor\\html\\public\\test.html";

            // Load the HTML page into a string
            string html = File.ReadAllText(path);

            // Send to browser
            return html;
        }
    }
}

