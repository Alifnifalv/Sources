using Eduegate.Domain.Entity.Models.Inventory;
using Eduegate.Domain.Entity.School.Models;
using Eduegate.Domain.Entity.School.Models.School;
using Eduegate.Domain.Repository.Frameworks;
using Eduegate.Framework;
using Eduegate.Framework.Contracts.Common;
using Eduegate.Frameworks.DynamicEntityMapper.Mappers;
using Eduegate.Services.Contracts.School.Exams;
using Microsoft.EntityFrameworkCore;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;

namespace Eduegate.Domain.Mappers.School.Exams
{
    public class RemarksEntryMapper : DTOEntityDynamicMapper
    {
        public static RemarksEntryMapper Mapper(CallContext context)
        {
            var mapper = new RemarksEntryMapper();
            mapper._context = context;
            return mapper;
        }

        public override BaseMasterDTO ToDTO(string entity)
        {
            return JsonConvert.DeserializeObject<RemarksEntryDTO>(entity);
        }

        public override string ToDTOString(BaseMasterDTO dto)
        {
            return dto == null ? null : JsonConvert.SerializeObject(dto);
        }
        public override string GetEntity(long IID)
        {
            return ToDTOString(ToDTO(IID));
        }

        private RemarksEntryDTO ToDTO(long IID)
        {
            using (dbEduegateSchoolContext dbContext = new dbEduegateSchoolContext())
            {
                var entity = dbContext.RemarksEntries.Where(X => X.RemarksEntryIID == IID)
                    .Include(i => i.Class)
                    .Include(i => i.Section)
                    .Include(i => i.ExamGroup)
                    .Include(i => i.RemarksEntryStudentMaps).ThenInclude(i => i.Student)
                    .Include(i => i.RemarksEntryStudentMaps).ThenInclude(i => i.RemarksEntryExamMaps).ThenInclude(i => i.Exam)
                    .Include(i => i.RemarksEntryStudentMaps).ThenInclude(i => i.RemarksEntryExamMaps).ThenInclude(i => i.Subject)
                    .AsNoTracking()
                    .FirstOrDefault();

                var remark = new RemarksEntryDTO()
                {
                    RemarksEntryIID = entity.RemarksEntryIID,
                    ClassID = entity.ClassID.HasValue ? entity.ClassID : null,
                    SectionID = entity.SectionID.HasValue ? entity.SectionID : null,
                    Class = entity.ClassID.HasValue ? new KeyValueDTO() { Key = entity.ClassID.ToString(), Value = entity.Class != null ? entity.Class.ClassDescription : null } : new KeyValueDTO(),
                    Section = entity.SectionID.HasValue ? new KeyValueDTO() { Key = entity.SectionID.ToString(), Value = entity.Section != null ? entity.Section.SectionName : null } : new KeyValueDTO(),
                    ExamGroupID = entity.ExamGroupID.HasValue ? entity.ExamGroupID : null,
                    ExamGroupName = entity.ExamGroupID.HasValue || entity.ExamGroup != null ? entity.ExamGroup.ExamGroupName : null,
                    CreatedBy = entity.CreatedBy,
                    CreatedDate = entity.CreatedDate,
                    UpdatedBy = entity.UpdatedBy,
                    UpdatedDate = entity.UpdatedDate,
                };

                var lstStudentData = entity.RemarksEntryStudentMaps.OrderBy(x => x.StudentID).ToList();
                foreach (var studMap in lstStudentData)
                {
                    //var remarkExamMaps = new List<RemarksEntryExamMapDTO>();

                    //foreach (var examMap in studMap.RemarksEntryExamMaps)
                    //{
                    //    if (examMap.subjectID.HasValue)
                    //    {
                    //        remarkExamMaps.Add(new RemarksEntryExamMapDTO()
                    //        {
                    //            RemarksEntryExamMapIID = examMap.RemarksEntryExamMapIID,
                    //            ExamID = examMap.ExamID.HasValue ? examMap.ExamID : null,
                    //            SubjectID = examMap.subjectID.HasValue ? examMap.subjectID : null,
                    //            Remarks = examMap.Remarks != null ? examMap.Remarks : null,
                    //            ExamName = examMap.ExamID.HasValue || examMap.Exam != null ? examMap.Exam.ExamDescription : null,
                    //            SubjectName = examMap.subjectID.HasValue || examMap.Subject != null ? examMap.Subject.SubjectName : null,
                    //        });
                    //    }
                    //}

                    remark.StudentsRemarks.Add(new RemarksEntryStudentsDTO()
                    {
                        RemarksEntryStudentMapIID = studMap.RemarksEntryStudentMapIID,
                        StudentID = studMap.StudentID.HasValue ? studMap.StudentID : null,
                        StudentName = studMap.StudentID.HasValue || studMap.Student != null ? studMap.Student.AdmissionNumber + " - " + studMap.Student.FirstName + "  " + studMap.Student.MiddleName + "  " + studMap.Student.LastName : null,
                        Remarks1 = studMap.Remarks1 != null ? studMap.Remarks1 : null,
                        //Remarks2 = studMap.Remarks2 != null ? studMap.Remarks2 : null,
                        //RemarksExam = remarkExamMaps,
                    });
                }

                return remark;
            }
        }

