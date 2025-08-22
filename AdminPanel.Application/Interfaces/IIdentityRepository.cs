using AdminPanel.Data.Entities.Identity;
using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AdminPanel.Data.Interfaces;

namespace AdminPanel.Application.Interfaces
{
    public interface IIdentityRepository
    {
        Task<IdentityResult> CreateAsync(User user, string password);
        Task<IdentityResult> UpdateAsync(User user);
        Task<User> FindByIdAsync(string userId);
        Task<User> FindByEmailAsync(string email);
        Task<User> FindByNameAsync(string username);
        Task<IReadOnlyList<User>> GetAllUsersAsync();
        Task<IdentityResult> AddToRoleAsync(User user, string roleName);
        Task<IdentityResult> RemoveFromRoleAsync(User user, string roleName);
        Task<IList<string>> GetRolesAsync(User user);
        Task<IdentityResult> ChangePasswordAsync(User user, string oldPassword, string newPassword);
        Task<IdentityResult> ResetPasswordAsync(User user, string token, string newPassword);
        Task<IReadOnlyList<User>> ListWithSpecAsync(ISpecification<User> spec);
        Task<int> CountAsync(ISpecification<User> spec);
        Task<int> CountActiveAsync();
    }
}
