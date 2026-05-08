using HRPlatform.Domain.Models;
using Microsoft.EntityFrameworkCore;

namespace HRPlatform.Data
{
    public class AppDbContext : DbContext
    {
        public DbSet<Candidate> Candidates { get; set; }
        public DbSet<Skill> Skills { get; set; }
    }
}
