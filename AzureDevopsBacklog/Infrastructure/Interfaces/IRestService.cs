using AzureDevopsBacklog.Application.Models.ResponseModels;
using System.Collections.Specialized;

namespace AzureDevopsBacklog.Infrastructure.Interfaces
{
    public interface IRestService
    {
        Task<BaseResponseModel<TModel>> GetApiResponseAsync<TModel>(string clientName, string routeUrl, NameValueCollection? headersCollection = null) where TModel : class, new();
        Task<BaseResponseModel<TModel>> PostApiResponseAsync<TModel>(string clientName, string routeUrl, object parameters, NameValueCollection? headersCollection = null) where TModel : class, new();
    }
}
