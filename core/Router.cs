using System.Net;
using Controllers;
using APIControllers;

namespace Core
{
    class Router
    {

        // Main Router for the Server.  This method takes the request and determines
        // which Controller the request should be invoked for.
        // @param ref HttpListenerRequest req
        // @param ref HttpListenerResponse resp
        // @return string
        public string FindRoute (ref HttpListenerRequest req, ref HttpListenerResponse resp) 
        {

            // MAIN SITE ROUTES
            // ===============================================================================

            // HomeController
            if((req.HttpMethod == "GET") && (req.Url.AbsolutePath == "/"))
                return (new HomeController()).HomePage(ref req, ref resp);
            

            // LoginController
            if((req.HttpMethod == "GET") && (req.Url.AbsolutePath == "/login/"))
                return (new LoginController()).LoginPage(ref req, ref resp);


            // Control Panel
            if((req.HttpMethod == "GET") && (req.Url.AbsolutePath == "/auth/portal/"))
                return (new ControlPanelController()).Index(ref req, ref resp);


            // Control Panel
            if((req.HttpMethod == "GET") && (req.Url.AbsolutePath == "/auth/portal/actions/"))
                return (new RizorActionsController()).Index(ref req, ref resp);



            // API ROUTES
            // ===============================================================================

            // Auth -- Login
            if((req.HttpMethod == "POST") && (req.Url.AbsolutePath == "/api/auth/login"))
                return (new Auth()).Login(ref req, ref resp);
            

            // Auth -- Logout
            if((req.HttpMethod == "POST") && (req.Url.AbsolutePath == "/api/auth/logout"))
                return (new Auth()).Logout(ref req, ref resp);


            // Auth -- SetPassword
            if((req.HttpMethod == "POST") && (req.Url.AbsolutePath == "/api/set-password"))
                return (new Auth()).CreateUser(ref req, ref resp);


            // WEATHER -- GetTopLevel
            if((req.HttpMethod == "POST") && (req.Url.AbsolutePath == "/api/weather/get-top-level"))
                return (new WeatherStats()).GetTopLevel(ref req, ref resp);




            // Error 404 - Route not found
            return (new ErrorController()).NotFound(ref req, ref resp);
        }
    }
}

