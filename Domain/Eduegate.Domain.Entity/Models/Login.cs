using Eduegate.Domain.Entity.Models.Mutual;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Eduegate.Domain.Entity.Models
{
    [Table("Logins", Schema = "admin")]
    public partial class Login
    {
        public Login()
        {
            this.ClaimLoginMaps = new List<ClaimLoginMap>();
            this.ClaimSetLoginMaps = new List<ClaimSetLoginMap>();
            this.LoginRoleMaps = new List<LoginRoleMap>();
            this.Contacts = new List<Contact>();
            this.Customers = new List<Customer>();
            this.Employees = new List<Employee>();
            this.FilterColumnUserValues = new List<FilterColumnUserValue>();
            this.NotificationAlerts = new List<NotificationAlert>();
            this.NotificationAlerts1 = new List<NotificationAlert>();
            this.UserDataFormatMaps = new List<UserDataFormatMap>();
            this.UserViews = new List<UserView>();
            this.StockNotifications = new List<StockNotification>();
            this.UserScreenFieldSettings = new HashSet<UserScreenFieldSetting>();
            this.CustomerCards = new HashSet<CustomerCard>();
            Parents = new HashSet<Parent>();
            UserDeviceMaps = new HashSet<UserDeviceMap>();
            Visitors = new HashSet<Visitor>();
            PaymentMasterVisas = new HashSet<PaymentMasterVisa>();
            PaymentLogs = new HashSet<PaymentLog>();
            Suppliers = new HashSet<Supplier>();
        }

        [Key]
        public long LoginIID { get; set; }
        public string LoginUserID { get; set; }
        public string LoginEmailID { get; set; }
        public string UserName { get; set; }
        public string PasswordSalt { get; set; }
        public string Password { get; set; }
        public string PasswordHint { get; set; }
        public string ProfileFile { get; set; }
        public Nullable<byte> StatusID { get; set; }
        public byte? SchoolID { get; set; }
        public Nullable<System.DateTime> LastLoginDate { get; set; }
        public Nullable<int> CreatedBy { get; set; }
        public Nullable<int> UpdatedBy { get; set; }
        public Nullable<System.DateTime> CreatedDate { get; set; }
        public Nullable<System.DateTime> UpdatedDate { get; set; }
        ////public byte[] TimeStamps { get; set; }
        public Nullable<int> RegisteredCountryID { get; set; }
        public string RegisteredIP { get; set; }
        public Nullable<int> SiteID { get; set; }
        public string RegisteredIPCountry { get; set; }
        public Nullable<bool> RequirePasswordReset { get; set; }
        public string LastOTP { get; set; }
        public long? EmployeeID { get; set; }

        public bool? IsForceLogout { get; set; }
        public bool? EnableForceLogout { get; set; }
        public bool? ExternalReference { get; set; }

        public long? SupplierID { get; set; }

        public virtual ICollection<ClaimLoginMap> ClaimLoginMaps { get; set; }
        public virtual ICollection<ClaimSetLoginMap> ClaimSetLoginMaps { get; set; }
        public virtual ICollection<LoginRoleMap> LoginRoleMaps { get; set; }
        public virtual ICollection<Contact> Contacts { get; set; }
        public virtual ICollection<Customer> Customers { get; set; }
        public virtual ICollection<Employee> Employees { get; set; }
        //public virtual ICollection<Site> Sites { get; set; }
        public virtual ICollection<FilterColumnUserValue> FilterColumnUserValues { get; set; }
        public virtual ICollection<NotificationAlert> NotificationAlerts { get; set; }
        public virtual ICollection<NotificationAlert> NotificationAlerts1 { get; set; }
        public virtual ICollection<Supplier> Suppliers { get; set; }
        public virtual ICollection<UserDataFormatMap> UserDataFormatMaps { get; set; }
        public virtual ICollection<UserView> UserViews { get; set; }
        public virtual ICollection<UserViewFilterMap> UserViewFilterMaps { get; set; }
        public virtual ICollection<StockNotification> StockNotifications { get; set; }
        public virtual ICollection<UserScreenFieldSetting> UserScreenFieldSettings { get; set; }
        public virtual ICollection<UserSetting> UserSettings { get; set; }
        public virtual ICollection<CustomerCard> CustomerCards { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<Parent> Parents { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<UserDeviceMap> UserDeviceMaps { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<Visitor> Visitors { get; set; }

        public virtual ICollection<PaymentMasterVisa> PaymentMasterVisas { get; set; }

        public virtual ICollection<PaymentLog> PaymentLogs { get; set; }

    }
}