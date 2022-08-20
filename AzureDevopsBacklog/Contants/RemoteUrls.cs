namespace AzureDevopsBacklog.Contants
{
    public static class RemoteUrls
    {
        public static string GetWorkItemDetailSingle(string baseUrl, int id) => $"{baseUrl}/wit/workitems?ids={id}&api-version=6.0";
        public static string GetWorkItemDetailList(string baseUrl, List<int> idList) => $"{baseUrl}/wit/workitems?ids={string.Join(",", idList)}&api-version=6.0";
        public static string GetWorkItemList(string baseUrl) => $"{baseUrl}/wit/wiql?api-version=6.0";

    }
}
