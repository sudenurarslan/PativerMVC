using Microsoft.EntityFrameworkCore;

namespace PativerMVC.Models
{
    public class ApplicationDbContext : DbContext
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options)
            : base(options)
        {
        }

        public DbSet<Animal> Animals { get; set; }
        public DbSet<Report> Reports { get; set; }
        public DbSet<Campaign> Campaigns { get; set; }
        public DbSet<Volunteer> Volunteers { get; set; }
        public DbSet<AdoptionRequest> AdoptionRequests { get; set; }
        public DbSet<Donation> Donations { get; set; }
        
        



    }
}