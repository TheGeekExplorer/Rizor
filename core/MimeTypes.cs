namespace Core
{
    class MimeTypes
    {

        // Determines and returns the correct MimeType header for the browser
        // @param string url
        // @return string
        public string[] Determine (string url) 
        {
            if (url.EndsWith(".jpg"))
                return new string[]{"binary","image/jpeg"};

            if (url.EndsWith(".png"))
                return new string[]{"binary","image/png"};
            
            if (url.EndsWith(".gif"))
                return new string[]{"binary","image/gif"};
            
            if (url.EndsWith(".zip"))
                return new string[]{"binary","application/zip"};

            if (url.EndsWith(".html"))
                return new string[]{"text","text/html"};

            if (url.EndsWith(".txt"))
                return new string[]{"text","text/plain"};

            if (url.EndsWith(".js"))
                return new string[]{"text","application/javascript"};

            if (url.EndsWith(".css"))
                return new string[]{"text","text/css"};

            if (url.EndsWith(".woff"))
                return new string[]{"binary","font/woff"};

            if (url.EndsWith(".woff2"))
                return new string[]{"binary","font/woff2"};

            // Shutdown command
            if (url.EndsWith("/shutdown"))
                return new string[]{"command", "text/html"};

            // Everything else is a controller
            return new string[]{"controller","text/html"};
        }
    }
}

