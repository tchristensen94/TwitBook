using System.Data.Entity;
using System.Security.Claims;
using System.Threading.Tasks;
using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.EntityFramework;
using System;
using System.Collections.Generic;

namespace TwitBook.Models
{
    // You can add profile data for the user by adding more properties to your ApplicationUser class, please visit http://go.microsoft.com/fwlink/?LinkID=317594 to learn more.
    public class ApplicationUser : IdentityUser
    {
        public async Task<ClaimsIdentity> GenerateUserIdentityAsync(UserManager<ApplicationUser> manager)
        {
            // Note the authenticationType must match the one defined in CookieAuthenticationOptions.AuthenticationType
            var userIdentity = await manager.CreateIdentityAsync(this, DefaultAuthenticationTypes.ApplicationCookie);
            // Add custom user claims here
            return userIdentity;
        }
        public String handle { get; set; }
        public ICollection<Twit> Twits { get; set; }
    }
    public class Twit
    {
        public int ID { get; set; }
        public string message { get; set; }
        public DateTime date { get; set; }

        public virtual ApplicationUser User { get; set; }
    }

    public class Message
    {
        public int ID { get; set; }
        public string subject { get; set; }
        public string message { get; set; }
        public DateTime date { get; set; }

        public virtual ApplicationUser To { get; set; }
        public virtual ApplicationUser From { get; set; }
    }

    public class ApplicationDbContext : IdentityDbContext<ApplicationUser>
    {
        public ApplicationDbContext()
            : base("DefaultConnection", throwIfV1Schema: false)
        {
        }
        public DbSet<Twit> Twits { get; set; }
        public DbSet<Message> Messages { get; set; }
        public static ApplicationDbContext Create()
        {
            return new ApplicationDbContext();
        }

        public System.Data.Entity.DbSet<TwitBook.Models.ApplicationUser> ApplicationUsers { get; set; }
    }
}