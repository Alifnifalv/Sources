using Eduegate.Domain.Entity.Models;
using Eduegate.Framework;
using Eduegate.Platforms.SubscriptionManager;
using Eduegate.Services.Contracts.Enums;
using Eduegate.Services.Contracts.Enums.Logging;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using System.Linq;
using Eduegate.Services.Contracts.Notifications;
using Eduegate.Domain.Entity.Models.Workflows;
using Eduegate.Services.Contracts.Logging;

namespace Eduegate.Domain.Logging
{
    public class LoggingHelper
    {
        public static void TransactionCreated(CallContext context, TransactionHead transactionHead)
        {
            var bl = new LoggingBL(context);
            var activities = new List<Services.Contracts.Logging.ActivityDTO>();
            activities.Add(new Services.Contracts.Logging.ActivityDTO()
            {
                ReferenceID = transactionHead.HeadIID.ToString(),
                Description = transactionHead.TransactionNo + " created as " + 
                    ((DocumentStatuses) transactionHead.DocumentStatusID).ToString() + " by " + context.EmailID,
                ActionStatusID = (int)ActionStatuses.Success,
                ActionTypeID = (int) ActivityTypes.Transactions,
                ActivityTypeID = (int)ActivityTypes.Transactions,
                CreatedBy = context.LoginID.Value,
                CreatedDate = DateTime.Now
            });

            Task.Factory.StartNew(() => bl.SaveActivitiesAsynch(activities));
            SubscriptionHandler.Notify(new SubscriptionDetail()
            {
                 SubScriptionType = SubscriptionTypes.NewActivity,
                 Data = activities
            });
        }

        public static void WorkflowCreated(CallContext context, List<WorkflowTransactionHeadMap> workflowLog, TransactionHead transactionHead)
        {
            var activities = new List<Services.Contracts.Logging.ActivityDTO>();

            var currentWorkflow = workflowLog.FirstOrDefault();

            if (currentWorkflow == null) return; 
            var rules = currentWorkflow.WorkflowTransactionHeadRuleMaps.FirstOrDefault();

            if (rules == null) return;

            var approvers = rules.WorkflowTransactionRuleApproverMaps.ToList();

            if (approvers == null) return;
            string approverEmailds = string.Empty;
            var notifications = new List<NotificationAlertsDTO>();


            ActivityDTO activity = null;

            foreach (var approver in approvers) {
                var approverName = approver.Employee.Login == null ? approver.Employee.EmployeeName 
                    : approver.Employee.Login.LoginEmailID;
                activity = new ActivityDTO()
                {
                    ReferenceID = transactionHead.HeadIID.ToString(),
                    Description = "Workflow created for " + transactionHead.TransactionNo +
                    " and assigned to " + approverName,
                    ActionStatusID = (int)ActionStatuses.Success,
                    ActionTypeID = (int)ActivityTypes.Transactions,
                    ActivityTypeID = (int)ActivityTypes.Transactions,
                    CreatedBy = context.LoginID.Value,
                    CreatedDate = DateTime.Now
                };

                SubscriptionHandler.Notify(new SubscriptionDetail()
                {
                    SubScriptionType = SubscriptionTypes.NewActivity,
                    Data = activity,
                    ReceiverLoginID = approver.Employee.Login == null ? (long?) null :
                        approver.Employee.Login.LoginIID,
                    ReceiverEmailID = approverName,
                    InitiaterLoginID = context.LoginID,
                    InitiaterEmailID = context.EmailID,
                });
                
                notifications.Add(new NotificationAlertsDTO()
                {
                    AlertStatusID = 2,
                    AlertTypeID = 2,
                    CreatedBy = (int?)context.LoginID,
                    FromLoginID = context.LoginID,
                    ToLoginID = approver.Employee == null ? (long?) null : approver.Employee.LoginID,
                    NotificationDate = DateTime.Now,
                    ReferenceID = transactionHead.HeadIID,
                    UpdatedBy = (int?)context.LoginID,
                    UpdatedDate = DateTime.Now,
                    CreatedDate = DateTime.Now,
                    Message = "Transaction " + transactionHead.TransactionNo +
                                 " waiting of approval of " + approverName,
                });

                approverEmailds = approverEmailds + approverName + ",";
            }          

            activity.Description = "Transaction " + transactionHead.TransactionNo +
                    "  waiting of approval of " + approverEmailds;
            activities.Add(activity);
            Task.Factory.StartNew(() => new LoggingBL(context).SaveActivitiesAsynch(activities));
            //set the inbox notification
            Task.Factory.StartNew(() => new NotificationBL(context).SaveNotificationAlerts(notifications));
        }

