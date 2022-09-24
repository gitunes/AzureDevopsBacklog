namespace AzureDevopsBacklog.Application.Models.RequestModels
{
    public class SendNotificationRequestModel
    {
        public string Subject { get; set; }
        public string Body { get; set; }
        public List<string> ToEmails { get; set; }
    }
}
