using MzansiJobs.Domain.Enums;

namespace MzansiJobs.Application.Common.DTOs.Applying
{
    public class JobApplicationDTO
    {
        public Guid ApplicationId { get; set; }
        public Guid CandidateId { get; set; }
        public string CandidateFirstName { get; set; } = string.Empty;
        public string CandidateLastName { get; set; } = string.Empty;
        public string? CandidateEmail { get; set; }
        public string? CoverLetter { get; set; }
        public DateTime AppliedDate { get; set; }
        public ApplicationStatus Status { get; set; }
    }
}
