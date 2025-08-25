using AdminPanel.Data.Entities.Identity;
using Microsoft.AspNetCore.Identity;

namespace AdminPanel.Persistence.Repositories.Interfaces
{
    public interface IRoleRepository
    {
        Task<IdentityResult> AddToRoleAsync(User user, string roleName);
        Task<IdentityResult> RemoveFromRoleAsync(User user, string roleName);
    }
}
