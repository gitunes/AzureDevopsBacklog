namespace AzureDevopsBacklog.Application.Models
{
    public class SprintWorkItemDetail
    {
        public int Id { get; set; }
        public string Title { get; set; }
        public string Tag { get; set; }
        public string State { get; set; }
        public string TShirtSize { get; set; }
        public bool IsHold { get; set; }
    }
}
