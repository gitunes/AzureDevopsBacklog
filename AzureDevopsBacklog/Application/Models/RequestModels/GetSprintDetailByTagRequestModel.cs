namespace AzureDevopsBacklog.Application.Models.RequestModels
{
    public class GetSprintDetailByTagRequestModel
    {
        public string Tag { get; set; }
        public bool IsEnabledLowEffortMonitoring { get; set; }
        public double WeeklyMaxEffort { get; set; }
    }
}
