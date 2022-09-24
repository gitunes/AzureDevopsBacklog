using AzureDevopsBacklog.Application.Methods;
using AzureDevopsBacklog.Application.Models.RequestModels;
using AzureDevopsBacklog.Application.Models.ResponseModels;
using AzureDevopsBacklog.Contants;
using AzureDevopsBacklog.Contants.Enums;
using AzureDevopsBacklog.Infrastructure.Interfaces;

namespace AzureDevopsBacklog.Infrastructure.Services
{
    public class WorkItemService : IWorkItemService
    {
        private readonly IAzureApiSettings _azureApiSettings;
        private readonly IRestService _restService;

        public WorkItemService(IAzureApiSettings azureApiSettings, IRestService restService)
        {
            _azureApiSettings = azureApiSettings;
            _restService = restService;
        }

        public async Task<BaseResponseModel<WorkItemDetailResponseModel>> GetWorkItemDetail(int id)
        {
            var routeUrl = RemoteUrls.GetWorkItemDetailSingle(_azureApiSettings.BaseUrl, id);
            var response = await _restService.GetApiResponseAsync<WorkItemDetailResponseModel>(nameof(GetWorkItemDetail), routeUrl, HelperMethods.GetAuthorizationHeaderCollection(_azureApiSettings.Username, _azureApiSettings.Password));
            return response;
        }

        public async Task<BaseResponseModel<WorkItemListResponseModel>> GetWorkItemsByQuery(GetWorkItemDetailRequestModel model)
        {
            var routeUrl = RemoteUrls.GetWorkItemList(_azureApiSettings.BaseUrl);
            var response = await _restService.PostApiResponseAsync<WorkItemListResponseModel>(nameof(GetWorkItemsByQuery), routeUrl, model, HelperMethods.GetAuthorizationHeaderCollection(_azureApiSettings.Username, _azureApiSettings.Password));
            return response;
        }

        public async Task<BaseResponseModel<WorkItemDetailResponseModel>> GetWorkItemDetailsByFilter(GetWorkItemDetailByFilterRequestModel model)
        {
            var filteredQuery = FilterMethods.GetFilteredQuery(model);
            var requestModel = new GetWorkItemDetailRequestModel { Query = filteredQuery };

            var responseForWorkItemList = await GetWorkItemsByQuery(requestModel);

            if (!responseForWorkItemList.IsSucceeded)
                return new SuccessResponseModel<WorkItemDetailResponseModel>() { ResponseMessage = responseForWorkItemList.ResponseMessage };

            var workItemListCount = responseForWorkItemList.Data.WorkItems.Count();
            var controlledWorkItemCount = 0;
            var responseModel = new SuccessResponseModel<WorkItemDetailResponseModel>() { Data = new WorkItemDetailResponseModel() };
            var workItemMethods = new WorkItemMethods();
            while (workItemListCount > 0)
            {
                var workItemDetailResponse = await workItemMethods.GetWorkItemDetailsAsync(_azureApiSettings, _restService, responseForWorkItemList.Data.WorkItems, controlledWorkItemCount);

                if (!workItemDetailResponse.IsSucceeded)
                {
                    responseModel.ResponseMessage = workItemDetailResponse.ResponseMessage;
                    return responseModel;
                }                 
                responseModel.Data.WorkItemDetails.AddRange(workItemDetailResponse.Data.WorkItemDetails);
                workItemListCount -= (int)ApiEnums.RowLimit;
                controlledWorkItemCount += (int)ApiEnums.RowLimit;
            }
            responseModel.Data.Count = responseModel.Data.WorkItemDetails.Count();
            return responseModel;
        }
    }
}
