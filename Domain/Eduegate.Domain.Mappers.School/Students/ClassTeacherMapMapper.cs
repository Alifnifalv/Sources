using Eduegate.Domain.Entity;
using Eduegate.Domain.Entity.School.Models;
using Eduegate.Domain.Entity.School.Models.School;
using Eduegate.Framework;
using Eduegate.Framework.Contracts.Common;
using Eduegate.Framework.Extensions;
using Eduegate.Frameworks.DynamicEntityMapper.Mappers;
using Eduegate.Services.Contracts.Communications;
using Eduegate.Services.Contracts.Mutual;
using Eduegate.Services.Contracts.Payroll;
using Eduegate.Services.Contracts.School.Students;
using Microsoft.EntityFrameworkCore;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Threading.Tasks;

namespace Eduegate.Domain.Mappers.School.Students
{
    public class ClassTeacherMapMapper : DTOEntityDynamicMapper
    {
        List<string> validationFields = new List<string>() { "ClassID" };

        public static ClassTeacherMapMapper Mapper(CallContext context)
        {
            var mapper = new ClassTeacherMapMapper();
            mapper._context = context;
            return mapper;
        }

        public override BaseMasterDTO ToDTO(string entity)
        {
            return JsonConvert.DeserializeObject<ClassClassTeacherMapDTO>(entity);
        }

        public override string ToDTOString(BaseMasterDTO dto)
        {
            return dto == null ? null : JsonConvert.SerializeObject(dto);
        }

        public override string GetEntity(long IID)
        {
            return ToDTOString(ToDTO(IID));
        }

        private ClassClassTeacherMapDTO ToDTO(long IID)
        {
            using (var dbContext = new dbEduegateSchoolContext())
            {
                var entity = dbContext.ClassClassTeacherMaps.Where(x => x.ClassClassTeacherMapIID == IID)
                    .Include(i => i.Class)
                    .Include(i => i.Section)
                    .Include(i => i.Employee)
                    .Include(i => i.Employee1)
                    .Include(i => i.ClassAssociateTeacherMaps).ThenInclude(i => i.Employee)
                    .Include(i => i.ClassTeacherMaps).ThenInclude(i => i.Employee2)
                    .Include(i => i.ClassTeacherMaps).ThenInclude(i => i.Subject)
                    .Include(i => i.ClassTeacherMaps).ThenInclude(i => i.Class)
                    .Include(i => i.ClassTeacherMaps).ThenInclude(i => i.Section)
                    .AsNoTracking()
                    .FirstOrDefault();

                var associate = new List<KeyValueDTO>();
                //ClassAssociateTeacherMap associateMap = null;
                var teacherMap = new ClassClassTeacherMapDTO()
                {
                    ClassClassTeacherMapIID = entity.ClassClassTeacherMapIID,
                    ClassTeacherID = entity.TeacherID,
                    CoordinatorID = entity.CoordinatorID,
                    HeadTeacherName = entity.TeacherID.HasValue ? entity.Employee1.EmployeeCode + " - " + entity.Employee1.FirstName + " " + entity.Employee1.MiddleName + " " + entity.Employee1.LastName : null,
                    CoordinatorName = entity.CoordinatorID.HasValue ? entity.Employee.EmployeeCode + " - " + entity.Employee.FirstName + " " + entity.Employee.MiddleName + " " + entity.Employee.LastName : null,
                    ClassID = entity.ClassID,
                    ClassName = entity.ClassID.HasValue ? entity.Class.ClassDescription : null,
                    SectionID = entity.SectionID,
                    OldSectionID = entity.SectionID,
                    SectionName = entity.SectionID.HasValue ? entity.Section.SectionName : null,
                    SchoolID = entity.SchoolID,
                    AcademicYearID = entity.AcademicYearID,
                    CreatedBy = entity.CreatedBy,
                    UpdatedBy = entity.UpdatedBy,
                    CreatedDate = entity.CreatedDate,
                    UpdatedDate = entity.UpdatedDate,
                };

                teacherMap.ClassAssociateTeacherMaps = new List<KeyValueDTO>();
                foreach (var associateTMap in entity.ClassAssociateTeacherMaps)
                {
                    teacherMap.ClassAssociateTeacherMaps.Add(new KeyValueDTO()
                    {
                        Key = associateTMap.TeacherID.ToString(),
                        Value = associateTMap.TeacherID.HasValue ? associateTMap.Employee.EmployeeCode + " - " + associateTMap.Employee.FirstName + " " + associateTMap.Employee.MiddleName + " " + associateTMap.Employee.LastName : null,

                    });
                }
                teacherMap.ClassTeacherMaps = new List<ClassTeacherMapDTO>();

                foreach (var teacher in entity.ClassTeacherMaps)
                {
                    teacherMap.ClassTeacherMaps.Add(new ClassTeacherMapDTO()
                    {
                        ClassTeacherMapIID = teacher.ClassTeacherMapIID,
                        ClassClassTeacherMapID = teacher.ClassClassTeacherMapID,
                        ClassID = teacher.ClassID,
                        ClassName = teacher.Class != null ? teacher.Class.ClassDescription : null,
                        SectionID = teacher.SectionID,
                        SectionName = teacher.Section != null ? teacher.Section.SectionName : null,
                        OtherTeacherID = teacher.TeacherID,
                        SubjectID = teacher.SubjectID,
                        OtherTeacherName = teacher.TeacherID.HasValue || teacher.Employee2 != null ? teacher.Employee2.EmployeeCode + " - " + teacher.Employee2.FirstName + " " + teacher.Employee2.MiddleName + " " + teacher.Employee2.LastName : null,
                        SubjectName = teacher.SubjectID.HasValue || teacher.Subject != null ? teacher.Subject.SubjectName : null,
                        SchoolID = teacher.SchoolID,
                        AcademicYearID = teacher.AcademicYearID,
                        CreatedBy = teacher.CreatedBy,
                        UpdatedBy = teacher.UpdatedBy,
                        CreatedDate = teacher.CreatedDate,
                        UpdatedDate = teacher.UpdatedDate,
                    });


                }
                return teacherMap;
            }
        }

