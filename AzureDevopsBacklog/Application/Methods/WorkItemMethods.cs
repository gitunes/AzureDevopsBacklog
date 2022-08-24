using AzureDevopsBacklog.Application.Models;
using AzureDevopsBacklog.Application.Models.ResponseModels;
using AzureDevopsBacklog.Contants;

namespace AzureDevopsBacklog.Application.Methods
{
    public class WorkItemMethods
    {
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
            else if(workItemDetail.State == WorkItemStates.Cancelled)
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

        public SprintDetailResponseModel GetWorkItems(BaseResponseModel<WorkItemDetailResponseModel> workItemDetailResponse)
        {
            var list = workItemDetailResponse?.Data?.WorkItemDetails.GroupBy(x => x.Fields.AssignedTo.UniqueName).Select(x => x.ToList()).ToList();
            var responseModel = new SprintDetailResponseModel() { Count = list.Count() };
            list.ForEach(userList =>
            {
                userList.ForEach(workItem =>
                {
                    SprintWorkItemDetail workItemDetail = new()
                    {
                        State = workItem.Fields.State,
                        IsHold = workItem.Fields.IsHold,
                        Tag = workItem.Fields.Tags,
                        Title = workItem.Fields.Title,
                        TShirtSize = workItem.Fields.TShirtSize
                    };
                    var user = responseModel.Users.Find(x => x.User.UniqueName == workItem.Fields.AssignedTo.UniqueName);
                    if (user == null)
                    {
                        UserSprintDetail userSprintDetail = new()
                        {
                            User = new() { Id = workItem.Fields.AssignedTo.Id, DisplayName = workItem.Fields.AssignedTo.DisplayName, UniqueName = workItem.Fields.AssignedTo.UniqueName },
                            WorkItemCount = userList.Count()
                        };

                        CheckWorkItemStatus(userSprintDetail, workItemDetail);
                        responseModel.Users.Add(userSprintDetail);
                    }
                    else
                    {
                        CheckWorkItemStatus(user, workItemDetail);
                    }

                });
            }
            );
            return responseModel;
        }

        public bool IsWorkItemDone(string state)
        {
            switch (state)
            {
                case WorkItemStates.New:
                    return false;
                case WorkItemStates.Committed:
                    return false;
                case WorkItemStates.Development:
                    return true;
                case WorkItemStates.Test:
                    return true;
                case WorkItemStates.Uat:
                    return true;
                case WorkItemStates.ReadyForDeployment:
                    return true;
                case WorkItemStates.Deployment:
                    return true;
                case WorkItemStates.Closed:
                    return true;
                case WorkItemStates.Cancelled:
                    return false;
                case WorkItemStates.Archive:
                    return true;
                default:
                    return false;
            }
        }
    }
}
