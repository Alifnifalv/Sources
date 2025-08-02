using Newtonsoft.Json;
using Eduegate.Domain.Entity.School.Models;
using Eduegate.Domain.Entity.School.Models.School;
using Eduegate.Frameworks.DynamicEntityMapper.Mappers;
using Eduegate.Framework.Contracts.Common;
using Eduegate.Services.Contracts.School.Library;
using Eduegate.Framework;
using System;
using Eduegate.Domain.Repository;
using System.Linq;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore;

namespace Eduegate.Domain.Mappers.School.Library
{
    public class LibraryStudentRegisterMapper : DTOEntityDynamicMapper
    {
        List<string> validationFields = new List<string>() { "StudentID" };
        public static LibraryStudentRegisterMapper Mapper(CallContext context)
        {
            var mapper = new LibraryStudentRegisterMapper();
            mapper._context = context;
            return mapper;
        }

        public override BaseMasterDTO ToDTO(string entity)
        {
            return JsonConvert.DeserializeObject<LibraryStudentRegisterDTO>(entity);
        }

        public override string ToDTOString(BaseMasterDTO dto)
        {
            return dto == null ? null : JsonConvert.SerializeObject(dto);
        }

        public override string GetEntity(long IID)
        {
            return ToDTOString(ToDTO(IID));
        }

        private LibraryStudentRegisterDTO ToDTO(long IID)
        {
            using (var dbContext = new dbEduegateSchoolContext())
            {
                var entity = dbContext.LibraryStudentRegisters.Where(a => a.LibraryStudentRegisterIID == IID)
                    .Include(i => i.Student)
                    .AsNoTracking()
                    .FirstOrDefault();

                return new LibraryStudentRegisterDTO()
                {
                    LibraryStudentRegisterIID = entity.LibraryStudentRegisterIID,
                    LibraryCardNumber = entity.LibraryCardNumber,
                    RegistrationDate = entity.RegistrationDate,
                    Notes = entity.Notes,
                    StudentID = entity.StudentID,
                    StudentName = entity.Student != null ? entity.Student.FirstName + " " + entity.Student.MiddleName + " " + entity.Student.LastName : null,
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

            var toDto = dto as LibraryStudentRegisterDTO;
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

            if (toDto.LibraryStudentRegisterIID == 0)
            {
                try
                {
                    sequence = mutualRepository.GetNextSequence("LibraryStudentCardNo", null);
                }
                catch (Exception ex)
                {
                    throw new Exception("Please generate sequence with 'LibraryStudentCardNo'");
                }
            }
            //convert the dto to entity and pass to the repository.
            var entity = new LibraryStudentRegister()
            {
                LibraryStudentRegisterIID = toDto.LibraryStudentRegisterIID,
                //LibraryCardNumber = toDto.LibraryCardNumber,
                LibraryCardNumber = toDto.LibraryStudentRegisterIID == 0 ? sequence.Prefix + sequence.LastSequence : toDto.LibraryCardNumber,
                RegistrationDate = toDto.RegistrationDate,
                Notes = toDto.Notes,
                StudentID = toDto.StudentID,
                SchoolID = toDto.SchoolID == null ? (byte)_context.SchoolID : toDto.SchoolID,
                AcademicYearID = toDto.AcademicYearID == null ? (int)_context.AcademicYearID : toDto.AcademicYearID,
                CreatedBy = toDto.LibraryStudentRegisterIID == 0 ? (int)_context.LoginID : dto.CreatedBy,
                UpdatedBy = toDto.LibraryStudentRegisterIID > 0 ? (int)_context.LoginID : dto.UpdatedBy,
                CreatedDate = toDto.LibraryStudentRegisterIID == 0 ? DateTime.Now : dto.CreatedDate,
                UpdatedDate = toDto.LibraryStudentRegisterIID > 0 ? DateTime.Now : dto.UpdatedDate,
            };

            using (var dbContext = new dbEduegateSchoolContext())
            {
                dbContext.LibraryStudentRegisters.Add(entity);
                if (entity.LibraryStudentRegisterIID == 0)
                {
                    dbContext.Entry(entity).State = Microsoft.EntityFrameworkCore.EntityState.Added;
                }
                else
                {
                    dbContext.Entry(entity).State = Microsoft.EntityFrameworkCore.EntityState.Modified;
                }
                dbContext.SaveChanges();
            }

            return ToDTOString(ToDTO(entity.LibraryStudentRegisterIID));
        }

        public override KeyValueDTO ValidateField(BaseMasterDTO dto, string field)
        {
            var toDto = dto as LibraryStudentRegisterDTO;
            var valueDTO = new KeyValueDTO();

            switch (field)
            {
                case "StudentID":
                    if (!string.IsNullOrEmpty(toDto.StudentID.ToString()))
                    {
                        var hasDuplicated = IsStudentDuplicated(toDto.StudentID.ToString(), toDto.LibraryStudentRegisterIID);
                        if (hasDuplicated)
                        {
                            valueDTO.Key = "true";
                            valueDTO.Value = "Student already exists, Please try with different Student.";
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

        public bool IsStudentDuplicated(string StudentID, long LibraryStudentRegisterIID)
        {
            using (var db = new dbEduegateSchoolContext())
            {
                List<LibraryStudentRegister> registers;

                if (LibraryStudentRegisterIID == 0)
                {
                    registers = db.LibraryStudentRegisters.Where(x => x.StudentID.ToString() == StudentID).AsNoTracking().ToList();
                }
                else
                {
                    registers = db.LibraryStudentRegisters.Where(x => x.LibraryStudentRegisterIID != LibraryStudentRegisterIID && x.StudentID.ToString() == StudentID).AsNoTracking().ToList();
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