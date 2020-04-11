using System;
using System.IO;
using System.Text;
using System.Net;
using System.Threading.Tasks;
using Controllers;


namespace Core
{
    class Router
    {
        public string FindRoute (ref HttpListenerRequest req, ref HttpListenerResponse resp) 
        {
            string data="";
            bool foundRoute = false;

            // HomeController
            if((req.HttpMethod == "GET") && (req.Url.AbsolutePath == "/")) {
                HomeController c = new HomeController();
                data = c.HomePage(ref req, ref resp);
                foundRoute = true;
            }

            // Version
            if((req.HttpMethod == "GET") && (req.Url.AbsolutePath == "/version")) {
                HomeController c = new HomeController();
                data = c.Version(ref req, ref resp);
                foundRoute = true;
            }

            // LoginController
            if((req.HttpMethod == "GET") && (req.Url.AbsolutePath == "/login")) {
                LoginController c = new LoginController();
                data = c.LoginPage(ref req, ref resp);
                foundRoute = true;
            }


            // API -- Auth -- Login
            if((req.HttpMethod == "POST") && (req.Url.AbsolutePath == "/auth")) {
                Auth c = new Auth();
                data = c.Login(ref req, ref resp);
                foundRoute = true;
            }

            // API -- Auth -- SetPassword
            if((req.HttpMethod == "POST") && (req.Url.AbsolutePath == "/set-password")) {
                Auth c = new Auth();
                data = c.SetPassword(ref req, ref resp);
                foundRoute = true;
            }




            // Error 404 - Route not found
            if (!foundRoute) {
                ErrorController c = new ErrorController();
                data = c.NotFound(ref req, ref resp);
            }

            // Return to main thread
            return data;
        }
    }
}