        public override string SaveEntity(BaseMasterDTO dto)
        {
            var toDto = dto as ClassClassTeacherMapDTO;

            //var toDto = dto as ClassAssociateTeacherMapDTO;
            using (var dbContext = new dbEduegateSchoolContext())
            {
                if (toDto.ClassID == null || toDto.ClassID == 0)
                {
                    throw new Exception("Please Select class");
                }

                if (toDto.SectionID == null || toDto.SectionID == 0)
                {
                    throw new Exception("Please Select section");
                }

                if (toDto.ClassTeacherID == null || toDto.ClassTeacherID == 0)
                {
                    throw new Exception("Please Select Class Teacher");
                }

                if (toDto.OldSectionID.HasValue && toDto.OldSectionID != toDto.SectionID)
                {
                    throw new Exception("Can't change section from edit screen !");
                }

                //if (toDto.ClassTeacherMaps.Count <= 0 || toDto.ClassTeacherMaps == null)
                //{
                //    throw new Exception("Please fill Class Teachers Subject Map !");
                //}

                if (toDto.ClassClassTeacherMapIID == 0)
                {
                    //duplicate validation
                    var oldData = dbContext.ClassClassTeacherMaps.Where(x => x.ClassID == toDto.ClassID && x.SectionID == toDto.SectionID && x.AcademicYearID == _context.AcademicYearID).AsNoTracking().ToList();

                    if (oldData.Count > 0)
                    {
                        throw new Exception("The Same Class and Section already exists for this academic, Please try with different or use edit option");
                    }
                }

                foreach (var check in toDto.ClassTeacherMaps)
                {
                    if (check.SubjectID.HasValue && check.OtherTeacherID == null || check.OtherTeacherID == 0)
                    {
                        throw new Exception("Please select Teacher against the subject " + check.SubjectName);
                    }
                }

                //convert the dto to entity and pass to the repository.
                var entity = new ClassClassTeacherMap()
                {
                    ClassClassTeacherMapIID = toDto.ClassClassTeacherMapIID,
                    TeacherID = toDto.ClassTeacherID,
                    CoordinatorID = toDto.CoordinatorID,
                    SchoolID = toDto.SchoolID.HasValue ? toDto.SchoolID : Convert.ToByte(_context.SchoolID),
                    AcademicYearID = toDto.AcademicYearID.HasValue ? toDto.AcademicYearID : _context.AcademicYearID,
                    ClassID = toDto.ClassID,
                    SectionID = toDto.SectionID,
                    CreatedBy = toDto.ClassClassTeacherMapIID == 0 ? (int)_context.LoginID : dto.CreatedBy,
                    UpdatedBy = toDto.ClassClassTeacherMapIID > 0 ? (int)_context.LoginID : dto.UpdatedBy,
                    CreatedDate = toDto.ClassClassTeacherMapIID == 0 ? DateTime.Now.Date : dto.CreatedDate,
                    UpdatedDate = toDto.ClassClassTeacherMapIID > 0 ? DateTime.Now.Date : dto.UpdatedDate,
                    //TimeStamps = toDto.TimeStamps == null ? null : Convert.FromBase64String(toDto.TimeStamps),
                };

                var IIDs = toDto.ClassTeacherMaps
                    .Select(a => a.ClassTeacherMapIID).ToList();

                //delete maps
                var entities = dbContext.ClassTeacherMaps.Where(x =>
                    x.ClassClassTeacherMapID == entity.ClassClassTeacherMapIID &&
                    !IIDs.Contains(x.ClassTeacherMapIID)).AsNoTracking().ToList();

                if (entities.IsNotNull())
                    dbContext.ClassTeacherMaps.RemoveRange(entities);

                entity.ClassTeacherMaps = new List<ClassTeacherMap>();
                foreach (var teacherMap in toDto.ClassTeacherMaps)
                {
                    entity.ClassTeacherMaps.Add(new ClassTeacherMap()
                    {
                        ClassTeacherMapIID = teacherMap.ClassTeacherMapIID,
                        ClassClassTeacherMapID = toDto.ClassClassTeacherMapIID,
                        ClassID = toDto.ClassID,
                        SectionID = toDto.SectionID,
                        TeacherID = teacherMap.OtherTeacherID,
                        SubjectID = teacherMap.SubjectID,
                        SchoolID = teacherMap.SchoolID.HasValue ? teacherMap.SchoolID : Convert.ToByte(_context.SchoolID),
                        AcademicYearID = teacherMap.AcademicYearID.HasValue ? teacherMap.AcademicYearID : _context.AcademicYearID,
                        CreatedBy = toDto.ClassClassTeacherMapIID == 0 ? (int)_context.LoginID : dto.CreatedBy,
                        UpdatedBy = toDto.ClassClassTeacherMapIID > 0 ? (int)_context.LoginID : dto.UpdatedBy,
                        CreatedDate = toDto.ClassClassTeacherMapIID == 0 ? DateTime.Now.Date : dto.CreatedDate,
                        UpdatedDate = toDto.ClassClassTeacherMapIID > 0 ? DateTime.Now.Date : dto.UpdatedDate,
                        //TimeStamps = teacherMap.TimeStamps == null ? null : Convert.FromBase64String(toDto.TimeStamps),
                    });
                }

                entity.ClassAssociateTeacherMaps = new List<ClassAssociateTeacherMap>();
                foreach (var associate in toDto.ClassAssociateTeacherMaps)
                {
                    entity.ClassAssociateTeacherMaps.Add(new ClassAssociateTeacherMap()
                    {
                        ClassClassTeacherMapID = toDto.ClassClassTeacherMapIID,
                        TeacherID = long.Parse(associate.Key),
                    });
                }

                dbContext.ClassClassTeacherMaps.Add(entity);
                if (entity.ClassClassTeacherMapIID == 0)
                {
                    dbContext.Entry(entity).State = Microsoft.EntityFrameworkCore.EntityState.Added;
                }
                else
                {
                    using (var dbContext1 = new dbEduegateSchoolContext())
                    {
                        var classAssociateTeacherMaps = dbContext1.ClassAssociateTeacherMaps.Where(x => x.ClassClassTeacherMapID == entity.ClassClassTeacherMapIID).AsNoTracking().ToList();
                        if (classAssociateTeacherMaps != null && classAssociateTeacherMaps.Count() > 0)
                        {
                            dbContext1.ClassAssociateTeacherMaps.RemoveRange(classAssociateTeacherMaps);
                            dbContext1.SaveChanges();
                        }
                    }

                    if (entity.ClassTeacherMaps.Count > 0)
                    {
                        foreach (var teacher in entity.ClassTeacherMaps)
                        {
                            if (teacher.ClassTeacherMapIID == 0)
                            {
                                dbContext.Entry(teacher).State = Microsoft.EntityFrameworkCore.EntityState.Added;
                            }
                            else
                            {
                                dbContext.Entry(teacher).State = Microsoft.EntityFrameworkCore.EntityState.Modified;
                            }
                        }
                    }

                    if (entity.ClassAssociateTeacherMaps.Count > 0)
                    {
                        foreach (var associateteacher in entity.ClassAssociateTeacherMaps)
                        {
                            if (associateteacher.ClassAssociateTeacherMapIID == 0)
                            {
                                dbContext.Entry(associateteacher).State = Microsoft.EntityFrameworkCore.EntityState.Added;
                            }
                            else
                            {
                                dbContext.Entry(associateteacher).State = Microsoft.EntityFrameworkCore.EntityState.Modified;
                            }
                        }
                    }

                    dbContext.Entry(entity).State = Microsoft.EntityFrameworkCore.EntityState.Modified;
                }
                dbContext.SaveChanges();

                return ToDTOString(ToDTO(entity.ClassClassTeacherMapIID));
            }
        }

        public List<ClassClassTeacherMapDTO> GetTeacherDetails(long studentID)
        {
            var classTeacherListDTO = new List<ClassClassTeacherMapDTO>();

            using (var dbContext = new dbEduegateSchoolContext())
            {
                var studDet = dbContext.Students.Where(x => x.StudentIID == studentID).AsNoTracking().FirstOrDefault();

                #region get class teacher
                var classTeacher = dbContext.ClassClassTeacherMaps
                    .Where(z => z.ClassID == studDet.ClassID && z.SectionID == studDet.SectionID && z.AcademicYearID == studDet.AcademicYearID)
                    .Include(i => i.Class)
                    .Include(i => i.Section)
                    .Include(i => i.Subject)
                    .Include(i => i.Employee1).ThenInclude(i => i.Gender)
                    .Include(i => i.ClassTeacherMaps).ThenInclude(i => i.Subject)
                    .AsNoTracking()
                    .FirstOrDefault();

                if (classTeacher != null)
                {
                    classTeacherListDTO.Add(new ClassClassTeacherMapDTO
                    {
                        ClassID = classTeacher.ClassID,
                        SectionID = classTeacher.SectionID,
                        ClassName = classTeacher.Class?.ClassDescription,
                        SectionName = classTeacher.Section?.SectionName,
                        ClassTeacherID = classTeacher.TeacherID,
                        HeadTeacherName = classTeacher.Employee1?.FirstName + ' ' + classTeacher.Employee1?.MiddleName + ' ' + classTeacher.Employee1?.LastName + " (Class Teacher)",
                        EmployeePhoto = classTeacher.Employee1?.EmployeePhoto,
                        GenderID = classTeacher.Employee1?.GenderID,
                        GenderDescription = classTeacher.Employee1?.GenderID != null ? classTeacher.Employee1?.Gender?.Description : "NA",
                        SubjectName = BindTeacherSubject(classTeacher),
                        WorkEmail = classTeacher.Employee1?.WorkEmail ?? "NA",
                    });
                    #endregion class teacher

                    #region get Other teachers
                    var otherTeacher = dbContext.ClassTeacherMaps.Where(z => z.ClassClassTeacherMapID == classTeacher.ClassClassTeacherMapIID && z.TeacherID != classTeacher.TeacherID)
                        .Include(i => i.Class)
                        .Include(i => i.Section)
                        .Include(i => i.Subject)
                        .Include(i => i.Employee2).ThenInclude(i => i.Gender)
                        .AsNoTracking()
                        .ToList();

                    foreach (var teacherSub in otherTeacher)
                    {
                        var availableData = classTeacherListDTO.Find(t => t.ClassTeacherID == teacherSub.TeacherID);
                        if (availableData != null)
                        {
                            availableData.SubjectName = string.Concat(availableData.SubjectName, ",\n", teacherSub.Subject?.SubjectName);
                        }
                        else
                        {
                            classTeacherListDTO.Add(new ClassClassTeacherMapDTO
                            {
                                ClassID = teacherSub.ClassID.HasValue ? teacherSub.ClassID : null,
                                SectionID = teacherSub.SectionID.HasValue ? teacherSub.SectionID : null,
                                ClassName = teacherSub.ClassID.HasValue ? teacherSub.Class?.ClassDescription : null,
                                SectionName = teacherSub.SectionID.HasValue ? teacherSub.Section?.SectionName : null,
                                ClassTeacherID = teacherSub.TeacherID.HasValue ? teacherSub.TeacherID : null,
                                HeadTeacherName = teacherSub.TeacherID.HasValue ? teacherSub.Employee2?.FirstName + ' ' + teacherSub.Employee2?.MiddleName + ' ' + teacherSub.Employee2?.LastName : null,
                                EmployeePhoto = teacherSub.Employee2?.EmployeePhoto,
                                GenderID = teacherSub.Employee2?.GenderID,
                                GenderDescription = teacherSub.Employee2?.GenderID != null ? teacherSub.Employee2?.Gender?.Description : "NA",
                                SubjectID = teacherSub.SubjectID.HasValue ? teacherSub.SubjectID : null,
                                SubjectName = teacherSub.SubjectID.HasValue ? teacherSub.Subject?.SubjectName : "NA",
                                WorkEmail = teacherSub.Employee2?.WorkEmail ?? "NA",
                            });
                        }
                    }
                    #endregion other teacher
                }
            }

            return classTeacherListDTO;
        }

