using Newtonsoft.Json;
using Eduegate.Domain.Entity.School.Models;
using Eduegate.Domain.Entity.School.Models.School;
using Eduegate.Frameworks.DynamicEntityMapper.Mappers;
using Eduegate.Framework.Contracts.Common;
using Eduegate.Services.Contracts.School.Library;
using Eduegate.Framework;
using System;
using Eduegate.Domain.Repository;
using System.Collections.Generic;
using System.Linq;
using Microsoft.EntityFrameworkCore;

namespace Eduegate.Domain.Mappers.School.Library
{
    public class LibraryStaffRegisterMapper : DTOEntityDynamicMapper
    {
        List<string> validationFields = new List<string>() { "EmployeeID" };
        public static LibraryStaffRegisterMapper Mapper(CallContext context)
        {
            var mapper = new LibraryStaffRegisterMapper();
            mapper._context = context;
            return mapper;
        }

        public override BaseMasterDTO ToDTO(string entity)
        {
            return JsonConvert.DeserializeObject<LibraryStaffRegisterDTO>(entity);
        }

        public override string ToDTOString(BaseMasterDTO dto)
        {
            return dto == null ? null : JsonConvert.SerializeObject(dto);
        }

        public override string GetEntity(long IID)
        {
            return ToDTOString(ToDTO(IID));
        }

        private LibraryStaffRegisterDTO ToDTO(long IID)
        {
            using (var dbContext = new dbEduegateSchoolContext())
            {
                var entity = dbContext.LibraryStaffRegisters.Where(a => a.LibraryStaffResiterIID == IID)
                    .Include(i => i.Employee)
                    .AsNoTracking()
                    .FirstOrDefault();

                return new LibraryStaffRegisterDTO()
                {
                    LibraryStaffResiterIID = entity.LibraryStaffResiterIID,
                    EmployeeID = entity.EmployeeID,
                    LibraryCardNumber = entity.LibraryCardNumber,
                    RegistrationDate = entity.RegistrationDate,
                    Notes = entity.Notes,
                    StaffName = entity.Employee != null ? entity.Employee.FirstName + " " + entity.Employee.MiddleName + " " + entity.Employee.LastName : null,
                    AcademicYearID = entity.AcademicYearID,
                    SchoolID = entity.SchoolID,
                    CreatedBy = entity.CreatedBy,
                    CreatedDate = entity.CreatedDate,
                    UpdatedDate = entity.UpdatedDate,
                    UpdatedBy = entity.UpdatedBy
                };
            }
        }

        public override string SaveEntity(BaseMasterDTO dto)
        {
            var toDto = dto as LibraryStaffRegisterDTO;
            MutualRepository mutualRepository = new MutualRepository();
            Entity.Models.Settings.Sequence sequence = new Entity.Models.Settings.Sequence();
            var errorMessage = string.Empty;

            foreach (var field in validationFields)
            {
                var isValid = ValidateField(toDto, field);

                if (isValid.Key.Equals("true"))
                {
                    errorMessage = string.Concat(errorMessage, "", isValid.Value, "<br>");
                }
            }

            if (!string.IsNullOrEmpty(errorMessage))
            {
                throw new Exception(errorMessage);
            }

            if (toDto.LibraryStaffResiterIID == 0)
            {
                try

                {
                    sequence = mutualRepository.GetNextSequence("LibraryStaffCardNo", null);
                }
                catch (Exception ex)
                {
                    throw new Exception("Please generate sequence with 'LibraryStaffCardNo'");
                }
            }
            //convert the dto to entity and pass to the repository.
            var entity = new LibraryStaffRegister()
            {
                LibraryStaffResiterIID = toDto.LibraryStaffResiterIID,
                EmployeeID = toDto.EmployeeID,
                //LibraryCardNumber = toDto.LibraryCardNumber,
                LibraryCardNumber = toDto.LibraryStaffResiterIID == 0 ? sequence.Prefix + sequence.LastSequence : toDto.LibraryCardNumber,
                RegistrationDate = toDto.RegistrationDate,
                Notes = toDto.Notes,
                SchoolID = toDto.SchoolID == null ? (byte)_context.SchoolID : toDto.SchoolID,
                AcademicYearID = toDto.AcademicYearID == null ? (int)_context.AcademicYearID : toDto.AcademicYearID,
                CreatedBy = toDto.LibraryStaffResiterIID == 0 ? (int)_context.LoginID : dto.CreatedBy,
                UpdatedBy = toDto.LibraryStaffResiterIID > 0 ? (int)_context.LoginID : dto.UpdatedBy,
                CreatedDate = toDto.LibraryStaffResiterIID == 0 ? DateTime.Now : dto.CreatedDate,
                UpdatedDate = toDto.LibraryStaffResiterIID > 0 ? DateTime.Now : dto.UpdatedDate,
            };

            using (var dbContext = new dbEduegateSchoolContext())
            {
                dbContext.LibraryStaffRegisters.Add(entity);
                if (entity.LibraryStaffResiterIID == 0)
                {
                    dbContext.Entry(entity).State = Microsoft.EntityFrameworkCore.EntityState.Added;
                }
                else
                {
                    dbContext.Entry(entity).State = Microsoft.EntityFrameworkCore.EntityState.Modified;
                }
                dbContext.SaveChanges();
            }

            return ToDTOString(ToDTO(entity.LibraryStaffResiterIID));
        }

        public override KeyValueDTO ValidateField(BaseMasterDTO dto, string field)
        {
            var toDto = dto as LibraryStaffRegisterDTO;
            var valueDTO = new KeyValueDTO();

            switch (field)
            {
                case "EmployeeID":
                    if (!string.IsNullOrEmpty(toDto.EmployeeID.ToString()))
                    {
                        var hasDuplicated = IsStaffDuplicated(toDto.EmployeeID.ToString(), toDto.LibraryStaffResiterIID);
                        if (hasDuplicated)
                        {
                            valueDTO.Key = "true";
                            valueDTO.Value = "Staff already exists, Please try with different Staff.";
                        }
                        else
                        {
                            valueDTO.Key = "false";
                        }
                    }
                    else
                    {
                        valueDTO.Key = "false";
                    }
                    break;
            }
            return valueDTO;
        }

        public bool IsStaffDuplicated(string EmployeeID, long LibraryStaffResiterIID)
        {
            using (var db = new dbEduegateSchoolContext())
            {
                List<LibraryStaffRegister> registers;

                if (LibraryStaffResiterIID == 0)
                {
                    registers = db.LibraryStaffRegisters.Where(x => x.EmployeeID.ToString() == EmployeeID).AsNoTracking().ToList();
                }
                else
                {
                    registers = db.LibraryStaffRegisters.Where(x => x.LibraryStaffResiterIID != LibraryStaffResiterIID && x.EmployeeID.ToString() == EmployeeID).AsNoTracking().ToList();
                }

                if (registers.Count() >= 1)
                {
                    return true;
                }

                return false;
            }
        }

    }
}