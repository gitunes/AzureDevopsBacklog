namespace AzureDevopsBacklog.Application.Models.ResponseModels
{
    public class BaseResponseModel<TModel>
    {
        public TModel Data { get; set; }
        public string ResponseMessage { get; set; }
        public bool IsSucceeded { get; set; }
    }
}
