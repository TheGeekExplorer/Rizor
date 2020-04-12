using System.IO;
using System.Net;
using System.Collections.Generic;


namespace Core
{
    class Query
    {
        // Memory cache store for the query string items
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
                    Data.Add(
                        WebUtility.UrlDecode(pairs2[0]), 
                        WebUtility.UrlDecode(pairs2[1])
                    );
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

            string aKey   = "";
            string aValue = "";

            // Run through the QueryString
            foreach (var key in req.QueryString)

                // Get the Key
                aKey = WebUtility.UrlDecode(
                    key.ToString()
                );

                // Get the Value for the key
                aValue = WebUtility.UrlDecode(
                    req.QueryString[aKey]
                );

                // Add to the dictionary
                Data.Add(aKey, aValue);

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

