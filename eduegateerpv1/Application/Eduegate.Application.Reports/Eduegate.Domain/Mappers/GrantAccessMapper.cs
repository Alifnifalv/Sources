using Eduegate.Domain.Entity.Models;
using Eduegate.Domain.Entity.School.Models.School;
using Eduegate.Domain.Repository.Frameworks;
using Eduegate.Framework;
using Eduegate.Framework.Contracts.Common;
using Eduegate.Framework.Extensions;
using Eduegate.Frameworks.DynamicEntityMapper.Mappers;
using Eduegate.Services.Contracts.Inventory;
using Eduegate.Services.Contracts.Security;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;

namespace Eduegate.Domain.Mappers
{
    public class GrantAccessMapper : DTOEntityDynamicMapper
    {
        public static GrantAccessMapper Mapper(CallContext context)
        {
            var mapper = new GrantAccessMapper();
            mapper._context = context;
            return mapper;
        }

        public override BaseMasterDTO ToDTO(string entity)
        {
            return JsonConvert.DeserializeObject<ClaimLoginMainMapDTO>(entity);
        }

        public override string ToDTOString(BaseMasterDTO dto)
        {
            return JsonConvert.SerializeObject(dto);
        }

        public override string GetEntity(long IID)
        {
            return ToDTOString(ToDTO(IID));
        }

        private ClaimLoginMainMapDTO ToDTO(long IID)
        {
            using (var dbContext = new dbEduegateERPContext())
            {
                var entity = dbContext.ClaimLoginMaps
                    .Where(X => X.LoginID == IID).ToList();

                var ClaimLoginMainDTO = new ClaimLoginMainMapDTO()
                {
                    //GalleryIID = entity.GalleryIID,
                    //SchoolID = entity.SchoolID,
                    //Description = entity.Description,
                    //GalleryDate = entity.GalleryDate,
                    //StartDate = entity.StartDate,
                    //ExpiryDate = entity.ExpiryDate,
                    //ISActive = entity.ISActive,
                    //AcademicYearID = entity.AcademicYearID,
                    //CreatedBy = entity.CreatedBy,
                    //CreatedDate = entity.CreatedDate,
                    //UpdatedBy = entity.UpdatedBy,
                    //UpdatedDate = entity.UpdatedDate,
                    //AcademicYear = entity.AcademicYearID.HasValue ? new KeyValueDTO() { Key = entity.AcademicYearID.Value.ToString(), Value = entity.AcademicYear.Description + " " + "( " + entity.AcademicYear.AcademicYearCode + ")" } : new KeyValueDTO(),
                };


                ClaimLoginMainDTO.LoginMaps = new List<ClaimLoginMapDTO>();

                //if (entity.Count > 0)
                //{
                //    foreach (var logins in entity)
                //    {
                //            GalleryDTO.GalleryAttachmentMaps.Add(new GalleryAttachmentMapDTO()
                //            {
                //                GalleryAttachmentMapIID = attachment.GalleryAttachmentMapIID,
                //                GalleryID = attachment.GalleryID.HasValue ? attachment.GalleryID : null,
                //                AttachmentContentID = attachment.AttachmentContentID,
                //                AttachmentName = attachment.ContentFile.ContentFileName,
                //                CreatedBy = attachment.CreatedBy,
                //                CreatedDate = attachment.CreatedDate,
                //            });
                //    }
                //}

                return ClaimLoginMainDTO;
            }
        }

        public override string SaveEntity(BaseMasterDTO dto)
        {
            var toDto = dto as ClaimLoginMainMapDTO;

            using (var dbContext = new dbEduegateERPContext())
            {
                var employee = dbContext.Employees.FirstOrDefault(a => a.EmployeeIID == toDto.AssociateTeacherID);

                var entity = new ClaimLoginMap();

                foreach (var logins in toDto.LoginMaps)
                {
                    entity = new ClaimLoginMap()
                    {
                        LoginID = employee.LoginID,
                        ClaimID = logins.ClaimID,
                        CreatedBy = logins.ClaimLoginMapIID == 0 ? (int)_context.LoginID : dto.CreatedBy,
                        UpdatedBy = logins.ClaimLoginMapIID > 0 ? (int)_context.LoginID : dto.UpdatedBy,
                        CreatedDate = logins.ClaimLoginMapIID == 0 ? DateTime.Now : dto.CreatedDate,
                        UpdatedDate = logins.ClaimLoginMapIID > 0 ? DateTime.Now : dto.UpdatedDate,
                        TimeStamps = logins.TimeStamps == null ? null : Convert.FromBase64String(toDto.TimeStamps),
                    };


                    //dbContext.ClaimLoginMaps.Add(logins);

                    if (entity.ClaimLoginMapIID == 0)
                    {
                        dbContext.Entry(entity).State = System.Data.Entity.EntityState.Added;
                    }
                    else
                    {
                        dbContext.Entry(entity).State = System.Data.Entity.EntityState.Modified;
                    }
                    dbContext.SaveChanges();
                }                

                return ToDTOString(ToDTO(toDto.AssociateTeacherID));
            }
        }

        public List<ClaimDTO> GetClaimsByLoginID(long? employeeID)
        {
            using (dbEduegateERPContext dbContext = new dbEduegateERPContext())
            {
                //var employee = dbContext.Employees.FirstOrDefault(a => a.EmployeeIID == employeeID);
                var loginID = _context.LoginID;
                var claimsDTO = new List<ClaimDTO>();

                var claims = ((from map in dbContext.ClaimSetLoginMaps
                               join clsetmap in dbContext.ClaimSetClaimMaps
                               on map.ClaimSetID equals clsetmap.ClaimSetID
                               where map.LoginID.Value == loginID && clsetmap.Claim.ClaimTypeID == 2
                               select clsetmap.Claim
                                  ).Union(
                                  (from map in dbContext.ClaimLoginMaps
                                   join clmap in dbContext.ClaimLoginMaps
                                  on map.LoginID equals clmap.LoginID
                                   where map.LoginID.Value == loginID
                                   select clmap.Claim
                                ))).ToList();

                foreach (var claim in claims)
                {
                    claimsDTO.Add(new ClaimDTO()
                    {
                        ClaimIID = claim.ClaimIID,
                        ClaimName = claim.ClaimName,
                        ClaimTypeID = claim.ClaimTypeID.IsNotNull() ? (Eduegate.Services.Contracts.Enums.ClaimType)Convert.ToInt32(claim.ClaimTypeID) : 0,
                        ResourceName = claim.ResourceName,
                        Rights = claim.Rights,
                        CreatedBy = claim.CreatedBy,
                        CreatedDate = claim.CreatedDate,
                        UpdatedBy = claim.UpdatedBy,
                        UpdatedDate = claim.UpdatedDate,
                        TimeStamps = claim.TimeStamps == null ? null : Convert.ToBase64String(claim.TimeStamps),
                        CompanyID = claim.CompanyID.IsNotNull() ? claim.CompanyID : _context.CompanyID,
                    });
                }

                return claimsDTO;
            }
        }
    }
}