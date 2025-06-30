namespace TimeTrackerAPI.Data
{
    public class DataContext : DbContext
    {
        public DataContext(DbContextOptions<DataContext> options) : base(options)
        {

        }
        public DbSet<TimeEntry> TimeEntries { get; set; } = null!;
        //protected override void OnModelCreating(ModelBuilder modelBuilder)
        //{
        //    modelBuilder.Entity<TimeTracker.Shared.Entities.TimeEntry>()
        //        .Property(t => t.Project)
        //        .IsRequired()
        //        .HasMaxLength(100);

        //    modelBuilder.Entity<TimeTracker.Shared.Entities.TimeEntry>()
        //        .Property(t => t.Start)
        //        .IsRequired();
        //    modelBuilder.Entity<TimeTracker.Shared.Entities.TimeEntry>()
        //        .Property(t => t.End)
        //        .IsRequired();
        //}
    }
}
