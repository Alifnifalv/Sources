using Eduegate.Integrations.Engine.DbContexts;
using Eduegate.Integrations.Engine.DbContexts.Models;
using Eduegate.Integrations.Engine.Helper;
using Eduegate.Services.Contracts.School.Students;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Eduegate.Integrations.Adapters.BMS
{
    public class StudentMigrator : IStudentMigration
    {
        public List<StudentDTO> GetStudents()
        {
            List<Student> students = new List<Student>();
            var studentDto = new List<StudentDTO>();
            try
            {
                using (var dbContext = new IntegrationDbContext())
                {
                    //string qry = @" select sims_student_admission_number  as 'AdmissionNumber',sims_student_enroll_number as 'RollNumber',sims_student_passport_first_name_en as 'FirstName',sims_student_passport_middle_name_en as 'MiddleName', 
                    //                                    sims_student_passport_last_name_en as 'LastName', sims_student_gender 'GenderID',sims_student_dob as 'DateOfBirth',sims_student_ethnicity_code as  'CastID', " +
                    //                                  " sims_student_religion_code as 'RelegionID',sims_student_emergency_contact_name1 as  'MobileNumber',sims_student_birth_country_code as 'CurrentCountryID', " +
                    //                                  " sims_student_date as 'FeeStartDate',sims_student_passport_number as 'PassportNo', " +
                    //                                  "  sims_student_passport_issue_place as 'CountryofIssueID',sims_student_passport_expiry_date as 'PassportNoExpiry',sims_student_birth_country_code as 'CountryofBirthID', " +
                    //                                  "  sims_student_visa_number as 'VisaNo',sims_student_visa_expiry_date as 'VisaExpiry',sims_student_national_id as 'NationalIDNo', " +
                    //                                  " sims_student_national_id_expiry_date as 'NationalIDNoExpiry',sims_student_national_id_issue_date as 'StudentNationalIDNoIssueDate',sims_student_passport_issue_date as 'StudentPassportNoIssueDate'," +
                    //                                  "  c.sims_grade_name_en as 'Class', d.sims_section_name_en as 'Section' ,cu.sims_country_name_en as 'Country',	r.sims_religion_name_en as 'Religion'   from sims.sims_student_section a inner " +
                    //                                  "  join sims.sims_student b on a.sims_enroll_number = b.sims_student_enroll_number inner join sims.sims_grade c on a.sims_academic_year=c.sims_academic_year      and " +
                    //                                  "  a.sims_cur_code = c.sims_cur_code and a.sims_grade_code = c.sims_grade_code " +
                    //                                  "  inner join sims.sims_section d on a.sims_grade_code = d.sims_grade_code and a.sims_section_code = d.sims_section_code " +
                    //                                  " and a.sims_academic_year = d.sims_academic_year and a.sims_cur_code = d.sims_cur_code " +
                    //                                  " left join sims.sims_religion r on b.sims_student_religion_code = r.sims_religion_code " +
                    //                                  " left join sims.sims_country cu on b.sims_student_birth_country_code = cu.sims_country_code " +
                    //                                  "  where a.sims_academic_year in (2021) ";
                    string qry = @"select 
                                [sims_student_enroll_number]                                
                               
                                ,[sims_student_img]
                              
                                from [sims].[sims_student] S
                             ";
                    // var result = dbContext.Students.FromSqlRaw(qry).ToList();


                    students = dbContext.Students.FromSqlRaw(qry).ToList();
                }

                var studentPassportDetails = new StudentPassportDetailDTO();
                studentPassportDetails.NationalityID = 1;
                studentPassportDetails.CountryofIssueID = 1;
                studentPassportDetails.CountryofIssueID = 1;
                studentPassportDetails.CountryofBirthID = 1;

                foreach (var student in students)
                {
                    studentDto.Add(new StudentDTO()
                    {
                        AdmissionNumber = student.sims_student_enroll_number,
                        //FirstName = student.sims_student_passport_first_name_en,
                        //MiddleName = student.sims_student_passport_middle_name_en,
                        //RollNumber = student.sims_student_enroll_number,
                        ////ApplicationID = student.ApplicationID,
                        //ClassID = GetClassByName(student.Class),
                        //SectionID = GetSectionByName(student.Section),
                        //RelegionID = (student.Religion != null && student.Religion != string.Empty) ? (byte?)GetReligionByName(student.Religion) : (byte?)null,

                        //GenderID = student.sims_student_gender == string.Empty ? (byte?)null : ((student.sims_student_gender == "M") ? (byte?)1 : (byte?)2),
                        //DateOfBirth = student.sims_student_dob == null ? (DateTime?)null : (DateTime?)student.sims_student_dob,

                        ////RelegionID = student.RelegionID,
                        //MobileNumber = student.sims_student_emergency_contact_number1,
                        ////EmailID = student.EmailID,
                        //AdmissionDate = student.sims_student_date,
                        //StudentProfile = student.StudentProfile,
                        //BloodGroupID = student.BloodGroupID,
                        //StudentHouseID = student.StudentHouseID,
                        //Height = student.Height,
                        //Weight = student.Weight,
                        //AsOnDate = student.sims_student_date == null ? new DateTime(2000, 1, 1) : (DateTime?)student.sims_student_dob,

                        //FeeStartDate = student.sims_student_date == null ? new DateTime(2000, 1, 1) : (DateTime?)student.sims_student_dob,
                        //IsActive = true,//student.IsActive,
                        //StudentPassportDetails = studentPassportDetails,
                        StudentProfile = student.sims_student_img
                        // studentPassportDetails.
                        //ParentID = student.ParentID,
                        //LoginID = student.LoginID,
                        //PermenentBuildingNo = student.AdditionalInfo.PermenentBuildingNo,
                        //PermenentFlatNo = student.AdditionalInfo.PermenentFlatNo,
                        //PermenentStreetNo = student.AdditionalInfo.PermenentStreetNo,
                        //PermenentStreetName = student.AdditionalInfo.PermenentStreetName,
                        //PermenentLocationNo = student.AdditionalInfo.PermenentLocationNo,
                        //PermenentLocationName = student.AdditionalInfo.PermenentLocationName,
                        //PermenentZipNo = student.AdditionalInfo.PermenentZipNo,
                        //PermenentPostBoxNo = student.AdditionalInfo.PermenentPostBoxNo,
                        //PermenentCity = student.AdditionalInfo.PermenentCity,
                        //PermenentCountryID = 1,
                        //IsAddressIsCurrentAddress = student.AdditionalInfo.IsCurrentAddresIsGuardian,
                        //IsAddressIsPermenentAddress = student.AdditionalInfo.IsPermenentAddresIsCurrent,

                        //IsOnlyChildofParent = student.IsOnlyChildofParent,
                        //IsStudentStudiedBefore = student.IsStudentStudiedBefore,
                        //PrimaryContactID = student.PrimaryContactID,
                        //CommunityID = student.CommunityID,
                        //SecoundLanguageID = student.SecoundLanguageID,
                        //ThridLanguageID = student.ThridLanguageID,
                        //PreviousSchoolName = student.PreviousSchoolName,
                        //PreviousSchoolAcademicYear = student.PreviousSchoolAcademicYear,
                        //PreviousSchoolAddress = student.PreviousSchoolAddress,
                        //PreviousSchoolClassCompletedID = student.PreviousSchoolClassCompletedID,
                        //PreviousSchoolSyllabusID = student.PreviousSchoolSyllabusID,
                    });
                }
            }
            catch (Exception ex)
            {

            }
            return studentDto;

        }

        public int? GetClassByName(string className)
        {
            using (var dbContext = new EduegateDbContext())
            {
                return dbContext.Classes.FromSqlRaw(string.Format("Select ClassID, ClassDescription from schools.Classes where ClassDescription='{0}'", className)).FirstOrDefault().ClassID;
            }
        }
        public int? GetSectionByName(string section)
        {
            using (var dbContext = new EduegateDbContext())
            {
                return dbContext.Sections.FromSqlRaw(string.Format("Select SectionID,SectionName from schools.Sections where SectionName='{0}'", section)).FirstOrDefault().SectionID;
            }
        }

        public int? GetReligionByName(string religion)
        {
            using (var dbContext = new EduegateDbContext())
            {
                return dbContext.Religion.FromSqlRaw(string.Format("Select [RelegionID],RelegionDescription from schools.Relegions where [RelegionDescription]='{0}'", religion)).FirstOrDefault().RelegionID;
            }
        }

        //public int? GetCountryByName(string country)
        //{
        //    using (var dbContext = new EduegateDbContext())
        //    {
        //        return dbContext.Sections.FromSqlRaw(string.Format("Select CountryID from [mutual].[Countries] where CountryName='{0}'", country)).FirstOrDefault().SecionID;
        //    }
        //}

        //public int? GetNationalityByName(string classCode)
        //{
        //    using (var dbContext = new EduegateDbContext())
        //    {
        //        return dbContext.Sections.FromSqlRaw(string.Format("Select SectionID from school.Sections where SectionID={0}", classCode)).FirstOrDefault().SecionID;
        //    }
        //}
    }
}
