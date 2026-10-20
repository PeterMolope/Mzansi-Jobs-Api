namespace MzansiJobs.Application.Common.DTOs.Candidates
{
    public class CandidateProfileDTO
    {
        public Guid Id { get; set; }
        public Guid UserId { get; set; }
        public string FirstName { get; set; } = string.Empty;
        public string LastName { get; set; } = string.Empty;
        public string Email { get; set; } = string.Empty;
        public string? Phone { get; set; }
        public string? CVUrl { get; set; }
        public string? Bio { get; set; }
    }
}
