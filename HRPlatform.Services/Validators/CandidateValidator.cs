using HRPlatform.Domain.Models;

namespace HRPlatform.Services.Validators
{
    public class CandidateValidationResults(string message)
    {
        public string Message { get; set; } = message;
    }

    public static class CandidateValidator
    {
        public static CandidateValidationResults Validate(Candidate candidate)
        {
            if (candidate is null)
                return new("Candidate is null");

            if (candidate.Id == Guid.Empty)
                return new("Candidate ID not set");

            if (candidate.Name.Equals(string.Empty))
                return new("Candidate name not provided");

            if (candidate.Email.Equals(string.Empty))
                return new("Candidate email not provided");

            if (candidate.ContactNumber.Equals(string.Empty))
                return new("Candidate contact number not provided");

            return new(string.Empty);
        }    
    }
}
