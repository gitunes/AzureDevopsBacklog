using AzureDevopsBacklog.Application.Models.RequestModels;
using AzureDevopsBacklog.Application.Models.ResponseModels;

namespace AzureDevopsBacklog.Infrastructure.Interfaces
{
    public interface IWorkItemService
    {
        Task<BaseResponseModel<WorkItemDetailResponseModel>> GetWorkItemDetail(int id);
        Task<BaseResponseModel<WorkItemListResponseModel>> GetWorkItemsByQuery(GetWorkItemDetailRequestModel model);
        Task<BaseResponseModel<WorkItemDetailResponseModel>> GetWorkItemDetailsByFilter(GetWorkItemDetailByFilterRequestModel model);
    }
}
