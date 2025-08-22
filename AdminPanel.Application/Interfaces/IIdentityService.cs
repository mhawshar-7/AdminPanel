using AdminPanel.Application.Dtos;
using AdminPanel.Data.Entities;
using AdminPanel.Data.Entities.Identity;
using AdminPanel.Data.Interfaces;
using Microsoft.AspNetCore.Identity;

namespace AdminPanel.Application.Interfaces
{
    public interface IIdentityService
    {
        Task<IdentityResult> RegisterAsync(RegisterDto registerDto);
        Task<IReadOnlyList<UserDto>> GetAllUsersAsync();
        Task<IReadOnlyList<UserDto>> GetAllWithSpec(ISpecification<User> spec);
        Task<int> Count();
        Task<int> CountWithSpecAsync(ISpecification<User> spec);
        Task<UserDto> GetUserByIdAsync(string userId);
        Task<IdentityResult> AddUserToRoleAsync(string userId, string roleName);
        Task<IdentityResult> RemoveUserFromRoleAsync(string userId, string roleName);
        Task<IList<string>> GetUserRolesAsync(string userId);
        Task<IdentityResult> ChangePasswordAsync(string userId, string oldPassword, string newPassword);
        Task<IdentityResult> ResetPasswordAsync(string email, string token, string newPassword);
        Task<IdentityResult> DeleteUserAsync(string userId);
        Task<IdentityResult> UpdateUserAsync(UserDto userDto);
    }
}
