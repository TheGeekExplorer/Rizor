using System;
using System.IO;
using System.Net;
using System.Collections.Generic;


namespace Core
{
    class Query
    {
        public static Dictionary<string, string> Data = new Dictionary<string, string>(); 


        // Main method to get query data into a Dictionary
        // @param ref HttpListenerRequest req
        // @return Dictionary<string, string>
        public static Dictionary<string, string> POST (HttpListenerRequest req) 
        {
            var stream = req.InputStream;
            string[] pairs;
            string[] pairs2;

            // Clear down the dictionary
            Data.Clear();

            // Run through key/value pairs, and put into array (key=value)
            using (var reader = new StreamReader(stream))
                pairs = reader.ReadToEnd().Split('&');

            // Run through pairs, and put into array (key, value), then
            // put into Dictionary
            foreach (var pair in pairs) {
                pairs2 = pair.Split('=');
                
                // Check that array not empty
                if (pairs2.Length > 1)
                    Data.Add(pairs2[0], pairs2[1]);
            }

            // Return data
            return Data;
        }




        
        // Main method to get query data into a Dictionary
        // @param ref HttpListenerRequest req
        // @return Dictionary<string, string>
        public static Dictionary<string, string> GET (HttpListenerRequest req) 
        {
            // Clear down the dictionary
            Data.Clear();

            // Run through the QueryString
            foreach (var key in req.QueryString)
                Data.Add(key.ToString(), req.QueryString[key.ToString()]);

            // Return data
            return Data;
        }


        // Decides which type of request method it is, and then
        // passes to the correct method to grab the query data
        // and put it into a Dictionary in the state
        public static Dictionary<string, string> GetQuery (HttpListenerRequest req)
        {
            if (req.HttpMethod == "GET") {
                return GET (req);
            } else {
                return POST (req);
            }
        }

    }
}

