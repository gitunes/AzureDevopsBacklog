namespace AzureDevopsBacklog.Application.Models.ResponseModels
{
    public class ErrorResponseModel<TModel> : BaseResponseModel<TModel>
    {
        public ErrorResponseModel(TModel data, string message) : base(data, false, message)
        {
        }
    }
}
