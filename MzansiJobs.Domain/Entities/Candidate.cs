namespace MzansiJobs.Domain.Entities
{
    internal class Candidate : User
    {
        public User User { get; private set; }
        public string firstName { get; private set; }
        public string surname { get; private set; }
        public string? CVUrl { get; private set; }
        public string? bio { get; private set; }

        public ICollection<Application> applications { get; private set; }
        public Candidate()
        {

        }

        public Candidate(int id, string email, string passwordHash, string userrole)
        {

        }
    }
}
