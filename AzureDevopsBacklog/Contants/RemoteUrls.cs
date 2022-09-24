namespace AzureDevopsBacklog.Contants
{
    public static class RemoteUrls
    {
        public static string GetWorkItemDetailSingle(string baseUrl, int id) => $"{baseUrl}/_apis/wit/workitems?ids={id}&api-version=6.0";
        public static string GetWorkItemDetailList(string baseUrl, List<int> idList) => $"{baseUrl}/_apis/wit/workitems?ids={string.Join(",", idList)}&api-version=6.0";
        public static string GetWorkItemList(string baseUrl) => $"{baseUrl}/_apis/wit/wiql?$top=19999&api-version=6.0";
        public static string GetWorkItemViewUrl(string baseUrl, int id) => $"{baseUrl}/_workitems/edit/{id}";
    }
}