        public override string SaveEntity(BaseMasterDTO dto)
        {
            var toDto = dto as RemarksEntryDTO;

            //convert the dto to entity and pass to the repository.
            using (var dbContext = new dbEduegateSchoolContext())
            {
                var entity = new RemarksEntry()
                {
                    RemarksEntryIID = toDto.RemarksEntryIID,
                    ClassID = toDto.ClassID,
                    SectionID = toDto.SectionID,
                    ExamGroupID = toDto.ExamGroupID,
                    TeacherID = _context.EmployeeID.HasValue ? _context.EmployeeID : null,
                    SchoolID = _context.SchoolID.HasValue ? Convert.ToByte(_context.SchoolID) : (byte?)null,
                    AcademicYearID = _context.AcademicYearID.HasValue ? _context.AcademicYearID : null,
                    CreatedBy = toDto.RemarksEntryIID == 0 ? Convert.ToInt32(_context.LoginID) : toDto.CreatedBy,
                    CreatedDate = toDto.RemarksEntryIID == 0 ? DateTime.Now : toDto.CreatedDate,
                    UpdatedBy = toDto.RemarksEntryIID != 0 ? Convert.ToInt32(_context.LoginID) : toDto.UpdatedBy,
                    UpdatedDate = toDto.RemarksEntryIID != 0 ? DateTime.Now : toDto.UpdatedDate,
                };

                foreach (var Det in toDto.StudentsRemarks)
                {
                    //var remarkExm = new List<RemarksEntryExamMap>();

                    //foreach (var Exm in Det.RemarksExam)
                    //{
                    //    if (Exm.SubjectID.HasValue)
                    //    {
                    //        remarkExm.Add(new RemarksEntryExamMap()
                    //        {
                    //            RemarksEntryExamMapIID = Exm.RemarksEntryExamMapIID,
                    //            RemarksEntryStudentMapID = Det.RemarksEntryStudentMapIID,
                    //            ExamID = Exm.ExamID,
                    //            subjectID = Exm.SubjectID,
                    //            Remarks = Exm.Remarks,
                    //        });
                    //    }
                    //}

                    entity.RemarksEntryStudentMaps.Add(new RemarksEntryStudentMap()
                    {
                        RemarksEntryStudentMapIID = Det.RemarksEntryStudentMapIID,
                        StudentID = Det.StudentID,
                        Remarks1 = Det.Remarks1,
                        //Remarks2 = Det.Remarks2,
                        //RemarksEntryExamMaps = remarkExm,
                    });

                }

                //Check already exist data when new create remark entry 
                if (entity.RemarksEntryIID == 0)
                {
                    var getDataRemarks = dbContext.RemarksEntries.Where(X => X.ClassID == toDto.ClassID && X.SectionID == toDto.SectionID && X.ExamGroupID == toDto.ExamGroupID && X.AcademicYearID == _context.AcademicYearID)
                        .AsNoTracking().FirstOrDefault();

                    if (getDataRemarks != null)
                    {
                        throw new Exception("For this Class,Section,Term data already exist. Please use Edit option!");
                    }
                }

                dbContext.RemarksEntries.Add(entity);
                if (entity.RemarksEntryIID == 0)
                {
                    dbContext.Entry(entity).State = Microsoft.EntityFrameworkCore.EntityState.Added;
                }
                else
                {
                    foreach (var studentMap in entity.RemarksEntryStudentMaps)
                    {
                        if (studentMap.RemarksEntryStudentMapIID != 0)
                        {
                            dbContext.Entry(studentMap).State = Microsoft.EntityFrameworkCore.EntityState.Modified;
                        }
                        else
                        {
                            dbContext.Entry(studentMap).State = Microsoft.EntityFrameworkCore.EntityState.Added;
                        }

                        //foreach (var examMap in studentMap.RemarksEntryExamMaps)
                        //{
                        //    if (examMap.RemarksEntryStudentMapID == 0)
                        //    {
                        //        examMap.RemarksEntryStudentMapID = studentMap.RemarksEntryStudentMapIID;
                        //    }

                        //    if (examMap.RemarksEntryExamMapIID != 0)
                        //    {
                        //        dbContext.Entry(examMap).State = Microsoft.EntityFrameworkCore.EntityState.Modified;
                        //    }
                        //    else
                        //    {
                        //        dbContext.Entry(examMap).State = Microsoft.EntityFrameworkCore.EntityState.Added;
                        //    }
                        //}

                    }

                    dbContext.Entry(entity).State = Microsoft.EntityFrameworkCore.EntityState.Modified;
                }
                dbContext.SaveChanges();

                return ToDTOString(ToDTO(entity.RemarksEntryIID));
            }
        }

