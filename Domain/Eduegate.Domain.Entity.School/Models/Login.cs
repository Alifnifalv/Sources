namespace Eduegate.Domain.Entity.School.Models
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;

    [Table("Logins", Schema = "admin")]
    public partial class Login
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public Login()
        {
            //ClaimLoginMaps = new HashSet<ClaimLoginMap>();
            //ClaimSetLoginMaps = new HashSet<ClaimSetLoginMap>();
            LoginRoleMaps = new HashSet<LoginRoleMap>();
            //CustomerCards = new HashSet<CustomerCard>();
            //Customers = new HashSet<Customer>();
            Employees = new HashSet<Employee>();
            //FilterColumnUserValues = new HashSet<FilterColumnUserValue>();
            //NotificationAlerts = new HashSet<NotificationAlert>();
            //NotificationAlerts1 = new HashSet<NotificationAlert>();
            Parents = new HashSet<Parent>();
            //ShareHolders = new HashSet<ShareHolder>();
            StudentApplications = new HashSet<StudentApplication>();
            Students = new HashSet<Student>();
            //Suppliers = new HashSet<Supplier>();
            //TaskAssingners = new HashSet<TaskAssingner>();
            //UserDataFormatMaps = new HashSet<UserDataFormatMap>();
            //UserScreenFieldSettings = new HashSet<UserScreenFieldSetting>();
            //UserSettings = new HashSet<UserSetting>();
            //UserViews = new HashSet<UserView>();
            NotificationAlerts = new HashSet<NotificationAlert>();
            NotificationAlerts1 = new HashSet<NotificationAlert>();

            Visitors = new HashSet<Visitor>();
        }

        [Key]
        public long LoginIID { get; set; }

        [StringLength(100)]
        public string LoginUserID { get; set; }

        [StringLength(100)]
        public string LoginEmailID { get; set; }

        [StringLength(200)]
        public string UserName { get; set; }

        [StringLength(1000)]
        public string ProfileFile { get; set; }

        public long? CustomerID { get; set; }

        public long? SupplierID { get; set; }

        public long? EmployeeID { get; set; }

        [StringLength(50)]
        public string PasswordHint { get; set; }

        [StringLength(128)]
        public string PasswordSalt { get; set; }

        [StringLength(128)]
        public string Password { get; set; }

        public byte? StatusID { get; set; }

        public int? RegisteredCountryID { get; set; }

        [StringLength(20)]
        public string RegisteredIP { get; set; }

        [StringLength(20)]
        public string RegisteredIPCountry { get; set; }

        public DateTime? LastLoginDate { get; set; }

        public bool? RequirePasswordReset { get; set; }

        public int? SiteID { get; set; }

        [StringLength(50)]
        public string LastOTP { get; set; }

        public int? CreatedBy { get; set; }

        public int? UpdatedBy { get; set; }

        public DateTime? CreatedDate { get; set; }

        public DateTime? UpdatedDate { get; set; }

        public byte? SchoolID { get; set; }

        //[Column(TypeName = "timestamp")]
        //[DatabaseGenerated(DatabaseGeneratedOption.Computed)]
        //[MaxLength(8)]
        ////public byte[] TimeStamps { get; set; }

        //[System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        //public virtual ICollection<ClaimLoginMap> ClaimLoginMaps { get; set; }

        //[System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        //public virtual ICollection<ClaimSetLoginMap> ClaimSetLoginMaps { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<LoginRoleMap> LoginRoleMaps { get; set; }

        //[System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        //public virtual ICollection<CustomerCard> CustomerCards { get; set; }

        //[System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        //public virtual ICollection<Customer> Customers { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<Employee> Employees { get; set; }

        //[System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        //public virtual ICollection<FilterColumnUserValue> FilterColumnUserValues { get; set; }

        public virtual Country Country { get; set; }

        //[System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        //public virtual ICollection<NotificationAlert> NotificationAlerts { get; set; }

        //[System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        //public virtual ICollection<NotificationAlert> NotificationAlerts1 { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<Parent> Parents { get; set; }

        //[System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        //public virtual ICollection<ShareHolder> ShareHolders { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<StudentApplication> StudentApplications { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<Student> Students { get; set; }
        public virtual Schools School { get; set; }

        //[System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        //public virtual ICollection<Supplier> Suppliers { get; set; }

        //[System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        //public virtual ICollection<TaskAssingner> TaskAssingners { get; set; }

        //[System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        //public virtual ICollection<UserDataFormatMap> UserDataFormatMaps { get; set; }

        //[System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        //public virtual ICollection<UserScreenFieldSetting> UserScreenFieldSettings { get; set; }

        //[System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        //public virtual ICollection<UserSetting> UserSettings { get; set; }

        //[System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        //public virtual ICollection<UserView> UserViews { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<NotificationAlert> NotificationAlerts { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<NotificationAlert> NotificationAlerts1 { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<Visitor> Visitors { get; set; }
    }
}
