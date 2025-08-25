using AdminPanel.Data.Entities.Identity;
using Microsoft.AspNetCore.Identity;

namespace AdminPanel.Persistence.Repositories.Interfaces
{
    public interface IAuthRepository
    {
        Task<IdentityResult> ResetPasswordAsync(User user, string token, string newPassword);
    }
}
