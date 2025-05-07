using Microsoft.EntityFrameworkCore;
using App.Models;

namespace App.DataBase
{
    public class AppDbContext : DbContext
    {
        public AppDbContext(DbContextOptions<AppDbContext> options) : base(options) { }

        public DbSet<User> User { get; set; }
        public DbSet<Session> Session { get; set; }
        public DbSet<JobVacancy> JobVacancy { get; set; }
        public DbSet<VacancyStatus> VacancyStatus { get; set; }
        public DbSet<MetaInfo> MetaInfo { get; set; }


        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<User>()
            .HasKey(u => u.Id);
            modelBuilder.Entity<Session>()
            .HasKey(s => s.Id);
            modelBuilder.Entity<JobVacancy>()
            .HasKey(j => j.Id);
            modelBuilder.Entity<VacancyStatus>()
            .HasKey(v => v.Id);
            modelBuilder.Entity<MetaInfo>()
            .HasKey(mi => mi.ProviderId);

            modelBuilder.Entity<User>()
            .HasIndex(u => u.UserName).IsUnique();
            modelBuilder.Entity<User>()
            .HasIndex(u => u.Email).IsUnique();

            modelBuilder.Entity<Session>()
            .HasOne(s => s.User)
            .WithOne(u => u.Session)
            .HasForeignKey<Session>(s => s.UserId);

            modelBuilder.Entity<JobVacancy>()
            .HasOne(j => j.VacancyStatus)
            .WithOne(v => v.JobVacancy)
            .HasForeignKey<JobVacancy>(j => j.VacancyStatusId);

            modelBuilder.Entity<User>()
            .HasMany(u => u.JobVacancy)
            .WithOne(j => j.User)
            .HasForeignKey(u => u.UserId);

            modelBuilder.Entity<MetaInfo>()
            .HasOne(mi => mi.User)
            .WithOne(u => u.MetaInfo)
            .HasForeignKey<MetaInfo>(u => u.UserId);
        }
    }
}