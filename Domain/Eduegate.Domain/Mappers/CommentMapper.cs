

using System;
using Eduegate.Domain.Entity.Models;
using Eduegate.Framework;
using Eduegate.Framework.Contracts.Common.Enums;
using Eduegate.Framework.Extensions;
using Eduegate.Framework.Interfaces;
using Eduegate.Services.Contracts.Enums;
using Eduegate.Services.Contracts.Mutual;

namespace Eduegate.Domain.Mappers
{
    public class CommentMapper : IDTOEntityMapper<CommentDTO, Comment>
    {
        private CallContext _context;

        public static CommentMapper Mapper(CallContext context)
        {
            var mapper = new CommentMapper();
            mapper._context = context;
            return mapper;
        }

        public CommentDTO ToDTO(Comment entity)
        {
            if (entity != null)
            {
                var userInfo = new AccountBL(null).GetUserDetailsByID(Convert.ToInt64(entity.CreatedBy));
                string userName = string.Empty;

                if (userInfo.IsNotNull())
                {
                    if (userInfo.Employee.IsNotNull())
                    {
                        userName = userInfo.Employee.EmployeeName;
                    }
                    else if (userInfo.Parent.IsNotNull())
                    {
                        if (userInfo.Parent.GuardianFirstName.IsNotNull())
                        {
                            userName = userInfo.Parent.GuardianFirstName + " " + (userInfo.Parent.GuardianMiddleName ?? "") + " " + (userInfo.Parent.GuardianLastName ?? "");
                        }
                        else
                        {
                            userName = userInfo.Parent.FatherFirstName + " " + (userInfo.Parent.FatherMiddleName ?? "") + " " + (userInfo.Parent.FatherLastName ?? "");
                        }
                    }
                    else
                    {
                        userName = userInfo.LoginEmailID;
                    }
                }

                return new CommentDTO()
                {
                    CommentIID = entity.CommentIID,
                    ParentCommentID = entity.ParentCommentID,
                    CommentText = entity.Comment1.Replace("\n", "< br />"),
                    EntityType = (EntityTypes)Enum.Parse(typeof(EntityTypes), entity.EntityTypeID.ToString()),
                    ReferenceID = entity.ReferenceID,
                    Username = userName,
                    CreatedBy = Convert.ToInt32(entity.CreatedBy),
                    UpdatedBy = Convert.ToInt32(entity.UpdatedBy),
                    CreatedDate = entity.CreatedDate,
                    UpdatedDate = entity.UpdatedDate,
                    DepartmentID = entity.DepartmentID,
                    BroadcastID = entity.BroadcastID, // ✅ MAKE SURE THIS IS INCLUDED
                    FromLoginID = entity.FromLoginID,
                    ToLoginID = entity.ToLoginID,
                    IsRead = entity.IsRead,
                    PhotoContentID =  entity.PhotoContentID

                    //TimeStamps = entity.TimeStamps == null ? null : Convert.ToBase64String(entity.TimeStamps),
                };
            }
            else return new CommentDTO();
        }

        public Comment ToEntity(CommentDTO dto)
        {
            if (dto != null)
            {
                return new Comment()
                {
                    CommentIID = dto.CommentIID,
                    Comment1 = dto.CommentText,
                    ParentCommentID = dto.ParentCommentID,
                    EntityTypeID = Convert.ToInt16(dto.EntityType),
                    ReferenceID = dto.ReferenceID,
                    CreatedBy = dto.CommentIID > 0 ? dto.CreatedBy : (int)_context.LoginID,
                    UpdatedBy = dto.CommentIID > 0 ? (int)_context.LoginID : dto.UpdatedBy,
                    CreatedDate = dto.CommentIID > 0 ? dto.CreatedDate : DateTime.Now,
                    UpdatedDate = dto.CommentIID > 0 ? DateTime.Now : dto.UpdatedDate,
                    DepartmentID = dto.DepartmentID > 0 ? dto.DepartmentID : null,
                    ToLoginID =  dto.ToLoginID,  // Assuming referenceId is in route params
                    FromLoginID =  dto.FromLoginID,  // Assuming referenceId is in route params
                    PhotoContentID =  dto.PhotoContentID
                    //TimeStamps = dto.TimeStamps == null ? null : Convert.FromBase64String(dto.TimeStamps),
                };
            }
            else return new Comment();
        }
    }
}
