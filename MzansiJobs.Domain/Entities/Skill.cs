namespace MzansiJobs.Domain.Entities
{
    internal class Skill
    {

        public Guid ID { get; private set; }
        public string Name { get; private set; }
        public ICollection<Job> jobs { get; private set; } = new List<Job>();
        public ICollection<JobSkill> jobSkills { get; private set; } = new List<JobSkill>();
        public Skill(string name)
        {
            ID = Guid.NewGuid();
            Name = name;
        }
        public Skill()
        {

        }
    }
}
