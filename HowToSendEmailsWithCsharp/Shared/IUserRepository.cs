namespace Shared
{
    public interface IUserRepository
    {
        public Task<User> FindByEmailAsync(string key);
    }
}
