using AzureDevopsBacklog.Application.Models.RequestModels;
using AzureDevopsBacklog.Contants;
using AzureDevopsBacklog.Extensions;

namespace AzureDevopsBacklog.Application.Methods
{
    public static class FilterMethods
    {
        public static string GetFilteredQuery(GetWorkItemDetailByFilterRequestModel model)
        {
            var query = string.Empty;
            if (model.CreateDate.MinDate.HasValue)
            {
                query = string.Concat(query, $"[System.CreatedDate] >= '{model.CreateDate.MinDate.Value.ToString(DateFormats.YearMonthDayWithHyphen)}' ");
            }
            if (model.CreateDate.MaxDate.HasValue)
            {
                query = string.Concat(string.IsNullOrEmpty(query) ? query : $"{query} AND ", $"[System.CreatedDate] <= '{model.CreateDate.MaxDate.Value.ToString(DateFormats.YearMonthDayWithHyphen)}' ");
            }
            if (!model.Tags.IsNullOrNotAny())
            {
                model.Tags.ForEach(tag =>
                {
                    query = string.Concat(string.IsNullOrEmpty(query) ? query : $"{query} AND ", $"[System.Tags] Contains '{tag}' ");
                });
            }
            if (!string.IsNullOrEmpty(model.State))
            {
                query = string.Concat(string.IsNullOrEmpty(query) ? query : $"{query} AND ", $"[System.State] = '{model.State}' ");
            }
            if (model.IsHold ?? false)
            {
                query = string.Concat(string.IsNullOrEmpty(query) ? query : $"{query} AND ", $"[Custom.IsHold] = true ");
            }
            return Queries.GetWorkItemFilteredQuery(query);
        }

        public static string GetSprintWorkItemsQuery(GetSprintDetailByTagRequestModel model)
        {
            if (!string.IsNullOrEmpty(model.Tag))
            {
                var query = $"[System.Tags] Contains '{model.Tag}'";
                return Queries.GetWorkItemFilteredQuery(query);
            }
            return string.Empty;
        }
    }
}
