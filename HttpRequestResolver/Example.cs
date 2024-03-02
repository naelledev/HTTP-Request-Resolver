using System;
using System.Collections.Generic;

namespace HttpRequestResolver
{
    internal class Example
    {
        static void Main(string[] args)
        {
            Menu();
        }
        public static void Menu()
        {
            Console.Clear();
            Console.Write("Nickname : ");
            string Name = Console.ReadLine();
            Console.Write("Password : ");
            string Password = Console.ReadLine();
            Console.Write("Server : ");
            string Server = Console.ReadLine();

            Utils.serverHelper(Server);

            string url = "https://eu-secure.mspapis.com/loginidentity/connect/token";

            var parameters = new Dictionary<string, string>
            {
                { "client_id", "unity.client" },
                { "client_secret", "secret" },
                { "grant_type", "password" },
                { "scope", "openid nebula offline_access" },
                { "username", Server.ToUpper() + "|" + Name },
                { "password", Password },
                { "acr_values", "gameId:j68d" }
            };

            RequestResolver.SendAuthorizationRequest(parameters, url);
            Console.ReadLine();
        }
    }
}
