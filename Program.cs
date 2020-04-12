using System;
using System.IO;
using System.Text;
using System.Net;
using System.Threading.Tasks;
using Controllers;
using Core;

namespace WebServer
{
    class HttpServer
    {
        public static HttpListener listener;
        public static string url = "http://localhost:8000/";
        public static int pageViews = 0;
        public static int requestCount = 0;


        public static async Task HandleIncomingConnections()
        {
            bool runServer = true;

            // While a user hasn't visited the `shutdown` url, keep on handling requests
            while (runServer)
            {
                // Create default admin user
                MemoryCache.Universe.Add("LoginDetails_mark", "lol123");

                // Will wait here until we hear from a connection
                HttpListenerContext ctx = await listener.GetContextAsync();
                HttpListenerRequest req = ctx.Request;
                HttpListenerResponse resp = ctx.Response;
                byte[] data = new byte[]{};

                // Output requested url to console
                Console.WriteLine(req.Url.AbsolutePath);


                // Put the POST / GET data into the state object
                Query.GetQuery(req);


                // Determine what sort of file request it is
                MimeTypes mime = new MimeTypes();
                string[] filetype = mime.Determine(req.Url.AbsolutePath);


                // Is a Binary asset
                if (filetype[0] == "binary")
                {
                    // Load the asset
                    LoadAsset asset = new LoadAsset();
                    data = asset.LoadBinary(req.Url.AbsolutePath);

                    // If not found send a 404
                    if (data == null) {
                        resp.StatusCode = 404;
                        resp.Close();
                        continue;
                    }

                    // Write the response info
                    resp.ContentType = filetype[1];
                    resp.ContentLength64 = data.LongLength;


                // Is a text file asset
                } else if (filetype[0] == "text")
                {
                    // Load the asset
                    LoadAsset asset = new LoadAsset();
                    data = asset.LoadText(req.Url.AbsolutePath);
                    
                    // If not found send a 404
                    if (data == null) {
                        resp.StatusCode = 404;
                        resp.Close();
                        continue;
                    }

                    // Write the response info
                    resp.ContentType = filetype[1];
                    resp.ContentLength64 = data.LongLength;



                // Is a controller
                } else if (filetype[0] == "controller")
                {
                    // Write the response info
                    resp.StatusCode = 200;
                    resp.ContentType = filetype[1];
                    resp.ContentEncoding = Encoding.UTF8;

                    // The router will determine which controller to run
                    Router router = new Router();
                    data = Encoding.UTF8.GetBytes(
                        router.FindRoute(ref req, ref resp)
                    );

                    // Set the content lenghth
                    resp.ContentLength64 = data.LongLength;

                } else
                {
                    // Shutdown - If shutdown url requested w/ POST, then shutdown the server after serving the page
                    if ((req.HttpMethod == "POST") && (req.Url.AbsolutePath == "/shutdown")) {
                        runServer = false;
                        data = Encoding.ASCII.GetBytes("Rizor is now shutdown.");
                    }
                }


                // Write out to the response stream (asynchronously), then close it
                await resp.OutputStream.WriteAsync(data, 0, data.Length);

                resp.Close();
            }
        }


        public static void Main(string[] args)
        {
            // Create a Http server and start listening for incoming connections
            listener = new HttpListener();
            listener.Prefixes.Add(url);
            listener.Start();
            Console.WriteLine("Listening for connections on {0}", url);

            // Handle requests
            Task listenTask = HandleIncomingConnections();
            listenTask.GetAwaiter().GetResult();

            // Close the listener
            listener.Close();
        }
    }
}
