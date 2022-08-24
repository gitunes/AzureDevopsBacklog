namespace AzureDevopsBacklog.Application.Models.ResponseModels
{
    public class SprintDetailResponseModel
    {
        public int Count { get; set; }
        public List<UserSprintDetail> Users { get; set; } = new();
    }

    public class UserSprintDetail
    {
        public int WorkItemCount { get; set; }
        public UserModel User { get; set; } = new();
        public List<SprintWorkItemDetail> Finished { get; set; } = new();
        public List<SprintWorkItemDetail> NotFinished { get; set; } = new();
        public List<SprintWorkItemDetail> Holds { get; set; } = new();
        public List<SprintWorkItemDetail> ContainsMinimum3Tags { get; set; } = new();
    }

    public class SprintWorkItemDetail
    {
        public string Title { get; set; }
        public string Tag { get; set; }
        public string State { get; set; }
        public string TShirtSize { get; set; }
        public bool IsHold { get; set; }
    }
}
