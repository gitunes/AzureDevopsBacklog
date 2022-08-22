namespace AzureDevopsBacklog.Extensions
{
    public static class CollectionExtensions
    {
        public static bool IsNullOrNotAny<T>(this IEnumerable<T> source)
        {
            return !(source?.Any() ?? false);
        }
    }
}
