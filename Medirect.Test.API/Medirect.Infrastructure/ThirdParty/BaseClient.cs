using Microsoft.Extensions.Logging;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Medirect.Infrastructure.ThirdParty
{
    internal class BaseClient
    {
        private readonly HttpClient _httpClient;
        private readonly ILogger<BaseClient> _logger;

        private string BaseUrl = string.Empty;
        private string ApiKey = string.Empty;

        public BaseClient(HttpClient httpClient, ILogger<BaseClient> logger)
        {
            _httpClient = httpClient;
            _logger = logger;
        }

        public JsonSerializerSettings JsonSerializerSettings { get; set; }

        internal async Task<HttpRequestMessage> PrepareRequest<TReq>(TReq body, HttpRequestMessage request)
        {
            var content = new StringContent(JsonConvert.SerializeObject(body));
            //content.Headers.ContentType = System.Net.Http.Headers.MediaTypeHeaderValue.Parse("application/json");
            //request.Content = content;
            //request.Headers.Accept.Add(System.Net.Http.Headers.MediaTypeWithQualityHeaderValue.Parse("application/json"));
            request.Headers.Add("apikey", ApiKey);
            return request;
        }

        internal async Task<TResp> ProcessResponse<TResp>(HttpResponseMessage response)
        {
            try
            {
                if (response.StatusCode != System.Net.HttpStatusCode.OK)
                {
                    return default;
                }

                var headers_ = System.Linq.Enumerable.ToDictionary(response.Headers, h_ => h_.Key, h_ => h_.Value);
                if (response.Content != null && response.Content.Headers != null)
                {
                    foreach (var item_ in response.Content.Headers)
                    {
                        headers_[item_.Key] = item_.Value;
                    }
                }

                return await ReadObjectResponseAsync<TResp>(response, headers_);
            }
            finally
            {
                if (response != null)
                {
                    response.Dispose();
                }
            }
        }

        protected virtual async Task<T> ReadObjectResponseAsync<T>(System.Net.Http.HttpResponseMessage response, System.Collections.Generic.IReadOnlyDictionary<string, System.Collections.Generic.IEnumerable<string>> headers)
        {
            if (response == null || response.Content == null)
            {
                return default;
            }

            var responseText = await response.Content.ReadAsStringAsync();
            try
            {
                var typedBody = Newtonsoft.Json.JsonConvert.DeserializeObject<T>(responseText, JsonSerializerSettings);

                return typedBody;
            }
            catch (Newtonsoft.Json.JsonException exception)
            {
                var message = "Could not deserialize the response body string as " + typeof(T).FullName + ".";
                return default;
            }
        }

        public void SetApiKey(string apiKey)
        {
            this.ApiKey = apiKey;
        }

        public async Task<TResp> SendRequest<TReq, TResp>(TReq body, string pathUrl, VerbType verbAction, CancellationToken cancellationToken = default(CancellationToken))
        {
            var urlBuilder = new System.Text.StringBuilder();
            urlBuilder.Append(pathUrl);

            var client = _httpClient;
            try
            {
                using (var request = new System.Net.Http.HttpRequestMessage())
                {
                    var req = await PrepareRequest(body, request);

                    req.Method = new System.Net.Http.HttpMethod(verbAction.ToString());
                    req.RequestUri = new System.Uri(urlBuilder.ToString(), System.UriKind.RelativeOrAbsolute);
                    var response = await client.SendAsync(req, HttpCompletionOption.ResponseHeadersRead, cancellationToken);

                    var resp = await ProcessResponse<TResp>(response);
                    return resp;
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
                _logger.LogError("Error in procesing request {ex}", ex.Message);
                return default;
            }
        }
    }

    public class CachedToken
    {
        public string Token { get; set; }

        public string TokenType { get; set; }

        public DateTime ExpireDate { get; set; }
    }

    public enum VerbType
    {
        POST,
        GET,
        PUT,
        DELETE
    }
}