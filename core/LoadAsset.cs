using System;
using System.IO;
using System.Text;
using System.Net;
using System.Threading.Tasks;
using Controllers;


namespace Core
{
    class LoadAsset
    {

        // Loads and returns the binary for an asset, such as a JPG
        // @param string url
        // @return binary bin
        public byte[] LoadBinary (string url) 
        {
            byte[] bin;
            string path = "D:/Development/C#/Rizor" + url;

            try {
                // Read all bytes from file
                bin = File.ReadAllBytes(path);
            } catch {
                bin = null;
            }

            // Return file
            return bin;
        }


        // Loads and returns the text for an asset, such as a CSS file
        // @param string url
        // @return binary text
        public byte[] LoadText (string url) 
        {
            byte[] text;
            string path = "D:/Development/C#/Rizor" + url;

            try {
                // Read all bytes from file
                text = File.ReadAllBytes(path);
            } catch {
                text = null;
            }

            // Return file
            return text;
        }
    }
}

