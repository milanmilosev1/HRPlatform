using HRPlatform.Domain.DTOs.CandidateDTOs;

namespace HRPlatform.Domain.Services
{
    public interface ICandidateServices
    {
        Task<CandidateResponseDTO?> AddCandidateAsync(CreateCandidateDTO candidate);
        Task RemoveCandidateAsync(Guid id);
        Task<CandidateResponseDTO?> UpdateCandidateInfoAsync(UpdateCandidateDTO newCandidate);
        Task<CandidateResponseDTO?> GetCandidateByIdAsync(Guid id);
        Task<List<CandidateResponseDTO>> GetCandidatesAsync();
        Task<List<CandidateResponseDTO>> GetCandidatesByName(string name);
        Task<List<CandidateResponseDTO>> GetCandidatesBySkills(List<Guid> skillIds);
        Task<CandidateResponseDTO?> RemoveSkillFromCandidateAsync(Guid candidateId, string skillName);
    }
}
