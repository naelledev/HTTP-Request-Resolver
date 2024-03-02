using System;
using System.Net.Http.Headers;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using Newtonsoft.Json;
using System.Net;
using System.Collections.Generic;
using System.Threading;
using System.Text.RegularExpressions;

internal class RequestResolver
{

    public static async Task BuildRequest(object data, string url, string Proxy = null)
    {
        WebClient webClient = new WebClient();
        HttpClient client = new HttpClient();
        try
        {
            client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
            string jsonString = JsonConvert.SerializeObject(data);
            StringContent content = new StringContent(jsonString, Encoding.UTF8, "application/json");
            HttpResponseMessage response = await client.PostAsync(url, (HttpContent)(object)content);
            if (Proxy != null)
            {
                webClient.Proxy = new WebProxy(Proxy);
            }
            try
            {
                string responseContent = await response.Content.ReadAsStringAsync();
                dynamic responseJson = JsonConvert.DeserializeObject<object>(responseContent);
                /// your response code

            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error reading response: {ex.Message}");
            }
        }
        finally
        {
            ((IDisposable)client)?.Dispose();
        }
    }
    public static async Task SendAuthorizationRequest(IEnumerable<KeyValuePair<string, string>> parameters, string url)
    {
        bool UseProxy = false; // true or false here! 
        using (HttpClient client = new HttpClient(new HttpClientHandler { Proxy = null, UseProxy = UseProxy}))
        {

            var content = new FormUrlEncodedContent(parameters);
            try
            {
                var response = await client.PostAsync(url, content);
                string responseContent = await response.Content.ReadAsStringAsync();
                dynamic responseJson = JsonConvert.DeserializeObject<object>(responseContent);
                // your reponse code, here is mine!
                if (responseJson.ToString().Contains("ERROR"))
                {
                    string msg = responseJson.error_description.Replace('_', ' ');
                    Console.WriteLine(msg);
                }
                else if (responseJson.ToString().Contains("access_token"))
                {
                    Console.WriteLine("Token : " + responseJson.refresh_token);
                }
            }
           
            catch (Exception ex)
            {
                Console.WriteLine($"An error occured: {ex}");
                Thread.Sleep(5000);
                
            }
            
        }
    }
}

