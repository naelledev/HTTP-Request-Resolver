using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using HttpRequestResolver;

    internal class Utils
    {
        public static string serverHelper(string server)
        {
            string text = "";
            text = ((!(server.ToLower() == "uk")) ? server : "gb");
            if (server.ToLower() == "uk")
            {
                server = "gb";
            }
            WebClient webClient = new WebClient();
            webClient.Proxy = null;
            try
            {
                webClient.DownloadData("https://disco.mspapis.com/disco/v1/services/msp/" + server.ToLower());
                return text.ToUpper();
            }
            catch (Exception ex)
            {
                if (ex.ToString().Contains("404"))
                {
                    Console.WriteLine("ERROR 404 : THIS SERVER DOESN'T EXIST!");
                    Thread.Sleep(1500);
                    Example.Menu();
                }
                else
                {
                    Console.WriteLine("UNKNOWN ERROR : " + ex);
                    Thread.Sleep(1500);
                    Example.Menu();
                }
            }
            return null;
        }
    }
