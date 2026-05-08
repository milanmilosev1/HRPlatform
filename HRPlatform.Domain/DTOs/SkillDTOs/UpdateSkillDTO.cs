namespace HRPlatform.Domain.DTOs.SkillDTOs
{
    public class UpdateSkillDTO
    {
        public Guid Id { get; set; }
        public string Name { get; set; } = string.Empty;
        public List<Guid> CandidateIds { get; set; } = [];
    }
}
