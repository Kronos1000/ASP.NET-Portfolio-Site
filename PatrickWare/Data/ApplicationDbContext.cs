using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using PatrickWare.Models;

namespace PatrickWare.Data
{
    public class ApplicationDbContext : IdentityDbContext
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options)
            : base(options)
        {
        }
        public DbSet<PatrickWare.Models.About>? About { get; set; }
        public DbSet<PatrickWare.Models.Contact>? Contact { get; set; }
        public DbSet<PatrickWare.Models.PreviousExperience>? PreviousExperience { get; set; }
        public DbSet<PatrickWare.Models.Services>? Services { get; set; }
        public DbSet<PatrickWare.Models.SoftSkills>? SoftSkills { get; set; }
        public DbSet<PatrickWare.Models.TechnicalSkills>? TechnicalSkills { get; set; }
    }
}