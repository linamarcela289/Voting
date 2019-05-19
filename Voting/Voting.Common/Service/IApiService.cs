using System.Threading.Tasks;
using Voting.Common.Model;
using Voting.Common.Models;

namespace Voting.Common.Service
{
    public interface IApiService
    {
        Task<Response> GetListAsync<T>(string urlBase, string servicePrefix, string controller);
        Task<Response> GetListAsync<T>(string urlBase, string servicePrefix, string controller, string tokenType, string accessToken);
        Task<Response> GetTokenAsync(string urlBase, string servicePrefix, string controller, TokenRequest request);
        Task<Response> PutAsync<T>(string urlBase, string servicePrefix, string controller, int id, T model, string tokenType, string accessToken);
        Task<Response> PutAsync<T>(string urlBase, string servicePrefix, string controller, T model, string tokenType, string accessToken);
        Task<Response> PostAsync<T>(string urlBase, string servicePrefix, string controller, T model, string tokenType, string accessToken);
        Task<Response> RegisterUserAsync(string urlBase, string servicePrefix, string controller, NewUserRequest newUserRequest);
    }
}