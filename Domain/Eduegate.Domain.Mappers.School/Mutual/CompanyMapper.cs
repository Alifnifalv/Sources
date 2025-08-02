using Newtonsoft.Json;
using Eduegate.Domain.Entity.School.Models;
using Eduegate.Domain.Entity.School.Models.School;
using Eduegate.Domain.Repository.Frameworks;
using Eduegate.Frameworks.DynamicEntityMapper.Mappers;
using Eduegate.Framework.Contracts.Common;
using Eduegate.Services.Contracts.School.Mutual;
using Eduegate.Framework;
using System;
using System.Linq;
using Microsoft.EntityFrameworkCore;

namespace Eduegate.Domain.Mappers.School.Mutual
{
    public class CompanyMapper : DTOEntityDynamicMapper
    {   
        public static  CompanyMapper Mapper(CallContext context)
        {
            var mapper = new  CompanyMapper();
            mapper._context = context;
            return mapper;
        }

        public override BaseMasterDTO ToDTO(string entity)
        {
            return JsonConvert.DeserializeObject<CompanyDTO>(entity);
        }

        public override string ToDTOString(BaseMasterDTO dto)
        {
            return dto == null ? null : JsonConvert.SerializeObject(dto);
        }

        public override string GetEntity(long IID)
        {
             return ToDTOString(ToDTO(IID));
        }

        private CompanyDTO ToDTO(long IID)
        {
            using (var dbContext = new dbEduegateSchoolContext())
            {
                var entity = dbContext.Companies.Where(a => a.CompanyID == IID)
                    .AsNoTracking()
                    .FirstOrDefault();

                return new CompanyDTO()
                {
                    CompanyID = entity.CompanyID,
                    CompanyName = entity.CompanyName,
                    CompanyGroupID = entity.CompanyGroupID,
                    CountryID = entity.CountryID,
                    BaseCurrencyID = entity.BaseCurrencyID,
                    LanguageID = entity.LanguageID,
                    RegistraionNo = entity.RegistraionNo,
                    RegistrationDate = entity.RegistrationDate,
                    ExpiryDate = entity.ExpiryDate,
                    Address = entity.Address,
                    CreatedDate = entity.CreatedDate,
                    UpdatedDate = entity.UpdatedDate,
                    CreatedBy = entity.CreatedBy,
                    UpdatedBy = entity.UpdatedBy,
                    //TimeStamps = entity.TimeStamps == null ? null : Convert.ToBase64String(entity.TimeStamps),
                    StatusID = entity.StatusID,
                };
            }
        }

        public override string SaveEntity(BaseMasterDTO dto)
        {
            var toDto = dto as CompanyDTO;
            //convert the dto to entity and pass to the repository.
            var entity = new Company()
            {
                CompanyID = toDto.CompanyID,
                CompanyName = toDto.CompanyName,
                CompanyGroupID = toDto.CompanyGroupID,
                CountryID = toDto.CountryID,
                BaseCurrencyID = toDto.BaseCurrencyID,
                LanguageID = toDto.LanguageID,
                RegistraionNo = toDto.RegistraionNo,
                RegistrationDate = toDto.RegistrationDate,
                ExpiryDate = toDto.ExpiryDate,
                Address = toDto.Address,
                StatusID = toDto.StatusID,
                CreatedBy = toDto.CompanyID == 0 ? (int)_context.LoginID : dto.CreatedBy,
                UpdatedBy = toDto.CompanyID > 0 ? (int)_context.LoginID : dto.UpdatedBy,
                CreatedDate = toDto.CompanyID == 0 ? DateTime.Now : dto.CreatedDate,
                UpdatedDate = toDto.CompanyID > 0 ? DateTime.Now : dto.UpdatedDate,
                //TimeStamps = toDto.TimeStamps == null ? null : Convert.FromBase64String(toDto.TimeStamps),
            };

            using (var dbContext = new dbEduegateSchoolContext())
            {
               
                if (entity.CompanyID == 0)
                {                   
                    var maxGroupID = dbContext.Companies.Max(a => a.CompanyGroupID);
                    entity.CompanyID = maxGroupID == null ? 1 : int.Parse(maxGroupID.ToString()) + 1;
                    dbContext.Companies.Add(entity);
                    dbContext.Entry(entity).State = Microsoft.EntityFrameworkCore.EntityState.Added;
                }
                else
                {
                    dbContext.Companies.Add(entity);
                    dbContext.Entry(entity).State = Microsoft.EntityFrameworkCore.EntityState.Modified;
                }

                dbContext.SaveChanges();
            }

            return ToDTOString(ToDTO(entity.CompanyID ));
        }       
    }
}