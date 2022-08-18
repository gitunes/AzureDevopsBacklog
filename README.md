# AzureDevopsBacklog
Listing work items in Azure Devops's Backlog

# PlanningDemoProject

## Context
Retrieves the work items in the backlog with wsql query statements.\
Brings the details by work item id

## Notes
Application requires BaseUrl, Username and Password for Azure Devops authentication.\
You should put "AzureApiSettings" object containing "Username", "Password" and "BaseUrl" in secret.json.\
BaseUrl -> https://dev.azure.com/{organization}/{project}/{team}/_apis
