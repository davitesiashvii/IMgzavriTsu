using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;

namespace IMgzavri.Shared.Services
{
    public class HttpClientService : IHttpClientService
    {
        private readonly IHttpClientFactory _httpClientFactory;
        private readonly string _clientName;
        private HttpClient _client;

        public HttpClientService(IHttpClientFactory httpClientFactory, string clientName)
        {
            _httpClientFactory = httpClientFactory;
            _clientName = clientName;
        }

        public async Task<T> GetAsync<T>(string url)
        {
            T data;
            _client = _httpClientFactory.CreateClient(_clientName);

            try
            {
                using (HttpResponseMessage response = await _client.GetAsync(url))
                using (HttpContent content = response.Content)
                {
                    string d = await content.ReadAsStringAsync();
                    if (d != null)
                    {
                        data = JsonConvert.DeserializeObject<T>(d);
                        return (T)data;
                    }
                }
            }
            catch (Exception ex)
            {
                throw;
            }

            Object o = new Object();
            return (T)o;
        }

        public async Task<T> PostAsync<T>(string url, HttpContent contentPost)
        {
            T data;
            _client = _httpClientFactory.CreateClient(_clientName);

            try
            {
                using (HttpResponseMessage response = await _client.PostAsync(url, contentPost))
                using (HttpContent content = response.Content)
                {
                    string d = await content.ReadAsStringAsync();
                    if (d != null)
                    {
                        data = JsonConvert.DeserializeObject<T>(d);
                        return (T)data;
                    }
                }
            }
            catch (Exception e)
            {
                throw;
            }

            Object o = new Object();
            return (T)o;
        }

        public async Task<T> PutAsync<T>(string url, HttpContent contentPut)
        {
            T data;
            _client = _httpClientFactory.CreateClient(_clientName);

            try
            {
                using (HttpResponseMessage response = await _client.PutAsync(url, contentPut))
                using (HttpContent content = response.Content)
                {
                    string d = await content.ReadAsStringAsync();
                    if (d != null)
                    {
                        data = JsonConvert.DeserializeObject<T>(d);
                        return (T)data;
                    }
                }
            }
            catch (Exception e)
            {
                throw;
            }

            Object o = new Object();
            return (T)o;
        }

        public async Task<T> DeleteAsync<T>(string url)
        {
            T newT;
            _client = _httpClientFactory.CreateClient(_clientName);

            try
            {
                using (HttpResponseMessage response = await _client.DeleteAsync(url))
                using (HttpContent content = response.Content)
                {
                    string data = await content.ReadAsStringAsync();
                    if (data != null)
                    {
                        newT = JsonConvert.DeserializeObject<T>(data);
                        return newT;
                    }
                }
            }
            catch (Exception e)
            {
                throw;
            }

            Object o = new Object();
            return (T)o;
        }
    }
}
