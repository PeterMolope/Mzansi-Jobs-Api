namespace MzansiJobs.Domain.Entities
{
    public class Employer
    {
        public Guid ID { get; private set; }
        public User User { get; private set; }
        public Company Company { get; private set; }
        public Guid UserID { get; private set; }
        public Guid CompanyID { get; private set; }
        public string firstName { get; private set; }
        public string? phoneNumber { get; private set; }
        public string surname { get; private set; }
        public string jobTitle { get; private set; }
        public Employer()
        {
        }

        public Employer(Guid UserID, Guid CompanyID, string firstName, string surname, string jobTitle)
        {
            ID = Guid.NewGuid();
            this.UserID = UserID;
            this.CompanyID = CompanyID;
            this.firstName = firstName;
            this.surname = surname;
            this.jobTitle = jobTitle;
        }
    }
}
