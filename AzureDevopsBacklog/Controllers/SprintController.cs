using AzureDevopsBacklog.Application.Methods;
using AzureDevopsBacklog.Application.Models.RequestModels;
using AzureDevopsBacklog.Application.Models.ResponseModels;
using AzureDevopsBacklog.Contants;
using AzureDevopsBacklog.Contants.Enums;
using AzureDevopsBacklog.Infrastructure.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace AzureDevopsBacklog.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class SprintController : ControllerBase
    {
        private readonly IRestService _restService;
        private readonly IAzureApiSettings _azureApiSettings;

        public SprintController(IRestService restService, IAzureApiSettings azureApiSettings)
        {
            _restService = restService;
            _azureApiSettings = azureApiSettings;
        }

        [HttpPost]
        [Route("sprints/filter")]
        public async Task<IActionResult> GetWorkItemDetailsByFilter(GetSprintDetailByTagRequestModel request)
        {
            var routeUrl = RemoteUrls.GetWorkItemList(_azureApiSettings.BaseUrl);
            var filteredQuery = FilterMethods.GetSprintWorkItemsQuery(request);
            var requestModel = new GetWorkItemDetailRequestModel { Query = filteredQuery };
            var response = await _restService.PostApiResponseAsync<WorkItemListResponseModel>("GetWorkItemList", routeUrl, requestModel, HelperMethods.GetAuthorizationHeaderCollection(_azureApiSettings.Username, _azureApiSettings.Password));

            if (response.IsSucceeded)
            {
                var responseModel = new SprintDetailResponseModel();
                var workItemListCount = response.Data.WorkItems.Count();
                var controlledWorkItemCount = 0;
                var workItemMethods = new WorkItemMethods();
                while (workItemListCount > 0)
                {
                    var detailUrl = RemoteUrls.GetWorkItemDetailList(_azureApiSettings.BaseUrl, response.Data.WorkItems.Skip(controlledWorkItemCount).Take(200).Select(x => x.Id).ToList());
                    var workItemDetailResponse = await _restService.GetApiResponseAsync<WorkItemDetailResponseModel>("GetWorkItemDetail", detailUrl, HelperMethods.GetAuthorizationHeaderCollection(_azureApiSettings.Username, _azureApiSettings.Password));

                    workItemMethods.GetWorkItems(workItemDetailResponse, responseModel);

                    workItemListCount -= (int) ApiEnums.RowLimit;
                    controlledWorkItemCount += (int) ApiEnums.RowLimit;
                }
                responseModel.Count = responseModel.Users.Count();
                if (request.IsEnabledLowEffortMonitoring)
                {
                    workItemMethods.MarkLowEffortUsers(responseModel.Users, request.WeeklyMaxEffort);
                }
                return Ok(responseModel);
            }
            return NotFound(response);
        }
    }
}
