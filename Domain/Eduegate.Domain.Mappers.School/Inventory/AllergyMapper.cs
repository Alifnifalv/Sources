using Eduegate.Domain.Entity;
using Eduegate.Domain.Entity.Models;
using Eduegate.Domain.Entity.School.Models.School;
using Eduegate.Domain.Repository;
using Eduegate.Domain.Repository.Frameworks;
using Eduegate.Framework;
using Eduegate.Framework.Contracts.Common;
using Eduegate.Framework.Extensions;
using Eduegate.Frameworks.DynamicEntityMapper.Mappers;
using Eduegate.Services.Contracts.Catalog;
using Eduegate.Services.Contracts.School.Inventory;
using Eduegate.Services.Contracts.School.Transports;
using Microsoft.EntityFrameworkCore;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;

namespace Eduegate.Domain.Mappers.School.Inventory
{
    public class AllergyMapper : DTOEntityDynamicMapper
    {
        public static AllergyMapper Mapper(CallContext context)
        {
            var mapper = new AllergyMapper();
            mapper._context = context;
            return mapper;
        }
        public override BaseMasterDTO ToDTO(string entity)
        {
            return JsonConvert.DeserializeObject<AllergyDTO>(entity);
        }

        public override string ToDTOString(BaseMasterDTO dto)
        {
            return dto == null ? null : JsonConvert.SerializeObject(dto);
        }

        public override string GetEntity(long IID)
        {
            return ToDTOString(ToDTO(IID));
        }
        public AllergyDTO ToDTO(long IID)
        {
            using (var dbContext = new dbEduegateERPContext())
            {
                var entity = dbContext.Allergies.AsNoTracking().FirstOrDefault(x => x.AllergyID == IID);

                return new AllergyDTO()
                {
                    AllergyID = entity.AllergyID,
                    AllergyName = entity.AllergyName,
                    IsActive = entity.IsActive,
                    CreatedBy = entity.CreatedBy,
                    UpdatedBy = entity.UpdatedBy,
                    CreatedDate = entity.CreatedDate,
                    UpdatedDate = entity.UpdatedDate,
                    //TimeStamps = Convert.ToBase64String(entity.TimeStamps),
                };

            }
        }

        public override string SaveEntity(BaseMasterDTO dto)
        {
            var toDto = dto as AllergyDTO;
            //convert the dto to entity and pass to the repository.
            using (var dbContext = new dbEduegateERPContext())
            {
                var entity = new Allergy()
                {
                    AllergyID = toDto.AllergyID,
                    AllergyName = toDto.AllergyName,
                    IsActive = toDto.IsActive,
                    CreatedBy = toDto.AllergyID == 0 ? (int)_context.LoginID : dto.CreatedBy,
                    UpdatedBy = toDto.AllergyID > 0 ? (int)_context.LoginID : dto.UpdatedBy,
                    CreatedDate = toDto.AllergyID == 0 ? DateTime.Now : dto.CreatedDate,
                    UpdatedDate = toDto.AllergyID > 0 ? DateTime.Now : dto.UpdatedDate,
                    //TimeStamps = toDto.TimeStamps == null ? null : Convert.FromBase64String(toDto.TimeStamps),
                };

                if (entity.AllergyID == 0)
                {
                    var maxGroupID = dbContext.Allergies.OrderByDescending(a => a.AllergyID).AsNoTracking().FirstOrDefault()?.AllergyID;
                    entity.AllergyID = (byte)(maxGroupID == null || maxGroupID == 0 ? 1 : byte.Parse(maxGroupID.ToString()) + 1);

                    dbContext.Entry(entity).State = Microsoft.EntityFrameworkCore.EntityState.Added;
                }
                else
                {
                    dbContext.Entry(entity).State = Microsoft.EntityFrameworkCore.EntityState.Modified;
                }
                dbContext.SaveChanges();

                return ToDTOString(ToDTO(entity.AllergyID));
            }
        }

        public List<KeyValueDTO> GetAllergies()
        {
            using (dbEduegateERPContext dbContext = new dbEduegateERPContext())
            {
                var allergies = new List<KeyValueDTO>();

                var allergy = dbContext.Allergies.AsNoTracking().ToList();

                allergies = allergy.Select(x => new KeyValueDTO()
                {
                    Key = x.AllergyID.ToString(),
                    Value = x.AllergyName,
                }).ToList();

                return allergies;
            }
        }

