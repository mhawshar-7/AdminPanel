using AdminPanel.Data.Entities.Identity;
using Microsoft.AspNetCore.Identity;
using AdminPanel.Data.Interfaces;

namespace AdminPanel.Application.Interfaces
{
    public interface IUserRepository
    {
        Task<IdentityResult> CreateAsync(User user, string password);
        Task<IdentityResult> UpdateAsync(User user);
        Task<User> FindByIdAsync(string userId);
        Task<User> FindByEmailAsync(string email);
        Task<User> FindByNameAsync(string username);
        Task<IReadOnlyList<User>> GetAllUsersAsync();
        Task<IdentityResult> ChangePasswordAsync(User user, string oldPassword, string newPassword);
    }
}