        public static void TransactionModified(CallContext context, TransactionHead transactionHead)
        {
            var bl = new LoggingBL(context);
            var activities = new List<Services.Contracts.Logging.ActivityDTO>();
            activities.Add(new Services.Contracts.Logging.ActivityDTO()
            {
                ReferenceID = transactionHead.HeadIID.ToString(),
                Description = transactionHead.TransactionNo + " modified by " + context.EmailID,
                ActionStatusID = (int)ActionStatuses.Success,
                ActionTypeID = (int)ActivityTypes.Transactions,
                ActivityTypeID = (int)ActivityTypes.Transactions,
                CreatedBy = context.LoginID.Value,
                CreatedDate = DateTime.Now
            });

            Task.Factory.StartNew(() => bl.SaveActivitiesAsynch(activities));
            SubscriptionHandler.Notify(new SubscriptionDetail()
            {
                SubScriptionType = SubscriptionTypes.NewActivity,
                Data = activities,
                InitiaterLoginID = context.LoginID,
                InitiaterEmailID = context.EmailID,
            });
        }

        public static void TransactionStatusChanged(CallContext context, TransactionHead transactionHead)
        {
            var bl = new LoggingBL(context);
            var activities = new List<Services.Contracts.Logging.ActivityDTO>();
            activities.Add(new Services.Contracts.Logging.ActivityDTO()
            {
                ReferenceID = transactionHead.HeadIID.ToString(),
                Description = transactionHead.TransactionNo + " status changed to " + ((DocumentStatuses)transactionHead.DocumentStatusID).ToString()
                    + " by " + context.EmailID,
                ActionStatusID = (int)ActionStatuses.Success,
                ActionTypeID = (int)ActivityTypes.Transactions,
                ActivityTypeID = (int)ActivityTypes.Transactions,
                CreatedBy = context.LoginID.Value,
                CreatedDate = DateTime.Now
            });

            Task.Factory.StartNew(() => bl.SaveActivitiesAsynch(activities));
            SubscriptionHandler.Notify(new SubscriptionDetail()
            {
                SubScriptionType = SubscriptionTypes.NewActivity,
                Data = activities,
                InitiaterLoginID = context.LoginID,
                InitiaterEmailID = context.EmailID,
            });
        }

        public static void TransactionProcessingFailed(CallContext context, TransactionHead transactionHead, string errorMessage)
        {
            var bl = new LoggingBL(context);
            var activities = new List<Services.Contracts.Logging.ActivityDTO>();
            activities.Add(new Services.Contracts.Logging.ActivityDTO()
            {
                ReferenceID = transactionHead.HeadIID.ToString(),
                Description = transactionHead.TransactionNo + " processing failed," + errorMessage,
                ActionStatusID = (int)ActionStatuses.Failed,
                ActionTypeID = (int)ActivityTypes.Transactions,
                ActivityTypeID = (int)ActivityTypes.Transactions,
                CreatedBy = context.LoginID.Value,
                CreatedDate = DateTime.Now
            });

            Task.Factory.StartNew(() => bl.SaveActivitiesAsynch(activities));
            SubscriptionHandler.Notify(new SubscriptionDetail()
            {
                SubScriptionType = SubscriptionTypes.NewActivity,
                Data = activities,
                InitiaterLoginID = context.LoginID,
                InitiaterEmailID = context.EmailID,
            });
        }
    }
}
