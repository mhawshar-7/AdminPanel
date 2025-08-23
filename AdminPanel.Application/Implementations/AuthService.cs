using AdminPanel.Application.Dtos;
using AdminPanel.Application.Interfaces;
using AdminPanel.Data.Entities;
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
    public class AuthService : IAuthService
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;
        private readonly IUserRepository _userRepository;

        public AuthService(IUnitOfWork unitOfWork, IMapper mapper, IUserRepository userRepository)
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
            _userRepository = userRepository;
        }

        public Task<IdentityResult> RegisterAsync(RegisterDto registerDto)
        {
            throw new NotImplementedException();
        }

        public Task<IdentityResult> ResetPasswordAsync(string email, string token, string newPassword)
        {
            throw new NotImplementedException();
        }
    }
}
