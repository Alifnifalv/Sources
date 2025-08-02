using Eduegate.Domain.Entity.Models;
using Eduegate.Framework.Contracts.Common;
using System;
using System.Collections.Generic;
using System.Runtime.Serialization;

namespace Eduegate.Services.Contracts.Jobs
{
    public class RegisterUserDTO : Eduegate.Framework.Contracts.Common.BaseMasterDTO
    {
        public RegisterUserDTO()
        {
            Gender = new KeyValueDTO();
            Qualification = new KeyValueDTO();
            Country = new KeyValueDTO();
            Nationality = new KeyValueDTO();
            PassportIssueCountry = new KeyValueDTO();
            Skill = new List<KeyValueDTO>();
        }

        [DataMember]
        public string FirstName { get; set; }
        [DataMember]
        public string MiddleName { get; set; }
        [DataMember]
        public string LastName { get; set; }
        [DataMember]
        public string EmailID { get; set; }       
        [DataMember]
        public string TelephoneNo { get; set; }
        [DataMember]
        public string OldPassword { get; set; }
        [DataMember]
        public string Password { get; set; }       
        [DataMember]
        public string ConfirmPassword { get; set; }
        [DataMember]
        public string PasswordSalt { get; set; }
        [DataMember]
        public bool? IsError { get; set; }
        [DataMember]
        public string ReturnMessage { get; set; }   
        
        [DataMember]
        public string OTP { get; set; }


        //For MyProfile 
        [DataMember]
        public long SeekerID { get; set; } 
        [DataMember]
        public byte? GenderID { get; set; } 
        [DataMember]
        public KeyValueDTO Gender { get; set; }
        [DataMember]
        public string PhoneNumber { get; set; }
        [DataMember]
        public string MobileNumber { get; set; }
        [DataMember]
        public string Education { get; set; }      
        [DataMember]
        public int? TotalWorkExperience { get; set; }         
        [DataMember]
        public string ReferenceCode { get; set; }
        [DataMember]
        public string Facebook { get; set; }
        [DataMember]
        public string Twitter { get; set; }
        [DataMember]
        public string LinkedIn { get; set; }
        [DataMember]
        public string Instagram { get; set; }
        [DataMember]
        public long? ProfileContentID { get; set; }
        [DataMember]
        public long? CVContentID { get; set; }
        [DataMember]
        public string DateOfBirthString { get; set; }
        [DataMember]
        public int? CountryID { get; set; }  
        [DataMember]
        public int? NationalityID { get; set; }  
        [DataMember]
        public byte? QualificationID { get; set; } 
        [DataMember]
        public KeyValueDTO Qualification { get; set; }

        [DataMember]
        public KeyValueDTO Country { get; set; }  
        
        [DataMember]
        public KeyValueDTO Nationality { get; set; } 
        
        [DataMember]
        public List<KeyValueDTO> Skill { get; set; }

        [DataMember]
        public byte? Age { get; set; }
        [DataMember]
        public KeyValueDTO BloodGroup { get; set; }
        [DataMember]
        public string PassportNumber { get; set; }
        [DataMember]
        public string PassportExpiryString { get; set; }
        [DataMember]
        public KeyValueDTO PassportIssueCountry { get; set; }
        [DataMember]
        public string NationalID { get; set; }

        //For Login
        [DataMember]
        public long? LoginID { get; set; }
        [DataMember]
        public string UserID { get; set; }
        [DataMember]
        public string UserName { get; set; }  
        [DataMember]
        public bool? IsActive { get; set; }   
        [DataMember]
        public bool? PasswordUpdate { get; set; } 

    }
}
