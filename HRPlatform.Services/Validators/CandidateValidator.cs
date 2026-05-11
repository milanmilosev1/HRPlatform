using System.Text.RegularExpressions;
using HRPlatform.Domain.Models;

namespace HRPlatform.Services.Validators
{
    public class CandidateValidationResults(string message)
    {
        public string Message { get; set; } = message;
    }

    public static partial class CandidateValidator
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

            if (!ContactNumberRegex().IsMatch(candidate.ContactNumber))
                return new("Candidate contact number format is invalid");

            return new(string.Empty);
        }

        [GeneratedRegex(@"^\+3816[0-9] ?[0-9]{6,7}$")]
        private static partial Regex ContactNumberRegex();
    }
}
