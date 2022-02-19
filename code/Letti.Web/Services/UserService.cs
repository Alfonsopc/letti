using System;
using System.Net.Http;
using System.Net.Http.Json;
using System.Threading.Tasks;
using System.Collections.Generic;
using Letti.Web.Contracts;
using Blazored.LocalStorage;
using Letti.Web.Entities;

namespace Letti.Web.Service
{
    public class UserService : IUserService
    {
        private readonly IHttpClientFactory httpClientFactory;
        private readonly ILocalStorageService localStorageService;

       // private readonly SecuritySettings securitySettings;

        public event Action<object, Session> OnSessionChanged;

        public UserService(IHttpClientFactory httpClientFactory, ILocalStorageService localStorageService)
        {
            this.httpClientFactory = httpClientFactory;
            this.localStorageService = localStorageService;
           // this.securitySettings = securitySettings;
        }

        public Task<Session> GetSession() => localStorageService.GetItemAsync<Session>("session").AsTask(); 
        //{
        //   // 
        //    Session readSession = syncLocalStorageService.GetItem<Session>("session");
        //    if (readSession != null)
        //    {
        //        Console.WriteLine("sesion no nula en GetSession");
        //        Console.WriteLine(readSession.access_token);
        //    }
        //    else
        //    {
        //        Console.WriteLine("sesion nula");
        //    }
        //    return Task.FromResult<Session>(readSession);        
        //}
       
      

        public async Task<bool> Login(string username, string password)
        {
            var httpClient = httpClientFactory.CreateClient("auth");
            //var request = new
            //{
            //    username,
            //    password,
            //    securitySettings.ClientId,
            //    securitySettings.ClientSecret
            //};
            string ClientId = "580e59d0-323d-4854-81d5-edd557079feb";
            string ClientSecret = "ntuymxoUzWHgmDHaSaG4PdKQIfW90gYH2wkFl5rPasffmNIJ3Dc/kG54nO/eHyMcz1SygFDnTbbFU9rHJqNutw==";
            //var request = new
            //{
            //    username,
            //    password,
            //    ClientId,
            //    ClientSecret
            //};
            var parameters = new List<KeyValuePair<string, string>>();
            parameters.Add(new KeyValuePair<string, string>("grant_type", "password"));
            parameters.Add(new KeyValuePair<string, string>("client_id", ClientId));
            parameters.Add(new KeyValuePair<string, string>("client_secret", ClientSecret));
            parameters.Add(new KeyValuePair<string, string>("username", username));
            parameters.Add(new KeyValuePair<string, string>("password", password));

            var request = new HttpRequestMessage(HttpMethod.Post, "jwt") { Content = new FormUrlEncodedContent(parameters) };
           

            HttpResponseMessage response = await httpClient.SendAsync(request);
            var success = response.IsSuccessStatusCode;

            if (success)
            {
                try
                {
                    // var content = await response.Content.ReadAsStringAsync();                   
                    // Session session = new Session(content);
                    Session session = await response.Content.ReadFromJsonAsync<Session>();
                    DateTime foo = DateTime.UtcNow;
                    session.Expires= ((DateTimeOffset)foo).ToUnixTimeSeconds() + session.expires_in;
                    //ClearSession(session);
                    //Session session = new Session();
                    //var session = await response.Content.ReadFromJsonAsync<Session>();
                    await localStorageService.SetItemAsync("session", session);
                    
                    OnSessionChanged?.Invoke(this, session);
                }
                catch(Exception exp)
                {
                    string message = exp.Message;
                }
            }

            return success;
        }
        private void ClearSession(Session session)
        {
            string token1 = session.access_token;
            int index = token1.IndexOf('.');
            string token2 = token1.Substring(0, index) + token1.Substring(index + 1);
            index =token2.IndexOf('.');
            if(index>0)
            {
                token2 = token2.Substring(0, index); 
            }
            token2 += "==";
            session.access_token = token2;
        }

        public async Task Logout()
        {
            await localStorageService.ClearAsync().AsTask();
            OnSessionChanged?.Invoke(this, null);
        }

        public async Task<bool> Refresh()
        {
            var httpClient = httpClientFactory.CreateClient("auth");
            var localSession = await localStorageService.GetItemAsync<Session>("session");
            var request = new
            {
                localSession.access_token,
                localSession.refresh_token
            };

            var response = await httpClient.PostAsJsonAsync("security/sign-in/refresh", request);
            var success = response.IsSuccessStatusCode;

            if (success)
            {
                var session = await response.Content.ReadFromJsonAsync<Session>();
                await localStorageService.SetItemAsync("session", session);
                OnSessionChanged?.Invoke(this, session);
            }

            return success;
        }

        
    }
}
