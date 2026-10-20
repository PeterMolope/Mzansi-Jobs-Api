using MzansiJobs.Domain.Enums;

namespace MzansiJobs.Application.Common.DTOs.Jobs
{
    public class JobDetailsDTO
    {
        public Guid Id { get; set; }
        public string Title { get; set; } = string.Empty;
        public string Description { get; set; } = string.Empty;
        public Province Province { get; set; }
        public EmploymentType EmploymentType { get; set; }
        public Guid CompanyId { get; set; }
        public string? CompanyName { get; set; }
        public decimal? SalaryRange { get; set; }
        public DateTime PostedDate { get; set; }
        public DateTime? ExpirationDate { get; set; }
        public bool IsActive { get; set; }
    }
}
