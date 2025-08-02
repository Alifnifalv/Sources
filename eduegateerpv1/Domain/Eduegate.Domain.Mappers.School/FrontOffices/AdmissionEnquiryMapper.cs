using Newtonsoft.Json;
using Eduegate.Domain.Entity.School.Models;
using Eduegate.Domain.Entity.School.Models.School;
using Eduegate.Frameworks.DynamicEntityMapper.Mappers;
using Eduegate.Framework.Contracts.Common;
using Eduegate.Services.Contracts.School.FrontOffices;
using Eduegate.Framework;
using System;
using System.Linq;
using Microsoft.EntityFrameworkCore;

namespace Eduegate.Domain.Mappers.School.FrontOffices
{
    public class AdmissionEnquiryMapper : DTOEntityDynamicMapper
    {
        public static AdmissionEnquiryMapper Mapper(CallContext context)
        {
            var mapper = new AdmissionEnquiryMapper();
            mapper._context = context;
            return mapper;
        }

        public override BaseMasterDTO ToDTO(string entity)
        {
            return JsonConvert.DeserializeObject<AdmissionEnquiryDTO>(entity);
        }

        public override string ToDTOString(BaseMasterDTO dto)
        {
            return dto == null ? null : JsonConvert.SerializeObject(dto);
        }

        public override string GetEntity(long IID)
        {
            return ToDTOString(ToDTO(IID));
        }

        private AdmissionEnquiryDTO ToDTO(long IID)
        {
            using (var dbContext = new dbEduegateSchoolContext())
            {
                var entity = dbContext.AdmissionEnquiries.Where(x => x.AdmissionEnquiryIID == IID)
                    .AsNoTracking()
                    .FirstOrDefault();

                return new AdmissionEnquiryDTO()
                {
                    AdmissionEnquiryIID = entity.AdmissionEnquiryIID,
                    PersonName = entity.PersonName,
                    PhoneNumber = entity.PhoneNumber,
                    Email = entity.Email,
                    Address = entity.Address,
                    Description = entity.Description,
                    Note = entity.Note,
                    Date = entity.Date,
                    NextFollowUpDate = entity.NextFollowUpDate,
                    Assinged = entity.Assinged,
                    ReferenceTypeID = entity.ReferenceTypeID,
                    SourceID = entity.SourceID,
                    ClassID = entity.ClassID,
                    NumberOfChild = entity.NumberOfChild,
                    CreatedDate = entity.CreatedDate,
                    UpdatedDate = entity.UpdatedDate,
                    CreatedBy = entity.CreatedBy,
                    UpdatedBy = entity.UpdatedBy,
                    //TimeStamps = entity.TimeStamps == null ? null : Convert.ToBase64String(entity.TimeStamps),
                    SchoolID = entity.SchoolID,
                };
            }
        }

        public override string SaveEntity(BaseMasterDTO dto)
        {
            var toDto = dto as AdmissionEnquiryDTO;
            //convert the dto to entity and pass to the repository.
            var entity = new AdmissionEnquiry()
            {
                AdmissionEnquiryIID = toDto.AdmissionEnquiryIID,
                PersonName = toDto.PersonName,
                PhoneNumber = toDto.PhoneNumber,
                Email = toDto.Email,
                Address = toDto.Address,
                Description = toDto.Description,
                Note = toDto.Note,
                Date = toDto.Date,
                NextFollowUpDate = toDto.NextFollowUpDate,
                Assinged = toDto.Assinged,
                ReferenceTypeID = toDto.ReferenceTypeID,
                SourceID = toDto.SourceID,
                ClassID = toDto.ClassID,
                NumberOfChild = toDto.NumberOfChild,
                CreatedBy = toDto.AdmissionEnquiryIID == 0 ? (int)_context.LoginID : dto.CreatedBy,
                UpdatedBy = toDto.AdmissionEnquiryIID > 0 ? (int)_context.LoginID : dto.UpdatedBy,
                CreatedDate = toDto.AdmissionEnquiryIID == 0 ? DateTime.Now : dto.CreatedDate,
                UpdatedDate = toDto.AdmissionEnquiryIID > 0 ? DateTime.Now : dto.UpdatedDate,
                //TimeStamps = toDto.TimeStamps == null ? null : Convert.FromBase64String(toDto.TimeStamps),
                SchoolID = toDto.SchoolID,
            };

            using (var dbContext = new dbEduegateSchoolContext())
            {
                dbContext.AdmissionEnquiries.Add(entity);
                if (entity.AdmissionEnquiryIID == 0)
                {
                    dbContext.Entry(entity).State = Microsoft.EntityFrameworkCore.EntityState.Added;
                }
                else
                {
                    dbContext.Entry(entity).State = Microsoft.EntityFrameworkCore.EntityState.Modified;
                }
                dbContext.SaveChanges();

            }

            return ToDTOString(ToDTO(entity.AdmissionEnquiryIID));
        }

    }
}