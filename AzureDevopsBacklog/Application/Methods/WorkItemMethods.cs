using AzureDevopsBacklog.Application.Models;
using AzureDevopsBacklog.Application.Models.ResponseModels;
using AzureDevopsBacklog.Contants;
using AzureDevopsBacklog.Infrastructure.Interfaces;

namespace AzureDevopsBacklog.Application.Methods
{
    public class WorkItemMethods
    {

        public SprintDetailResponseModel GetWorkItems(BaseResponseModel<WorkItemDetailResponseModel> workItemDetailResponse, SprintDetailResponseModel responseModel)
        {
            var list = workItemDetailResponse?.Data?.WorkItemDetails.Where(x => !string.IsNullOrEmpty(x.Fields.AssignedTo.Id)).GroupBy(x => x.Fields.AssignedTo.UniqueName).Select(x => x.ToList()).ToList();
            var x = list.Count();
            list?.ForEach(userList =>
            {
                userList.ForEach(workItem =>
                {
                    SprintWorkItemDetail workItemDetail = new()
                    {
                        Id = workItem.Id,
                        State = workItem.Fields.State,
                        IsHold = workItem.Fields.IsHold,
                        Tag = workItem.Fields.Tags,
                        Title = workItem.Fields.Title,
                        TShirtSize = workItem.Fields.TShirtSize,
                        CreatedBy = workItem.Fields.CreatedBy.DisplayName
                    };

                    var user = responseModel.Users.FirstOrDefault(x => x.User.UniqueName == workItem.Fields.AssignedTo.UniqueName);
                    if (user == null)
                    {
                        UserSprintDetail userSprintDetail = new()
                        {
                            User = new()
                            {
                                Id = workItem.Fields.AssignedTo.Id,
                                DisplayName = workItem.Fields.AssignedTo.DisplayName,
                                UniqueName = workItem.Fields.AssignedTo.UniqueName
                            },
                            WorkItemCount = 1,
                        };

                        CheckWorkItemStatus(userSprintDetail, workItemDetail);
                        responseModel.Users.Add(userSprintDetail);

                    }
                    else
                    {
                        CheckWorkItemStatus(user, workItemDetail);
                        var userRecord = responseModel.Users.Find(x => x.User.Id == user.User.Id);
                        userRecord = user;
                        userRecord.WorkItemCount++;
                    }
                });
            }
            );
            return responseModel;
        }

        public async Task<BaseResponseModel<WorkItemDetailResponseModel>> GetWorkItemDetailsAsync(IAzureApiSettings azureApiSettings, IRestService restService,
            List<WorkItemModel> list, int skipCount)
        {
            var detailUrl = RemoteUrls.GetWorkItemDetailList(azureApiSettings.BaseUrl, list.Skip(skipCount).Take(200).Select(x => x.Id).ToList());
            var workItemDetailResponse = await restService.GetApiResponseAsync<WorkItemDetailResponseModel>("GetWorkItemDetail", detailUrl, HelperMethods.GetAuthorizationHeaderCollection(azureApiSettings.Username, azureApiSettings.Password));
            return workItemDetailResponse;
        }
        public void CheckWorkItemStatus(UserSprintDetail userSprintDetail, SprintWorkItemDetail workItemDetail)
        {
            var tags = workItemDetail.Tag.Split(";");
            if (tags.Count(x => x.Contains("S-")) > 2)
            {
                userSprintDetail.ContainsMinimum3Tags.Add(workItemDetail);
            }
            if (workItemDetail.IsHold)
            {
                userSprintDetail.Holds.Add(workItemDetail);
            }
            else if (workItemDetail.State == WorkItemStates.Cancelled)
            {
                userSprintDetail.Cancelled.Add(workItemDetail);
            }
            else if (!IsWorkItemDone(workItemDetail.State))
            {
                userSprintDetail.NotFinished.Add(workItemDetail);
            }
            else
            {
                userSprintDetail.Finished.Add(workItemDetail);
            }
        }

        public bool IsWorkItemDone(string state)
        {
            switch (state)
            {
                case WorkItemStates.New:
                case WorkItemStates.Committed:
                case WorkItemStates.Development:
                case WorkItemStates.Cancelled:
                    return false;
                case WorkItemStates.Test:
                case WorkItemStates.Uat:
                case WorkItemStates.ReadyForDeployment:
                case WorkItemStates.Deployment:
                case WorkItemStates.Closed:
                case WorkItemStates.Archive:
                    return true;
                default:
                    return false;
            }
        }

        public double GetTotalEffort(List<string> sizes)
        {
            double effort = 0;
            sizes.ForEach(size =>
            {
                switch (size)
                {
                    case TShirtSizes.XXSmall:
                        effort += 0.2;
                        break;
                    case TShirtSizes.XSmall:
                        effort += 0.5;
                        break;
                    case TShirtSizes.Small:
                        effort += 1;
                        break;
                    case TShirtSizes.Medium:
                        effort += 2.5;
                        break;
                    case TShirtSizes.Large:
                        effort += 4.5;
                        break;
                    default:
                        break;
                }
            });
            return effort;
        }

        public void MarkLowEffortUsers(SprintDetailResponseModel model, double weeklyMaxEffort)
        {
            model.Users.ForEach(user =>
            {
                var size = GetTotalEffort(user.Finished.Select(x => x.TShirtSize).ToList());
                if (size < weeklyMaxEffort)
                {
                    user.IsLowEffort = true;
                    model.LowEffortUsers.Add(user.User.UniqueName);
                }
            });
        }
    }
}