        public string BindTeacherSubject(ClassClassTeacherMap classTeacher)
        {
            var teacherSubject = classTeacher.SubjectID.HasValue ? classTeacher.Subject?.SubjectName :
                classTeacher.ClassTeacherMaps?.FirstOrDefault(t => t.TeacherID == classTeacher.TeacherID) != null ?
                classTeacher.ClassTeacherMaps.FirstOrDefault(t => t.TeacherID == classTeacher.TeacherID).Subject != null ?
                classTeacher.ClassTeacherMaps.FirstOrDefault(t => t.TeacherID == classTeacher.TeacherID).Subject?.SubjectName != null ?
                classTeacher.ClassTeacherMaps.FirstOrDefault(t => t.TeacherID == classTeacher.TeacherID).Subject?.SubjectName : "NA" : "NA" : "NA";

            return teacherSubject;
        }

        public List<KeyValueDTO> GetTeacherEmailByParentLoginID(long loginID)
        {
            using (var dbContext = new dbEduegateSchoolContext())
            {
                var teacherEmailList = new List<KeyValueDTO>();

                var students = dbContext.Students.Where(x => x.Parent.LoginID == loginID)
                    .Include(i => i.Parent)
                    .AsNoTracking()
                    .ToList();

                foreach (var stud in students)
                {
                    var teachersList = dbContext.ClassTeacherMaps.Where(x => x.ClassID == stud.ClassID)
                        .Include(i => i.Employee1).ThenInclude(i => i.Login)
                        .AsNoTracking()
                        .ToList();

                    foreach (var teacher in teachersList)
                    {
                        var login = teacher.Employee1?.Login;

                        if (login != null)
                        {
                            if (login.LoginEmailID != null)
                            {
                                teacherEmailList.Add(new KeyValueDTO
                                {
                                    Key = login.LoginIID.ToString(),
                                    Value = login.LoginEmailID
                                });
                            }
                        }
                    }
                }

                return teacherEmailList;
            }
        }

       public List<StudentChatDTO> GetStudentTeacherEmailByParentLoginID(long loginID)
       {
            var studentTeacherList = new List<StudentChatDTO>();

            using (var dbContext = new dbEduegateSchoolContext())
            {
                // Get all students for the parent login
                var studentList = dbContext.Students
                    .Where(x => x.Parent.LoginID == loginID && x.IsActive == true)
                    .Include(i => i.Parent) // Include Parent if needed elsewhere, not directly used in DTO here
                    .AsNoTracking()
                    .ToList();

                foreach (var student in studentList)
                {
                    var dto = new StudentChatDTO
                    {
                        StudentID = student.StudentIID,
                        StudentName = FormatEmployeeName(student.FirstName, student.MiddleName, student.LastName),
                        TeacherMessages = new List<EmployeeDTO>() // Initialize the list
                    };

                    // Get class teacher map for the student's current class, section, and academic year
                    var classTeacherMap = dbContext.ClassClassTeacherMaps
                        .Include(i => i.Employee1) // Includes the Class Teacher's Employee details
                            // .ThenInclude(i => i.Gender) // Only if Gender is needed for EmployeeDTO
                        // .Include(i => i.ClassTeacherMaps) // This loads subject teacher *mappings*, not the teachers themselves yet
                        //     .ThenInclude(i => i.Subject) // Subject for subject teachers
                        .AsNoTracking()
                        .FirstOrDefault(z => z.ClassID == student.ClassID &&
                                             z.SectionID == student.SectionID &&
                                             z.AcademicYearID == student.AcademicYearID);

                    if (classTeacherMap != null)
                    {
                        // Head/Class Teacher
                        if (classTeacherMap.Employee1 != null) // Employee1 is the Class Teacher
                        {
                            dto.TeacherMessages.Add(new EmployeeDTO
                            {
                                EmployeeCode = classTeacherMap.Employee1.EmployeeCode,
                                EmployeeIID = classTeacherMap.Employee1.EmployeeIID,
                                EmployeeName = FormatEmployeeName(classTeacherMap.Employee1.FirstName,
                                                               classTeacherMap.Employee1.MiddleName,
                                                               classTeacherMap.Employee1.LastName, " (Class Teacher)"),
                                WorkEmail = classTeacherMap.Employee1.WorkEmail ?? "NA",
                                EmployeePhoto = classTeacherMap.Employee1.EmployeePhoto ?? "NA"
                                // Add Gender if needed: Gender = classTeacherMap.Employee1.Gender?.GenderName
                            });
                        }

                        // Subject Teachers
                        // These are linked via ClassTeacherMap which has a FK to ClassClassTeacherMap
                        var subjectTeachers = dbContext.ClassTeacherMaps
                            .Where(z => z.ClassClassTeacherMapID == classTeacherMap.ClassClassTeacherMapIID &&
                                        (classTeacherMap.TeacherID == null || z.TeacherID != classTeacherMap.TeacherID)) // Exclude class teacher if they are also listed as subject teacher
                            .Include(i => i.Employee2) // Employee2 is the Subject Teacher's Employee details
                               // .ThenInclude(i => i.Gender) // Only if Gender is needed
                            .Include(i => i.Subject)     // Subject details
                            .AsNoTracking()
                            .ToList();

                        foreach (var subjectTeacher in subjectTeachers)
                        {
                            if (subjectTeacher.Employee2 != null) // Employee2 is the Subject Teacher
                            {
                                dto.TeacherMessages.Add(new EmployeeDTO
                                {
                                    EmployeeIID = subjectTeacher.Employee2.EmployeeIID,

                                    EmployeeCode = subjectTeacher.Employee2.EmployeeCode, // Provide a fallback value of null if LoginID is null
                                    EmployeeName = FormatEmployeeName(subjectTeacher.Employee2.FirstName,
                                                                   subjectTeacher.Employee2.MiddleName,
                                                                   subjectTeacher.Employee2.LastName,
                                                                   subjectTeacher.Subject != null ? $" ({subjectTeacher.Subject.SubjectName})" : " (Subject Teacher)"),
                                    WorkEmail = subjectTeacher.Employee2.WorkEmail ?? "NA",
                                    EmployeePhoto = subjectTeacher.Employee2.EmployeePhoto ?? "NA"
                                });
                            }
                        }
                    }
                    studentTeacherList.Add(dto);
                }
            }
            return studentTeacherList;
        }
        // Helper method for formatting names consistently
        private string FormatEmployeeName(string firstName, string middleName, string lastName, string suffix = "")
        {
            var nameParts = new List<string>();
            if (!string.IsNullOrWhiteSpace(firstName)) nameParts.Add(firstName);
            if (!string.IsNullOrWhiteSpace(middleName)) nameParts.Add(middleName);
            if (!string.IsNullOrWhiteSpace(lastName)) nameParts.Add(lastName);

            var fullName = string.Join(" ", nameParts);
            if (!string.IsNullOrWhiteSpace(suffix))
            {
                fullName += suffix;
            }
            return fullName;
        }

        //public List<EmployeeDTO> GetStudentTeacherEmailByParentLoginID(long loginID)
        //{
        //    var teacherEmailList = new List<EmployeeDTO>();
        //    var uniqueEmployeeLogins = new HashSet<string>();

        //    using (SqlConnection conn = new SqlConnection(Infrastructure.ConfigHelper.GetDefaultConnectionString()))
        //    {
        //        try
        //        {
        //            conn.Open();

        //            // Set up the command to execute the stored procedure
        //            using (SqlCommand cmd = new SqlCommand("[schools].[GET_ALL_TEACHERS_MAIL_ID_BY_PARENT_LOGINID]", conn))
        //            {
        //                cmd.CommandType = CommandType.StoredProcedure;
        //                cmd.Parameters.AddWithValue("@LoginID", loginID);  // Parent's login ID as the input parameter

        //                // Execute the stored procedure and read the results
        //                using (SqlDataReader reader = cmd.ExecuteReader())
        //                {
        //                    while (reader.Read())
        //                    {
        //                        // Map the result to KeyValueDTO
        //                        var email = reader["LoginEmailID"] != DBNull.Value ? reader["LoginEmailID"].ToString() : string.Empty;
        //                        var employeeID = reader["EmployeeLoginID"] != DBNull.Value ? reader["EmployeeLoginID"].ToString() : string.Empty;
        //                        var EmployeeName = reader["EmployeeName"] != DBNull.Value ? reader["EmployeeName"].ToString() : string.Empty;

        //                        if (!string.IsNullOrEmpty(email) && !string.IsNullOrEmpty(employeeID))
        //                        {
        //                            if (uniqueEmployeeLogins.Add(employeeID))
        //                            {
        //                                teacherEmailList.Add(new EmployeeDTO
        //                                {
        //                                    WorkEmail = email,
        //                                    EmployeeCode = employeeID,
        //                                    EmployeeName = EmployeeName
        //                                });
        //                            }
        //                        }
        //                    }
        //                }
        //            }
        //        }
        //        catch (Exception ex)
        //        {
        //            // Handle any exceptions
        //            // Log the exception or return an empty list
        //            Console.WriteLine($"Error: {ex.Message}");
        //        }
        //    }

