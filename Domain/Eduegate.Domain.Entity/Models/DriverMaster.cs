using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace Eduegate.Domain.Entity.Models
{
    public partial class DriverMaster
    {
        [Key]
        public int DriverMasterID { get; set; }
        public string EmployeeNo { get; set; }
        public string DriverCode { get; set; }
        public string DriverName { get; set; }
        public Nullable<System.DateTime> DateofJoin { get; set; }
        public Nullable<System.DateTime> DateofBirth { get; set; }
        public string PassportNo { get; set; }
        public Nullable<System.DateTime> PassportIssueDate { get; set; }
        public Nullable<System.DateTime> PassoprtExpiryDate { get; set; }
        public string DriverAddress { get; set; }
        public Nullable<int> ResidensyType { get; set; }
        public string ResidensyNo { get; set; }
        public Nullable<int> ResidensyStatus { get; set; }
        public string MobileNo { get; set; }
        public string LicenseNo { get; set; }
        public Nullable<System.DateTime> LicenseIssueDate { get; set; }
        public Nullable<System.DateTime> LicenseExpiryDate { get; set; }
        public string Education { get; set; }
        public Nullable<int> LicenseType { get; set; }
        public Nullable<int> BloodGroup { get; set; }
        public Nullable<int> Nationality { get; set; }
        public string ViolationHistory { get; set; }
        public System.DateTime CreatedDatetimeStamp { get; set; }
        public int CreatedByID { get; set; }
        public Nullable<System.DateTime> EditedDatetimeStamp { get; set; }
        public Nullable<int> EditedByID { get; set; }
        public bool IsActive { get; set; }
    }
}
