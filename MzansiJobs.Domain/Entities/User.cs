using MzansiJobs.Domain.Enums;

namespace MzansiJobs.Domain.Entities
{
    public class User
    {
        public Guid ID { get; private set; } // primary key
        public string Email { get; private set; }
        public string passwordHash { get; private set; }
        public UserRole UserRole { get; private set; }
        public DateTime dateCreated { get; private set; }
        public ICollection<RefreshToken> RefreshTokens { get; private set; } = new List<RefreshToken>();

        public User(Guid id, string email, string passwordHash, UserRole userrole)
        {

            this.ID = id;
            this.Email = email;
            this.passwordHash = passwordHash;
            this.UserRole = userrole;
            dateCreated = DateTime.UtcNow;
        }

        public User(Guid id, string email, string passwordHash)
        {

            this.ID = id;
            this.Email = email;
            this.passwordHash = passwordHash;
            dateCreated = DateTime.UtcNow;

        }

        public User()
        {

        }







    }
}
