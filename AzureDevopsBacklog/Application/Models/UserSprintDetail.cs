namespace AzureDevopsBacklog.Application.Models
{
    public class UserSprintDetail
    {
        public int WorkItemCount { get; set; }
        public bool IsLowEffort { get; set; }
        public UserModel User { get; set; } = new();
        public List<SprintWorkItemDetail> Finished { get; set; } = new();
        public List<SprintWorkItemDetail> NotFinished { get; set; } = new();
        public List<SprintWorkItemDetail> Holds { get; set; } = new();
        public List<SprintWorkItemDetail> Cancelled { get; set; } = new();
        public List<SprintWorkItemDetail> ContainsMinimum3Tags { get; set; } = new();
    }
}
