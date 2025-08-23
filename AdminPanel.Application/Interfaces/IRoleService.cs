using AdminPanel.Application.Dtos;
using AdminPanel.Data.Entities;
using AdminPanel.Data.Entities.Identity;
using AdminPanel.Data.Interfaces;
using Microsoft.AspNetCore.Identity;

namespace AdminPanel.Application.Interfaces
{
    public interface IRoleService
    {
        Task<IdentityResult> AddUserToRoleAsync(string userId, string roleName);
        Task<IList<string>> GetUserRolesAsync(string userId);
        Task<IdentityResult> RemoveUserFromRoleAsync(string userId, string roleName);
        Task<IReadOnlyList<UserDto>> GetAllWithSpec(ISpecification<User> spec);
        Task<int> Count();
        Task<int> CountWithSpecAsync(ISpecification<User> spec);
    }
}
