using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using ServicePlatform.DataLayer.Models;

namespace ServicePlatform.DataLayer
{
    public class ApiContext : IdentityDbContext
    {
        public ApiContext(DbContextOptions<ApiContext> options) : base(options)
        {

        }

        public DbSet<AccountUser> AccountUsers { get; set; }

        public DbSet<ServiceListing> ServiceListings { get; set; }

        public DbSet<Assessment> Assessments { get; set; }

        public DbSet<Service> Services { get; set; }
    }
}
