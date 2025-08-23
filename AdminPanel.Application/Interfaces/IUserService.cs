using AdminPanel.Application.Dtos;
using AdminPanel.Data.Entities;
using AdminPanel.Data.Entities.Identity;
using AdminPanel.Data.Interfaces;
using Microsoft.AspNetCore.Identity;

namespace AdminPanel.Application.Interfaces
{
    public interface IUserService
    {
        Task<IReadOnlyList<UserDto>> GetAllUsersAsync();
        Task<UserDto> GetUserByIdAsync(string userId);
        Task<IdentityResult> CreateUserAsync(RegisterDto registerDto);
        Task<IdentityResult> UpdateUserAsync(UserDto userDto);
        Task<IdentityResult> DeleteUserAsync(string userId);
        Task<IReadOnlyList<UserDto>> GetAllWithSpec(ISpecification<User> spec);
        Task<int> Count();
        Task<int> CountWithSpecAsync(ISpecification<User> spec);
        Task<IdentityResult> ChangePasswordAsync(string userId, string oldPassword, string newPassword);
    }
}
