using HRPlatform.Domain.DTOs.CandidateDTOs;
using HRPlatform.Domain.Models;

namespace HRPlatform.Domain.Services
{
    public interface ICandidateServices
    {
        Task<Candidate> AddCandidateAsync(CreateCandidateDTO candidate);
        Task RemoveCandidateAsync(Guid id);
        Task<Candidate> UpdateCandidateInfoAsync(UpdateCandidateDTO newCandidate);
        Task<Candidate> GetCandidateByIdAsync(Guid id);
        Task<List<Candidate>> GetCandidatesAsync();
    }
}
