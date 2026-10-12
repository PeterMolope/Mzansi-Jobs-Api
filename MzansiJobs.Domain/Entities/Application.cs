using MzansiJobs.Domain.Enums;

namespace MzansiJobs.Domain.Entities
{
    internal class Application
    {
        public Guid ID { get; private set; }
        public Job Job { get; private set; }
        public Guid JobID { get; private set; }
        public Candidate Candidate { get; private set; }
        public Guid CandidateID { get; private set; }
        public ApplicationStatus Status { get; private set; }
        public DateTime dateApplied { get; private set; }
        public string? coverLetter { get; private set; }

        public Application(Guid jobId, Guid candidateId, string? coverLetter = null)
        {
            ID = Guid.NewGuid();
            JobID = jobId;
            CandidateID = candidateId;
            coverLetter = coverLetter;
            dateApplied = DateTime.UtcNow;
            Status = ApplicationStatus.Submitted;
        }

        public void UpdateStatus(ApplicationStatus Status)
        {
            this.Status = Status;
        }

    }
}
