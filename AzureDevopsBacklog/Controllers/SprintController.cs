using AzureDevopsBacklog.Application.Methods;
using AzureDevopsBacklog.Application.Models.RequestModels;
using AzureDevopsBacklog.Application.Models.ResponseModels;
using AzureDevopsBacklog.Contants;
using AzureDevopsBacklog.Infrastructure.Interfaces;
using Microsoft.AspNetCore.Mvc;
using System.Text;

namespace AzureDevopsBacklog.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class SprintController : ControllerBase
    {
        private readonly ISprintService _sprintService;
        private readonly IReportService _reportService;
        private readonly IAzureApiSettings _azureApiSettings;

        public SprintController(ISprintService sprintService, IReportService reportService, IAzureApiSettings azureApiSettings)
        {
            _sprintService = sprintService;
            _reportService = reportService;
            _azureApiSettings = azureApiSettings;
        }

        [HttpPost]
        [Route("detail")]
        public async Task<IActionResult> GetSprintDetails(GetSprintDetailByTagRequestModel request)
        {
            var response = await _sprintService.GetSprintDetailAsync(request);
            return Ok(response);
        }

        [HttpPost]
        [Route("report")]
        public async Task<IActionResult> ReportToDevs(GetSprintDetailByTagRequestModel request)
        {
            var response = await _sprintService.GetSprintDetailAsync(request);
            _reportService.SendSprintReport(response.Data.Users, request.Tag, request.WeeklyMaxEffort);
            return Ok();
        }
    }
}
