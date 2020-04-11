using System;
using System.IO;
using System.Text;
using System.Net;
using System.Threading.Tasks;

namespace Controllers
{
    class ErrorController
    {
        public string NotFound (ref HttpListenerRequest req, ref HttpListenerResponse resp) 
        {
            // Define the path to the content
            string path = "D:\\Development\\C#\\Rizor\\html\\error\\NotFound.html";

            // Load the HTML page into a string
            string[] html = File.ReadAllLines(path);

            // Return the HTML to be sent to the browser
            return string.Join("", html);
        }
    }
}

