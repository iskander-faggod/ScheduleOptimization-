using Microsoft.EntityFrameworkCore;
using ScheduleOptimization.Models;

namespace ScheduleOptimization.Data.Context
{
    public sealed class ScheduleContext : DbContext
    {
        public DbSet<Entry> Entries { get; set; } = null!;
        public DbSet<Group> Groups { get; set; } = null;
        public DbSet<Lesson> Lessons { get; set; } = null;
        public DbSet<Person> Persons { get; set; } = null;

        public ScheduleContext(DbContextOptions<ScheduleContext> options) : base(options)
        {
            Database.EnsureCreated();
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {

        }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            base.OnConfiguring(optionsBuilder);
        }
    }
}
