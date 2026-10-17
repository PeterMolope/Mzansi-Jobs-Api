using MzansiJobs.Domain.Enums;

namespace MzansiJobs.Domain.Entities
{
    public class Job
    {
        public Guid ID { get; private set; }
        public string Title { get; private set; }
        public string Description { get; private set; }
        public Province Province { get; private set; }
        public EmploymentType EmploymentType { get; private set; }
        public Guid CompanyID { get; private set; }
        public Company Company { get; private set; }
        public decimal? salaryRange { get; private set; }
        public DateTime postedDate { get; private set; }
        public DateTime? expirationDate { get; private set; }
        public ICollection<Application> applications { get; private set; } = new List<Application>();
        public ICollection<JobSkill> jobSkills { get; private set; } = new List<JobSkill>();
        public bool isActive { get; private set; }

        public Job() { }

        public Job(string title, string description, Province province, EmploymentType employmentType, Guid companyId, decimal? salary = null)
        {
            ID = Guid.NewGuid();
            Title = title;
            Description = description;
            Province = province;
            EmploymentType = employmentType;
            CompanyID = companyId;
            salaryRange = salary;
            postedDate = DateTime.UtcNow;
            isActive = true;
        }


    }
}
