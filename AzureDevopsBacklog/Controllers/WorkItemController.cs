using AzureDevopsBacklog.Application.Methods;
using AzureDevopsBacklog.Application.Models;
using AzureDevopsBacklog.Application.Models.RequestModels;
using AzureDevopsBacklog.Contants;
using AzureDevopsBacklog.Infrastructure.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace AzureDevopsBacklog.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class WorkItemController : ControllerBase
    {
        private readonly IRestService _restService;
        private readonly IAzureApiSettings _azureApiSettings;

        public WorkItemController(IRestService restService, IAzureApiSettings azureApiSettings)
        {
            _restService = restService;
            _azureApiSettings = azureApiSettings;
        }

        [HttpGet]
        [Route("details")]
        public async Task<IActionResult> GetWorkItemDetail(int id)
        {
            var routeUrl = RemoteUrls.GetWorkItemDetailSingle(_azureApiSettings.BaseUrl, id);

            var response = await _restService.GetApiResponseAsync<WorkItemDetailResponseModel>("GetWorkItemDetail", routeUrl, HelperMethods.GetAuthorizationHeaderCollection(_azureApiSettings.Username, _azureApiSettings.Password));

            if (response.IsSucceeded)
                return Ok(response);
            return NotFound(response);
        }

        [HttpPost]
        [Route("workItems")]
        public async Task<IActionResult> GetWorkItemDetail(GetWorkItemDetailRequestModel request)
        {
            var routeUrl = RemoteUrls.GetWorkItemList(_azureApiSettings.BaseUrl);

            var response = await _restService.PostApiResponseAsync<WorkItemListModel>("GetWorkItemList", routeUrl, request, HelperMethods.GetAuthorizationHeaderCollection(_azureApiSettings.Username, _azureApiSettings.Password));

            if (response.IsSucceeded)
                return Ok(response);
            return NotFound(response);
        }

        [HttpPost]
        [Route("workItems/filter")]
        public async Task<IActionResult> GetWorkItemDetailsByFilter(GetWorkItemDetailByFilterRequestModel request)
        {
            var routeUrl = RemoteUrls.GetWorkItemList(_azureApiSettings.BaseUrl);
            var filteredQuery = FilterMethods.GetFilteredQuery(request);
            var requestModel = new GetWorkItemDetailRequestModel { Query = filteredQuery };
            var response = await _restService.PostApiResponseAsync<WorkItemListModel>("GetWorkItemList", routeUrl, requestModel, HelperMethods.GetAuthorizationHeaderCollection(_azureApiSettings.Username, _azureApiSettings.Password));

            if (response.IsSucceeded)
            {
                var detailUrl = RemoteUrls.GetWorkItemDetailList(_azureApiSettings.BaseUrl, response.Data.WorkItems.Select(x => x.Id).ToList());
                var workItemDetailResponse = await _restService.GetApiResponseAsync<WorkItemDetailResponseModel>("GetWorkItemDetail", detailUrl, HelperMethods.GetAuthorizationHeaderCollection(_azureApiSettings.Username, _azureApiSettings.Password));
                if (workItemDetailResponse.IsSucceeded)
                    return Ok(workItemDetailResponse);
                return NotFound(workItemDetailResponse);
            }
            return NotFound(response);
        }
    }
}
