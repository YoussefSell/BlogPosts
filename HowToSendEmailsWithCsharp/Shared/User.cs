namespace Shared
{
    public class User
    {
        public User(string fullName, string email)
        {
            Email = email ?? throw new ArgumentNullException(nameof(email));
            FullName = fullName ?? throw new ArgumentNullException(nameof(fullName));
        }

        public int Id { get; set; }

        public string FullName { get; set; }

        public string Email { get; set; }
    }
}