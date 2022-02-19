using Blazored.LocalStorage;
using Letti.ServiceProxies.Contracts;
using Letti.ServiceProxies.Service;
using Letti.ServiceProxies.Settings;
using Letti.Web.Contracts;
using Letti.Web.Service;
using Letti.Web.Providers;
using Microsoft.AspNetCore.Components.Authorization;
using Microsoft.AspNetCore.Components.WebAssembly.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Polly;
using System;
using System.Net;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Threading.Tasks;

namespace Letti.Web
{
    public class Program
    {
        public static async Task Main(string[] args)
        {
            var builder = WebAssemblyHostBuilder.CreateDefault(args);
            builder.RootComponents.Add<App>("app");
            IAsyncPolicy<HttpResponseMessage> retry = Policy.HandleResult<HttpResponseMessage>(r => !r.IsSuccessStatusCode).WaitAndRetryAsync(3, retries => TimeSpan.FromSeconds(Math.Pow(2, retries) / 2), (response, retries) =>
            {
                if (response.Result.StatusCode == HttpStatusCode.Unauthorized)
                {
                    var userService = builder.Build().Services.GetService<IUserService>();
                    userService.Refresh();
                }
            });
            builder.Services.AddBlazoredLocalStorage();
            builder.Services.AddOptions();

            builder.Services.AddAuthorizationCore();
            builder.Services.AddScoped<IUserService, UserService>();
            builder.Services.AddScoped<AuthenticationStateProvider, AuthStateProvider>();
            builder.Services.AddHttpClient("auth", client =>
            {
                client.BaseAddress = new Uri(builder.Configuration["SecuritySettings:ApiBaseAddress"]);
                client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
            });
            //string api = "http://localhost:55926/api/";
            //string api = "http://camilledata.cloudapp.net:8020/LettiAPI/api/";
            //builder.Services.AddHttpClient("api", client =>
            //{
            //    client.BaseAddress = new Uri(api);
            //    client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
            //});
            builder.Services.AddHttpClient("api", client =>
            {
                client.BaseAddress = new Uri(builder.Configuration["ApplicationSettings:ApiBaseAddress"]);
                client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
            }).AddPolicyHandler(retry);
            builder.Services.AddSingleton<SecuritySettings>(factory => builder.Configuration.GetSection("SecuritySettings") as SecuritySettings);

            builder.Services.AddSingleton<IOrganizationService, OrganizationService>(factory =>
            {
                var httpClientFactory = factory.GetService<IHttpClientFactory>();
                var httpClient = httpClientFactory.CreateClient("api");
                return new OrganizationService(httpClient);
            });
            builder.Services.AddSingleton<IPersonService, PersonService>(factory =>
            {
                var httpClientFactory = factory.GetService<IHttpClientFactory>();
                var httpClient = httpClientFactory.CreateClient("api");
                return new PersonService(httpClient);
            });
            builder.Services.AddSingleton<IContractorService, ContractorService>(factory =>
            {
                var httpClientFactory = factory.GetService<IHttpClientFactory>();
                var httpClient = httpClientFactory.CreateClient("api");
                return new ContractorService(httpClient);
            });
            builder.Services.AddSingleton<IContractsService, ContractsService>(factory =>
            {
                var httpClientFactory = factory.GetService<IHttpClientFactory>();
                var httpClient = httpClientFactory.CreateClient("api");
                return new ContractsService(httpClient);
            });
            //  builder.Services.AddScoped(sp => new HttpClient { BaseAddress = new Uri(builder.HostEnvironment.BaseAddress) });

            await builder.Build().RunAsync();
        }
    }
}
