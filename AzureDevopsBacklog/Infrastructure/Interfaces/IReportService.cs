using AzureDevopsBacklog.Application.Models;
using AzureDevopsBacklog.Application.Models.RequestModels;

namespace AzureDevopsBacklog.Infrastructure.Interfaces
{
    public interface IReportService
    {
        bool SendNotification(SendNotificationRequestModel model);
        void SendSprintReport(List<UserSprintDetail> users, string sprintTag, double effort);
    }
}
