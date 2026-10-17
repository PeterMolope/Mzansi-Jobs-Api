namespace MzansiJobs.Domain.Entities
{
    public class Candidate
    {
        public User User { get; private set; }
        public Guid ID { get; private set; } // primary key
        public Guid UserID { get; private set; } // foreign key
        public string firstName { get; private set; }
        public string surname { get; private set; }
        public string? CVUrl { get; private set; }
        public string? phoneNumber { get; private set; }
        public string? bio { get; private set; }

        public ICollection<Application> applications { get; private set; } = new List<Application>();
        public Candidate()
        {

        }

        public Candidate(Guid UserID, string firstName, string surname)
        {
            ID = Guid.NewGuid();
            this.UserID = UserID;
            this.firstName = firstName;
            this.surname = surname;
        }
    }
}
