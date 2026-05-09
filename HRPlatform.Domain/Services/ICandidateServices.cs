using HRPlatform.Domain.Common;
using HRPlatform.Domain.DTOs.CandidateDTOs;

namespace HRPlatform.Domain.Services
{
    public interface ICandidateServices
    {
        Task<Result<CandidateResponseDTO>> AddCandidateAsync(CreateCandidateDTO candidate);
        Task<Result> RemoveCandidateAsync(Guid id);
        Task<Result<CandidateResponseDTO>> UpdateCandidateInfoAsync(UpdateCandidateDTO newCandidate);
        Task<Result<CandidateResponseDTO>> GetCandidateByIdAsync(Guid id);
        Task<Result<List<CandidateResponseDTO>>> GetCandidatesAsync();
        Task<Result<List<CandidateResponseDTO>>> GetCandidatesByName(string name);
        Task<Result<List<CandidateResponseDTO>>> GetCandidatesBySkills(List<Guid> skillIds);
        Task<Result<CandidateResponseDTO>> RemoveSkillFromCandidateAsync(Guid candidateId, string skillName);
    }
}
