using IdentityModel.Client;
using Newtonsoft.Json.Linq;
using System;
using System.Net.Http;
using System.Threading.Tasks;


namespace Client
{
    class Program
    {
        static void Main(string[] args) => MainAsync().GetAwaiter().GetResult();

        private static async Task MainAsync()
        {
            var identityServer = await DiscoveryClient.GetAsync("http://localhost:4343"); //discover the IdentityServer
            if (identityServer.IsError)
            {
                Console.Write(identityServer.Error);
                return;
            }

            //Get the token2
            var tokenClient = new TokenClient(identityServer.TokenEndpoint, "Client1", "secret");
            //var tokenResponse = await tokenClient.RequestClientCredentialsAsync("api1");
            var tokenResponse = await tokenClient.RequestResourceOwnerPasswordAsync("admin", "password");
            //    var refreshResponse = await tokenClient.RequestRefreshTokenAsync(new RefreshTokenRequest(identityServer.TokenEndpoint, "Client1", "secret",tokenResponse.RefreshToken));
            var refreshResponse = await tokenClient.RequestRefreshTokenAsync(tokenResponse.RefreshToken);
            //    new RefreshTokenRequest
            //{
            //    Address = identityServer.TokenEndpoint,

            //    ClientId = "client",
            //    ClientSecret = "secret",

            //    RefreshToken = "xyz"
            //});
            //Call the API

            HttpClient client = new HttpClient();
            client.SetBearerToken(tokenResponse.AccessToken);
            //  client.SetBearerToken(tokenResponse.AccessToken);

            var response = await client.GetAsync("http://localhost:52037/api/values");
            var content = await response.Content.ReadAsStringAsync();
            Console.WriteLine(JArray.Parse(content));
            client = new HttpClient();
            client.SetBearerToken(refreshResponse.AccessToken);
            var response1 = await client.GetAsync("http://localhost:52037/api/values/5");
            var content1 = await response1.Content.ReadAsStringAsync();
            Console.WriteLine(content1);
            Console.ReadKey();
        }
    }
}
