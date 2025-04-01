using BlogPost.Api.Models;

namespace BlogPost.Api.Interfaces

{
    public interface IUserRepository
    {
        public Task<User?> GetByEmailAsync(string user);
        public Task<User?> GetByIdAsync(Guid id);
        public Task<IEnumerable<User>> GetAllAsync();
        public Task<User> CreateAsync(User user);
    }
}
