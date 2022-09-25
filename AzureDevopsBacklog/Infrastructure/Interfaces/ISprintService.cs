using AzureDevopsBacklog.Application.Models.RequestModels;
using AzureDevopsBacklog.Application.Models.ResponseModels;

namespace AzureDevopsBacklog.Infrastructure.Interfaces
{
    public interface ISprintService
    {
        Task<BaseResponseModel<SprintDetailResponseModel>> GetSprintDetailAsync(GetSprintDetailByTagRequestModel request);
    }
}
