namespace MzansiJobs.Domain.Entities
{
    internal class RefreshToken
    {
        public Guid ID { get; private set; }

        public Guid UserID { get; private set; }
        public User User { get; private set; }
        public string Token { get; private set; }
        public DateTime createdAt { get; private set; }
        public DateTime expiresOn { get; private set; }

        public bool isRevoked { get; private set; }
        public bool isUsed { get; private set; }

        public bool IsExpired => DateTime.UtcNow >= expiresOn;
        public bool IsActive => !isRevoked && !IsExpired && !isUsed;

        public RefreshToken()
        {

        }

        public RefreshToken(string token, Guid userId, DateTime expiresOn)
        {
            ID = Guid.NewGuid();
            Token = token;
            UserID = userId;
            expiresOn = expiresOn;
            isRevoked = false;
            isUsed = false;
        }

        // Domain methods for modifying state safely
        public void Revoke()
        {
            isRevoked = true;
        }

        public void MarkAsUsed()
        {
            isUsed = true;
        }


    }
}
