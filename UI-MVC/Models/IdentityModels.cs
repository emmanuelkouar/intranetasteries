using System.Data.Entity;
using System.Security.Claims;
using System.Threading.Tasks;
using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.EntityFramework;
using UI_MVC.Domain;

namespace UI_MVC.Models
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
    }

    public class ApplicationDbContext : IdentityDbContext<ApplicationUser>
    {
        public ApplicationDbContext()
            : base("DefaultConnection", throwIfV1Schema: false)
        {
            Database.SetInitializer(new MySqlInitializer());
        }

        public static ApplicationDbContext Create()
        {
            return new ApplicationDbContext();
        }

        public DbSet<Member> Members { get; set; }
        public DbSet<MedicalVisit> MedicalVisits { get; set; }
        public DbSet<Subscription> Subscriptions { get; set; }
        public DbSet<CFPS> CFPSs { get; set; }
        public DbSet<DiveLicense> DiveLicenses { get; set; }
        public DbSet<SpecialisationLicense> SpecialisationLicenses { get; set; }
        public DbSet<ApneaLicense>  ApneaLicenses { get; set; }
        public DbSet<ICE> ICEs { get; set; }
        public DbSet<MemberFunction> MemberFunctions { get; set; }

        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            modelBuilder.HasDefaultSchema("asteries");
            base.OnModelCreating(modelBuilder);
            
            //PRIMARY KEYS
            modelBuilder.Entity<Member>().HasKey(p => p.MemberId);
            modelBuilder.Entity<MedicalVisit>().HasKey(p => p.MedicalVisitId);
            modelBuilder.Entity<ECG>().HasKey(p => p.ECGId);
            modelBuilder.Entity<Subscription>().HasKey(p => p.SubscriptionId);
            modelBuilder.Entity<CFPS>().HasKey(p => p.CFPSId);
            modelBuilder.Entity<DiveLicense>().HasKey(p => p.DiveLicenseId);
            modelBuilder.Entity<SpecialisationLicense>().HasKey(p => p.SpecialisationLicenseId);
            modelBuilder.Entity<ApneaLicense>().HasKey(p => p.ApneaLicenseId);
            modelBuilder.Entity<ICE>().HasKey(p => p.ICEId);
            modelBuilder.Entity<MemberFunction>().HasKey(p => p.MemberFunctionId);

            //FOREIGN KEYS
            modelBuilder.Entity<Member>().HasMany<MedicalVisit>(f => f.MedicalVisits).WithRequired(x => x.MemberId);
            modelBuilder.Entity<Member>().HasMany<ECG>(f => f.Ecgs).WithRequired(x => x.MemberId);
            modelBuilder.Entity<Member>().HasMany<Subscription>(f => f.Subscriptions).WithRequired(x => x.MemberId);
            modelBuilder.Entity<Member>().HasMany<CFPS>(f => f.CFPSs).WithRequired(x => x.MemberId);
            modelBuilder.Entity<Member>().HasMany<DiveLicense>(f => f.DiveLicenses).WithRequired(x => x.MemberId);
            modelBuilder.Entity<Member>().HasMany<SpecialisationLicense>(f => f.SpecialisationLicenses).WithRequired(x => x.MemberId);
            modelBuilder.Entity<Member>().HasMany<ApneaLicense>(f => f.ApneaLicenses).WithRequired(x => x.MemberId);
            modelBuilder.Entity<Member>().HasMany<ICE>(f => f.ICEs).WithRequired(x => x.MemberId);
            modelBuilder.Entity<Member>().HasMany<MemberFunction>(f => f.MemberFunctions).WithRequired(x => x.MemberId);
        }

        public System.Data.Entity.DbSet<UI_MVC.Domain.ECG> ECGs { get; set; }
    }
}