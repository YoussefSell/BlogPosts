namespace Shared
{
    public class UserRepository : IUserRepository
    {
        private readonly User _testUser = new("test user", "test@email.net");

        public Task<User> FindByEmailAsync(string key) 
            => Task.FromResult(_testUser);
    }
}
