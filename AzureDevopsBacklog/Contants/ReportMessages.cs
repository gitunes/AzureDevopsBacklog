namespace AzureDevopsBacklog.Contants
{
    public static class ReportMessages
    {
        public const string InCompleteWorkItemMessage = "Şuana kadar tamamlanmamış maddeleriniz bulunmaktadır.\n\n";
        public static string SprintReminding(string sprintTag) => $"Sprint({sprintTag}) Bilgilendirilmesi";
        public static string ReportDescription(double effort) => $"Haftalık efordan({effort}) az iş tamamlamış kişiler ve tamamlanmamış maddeleri aşağıdaki gibidir.\n\n";
        public static string UserInfo(string uniqueName, int workItemCount) => $"<p style='color:red'>Kişi: {uniqueName}({workItemCount})</p> \n";
        public static string InCompleteWorkItemMessageTemplate(string baseUrl, string title, int id) => $"Başlık: {title} \n Url: {RemoteUrls.GetWorkItemViewUrl(baseUrl, id)} \n\n";
    }
}
