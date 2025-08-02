using Eduegate.PublicAPI.Models;
using Eduegate.Services.Client.Factory;
using Eduegate.Services.Contracts.Leads;
using System;
using System.Web.Http;
using System.Web.Http.Cors;

namespace Eduegate.PublicAPI.Controllers
{
    [EnableCors(origins: "*", headers: "*", methods: "*")]
    public class CRMController : ApiController
    {
        [HttpPost]
        public IHttpActionResult AddLead([FromBody] LeadInfo leadInfo)
        {
            try
            {
                if (!ModelState.IsValid)
                {
                    return BadRequest();
                }

                var rtnValue = ClientFactory.CRMServiceClient(null).AddLead(new LeadDTO()
                {
                    LeadTypeID = 1,
                    DateOfBirth = Convert.ToDateTime(leadInfo.Dob1),
                    MobileNumber = leadInfo.Mobile,
                    ParentName = leadInfo.Pname,
                    GenderID = leadInfo.Gender == "M" ? (byte?)1 : (byte?)2,
                    EmailAddress = leadInfo.Email,
                    ClassName = leadInfo.Grade,
                    ClassID = int.Parse(leadInfo.Grade),
                    StudentName = leadInfo.Sname,
                    AcademicYear = leadInfo.Academic_year,
                    AcademicYearID = int.Parse(leadInfo.Academic_year),
                    NationalityID = int.Parse(leadInfo.Nationality),
                    LeadContact = new LeadContactDTO() { MobileNo1 = leadInfo.Mobile },
                    RequestTypeID = byte.Parse(leadInfo.Referal_code),
                    LeadStatusID = 1,
                    LeadName =  leadInfo.Sname,
                    ReferalCode = leadInfo.Referal_code,
                    SchoolID = byte.Parse(leadInfo.SchoolID),
                    CurriculamID = 1

                });


                return Ok(true);
            }
            catch (Exception ex)
            {
                return Ok(new {IsError = true, Message = ex.Message });
                //return InternalServerError();
            }
        }

        [System.Web.Http.HttpPost]
        public IHttpActionResult AddLeadExternal(string Name, string Email, string Mobile, string Grade, string Query)
        {
            dynamic response = new { IsError = false, Message = "Successfully Registered" };
            try
            {

                // var dClass= ClientFactory.SchoolServiceClient(null).GetClassesBySchool()

                var rtnValue = ClientFactory.CRMServiceClient(null).AddLead(new LeadDTO()
                {
                    LeadTypeID = 1,
                    //DateOfBirth = Convert.ToDateTime(leadInfo.Dob1),
                    MobileNumber = Mobile,
                    //ParentName = leadInfo.Pname,
                    //GenderID = leadInfo.Gender == "M" ? (byte?)1 : (byte?)2,
                    EmailAddress = Email,
                    ClassName = Grade,
                    //                    ClassID = int.Parse(leadInfo.Grade),
                    StudentName = Name,
                    //AcademicYear = leadInfo.Academic_year,
                    //AcademicYearID = int.Parse(leadInfo.Academic_year),
                    LeadContact = new LeadContactDTO() { MobileNo1 = Mobile },
                    // RequestTypeID = byte.Parse(leadInfo.Referal_code),
                    LeadStatusID = 1,
                    LeadName = Grade,
                    //ReferalCode = leadInfo.Referal_code,
                    //SchoolID = byte.Parse(leadInfo.SchoolID),
                    CurriculamID = 1,
                    // LeadSourceID = int.Parse(leadInfo.Referal_code),
                    Remarks = Query,

                });


            }
            catch (Exception ex)
            {
                response = new { IsError = true, Message = ex.Message };

            }
            return Ok(true);
        }

        public IHttpActionResult GetAcademicYear(byte schoolID)
        {
            var academicyearList = ClientFactory.CRMServiceClient(null).GetAcademicYearCodeBySchool(schoolID);

            return Ok(academicyearList);
        }
        public IHttpActionResult GetClasses(byte schoolID)
        {
            var classes = ClientFactory.CRMServiceClient(null).GetClassesBySchool(schoolID, 0);

            return Ok(classes);
        }
        public IHttpActionResult GetLeadSource()
        {
            var sources = ClientFactory.CRMServiceClient(null).GetLeadSource();

            return Ok(sources);
        }

        public IHttpActionResult GetNationalities()
        {
            var sources = ClientFactory.CRMServiceClient(null).GetNationalities();

            return Ok(sources);
        }

        public IHttpActionResult GetDeafaultSchool()
        {
            var school = ClientFactory.CRMServiceClient(null).GetDeafaultSchool();

            return Ok(school);

        }
    }

}
