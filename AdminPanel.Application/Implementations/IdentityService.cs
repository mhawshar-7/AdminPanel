using AdminPanel.Application.Dtos;
using AdminPanel.Application.Interfaces;
using AdminPanel.Data.Entities;
using AdminPanel.Data.Entities.Identity;
using AdminPanel.Data.Interfaces;
using AutoMapper;
using Microsoft.AspNetCore.Identity;

namespace AdminPanel.Application.Implementations
{
    public class IdentityService : IIdentityService
    {
        private readonly IMapper _mapper;
        private readonly IIdentityRepository _identityRepository;

        public IdentityService(IMapper mapper, IIdentityRepository identityRepository)
        {
            _mapper = mapper;
            _identityRepository = identityRepository;
        }

        public async Task<IdentityResult> RegisterAsync(RegisterDto registerDto)
        {
            var user = new User
            {
                UserName = registerDto.Username,
                Email = registerDto.Email,
                FirstName = registerDto.FirstName,
                LastName = registerDto.LastName
            };

            return await _identityRepository.CreateAsync(user, registerDto.Password);
        }

        public async Task<IReadOnlyList<UserDto>> GetAllUsersAsync()
        {
            var users = await _identityRepository.GetAllUsersAsync();
            return _mapper.Map<IReadOnlyList<UserDto>>(users);
        }

        public async Task<UserDto> GetUserByIdAsync(string userId)
        {
            var user = await _identityRepository.FindByIdAsync(userId);
            return _mapper.Map<UserDto>(user);
        }

        public async Task<IdentityResult> AddUserToRoleAsync(string userId, string roleName)
        {
            var user = await _identityRepository.FindByIdAsync(userId);
            if (user == null)
            {
                return IdentityResult.Failed(new IdentityError { Description = "User not found." });
            }

            return await _identityRepository.AddToRoleAsync(user, roleName);
        }

        public async Task<IdentityResult> RemoveUserFromRoleAsync(string userId, string roleName)
        {
            var user = await _identityRepository.FindByIdAsync(userId);
            if (user == null)
            {
                return IdentityResult.Failed(new IdentityError { Description = "User not found." });
            }

            return await _identityRepository.RemoveFromRoleAsync(user, roleName);
        }

        public async Task<IList<string>> GetUserRolesAsync(string userId)
        {
            var user = await _identityRepository.FindByIdAsync(userId);
            if (user == null)
            {
                return new List<string>();
            }

            return await _identityRepository.GetRolesAsync(user);
        }

        public async Task<IdentityResult> ChangePasswordAsync(string userId, string oldPassword, string newPassword)
        {
            var user = await _identityRepository.FindByIdAsync(userId);
            if (user == null)
            {
                return IdentityResult.Failed(new IdentityError { Description = "User not found." });
            }

            return await _identityRepository.ChangePasswordAsync(user, oldPassword, newPassword);
        }

        public async Task<IdentityResult> ResetPasswordAsync(string email, string token, string newPassword)
        {
            var user = await _identityRepository.FindByEmailAsync(email);
            if (user == null)
            {
                // Don't reveal that the user does not exist
                return IdentityResult.Success;
            }

            return await _identityRepository.ResetPasswordAsync(user, token, newPassword);
        }

        public async Task<IdentityResult> DeleteUserAsync(string userId)
        {
            var user = await _identityRepository.FindByIdAsync(userId);
            if (user == null)
            {
                return IdentityResult.Failed(new IdentityError { Description = "User not found." });
            }

            user.IsDeleted = true;
            return await _identityRepository.UpdateAsync(user);
        }

        public async Task<IdentityResult> UpdateUserAsync(UserDto userDto)
        {
            var user = await _identityRepository.FindByIdAsync(userDto.Id);
            if (user == null)
            {
                return IdentityResult.Failed(new IdentityError { Description = "User not found." });
            }

            user.FirstName = userDto.FirstName;
            user.LastName = userDto.LastName;
            user.Email = userDto.Email;
            user.UserName = userDto.UserName;

            return await _identityRepository.UpdateAsync(user);
        }

        public async Task<IReadOnlyList<UserDto>> GetAllWithSpec(ISpecification<User> spec)
        {
            var list = await _identityRepository.ListWithSpecAsync(spec);
            return _mapper.Map<IReadOnlyList<UserDto>>(list);
        }

        public async Task<int> Count()
        {
            return await _identityRepository.CountActiveAsync();
        }

        public async Task<int> CountWithSpecAsync(ISpecification<User> spec)
        {
            return await _identityRepository.CountAsync(spec);
        }
    }
}