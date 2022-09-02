using AzureDevopsBacklog.Contants;

namespace AzureDevopsBacklog.Application.Models.ResponseModels
{
    public class SuccessResponseModel<TModel> : BaseResponseModel<TModel>
    {
        public SuccessResponseModel(TModel data, string message) : base(data, true, message)
        {
        }
        public SuccessResponseModel(TModel data) : base(data, true, SuccessMessages.RequestSuccessful)
        {
        }

        public SuccessResponseModel() : base(default, true, SuccessMessages.RequestSuccessful)
        {
        }
    }
}
