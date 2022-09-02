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

            var response = new BaseResponseModel<TModel>();
            HttpResponseMessage httpResponseMessage = await httpClient.GetAsync(routeUrl);
            if (!httpResponseMessage.IsSuccessStatusCode)
            {
                response.ResponseMessage = ExceptionMessages.RequestFailed;
                return response;
            }

            var content = await httpResponseMessage.Content.ReadAsStringAsync();
            var data = JsonSerializer.Deserialize<TModel>(content);
            return FillResponseModel<TModel>(response, data);
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

            var response = new BaseResponseModel<TModel>();
            HttpResponseMessage httpResponseMessage = await httpClient.PostAsJsonAsync(routeUrl, requestModel);
            if (!httpResponseMessage.IsSuccessStatusCode)
            {
                response.ResponseMessage = ExceptionMessages.RequestFailed;
                return response;
            }

            var content = await httpResponseMessage.Content.ReadAsStringAsync();
            var data = JsonSerializer.Deserialize<TModel>(content);
            return FillResponseModel<TModel>(response, data);
        }

        private static void ValidateClientName(string clientName)
        {
            if (string.IsNullOrWhiteSpace(clientName))
                throw new ArgumentNullException(nameof(clientName), ExceptionMessages.ClientNameRequired);
        }

        private static BaseResponseModel<TModel> FillResponseModel<TModel>(BaseResponseModel<TModel> response, TModel data)
        {
            response.Data = data;
            response.IsSucceeded = true;
            response.ResponseMessage = SuccessMessages.RequestSuccessful;
            return response;
        }
    }
}
