using System;
using System.Net.Http;
using System.Net.Http.Formatting;
using System.Text;
using System.Threading.Tasks;
using Juniper.Taxation.Core.Application.Interfaces;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using Newtonsoft.Json;
using Newtonsoft.Json.Serialization;

namespace Juniper.Infrastrutcure.ExternalCommunication.Service
{
    public class HttpClientAdapter:IHttpClientAdapter
    {
        private readonly IHttpClientFactory _httpFactory;
        private readonly ILogger<HttpClientAdapter> _logger;
        private readonly IHttpContextAccessor _httpContextAccessor;
        private readonly IOptions<ConsumerKeyProviderConfiguration> _providerConfigurations;
        private const string MediaType = "application/json";

        public JsonMediaTypeFormatter JsonFormatter { get; set; } = new JsonMediaTypeFormatter()
        {
            SerializerSettings =
            {
                ContractResolver = new CamelCasePropertyNamesContractResolver()
            }
        };

        public HttpClientAdapter(IHttpClientFactory httpClientFactory, ILogger<HttpClientAdapter> logger,IOptions<ConsumerKeyProviderConfiguration> options, IHttpContextAccessor httpContextAccessor)
        {
            _httpFactory = httpClientFactory;
            _logger = logger;
            _httpContextAccessor = httpContextAccessor;
            _providerConfigurations = options;
        }



        public async Task<T> PostAsync<T>(string clientName, string apiPath, object content)
        {
            var httpClient = _httpFactory.CreateClient(clientName);

            try
            {
                HttpResponseMessage response = null;
                // Create HTTP Client
                var httpclient = _httpFactory.CreateClient(clientName);

                // Exceute HTTP request message
                using (var request = GenerateHttpRequest(HttpMethod.Post, apiPath, content))
                {
                    response = await httpclient.SendAsync(request);

                    response.EnsureSuccessStatusCode();

                    var stringval = await response.Content.ReadAsStringAsync();
                    T value = await response.Content.ReadAsAsync<T>();
                    
                    return value;
                   
                }
            }
            catch (Exception ex)
            {
                _logger.LogError($"Http Post call failed with error {ex.Message} stacktrace:{ex.StackTrace}");
                throw;
            }

        }

        public async Task<T> GetAsync<T>(string clientName, string apiPath)
        {

            try
            {
                var httpClient = _httpFactory.CreateClient(clientName);
                HttpResponseMessage response = null;
                // Exceute HTTP request message
                using (var request = GenerateHttpRequest(HttpMethod.Get, apiPath))
                {
                    response = await httpClient.SendAsync(request);

                    response.EnsureSuccessStatusCode();
                    var stringval = await response.Content.ReadAsStringAsync();

                    T value = await response.Content.ReadAsAsync<T>();
                    return value;
                }

            }
            catch (Exception e)
            {
                _logger.LogError($"Http Get call failed with error {e.Message} stacktrace:{e.StackTrace}");
                throw;
            }
           
        }

        private HttpRequestMessage GenerateHttpRequest(HttpMethod httpMethod, string uri, object body = null)
        {
            // Verify is ConsumerKey is available in the Request Headers, 
            // If not, throw error
            if (!_httpContextAccessor.HttpContext.Request.Headers.TryGetValue("ProviderKey", out var consumerKey))
                throw new InvalidOperationException("Invalid Provider Key");

            // Fetch PartnerConfigurations (URL, SecretKey) to setup the http client
            var providerConfig = _providerConfigurations.Value.ProviderConfigurations.Find(x => x.ProviderKey.Equals(consumerKey, StringComparison.CurrentCultureIgnoreCase));

            // If PartnerConfigurations not found for a given consumer then return an error
            if (providerConfig == null)
                throw new InvalidOperationException("Provider configuration not found");
          
            var request = new HttpRequestMessage(httpMethod, new Uri($"{providerConfig.ProviderBaseEndPoint}{uri}"));

            var JsonFormatter = new JsonMediaTypeFormatter() { SerializerSettings = { ContractResolver = new CamelCasePropertyNamesContractResolver() } };

            //For PUT and POST Methods append the required body if available
            if (httpMethod == HttpMethod.Put || httpMethod == HttpMethod.Post)
            {
                var data = JsonConvert.SerializeObject(body, Formatting.None, JsonFormatter.SerializerSettings);
                request.Content = new StringContent(data, Encoding.UTF8, MediaType);
            }

            // Set bearer token from PartnerConfigurations
            if (providerConfig.IsTokenAuthRequired)
                request.Headers.Add("Authorization", "Bearer " + providerConfig.AuthToken);

            return request;
        }
    }
}