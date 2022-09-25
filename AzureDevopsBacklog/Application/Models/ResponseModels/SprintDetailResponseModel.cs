namespace AzureDevopsBacklog.Application.Models.ResponseModels
{
    public class SprintDetailResponseModel
    {
        public int WorkItemCount { get; set; }
        public int UserCount { get; set; }
        public List<UserSprintDetail> Users { get; set; } = new();
        public List<string> LowEffortUsers { get; set; } = new();
    }
}
