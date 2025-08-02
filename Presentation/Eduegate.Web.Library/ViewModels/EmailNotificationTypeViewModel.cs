using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Eduegate.Framework.Extensions;
using Eduegate.Framework.Translator;
using Eduegate.Services.Contracts.Catalog;
using Eduegate.Services.Contracts.Notifications;

namespace Eduegate.Web.Library.ViewModels
{
    public class EmailNotificationTypeViewModel : BaseMasterViewModel
    {
        public int EmailNotificationTypeID { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public string EmailTemplateFilePath { get; set; }
        public string EmailSubject { get; set; }
        public Nullable<System.DateTime> CreatedDate { get; set; }
        public Nullable<System.DateTime> ModifiedDate { get; set; }
        public byte[] TimeStamp { get; set; }
        public string CreatedBy { get; set; }
        public string ModifiedBy { get; set; }
        public string ToCCEmailID { get; set; }
        public string ToBCCEmailID { get; set; }

        // Mappers
        public static EmailNotificationTypeDTO ToDTO(EmailNotificationTypeViewModel vm)
        {
            Mapper<EmailNotificationTypeViewModel, EmailNotificationTypeDTO>.CreateMap();
            var mapper = Mapper<EmailNotificationTypeViewModel, EmailNotificationTypeDTO>.Map(vm);
            return mapper;
        }

        public static EmailNotificationTypeViewModel ToVM(EmailNotificationTypeDTO dto)
        {
            Mapper<EmailNotificationTypeDTO, EmailNotificationTypeViewModel>.CreateMap();
            return Mapper<EmailNotificationTypeDTO, EmailNotificationTypeViewModel>.Map(dto);

        }
    }

}
