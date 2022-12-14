namespace AzureDevopsBacklog.Contants
{
    public static class ReportMessages
    {
        public const string InCompleteWorkItemMessage = "Şuana kadar tamamlanmamış maddeleriniz bulunmaktadır.</br></br> ";
        public static string SprintReminding(string sprintTag) => $"Sprint({sprintTag}) Bilgilendirilmesi";
        public static string ReportDescription(int sprintUserCount, int lowEfforUserCount, double effort) => $"Sprinte Katılan Yazılımcı Sayısı:{sprintUserCount}. Haftalık efordan({effort}) az iş tamamlamış kişiler({lowEfforUserCount}) ve tamamlanmamış maddeleri aşağıdaki gibidir. </br></br> ";
        public static string UserInfo(string uniqueName, int workItemCount) => $"<p style='color:red'>Kişi: {uniqueName}({workItemCount})</p> </br>";
        public static string InCompleteWorkItemMessageTemplate(string baseUrl, string title, int id) => $"Başlık: {title} </br> Url: {RemoteUrls.GetWorkItemViewUrl(baseUrl, id)} </br></br> ";
    }
}
