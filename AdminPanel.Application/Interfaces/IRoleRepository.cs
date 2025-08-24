using AdminPanel.Data.Entities.Identity;
using Microsoft.AspNetCore.Identity;
using AdminPanel.Data.Interfaces;

namespace AdminPanel.Application.Interfaces
{
    public interface IRoleRepository
    {
        Task<IdentityResult> AddToRoleAsync(User user, string roleName);
        Task<IdentityResult> RemoveFromRoleAsync(User user, string roleName);
    }
}
