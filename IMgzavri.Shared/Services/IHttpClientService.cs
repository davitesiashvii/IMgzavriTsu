using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace IMgzavri.Shared.Services
{
    public interface IHttpClientService
    {
        Task<T> GetAsync<T>(string url);

        Task<T> PostAsync<T>(string url, HttpContent contentPost);

        Task<T> PutAsync<T>(string url, HttpContent contentPut);

        Task<T> DeleteAsync<T>(string url);
    }
}
