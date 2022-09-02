using System.Text.Json.Serialization;

namespace AzureDevopsBacklog.Application.Models
{
    public class FieldModel
    {
        [JsonPropertyName("System.AssignedTo")]
        public UserModel AssignedTo { get; set; } = new();

        [JsonPropertyName("System.CreatedBy")]
        public UserModel CreatedBy { get; set; } = new();

        [JsonPropertyName("Microsoft.VSTS.Common.Priority")]
        public int Priority { get; set; }

        [JsonPropertyName("Custom.DominoId")]
        public int DominoId { get; set; }

        [JsonPropertyName("System.CommentCount")]
        public int CommentCount { get; set; }

        [JsonPropertyName("Custom.TShirtSize")]
        public string TShirtSize { get; set; }

        [JsonPropertyName("System.Tags")]
        public string Tags { get; set; }

        [JsonPropertyName("System.State")]
        public string State { get; set; }

        [JsonPropertyName("System.Title")]
        public string Title { get; set; }

        [JsonPropertyName("System.WorkItemType")]
        public string WorkItemType { get; set; }

        [JsonPropertyName("System.Description")]
        public string Description { get; set; }

        [JsonPropertyName("Microsoft.VSTS.Common.AcceptanceCriteria")]
        public string AcceptanceCriteria { get; set; }

        [JsonPropertyName("System.AreaPath")]
        public string AreaPath { get; set; }

        [JsonPropertyName("System.TeamProject")]
        public string TeamProject { get; set; }

        [JsonPropertyName("System.IterationPath")]
        public string IterationPath { get; set; }

        [JsonPropertyName("System.Reason")]
        public string Reason { get; set; }

        [JsonPropertyName("System.BoardLan")]
        public string BoardLane { get; set; }

        [JsonPropertyName("System.BoardColumn")]
        public string BoardColumn { get; set; }

        [JsonPropertyName("System.BoardColumnDone")]
        public bool BoardColumnDone { get; set; }

        [JsonPropertyName("Custom.IsHold")]
        public bool IsHold { get; set; }

        [JsonPropertyName("Microsoft.VSTS.Common.StateChangeDate")]
        public string StateChangeDate { get; set; }

        [JsonPropertyName("Microsoft.VSTS.Common.ActivatedDate")]
        public string ActivatedDate { get; set; }

        [JsonPropertyName("Microsoft.VSTS.Common.ResolvedDate")]
        public string ResolvedDate { get; set; }

        [JsonPropertyName("Microsoft.VSTS.Scheduling.TargetDate")]
        public string TargetDate { get; set; }

        [JsonPropertyName("Custom.ReadyDate")]
        public string ReadyDate { get; set; }

        [JsonPropertyName("Custom.DevelopmentDate")]
        public string DevelopmentDate { get; set; }

        [JsonPropertyName("Custom.DevelopmentEndDate")]
        public string DevelopmentEndDate { get; set; }

        [JsonPropertyName("Custom.DevelopmentEndDate2")]
        public string DevelopmentEndDate2 { get; set; }
        
        [JsonPropertyName("System.ChangedDate")]
        public string ChangedDate { get; set; }

        [JsonPropertyName("Custom.CreatedDate")]
        public string CreatedDate { get; set; }

        [JsonPropertyName("Custom.CreatedDate2")]
        public string CreatedDate2 { get; set; }
    }
}
