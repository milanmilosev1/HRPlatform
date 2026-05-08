using HRPlatform.Domain.DTOs.SkillDTOs;

namespace HRPlatform.Domain.DTOs.CandidateDTOs
{
    public class CandidateResponseDTO
    {
        public Guid Id { get; set; }
        public string Name { get; set; } = string.Empty;
        public string Email { get; set; } = string.Empty;
        public string ContactNumber { get; set; } = string.Empty;
        public DateOnly DateOfBirth { get; set; }
        public List<SkillResponseDTO> Skills { get; set; } = [];
    }
}
