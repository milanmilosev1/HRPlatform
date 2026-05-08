using HRPlatform.Domain.Models;
using Microsoft.EntityFrameworkCore;

namespace HRPlatform.Data
{
    public class AppDbContext : DbContext
    {
        DbSet<Candidate> Candidates { get; set; }
        DbSet<Skill> Skills { get; set; }
    }
}
