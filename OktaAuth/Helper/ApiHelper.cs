using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Net.Http.Json;
using System.Threading.Tasks;

namespace OktaModels.Services
{
    public class ApiHelper
    {
        private readonly string _baseUrl;
        private readonly HttpClient _client;
        private IDictionary<string, string> _defaultHeaders;
        private readonly IDictionary<string, string> _defaultQueryParams;

        public ApiHelper(string baseUrl, HttpClient client = null)
           : this(baseUrl, null, null)
        {
            _client = client;
        }
        public ApiHelper(string baseUrl, IDictionary<string, string> defaultHeaders = null, IDictionary<string, string> defaultQueryParams = null)
        {
            _baseUrl = baseUrl ?? string.Empty;
            _defaultHeaders = defaultHeaders;
            _defaultQueryParams = defaultQueryParams;
        }
        protected void SetDefaultHeaders(IDictionary<string, string> defaultHeaders)
        {
            _defaultHeaders = defaultHeaders;
        }

        public Task<TResponse> Get<TResponse>(string uri, IDictionary<string, string> queryParams = null)
        {
            return SendRequest<TResponse>(uri, HttpMethod.Get, queryParams, null);
        }

        public Task<TResponse> Post<TResponse>(string uri, object data, IDictionary<string, string> queryParams = null)
        {
            return SendRequest<TResponse>(uri, HttpMethod.Post, queryParams, data);
        }

        public Task<TResponse> Delete<TResponse>(string uri, IDictionary<string, string> queryParams = null)
        {
            return SendRequest<TResponse>(uri, HttpMethod.Delete, queryParams, null);
        }

        protected virtual Task<HttpResponseMessage> PostAsync(HttpClient client, string url, object data)
        {
            return client.PostAsJsonAsync(url, data);
        }

        protected virtual Task<HttpResponseMessage> GetAsync(HttpClient client, string url)
        {
            return client.GetAsync(url);
        }

        private async Task<TResponse> SendRequest<TResponse>(string uri, HttpMethod httpMethod, IDictionary<string, string> queryParams, object data)
        {
            try
            {
                var requestUrl = BuildUrl(uri, queryParams);
                HttpClient client;
                if (_client == null)
                {
                    client = _client ?? new HttpClient { BaseAddress = new Uri(requestUrl, UriKind.Absolute) };
                    client.DefaultRequestHeaders.Accept.Clear();
                    client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
                    client.Timeout = TimeSpan.FromMinutes(10);
                    if (_defaultHeaders != null)
                    {
                        foreach (var kv in _defaultHeaders)
                        {
                            client.DefaultRequestHeaders.Add(kv.Key, kv.Value);
                        }
                    }
                }
                else
                {
                    client = _client;
                }

                HttpResponseMessage response;

                if (httpMethod == HttpMethod.Get)
                {
                    response = await client.GetAsync(requestUrl).ConfigureAwait(false);
                }
                else if (httpMethod == HttpMethod.Post)
                {
                    response = await PostAsync(client, requestUrl, data).ConfigureAwait(false);
                }
                else if (httpMethod == HttpMethod.Delete)
                {
                    response = await client.DeleteAsync(requestUrl).ConfigureAwait(false);
                }
                else
                {
                    throw new NotSupportedException($"Method {httpMethod} is not supported.");
                }

                return await GetResponseDetail<TResponse>(response).ConfigureAwait(false);
            }
            catch (Exception ex)
            {
                throw;
            }
        }

        private string BuildUrl(string uri, IDictionary<string, string> queryParams = null)
        {
            Dictionary<string, string> allParams = _defaultQueryParams != null
                ? new Dictionary<string, string>(_defaultQueryParams)
                : new Dictionary<string, string>();
            if (queryParams != null)
            {
                foreach (var keyValuePair in queryParams)
                {
                    allParams.Add(keyValuePair.Key, keyValuePair.Value);
                }
            }

            // Remove trailing slash
            var baseUrl = _baseUrl.TrimEnd('/');
            // Remove starting slash
            uri = uri.TrimStart('/');

            if (allParams.Count > 0)
            {
                var queryString = string.Join("&", allParams.Select(kv => $"{kv.Key}={kv.Value}"));
                return $"{baseUrl}/{uri}?{queryString}";
            }
            return $"{baseUrl}/{uri}";
        }

        private async Task<T> GetResponseDetail<T>(HttpResponseMessage response)
        {
            var content = response.Content;
            var result = await content.ReadAsStringAsync().ConfigureAwait(false);

            if (response.StatusCode == HttpStatusCode.InternalServerError)
            {
                throw new ApiHelperException(response.StatusCode, result);
            }

            if (result is T res) return res;

            if (string.IsNullOrWhiteSpace(result))
            {
                return default;
            }

            else
            {
                if (!string.IsNullOrEmpty(result))
                {
                    return JsonConvert.DeserializeObject<T>(result);
                }
            }

            return default(T);
        }
    }

    public class ApiHelperException : Exception
    {
        public ApiHelperException() : this("Unknown Error")
        {
        }

        public ApiHelperException(HttpStatusCode statusCode, string rawResponse)
            : this($"HTTP ERROR {(int)statusCode}\n\n{rawResponse}")
        {
        }

        public ApiHelperException(string message) : base(message)
        {
        }

        public ApiHelperException(string message, Exception innerException) : base(message, innerException)
        {
        }
    }
}

