

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
                return new CommentDTO()
                {
                    CommentIID = entity.CommentIID,
                    ParentCommentID = entity.ParentCommentID,
                    CommentText = entity.Comment1.Replace("\n", "< br />"),
                    EntityType = (EntityTypes)Enum.Parse(typeof(EntityTypes), entity.EntityTypeID.ToString()),
                    ReferenceID = entity.ReferenceID,
                    Username = userInfo.IsNotNull() ? (userInfo.Customer.IsNotNull() ? userInfo.Customer.FirstName + " " + userInfo.Customer.LastName : userInfo.LoginEmailID) : string.Empty,
                    CreatedBy = Convert.ToInt32(entity.CreatedBy),
                    UpdatedBy = Convert.ToInt32(entity.UpdatedBy),
                    CreatedDate = entity.CreatedDate,
                    UpdatedDate = entity.UpdatedDate,
                    DepartmentID = entity.DepartmentID,
                    TimeStamps = entity.TimeStamps == null ? null : Convert.ToBase64String(entity.TimeStamps),
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
                    TimeStamps = dto.TimeStamps == null ? null : Convert.FromBase64String(dto.TimeStamps),
                };
            }
            else return new Comment();
        }
    }
}
