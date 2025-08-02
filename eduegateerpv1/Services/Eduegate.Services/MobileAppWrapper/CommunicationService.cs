using Eduegate.Services.Contracts.Communications;
using Eduegate.Framework.Contracts.Common.Enums;
using Eduegate.Framework.Services;
using Eduegate.Framework;
using Eduegate.Domain.Mappers.School.Mutual;
using Eduegate.Domain.Mappers.School.Students;
using Eduegate.Framework.Contracts.Common;
using Eduegate.Services.Contracts.Commons;
using Eduegate.Services.Contracts.Mutual;
using Eduegate.Domain.Mappers;
using Eduegate.Domain;
using System.ServiceModel;
using Eduegate.Services.Contracts.School.Students;
using Eduegate.Services.Contracts.Payroll;
using Eduegate.Domain.Mappers.School.School;

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
                return new MutualBL(CallContext).SaveComment(comment);
            }
            catch (Exception exception)
            {
                Eduegate.Logger.LogHelper<MutualBL>.Fatal(exception.Message, exception);
                throw new FaultException("Internal server, please check with your administrator");
            }
        }


        public List<EmployeeDTO> GetTeacherEmailByParentLoginID()
        {
            return ClassTeacherMapMapper.Mapper(CallContext).GetStudentTeacherEmailByParentLoginID(CallContext.LoginID.Value);
        }

        


        public List<CommentDTO> GetChats(Eduegate.Infrastructure.Enums.EntityTypes entityType, long? fromLoginID, long? toLoginID)
        {
            return new MutualBL(CallContext).GetChats( entityType,  fromLoginID, toLoginID);
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

        public List<EmployeeDTO> GetTeachersWhoMessagedByParentLoginID()
        {
            return ClassTeacherMapMapper.Mapper(CallContext).GetTeachersWhoMessagedByParentLoginID(CallContext.LoginID.Value);
        }

        public GuardianDTO GetParentDetailsByLoginID(long loginID)
        {
            return ParentMapper.Mapper(CallContext).GetParentDetailsByLoginID(loginID);
        }
    }
}
