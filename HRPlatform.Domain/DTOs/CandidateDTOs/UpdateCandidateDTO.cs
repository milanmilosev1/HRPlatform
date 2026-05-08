namespace HRPlatform.Domain.DTOs.CandidateDTOs
{
    public class UpdateCandidateDTO
    {
        public Guid Id { get; set; }
        public string Name { get; set; } = string.Empty;
        public DateOnly DateOfBirth { get; set; }
        public string ContactNumber { get; set; } = string.Empty;
        public string Email { get; set; } = string.Empty;
        public List<Guid> SkillIds { get; set; } = [];
    }
}
