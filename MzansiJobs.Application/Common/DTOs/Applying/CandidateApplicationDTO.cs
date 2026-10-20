using MzansiJobs.Domain.Enums;

namespace MzansiJobs.Application.Common.DTOs.Applying
{
    public class CandidateApplicationDTO
    {
        public Guid ApplicationId { get; set; }
        public Guid JobId { get; set; }
        public string JobTitle { get; set; } = string.Empty;
        public string? CompanyName { get; set; }
        public Province Province { get; set; }
        public EmploymentType EmploymentType { get; set; }
        public string? CoverLetter { get; set; }
        public DateTime AppliedDate { get; set; }
        public ApplicationStatus Status { get; set; }
    }
}
