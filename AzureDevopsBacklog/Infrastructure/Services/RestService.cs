using System.Text.Json;
using AzureDevopsBacklog.Application.Models.ResponseModels;
using AzureDevopsBacklog.Contants;
using System.Collections.Specialized;
using AzureDevopsBacklog.Infrastructure.Interfaces;

namespace AzureDevopsBacklog.Infrastructure.Services
{
    public sealed class RestService : IRestService
    {
        private readonly IHttpClientFactory _httpClientFactory;

        public RestService(IHttpClientFactory httpClientFactory)
        {
            _httpClientFactory = httpClientFactory;
        }

        public async Task<BaseResponseModel<TModel>> GetApiResponseAsync<TModel>(string clientName, string routeUrl, NameValueCollection? headersCollection = null) where TModel : class, new()
        {
            ValidateClientName(clientName);

            HttpClient httpClient = _httpClientFactory.CreateClient(clientName);
            ArgumentNullException.ThrowIfNull(httpClient, nameof(httpClient));

            if (headersCollection is not null)
            {
                foreach (var key in headersCollection.AllKeys)
                {
                    httpClient.DefaultRequestHeaders.TryAddWithoutValidation(key, headersCollection[key]);
                }
            }

            HttpResponseMessage httpResponseMessage = await httpClient.GetAsync(routeUrl);
            if (!httpResponseMessage.IsSuccessStatusCode)
            {
                return new BaseResponseModel<TModel>() { IsSucceeded = false, ResponseMessage = ExceptionMessages.RequestFailed };
            }

            var content = await httpResponseMessage.Content.ReadAsStringAsync();
            var data = JsonSerializer.Deserialize<TModel>(content);
            return new BaseResponseModel<TModel>() { Data = data, IsSucceeded = true, ResponseMessage = SuccessMessages.RequestSuccessful };
        }

        public async Task<BaseResponseModel<TModel>> PostApiResponseAsync<TModel>(string clientName, string routeUrl, object requestModel, NameValueCollection? headersCollection = null) where TModel : class, new()
        {
            ValidateClientName(clientName);

            HttpClient httpClient = _httpClientFactory.CreateClient(clientName);
            ArgumentNullException.ThrowIfNull(httpClient, nameof(httpClient));

            if (headersCollection is not null)
            {
                foreach (var key in headersCollection.AllKeys)
                {
                    httpClient.DefaultRequestHeaders.TryAddWithoutValidation(key, headersCollection[key]);
                }
            }

            HttpResponseMessage httpResponseMessage = await httpClient.PostAsJsonAsync(routeUrl, requestModel);
            if (!httpResponseMessage.IsSuccessStatusCode)
            {
                return new BaseResponseModel<TModel>() { IsSucceeded = false, ResponseMessage = ExceptionMessages.RequestFailed };
            }

            var content = await httpResponseMessage.Content.ReadAsStringAsync();
            var data = JsonSerializer.Deserialize<TModel>(content);
            return new BaseResponseModel<TModel>() { Data = data, IsSucceeded = true, ResponseMessage = SuccessMessages.RequestSuccessful };
        }

        private static void ValidateClientName(string clientName)
        {
            if (string.IsNullOrWhiteSpace(clientName))
                throw new ArgumentNullException(nameof(clientName), ExceptionMessages.ClientNameRequired);
        }
    }
}
