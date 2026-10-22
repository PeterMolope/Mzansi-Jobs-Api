namespace MzansiJobs.Application.Common.DTOs.Employers
{
    public class EmployerDTO
    {
        public Guid Id { get; set; }
        public Guid UserId { get; set; }
        public Guid CompanyId { get; set; }
        public string FirstName { get; set; } = string.Empty;
        public string LastName { get; set; } = string.Empty;
        public string Email { get; set; } = string.Empty;
        public string JobTitle { get; set; } = string.Empty;
        public string? Phone { get; set; }
        public string CompanyName { get; set; } = string.Empty;
    }
}
