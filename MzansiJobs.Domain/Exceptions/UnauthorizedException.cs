namespace MzansiJobs.Domain.Exceptions
{
    public class UnauthorizedException : DomainException
    {
        public UnauthorizedException(string message = "Invalid authentication credentials.")
            : base(message)
        {
        }
    }
}
