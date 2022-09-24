using AzureDevopsBacklog.Application.Models;
using AzureDevopsBacklog.Application.Models.RequestModels;
using AzureDevopsBacklog.Contants;
using AzureDevopsBacklog.Infrastructure.Interfaces;
using System.Net;
using System.Net.Mail;
using System.Text;

namespace AzureDevopsBacklog.Infrastructure.Services
{
    public class ReportService : IReportService
    {
        private readonly ISmtpClientSettings _smtpClientSettings;
        private IAzureApiSettings _azureApiSettings;

        public ReportService(ISmtpClientSettings smtpClientSettings, IAzureApiSettings azureApiSettings)
        {
            _smtpClientSettings = smtpClientSettings;
            _azureApiSettings = azureApiSettings;
        }
        public bool SendNotification(SendNotificationRequestModel model)
        {
            using MailMessage message = new MailMessage();
            message.From = new(_smtpClientSettings.FromEmail);
            model.ToEmails.ForEach(email => message.To.Add(new(email)));
            message.Subject = model.Subject;
            message.Body = model.Body;
            using SmtpClient client = new SmtpClient(_smtpClientSettings.Host, _smtpClientSettings.Port);
            client.EnableSsl = true;
            client.Credentials = new NetworkCredential(_smtpClientSettings.UserName, _smtpClientSettings.Password);
            try
            {
                client.Send(message);
            }
            catch (SmtpException)
            {
                return false;
            }
            return true;
        }

        public void SendSprintReport(List<UserSprintDetail> users, string sprintTag, double effort)
        {
            var bodyForManagers = new StringBuilder();
            bodyForManagers.AppendLine(ReportMessages.ReportDescription(effort));
            users.Where(user => user.IsLowEffort && user.NotFinished.Any()).ToList().ForEach(workItem =>
            {
                bodyForManagers.AppendLine(ReportMessages.UserInfo(workItem.User.UniqueName, workItem.NotFinished.Count()));
                var bodyForDevs = new StringBuilder();
                bodyForDevs.AppendLine(ReportMessages.InCompleteWorkItemMessage);
                workItem.NotFinished.ForEach(item =>
                {
                    bodyForDevs.AppendLine(ReportMessages.InCompleteWorkItemMessageTemplate(_azureApiSettings.BaseUrl, item.Title, item.Id));
                    bodyForManagers.AppendLine(ReportMessages.InCompleteWorkItemMessageTemplate(_azureApiSettings.BaseUrl, item.Title, item.Id));
                });
                SendNotification(new() { Body = bodyForDevs.ToString(), Subject = ReportMessages.SprintReminding(sprintTag), ToEmails = new() { workItem.User.UniqueName } });
            });
            SendNotification(new() { Body = bodyForManagers.ToString(), Subject = ReportMessages.SprintReminding(sprintTag), ToEmails = UserInformations.Managers });
        }
    }
}
