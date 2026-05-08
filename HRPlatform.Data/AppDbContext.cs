using HRPlatform.Domain.Models;
using Microsoft.EntityFrameworkCore;

namespace HRPlatform.Data
{
    public class AppDbContext(DbContextOptions<AppDbContext> options) : DbContext(options)
    {
        public DbSet<Candidate> Candidates { get; set; }
        public DbSet<Skill> Skills { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            // Candidate constraints
            modelBuilder.Entity<Candidate>(entity =>
            {
                entity.HasKey(c => c.Id);

                entity.Property(c => c.Name).IsRequired()
                    .HasMaxLength(100);

                entity.Property(c => c.Email).IsRequired()
                    .HasMaxLength(255);

                entity.HasIndex(c => c.Email).IsUnique();

                entity.Property(c => c.ContactNumber).IsRequired()
                    .HasMaxLength(20);

                entity.ToTable(t => t.HasCheckConstraint("CK_Candidate_ContactNumber",
                        "\"ContactNumber\" ~ '^\\+3816[0-9] ?[0-9]{6,7}$'"));

                entity.Property(c => c.DateOfBirth).IsRequired();
            });

            // Skill constraints
            modelBuilder.Entity<Skill>(entity =>
            {
                entity.HasKey(s => s.Id);

                entity.Property(s => s.Name).IsRequired()
                    .HasMaxLength(100);

                entity.HasIndex(s => s.Name).IsUnique();

                entity.ToTable(t => t.HasCheckConstraint(
                    "CK_Skill_Name_NotEmpty", "\"Name\" <> ''"));
            });

            // CandidateSkills join table
            modelBuilder.Entity<CandidateSkills>(entity =>
            {
                entity.HasKey(cs => new { cs.CandidateId, cs.SkillId });

                entity.HasOne(cs => cs.Candidate).WithMany(c => c.CandidateSkills)
                    .HasForeignKey(cs => cs.CandidateId)
                    .OnDelete(DeleteBehavior.Cascade);

                entity.HasOne(cs => cs.Skill).WithMany(s => s.CandidateSkills)
                    .HasForeignKey(cs => cs.SkillId)
                    .OnDelete(DeleteBehavior.Cascade);
            });
        }
    }
}
