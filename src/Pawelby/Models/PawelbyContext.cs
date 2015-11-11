#region Usings
    using Microsoft.AspNet.Identity.EntityFramework;
    using Microsoft.Data.Entity;
#endregion

namespace Pawelby.Models
{
    /// <summary>
    /// Main Context class of the project
    /// </summary>
    public class PawelbyContext : IdentityDbContext<PawelbyUser>
    {
        public PawelbyContext()
        {
            Database.EnsureCreated();
        }

#region Properties
        public DbSet<Trip> Trips { get; set; }
        public DbSet<Stop> Stops { get; set; }
#endregion

        /// <summary>
        /// Configuring of the context
        /// </summary>
        /// <param name="optionsBuilder">Options builder</param>
        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            var connString = Startup.Configuration["Data:PawelByContextConnection"];

            optionsBuilder.UseSqlServer(connString);

            base.OnConfiguring(optionsBuilder);
        }
    }

}
