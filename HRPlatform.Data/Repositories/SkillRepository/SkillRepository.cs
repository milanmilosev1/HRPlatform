using HRPlatform.Domain.Models;
using HRPlatform.Domain.Repositories;
using Microsoft.EntityFrameworkCore;

namespace HRPlatform.Data.Repositories.SkillRepository
{
    public class SkillRepository(AppDbContext context) : ISkillRepository
    {
        private readonly AppDbContext _context = context;
        public async Task<Skill> AddAsync(Skill skill)
        {
            await _context.Skills.AddAsync(skill);
            await _context.SaveChangesAsync();
            return skill;
        }

        public async Task<List<Skill>> GetAllAsync()
        {
            return await _context.Skills.Include(s => s.CandidateSkills).ToListAsync();
        }

        public async Task<Skill?> GetByIdAsync(Guid id)
        {
            return await _context.Skills.Include(s => s.CandidateSkills).FirstOrDefaultAsync(s => s.Id == id);
        }

        public async Task RemoveAsync(Skill skill)
        {
            _context.Remove(skill);
            await _context.SaveChangesAsync();
        }

        public async Task SaveChangesAsync()
        {
            await _context.SaveChangesAsync();
        }
    }
}
