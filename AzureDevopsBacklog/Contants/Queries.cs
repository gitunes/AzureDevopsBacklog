namespace AzureDevopsBacklog.Contants
{
    public static class Queries
    {
        public static string GetWorkItemFilteredQuery(string query) => $"Select * From WorkItems Where {query}";
    }
}