        public List<RemarksEntryStudentsDTO> GetClasswiseRemarksEntryStudentData(int classId, int sectionId, int examGroupID)
        {
            using (var dbContext = new dbEduegateSchoolContext())
            {
                var classStudentList = new List<RemarksEntryStudentsDTO>();

                var remarksMapData = dbContext.RemarksEntryStudentMaps
                    .Where(x => x.RemarksEntry.ClassID == classId && x.RemarksEntry.SectionID == sectionId && x.RemarksEntry.AcademicYearID == _context.AcademicYearID && x.RemarksEntry.ExamGroupID == examGroupID)
                    .AsNoTracking().ToList();

                var students = dbContext.Students
                    .Where(a => a.ClassID == classId && (sectionId == 0 || a.SectionID == sectionId) && a.IsActive == true && a.SchoolID == _context.SchoolID && a.AcademicYearID == _context.AcademicYearID && a.Status == 1)
                    .OrderBy(z => z.AdmissionNumber)
                    .AsNoTracking().ToList();

                foreach (var classStud in students)
                {
                    var filterDetails = remarksMapData.Count > 0 ? remarksMapData.Find(x => x.StudentID == classStud.StudentIID) : null;
                    classStudentList.Add(new RemarksEntryStudentsDTO
                    {
                        StudentID = classStud.StudentIID,
                        StudentName = classStud.AdmissionNumber + " - " + classStud.FirstName + " " + classStud.MiddleName + " " + classStud.LastName,
                        RemarksEntryStudentMapIID = filterDetails != null ? filterDetails.RemarksEntryStudentMapIID : 0,
                        Remarks1 = filterDetails != null ? filterDetails.Remarks1 : null,
                    });
                }

                return classStudentList;
            }
        }

        public List<StudentBehavioralRemarksDTO> GetStudentBehavioralRemarks(StudentBehavioralRemarksDTO studentBehavioralRemarksDTO)
        {
            var lststudentsBehavioralRemarks = new List<StudentBehavioralRemarksDTO>();
            var dateFormat = new Domain.Setting.SettingBL().GetSettingValue<string>("DateFormat", 0, "dd/MM/yyyy");

            using (SqlConnection conn = new SqlConnection(Infrastructure.ConfigHelper.GetDefaultConnectionString()))
            using (SqlCommand cmd = new SqlCommand("[schools].[Student_Behavioral_Remarks_Report_view]", conn))
            {
                SqlDataAdapter adapter = new SqlDataAdapter(cmd);
                adapter.SelectCommand.CommandType = CommandType.StoredProcedure;

                adapter.SelectCommand.Parameters.Add(new SqlParameter("@STUDENT_IDs", SqlDbType.Int));
                adapter.SelectCommand.Parameters["@STUDENT_IDs"].Value = studentBehavioralRemarksDTO.StudentID;

                adapter.SelectCommand.Parameters.Add(new SqlParameter("@CLASS_IDs", SqlDbType.NVarChar));
                adapter.SelectCommand.Parameters["@CLASS_IDs"].Value = studentBehavioralRemarksDTO.ClassID;

                adapter.SelectCommand.Parameters.Add(new SqlParameter("@ACDEMICYEAR_IDs", SqlDbType.NVarChar));
                adapter.SelectCommand.Parameters["@ACDEMICYEAR_IDs"].Value = studentBehavioralRemarksDTO.AcademicYearID;

                adapter.SelectCommand.Parameters.Add(new SqlParameter("@SECTION_IDs", SqlDbType.NVarChar));
                adapter.SelectCommand.Parameters["@SECTION_IDs"].Value = studentBehavioralRemarksDTO.SectionID;

                DataSet dt = new DataSet();
                adapter.Fill(dt);
                DataTable dataTable = null;

                if (dt.Tables.Count > 0)
                {
                    if (dt.Tables[0].Rows.Count > 0)
                    {
                        dataTable = dt.Tables[0];
                    }
                }
                if (dataTable != null)
                {
                    foreach (DataRow row in dataTable.Rows)
                    {
                        String stringDateFrom = row.ItemArray[6].ToString();
                        String stringDateTo = row.ItemArray[7].ToString();

                        StudentBehavioralRemarksDTO studentsBehavioralRemarks = new StudentBehavioralRemarksDTO();
                        studentsBehavioralRemarks.StudentID = Convert.ToInt64(row.ItemArray[0]);
                        studentsBehavioralRemarks.StudentName = row.ItemArray[1].ToString();
                        studentsBehavioralRemarks.Teacher = row.ItemArray[2].ToString();
                        studentsBehavioralRemarks.Class = row.ItemArray[3].ToString();
                        studentsBehavioralRemarks.Section = row.ItemArray[4].ToString();
                        studentsBehavioralRemarks.Remarks = row.ItemArray[5].ToString();
                        studentsBehavioralRemarks.AcademicYear = row.ItemArray[6].ToString();
                        lststudentsBehavioralRemarks.Add(studentsBehavioralRemarks);
                    }
                }
                return lststudentsBehavioralRemarks;

            }
        }

    }
}