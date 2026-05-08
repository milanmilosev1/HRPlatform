using HRPlatform.Domain.Models;
using HRPlatform.Domain.Repositories;
using Microsoft.EntityFrameworkCore;

namespace HRPlatform.Data.Repositories.CandidateRepository
{
    public class CandidateRepository(AppDbContext context) : ICandidateRepository
    {
        private readonly AppDbContext _context = context;

        public async Task<Candidate?> AddAsync(Candidate candidate)
        {
            await _context.Candidates.AddAsync(candidate);
            await _context.SaveChangesAsync();
            return candidate;
        }

        public async Task<List<Candidate>> GetAllAsync()
        {
            return await _context.Candidates.ToListAsync();
        }

        public async Task<Candidate?> GetByIdAsync(Guid id)
        {
            return await _context.Candidates.FindAsync(id);
        }

        public async Task RemoveAsync(Candidate candidate)
        {
            _context.Remove(candidate);
            await _context.SaveChangesAsync();
        }   
    }
}
