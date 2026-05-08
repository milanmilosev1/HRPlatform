using HRPlatform.Domain.Models;

namespace HRPlatform.Domain.Repositories
{
    public interface ICandidateRepository
    {
        Task<Candidate> AddAsync(Candidate candidate);
        Task RemoveAsync(Guid candidateId);
        Task<Candidate> UpdateAsync(Candidate newCandidate);
        Task<Candidate> GetByIdAsync(Guid id);
        Task<List<Candidate>> GetAllAsync();
    }
}
