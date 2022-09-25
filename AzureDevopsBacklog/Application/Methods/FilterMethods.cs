using AzureDevopsBacklog.Application.Models.RequestModels;
using AzureDevopsBacklog.Contants;
using AzureDevopsBacklog.Extensions;

namespace AzureDevopsBacklog.Application.Methods
{
    public static class FilterMethods
    {
        public static string GetFilteredQuery(GetWorkItemDetailByFilterRequestModel model)
        {
            List<string> filters = new List<string>();
            if (model.CreateDate.MinDate.HasValue)
            {
                filters.Add(BiggerOrEqual(WorkItemAttributes.CreatedDate, model.CreateDate.MinDate.Value.ToString(DateFormats.YearMonthDayWithHyphen)));
            }
            if (model.CreateDate.MaxDate.HasValue)
            {
                filters.Add(FilterMethods.SmallerOrEqual(WorkItemAttributes.CreatedDate, model.CreateDate.MaxDate.Value.ToString(DateFormats.YearMonthDayWithHyphen)));
            }
            if (!model.Tags.IsNullOrNotAny())
            {
                model.Tags.ForEach(tag =>
                {
                    filters.Add(Contains(WorkItemAttributes.Tags, tag));
                });
            }
            if (!string.IsNullOrEmpty(model.State))
            {
                filters.Add(Equals(WorkItemAttributes.State, model.State));
            }
            if (model.IsHold ?? false)
            {
                filters.Add(Equals(WorkItemAttributes.IsHold, model.IsHold));
            }
            return GetWorkItemFilteredQuery(filters);
        }

        public static string GetWorkItemFilteredQuery(List<string> filters)
        {
            var query = Queries.GetWorkItemFilteredQuery(string.Join(" AND ", filters));
            return query;
        }

        public static string GetSprintWorkItemsQuery(GetSprintDetailByTagRequestModel model)
        {
            if (!string.IsNullOrEmpty(model.Tag))
            {
                var query = $"[Tags] Contains '{model.Tag}'";
                return Queries.GetWorkItemFilteredQuery(query);
            }
            return string.Empty;
        }

        public static string Contains(string type, string value) => $" [{type}] Contains '{value}' ";

        public static string Equals<T>(string type, T value) => $" [{type}] = '{value}' ";

        public static string BiggerOrEqual<T>(string type, T value) => $" [{type}] >= '{value}' ";

        public static string SmallerOrEqual<T>(string type, T value) => $" [{type}] <= '{value}' ";
    }
}
