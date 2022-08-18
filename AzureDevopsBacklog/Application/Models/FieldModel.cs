using Newtonsoft.Json;

namespace AzureDevopsBacklog.Application.Models
{
    public class FieldModel
    {
        [JsonProperty("System.AssignedTo")]
        public UserModel AssignedTo { get; set; } = new();

        [JsonProperty("System.CreatedBy")]
        public UserModel CreatedBy { get; set; } = new();

        [JsonProperty("Microsoft.VSTS.Common.Priority")]
        public int Priority { get; set; }

        [JsonProperty("Custom.DominoId")]
        public int DominoId { get; set; }

        [JsonProperty("System.CommentCount")]
        public int CommentCount { get; set; }

        [JsonProperty("Custom.TShirtSize")]
        public string TShirtSize { get; set; }

        [JsonProperty("System.Tags")]
        public string Tags { get; set; }

        [JsonProperty("System.State")]
        public string State { get; set; }

        [JsonProperty("System.Title")]
        public string Title { get; set; }

        [JsonProperty("System.WorkItemType")]
        public string WorkItemType { get; set; }

        [JsonProperty("System.Description")]
        public string Description { get; set; }

        [JsonProperty("Microsoft.VSTS.Common.AcceptanceCriteria")]
        public string AcceptanceCriteria { get; set; }

        [JsonProperty("System.AreaPath")]
        public string AreaPath { get; set; }

        [JsonProperty("System.TeamProject")]
        public string TeamProject { get; set; }

        [JsonProperty("System.IterationPath")]
        public string IterationPath { get; set; }

        [JsonProperty("System.Reason")]
        public string Reason { get; set; }

        [JsonProperty("System.BoardLan")]
        public string BoardLane { get; set; }

        [JsonProperty("System.BoardColumn")]
        public string BoardColumn { get; set; }

        [JsonProperty("System.BoardColumnDone")]
        public bool BoardColumnDone { get; set; }

        [JsonProperty("Custom.IsHold")]
        public bool IsHold { get; set; }

        [JsonProperty("Microsoft.VSTS.Common.StateChangeDate")]
        public string StateChangeDate { get; set; }

        [JsonProperty("Microsoft.VSTS.Common.ActivatedDate")]
        public string ActivatedDate { get; set; }

        [JsonProperty("Microsoft.VSTS.Common.ResolvedDate")]
        public string ResolvedDate { get; set; }

        [JsonProperty("Microsoft.VSTS.Scheduling.TargetDate")]
        public string TargetDate { get; set; }

        [JsonProperty("Custom.ReadyDate")]
        public string ReadyDate { get; set; }

        [JsonProperty("Custom.DevelopmentDate")]
        public string DevelopmentDate { get; set; }

        [JsonProperty("Custom.DevelopmentEndDate")]
        public string DevelopmentEndDate { get; set; }

        [JsonProperty("Custom.DevelopmentEndDate2")]
        public string DevelopmentEndDate2 { get; set; }
        
        [JsonProperty("System.ChangedDate")]
        public string ChangedDate { get; set; }

        [JsonProperty("Custom.CreatedDate")]
        public string CreatedDate { get; set; }

        [JsonProperty("Custom.CreatedDate2")]
        public string CreatedDate2 { get; set; }
    }
}
