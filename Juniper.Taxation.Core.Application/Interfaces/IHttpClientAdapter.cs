using System.Net.Http;
using System.Threading.Tasks;

namespace Juniper.Taxation.Core.Application.Interfaces
{
    public interface IHttpClientAdapter
    {
        Task<T> PostAsync<T>(string clientName, string apiPath, object content);
        Task<T> GetAsync<T>(string clientName, string apiPath);
    }
}