using System.Net;
using System.Text.Json;
using System.Text.Json.Serialization;
using System.Collections.Generic;
using Core;

namespace Core
{
    class Respond
    {
        // Sends response in JSON
        // @param ref HttpListenerRequest req
        // @param ref HttpListenerResponse resp
        // @return string
        public static string JSON (ref HttpListenerResponse resp, Dictionary<string, string> response) 
        {
            // Set response document type to JSON
            resp.ContentType = "application/json";

            // Return JSON response
            return JsonSerializer.Serialize(response);
        }
    }
}

