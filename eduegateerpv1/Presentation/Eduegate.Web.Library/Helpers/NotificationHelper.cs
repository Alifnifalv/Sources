using System;
using System.Collections.Generic;
using System.Linq;
using Eduegate.Framework;
using Eduegate.Services.Contracts.Notifications;
using Eduegate.Framework.Extensions;
using Eduegate.Application.Mvc;
using Eduegate.Services.Client.Factory;

namespace Eduegate.Web.Library.Helpers
{
    public class NotificationHelper
    {
        private BaseController _baseController;
        private CallContext _context;
        public NotificationHelper(BaseController baseController,CallContext context)
        {
            _baseController = baseController;
            _context = context;
        }
        public void SendConfirmationEmail(long headID, EmailNotificationTypeDTO emailNotificationTypeDetail, string transactionNumber, long? companyID = null, int? siteID = null)
        {
            var notificationDTO = new EmailNotificationDTO()
            {
                ToEmailID = _context.EmailID,
                EmailNotificationType = Eduegate.Services.Contracts.Enums.EmailNotificationTypes.OrderConfirmation,
                AdditionalParameters = new List<Eduegate.Services.Contracts.Commons.KeyValueParameterDTO>(),
                Subject = emailNotificationTypeDetail.IsNotNull() ? string.Concat(emailNotificationTypeDetail.EmailSubject, " ", transactionNumber) : string.Empty,
            };

            notificationDTO.AdditionalParameters.Add(new Eduegate.Services.Contracts.Commons.KeyValueParameterDTO()
            {
                ParameterName = Eduegate.Services.Contracts.Constants.EmailNotificationType.OrderConfirmation.Keys.OrderID,
                ParameterValue = headID.ToString()
            });

            notificationDTO.AdditionalParameters.Add(new Eduegate.Services.Contracts.Commons.KeyValueParameterDTO()
            {
                ParameterName = Eduegate.Services.Contracts.Constants.EmailNotificationType.OrderConfirmation.Keys.OrderHistoryURL,
                ParameterValue = new Domain.Setting.SettingBL().GetSettingValue<string>("RootUrl") + Convert.ToString(new Domain.Setting.SettingBL().GetSettingValue<string>("OrderHistoryURL"))
            });

            if (companyID.HasValue)
            {
                notificationDTO.AdditionalParameters.Add(new Eduegate.Services.Contracts.Commons.KeyValueParameterDTO()
                {
                    ParameterName = "CompanyID",
                    ParameterValue = companyID.Value.ToString()
                });
            }


            if (siteID.HasValue)
            {
                notificationDTO.AdditionalParameters.Add(new Eduegate.Services.Contracts.Commons.KeyValueParameterDTO()
                {
                    ParameterName = "SiteID",
                    ParameterValue = siteID.Value.ToString()
                });
            }

            notificationDTO.AdditionalParameters.Add(new Eduegate.Services.Contracts.Commons.KeyValueParameterDTO()
            {
                ParameterName = Eduegate.Services.Contracts.Constants.EmailNotificationType.OrderConfirmation.Keys.OrderHistoryURL,
                ParameterValue = new Domain.Setting.SettingBL().GetSettingValue<string>("RootUrl") + Convert.ToString(new Domain.Setting.SettingBL().GetSettingValue<string>("OrderHistoryURL"))
            });

            notificationDTO = GetAdditionalNotificationParameters(notificationDTO, _context);
            notificationDTO.FromEmailID = new Domain.Setting.SettingBL().GetSettingValue<string>("FromAddress");

            ClientFactory.NotificationServiceClient(_context).SaveEmailData(notificationDTO);
        }

