using Eduegate.Services.Contracts.Communications;
using Eduegate.Framework.Services;
using Eduegate.Framework;
using Eduegate.Domain.Mappers.School.Students;
using Eduegate.Services.Contracts.Mutual;
using Eduegate.Domain;
using System.ServiceModel;
using Eduegate.Services.Contracts.School.Students;
using Eduegate.Services.Contracts.Payroll;
using Eduegate.Domain.Mappers.School.School;
using Eduegate.Domain.Mappers.Notification.Notifications.Helpers;
using Eduegate.Domain.Mappers.Notification.Notifications;
using Microsoft.AspNetCore.Mvc;

namespace Eduegate.Services.MobileAppWrapper
{
    public class CommunicationService : BaseService, ICommunicationService
    {
        public CommunicationService(CallContext context)
        {
            CallContext = context;
        }
       
        public CommentDTO SaveComment(CommentDTO comment)
        {
            try
            {
                var commentsend =  new MutualBL(CallContext).SaveComment(comment);
                return commentsend;
            }
            catch (Exception exception)
            {
                Eduegate.Logger.LogHelper<MutualBL>.Fatal(exception.Message, exception);
                throw new FaultException("Internal server, please check with your administrator");
            }
        }


        public List<StudentChatDTO> GetTeacherEmailByParentLoginID()
        {
            return ClassTeacherMapMapper.Mapper(CallContext).GetStudentTeacherEmailByParentLoginID(CallContext.LoginID.Value);
        }

        


        public List<CommentDTO> GetChats(Eduegate.Infrastructure.Enums.EntityTypes entityType, long? fromLoginID, long? toLoginID, int page, int pageSize , int studentID)
        {
            return new MutualBL(CallContext).GetChats( entityType,  fromLoginID, toLoginID, page, pageSize, studentID);
        }

        public List<GuardianDTO> GetParentListByLoginID()
        {
            return ClassTeacherMapMapper.Mapper(CallContext).GetParentsByTeacherLoginID(CallContext.LoginID.Value);
        }

        //public bool MarkCommentAsRead(long commentId, long userId)
        //{
        //    try
        //    {
        //        using (var db = new dbEduegateSchoolContext())
        //        {
        //            var readStatus = db.CommentReadStatus
        //                .FirstOrDefault(crs => crs.CommentIID == commentId && crs.UserID == userId);

        //            if (readStatus != null)
        //            {
        //                readStatus.IsRead = true;
        //                readStatus.ReadTimestamp = DateTime.Now;
        //                db.SaveChanges();
        //                return true;
        //            }

        //            // If no existing read status, create a new entry
        //            db.CommentReadStatus.Add(new CommentReadStatus
        //            {
        //                CommentIID = commentId,
        //                UserID = userId,
        //                IsRead = true,
        //                ReadTimestamp = DateTime.Now
        //            });
        //            db.SaveChanges();
        //            return true;
        //        }
        //    }
        //    catch (Exception ex)
        //    {
        //        Eduegate.Logger.LogHelper<CommunicationService>.Fatal(ex.Message, ex);
        //        return false;
        //    }
        //}
        public List<GuardianDTO> GetActiveParentListByLoginID()
        {
            return ClassTeacherMapMapper.Mapper(CallContext).GetActiveParentListByLoginID(CallContext.LoginID.Value);
        }

        public List<StudentChatDTO> GetTeachersWhoMessagedByParentLoginID()
        {
            return ClassTeacherMapMapper.Mapper(CallContext).GetTeachersWhoMessagedByParentLoginID(CallContext.LoginID.Value);
        }

        public GuardianDTO GetParentDetailsByLoginID(long loginID)
        {
            return ParentMapper.Mapper(CallContext).GetParentDetailsByLoginID(loginID);
        }

        public bool? GetIsEnableCommunication(long loginID)
        {
            return ClassTeacherMapMapper.Mapper(CallContext).GetIsEnableCommunication(loginID);
        }

        public  Task<bool?> MarkEnableCommunication(long LoginID, bool enableCommunication)
        {
            return ClassTeacherMapMapper.Mapper(CallContext).MarkEnableCommunication(LoginID,  enableCommunication);
        }

        public Dictionary<string, List<GuardianDTO>> GetParentsByTeacherLoginIDGroupedByClass()
        {
            return ClassTeacherMapMapper.Mapper(CallContext).GetParentsByTeacherLoginIDGroupedByClass(CallContext.LoginID.Value);
        }


        public List<CommentDTO> SaveCommentsWithBroadcast(CommentDTO comment, List<BroadcastRecipientDTO> broadcastRecipients)
        {
            return new MutualBL(CallContext).SaveCommentsWithBroadcast(comment, broadcastRecipients);
        }


        public List<BroadCastDTO> SaveBroadcastList(List<BroadCastDTO> broadcastLoginIDs)
        {
            return new MutualBL(CallContext).SaveBroadcastList(broadcastLoginIDs);

        }

        public BroadCastDTO GetBroadcastDetailsById(long broadcastId)
        {
            return new MutualBL(CallContext).GetBroadcastDetailsById(broadcastId);

        }

        public List<BroadCastDetailsDTO> GetBroadcastDetailsByUserId(long userId)
        {
            return ClassTeacherMapMapper.Mapper(CallContext).GetBroadcastDetailsByUserId(userId);

        }

        public List<GuardianDTO> GetStudentsByBroadCastID(long broadcastId)
        {
            return new MutualBL(CallContext).GetStudentsByBroadCastID(broadcastId);

        }


        public List<BroadCastDTO> UpdateBroadcastList(List<BroadCastDTO> broadcastLoginIDs)
        {
            return new MutualBL(CallContext).UpdateBroadcastList(broadcastLoginIDs);

        }

        public async Task<string> SendPushNotifications(string title, string message, int claimSetID)
        {
            return await new NotificationBL(CallContext).SendPushNotifications(title, message, claimSetID);
        }

    }
}
