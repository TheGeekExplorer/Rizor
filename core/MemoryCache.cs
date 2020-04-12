using System.Collections.Generic;
using Core;

namespace MemoryCache
{
    class Universe
    {

        // Main cache object in memory
        // Key   == string
        // Value == string
        private static Dictionary<string, string> Cache = new Dictionary<string, string>();


        // ADDS KEY AND VALUE, AND IF EXIST THEN UPDATES
        // @param string key
        // @param string value
        // @return bool
        public static bool Add (string key, string value) 
        {
            if (Cache.ContainsKey(key)) {
                Cache[key] = value;
            } else {
                Cache.Add(key, value);
            }

            // Send to browser
            return true;
        } 

        
        // RETRIEVES A VALUE FOR A KEY
        // @param string key
        // @return bool
        public static string Get (string key) 
        {
            if (Cache.ContainsKey(key))
                return Cache[key];
            return null;
        } 


        // DELETES A GIVEN KEY
        // @param string key
        // @return bool
        public static bool Delete (string key) 
        {
            if (Cache.ContainsKey(key))
                Cache.Remove(key);
            
            return true;
        } 

    }
}

