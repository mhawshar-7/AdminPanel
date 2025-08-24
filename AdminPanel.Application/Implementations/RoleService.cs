using AdminPanel.Application.Dtos;
using AdminPanel.Application.Interfaces;
using AdminPanel.Data.Entities.Identity;
using AdminPanel.Data.Interfaces;
using AutoMapper;
using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AdminPanel.Application.Implementations
{
    public class RoleService : IRoleService
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;
        private readonly IRoleRepository _roleRepository;
        private readonly IUserRepository _userRepository;

        public RoleService(IUnitOfWork unitOfWork, IMapper mapper, IRoleRepository roleRepository, IUserRepository userRepository)
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
            _roleRepository = roleRepository;
            _userRepository = userRepository;
        }

        public async Task<IdentityResult> AddUserToRoleAsync(string userId, string roleName)
        {
            var user = await _userRepository.FindByIdAsync(userId);
            if (user == null)
            {
                return IdentityResult.Failed(new IdentityError { Description = "User not found." });
            }

            return await _roleRepository.AddToRoleAsync(user, roleName);
        }

        public Task<int> Count()
        {
            throw new NotImplementedException();
        }

        public Task<int> CountWithSpecAsync(ISpecification<User> spec)
        {
            throw new NotImplementedException();
        }

        public Task<IReadOnlyList<UserDto>> GetAllWithSpec(ISpecification<User> spec)
        {
            throw new NotImplementedException();
        }

        public async Task<IList<string>> GetUserRolesAsync(string userId)
        {
            throw new NotImplementedException();
        }

        public async Task<IdentityResult> RemoveUserFromRoleAsync(string userId, string roleName)
        {
            var user = await _userRepository.FindByIdAsync(userId);
            if (user == null)
            {
                return IdentityResult.Failed(new IdentityError { Description = "User not found." });
            }

            return await _roleRepository.RemoveFromRoleAsync(user, roleName);
        }
    }
}
