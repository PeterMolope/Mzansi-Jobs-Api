using MzansiJobs.Domain.Enums;

namespace MzansiJobs.Application.Common.DTOs.Jobs
{
    public class CreateJobRequestDTO
    {
        public string Title { get; set; } = string.Empty;
        public string Description { get; set; } = string.Empty;
        public Province Province { get; set; }
        public EmploymentType EmploymentType { get; set; }
        public Guid CompanyID { get; set; }
        public decimal? SalaryRange { get; set; }
    }
}
