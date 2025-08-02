using System;
using Eduegate.Domain.Entity.Models;
using Eduegate.Services.Contracts;
using Eduegate.Framework.Interfaces;
using Eduegate.Framework;
using Eduegate.Framework.Extensions;


namespace Eduegate.Domain.Mappers
{
    public partial class JobStatusMapper : IDTOEntityMapper<JobStatusDto, JobStatus>
    {
        private CallContext _context;

        public static JobStatusMapper Mapper(CallContext context)
        {
            var mapper = new JobStatusMapper();
            mapper._context = context;
            return mapper;
        }
        public JobStatusDto ToDTO(JobStatus entity)
        {
            if (entity.IsNotNull())
            {
                var dto = new JobStatusDto()
                {
                    JobStatusID = entity.JobStatusID,
                    StatusName = entity.StatusName,
                };
                return dto;
            }
            else
            {
                return null;
            }
        }

        public JobStatus ToEntity(JobStatusDto dto)
        {
            throw new NotImplementedException();
        }
    }
}
