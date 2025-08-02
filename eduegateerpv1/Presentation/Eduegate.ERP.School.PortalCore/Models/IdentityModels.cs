using Microsoft.AspNetCore.Identity;
//using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace Eduegate.ERP.Admin.Models
{
    // You can add profile data for the user by adding more properties to your ApplicationUser class, please visit http://go.microsoft.com/fwlink/?LinkID=317594 to learn more.
    public class ApplicationUser : IdentityUser
    {
        //TODO: Check if this is needed
        //public async Task<ClaimsIdentity> GenerateUserIdentityAsync(UserManager<ApplicationUser> manager)
        //{
        //    // Note the authenticationType must match the one defined in CookieAuthenticationOptions.AuthenticationType
        //    var userIdentity = await manager.CreateIdentityAsync(this, DefaultAuthenticationTypes.ApplicationCookie);
        //    // Add custom user claims here
        //    return userIdentity;
        //}
    }

    //public class ApplicationDbContext : IdentityDbContext<ApplicationUser>
    //{
    //    //TODO: Check later
    //    //public ApplicationDbContext()
    //    //    : base("DefaultConnection", throwIfV1Schema: false)
    //    //{
    //    //}

    //    public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options)
    //        : base(options)
    //    {
    //    }

    //    //public static ApplicationDbContext Create()
    //    //{
    //    //    return new ApplicationDbContext();
    //    //}
    //}
}