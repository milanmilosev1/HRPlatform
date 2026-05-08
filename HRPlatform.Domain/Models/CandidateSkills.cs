namespace HRPlatform.Domain.Models
{
    public class CandidateSkills
    {
        public Guid CandidateId { get; set; }
        public Candidate Candidate { get; set; } = new();
        public Guid SkillId { get; set; }
        public Skill Skill { get; set; } = new();
    }
}
