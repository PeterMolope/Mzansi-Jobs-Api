using MzansiJobs.Domain.Enums;

namespace MzansiJobs.Domain.Entities
{
    public class Company
    {


        public Guid ID { get; private set; }
        public string Name { get; private set; }
        public string? Description { get; private set; }
        public Province Province { get; private set; }
        public string? Website { get; private set; }
        public string? logoUrl { get; private set; }
        public Industry Industry { get; private set; }

        public ICollection<Employer> employers { get; private set; } = new List<Employer>();
        public ICollection<Job> jobs { get; private set; } = new List<Job>();
        public Company(string Name, string description, Province province, Industry industry)
        {
            ID = Guid.NewGuid();
            this.Name = Name;
            Description = description;
            Province = province;
            Industry = industry;
        }
        public Company()
        {
        }
    }
}
