using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Eduegate.Domain.Entity.Models;
using Eduegate.Framework;
using Eduegate.Framework.Interfaces;
using Eduegate.Services.Contracts;

namespace Eduegate.Domain.Mappers
{
    public class UserJobApplicationMapper : IDTOEntityMapper<UserJobApplicationDTO, UserJobApplication>
    {
        private CallContext _context;

        public static UserJobApplicationMapper Mapper(CallContext context)
        {
            var mapper = new UserJobApplicationMapper();
            mapper._context = context;
            return mapper;
        }

        public UserJobApplicationDTO ToDTO(UserJobApplication entity)
        {
            throw new NotImplementedException();
        }

        public UserJobApplication ToEntity(UserJobApplicationDTO dto)
        {
            var entity = new UserJobApplication()
            {
                CreatedBy = _context.LoginID != null ? int.Parse(_context.LoginID.ToString()) : 0,
                CreatedDate = DateTime.Now,
                CultureID = dto.CultureID,
                IPAddress = dto.IPAddress,
                Name = dto.Name,
                Telephone = dto.Telephone,
                Email = dto.Email,
                JobID = dto.JobID,
                Resume = dto.Resume,
                UpdatedDate = DateTime.Now
            };
            return entity;
        }
    }
}