        public bool SaveAllergies(long studentID, int allergyID, byte SeverityID)
        {
            var entity = new AllergyStudentMap();
            using (dbEduegateERPContext dbContext = new dbEduegateERPContext())
            {
                var student = dbContext.Students.FirstOrDefault(a => a.StudentIID == studentID);
                var allergyStudentMap = dbContext.AllergyStudentMaps.FirstOrDefault(a => a.StudentID == studentID && a.AllergyID == allergyID);

                entity = new AllergyStudentMap()
                {
                    AllergyStudentMapIID = allergyStudentMap == null ? 0 : allergyStudentMap.AllergyStudentMapIID,
                    AllergyID = allergyID,
                    StudentID = studentID,
                    SeverityID = SeverityID,
                    SchoolID = student?.SchoolID,
                    AcademicYearID = student?.AcademicYearID,
                    CreatedDate = allergyStudentMap == null ? DateTime.Now : allergyStudentMap.CreatedDate,
                    UpdatedDate = allergyStudentMap?.AllergyStudentMapIID > 0 ? DateTime.Now : (DateTime?)null,
                };
            }

            using (dbEduegateERPContext dbContext1 = new dbEduegateERPContext())
            {
                if (entity.AllergyStudentMapIID == 0)
                {
                    dbContext1.Entry(entity).State = Microsoft.EntityFrameworkCore.EntityState.Added;
                }
                else
                {
                    dbContext1.Entry(entity).State = Microsoft.EntityFrameworkCore.EntityState.Modified;
                }

                dbContext1.SaveChanges();
            }

            return true;
        }

        public List<AllergyStudentDTO> GetStudentAllergies(long loginID)
        {
            var dateFormat = Eduegate.Framework.Extensions.ConfigurationExtensions.GetAppConfigValue("DateFormat");

            using (var dbContext = new dbEduegateERPContext())
            {
                var login = dbContext.Parents.FirstOrDefault(x => x.LoginID == loginID);

                var students = dbContext.Students.Where(x => x.ParentID == login.ParentIID).ToList();

                var studentsAllergies = new List<AllergyStudentDTO>();

                foreach (var stud in students)
                {
                    var allergyDTOs = new List<AllergyDTO>();

                    var student = dbContext.AllergyStudentMaps.FirstOrDefault(x => x.StudentID == stud.StudentIID);

                    var allergyMaps = dbContext.AllergyStudentMaps.Where(x => x.StudentID == stud.StudentIID)
                        .Include(i => i.Allergy)
                        .Include(i => i.Severity)
                        .ToList();

                    foreach (var map in allergyMaps)
                    {
                        allergyDTOs.Add(new AllergyDTO()
                        {
                            AllergyID = (int)map.AllergyID,
                            AllergyName = map.Allergy?.AllergyName,
                            AllergySeverityName = map.Severity?.SeverityName,
                        });

                    }

                    studentsAllergies.Add(new AllergyStudentDTO()
                    {
                        StudentID = stud.StudentIID,
                        Allergies = allergyDTOs,
                    });
                }

                return studentsAllergies;
            }
        }

        public List<AllergyStudentDTO> CheckStudentAllergy(long studentID , long loginID , long cartID)
        {

            using (var dbContext = new dbEduegateERPContext())
            {
                var registeredListDTOs = new List<AllergyStudentDTO>();

                var allergies = dbContext.AllergyStudentMaps.Where(x => x.StudentID == studentID).AsNoTracking().FirstOrDefault();

                var carts = dbContext.ShoppingCarts.Where(x => x.ShoppingCartIID == cartID).AsNoTracking().FirstOrDefault();

                var cartsItems = dbContext.ShoppingCartItems.Where(x => x.ShoppingCartID == carts.ShoppingCartIID).AsNoTracking().ToList();

                foreach (var items in cartsItems)
                { 
                    var product = dbContext.ProductSKUMaps.Where(x => x.ProductSKUMapIID == items.ProductSKUMapID)
                        .Include(x=>x.Product).AsNoTracking().FirstOrDefault();

                    var productAllergies = dbContext.ProductAllergyMaps.Where(x => x.ProductID == product.ProductID).AsNoTracking().ToList();

                     foreach (var productAllergy in productAllergies)
                    {
               
                        if (allergies != null && allergies.StudentID == studentID)
                        {

                            var studentAllergy = dbContext.AllergyStudentMaps.Where(x => x.StudentID == studentID)
                                .Include(i=>i.Allergy)
                                .AsNoTracking().FirstOrDefault();

                            if (studentAllergy.AllergyID == productAllergy.AllergyID && studentAllergy.SeverityID != 1)
                            {
                                registeredListDTOs.Add(new AllergyStudentDTO()
                                {
                                    StudentID = studentAllergy.StudentID,
                                    AllergyID = studentAllergy.AllergyID,
                                    AllergyName = studentAllergy.Allergy.AllergyName,
                                    ProductID = product.ProductID,
                                    ProductName = product.Product.ProductName,
                                });
                            }

                        }
                    }
                }

                return registeredListDTOs;

            }
        }
        public List<KeyValueDTO> GetSeverity()
        {
            using (dbEduegateERPContext dbContext = new dbEduegateERPContext())
            {
                var Severity = new List<KeyValueDTO>();

                var severity = dbContext.Severities.ToList();

                Severity = severity.Select(x => new KeyValueDTO()
                {
                    Key = x.SeverityID.ToString(),
                    Value = x.SeverityName,
                }).ToList();

                return Severity;
            }
        }
    }
}