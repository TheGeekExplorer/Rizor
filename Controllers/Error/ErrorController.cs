using System;
using System.IO;
using System.Text;
using System.Net;
using System.Threading.Tasks;

namespace Controllers
{
    class ErrorController
    {

        // Error Controller
        // @param ref HttpListenerRequest req
        // @param ref HttpListenerResponse resp
        // @return string html
        public string NotFound (ref HttpListenerRequest req, ref HttpListenerResponse resp) 
        {
            // Define the path to the content
            string path = "D:\\Development\\C#\\Rizor\\html\\error\\NotFound.html";

            // Load the HTML page into a string
            string[] html = File.ReadAllLines(path);

            // Set the 404 status code
            resp.StatusCode = 404;

            // Return the HTML to be sent to the browser
            return string.Join("", html);
        }
    }
}

