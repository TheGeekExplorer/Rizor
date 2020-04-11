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
        public string HomePage (ref HttpListenerRequest req, ref HttpListenerResponse resp) 
        {
            // Define the path to the content
            string path = "D:\\Development\\C#\\Rizor\\html\\public\\index.html";

            // Load the HTML page into a string
            string html = File.ReadAllText(path);

            // Set some headers
            resp.AddHeader("Powered-By-X", "Rizor!");
            resp.AddHeader("LoadBalancer", "1");

            MemoryCache.Universe.Add("wonder", "WALL!!");
            html += MemoryCache.Universe.Get("wonder");

            // Send to browser
            return html;
        } 


        public string Version (ref HttpListenerRequest req, ref HttpListenerResponse resp) 
        {
            string html = "v0.1<br />\r\nCopyright &copy;Rizor 2020.";
            
            html += MemoryCache.Universe.Get("wonder");

            return string.Join("", html);
        }
    }
}

