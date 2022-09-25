using AzureDevopsBacklog.Application.Methods;
using AzureDevopsBacklog.Application.Models.RequestModels;
using AzureDevopsBacklog.Application.Models.ResponseModels;
using AzureDevopsBacklog.Contants;
using AzureDevopsBacklog.Contants.Enums;
using AzureDevopsBacklog.Infrastructure.Interfaces;

namespace AzureDevopsBacklog.Infrastructure.Services
{
    public class SprintService : ISprintService
    {
        private IAzureApiSettings _azureApiSettings;
        private IRestService _restService;
        private IWorkItemService _workItemService;

        public SprintService(IAzureApiSettings azureApiSettings, IRestService restService, IWorkItemService workItemService)
        {
            _azureApiSettings = azureApiSettings;
            _restService = restService;
            _workItemService = workItemService;
        }
        public async Task<BaseResponseModel<SprintDetailResponseModel>> GetSprintDetailAsync(GetSprintDetailByTagRequestModel request)
        {
            var filteredQuery = FilterMethods.GetSprintWorkItemsQuery(request);
            var response = await _workItemService.GetWorkItemsByQuery(new() { Query = filteredQuery});
            if (!response.IsSucceeded)
                return new() { IsSucceeded = false, ResponseMessage = response.ResponseMessage };

            var workItemMethods = new WorkItemMethods();

            var responseModel = new SprintDetailResponseModel() { WorkItemCount = response.Data.WorkItems.Count() };

            var workItemListCount = responseModel.WorkItemCount;
            var controlledWorkItemCount = 0;
            while (workItemListCount > 0)
            {
                var workItemDetailResponse = await workItemMethods.GetWorkItemDetailsAsync(_azureApiSettings, _restService, response.Data.WorkItems, controlledWorkItemCount);

                if (!workItemDetailResponse.IsSucceeded)
                    return new() { IsSucceeded = false, ResponseMessage = workItemDetailResponse.ResponseMessage };
                workItemMethods.GetWorkItems(workItemDetailResponse, responseModel);

                workItemListCount -= (int)ApiEnums.RowLimit;
                controlledWorkItemCount += (int)ApiEnums.RowLimit;
            }
            responseModel.UserCount = responseModel.Users.Count();
            if (request.IsEnabledLowEffortMonitoring)
            {
                workItemMethods.MarkLowEffortUsers(responseModel, request.WeeklyMaxEffort);
            }
            return new() { Data = responseModel, IsSucceeded = true, ResponseMessage = SuccessMessages.RequestSuccessful };
        }
    }
}
