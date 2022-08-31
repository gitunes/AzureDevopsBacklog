using AzureDevopsBacklog.Application.Methods;
using AzureDevopsBacklog.Application.Models.RequestModels;
using AzureDevopsBacklog.Application.Models.ResponseModels;
using AzureDevopsBacklog.Contants;
using AzureDevopsBacklog.Infrastructure.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace AzureDevopsBacklog.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class WorkItemController : ControllerBase
    {
        private readonly IWorkItemService _workItemService;

        public WorkItemController(IWorkItemService workItemService)
        {
            _workItemService = workItemService;
        }

        [HttpGet]
        [Route("details")]
        public async Task<IActionResult> GetWorkItemDetail(int id)
        {
            var response = await _workItemService.GetWorkItemDetail(id);
            if (response.IsSucceeded)
                return Ok(response);
            return NotFound(response);
        }

        [HttpPost]
        [Route("workItems")]
        public async Task<IActionResult> GetWorkItemsByQuery(GetWorkItemDetailRequestModel request)
        {
            var response = await _workItemService.GetWorkItemsByQuery(request);
            if (response.IsSucceeded)
                return Ok(response);
            return NotFound(response);
        }

        [HttpPost]
        [Route("workItems/filter")]
        public async Task<IActionResult> GetWorkItemDetailsByFilter(GetWorkItemDetailByFilterRequestModel request)
        {
            var response = await _workItemService.GetWorkItemDetailsByFilter(request);
            if (response.IsSucceeded)
                return Ok(response);
            return NotFound(response);
        }
    }
}
