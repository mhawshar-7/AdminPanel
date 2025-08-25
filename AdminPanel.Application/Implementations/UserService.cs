using AdminPanel.Application.Dtos;
using AdminPanel.Application.Interfaces;
using AdminPanel.Data.Entities;
using AdminPanel.Data.Entities.Identity;
using AdminPanel.Data.Interfaces;
using AdminPanel.Persistence.Repositories.Interfaces;
using AutoMapper;
using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AdminPanel.Application.Implementations
{
    public class UserService : IUserService
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;
        private readonly IUserRepository _userRepository;

        public UserService(IUnitOfWork unitOfWork, IMapper mapper, IUserRepository userRepository)
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
            _userRepository = userRepository;
        }

        public async Task<IdentityResult> ChangePasswordAsync(string userId, string oldPassword, string newPassword)
        {
            var user = await _userRepository.FindByIdAsync(userId);
            if (user == null)
            {
                return IdentityResult.Failed(new IdentityError { Description = "User not found." });
            }

            return await _userRepository.ChangePasswordAsync(user, oldPassword, newPassword);
        }

        public async Task<int> Count()
        {
            var count = await _unitOfWork.Repository<User>().Count();
            return count;
        }

        public async Task<int> CountWithSpecAsync(ISpecification<User> spec)
        {
            return await _unitOfWork.Repository<User>().CountAsync(spec);
        }

        public async Task<IdentityResult> CreateUserAsync(RegisterDto registerDto)
        {
            var user = new User
            {
                UserName = registerDto.Username,
                Email = registerDto.Email,
                FirstName = registerDto.FirstName,
                LastName = registerDto.LastName
            };

            return await _userRepository.CreateAsync(user, registerDto.Password);
        }

        public async Task<IdentityResult> DeleteUserAsync(string userId)
        {
            var user = await _userRepository.FindByIdAsync(userId);
            if (user == null)
            {
                return IdentityResult.Failed(new IdentityError { Description = "User not found." });
            }

            user.IsDeleted = true;
            return await _userRepository.UpdateAsync(user);
        }

        public async Task<IReadOnlyList<UserDto>> GetAllUsersAsync()
        {
            var users = await _userRepository.GetAllUsersAsync();
            return _mapper.Map<IReadOnlyList<UserDto>>(users);
        }

        public async Task<IReadOnlyList<UserDto>> GetAllWithSpec(ISpecification<User> spec)
        {
            var list = await _unitOfWork.Repository<User>().ListWithSpecAsync(spec);
            return _mapper.Map<IReadOnlyList<UserDto>>(list);
        }

        public async Task<UserDto> GetUserByIdAsync(string userId)
        {
            var user = await _userRepository.FindByIdAsync(userId);
            return _mapper.Map<UserDto>(user);
        }

        public async Task<IdentityResult> UpdateUserAsync(UserDto userDto)
        {
            var user = await _userRepository.FindByIdAsync(userDto.Id);
            if (user == null)
            {
                return IdentityResult.Failed(new IdentityError { Description = "User not found." });
            }

            user.FirstName = userDto.FirstName;
            user.LastName = userDto.LastName;
            user.Email = userDto.Email;
            user.UserName = userDto.UserName;

            return await _userRepository.UpdateAsync(user);
        }
    }
}
