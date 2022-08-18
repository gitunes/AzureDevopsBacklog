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
            var routeUrl = RemoteUrl.GetWorkItemDetail(_azureApiSettings.BaseUrl, id);

            var response = await _restService.GetApiResponseAsync<WorkItemDetailResponseModel>("GetWorkItemDetail", routeUrl, HelperMethods.GetAuthorizationHeaderCollection(_azureApiSettings.Username, _azureApiSettings.Password));

            if (response.IsSucceeded)
                return Ok(response);
            return NotFound(response);
        }

        [HttpPost]
        [Route("workItems")]
        public async Task<IActionResult> GetWorkItemDetail(GetWorkItemDetailRequestModel request)
        {
            var routeUrl = RemoteUrl.GetWorkItemList(_azureApiSettings.BaseUrl);

            var response = await _restService.PostApiResponseAsync<WorkItemListModel>("GetWorkItemList", routeUrl, request, HelperMethods.GetAuthorizationHeaderCollection(_azureApiSettings.Username, _azureApiSettings.Password));

            if (response.IsSucceeded)
                return Ok(response);
            return NotFound(response);
        }
    }
}
