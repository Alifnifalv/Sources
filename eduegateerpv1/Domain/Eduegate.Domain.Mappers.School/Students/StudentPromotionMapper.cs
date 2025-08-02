using Newtonsoft.Json;
using Eduegate.Domain.Entity.School.Models;
using Eduegate.Domain.Entity.School.Models.School;
using Eduegate.Domain.Repository.Frameworks;
using Eduegate.Frameworks.DynamicEntityMapper.Mappers;
using Eduegate.Framework.Contracts.Common;
using Eduegate.Services.Contracts.School.Fees;
using Eduegate.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using Eduegate.Framework.Extensions;
using Eduegate.Domain.Repository;
using Eduegate.Services.Contracts.School.Students;
using System.Data.SqlClient;
using System.Data;
using Eduegate.Services.Contracts.School.Exams;
using Microsoft.EntityFrameworkCore;

namespace Eduegate.Domain.Mappers.School.Students
{
    public class StudentPromotionMapper : DTOEntityDynamicMapper
    {
        public static StudentPromotionMapper Mapper(CallContext context)
        {
            var mapper = new StudentPromotionMapper();
            mapper._context = context;
            return mapper;
        }

        public override BaseMasterDTO ToDTO(string entity)
        {
            return JsonConvert.DeserializeObject<StudentPromotionDTO>(entity);
        }

        public override string ToDTOString(BaseMasterDTO dto)
        {
            return dto == null ? null : JsonConvert.SerializeObject(dto);
        }

        public override string SaveEntity(BaseMasterDTO dto)
        {
            string message = "Saved Successfully";
            var toDto = dto as StudentPromotionDTO;

            #region Validation

            if (toDto.ShiftFromAcademicYearID == 0)
            {
                throw new Exception("Please select Acadamic Year!");
            }

            //Validation check for 'fromclass' and 'toclass'
            //'fromclass' and 'toclass' are multi select so validation only set for single selection cases -- TaskID 

            if (toDto.Student.Count > 0 && string.IsNullOrEmpty(toDto.Remarks))
            {
                throw new Exception("Remarks cannot be left empty!");
            }
            if (toDto.PromotionStatusID == null)
            {
                throw new Exception("Please select any Promotion Status!");
            }
            #endregion


            #region Filteration
            bool _sIsSucces = false;

            string _promoteStudent_IDs = string.Empty;
            if (toDto.PromoteStudent != null && toDto.PromoteStudent.Any())
                _promoteStudent_IDs = string.Join(",", toDto.PromoteStudent.Select(w => w.Key));

            string _sStudent_IDs = string.Empty;
            if (toDto.Student != null && toDto.Student.Any())
                _sStudent_IDs = string.Join(",", toDto.Student.Select(w => w.Key));


            string _sClass_IDs = string.Empty;
            if (toDto.ShiftFromClass != null && toDto.ShiftFromClass.Any())
                _sClass_IDs = string.Join(",", toDto.ShiftFromClass.Select(w => w.Key));


            string _sSection_IDs = string.Empty;
            if (toDto.ShiftFromSection != null && toDto.ShiftFromSection.Any())
                _sSection_IDs = string.Join(",", toDto.ShiftFromSection.Select(w => w.Key));

            string _sClass_IDTos = string.Empty;
            if (toDto.Class.Any())
                if (toDto.Class != null && toDto.Class.Any())
                    _sClass_IDTos = string.Join(",", toDto.Class.Select(w => w.Key));

            string _sSection_IDTos = string.Empty;
            if (toDto.Section.Any())
                if (toDto.Section != null && toDto.Section.Any())
                    _sSection_IDTos = string.Join(",", toDto.Section.Select(w => w.Key));
            toDto.ClassID = int.Parse(toDto.Class.Select(x => x.Key).FirstOrDefault());
            toDto.SectionID = toDto.Section != null && toDto.Section.Count > 0 ? int.Parse(toDto.Section.Select(x => x.Key).FirstOrDefault()) : 0;
            #endregion

            SqlConnectionStringBuilder _sBuilder = new SqlConnectionStringBuilder(Infrastructure.ConfigHelper.GetSchoolConnectionString());
            _sBuilder.ConnectTimeout = 30; // Set Timedout
            using (SqlConnection conn = new SqlConnection(_sBuilder.ConnectionString))
            {
                try { conn.Open(); }
                catch (Exception ex)
                {
                    message = ex.Message; throw new Exception(message);
                }
                using (SqlCommand sqlCommand = new SqlCommand("[schools].[SPS_STUDENT_PROMOTION]", conn))
                {
                    sqlCommand.CommandType = CommandType.StoredProcedure;

                    sqlCommand.Parameters.Add(new SqlParameter("@SCHOOLID", SqlDbType.TinyInt));
                    sqlCommand.Parameters["@SCHOOLID"].Value = toDto.SchoolID;

                    sqlCommand.Parameters.Add(new SqlParameter("@SHIFTFROMSCHOOLID", SqlDbType.TinyInt));
                    sqlCommand.Parameters["@SHIFTFROMSCHOOLID"].Value = toDto.ShiftFromSchoolID;

                    sqlCommand.Parameters.Add(new SqlParameter("@ACADEMICYEARID", SqlDbType.Int));
                    sqlCommand.Parameters["@ACADEMICYEARID"].Value = toDto.AcademicYearID;

                    sqlCommand.Parameters.Add(new SqlParameter("@SHIFTFROMACADEMICYEARID", SqlDbType.Int));
                    sqlCommand.Parameters["@SHIFTFROMACADEMICYEARID"].Value = toDto.ShiftFromAcademicYearID;

                    sqlCommand.Parameters.Add(new SqlParameter("@CLASSIDs", SqlDbType.Int));
                    sqlCommand.Parameters["@CLASSIDs"].Value = toDto.ClassID;

                    sqlCommand.Parameters.Add(new SqlParameter("@SECTIONIDs", SqlDbType.Int));
                    sqlCommand.Parameters["@SECTIONIDs"].Value = toDto.SectionID;

                    sqlCommand.Parameters.Add(new SqlParameter("@SHIFTFROMCLASSIDs", SqlDbType.VarChar));
                    sqlCommand.Parameters["@SHIFTFROMCLASSIDs"].Value = _sClass_IDs;

                    sqlCommand.Parameters.Add(new SqlParameter("@SHIFTFROMSECTIONIDs", SqlDbType.VarChar));
                    sqlCommand.Parameters["@SHIFTFROMSECTIONIDs"].Value = _sSection_IDs;

                    sqlCommand.Parameters.Add(new SqlParameter("@PromoteSTUDENTIDs", SqlDbType.VarChar));
                    sqlCommand.Parameters["@PromoteSTUDENTIDs"].Value = _promoteStudent_IDs;

                    sqlCommand.Parameters.Add(new SqlParameter("@STUDENTIDs", SqlDbType.VarChar));
                    sqlCommand.Parameters["@STUDENTIDs"].Value = _sStudent_IDs;

                    sqlCommand.Parameters.Add(new SqlParameter("@REMARKS", SqlDbType.VarChar));
                    sqlCommand.Parameters["@REMARKS"].Value = toDto.Remarks;

                    sqlCommand.Parameters.Add(new SqlParameter("@PROMOTIONSTATUS", SqlDbType.TinyInt));
                    sqlCommand.Parameters["@PROMOTIONSTATUS"].Value = toDto.PromotionStatusID;

                    sqlCommand.Parameters.Add(new SqlParameter("@ISPROMOTED", SqlDbType.Bit));
                    sqlCommand.Parameters["@ISPROMOTED"].Value = toDto.IsPromoted;


                    try
                    {
                        // Run the stored procedure.
                        message = Convert.ToString(sqlCommand.ExecuteScalar() ?? string.Empty);

                    }
                    catch (Exception ex)
                    {
                        var errorMessage = ex.Message.ToLower().Contains("inner") && ex.Message.ToLower().Contains("exception")
                        ? ex.InnerException?.Message : ex.Message;

                        Eduegate.Logger.LogHelper<string>.Fatal($"Student Promotions. Error message: {errorMessage}", ex);

                        throw new Exception("Error on Saving");
                    }
                    finally
                    {
                        conn.Close();
                    }
                }
            }
            string[] resp = message.Split('#');

            if (resp != null && resp.Length > 1)
                return ToDTOString(toDto);
            else
                throw new Exception(resp[0]);


        }

