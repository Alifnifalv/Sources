using System;
using System.Collections.Generic;

namespace Eduegate.Domain.Entity.Models
{
    public partial class Login
    {
        public Login()
        {
            this.ClaimLoginMaps = new List<ClaimLoginMap>();
            this.ClaimSetLoginMaps = new List<ClaimSetLoginMap>();
            this.LoginRoleMaps = new List<LoginRoleMap>();
            this.Employees = new List<Employee>();
            this.FilterColumnUserValues = new List<FilterColumnUserValue>();
            this.NotificationAlerts = new List<NotificationAlert>();
            this.NotificationAlerts1 = new List<NotificationAlert>();
            this.Suppliers = new List<Supplier>();
            this.UserDataFormatMaps = new List<UserDataFormatMap>();
            this.UserViews = new List<UserView>();
        }

        public long LoginIID { get; set; }
        public string LoginUserID { get; set; }
        public string LoginEmailID { get; set; }
        public Nullable<long> CustomerID { get; set; }
        public Nullable<long> SupplierID { get; set; }
        public Nullable<long> EmployeeID { get; set; }
        public string PasswordHint { get; set; }
        public string PasswordSalt { get; set; }
        public string Password { get; set; }
        public Nullable<byte> StatusID { get; set; }
        public Nullable<int> RegisteredCountryID { get; set; }
        public string RegisteredIP { get; set; }
        public string RegisteredIPCountry { get; set; }
        public Nullable<System.DateTime> LastLoginDate { get; set; }
        public Nullable<bool> RequirePasswordReset { get; set; }
        public Nullable<int> SiteID { get; set; }
        public Nullable<long> CreatedBy { get; set; }
        public Nullable<long> UpdatedBy { get; set; }
        public Nullable<System.DateTime> CreatedDate { get; set; }
        public Nullable<System.DateTime> UpdatedDate { get; set; }
        public byte[] TimeStamps { get; set; }
        public virtual ICollection<ClaimLoginMap> ClaimLoginMaps { get; set; }
        public virtual ICollection<ClaimSetLoginMap> ClaimSetLoginMaps { get; set; }
        public virtual ICollection<LoginRoleMap> LoginRoleMaps { get; set; }
        public virtual ICollection<Employee> Employees { get; set; }
        public virtual ICollection<FilterColumnUserValue> FilterColumnUserValues { get; set; }
        public virtual Country Country { get; set; }
        public virtual ICollection<NotificationAlert> NotificationAlerts { get; set; }
        public virtual ICollection<NotificationAlert> NotificationAlerts1 { get; set; }
        public virtual ICollection<Supplier> Suppliers { get; set; }
        public virtual ICollection<UserDataFormatMap> UserDataFormatMaps { get; set; }
        public virtual ICollection<UserView> UserViews { get; set; }
    }
}
