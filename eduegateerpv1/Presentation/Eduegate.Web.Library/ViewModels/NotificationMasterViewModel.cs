using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

using Eduegate.Framework.Mvc.Attributes;
using Eduegate.Framework.Translator;
using Eduegate.Services.Contracts;

namespace Eduegate.Web.Library.ViewModels
{
    [ContainerType(Framework.Enums.ContainerTypes.Tab, "NotificationDetails", "CRUDModel.ViewModel")]
    [DisplayName("Notification Details")]
    public class NotificationMasterViewModel : BaseMasterViewModel
    {
        
        [ControlType(Framework.Enums.ControlTypes.Label)]
        [DisplayName("Notification ID")]
        public long NotificationQueueIID { get; set; }

        [ControlType(Framework.Enums.ControlTypes.Label)]
        [DisplayName("Notification Type")]
        public string NotificationTypeName { get; set; }

        [Required]
        [ControlType(Framework.Enums.ControlTypes.DropDown)]
        [LookUp("LookUps.NotificationStatus")]
        [DisplayName("Notification Status")]
        public string NotificationStatusID { get; set; }


        [ControlType(Framework.Enums.ControlTypes.Label)]
        [DisplayName("Email To")]
        public string ToEmailID { get; set; }


        [ControlType(Framework.Enums.ControlTypes.Label)]
        [DisplayName("Email From")]
        public string FromEmailID { get; set; }

        
        //[Required]
        //[ControlType(Framework.Enums.ControlTypes.DropDown)]
        //[LookUp("LookUps.CustomerStatus")]
        //[DisplayName("Status")]
        //public string StatusID { get; set; }

        //public Eduegate.Services.Contracts.Enums.CustomerStatus? Status
        //{
        //    get
        //    {
        //        if (string.IsNullOrEmpty(StatusID))
        //            return Eduegate.Services.Contracts.Enums.CustomerStatus.Active;
        //        else
        //            return (Eduegate.Services.Contracts.Enums.CustomerStatus)int.Parse(StatusID);
        //    }

        //    set
        //    {
        //        StatusID = ((int)value).ToString();
        //    }
        //}
             

        public static NotificationMasterViewModel FromDTO(NotificationDTO dto)
        {
            Mapper<NotificationDTO, NotificationMasterViewModel>.CreateMap();
            return Mapper<NotificationDTO, NotificationMasterViewModel>.Map(dto);
        }

        public static NotificationMasterViewModel FromEmailNotificationDTO(Services.Contracts.Notifications.EmailNotificationDTO dto)
        {
            Mapper<Services.Contracts.Notifications.EmailNotificationDTO, NotificationMasterViewModel>.CreateMap();
            return Mapper<Services.Contracts.Notifications.EmailNotificationDTO, NotificationMasterViewModel>.Map(dto);
        }

        public static NotificationDTO ToDTO(NotificationMasterViewModel vm)
        {
            Mapper<NotificationMasterViewModel, NotificationDTO>.CreateMap();
            return Mapper<NotificationMasterViewModel, NotificationDTO>.Map(vm);
        }
    }
}
