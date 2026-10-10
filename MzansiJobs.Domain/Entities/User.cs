namespace MzansiJobs.Domain.Entities
{
    internal class User
    {
        public int ID { get; private set; }
        public string Email { get; private set; }
        public string passwordHash { get; private set; }
        public string UserRole { get; private set; }

        public User(int id, string email, string passwordHash, string userrole)
        {

            this.ID = id;
            this.Email = email;
            this.passwordHash = passwordHash;
            this.UserRole = userrole;
        }

        public User(int id, string email, string passwordHash)
        {

            this.ID = id;
            this.Email = email;
            this.passwordHash = passwordHash;

        }

        public User()
        {

        }







    }
}
