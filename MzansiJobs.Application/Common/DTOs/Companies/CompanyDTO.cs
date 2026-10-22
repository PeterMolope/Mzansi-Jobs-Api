using MzansiJobs.Domain.Enums;

namespace MzansiJobs.Application.Common.DTOs.Companies
{
    public class CompanyDTO
    {
        public Guid Id { get; set; }
        public string Name { get; set; } = string.Empty;
        public string? Description { get; set; }
        public Province Province { get; set; }
        public Industry Industry { get; set; }
        public string? Website { get; set; }
        public string? LogoUrl { get; set; }
    }
}
