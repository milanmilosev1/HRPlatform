using HRPlatform.Domain.Models;

namespace HRPlatform.Domain.Services
{
    public interface ICandidateServices
    {
        Task<Candidate> AddCandidateAsync(Candidate candidate);
        Task RemoveCandidateAsync(Guid id);
        Task<Candidate> UpdateCandidateInfoAsync(Candidate newCandidate);
        Task<Candidate> GetCandidateByIdAsync(Guid id);
        Task<List<Candidate>> GetCandidatesAsync();
    }
}