        public List<KeyValueDTO> GetProgressReportIDsByStudPromStatus(int classID, int sectionID, long academicYearID, byte statusID, int examID, int examGroupID)
        {
            var studList = new List<long>();
            var reportsList = new List<KeyValueDTO>();

            using (var dbContext = new dbEduegateSchoolContext())
            {
                var studPromDetails = dbContext.StudentPromotionLogs.Where(a => a.ShiftFromClassID == classID && a.ShiftFromSectionID == sectionID && a.ShiftFromAcademicYearID == academicYearID && a.PromotionStatusID == statusID).AsNoTracking().ToList();

                if (studPromDetails != null)
                {
                    studList.AddRange(studPromDetails.Select(a => a.StudentID));
                }

                reportsList = GetProgressReportDetails(studList, classID, sectionID, academicYearID, examID, examGroupID);

            }

            return reportsList;
        }

        public List<KeyValueDTO> GetProgressReportDetails(List<long> studentIDs, int classID, int sectionID, long academicYearID, int examID, int examGroupID)
        {
            //var reportsList = new List<long>();
            var prgrsReportDtls = new List<KeyValueDTO>();

            using (var dbContext = new dbEduegateSchoolContext())
            {
                var progressReportData = dbContext.ProgressReports.Where(a => a.ClassID == classID && a.SectionID == sectionID && a.AcademicYearID == academicYearID && a.ExamID == examID && a.ExamGroupID == examGroupID 
                && studentIDs.Contains(a.StudentId??0))
                    .Include(i => i.Student).ThenInclude(i => i.StudentPassportDetails)
                    .AsNoTracking().ToList();

                if (progressReportData != null)
                {
                    foreach (var details in progressReportData)
                    {
                        prgrsReportDtls.Add(new KeyValueDTO()
                        {
                            Key = details.ReportContentID.ToString(),
                            Value = details.Student.StudentPassportDetails.Select(a => a.NationalIDNo).FirstOrDefault(),
                        });
                    }
                }
            }

            return prgrsReportDtls;
        }

    }
}
