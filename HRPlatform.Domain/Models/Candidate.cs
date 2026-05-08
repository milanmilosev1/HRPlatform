namespace HRPlatform.Domain.Models
{
    public class Candidate
    {
        public Guid Id { get; set; }
        public string Name { get; set; } = string.Empty;
        public DateOnly DateOfBirth { get; set; }
        public string ContactNumber { get; set; } = string.Empty;
        public string Email { get; set; } = string.Empty;
        public Guid SkillId { get; set; }
    }
}
