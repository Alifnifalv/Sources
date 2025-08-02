using Eduegate.Framework.Translator;
using Eduegate.Services.Contracts.Notifications;

namespace Eduegate.Notification.Email.ViewModels
{
    public class NotificationQueueViewModel
    {
        public int EmailNotificationQueueID { get; set; }
        public int NotificationTypeID { get; set; }

        public static NotificationQueueViewModel FromDTO(NotificationQueueDTO dto)
        {
            Mapper<NotificationQueueDTO, NotificationQueueViewModel>.CreateMap();
            return Mapper<NotificationQueueDTO, NotificationQueueViewModel>.Map(dto);
        }

        public static NotificationQueueDTO ToDTO(NotificationQueueViewModel vm)
        {
            Mapper<NotificationQueueViewModel, NotificationQueueDTO>.CreateMap();
            return Mapper<NotificationQueueViewModel, NotificationQueueDTO>.Map(vm);
        }
    }
}