        //    return teacherEmailList;
        //}
        public List<StudentDTO> GetStudentsByTeacherClassAndSection(long? classID, long? sectionID)
        {
            using (var dbContext = new dbEduegateSchoolContext())
            {
                var currentAcademicYearStatus = new Domain.Setting.SettingBL(null).GetSettingValue<string>("CURRENT_ACADEMIC_YEAR_STATUSID");
                int? currentAcademicYearStatusID = int.Parse(currentAcademicYearStatus);
                byte studentActiveStatus = new Domain.Setting.SettingBL(null).GetSettingValue<byte>("STUDENT_ACTIVE_STATUSID");

                var studentList = new List<StudentDTO>();

                //var students = sectionID.HasValue || sectionID != 0 ? dbContext.Students.Where(X => X.ClassID == classID && X.SectionID == sectionID && X.IsActive == true && X.AcademicYear.AcademicYearStatusID == currentAcademicYearStatusID).AsNoTracking().ToList()
                //    : dbContext.Students.Where(X => X.ClassID == classID && X.IsActive == true && X.AcademicYear.AcademicYearStatusID == currentAcademicYearStatusID).AsNoTracking().ToList();

                var students = dbContext.Students
                    .Where(X => (sectionID.HasValue ? X.ClassID == classID && X.SectionID == sectionID : X.ClassID == classID)
                    && X.IsActive == true && X.AcademicYear.AcademicYearStatusID == currentAcademicYearStatusID && X.Status == studentActiveStatus)
                    .Include(i => i.Class)
                    .Include(i => i.Section)
                    .Include(i => i.Gender)
                    .Include(i => i.BloodGroup)
                    .Include(i => i.AcademicYear)
                    .Include(i => i.AcademicYear1)
                    .Include(i => i.School)
                    .Include(i => i.Cast)
                    .Include(i => i.StudentCategory)
                    .Include(i => i.Relegion)
                    .Include(i => i.Community)
                    .Include(i => i.StudentHouse)
                    .Include(i => i.Parent)
                    .Include(i => i.StudentPassportDetails)
                    .AsNoTracking().ToList();

                if (students != null)
                {
                    foreach (var stud in students)
                    {
                        var siblingKeyValueList = new List<KeyValueDTO>();
                        var siblingList = dbContext.Students.Where(y => y.ParentID == stud.ParentID && y.StudentIID != stud.StudentIID).ToList();

                        foreach (var sib in siblingList)
                        {
                            siblingKeyValueList.Add(new KeyValueDTO()
                            {
                                Key = sib.StudentIID.ToString(),
                                Value = sib.AdmissionNumber + " - " + sib.FirstName + " " + sib.MiddleName + " " + sib.LastName,
                            });
                        }

                        studentList.Add(new StudentDTO
                        {
                            StudentIID = stud.StudentIID,
                            AdmissionNumber = stud.AdmissionNumber,
                            FirstName = stud.FirstName,
                            MiddleName = stud.MiddleName,
                            LastName = stud.LastName,
                            StudentFullName = stud.FirstName + " " + stud.MiddleName + " " + stud.LastName,
                            StudentProfile = stud.StudentProfile,
                            ClassID = stud.ClassID,
                            ClassCode = stud.ClassID.HasValue ? stud.Class?.Code ?? "NA" : "NA",
                            ClassName = stud.ClassID.HasValue ? stud.Class?.ClassDescription ?? "NA" : "NA",
                            SectionID = stud.SectionID,
                            SectionName = stud.SectionID.HasValue ? stud.Section?.SectionName ?? "NA" : "NA",
                            GenderID = stud.GenderID,
                            GenderName = stud.GenderID.HasValue ? stud.Gender?.Description ?? "NA" : "NA",
                            EmailID = string.IsNullOrEmpty(stud.EmailID) ? "NA" : stud.EmailID,
                            MobileNumber = string.IsNullOrEmpty(stud.MobileNumber) ? "NA" : stud.MobileNumber,
                            DateOfBirth = stud.DateOfBirth.HasValue ? stud.DateOfBirth : (DateTime?)null,
                            StudentAge = GetAgeByDateOfBirth(stud.DateOfBirth),
                            AdmissionDate = stud.AdmissionDate.HasValue ? stud.AdmissionDate : (DateTime?)null,
                            FeeStartDate = stud.FeeStartDate.HasValue ? stud.FeeStartDate : (DateTime?)null,
                            BloodGroupID = stud.BloodGroupID,
                            BloodGroupName = stud.BloodGroupID.HasValue ? stud.BloodGroup?.BloodGroupName ?? "NA" : "NA",
                            Height = string.IsNullOrEmpty(stud.Height) ? "NA" : stud.Height,
                            Weight = string.IsNullOrEmpty(stud.Weight) ? "NA" : stud.Weight,
                            AsOnDate = stud.AsOnDate.HasValue ? stud.AsOnDate : (DateTime?)null,
                            SchoolAcademicyearID = stud.SchoolAcademicyearID,
                            SchoolAcademicYearName = stud.SchoolAcademicyearID.HasValue ? stud.AcademicYear1?.Description + '(' + stud.AcademicYear1?.AcademicYearCode + ')' : "NA",
                            SchoolID = stud.SchoolID,
                            SchoolName = stud.SchoolID.HasValue ? stud.School?.SchoolName ?? "NA" : "NA",
                            AcademicYearID = stud.AcademicYearID,
                            AcademicYear = stud.AcademicYearID.HasValue ? stud.AcademicYear?.Description + '(' + stud.AcademicYear?.AcademicYearCode + ')' : "NA",
                            CastID = stud.CastID,
                            CastName = stud.CastID.HasValue ? stud.Cast?.CastDescription ?? "NA" : "NA",
                            CategoryID = stud.StudentCategoryID,
                            CategoryName = stud.StudentCategoryID.HasValue ? stud.StudentCategory?.Description ?? "NA" : "NA",
                            RelegionID = stud.RelegionID,
                            RelegionName = stud.RelegionID.HasValue ? stud.Relegion?.RelegionName ?? "NA" : "NA",
                            CommunityID = stud.CommunityID,
                            Community = stud.CommunityID.HasValue ? stud.Community?.CommunityDescription ?? "NA" : "NA",
                            StudentHouseID = stud.StudentHouseID,
                            StudentHouse = stud.StudentHouseID.HasValue ? stud.StudentHouse?.Description ?? "NA" : "NA",
                            Guardian = new GuardianDTO()
                            {
                                ParentCode = stud.Parent?.ParentCode,
                                FatherFirstName = stud.Parent?.FatherFirstName,
                                FatherMiddleName = stud.Parent?.FatherMiddleName,
                                FatherLastName = stud.Parent?.FatherLastName,
                                PhoneNumber = string.IsNullOrEmpty(stud.Parent?.PhoneNumber) ? "NA" : stud.Parent?.PhoneNumber,
                                FatherEmailID = string.IsNullOrEmpty(stud.Parent?.FatherEmailID) ? "NA" : stud.Parent?.FatherEmailID,
                                MotherFirstName = stud.Parent?.MotherFirstName,
                                MotherMiddleName = stud.Parent?.MotherMiddleName,
                                MotherLastName = stud.Parent?.MotherLastName,
                                MotherPhone = string.IsNullOrEmpty(stud.Parent?.MotherPhone) ? "NA" : stud.Parent?.MotherPhone,
                                MotherEmailID = string.IsNullOrEmpty(stud.Parent?.MotherEmailID) ? "NA" : stud.Parent?.MotherEmailID,
                                GuardianFirstName = stud.Parent.GuardianFirstName,
                                GuardianMiddleName = stud.Parent.GuardianMiddleName,
                                GuardianLastName = stud.Parent.GuardianLastName,
                                GuardianPhone = string.IsNullOrEmpty(stud.Parent?.GuardianPhone) ? "NA" : stud.Parent?.GuardianPhone,
                                GaurdianEmail = string.IsNullOrEmpty(stud.Parent?.GaurdianEmail) ? "NA" : stud.Parent?.GaurdianEmail,
                            },
                            StudentPassportDetails = stud.StudentPassportDetails.Select(passportDetail => new StudentPassportDetailDTO()
                            {
                                AdhaarCardNo = passportDetail.AdhaarCardNo ?? "NA",
                                PassportNo = passportDetail.PassportNo ?? "NA",
                                PassportNoExpiry = passportDetail.PassportNoExpiry != null ? passportDetail.PassportNoExpiry : (DateTime?)null,
                                VisaNo = passportDetail.VisaNo ?? "NA",
                                VisaExpiry = passportDetail.VisaExpiry != null ? passportDetail.VisaExpiry : (DateTime?)null,
                                NationalIDNo = passportDetail.NationalIDNo ?? "NA",
                                NationalIDNoExpiry = passportDetail.NationalIDNoExpiry != null ? passportDetail.NationalIDNoExpiry : (DateTime?)null,
                            }).FirstOrDefault(),
                            StudentSiblings = siblingKeyValueList,
                        });
                    }
                }

                return studentList;
            }
        }

        public int? GetAgeByDateOfBirth(DateTime? dateOfBirth)
        {
            int? age = null;

            if (dateOfBirth.HasValue)
            {
                age = DateTime.Now.Subtract(Convert.ToDateTime(dateOfBirth)).Days;
                age = age / 365;
            }

            return age;
        }

