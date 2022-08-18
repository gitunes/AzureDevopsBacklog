namespace AzureDevopsBacklog.Contants
{
    public static class RemoteUrl
    {
        public static string GetWorkItemDetail(string baseUrl, int id) => $"{baseUrl}/wit/workitems?ids={id}&api-version=6.0";
        public static string GetWorkItemList(string baseUrl) => $"{baseUrl}/wit/wiql?api-version=6.0";

    }
}
