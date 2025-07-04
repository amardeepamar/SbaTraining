using System.Data.Entity;

namespace EplannerClone.DomainModels
{
    public class EplannerDbDbContext:DbContext
    {
        public EplannerDbDbContext()
        : base("EplannerDbDbContext") // ✅ Make sure this matches the connection string name
        {
        }
        public DbSet<User> Users { get; set; }
        public DbSet<Role> Roles { get; set; }
        public DbSet<Ac> AcTable { get; set; }
        public DbSet<District> Districts { get; set; }
        public DbSet<Remark> Remarks { get; set; }
        public DbSet<Activity> Activities { get; set; }
        public DbSet<PollDay> PollDays { get; set; }
        public DbSet<PollDaySubLayer> PollDaySubLayers { get; set; }


    }
}