        public List<ClassTeacherMapDTO> GetTeacherClass(long loginID)
        {
            using (var dbContext = new dbEduegateSchoolContext())
            {
                var currentAcademicYearStatus = new Domain.Setting.SettingBL(null).GetSettingValue<string>("CURRENT_ACADEMIC_YEAR_STATUSID");
                int? currentAcademicYearStatusID = int.Parse(currentAcademicYearStatus);

                var classSectionList = new List<ClassTeacherMapDTO>();

                var employee = dbContext.Employees.Where(X => X.LoginID == loginID).AsNoTracking().FirstOrDefault();

                var teacherClassMaps = dbContext.ClassTeacherMaps.Where(X => X.TeacherID == employee.EmployeeIID && X.AcademicYear.AcademicYearStatusID == currentAcademicYearStatusID)
                    .Include(x => x.Class)
                    .Include(x => x.Section)
                    .AsNoTracking().ToList();

                var teacherClassClassMaps = dbContext.ClassClassTeacherMaps.Where(X => X.TeacherID == employee.EmployeeIID && X.AcademicYear.AcademicYearStatusID == currentAcademicYearStatusID)
                        .Include(x => x.Class)
                    .Include(x => x.Section)
                    .AsNoTracking().ToList();

                if (teacherClassMaps != null)
                {
                    foreach (var classTeacher in teacherClassMaps)
                    {
                        if (!classSectionList.Any(x => x.ClassID == classTeacher.ClassID && x.SectionID == classTeacher.SectionID))
                        {
                            classSectionList.Add(new ClassTeacherMapDTO
                            {
                                ClassID = classTeacher.ClassID,
                                SectionID = classTeacher.SectionID,
                                ClassName = classTeacher.ClassID.HasValue ? classTeacher.Class.ClassDescription : null,
                                SectionName = classTeacher.SectionID.HasValue ? classTeacher.Section.SectionName : null,
                                ClassOrderNumber = classTeacher.ClassID.HasValue ? classTeacher.Class.ORDERNO : null,
                            });
                        }
                    }
                }

                if (teacherClassClassMaps != null)
                {
                    foreach (var classClassTeacher in teacherClassClassMaps)
                    {
                        if (!classSectionList.Any(x => x.ClassID == classClassTeacher.ClassID && x.SectionID == classClassTeacher.SectionID))
                        {
                            classSectionList.Add(new ClassTeacherMapDTO
                            {
                                ClassID = classClassTeacher.ClassID,
                                SectionID = classClassTeacher.SectionID,
                                ClassName = classClassTeacher.ClassID.HasValue ? classClassTeacher.Class.ClassDescription : null,
                                SectionName = classClassTeacher.SectionID.HasValue ? classClassTeacher.Section.SectionName : null,
                                ClassOrderNumber = classClassTeacher.ClassID.HasValue ? classClassTeacher.Class.ORDERNO : null,
                            });
                        }
                    }
                }

                if (classSectionList.Count > 0)
                {
                    classSectionList = classSectionList.OrderBy(y => y.ClassOrderNumber).OrderBy(y => y.SectionName).ToList();
                }

                return classSectionList;
            }
        }

        public int GetTeacherClassCount(long employeeID)
        {
            using (var dbContext = new dbEduegateSchoolContext())
            {
                var currentAcademicYearStatus = new Domain.Setting.SettingBL(null).GetSettingValue<string>("CURRENT_ACADEMIC_YEAR_STATUSID");
                int? currentAcademicYearStatusID = int.Parse(currentAcademicYearStatus);

                var mapDataList = new List<ClassTeacherMapDTO>();

                var teacherClassMaps = dbContext.ClassTeacherMaps.Where(x => x.TeacherID == employeeID && x.AcademicYear.AcademicYearStatusID == currentAcademicYearStatusID).AsNoTracking().ToList();
                var classClassTeacherMaps = dbContext.ClassClassTeacherMaps.Where(y => y.TeacherID == employeeID && y.AcademicYear.AcademicYearStatusID == currentAcademicYearStatusID).AsNoTracking().ToList();

                foreach (var map in teacherClassMaps)
                {
                    if (!mapDataList.Any(x => x.ClassID == map.ClassID && x.SectionID == map.SectionID))
                    {
                        mapDataList.Add(new ClassTeacherMapDTO()
                        {
                            ClassID = map.ClassID,
                            SectionID = map.SectionID,
                        });
                    }
                }

                foreach (var map in classClassTeacherMaps)
                {
                    if (!mapDataList.Any(x => x.ClassID == map.ClassID && x.SectionID == map.SectionID))
                    {
                        mapDataList.Add(new ClassTeacherMapDTO()
                        {
                            ClassID = map.ClassID,
                            SectionID = map.SectionID,
                        });
                    }
                }

                var totalClassCount = mapDataList.Count();

                return totalClassCount;
            }
        }

        public List<ClassClassTeacherMapDTO> FillEditDatasAndSubjects(int IID, int classID, int sectionID)
        {
            using (var dbContext = new dbEduegateSchoolContext())
            {
                var subjectList = new List<ClassClassTeacherMapDTO>();

                var getDatasByIDs = dbContext.ClassTeacherMaps.Where(x => x.ClassClassTeacherMapID == IID).AsNoTracking().ToList();

                var classSubjectList = dbContext.ClassSubjectMaps.Where(x => x.ClassID == classID && x.SectionID == sectionID && x.SchoolID == _context.SchoolID && x.AcademicYearID == _context.AcademicYearID).AsNoTracking().ToList();

                foreach (var data in getDatasByIDs)
                {
                    subjectList.Add(new ClassClassTeacherMapDTO
                    {
                        ClassClassTeacherMapIID = (long)data.ClassClassTeacherMapID,
                        Subject = new KeyValueDTO() { Key = data.SubjectID.ToString(), Value = data.Subject?.SubjectName },
                        SubjectID = data.SubjectID,
                        OtherTeacher = new KeyValueDTO() { Key = data.TeacherID.ToString(), Value = data.Employee2?.EmployeeCode + " - " + data.Employee2?.FirstName + ' ' + data.Employee2?.MiddleName + ' ' + data.Employee2?.LastName },
                    });
                }
                foreach (var subj in classSubjectList)
                {
                    if (!subjectList.Any(x => x.SubjectID == subj.SubjectID))
                    {
                        subjectList.Add(new ClassClassTeacherMapDTO
                        {
                            Subject = new KeyValueDTO() { Key = subj.SubjectID.ToString(), Value = subj.Subject?.SubjectName },
                            //OtherTeacher = new KeyValueDTO() { Key = subj.EmployeeID.ToString(), Value = subj.Employee?.EmployeeCode + " - " + subj.Employee?.FirstName + ' ' + subj.Employee?.MiddleName + ' ' + subj.Employee?.LastName },
                        });
                    }
                }

                return subjectList;
            }
        }

        public KeyValueDTO GetClassHeadTeacher(int classID, int sectionID, int academicYearID)
        {
            using (var dbContext = new dbEduegateSchoolContext())
            {
                var keyValue = new KeyValueDTO();

                var currentAcademicYearStatusID = new Domain.Setting.SettingBL(null).GetSettingValue<int>("CURRENT_ACADEMIC_YEAR_STATUSID", 2);

                var classTeacher = dbContext.ClassClassTeacherMaps
                    .Where(t => t.ClassID == classID && t.SectionID == sectionID && t.AcademicYearID == academicYearID)
                    .Include(i => i.ClassTeacherMaps).ThenInclude(i => i.Subject)
                    .AsNoTracking().FirstOrDefault();

                if (classTeacher != null && classTeacher.TeacherID.HasValue)
                {
                    var subjectMap = classTeacher.ClassTeacherMaps.FirstOrDefault(x => x.TeacherID == classTeacher.TeacherID);

                    string subjectName = subjectMap?.Subject?.SubjectName ?? null;

                    var entity = dbContext.Employees.Where(x => x.EmployeeIID == classTeacher.TeacherID && x.IsActive == true)
                        .AsNoTracking().FirstOrDefault();

                    keyValue = new KeyValueDTO()
                    {
                        Key = entity.EmployeeIID.ToString(),
                        Value = entity.EmployeeCode + " - " + entity.FirstName + " " + (entity.MiddleName != null ? entity.MiddleName + " " : "") + entity.LastName
                        + (string.IsNullOrEmpty(subjectName) ? null : " (" + subjectName + ")"),
                    };
                }
                else
                {
                    return null;
                }

                return keyValue;
            }
        }

        public KeyValueDTO GetClassCoordinator(int classID, int sectionID, int academicYearID)
        {
            using (var dbContext = new dbEduegateSchoolContext())
            {
                var keyValue = new KeyValueDTO();

                var currentAcademicYearStatusID = new Domain.Setting.SettingBL(null).GetSettingValue<int>("CURRENT_ACADEMIC_YEAR_STATUSID", 2);

                var mapEntity = dbContext.ClassClassTeacherMaps
                    .Where(t => t.ClassID == classID && t.SectionID == sectionID && t.AcademicYearID == academicYearID)
                    .Include(i => i.ClassTeacherMaps).ThenInclude(i => i.Subject)
                    .AsNoTracking().FirstOrDefault();

                if (mapEntity != null && mapEntity.CoordinatorID.HasValue)
                {
                    var subjectMap = mapEntity.ClassTeacherMaps.FirstOrDefault(x => x.TeacherID == mapEntity.CoordinatorID);

                    string subjectName = subjectMap?.Subject?.SubjectName ?? null;

                    var entity = dbContext.Employees.Where(x => x.EmployeeIID == mapEntity.CoordinatorID && x.IsActive == true)
                        .AsNoTracking().FirstOrDefault();

                    keyValue = new KeyValueDTO()
                    {
                        Key = entity.EmployeeIID.ToString(),
                        Value = entity.EmployeeCode + " - " + entity.FirstName + " " + (entity.MiddleName != null ? entity.MiddleName + " " : "") + entity.LastName
                        + (string.IsNullOrEmpty(subjectName) ? null : " (" + subjectName + ")"),
                    };
                }
                else
                {
                    return null;
                }

                return keyValue;
            }
        }

