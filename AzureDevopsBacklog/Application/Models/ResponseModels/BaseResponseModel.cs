namespace AzureDevopsBacklog.Application.Models.ResponseModels
{
    public class BaseResponseModel<TModel>
    {
        public BaseResponseModel()
        {
        }

        public BaseResponseModel(TModel data, bool isSucceeded, string responseMessage)
        {
            Data = data;
            ResponseMessage = responseMessage;
            IsSucceeded = isSucceeded;
        }

        public TModel Data { get; set; }
        public string ResponseMessage { get; set; }
        public bool IsSucceeded { get; set; }
    }
}
