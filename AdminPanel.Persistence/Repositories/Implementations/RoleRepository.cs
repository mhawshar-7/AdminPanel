using AdminPanel.Data.Entities.Identity;
using AdminPanel.Persistence.Repositories.Interfaces;
using Microsoft.AspNetCore.Identity;

namespace AdminPanel.Persistence.Repositories.Implementations
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