        public List<KeyValueDTO> GetAssociateTeachers(int classID, int sectionID, int academicYearID)
        {
            using (var dbContext = new dbEduegateSchoolContext())
            {
                var keyValueList = new List<KeyValueDTO>();

                var currentAcademicYearStatusID = new Domain.Setting.SettingBL(null).GetSettingValue<int>("CURRENT_ACADEMIC_YEAR_STATUSID", 2);

                var mapEntity = dbContext.ClassClassTeacherMaps
                    .Where(t => t.ClassID == classID && t.SectionID == sectionID && t.AcademicYearID == academicYearID)
                    .Include(i => i.ClassTeacherMaps).ThenInclude(i => i.Subject)
                    .Include(i => i.ClassAssociateTeacherMaps)
                    .AsNoTracking().FirstOrDefault();

                if (mapEntity != null)
                {
                    foreach (var assMap in mapEntity.ClassAssociateTeacherMaps)
                    {
                        var subjectMap = mapEntity.ClassTeacherMaps.FirstOrDefault(x => x.TeacherID == assMap.TeacherID);

                        string subjectName = subjectMap?.Subject?.SubjectName ?? null;

                        var entity = dbContext.Employees.Where(x => x.EmployeeIID == assMap.TeacherID && x.IsActive == true)
                            .AsNoTracking().FirstOrDefault();

                        keyValueList.Add(new KeyValueDTO()
                        {
                            Key = entity.EmployeeIID.ToString(),
                            Value = entity.EmployeeCode + " - " + entity.FirstName + " " + (entity.MiddleName != null ? entity.MiddleName + " " : "") + entity.LastName
                            + (string.IsNullOrEmpty(subjectName) ? null : " (" + subjectName + ")"),
                        });
                    }
                }

                return keyValueList;
            }
        }

        public List<KeyValueDTO> GetOtherTeachersByClass(int classID, int sectionID, int academicYearID)
        {
            using (var dbContext = new dbEduegateSchoolContext())
            {
                var keyValueList = new List<KeyValueDTO>();

                var currentAcademicYearStatusID = new Domain.Setting.SettingBL(null).GetSettingValue<int>("CURRENT_ACADEMIC_YEAR_STATUSID", 2);

                var mapEntity = dbContext.ClassClassTeacherMaps
                    .Where(t => t.ClassID == classID && t.SectionID == sectionID && t.AcademicYearID == academicYearID)
                    .Include(i => i.ClassTeacherMaps).ThenInclude(i => i.Subject)
                    .Include(i => i.ClassTeacherMaps).ThenInclude(i => i.Employee2)
                    .Include(i => i.ClassAssociateTeacherMaps)
                    .AsNoTracking().FirstOrDefault();

                if (mapEntity != null)
                {
                    var associateTeachersIDs = new List<long?>();
                    if (mapEntity.ClassAssociateTeacherMaps.Count > 0)
                    {
                        foreach (var associate in mapEntity.ClassAssociateTeacherMaps)
                        {
                            associateTeachersIDs.Add(associate.TeacherID);
                        }
                    }

                    foreach (var teachMap in mapEntity.ClassTeacherMaps)
                    {
                        if (teachMap.TeacherID != mapEntity.TeacherID && !associateTeachersIDs.Contains(teachMap.TeacherID))
                        {
                            string subjectName = teachMap?.Subject?.SubjectName ?? null;

                            var entity = teachMap.Employee2 != null && teachMap.Employee2.IsActive == true ? teachMap.Employee2 : null;

                            if (entity == null)
                            {
                                entity = dbContext.Employees.Where(x => x.EmployeeIID == teachMap.TeacherID && x.IsActive == true)
                                .AsNoTracking().FirstOrDefault();
                            }

                            if (entity != null)
                            {
                                keyValueList.Add(new KeyValueDTO()
                                {
                                    Key = entity.EmployeeIID.ToString(),
                                    Value = entity.EmployeeCode + " - " + entity.FirstName + " " + (entity.MiddleName != null ? entity.MiddleName + " " : "") + entity.LastName
                                    + (string.IsNullOrEmpty(subjectName) ? null : " (" + subjectName + ")"),
                                });
                            }
                        }
                    }
                }

                return keyValueList;
            }
        }

