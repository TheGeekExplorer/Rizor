using System.Net;
using Core;

namespace APIControllers
{
    class WeatherStats
    {

        // Gets the top level Weather Loaded stats, such as
        // how many total datapoints are loaded, how many sites,
        // how many sites for 3 Hr and 5 Day, etc.
        // @param ref HttpListenerRequest req
        // @param ref HttpListenerResponse resp
        // @return string
        public string GetTopLevel (ref HttpListenerRequest req, ref HttpListenerResponse resp) 
        {
            // Check session is active
            string username = Authentication.VerifySession(ref req, ref resp);
            if (username == null) {
                resp.StatusCode = 403;
                return "{\"status\":\"NOT AUTHORISED\"}";
            }
            
            // Set response document type to JSON
            resp.ContentType = "application/json";
            return "{\"stats\":[" +
                "\"452,997\",\"5,399\",\"5,397\",\"4,325\"" +
            "]}";
        }
    }
}

