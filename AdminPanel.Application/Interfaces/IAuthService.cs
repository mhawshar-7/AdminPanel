using AdminPanel.Application.Dtos;
using AdminPanel.Data.Entities;
using AdminPanel.Data.Entities.Identity;
using AdminPanel.Data.Interfaces;
using Microsoft.AspNetCore.Identity;

namespace AdminPanel.Application.Interfaces
{
    public interface IAuthService
    {
        Task<IdentityResult> RegisterAsync(RegisterDto registerDto);
        Task<IdentityResult> ResetPasswordAsync(string email, string token, string newPassword);
    }
}
