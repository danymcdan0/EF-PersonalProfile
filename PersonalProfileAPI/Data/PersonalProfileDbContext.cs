using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using PersonalProfileAPI.Models.Domains;

namespace PersonalProfileAPI.Data
{
    public class PersonalProfileDbContext : IdentityDbContext
	{
		public PersonalProfileDbContext(DbContextOptions<PersonalProfileDbContext> dbContextOptions) : base(dbContextOptions)
        {

		}

		public DbSet<Education> Education { get; set; }
		public DbSet<Experience> Experiences { get; set; }
		public DbSet<Project> Projects { get; set; }
		public DbSet<Owner> Owners { get; set; }
    }
}