        public List<GuardianDTO> GetParentsByTeacherLoginID(long loginID)
        {
            using (var dbContext = new dbEduegateSchoolContext())
            {
                // Retrieve the current academic year status
                var currentAcademicYearStatus = new Domain.Setting.SettingBL(null).GetSettingValue<string>("CURRENT_ACADEMIC_YEAR_STATUSID");
                byte studentActiveStatus = new Domain.Setting.SettingBL(null).GetSettingValue<byte>("STUDENT_ACTIVE_STATUSID");

                var classIds = new List<int>();
                var sectionIDs = new List<int>();

                int? currentAcademicYearStatusID = int.Parse(currentAcademicYearStatus);

                // Get the teacher's employee record
                var teacher = dbContext.Employees
                    .Where(e => e.LoginID == loginID)
                    .AsNoTracking()
                    .FirstOrDefault();

                if (teacher == null)
                {
                    return new List<GuardianDTO>(); // Teacher not found
                }

                // Retrieve the class and sections assigned to the teacher
                var teacherClassMaps = dbContext.ClassClassTeacherMaps
                    .Where(ct => ct.TeacherID == teacher.EmployeeIID && ct.AcademicYear.AcademicYearStatusID == currentAcademicYearStatusID)
                    .Select(ct => new { ct.ClassID, ct.SectionID })
                    .AsNoTracking()
                    .ToList();

                classIds.AddRange(teacherClassMaps.Select(a => a.ClassID ?? 0));
                sectionIDs.AddRange(teacherClassMaps.Select(a => a.SectionID ?? 0));

                var subjectTeacherClassMaps = dbContext.ClassTeacherMaps
                    .Where(ct => ct.TeacherID == teacher.EmployeeIID && ct.AcademicYear.AcademicYearStatusID == currentAcademicYearStatusID)
                    .Select(ct => new { ct.ClassID, ct.SectionID })
                    .AsNoTracking()
                    .ToList();

                classIds.AddRange(subjectTeacherClassMaps.Select(a => a.ClassID ?? 0));
                sectionIDs.AddRange(subjectTeacherClassMaps.Select(a => a.SectionID ?? 0));

                // Fetch all students for the current academic year
                var students = dbContext.Students
                    .Where(s => s.AcademicYear.AcademicYearStatusID == currentAcademicYearStatusID)
                    .Include(s => s.Parent)
                    .AsNoTracking()
                    .AsEnumerable() // Switch to client-side evaluation
                    .Where(s => classIds.Any(cl => cl == s.ClassID) && sectionIDs.Any(st => st == s.SectionID) && s.IsActive == true && s.Status == studentActiveStatus) // Apply filtering in memory
                    .ToList();

                // Prepare the list of ParentDTO
                var parentList = new List<GuardianDTO>();

                foreach (var student in students)
                {
                    var parent = student.Parent;
                    if (parent != null)
                    {
                     
                            // Add parent and student details to the list if not already present
                        
                                parentList.Add(new GuardianDTO
                                {
                                    ParentIID = parent.ParentIID,
                                    FatherFirstName = parent.FatherFirstName,
                                    FatherMiddleName = parent.FatherMiddleName,
                                    FatherLastName = parent.FatherLastName,
                                    MotherFirstName = parent.MotherFirstName,
                                    MotherMiddleName = parent.MotherMiddleName,
                                    MotherLastName = parent.MotherLastName,
                                    GuardianFirstName = parent.GuardianFirstName,
                                    GuardianMiddleName = parent.GuardianMiddleName,
                                    GuardianLastName = parent.GuardianLastName,
                                    FatherEmailID = parent.FatherEmailID,
                                    MotherEmailID = parent.MotherEmailID,
                                    PhoneNumber = parent.PhoneNumber,
                                    LoginID = parent.LoginID,
                                    Students = new List<StudentDTO>
                                    {
                                        new StudentDTO
                                        {
                                            StudentIID = student.StudentIID,
                                            FirstName = student.FirstName,
                                            MiddleName = student.MiddleName,
                                            LastName = student.LastName
                                        }
                                    },
                                    StudentFullName = student.AdmissionNumber + " - " + student.FirstName + " " + (student.MiddleName ?? "") + (student.LastName ?? ""),
                                    StudentProfilePhoto = student.StudentProfile,
                                    StudentID = student.StudentIID
                                });
                            
                         
                        
                    }
                }

                return parentList;
            }
        }
        public List<GuardianDTO> GetActiveParentListByLoginID(long loginID)
        {
            using (var dbSchoolContext = new dbEduegateSchoolContext())
            {
                var currentAcademicYearStatus = new Domain.Setting.SettingBL(null).GetSettingValue<string>("CURRENT_ACADEMIC_YEAR_STATUSID");
                int? currentAcademicYearStatusID = int.Parse(currentAcademicYearStatus);

                var classIds = new List<int>();
                var sectionIDs = new List<int>();

                var teacher = dbSchoolContext.Employees
                    .Where(e => e.LoginID == loginID)
                    .AsNoTracking()
                    .FirstOrDefault();

                if (teacher == null)
                    return new List<GuardianDTO>();

                var teacherClassMaps = dbSchoolContext.ClassClassTeacherMaps
                    .Where(ct => ct.TeacherID == teacher.EmployeeIID && ct.AcademicYear.AcademicYearStatusID == currentAcademicYearStatusID)
                    .Select(ct => new { ct.ClassID, ct.SectionID })
                    .AsNoTracking()
                    .ToList();

                classIds.AddRange(teacherClassMaps.Select(a => a.ClassID ?? 0));
                sectionIDs.AddRange(teacherClassMaps.Select(a => a.SectionID ?? 0));

                var subjectTeacherClassMaps = dbSchoolContext.ClassTeacherMaps
                    .Where(ct => ct.TeacherID == teacher.EmployeeIID && ct.AcademicYear.AcademicYearStatusID == currentAcademicYearStatusID)
                    .Select(ct => new { ct.ClassID, ct.SectionID })
                    .AsNoTracking()
                    .ToList();

                classIds.AddRange(subjectTeacherClassMaps.Select(a => a.ClassID ?? 0));
                sectionIDs.AddRange(subjectTeacherClassMaps.Select(a => a.SectionID ?? 0));

                var students = dbSchoolContext.Students
                    .Where(s => s.AcademicYear.AcademicYearStatusID == currentAcademicYearStatusID)
                    .Include(s => s.Parent)
                    .AsNoTracking()
                    .AsEnumerable()
                    .Where(s => classIds.Contains(s.ClassID ?? 0) && sectionIDs.Contains(s.SectionID ?? 0))
                    .ToList();

                // Fetch last messages for each parent+student (ReferenceID = StudentID)
                var lastMessages = dbSchoolContext.Comments
                    .Where(c =>
                        (c.ToLoginID == loginID || c.FromLoginID == loginID)
                        && c.FromLoginID != null
                        && c.BroadcastID == null
                        && c.ReferenceID != null // ReferenceID should hold StudentID
                    )
                    .AsNoTracking()
                    .ToList()
                    .GroupBy(c => new {
                        StudentID = c.ReferenceID,
                        ParentLoginID = (c.FromLoginID == loginID ? c.ToLoginID : c.FromLoginID)
                    })
                    .Select(g => g.OrderByDescending(c => c.CreatedDate).FirstOrDefault())
                    .ToList();

                var parentList = new List<GuardianDTO>();

                foreach (var student in students)
                {
                    var parent = student.Parent;
                    if (parent == null) continue;

                    var lastMessage = lastMessages.FirstOrDefault(m =>
                        m.ReferenceID == student.StudentIID &&
                        ((m.FromLoginID == parent.LoginID && m.ToLoginID == loginID) ||
                         (m.FromLoginID == loginID && m.ToLoginID == parent.LoginID)));

                    if (lastMessage != null)
                    {
                        // Unread messages for this parent/student (via ReferenceID)
                        var unreadCount = dbSchoolContext.Comments.Count(c =>
                            c.ReferenceID == student.StudentIID &&
                            c.ToLoginID == loginID &&
                            c.FromLoginID == parent.LoginID &&
                            c.CreatedBy == parent.LoginID &&
                            c.IsRead == false);

                        parentList.Add(new GuardianDTO
                        {
                            ParentIID = parent.ParentIID,
                            GuardianFirstName = parent.GuardianFirstName,
                            GuardianMiddleName = parent.GuardianMiddleName,
                            GuardianLastName = parent.GuardianLastName,
                            FatherFirstName = parent.FatherFirstName,
                            FatherMiddleName = parent.FatherMiddleName,
                            FatherLastName = parent.FatherLastName,
                            LoginID = parent.LoginID,
                            LastMessageText = lastMessage?.Comment1,
                            LastMessageDate = lastMessage?.CreatedDate,
                            UnreadCommentsCount = unreadCount,
                            StudentFullName = student.AdmissionNumber + " - " + student.FirstName + " " + (student.MiddleName ?? "") + (student.LastName ?? ""),
                            StudentID = student.StudentIID,
                            StudentProfilePhoto = student.StudentProfile,


                        });
                    }
                }

                return parentList
                    .OrderByDescending(t => t.LastMessageDate)
                    .ToList();
            }
        }


        public List<StudentChatDTO> GetTeachersWhoMessagedByParentLoginID(long parentLoginID)
        {
            using (var dbERPContext = new dbEduegateERPContext())
            {
                var comments = dbERPContext.Comments
                    .Where(c =>
                        (c.ToLoginID == parentLoginID || c.FromLoginID == parentLoginID)
                        && c.ReferenceID != 0) // Ignore if student ID is 0
                    .Select(c => new
                    {
                        Comment = c.Comment1,
                        CommentIID = c.CommentIID,
                        CreatedDate = c.CreatedDate,
                        FromLoginID = c.FromLoginID,
                        ToLoginID = c.ToLoginID,
                        IsRead = c.IsRead,
                        ReferenceID = c.ReferenceID
                    })
                    .ToList();

                var groupedByStudent = comments
                    .GroupBy(c => c.ReferenceID)
                    .ToList();

                var result = new List<StudentChatDTO>();

                foreach (var studentGroup in groupedByStudent)
                {
                    var studentID = studentGroup.Key;
                    var studentInfo = dbERPContext.Students
                       .Where(s => s.StudentIID == studentID)
                       .Select(s => new
                       {
                           FullName = (s.FirstName ?? "") + " " + (s.MiddleName ?? "") + " " + (s.LastName ?? ""),
                           AdmissionNumber = s.AdmissionNumber
                       })
                       .FirstOrDefault();

                    string studentName = studentInfo?.FullName?.Trim();
                    string admissionNumber = studentInfo?.AdmissionNumber;



                    var teacherMessages = studentGroup
                    .Select(c =>
                    {
                        var otherLoginID = c.FromLoginID == parentLoginID ? c.ToLoginID : c.FromLoginID;
                        return new
                        {
                            Message = c,
                            RelatedLoginID = otherLoginID
                        };
                    })
                    .GroupBy(x => x.RelatedLoginID)
                    .Select(g => g.OrderByDescending(m => m.Message.CreatedDate).FirstOrDefault())
                    .ToList();

                    var teacherMessageList = new List<EmployeeDTO>();

                    foreach (var entry in teacherMessages)
                    {
                        var teacherLoginID = entry.RelatedLoginID;

                        // 🔍 Only include if other party is a teacher
                        var teacher = dbERPContext.Employees
                            .Where(e => e.LoginID == teacherLoginID)
                            .Select(e => new
                            {
                                e.EmployeeName,
                                e.WorkEmail,
                                e.EmployeePhoto,
                                e.EmployeeIID,
                                e.LoginID
                            })
                            .FirstOrDefault();

                        if (teacher != null)
                        {
                            var unreadCount = dbERPContext.Comments.Count(c =>
                                c.ToLoginID == parentLoginID &&
                                c.FromLoginID == teacher.LoginID &&
                                c.IsRead == false &&
                                c.ReferenceID == studentID);

                            teacherMessageList.Add(new EmployeeDTO
                            {
                                CommentIID = entry.Message.CommentIID,
                                ReportingEmployeeID = teacher.LoginID,
                                EmployeeIID = teacher.EmployeeIID,
                                EmployeeName = teacher.EmployeeName,
                                EmployeePhoto = teacher.EmployeePhoto,
                                WorkEmail = teacher.WorkEmail,
                                LastMessageText = entry.Message.Comment,
                                LastMessageDate = entry.Message.CreatedDate,
                                UnreadCommentsCount = unreadCount
                            });
                        }
                    }

                    result.Add(new StudentChatDTO
                    {
                        StudentID = studentID,
                        AdmissionNumber = admissionNumber,
                        StudentName = studentName,
                        TeacherMessages = teacherMessageList.OrderByDescending(t => t.LastMessageDate).ToList()
                    });
                }

                return result;
            }
        }




        public bool? GetIsEnableCommunication(long teacherLoginID)
        {
            using (var dbERPContext = new dbEduegateERPContext())
            {
                // Check if the specific teacher with the given `teacherLoginID` has communication enabled
                var enableCommunication = dbERPContext.Employees
                    .Where(e => e.LoginID == teacherLoginID)
                    .Select(e => e.EnableCommunication)
                    .FirstOrDefault();

                return enableCommunication != false; // true if null or true, false if false
            }
        }

        public async Task<bool?> MarkEnableCommunication(long LoginID, bool enableCommunication)
        {
            long teacherLoginID = LoginID;
            bool EnableCommunication = enableCommunication;
            using (var dbERPContext = new dbEduegateERPContext())
            {
                var teacher = await dbERPContext.Employees.FirstOrDefaultAsync(e => e.LoginID == teacherLoginID);

                if (teacher != null)
                {
                    teacher.EnableCommunication = EnableCommunication;
                    teacher.UpdatedDate = DateTime.Now;
                    await dbERPContext.SaveChangesAsync();
                    return true;
                }
            }
            return false;
        }

