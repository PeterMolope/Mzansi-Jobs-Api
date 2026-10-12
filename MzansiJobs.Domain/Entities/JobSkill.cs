namespace MzansiJobs.Domain.Entities
{
    internal class JobSkill
    {
        public Guid ID { get; private set; }
        public Guid JobID { get; private set; }
        public Job Job { get; private set; }
        public Skill Skill { get; private set; }
        public Guid SkillID { get; private set; }
        public ICollection<Job> jobs { get; private set; } = new List<Job>();
        public ICollection<Skill> skills { get; private set; } = new List<Skill>();
        public JobSkill(Guid jobId, Guid skillId)
        {
            ID = Guid.NewGuid();
            JobID = jobId;
            SkillID = skillId;
        }
        public JobSkill()
        {

        }
    }
}
