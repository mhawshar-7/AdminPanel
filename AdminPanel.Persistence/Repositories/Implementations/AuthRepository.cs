using AdminPanel.Data.Entities.Identity;
using AdminPanel.Persistence.Repositories.Interfaces;
using Microsoft.AspNetCore.Identity;

namespace AdminPanel.Persistence.Repositories.Implementations
{
    public class AuthRepository: IAuthRepository
    {
        private readonly UserManager<User> _userManager;

        public AuthRepository(UserManager<User> userManager)
        {
            _userManager = userManager;
        }

        public async Task<IdentityResult> ResetPasswordAsync(User user, string token, string newPassword)
        {
            return await _userManager.ResetPasswordAsync(user, token, newPassword);
        }
    }
}