        public Dictionary<string, List<GuardianDTO>> GetParentsByTeacherLoginIDGroupedByClass(long loginID)
        {
            using (var dbContext = new dbEduegateSchoolContext())
            {
                // Retrieve the current academic year status
                var currentAcademicYearStatus = new Domain.Setting.SettingBL(null).GetSettingValue<string>("CURRENT_ACADEMIC_YEAR_STATUSID");
                int? currentAcademicYearStatusID = int.Parse(currentAcademicYearStatus);

                // Get the teacher's employee record
                var teacher = dbContext.Employees
                    .Where(e => e.LoginID == loginID)
                    .AsNoTracking()
                    .FirstOrDefault();

                if (teacher == null)
                {
                    return new Dictionary<string, List<GuardianDTO>>(); // Teacher not found
                }

                // Retrieve the class and sections assigned to the teacher
                var teacherClassMaps = dbContext.ClassTeacherMaps
                    .Where(ct => ct.TeacherID == teacher.EmployeeIID && ct.AcademicYear.AcademicYearStatusID == currentAcademicYearStatusID)
                    .Select(ct => new { ct.ClassID, ct.SectionID })
                    .AsNoTracking()
                    .ToList();

                // Fetch all students for the current academic year, grouped by class-section
                var studentsByClassSection = dbContext.Students
                    .Where(s => s.AcademicYear.AcademicYearStatusID == currentAcademicYearStatusID)
                    .Include(s => s.Parent)
                    .AsNoTracking()
                    .AsEnumerable() // Switch to client-side evaluation
                    .Where(s => teacherClassMaps.Any(tc => tc.ClassID == s.ClassID && tc.SectionID == s.SectionID))
                    .GroupBy(s => new { s.ClassID, s.SectionID })
                    .ToList();

                // Prepare the dictionary to hold parents grouped by class-section
                var parentsGroupedByClassSection = new Dictionary<string, List<GuardianDTO>>();

                foreach (var group in studentsByClassSection)
                {
                    // Key for the class-section group
                    string classSectionKey = $"Class {group.Key.ClassID}, Section {group.Key.SectionID}";

                    // Initialize list for each group
                    if (!parentsGroupedByClassSection.ContainsKey(classSectionKey))
                    {
                        parentsGroupedByClassSection[classSectionKey] = new List<GuardianDTO>();
                    }

                    // Add each unique parent to the class-section group
                    foreach (var student in group)
                    {
                        var parent = student.Parent;
                        if (parent != null && !parentsGroupedByClassSection[classSectionKey].Any(p => p.ParentIID == parent.ParentIID))
                        {
                            parentsGroupedByClassSection[classSectionKey].Add(new GuardianDTO
                            {
                                ParentIID = parent.ParentIID,
                                FatherFirstName = parent.FatherFirstName,
                                FatherMiddleName = parent.FatherMiddleName,
                                FatherLastName = parent.FatherLastName,
                                MotherFirstName = parent.MotherFirstName,
                                MotherMiddleName = parent.MotherMiddleName,
                                MotherLastName = parent.MotherLastName,
                                GuardianFirstName = parent.GuardianFirstName,
                                GuardianMiddleName = parent.GuardianMiddleName,
                                GuardianLastName = parent.GuardianLastName,
                                FatherEmailID = parent.FatherEmailID,
                                MotherEmailID = parent.MotherEmailID,
                                PhoneNumber = parent.PhoneNumber,
                                LoginID = parent.LoginID
                            });
                        }
                    }
                }

                return parentsGroupedByClassSection;
            }
        }

        public List<BroadCastDetailsDTO> GetBroadcastDetailsByUserId(long userId)
        {
            using (var dbContext = new dbEduegateERPContext())
            using (var dbSchoolContext = new dbEduegateSchoolContext()) // Assuming you need this for parent/student names
            {
                // Get all broadcast headers created by the user
                var broadcasts = dbContext.BroadCasts
                    .Where(b => b.FromLoginID == userId)
                    .Include(b => b.BroadcastRecipients) // Eager load recipients
                    .OrderByDescending(b => b.CreatedDate)
                    .AsNoTracking()
                    .ToList();

                if (!broadcasts.Any())
                    return new List<BroadCastDetailsDTO>();

                var result = new List<BroadCastDetailsDTO>();

                foreach (var broadcast in broadcasts)
                {
                    var participantsDtoList = new List<BroadcastRecipientsDTO>();
                    foreach (var recipientEntity in broadcast.BroadcastRecipients)
                    {
                        var studentName = "";
                        if (recipientEntity.StudentID.HasValue) // Check if StudentID is present
                        {
                            var student = dbSchoolContext.Students
                                .Where(s => s.StudentIID == recipientEntity.StudentID.Value) // Use StudentIID for lookup
                                .Select(s => new { s.FirstName, s.LastName, s.MiddleName })
                                .FirstOrDefault();
                            if (student != null)
                            {
                                studentName = $"{student.FirstName} {student.MiddleName ?? ""} {student.LastName}".Trim();
                            }
                        }

                        participantsDtoList.Add(new BroadcastRecipientsDTO
                        {
                            ToLoginID = recipientEntity.ToLoginID,       // Parent's LoginID
                            StudentID = recipientEntity.StudentID,       // <<<< CRITICAL: Populate StudentID from the recipient record
                            CreatedDate = recipientEntity.CreatedDate,
                            StudentName = studentName // ✅ assign here
                        });
                    }

                    result.Add(new BroadCastDetailsDTO
                    {
                        BroadcastID = broadcast.BroadcastIID,
                        BroadcastName = broadcast.BroadcastName,
                        FromLoginID = broadcast.FromLoginID,
                        CreatedDate = broadcast.CreatedDate,
                        Participants = participantsDtoList // Assign the list of detailed participants
                    });
                }
                return result;
            }
        }


        public List<ClassTeacherMapDTO> GetClassTeacherClass(long loginID)
        {
            using (var dbContext = new dbEduegateSchoolContext())
            {
                var currentAcademicYearStatus = new Domain.Setting.SettingBL(null).GetSettingValue<string>("CURRENT_ACADEMIC_YEAR_STATUSID");
                int? currentAcademicYearStatusID = int.Parse(currentAcademicYearStatus);

                var classSectionList = new List<ClassTeacherMapDTO>();

                var employee = dbContext.Employees
                    .Where(x => x.LoginID == loginID)
                    .AsNoTracking()
                    .FirstOrDefault();

                if (employee == null) return classSectionList;

                // Get all ClassClassTeacherMaps for the head teacher
                var teacherHeadClassMaps = dbContext.ClassClassTeacherMaps
                    .Where(x => x.TeacherID == employee.EmployeeIID
                                && x.AcademicYear.AcademicYearStatusID == currentAcademicYearStatusID)
                    .Include(x => x.Class)
                    .Include(x => x.Section)
                    .AsNoTracking()
                    .ToList();

                // Only process if head teacher maps are available
                foreach (var classHeadTeacher in teacherHeadClassMaps)
                {
                    if (!classSectionList.Any(x => x.ClassID == classHeadTeacher.ClassID && x.SectionID == classHeadTeacher.SectionID))
                    {
                        classSectionList.Add(new ClassTeacherMapDTO
                        {
                            ClassID = classHeadTeacher.ClassID,
                            SectionID = classHeadTeacher.SectionID,
                            ClassName = classHeadTeacher.ClassID.HasValue ? classHeadTeacher.Class.ClassDescription : null,
                            SectionName = classHeadTeacher.SectionID.HasValue ? classHeadTeacher.Section.SectionName : null,
                            ClassOrderNumber = classHeadTeacher.ClassID.HasValue ? classHeadTeacher.Class.ORDERNO : null,
                        });
                    }
                }

                if (classSectionList.Count > 0)
                {
                    classSectionList = classSectionList
                        .OrderBy(y => y.ClassOrderNumber)
                        .ThenBy(y => y.SectionName)
                        .ToList();
                }

                return classSectionList;
            }
        }

        public List<KeyValueDTO> GetTeacherByClassAndSubject(string classIDs, string subjectIDs)
        {
            var result = new List<KeyValueDTO>();

            var convClassIDs = classIDs.Split(',').Select(int.Parse).ToList();
            var convSubjectIDs = subjectIDs.Split(',').Select(int.Parse).ToList();

            using (var dbContext = new dbEduegateSchoolContext())
            {
                var teachers = dbContext.ClassTeacherMaps
                    .Where(a => convClassIDs.Contains(a.ClassID ?? 0) && convSubjectIDs.Contains(a.SubjectID ?? 0) && a.AcademicYearID == _context.AcademicYearID && a.SchoolID == _context.SchoolID)
                    .Include(i => i.Employee2)
                    .ToList();

                result.AddRange(teachers.Select(teacher => new KeyValueDTO
                {
                    Key = teacher.TeacherID.ToString(), 
                        Value = teacher.Employee2?.EmployeeCode + " - " + teacher.Employee2?.FirstName + " " + teacher.Employee2?.MiddleName + " " + teacher.Employee2?.LastName,
                    }));
                }

            return result;
        }

    }
}