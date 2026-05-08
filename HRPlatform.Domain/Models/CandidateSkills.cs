namespace HRPlatform.Domain.Models
{
    public class CandidateSkills
    {
        public Guid CandidateId { get; set; }
        public Candidate Candidate { get; set; } = null!;
        public Guid SkillId { get; set; }
        public Skill Skill { get; set; } = null!;
    }
}