        public void SendOrderDispatchEmail(long headID, EmailNotificationTypeDTO emailNotificationTypeDetail, string transactionNumber, string airwayBillNO, long jobEntryHeadId,int serviceProviderID,long customerID)
        {
            var userDTO = ClientFactory.AccountServiceClient(_context).GetUserDetailsByCustomerID(customerID);
            var notificationDTO = new EmailNotificationDTO()
            {
                //ToEmailID = _context.EmailID,
                ToEmailID = userDTO.LoginEmailID,
                EmailNotificationType = Eduegate.Services.Contracts.Enums.EmailNotificationTypes.OrderDispatch,
                AdditionalParameters = new List<Eduegate.Services.Contracts.Commons.KeyValueParameterDTO>(),
                Subject = emailNotificationTypeDetail.IsNotNull() ? string.Concat(emailNotificationTypeDetail.EmailSubject, " ", transactionNumber) : string.Empty,
            };

            notificationDTO.AdditionalParameters.Add(new Eduegate.Services.Contracts.Commons.KeyValueParameterDTO()
            {
                ParameterName = Eduegate.Services.Contracts.Constants.EmailNotificationType.OrderDispatch.Keys.OrderID,
                ParameterValue = headID.ToString()
            });

            notificationDTO.AdditionalParameters.Add(new Eduegate.Services.Contracts.Commons.KeyValueParameterDTO()
            {
                ParameterName = Eduegate.Services.Contracts.Constants.EmailNotificationType.OrderDispatch.Keys.OrderHistoryURL,
                ParameterValue = new Domain.Setting.SettingBL().GetSettingValue<string>("RootUrl") + Convert.ToString(new Domain.Setting.SettingBL().GetSettingValue<string>("OrderHistoryURL"))
            });

            notificationDTO.AdditionalParameters.Add(new Eduegate.Services.Contracts.Commons.KeyValueParameterDTO()
            {
                ParameterName = Eduegate.Services.Contracts.Constants.EmailNotificationType.OrderDispatch.Keys.AirwayBillNo,
                ParameterValue = airwayBillNO
            });

            notificationDTO.AdditionalParameters.Add(new Eduegate.Services.Contracts.Commons.KeyValueParameterDTO()
            {
                ParameterName = Eduegate.Services.Contracts.Constants.EmailNotificationType.OrderDispatch.Keys.JobEntryHeadID,
                ParameterValue = jobEntryHeadId.ToString()
            });

            notificationDTO.AdditionalParameters.Add(new Eduegate.Services.Contracts.Commons.KeyValueParameterDTO()
            {
                ParameterName = Eduegate.Services.Contracts.Constants.EmailNotificationType.OrderDispatch.Keys.ServiceProviderID,
                ParameterValue = serviceProviderID.ToString()
            });

            notificationDTO = GetAdditionalNotificationParameters(notificationDTO, _context);
            notificationDTO.FromEmailID = new Domain.Setting.SettingBL().GetSettingValue<string>("FromAddress");

            ClientFactory.NotificationServiceClient(_context).SaveEmailData(notificationDTO);
        }

        private EmailNotificationDTO GetAdditionalNotificationParameters(EmailNotificationDTO notificationDTO, CallContext context)
        {
            if (!notificationDTO.AdditionalParameters.Any(a => a.ParameterName.Equals("SiteID")))
            {
                notificationDTO.AdditionalParameters.Add(new Eduegate.Services.Contracts.Commons.KeyValueParameterDTO()
                {
                    ParameterName = "SiteID",
                    ParameterValue = new Domain.Setting.SettingBL().GetSettingValue<string>("SiteID")
                });
            }

            if (!notificationDTO.AdditionalParameters.Any(a => a.ParameterName.Equals("CompanyID")))
            {
                notificationDTO.AdditionalParameters.Add(new Eduegate.Services.Contracts.Commons.KeyValueParameterDTO()
                {
                    ParameterName = "CompanyID",
                    ParameterValue = new Domain.Setting.SettingBL().GetSettingValue<string>("CompanyID")
                });
            }

            notificationDTO.AdditionalParameters.Add(new Eduegate.Services.Contracts.Commons.KeyValueParameterDTO()
            {
                ParameterName = "Logo",
                ParameterValue = new Domain.Setting.SettingBL().GetSettingValue<string>("RootUrl") + "/Images/" + new Domain.Setting.SettingBL().GetSettingValue<string>("Logo")
            });

            notificationDTO.AdditionalParameters.Add(new Eduegate.Services.Contracts.Commons.KeyValueParameterDTO()
            {
                ParameterName = "LanguageCode",
                ParameterValue = _context.LanguageCode
            });

            return notificationDTO;
        }
    }
}