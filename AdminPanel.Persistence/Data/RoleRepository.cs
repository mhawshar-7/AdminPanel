using AdminPanel.Application.Interfaces;
using AdminPanel.Data.Entities.Identity;
using Microsoft.AspNetCore.Identity;

namespace AdminPanel.Persistence.Data
{
    public class RoleRepository: IRoleRepository
    {
        private readonly UserManager<User> _userManager;

        public RoleRepository(UserManager<User> userManager)
        {
            _userManager = userManager;
        }

        public async Task<IdentityResult> AddToRoleAsync(User user, string roleName)
        {
            return await _userManager.AddToRoleAsync(user, roleName);
        }

        public async Task<IdentityResult> RemoveFromRoleAsync(User user, string roleName)
        {
            return await _userManager.RemoveFromRoleAsync(user, roleName);
        }
    }
}